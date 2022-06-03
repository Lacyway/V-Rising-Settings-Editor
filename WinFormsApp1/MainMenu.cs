﻿using System;
using System.Windows.Forms;
using System.Net;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using System.Timers;
using System.Threading.Tasks;
using ServerManager.RCON;

namespace ServerManager
{
        public partial class MainMenu : Form
        {

        public Process serverProcess = new Process();
        private static System.Timers.Timer RestartTimer;
        public bool userStopped = false;
        public static string serverName = "V Rising Server";
        public static string saveName = "world1";
        public static int restartAttempts = 0;
        System.Windows.Forms.Timer ucTimer = new System.Windows.Forms.Timer();

        public MainMenu()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo;
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.Save_Path == "notset")
            {
                Properties.Settings.Default.Save_Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\LocalLow\Stunlock Studios\VRisingServer");
                Properties.Settings.Default.Save();
            }
            MainMenuConsole.AppendText("V Rising Server Manager Started\nServer Path: " + Properties.Settings.Default.Server_Path);
            ServerNameValue.Text = Properties.Settings.Default.Server_Name;
            SaveNameValue.Text = Properties.Settings.Default.Save_Name;
            ucTimer.Tick += AutoUpdateElapsed;
            Properties.Settings.Default.SettingChanging += (sender, e) =>
            {
                if (e.SettingName == "AutoUpdate")
                {
                    if (e.NewValue.ToString() == "True")
                    {
                        UpdateTimer();
                    }
                    else if (ucTimer.Enabled) ucTimer.Stop();
                }
            };
            if (Properties.Settings.Default.LastUpdateUNIXTime != "")
            {
                LastUpdateLabel.Text = "Last Update on Steam: " + DateTimeOffset.FromUnixTimeSeconds(long.Parse(Properties.Settings.Default.LastUpdateUNIXTime)).DateTime.ToString();
            } 
            if (Properties.Settings.Default.AutoUpdate == true) UpdateTimer();
            CheckServer();
        }

        private void UpdateTimer()
        {
            ucTimer.Interval = Properties.Settings.Default.AutoUpdateInterval * 60000;
            ucTimer.Interval = 5000;
        }

        private async void AutoUpdateElapsed(object? sender, EventArgs e)
        {
            bool updateFound = await CheckForUpdate();
            if (updateFound == true) AutoUpdateServer();
        }

        public async void AutoUpdateServer()
        {
            userStopped = true;
            if (Properties.Settings.Default.AutoUpdateRCONMessage) await SendRestartMessage();
            Process[] processList = Process.GetProcessesByName("vrisingserver");
            foreach (Process proc in processList)
            {
                if (proc.MainModule.FileName == Properties.Settings.Default.Server_Path + "\\VRisingServer.exe")
                {
                    MainMenuConsole.AppendText(Environment.NewLine + "Terminating server for update.");
                    proc.CloseMainWindow();
                    proc.WaitForExit();
                    MainMenuConsole.AppendText(Environment.NewLine + "Server terminated.");
                }
            }
            await UpdateGame();
            StartServer();
        }

        public async Task SendRestartMessage()
        {
            RemoteConClient rClient = new RemoteConClient();
            rClient.UseUtf8 = true;
            rClient.OnLog += async message =>
            {
                if (message == "Authentication success.")
                {
                    await Task.Delay(1000);
                    rClient.SendCommand("announcerestart 5", result =>
                    {
                        MainMenuConsole.AppendText(result);
                    });
                }
                
            };
            rClient.OnConnectionStateChange += state =>
            {
                if (state == RemoteConClient.ConnectionStateChange.Connected)
                {
                    rClient.Authenticate(Properties.Settings.Default.RCON_Pass);
                }
            };
            rClient.Connect(Properties.Settings.Default.RCON_Address, Properties.Settings.Default.RCON_Port);
            MainMenuConsole.AppendText(Environment.NewLine + "Waiting 5 minutes before quitting...");     
            await Task.Delay(300000);
            rClient.Disconnect();
        }

        public void CheckServer()
        {
            Process[] processList = Process.GetProcessesByName("vrisingserver");
            bool foundServer = false;
            foreach (Process proc in processList)
            {
                if (proc.MainModule.FileName == Properties.Settings.Default.Server_Path + "\\VRisingServer.exe")
                {
                    Process serverProcess = proc;
                    serverProcess.EnableRaisingEvents = true;
                    serverProcess.Exited += new EventHandler(serverProcessExited);
                    foundServer = true;
                }
            }
            if (foundServer == true)
            {
                StartGameServerButton.Enabled = false;
                StopGameServerButton.Enabled = true;
                StoppedPic.Visible = false;
                RunningPic.Visible = true;
                StatusLabel.Text = "Running";
                MainMenuConsole.AppendText(Environment.NewLine + "Server found running.");
            }
        }
        
        public async Task UpdateGame()
        {
            SteamCMDStatusLabel.ForeColor = System.Drawing.Color.Black;
            if (Process.GetProcessesByName("vrisingserver").Length > 0)
            {
                MessageBox.Show("Server is already running. Shut down the server before updating.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (File.Exists(Properties.Settings.Default.Server_Path + "\\SteamCMD\\steamcmd.exe") == false)
            {
                SteamCMDStatusLabel.Text = "SteamCMD: Downloading...";
                MainMenuConsole.AppendText(Environment.NewLine + "SteamCMD not found. Downloading...");
                using (WebClient wc = new WebClient())
                {
                    Uri SteamCMDLink = new Uri("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip");
                    Directory.CreateDirectory(Properties.Settings.Default.Server_Path);
                    MainMenuConsole.AppendText(Environment.NewLine + "Checking if folder exists...");
                    await wc.DownloadFileTaskAsync(SteamCMDLink, Properties.Settings.Default.Server_Path + "\\steamcmd.zip");
                }
                if (File.Exists(Properties.Settings.Default.Server_Path + "\\SteamCMD\\steamcmd.exe") == true)
                {
                    File.Delete(Properties.Settings.Default.Server_Path + "\\SteamCMD\\steamcmd.exe");
                }
                MainMenuConsole.AppendText(Environment.NewLine + "Unzipping...");
                ZipFile.ExtractToDirectory(Properties.Settings.Default.Server_Path + "\\steamcmd.zip", Properties.Settings.Default.Server_Path + "\\SteamCMD");
                MainMenuConsole.AppendText(Environment.NewLine + "Done!");
                if (File.Exists(Properties.Settings.Default.Server_Path + "\\steamcmd.zip"))
                {
                    File.Delete(Properties.Settings.Default.Server_Path + "\\steamcmd.zip");
                }
            }
            else
            {
                SteamCMDStatusLabel.Text = "SteamCMD: Running...";
                MainMenuConsole.AppendText(Environment.NewLine + "SteamCMD found. Running...");
            }
            if (File.Exists(Properties.Settings.Default.Server_Path + "\\VRisingServer.exe") == false)
            {
                SteamCMDStatusLabel.Text = "SteamCMD: Downloading game...";
                MainMenuConsole.AppendText(Environment.NewLine + "Server not found. Install required.");
                string Verify = (Properties.Settings.Default.VerifyUpdate) ? "validate " : "";
                string parameters = String.Format("+force_install_dir {0} +login anonymous +app_update 1829350 {1}+quit", Properties.Settings.Default.Server_Path, Verify);
                var steamcmd = Process.Start(Properties.Settings.Default.Server_Path + "\\SteamCMD\\steamcmd.exe", parameters);
                await steamcmd.WaitForExitAsync();
                MainMenuConsole.AppendText(Environment.NewLine + "Install completed.");
            }
            else
            {
                SteamCMDStatusLabel.Text = "SteamCMD: Updating game...";
                MainMenuConsole.AppendText(Environment.NewLine + "Server found. Updating.");
                string Verify = (Properties.Settings.Default.VerifyUpdate) ? "validate " : "";
                string parameters = String.Format("+force_install_dir {0} +login anonymous +app_update 1829350 {1}+quit", Properties.Settings.Default.Server_Path, Verify);
                var steamcmd = Process.Start(Properties.Settings.Default.Server_Path + "\\SteamCMD\\steamcmd.exe", parameters);
                await steamcmd.WaitForExitAsync();
                MainMenuConsole.AppendText(Environment.NewLine + "Update completed.");
            }
            SteamCMDStatusLabel.Text = "SteamCMD Status: Not running";
        }

        private async Task<bool> CheckForUpdate()
        {
            bool foundUpdate = false;
            await Task.Run(() =>
            {
                
                if (File.Exists(Properties.Settings.Default.Server_Path + "\\SteamCMD\\steamcmd.exe") == false)
                {
                    SteamCMDStatusLabel.ForeColor = System.Drawing.Color.Red;
                    SteamCMDStatusLabel.Text = "SteamCMD AutoUpdate: ERROR, could not find SteamCMD.";
                    foundUpdate = false;
                }
                else
                {
                    SteamCMDStatusLabel.ForeColor = System.Drawing.Color.Black;
                    SteamCMDStatusLabel.Text = "SteamCMD AutoUpdate: Fetching information.";
                    string parameters = @"+login anonymous +app_info_update 1604030 +app_info_print 1604030 +quit";
                    Process steamCMD = new Process();
                    steamCMD.StartInfo.FileName = Properties.Settings.Default.Server_Path + "\\SteamCMD\\steamcmd.exe";
                    steamCMD.StartInfo.CreateNoWindow = true;
                    steamCMD.StartInfo.UseShellExecute = false;
                    steamCMD.StartInfo.Arguments = parameters;
                    steamCMD.StartInfo.RedirectStandardOutput = true;
                    steamCMD.Start();
                    string output = steamCMD.StandardOutput.ReadToEnd();
                    string[] toScan = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    steamCMD.WaitForExit();
                    for (int i = 0; i < toScan.Length; i++)
                    {
                        if (toScan[i].Contains("\"buildid\"		\"8845337\""))
                        {
                            string lastUpdated = toScan[(i + 1)].Replace("\t", "");
                            lastUpdated = lastUpdated.Replace("\"timeupdated\"		", "").Replace("\"timeupdated\"", "").Replace("\"", "");
                            if (lastUpdated != Properties.Settings.Default.LastUpdateUNIXTime)
                            {
                                Properties.Settings.Default.LastUpdateUNIXTime = lastUpdated;
                                Properties.Settings.Default.Save();
                                foundUpdate = true;
                            }
                        }
                    }
                    SteamCMDStatusLabel.Text = "SteamCMD Status: Not running";
                    LastUpdateLabel.Text = "Last Update on Steam: " + DateTimeOffset.FromUnixTimeSeconds(long.Parse(Properties.Settings.Default.LastUpdateUNIXTime)).DateTime.ToString();
                }
            });
            return foundUpdate;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm SettingsMenu = new SettingsForm();
            SettingsMenu.Show();
        }

        private void ServerSettingsButton_Click(object sender, EventArgs e)
        {
            ServerSettingsForm ServerSettingsMenu = new ServerSettingsForm();
            ServerSettingsMenu.Show();
        }

        private void AppSettingsButton_Click(object sender, EventArgs e)
        {
            AppSettings AppSettingsMenu = new AppSettings();
            AppSettingsMenu.ShowDialog();
        }

        private void SteamCMDButton_Click(object sender, EventArgs e)
        {
            UpdateGame();
        }

        public void StartServer()
        {
            Process[] processList = Process.GetProcessesByName("vrisingserver");
            foreach (Process proc in processList)
            {
                if (proc.MainModule.FileName == Properties.Settings.Default.Server_Path + "\\VRisingServer.exe")
                {
                    MessageBox.Show("Server is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (restartAttempts == 5)
            {
                MainMenuConsole.AppendText(Environment.NewLine + "Unable to start server 5 times, disabling auto-restart.");
                AutoRestartCheck.Checked = false;
                StartGameServerButton.Enabled = true;
                StopGameServerButton.Enabled = false;
                return;
            }
            if (restartAttempts == 0)
            {
                SetTimer();
            }
            restartAttempts++;
            if (File.Exists(Properties.Settings.Default.Server_Path + "\\VRisingServer.exe") == true)
            {
                StopGameServerButton.Enabled = true;
                StartGameServerButton.Enabled = false;
                string parameters = String.Format(@"-persistentDataPath ""{0}"" -serverName ""{1}"" -saveName ""{2}"" -logFile ""{3}\VRisingServer.log""", Properties.Settings.Default.Save_Path, Properties.Settings.Default.Server_Name, Properties.Settings.Default.Save_Name, Properties.Settings.Default.Log_Path);
                Process serverProcess = new Process();
                serverProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                serverProcess.StartInfo.FileName = Properties.Settings.Default.Server_Path + "\\VRisingServer.exe";
                serverProcess.StartInfo.UseShellExecute = true;
                serverProcess.StartInfo.Arguments = parameters;
                serverProcess.EnableRaisingEvents = true;
                serverProcess.Exited += new EventHandler(serverProcessExited);
                serverProcess.Start();
                StoppedPic.Visible = false;
                RunningPic.Visible = true;
                StatusLabel.Text = "Running";
                MainMenuConsole.AppendText("\nServer starting.\nServer name: " + Properties.Settings.Default.Server_Name + "\nSave name: " + Properties.Settings.Default.Save_Name);
                userStopped = false;
            }
            else
            {
                MessageBox.Show("'VRisingServer.exe' not found. Please make sure server is installed correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void SetTimer()
        {
            if (RestartTimer != null)
            {
                RestartTimer.Start();
            }
            else
            {
                RestartTimer = new System.Timers.Timer(10000);
                RestartTimer.Elapsed += OnTimedEvent;
                RestartTimer.AutoReset = true;
                RestartTimer.Enabled = true;
            }            
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            restartAttempts = 0;
            RestartTimer.Stop();
        }

        public void StartGameServerButton_Click(object sender, EventArgs e)
        {            
            StartServer();
        }

        private void MainMenuConsole_TextChanged(object sender, EventArgs e)
        {
            if (MainMenuConsole.TextLength > 10000)
            {
                MainMenuConsole.ResetText();
            }
            MainMenuConsole.ScrollToCaret();
        }

        public void StopGameServerButton_Click(object sender, EventArgs e)
        {
            userStopped = true;
            StopGameServerButton.Enabled = false;
            MainMenuConsole.AppendText(Environment.NewLine + "Stopping server.");
            Process[] processList = Process.GetProcessesByName("vrisingserver");
            foreach (Process proc in processList)
            {
                if (proc.MainModule.FileName == Properties.Settings.Default.Server_Path + "\\VRisingServer.exe")
                {
                    proc.CloseMainWindow();
                }
            }
        }

        private void OpenGameFolderButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Properties.Settings.Default.Server_Path);
        }

        private void serverProcessExited(object sender, EventArgs e)
        {
            if (userStopped == false && AutoRestartCheck.Checked == true)
            {
                Invoke(new Action(() =>
                {
                    StoppedPic.Visible = true;
                    RunningPic.Visible = false;
                    StatusLabel.Text = "Stopped";
                    MainMenuConsole.AppendText(Environment.NewLine + "Server closed unexpectedly. Restarting.");
                    StartServer();
                }));                            
            }
            else
            {
                Invoke(new Action(() =>
                {
                    StoppedPic.Visible = true;
                    RunningPic.Visible = false;
                    StatusLabel.Text = "Stopped";
                    StopGameServerButton.Enabled = false;
                    StartGameServerButton.Enabled = true;
                    MainMenuConsole.AppendText(Environment.NewLine + "Server stopped.");
                    userStopped = false;
                }));                   
            }
        }

        private void RCONButton_Click(object sender, EventArgs e)
        {
            RconConsole RconConsoleMenu = new RconConsole();
            RconConsoleMenu.Show();
        }

        private void ManageAdminsButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Properties.Settings.Default.Server_Path + "\\VRisingServer_Data\\StreamingAssets\\Settings\\adminlist.txt"))
            {
                MessageBox.Show("Unable to find adminlist.txt\nPlease make sure server path is correctly configured and installed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AdminManager AdminManagerMenu = new AdminManager();
                AdminManagerMenu.Show();
            }            
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Server_Name = ServerNameValue.Text;
            Properties.Settings.Default.Save_Name = SaveNameValue.Text;
            Properties.Settings.Default.Save();
            MainMenuConsole.AppendText(Environment.NewLine + "Name and world name saved.");
        }
    }
}
