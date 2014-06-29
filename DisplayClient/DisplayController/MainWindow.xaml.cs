using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using SmartInspectHelper;

namespace DisplayController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public System.Windows.Threading.DispatcherTimer checkTimer;
        long Screenshot1 = 0;
        long Screenshot2 = 1;
        int SameScreenShotCount = 0;
        public MainWindow()
        {
            InitializeComponent();
            si.EnableSmartInspect("DCONTROLLER", false);
            Gurock.SmartInspect.SiAuto.Main.LogMessage("OPENING CONTROLLER");
            checkTimer = new System.Windows.Threading.DispatcherTimer();
            checkTimer.Interval = new TimeSpan(0, 0, 10);
            checkTimer.Tick += new EventHandler(checkTimer_Tick);

            try
            {
                Directory.CreateDirectory(@"c:\tmp");
            }
            catch (Exception)
            {
            }

            if (CheckIfDisplayClientIsResponding() == false || IsDisplayClientRunning() == false)
            {
                RunDisplayClient();
            }
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private bool IsDisplayClientRunning()
        {
            try
            {
                string processName = "DisplayClient";
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        void checkTimer_Tick(object sender, EventArgs e)
        {
            checkTimer.Stop();
            try
            {
                checkTimer.Interval = new TimeSpan(0, 0, 4);
                if (/*CheckIfDisplayClientIsResponding() == false || */IsDisplayClientRunning() == false || ScreenIsShowingActivity() == false)
                {
                    RunDisplayClient();
                    checkTimer.Interval = new TimeSpan(0, 0, 15);
                }
            }
            catch (Exception ex)
            {
            }
            
            checkTimer.Start();
        }

        private Boolean ScreenIsShowingActivity()
        {
            Boolean activeScreen = true;
            try
            {
                if (Screenshot1 == Screenshot2)
                {
                    SameScreenShotCount++;
                    if (SameScreenShotCount >= 9)
                    {
                        Gurock.SmartInspect.SiAuto.Main.LogMessage("RESETTING DISPLAY: SAME CHECKSUM>=9 : " + Screenshot1.ToString() + "/" + Screenshot2.ToString());
                        activeScreen = false;
                        SameScreenShotCount = 0;
                    }
                    else
                    {
                        Screenshot1 = Screenshot2;
                        try
                        {
                            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                            string saveToFileName = @"c:\tmp\screendump.jpg";
                            FileInfo f = new FileInfo(saveToFileName);
                            Screenshot2 = f.Length;
                            Gurock.SmartInspect.SiAuto.Main.LogMessage("POST CHECKSUM : " + Screenshot1.ToString() + "/" + Screenshot2.ToString());
                            Gurock.SmartInspect.SiAuto.Main.LogMessage("SAME SCREENSHOT COUNT = " + SameScreenShotCount.ToString());
                        }
                        catch (Exception)
                        {
                            Screenshot2 = 3;
                        }
                    }
                    //MessageBox.Show("RESETTING - SAME SCREEN CHECKSUM");
                    
                }
                else
                {
                    SameScreenShotCount = 0;
                    Gurock.SmartInspect.SiAuto.Main.LogMessage("PRE CHECKSUM : " + Screenshot1.ToString() + "/" + Screenshot2.ToString());
                    Gurock.SmartInspect.SiAuto.Main.LogMessage("SAME SCREENSHOT COUNT = " + SameScreenShotCount.ToString());
                    Screenshot1 = Screenshot2;
                    try 
	                {	        
		                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        string saveToFileName = @"c:\tmp\screendump.jpg";
                        FileInfo f = new FileInfo(saveToFileName);
                        Screenshot2 = f.Length;
                        Gurock.SmartInspect.SiAuto.Main.LogMessage("POST CHECKSUM : " + Screenshot1.ToString() + "/" + Screenshot2.ToString());
	                }
	                catch (Exception)
	                {
                        Screenshot2 = 3;
                    }
                }
            }
            catch (Exception ex)
            {        
            }
            return activeScreen;
        }

        private void RunDisplayClient()
        {
            checkTimer.Stop();
            checkTimer.Interval = new TimeSpan(0, 0, 15);
            try
            {
                KillDisplayClient();
                KillTicker();
                System.Threading.Thread.Sleep(2000);
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Process.Start(@appPath + @"\DisplayClient.exe");
            }
            catch (Exception)
            {
            }
            checkTimer.Start();
        }

        private void KillDisplayClient()
        {
            try
            {
                string processName = "DisplayClient";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void KillTicker()
        {
            try
            {
                string processName = "Ticker";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private Boolean CheckIfDisplayClientIsResponding()
        {
            Boolean resp = false;
            
            try
            {
                string processName = "DisplayClient";
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    if (process.Responding == true) resp = true;
                }
            }
            catch (Exception)
            {
                resp = false;
            }
            if (resp == false)
            {
               
            }
            
            return resp;
        }
    }
}
