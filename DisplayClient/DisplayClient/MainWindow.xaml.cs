using System;
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
using System.ServiceModel;
using System.Collections.ObjectModel;
using System.IO;
using Chilkat;
using DAL;
using Gurock.SmartInspect;
using WPFMediaKit;
using System.Diagnostics;
using f_in_box__lib;
using SmartInspectHelper;
using System.Collections;
using System.Xml.Serialization;
using System.Windows.Automation.Peers;

namespace DisplayClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //DAL.CoreServiceClient proxy = new DAL.CoreServiceClient();
        Ftp2 _ftp;
        public string _DISPLAYID;
        string CORESERVICE_IP_ADDRESS = "127.0.0.1";
        string CORESERVICE_PORT = "8080";
        public Boolean FTPONLY = false;
        string FTP_IP_ADDRESS = "173.248.129.30";
        string FTP_USERNAME = "ftpuser";
        string FTP_PASSWORD = "bugaboo";
        string TickerMeta = "";
        string TickerMetaToUse = "";
        string _GROUPID = "_none";
        private Boolean _FTPBUSY = false;

        private DateTime _XmlFileModDate = DateTime.Now;
        
        ObservableCollection<DAL.Delta> DeltaCollection = new ObservableCollection<DAL.Delta>();
        long LastLoopTick = 0;
        long LastScheduleTick = 0;
        long LastContentTick = 0;
        long LastTickerTick = 0;

        long KeepAlive = 0;
        public static long KeepAliveTicker = 1;
        Boolean KeepAlive1 = false;
        Boolean KeepAlive2 = false;
        Boolean ResetDisplayClient = false;
        public static long KeepAliveTicksThreshold = 0;  //anything below this will not force a reset, this is used to handle long video files etc


        UserControlContent uccTwo = new UserControlContent();
        UserControlContent uccThree = new UserControlContent();
        UserControlContent uccFour = new UserControlContent();
        UserControlContent uccFive = new UserControlContent();

        DAL.Loop currentLoop = new DAL.Loop();
        DAL.Schedule currentSchedule = new DAL.Schedule();
        ObservableCollection<DAL.LoopContent> loopContentCollection = new ObservableCollection<LoopContent>();
        ObservableCollection<DAL.Template> templateCollection = new ObservableCollection<Template>();

        ObservableCollection<DAL.Content> contentQ = new ObservableCollection<DAL.Content>();
        ObservableCollection<DAL.Content> contentQ2 = new ObservableCollection<DAL.Content>();
        ObservableCollection<DAL.Content> contentQ3 = new ObservableCollection<DAL.Content>();
        ObservableCollection<DAL.Content> contentQ4 = new ObservableCollection<DAL.Content>();
        ObservableCollection<DAL.Content> contentQ5 = new ObservableCollection<DAL.Content>();
   
        System.Windows.Threading.DispatcherTimer mediaTimer;
        System.Windows.Threading.DispatcherTimer minuteTimer;
        System.Windows.Threading.DispatcherTimer tickerTimer;
        System.Windows.Threading.DispatcherTimer keepaliveTimer;

        ObservableCollection<DAL.Content> contentCollection = new ObservableCollection<DAL.Content>();
        ObservableCollection<DAL.Content> zoneCollection = new ObservableCollection<DAL.Content>();
        ObservableCollection<DAL.Schedule> scheduleCollection = new ObservableCollection<DAL.Schedule>();
        ObservableCollection<DAL.Schedule> groupCollection = new ObservableCollection<DAL.Schedule>();

        List<OpenWeather.Model.WeatherDetails> _weatherList = new List<OpenWeather.Model.WeatherDetails>();
        List<OpenWeather.Model.WeatherDetails> _weatherForecast = new List<OpenWeather.Model.WeatherDetails>();
        public OpenWeather.Model.WeatherDetails _currentWeather = new OpenWeather.Model.WeatherDetails();
        

        private System.ComponentModel.Container components = null;

        DAL.RDB proxy = new DAL.RDB();

        public MainWindow()
        {
            InitializeComponent();

            this.Topmost = true;
            this.Activate();
            this.Focus();

            InitTmpFolder();

            borderWeatherStrip.Visibility = Visibility.Hidden;
            borderWeatherRight.Visibility = Visibility.Hidden;

            if (Properties.Settings.Default.Registration == "U")
            {
                AttemptSoftwareRegistration();
            }

            currentLoop = new DAL.Loop();
            currentLoop.Id = "0"; //Set for Startup so that there is no current loop firing and so that schedule fires right away

            si.EnableSmartInspect("DC", true);

            CreateFolders();
            BuildTimers();
            borderConfiguration.Visibility = Visibility.Collapsed;
            try
            {
                InitializeServices();
            }
            catch (Exception ex)
            {
            }

            try
            {
                TestService();
            }
            catch (Exception ex)
            {
            }
            
            //if (Properties.Settings.Default.Diagnostic == true)
            //{
            //    FetchTicker();
            //    SetTickerMeta();
            //    ShowTickerHTML(TickerMeta);
            //    if (IsTickerRunning() == false)
            //    {
            //        System.Threading.Thread.Sleep(500);
            //        ShowTickerHTML(TickerMeta); //"text 0 690 1024 80 clWhite Arial 48 5"
            //    }
            //}

            SimulateMouseClick();
        }

        private async void UpdateWeather()
        {
            try
            {
                _weatherList = await OpenWeather.HelperClass.Weather.GetWeather(Properties.Settings.Default.WeatherLocation);
                _currentWeather = _weatherList.First();
                _weatherForecast = _weatherList.Skip(1).ToList();
                tbCurrentLocation.Text = Properties.Settings.Default.WeatherLocation;
                //gridWeather.DataContext = _currentWeather;
                itemsControlForecase.DataContext = _weatherForecast;
                borderWeatherRight.DataContext = _currentWeather;
                borderWeatherStrip.DataContext = _currentWeather;
            }
            catch (System.Exception)
            {
            }
        }

        private void InitTmpFolder()
        {
            try
            {
                if (!Directory.Exists(@"c:\tmp")) Directory.CreateDirectory(@"c:\tmp");
                if (!Directory.Exists(@"c:\tmp\xml")) Directory.CreateDirectory(@"c:\tmp\xml");
                if (!Directory.Exists(@"c:\tmp\zone")) Directory.CreateDirectory(@"c:\tmp\zone");
            }
            catch (Exception)
            {
            }
        }

        private void AttemptSoftwareRegistration()
        {
            try
            {
                //WindowSoftwareRegistration wreg = new WindowSoftwareRegistration();
                //wreg.ShowDialog();
                //this.Close();
            }
            catch (Exception ex)
            {
                si.six(ex);
            }
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                Exception e = (Exception)args.ExceptionObject;
            }
            catch (Exception ex)
            {
            }
        }

        private void CollectUpdatedScheduleForThisDisplay()
        {
            si.sie("UpdateScheduleForThisDisplay");
            SimulateMouseClick();

            if (FTPONLY)
            {
                CollectXMLFileForFTPOnly();
            }

            try
            {
                //DAL.ScheduleCollection sch = new DAL.ScheduleCollection();
                ObservableCollection<Schedule> sch = new ObservableCollection<Schedule>();
                try
                {
                    sch = proxy.CollectScheduleForScreen();
                    SerializeScheduleCollection(@"c:\tmp\schedule.xml", sch);
                }
                catch (Exception)
                {
                    try
                    {
                        if (FTPONLY)
                        {
                            sch = DeSerializeScheduleCollection(@"c:\tmp\xml\schedule.xml");
                        } else
                        {
                            sch = DeSerializeScheduleCollection(@"c:\tmp\schedule.xml");    
                        }
                    }
                    catch (Exception)
                    {
                        sch.Clear();
                    }
                }
                
                si.sii("Current ClientID="+Properties.Settings.Default.ClientID);
                scheduleCollection.Clear();
                foreach (DAL.Schedule item in sch)
                {
                    if (Properties.Settings.Default.ClientID == item.Screenname || _GROUPID == item.Groupname)
                    {
                        scheduleCollection.Add(item);
                        si.sii(item.Loopname + ":" + item.Loopstart.ToString()+"/"+item.Screenname+"/"+item.Groupname);
                    }
                }

                try
                {
                    LastScheduleTick = proxy.CollectDelta("schedule");
                }
                catch (Exception)
                {
                }
                
            }
            catch (Exception ex)
            {
            }
            si.sil("UpdateScheduleForThisDisplay - "+scheduleCollection.Count().ToString()+" items in schedule");
        }

        private void CollectXMLFileForFTPOnly()
        {
            si.sie("CollectXMLFileForFTPOnly()");
            //System.IO.FileInfo file1 = new System.IO.FileInfo(@"c:\tmp\xml\template.xml");
            CollectFile("template.xml");
            DateTime creationTime = File.GetCreationTime(@"c:\tmp\xml\template.xml");
            //si.sii("XML FILE MOD DATE="+creationTime.ToString());
            //System.IO.FileInfo file2 = new System.IO.FileInfo(@"c:\tmp\xml\template.xml");
            if (_XmlFileModDate != creationTime || !File.Exists(@"c:\tmp\xml\template.xml"))
            {
                si.sii("FILES HAVE CHANGED - COLLECT XML FROM FTP");
                _XmlFileModDate = creationTime;
                CollectFile("schedule.xml");
                CollectFile("loops.xml");
                CollectFile("allloopcontent.xml");
                CollectFile("content.xml");    
            } else
            {
                si.sii("NO CHANGES TO DATE - DO NOT COLLECT XML FROM FTP");
            }
            si.sil("CollectXMLFileForFTPOnly()");
        }

        private void SetTickerMeta()
        {
            try
            {
                string filePath = @"c:\Content\_Media\TickerMeta.txt";
                string line;
                if (File.Exists(filePath))
                {
                    StreamReader file = null;
                    try
                    {
                        file = new StreamReader(filePath);
                        while ((line = file.ReadLine()) != null)
                        {
                            TickerMeta = line;
                        }
                    }
                    finally
                    {
                        if (file != null)
                            file.Close();
                    }
                }
            }
            catch (Exception ex)
            {       
            }
        }
        
        public string GetDefaultCoreIP(string filePath)
        {
            string ip = "127.0.0.1";
            try
            {
                string line;
                if (File.Exists(@filePath))
                {
                    StreamReader file = null;
                    try
                    {
                        file = new StreamReader(filePath);
                        while ((line = file.ReadLine()) != null)
                        {
                            ip = line;
                        }
                    }
                    finally
                    {
                        if (file != null)
                            file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return ip;
        }

        private void FetchTicker()
        {
            try
            {
            }
            catch (Exception ex)
            {
            }
        }

        private void CreateFolders()
        {
            if (!Directory.Exists(@"c:\content"))
            {
                Directory.CreateDirectory(@"c:\content");
            }
            if (!Directory.Exists(@"c:\content\_Media"))
            {
                Directory.CreateDirectory(@"c:\content\_Media");
            }
        }

        private void BuildTimers()
        {
            try
            {
                mediaTimer = new System.Windows.Threading.DispatcherTimer();
                mediaTimer.Interval = new TimeSpan(0, 0, 1);
                mediaTimer.Tick += new EventHandler(mediaTimer_Tick);

                minuteTimer = new System.Windows.Threading.DispatcherTimer();
                minuteTimer.Interval = new TimeSpan(0, 0, 1);
                minuteTimer.Tick += new EventHandler(minuteTimer_Tick);

                tickerTimer = new System.Windows.Threading.DispatcherTimer();
                tickerTimer.Interval = new TimeSpan(0, 0, 0,0,500);
                tickerTimer.Tick += new EventHandler(tickerTimer_Tick);

                keepaliveTimer = new System.Windows.Threading.DispatcherTimer();
                keepaliveTimer.Interval = new TimeSpan(0, 0, 4);
                keepaliveTimer.Tick += new EventHandler(keepaliveTimer_Tick);
                keepaliveTimer.Start();
            }
            catch (Exception ex)
            {
            }
        }

        void keepaliveTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (borderConfiguration.Visibility == Visibility.Visible) return; 
                MouseOperations mo = new MouseOperations();
                MouseOperations.SetCursorPosition(1023, 1);
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                TakeScreenShot.DumpScreen();
                //MouseOperations mo = new MouseOperations();
                MouseOperations.SetCursorPosition(2048, 1);
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            }
            catch (Exception)
            {
            }
            
            return;
        }

        void tickerTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                tickerTimer.Stop();
                ShowTickerHTML(TickerMetaToUse);
            }
            catch (Exception ex)
            {
            }
        }

        void minuteTimer_Tick(object sender, EventArgs e)
        {
            si.sii("MINUTE TIMER TICK AND STOP");
            SimulateMouseClick();
            minuteTimer.Stop();
            minuteTimer.Interval = new TimeSpan(0, 0, 2);
            DateTime dtn = new DateTime();
            dtn = DateTime.Now;

            if (_FTPBUSY == true)
            {
                si.sii("FTP IS BUSY - RETURNING FROM MINUTE TIMER");
                minuteTimer.Interval = new TimeSpan(0, 0, 6);
                minuteTimer.Start();
                return;
            }

            if (dtn.Second >= 55 && dtn.Minute == 1)
            {
                try
                {
                    KillTicker();
                }
                catch (Exception)
                {
                }
                try
                {
                    Application.Current.Shutdown();
                }
                catch (Exception)
                {
                }
            }

            if ((dtn.Second >= 55) || currentLoop.Id == "0")
            {
                si.sii("LP DIAG:SEC>55 START");
                minuteTimer.Interval = new TimeSpan(0, 0, 6); //Step over the minute so that this code doesn't run again
                try
                {
                    si.sii("LP DIAG:SEC>55 START COLL UPD SCHEDULE");
                    CollectUpdatedScheduleForThisDisplay();
                }
                catch (Exception ex)
                {
                    si.sii("LP DIAG:SEC>55 START COLL UPD SCHEDULE:"+ex.Message);
                }
                try
                {
                    si.sii("LP DIAG:SEC>55 START SEL NEW SCHED");
                    SelectNewCurrentScheduleAndFireIfRequired();
                }
                catch (Exception ex)
                {
                    si.sii("LP DIAG:SEC>55 START SEL NEW SCHED:" + ex.Message);
                }
            } //else
            if (dtn.Second > 36 && dtn.Second <= 30)
            {
                minuteTimer.Interval = new TimeSpan(0, 0, 5);
                si.sii("LASTTICKER=" + LastTickerTick.ToString());
                si.sii("LASTLOOPTick=" + LastLoopTick.ToString());
                si.sii("LASTSCHEDULE=" + LastScheduleTick.ToString());
                si.sii("LASTCONTENTTick=" + LastContentTick.ToString());

                si.sii("Current Ticker=" + proxy.CollectDelta("ticker"));
                si.sii("Current Loop=" + proxy.CollectDelta("loop"));
                si.sii("Current Schedule=" + proxy.CollectDelta("schedule"));
                si.sii("Current Content=" + proxy.CollectDelta("content"));

                long proxyCollectDeltaVal = 0;
                try
                {
                    si.sii("START COLLECT LOOP DELTA");
                    proxyCollectDeltaVal = proxy.CollectDelta("loop");
                }
                catch (Exception ex)
                {
                    si.sii("ERROR LOOP DELTA");
                    proxyCollectDeltaVal = LastLoopTick;
                }

                if (ResetDisplayClient == true || proxyCollectDeltaVal != LastLoopTick || proxy.CollectDelta("content") != LastContentTick || proxy.CollectDelta("ticker") != LastTickerTick)
                {
                    try
                    {
                        si.sii("LP DIAG:START WINDUP");
                        KeepAlive = 0;
                        KeepAliveTicker = 1;
                        KeepAlive1 = false;
                        KeepAlive2 = false;
                        ResetDisplayClient = false;
                        windupDisplay();
                    }
                    catch (Exception)
                    {
                        si.sii("LP DIAG:WINDUP");
                    }
                    try
                    {
                        si.sii("LP DIAG:START KILLTICKER");
                        KillTicker();
                    }
                    catch (Exception)
                    {
                        si.sii("LP DIAG:EXCEPT KILLTICKER");
                    }

                    try
                    {
                        si.sii("LP DIAG:START COLLECT UPDATEDSCHEDULE");
                        CollectUpdatedScheduleForThisDisplay();
                    }
                    catch (Exception)
                    {
                        si.sii("LP DIAG: EXCEPT UPDATEDSCHEDULE");
                    }
                    try
                    {
                        si.sii("LP DIAG:START SELECT CURRENT SCHED");
                        SelectNewCurrentScheduleAndFireIfRequired();
                    }
                    catch (Exception)
                    {
                        si.sii("LP DIAG:EXC CURRENT SCHEDULE");
                    }
                }
            }
            si.sii("LP DIAG:MINUTE TIMER RESTART");
            minuteTimer.Start();
        }

        private void SelectNewCurrentScheduleAndFireIfRequired()
        {
            //Decide if current schedule is valid, if not collect,media and templates & fire new schedule
            si.sie("SelectNewCurrentScheduleAndFireIfRequired()");
            Boolean foundSchedule = false;
            try
            {
                //Display
                foreach (DAL.Schedule schedule in scheduleCollection)
                {   DateTime dtStart = new DateTime();
                    DateTime dtEnd = new DateTime();
                    dtStart = (DateTime)schedule.Loopstart;
                    dtEnd = (DateTime)schedule.Loopend;
                    dtEnd.AddSeconds(-7);
                    long scheduleFrom = dtStart.Ticks;
                    long scheduleTo = dtEnd.Ticks;
                    DateTime dtnow = new DateTime();
                    dtnow = DateTime.Now.AddSeconds(10);
                    long schedulenow= dtnow.Ticks;
                    if (schedulenow>=scheduleFrom && schedulenow<=scheduleTo && schedule.Screenname == Properties.Settings.Default.ClientID)
                    {
                        foundSchedule = true;
                        si.sii("Found Schedule = True");
                        if (currentSchedule.Id != schedule.Id)
                        {
                            windupDisplay();
                            currentSchedule = schedule;
                            ConfigureAndTriggerSchedule(currentSchedule);
                            si.sii("Firing:" + schedule.Loopname);
                        }
                    }
                }

                //Group
                if (foundSchedule == false)
                {
                    foreach (DAL.Schedule schedule in scheduleCollection)
                    {
                        DateTime dtStart = new DateTime();
                        DateTime dtEnd = new DateTime();
                        dtStart = (DateTime)schedule.Loopstart;
                        dtEnd = (DateTime)schedule.Loopend;
                        dtEnd.AddSeconds(-7);
                        long scheduleFrom = dtStart.Ticks;
                        long scheduleTo = dtEnd.Ticks;
                        DateTime dtnow = new DateTime();
                        dtnow = DateTime.Now.AddSeconds(10);
                        long schedulenow = dtnow.Ticks;
                        if (schedulenow >= scheduleFrom && schedulenow <= scheduleTo && schedule.Groupname == _GROUPID)
                        {
                            si.sii("Firing:" + schedule.Loopname);
                            foundSchedule = true;
                            if (currentSchedule.Id != schedule.Id)
                            {
                                windupDisplay();
                                currentSchedule = schedule;
                                ConfigureAndTriggerSchedule(currentSchedule);
                                si.sii("Firing:" + schedule.Loopname);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                si.sii("SELECT NEW CURRENT AND FIRE IF REQUIRED EXCEPTION:"+ex.Message);
            }
            si.sil("SelectNewCurrentScheduleAndFireIfRequired()");
        }

        private void windupDisplay()
        {
            try
            {
                currentSchedule.Id = "0";
                KillTicker();
                gridTwo.Children.Clear();
                gridThree.Children.Clear();
                gridFour.Children.Clear();
                gridFive.Children.Clear();
                borderWeatherStrip.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
            }
        }

        private static String HexConverter(Color c)
        {
            return "$00" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private string CollectTickerText(ObservableCollection<DAL.Content> tickerContent)
        {
            string tickerString = "";
            try
            {
                foreach (var item in tickerContent)
                {
                    if (item.Contenttype == "Ticker") tickerString += item.Metadata8 + "     ";
                }
            }
            catch (Exception ex)
            {
            }
            return tickerString;
        }


        private void SerializeContentCollection(string FileName)
        {
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<DAL.Content>));
                    ser.Serialize(fs, contentCollection);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {    
            }
        }

        private ObservableCollection<DAL.Content> DeSerializeContentCollection(string FileName)
        {
            si.sii("Deserializing Content Collection from:"+FileName);
            ObservableCollection<DAL.Content> xmlResult = new ObservableCollection<DAL.Content>();
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<DAL.Content>));
                    xmlResult = (ObservableCollection<DAL.Content>)ser.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
            }
            return xmlResult;
        }

        private void SerializeTemplateCollection(string FileName)
        {
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Template>));
                    ser.Serialize(fs, templateCollection);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private ObservableCollection<Template> DeSerializeTemplateCollection(string FileName)
        {
            si.sii("Deserializing TemplateCollection for FTP Only from:"+FileName);
            ObservableCollection<Template> xmlResult = new ObservableCollection<Template>();
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Template>));
                    xmlResult = (ObservableCollection<Template>)ser.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                
            }
            return xmlResult;
        }

        private DAL.Loop DeSerializeCurrentLoop(string FileName)
        {
            DAL.Loop xmlResult = new DAL.Loop();
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(DAL.Loop));
                    xmlResult = (DAL.Loop)ser.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {

            }
            return xmlResult;
        }

        private void SerializeCurrentLoop(string FileName)
        {
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(DAL.Loop));
                    ser.Serialize(fs, currentLoop);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SerializeLoopContentCollection(string FileName, ObservableCollection<DAL.LoopContent> loopcc)
        {
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<DAL.LoopContent>));
                    ser.Serialize(fs, loopcc);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SerializeScheduleCollection(string FileName, ObservableCollection<Schedule> schedulec)
        {
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Schedule>));
                    ser.Serialize(fs, schedulec);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private ObservableCollection<Schedule> DeSerializeScheduleCollection(string FileName)
        {
            si.sii("Deserializing ScheduleCollection from:"+FileName);
            ObservableCollection<Schedule> xmlResult = new ObservableCollection<Schedule>();
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Schedule>));
                    xmlResult = (ObservableCollection<Schedule>)ser.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {

            }
            return xmlResult;
        }

        private ObservableCollection<LoopContent> DeSerializeLoopContentCollection(string FileName)
        {
            ObservableCollection<LoopContent> xmlResult = new ObservableCollection<LoopContent>();
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<LoopContent>));
                    xmlResult = (ObservableCollection<LoopContent>)ser.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {

            }
            return xmlResult;
        }

        private ObservableCollection<DAL.Loop> DeSerializeLoopCollection(string FileName)
        {
            ObservableCollection<DAL.Loop> xmlResult = new ObservableCollection<Loop>();
            //DAL.LoopCollection xmlResult = new DAL.LoopCollection();
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<DAL.Loop>));
                    xmlResult = (ObservableCollection<DAL.Loop>)ser.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {

            }
            return xmlResult;
        }

        private void ConfigureAndTriggerSchedule(DAL.Schedule currentSchedule)
        {
            Boolean CoreWorkingOK = true;

            si.sie("ConfigureAndTriggerSchedule");
            string allTickerText = "";
            KillTicker();

            Boolean tickerFound = false;
            TickerMetaToUse = "";
            contentCollection.Clear();

            try
            {
                //"text 0 690 1024 80 clWhite Arial 48 5"
                gridFive.Visibility = Visibility.Collapsed;
                gridFour.Visibility = Visibility.Collapsed;
                gridThree.Visibility = Visibility.Collapsed;
                gridTwo.Visibility = Visibility.Collapsed;

                #region CollectContentCollection
                try
                {
                    foreach (DAL.Content item in proxy.CollectMedia())
                    {
                        contentCollection.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    CoreWorkingOK = false;
                    contentCollection.Clear();
                }

                si.sii("Content Collection Count = " + contentCollection.Count().ToString());


                //if there is an error the collection is cleared and loaded from xml, if it worked the collection is saved for future use
                if (contentCollection.Count > 0)
                {
                    SerializeContentCollection(@"c:\tmp\content.xml");
                }
                else
                {
                    contentCollection.Clear();
                    if (FTPONLY)
                    {
                        contentCollection = DeSerializeContentCollection(@"c:\tmp\xml\content.xml");
                    } 
                    else
                    {
                        contentCollection = DeSerializeContentCollection(@"c:\tmp\content.xml");
                    }
                };
                #endregion


                si.sii("Collecting Media in Content Collection");
                CollectMediaInContentCollection(); //Collect All Media
                si.sii("Collect this loop:"+currentSchedule.Loopname);

                #region CollectCurrentLoop
                try
                {
                    if (CoreWorkingOK)
                    {
                        currentLoop = proxy.CollectThisLoop(currentSchedule.Loopname); //Collect Loop
                        SerializeCurrentLoop(@"c:\tmp\loop.xml");
                    }
                    else
                    {
                        currentLoop = null;
                    }
                }
                catch (Exception)
                {
                    currentLoop = null;
                }

                if (currentLoop == null) si.sii("Current Loop is NULL possibly because Core isn't working - Collect from XML");
                si.sii("FTP Only Variable = "+FTPONLY.ToString());

                if (currentLoop == null)
                {
                    if (FTPONLY)
                    {
                        si.sii(@"INSIDE FTP ONLY - Deserializing c:\tmp\xml\loops.xml");
                        ObservableCollection<Loop> lc = new ObservableCollection<Loop>();
                        //DAL.LoopCollection lc = new LoopCollection();
                        try
                        {
                            lc = DeSerializeLoopCollection(@"c:\tmp\xml\loops.xml");
                            si.sii(@"Items in Loops.xml:"+lc.Count.ToString());
                        }
                        catch (Exception e3)
                        {
                            si.six(e3);
                        }
                        
                        foreach (var l in lc)
                        {
                            si.sii("Stepping through loops.xml:"+l.Name+"/"+l.Templatename+"/"+l.Templateid.ToString());
                           if (l.Name == currentSchedule.Loopname)
                           {
                               currentLoop = l;
                               si.sii("Current Loop = "+currentLoop.Name);
                               break;
                           }
                        }
                    } else
                    {
                        currentLoop = DeSerializeCurrentLoop(@"c:\tmp\loop.xml");    
                    }
                }
                #endregion

                try
                {
                    LastLoopTick = proxy.CollectDelta("loop");
                    LastTickerTick = proxy.CollectDelta("ticker");
                }
                catch (Exception)
                {
                }

                #region CollectTemplateCollection
                si.sii("Collect this template:" + currentLoop.Templatename);
                try
                {
                    templateCollection = proxy.CollectZonesForThisTemplateName(currentLoop.Templatename); //CollectTemplate
                }
                catch (Exception)
                {
                    templateCollection.Clear();
                }

                if (templateCollection.Count > 0)
                {
                    SerializeTemplateCollection(@"c:\tmp\template.xml");
                }
                else
                {
                    if (FTPONLY)
                    {
                        try
                        {
                            templateCollection.Clear();
                            //TemplateCollection tmpCollection = new TemplateCollection();
                            ObservableCollection<Template> tmpCollection = new ObservableCollection<DAL.Template>();
                            tmpCollection = DeSerializeTemplateCollection(@"c:\tmp\xml\template.xml");
                            foreach (var t in tmpCollection)
                            {
                                if (t.Name == currentLoop.Templatename && t.Zonename != "_template")
                                {
                                    templateCollection.Add(t);
                                    si.sii("Template Zone Added = " + t.Name + "/" + t.Zonename + "/" + t.Zonedescription);
                                }
                            }
                            si.sii("Zones in Template = "+templateCollection.Count.ToString());
                        }
                        catch (Exception x1)
                        {
                            si.six(x1);
                        }
                    } else
                    {
                        templateCollection = DeSerializeTemplateCollection(@"c:\tmp\template.xml");
                        foreach (var t in templateCollection)
                        {
                            si.sii("Template Zone Added = " + t.Name + "/" + t.Zonename + "/" + t.Zonedescription);
                        }
                        si.sii("Zones in Template = " + templateCollection.Count.ToString());
                    }
                }
                #endregion

                //Populate Zones
                contentQ.Clear();
                contentQ2.Clear();
                contentQ3.Clear();
                contentQ4.Clear();
                contentQ5.Clear();
                int count = 1;
                int tickerInCount = 0; //equals count where ticker is found, 0 by default means no ticker
                int backGroundCount = 0; // //equals count where ticker is found, 0 by default means background is empty (we hide this grid)
                string TickerBackgroundColour = "";
                double TickerOpacity = 0;
                allTickerText = "";

                si.sii("===============================");
                si.sii("===============================");

                foreach (var item in templateCollection)
                {
                    try
                    {
                        if (CoreWorkingOK)
                        {
                            loopContentCollection = proxy.CollectLoopContentForZoneByName(item.Zonename, currentLoop.Name);
                            SerializeLoopContentCollection(@"c:\tmp\zone\" + item.Zonename + ".xml", loopContentCollection);
                        }
                        else
                            if (FTPONLY && File.Exists(@"c:\tmp\xml\allloopcontent.xml"))
                            {
                                //LoopContentCollection tmpLCC = new LoopContentCollection();
                                ObservableCollection<LoopContent> tmpLCC = new ObservableCollection<LoopContent>();
                                si.sii("Collecting from tmpxml alllloopcontent.xml");
                                tmpLCC = DeSerializeLoopContentCollection(@"c:\tmp\xml\allloopcontent.xml");
                                si.sii("LoopContent Items collected from xml = " + tmpLCC.Count.ToString());
                                loopContentCollection.Clear();
                                foreach (LoopContent c in tmpLCC)
                                {
                                    si.sii("STEPPING THROUGH Loop Content tmpLCC:" + c.Medianame);
                                    if (c.Zonename == item.Zonename && c.Loopname == currentLoop.Name)
                                    {
                                        si.sii("ADDING TO LOOP Content Collection Content tmpLCC:" + c.Medianame);
                                        loopContentCollection.Add(c);
                                    }
                                }
                            }
                            else
                            {
                                loopContentCollection = DeSerializeLoopContentCollection(@"c:\tmp\zone\" + item.Zonename + ".xml");
                            }
                    }
                    catch (Exception ei)
                    {
                        si.six(ei);
                    }

                    if (item.ZoneType.ToLower() == "Location and Weather strip".ToLower())
                    {
                        tbLocation.Text = Properties.Settings.Default.ClientID.ToUpper();
                        borderWeatherStrip.Visibility = Visibility.Visible;
                        UpdateWeather();
                    }

                    if (item.ZoneType.ToLower() == "Weather Box".ToLower())
                    {
                        tbLocation.Text = Properties.Settings.Default.ClientID.ToUpper();
                        borderWeatherRight.Visibility = Visibility.Visible;
                        UpdateWeather();
                    }

                    si.sii(item.Zonename + "/" + item.X + "/" + item.Width);
                    foreach (var lc in loopContentCollection)
                    {
                        #region First Configure the Ticker
                        try
                        {
                            foreach (var cc in contentCollection)
                            {
                                if (cc.Contenttype == "Ticker" && lc.Mediaid == cc.Id)
                                {
                                    //Return All TickerMeta
                                    allTickerText += cc.Metadata8 + "    ";
                                    TickerMeta = "text";
                                    TickerMeta += " " + item.X.ToString(); //"text 0 690 1024 80 clWhite Arial 48 5"
                                    TickerMeta += " " + item.Y.ToString();
                                    TickerMeta += " " + item.Width.ToString();
                                    TickerMeta += " " + item.Height.ToString();
                                    Color tickerC = new Color();
                                    ColorConverter ccv = new ColorConverter();
                                    string tickerColour = cc.Metadata4;
                                    TickerMeta += " $00" + tickerColour.Substring(7, 2) + tickerColour.Substring(5, 2) + tickerColour.Substring(3, 2);//0000FF";// Color.FromRgb(0xFF, 0xFF, 0xFF).ToString(); //Color

                                    TickerBackgroundColour = cc.Metadata6;
                                    TickerOpacity = Convert.ToDouble(cc.Metadata7);
                                    UserControlContent.TickerBackgroundOpacity = TickerOpacity;
                                    
                                    TickerMeta += " " + cc.Metadata2; //Font
                                    TickerMeta += " " + cc.Metadata3; //Size
                                    //TickerMeta += " " + cc.Metadata5; //Speed
                                    TickerMeta += " 12";
                                    if (lc.Order == 1)
                                    {
                                        tickerFound = true;
                                        UserControlContent.TickerBackgroundColour = TickerBackgroundColour;
                                        TickerMetaToUse = TickerMeta;
                                        tickerInCount = count;
                                        si.sii("TICKER METADATA = " + TickerMeta);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {    
                        }
                        #endregion

                        //Do the rest
                        si.sii("Looping through ContentCollection...Medianame="+lc.Medianame+" / LoopName="+lc.Loopname);
                        foreach (var cc in contentCollection)
                        {
                            if (cc.Contenttype != "Ticker" && lc.Mediaid == cc.Id)
                                {
                                    //selectedn = @"c:\content\_media\" + System.IO.Path.GetFileName(selectedn);
                                    if (cc.Contenttype == "Background") backGroundCount = count;
                                    if (count == 1)
                                    {
                                        contentQ2.Add(cc);
                                        si.sii("ADDING CONTENT TO Q2:"+cc.Filelocation);
                                    }
                                    if (count == 2)
                                    {
                                        contentQ3.Add(cc);
                                        si.sii("ADDING CONTENT TO Q3:" + cc.Filelocation);
                                    }
                                    if (count == 3)
                                    {
                                        contentQ4.Add(cc);
                                    }
                                    if (count == 4) contentQ5.Add(cc);
                                    si.sii(@"c:\content\_media\" + System.IO.Path.GetFileName(cc.Filelocation));
                                }
                        }
                    }

                    try
                    {
                        WriteTickerFile(allTickerText);
                    }
                    catch (Exception)
                    {
                    }

                    uccTwo = new UserControlContent();
                    uccThree = new UserControlContent();
                    uccFour = new UserControlContent();
                    uccFive = new UserControlContent();

                    Grid currentGrid = null;

                    #region PopulateGridWithContent
                    if (count == 1)
                    {
                        if (tickerInCount == 1)
                        {
                            uccTwo.IsTickerBackground = true;
                        }
                        gridTwo.Children.Clear();
                        gridTwo.Children.Add(uccTwo);
                        currentGrid = gridTwo;
                        if (contentQ.Count == 0) currentGrid = gridHiddenGrid;
                        uccTwo.SetContentQ(contentQ2);
                        KeepAliveTicker++;
                        si.sii("Firing Grid 2");
                    }
                    else
                        if (count == 2)
                        {
                            if (tickerInCount == 2)
                            {
                                uccThree.IsTickerBackground = true;
                            }
                            gridThree.Children.Clear();
                            gridThree.Children.Add(uccThree);
                            currentGrid = gridThree;
                            uccThree.SetContentQ(contentQ3);
                            KeepAliveTicker++;
                            si.sii("Firing Grid 3");
                        }
                        else
                            if (count == 3)
                            {
                                if (tickerInCount == 3)
                                {
                                    uccFour.IsTickerBackground = true;
                                }
                                gridFour.Children.Clear();
                                gridFour.Children.Add(uccFour);
                                currentGrid = gridFour;
                                uccFour.SetContentQ(contentQ4);
                                KeepAliveTicker++;
                                si.sii("Firing Grid 4");
                            }
                            else
                                if (count == 4)
                                {
                                    if (tickerInCount == 4)
                                    {
                                        uccFive.IsTickerBackground = true;
                                    }
                                    gridFive.Children.Clear();
                                    gridFive.Children.Add(uccFive);
                                    currentGrid = gridFive;
                                    uccFive.SetContentQ(contentQ5);
                                    KeepAliveTicker++;
                                    si.sii("Firing Grid 5");
                                };
                    #endregion
                    //gridTwo.Children.Add(ucc);
                    //ucc.SetContentQ(contentQ);

                    double x = Convert.ToDouble(item.X); //sim("targetx=" + target.x);
                    double y = Convert.ToDouble(item.Y); //sim("targety=" + target.y);
                    double width = Convert.ToDouble(item.Width);
                    double height = Convert.ToDouble(item.Height);

                    currentGrid.Width = width;
                    currentGrid.Height = height;
                    currentGrid.Opacity = Convert.ToDouble(item.Opacity);
                    Canvas.SetTop(currentGrid, y);
                    Canvas.SetLeft(currentGrid, x);

                    //Canvas.SetZIndex(currentGrid, Convert.ToInt16(target.layer));
                    //sim(gridIndex + "|" + currentGrid + "x=" + x + " y=" + y);

                    currentGrid.Visibility = Visibility.Visible;

                    count++;

                    if (backGroundCount == 1) gridTwo.Visibility = Visibility.Collapsed;
                    if (backGroundCount == 2) gridThree.Visibility = Visibility.Collapsed;
                    if (backGroundCount == 3) gridFour.Visibility = Visibility.Collapsed;
                    if (backGroundCount == 4) gridFive.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {
            }
            try
            {
                tickerTimer.Start();
            }
            catch (Exception ex)
            {
                
            }
            
            si.sil("ConfigureAndTriggerSchedule");
            //this.Topmost = true;
            //this.Activate();
            //this.Focus();
        }


        void WriteTickerFile(string s)
        {
            try
            {
                try
                {
                    if (File.Exists(@"c:\content\_media\ticker.txt"))
                    {
                        File.Delete(@"c:\content\_media\ticker.txt");
                    };

                    try
                    {
                        StreamWriter SW;
                        SW = File.CreateText(@"c:\content\_media\ticker.txt");
                        SW.WriteLine(s);
                        SW.Close();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
            }
        }


        void mediaTimer_Tick(object sender, EventArgs e)
        {
            si.sie("MEDIA_TIMER TICK");
            string s = "";
            try
            {
                mediaTimer.Stop();
                if (contentCollection.Count <= 0)
                {
                    //MessageBox.Show(contentCollection.Count.ToString());
                    foreach (DAL.Content item in proxy.CollectMedia())
                    {
                        contentCollection.Add(item);
                        s+=item.Filelocation+"|";
                    }
                    //MessageBox.Show(s);
                    CollectMediaInContentCollection();
                }
                else
                {
                   // DisplayBackground();
                  //  RollMedia();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
           // mediaTimer.Start();
            DisplayBackground();
            RollMedia();
            this.Topmost = true;
            this.Activate();
            this.Focus();
        }

        private void RollMedia()
        {
            try
            {
                Gurock.SmartInspect.SiAuto.Main.LogMessage("PRIOR TO UCC");
                foreach (DAL.Content item in contentQ)
                {
                    Gurock.SmartInspect.SiAuto.Main.LogMessage(item.Filelocation);
                }
                UserControlContent ucc = new UserControlContent();
                gridTwo.Children.Add(ucc);
                ucc.SetContentQ(contentQ);
            }
            catch (Exception ex)
            {
                Gurock.SmartInspect.SiAuto.Main.LogMessage("Roll Media error:"+ex.Message);
            }
            SimulateMouseClick();
        }

        private void DisplayBackground()
        {
            try
            {
                string filen = "";
                string selectedn = "";
                //DAL.Content _background = new DAL.Content();
                foreach (DAL.Content item in contentCollection)
                {
                    filen = item.Filelocation;
                    if (System.IO.Path.GetExtension(filen) == ".jpg" || System.IO.Path.GetExtension(filen) == ".png")
                    {
                        selectedn = item.Filelocation;
                    }
                }
                selectedn = @"c:\content\_media\"+System.IO.Path.GetFileName(selectedn);
                ImageSourceConverter imgConv = new ImageSourceConverter();
                ImageSource imageSource = (ImageSource)imgConv.ConvertFromString(@selectedn);
                imageBackground.Source = imageSource;
                imageBackground.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Gurock.SmartInspect.SiAuto.Main.LogMessage("Display Background error:"+ex.Message);
            }
        }

        private void CollectMediaInContentCollection()
        {
            si.sie("CollectMediaInContentCollection");

            contentQ = new ObservableCollection<DAL.Content>();
            string filenameonly;
            string filenameandpath;
            string fileext;
            try
            {
                foreach (DAL.Content item in contentCollection)
                {
                    fileext = System.IO.Path.GetExtension(item.Filelocation).ToLower();
                    filenameonly = System.IO.Path.GetFileName(item.Filelocation);
                    filenameandpath = @"c:\content\_Media\" + filenameonly;
                    if (fileext == ".wmv" || fileext == ".avi" || fileext==".mpg" || fileext==".swf")
                    {
                        Gurock.SmartInspect.SiAuto.Main.LogMessage(filenameandpath);
                        contentQ.Add(item);
                    }
                 
                    if (!File.Exists(filenameandpath) && filenameonly != "" && item.Contenttype!="Ticker")
                    {
                        if (item.Filelocation != null)
                        {
                            si.sii("Collecting "+filenameonly);
                            try
                            {
                                //CollectFile(filenameonly);
                                proxy.GetAndSaveAttachment(@"c:\content\_media\"+filenameonly, item.Id);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    } 
                }
                try
                {
                    LastContentTick = proxy.CollectDelta("content");
                }
                catch (Exception)
                {
                }
            }
            catch (Exception ex)
            {
                Gurock.SmartInspect.SiAuto.Main.LogMessage("CollectmediaInContentCollection:"+ex.Message);
            }
            si.sil("CollectMediaInContentCollection");
        }

        private void CollectFile(string filenameonly)
        {
            return;
          
            try
            {
                _FTPBUSY = true;
                Gurock.SmartInspect.SiAuto.Main.LogMessage("Starting CollectFile - filename = "+filenameonly);
                Boolean success;
                _ftp = new Ftp2();
                success = _ftp.UnlockComponent("FTP212345678_29E8FB35jA2U");
                if (success != true)
                {
                    _FTPBUSY = false;
                    return;
                }
                _ftp.Hostname = CORESERVICE_IP_ADDRESS;
                _ftp.Username = FTP_USERNAME;
                _ftp.Password = FTP_PASSWORD;

                // Connect and login to the FTP server.
                success = _ftp.Connect();
                if (success != true)
                {
                    //Wait and then try again
                    System.Threading.Thread.Sleep(250);
                    success = _ftp.Connect();
                    if (success != true)
                    {
                        //Andagain
                        System.Threading.Thread.Sleep(250);
                        success = _ftp.Connect();
                        if (success != true)
                        {
                        }
                    }
                }

                string fileext = System.IO.Path.GetExtension(filenameonly);
                string subFolder = "";
                if (fileext.ToLower() == ".wmv" || fileext.ToLower() == ".avi" || fileext.ToLower() == ".mpg")
                {
                    subFolder = "Video";
                }
                else
                    if (fileext.ToLower() == ".png" || fileext.ToLower() == ".jpg" || fileext.ToLower() == ".bmp")
                    {
                        subFolder = "Images";
                    }
                    else
                        if (fileext.ToLower() == ".swf")
                        {
                            subFolder = "Flash";
                        }
                        else
                            if (fileext.ToLower() == ".mp3")
                            {
                                subFolder = "Audio";
                            } else
                                if (fileext.ToLower() == ".txt")
                                {
                                    subFolder = "Ticker";
                                }
                                else
                                    if (fileext.ToLower() == ".xml")
                                    {
                                        subFolder = "Xml";
                                    };

                // Change to the remote directory where the file will be uploaded.
                success = _ftp.ChangeRemoteDir(subFolder);
                if (success != true)
                {
                    success = _ftp.ChangeRemoteDir(subFolder);
                    if (success != true)
                    {
                    }
                }

                string localFilename;
                if (fileext.ToLower() == ".xml")
                {
                    localFilename = @"c:\tmp\xml\" + filenameonly;
                } else
                {
                    localFilename = @"c:\content\_media\" + filenameonly;
                }
                string remoteFilename;
                remoteFilename = filenameonly;

                SiAuto.Main.LogMessage("FTPGET remotefilename - "+remoteFilename);
                SiAuto.Main.LogMessage("FTPGET localfilename - " + localFilename);

                success = _ftp.GetFile(remoteFilename, localFilename);
             
                if (success != true)
                {
                    success = _ftp.GetFile(remoteFilename, localFilename);
                    if (success != true)
                    {
                        _FTPBUSY = false;
                        return;
                    }
                }

                _ftp.Disconnect();
            }
            catch (Exception ex)
            {
                _FTPBUSY = false;
                Gurock.SmartInspect.SiAuto.Main.LogMessage("Collecting file error:"+ex.Message);
            }
            _FTPBUSY = false;
        }

        private void TestService()
        {
            Gurock.SmartInspect.SiAuto.Main.LogMessage("Start TEST Service");
            Gurock.SmartInspect.SiAuto.Main.LogMessage("FTPCORE MODE = "+FTPONLY.ToString());
            Boolean allok = true;
            try
            {
                //DAL.UserCollection _users = new DAL.UserCollection();
                ObservableCollection<User> _users = new ObservableCollection<User>();
                _users = proxy.CollectUsers();
                Gurock.SmartInspect.SiAuto.Main.LogMessage("USERS COLLECTED");
                try
                {
                    Gurock.SmartInspect.SiAuto.Main.LogMessage("USER COUNT = " + _users.Count());
                }
                catch (System.Exception x)
                {
                    si.six(x);
                }
                
                if (_users.Count <= 0 && FTPONLY == false)
                {
                    allok = false;
                    Gurock.SmartInspect.SiAuto.Main.LogMessage("FALSE");
                }
            }
            catch (Exception ex)
            {
                allok = false;
            }

            if (File.Exists(@"c:\tmp\schedule.xml") || (Boolean)Properties.Settings.Default.FTPCoreMode == true ) allok = true;

            if (allok == false)
            {
                tbClientID.Text = Properties.Settings.Default.ClientID;
                tbServiceAddress.Text = Properties.Settings.Default.ServiceAddress;
                tbPort.Text = Properties.Settings.Default.Port;
                tbWeatherLocation.Text = Properties.Settings.Default.WeatherLocation;
                cbFTPCore.IsChecked = (Boolean)Properties.Settings.Default.FTPCoreMode;
                tbGroupID.Text = Properties.Settings.Default.FTPCoreModeGroupID;
                if (cbFTPCore.IsChecked == true)
                {
                    spGroupID.Visibility = Visibility.Visible;
                } else
                {
                    spGroupID.Visibility = Visibility.Hidden;
                }
                borderConfiguration.Visibility = Visibility.Visible;
            }
            else
            {
                Gurock.SmartInspect.SiAuto.Main.LogMessage("Register Display Client with Core");
                try
                {
                    if (FTPONLY)
                    {
                        //_GROUPID = DeSerializeGroupID(@"c:\tmp\group.xml");
                        _GROUPID = Properties.Settings.Default.FTPCoreModeGroupID;
                        Gurock.SmartInspect.SiAuto.Main.LogMessage("FTPONLY MODE - collecting gid from xml:" + _GROUPID);
                    } else
                    {
                        si.sii("Attempting Registration");
                        _GROUPID = RegisterDisplayClientWithCore();
                        SerializeGroupID(@"c:\tmp\group.xml", _GROUPID);    
                    }
                }
                catch (Exception)
                {
                    _GROUPID = DeSerializeGroupID(@"c:\tmp\group.xml");
                    Gurock.SmartInspect.SiAuto.Main.LogMessage("Exception Register Display Client with Core - collecting gid="+_GROUPID);
                }
                Gurock.SmartInspect.SiAuto.Main.LogMessage("Done Register Display Client with Core");
                if (Properties.Settings.Default.Diagnostic == true)
                {
                    si.sii("Starting Service Check Timer");
                    StartServiceCheckTimer();
                }
                else
                {
                    si.sii("All OK - Starting Minute Timer");
                    minuteTimer.Start();
                    KeepAliveTicker++;
                }
            }
        }


        private void SerializeGroupID(string FileName, string gid)
        {
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(string));
                    ser.Serialize(fs, gid);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string DeSerializeGroupID(string FileName)
        {
            string xmlResult = "";
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(string));
                    xmlResult = (string)ser.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
            }
            return xmlResult;
        }

        private string RegisterDisplayClientWithCore()
        {
            string s = "";

            si.sie("RegisterDisplayClientWithCore()  : " + Properties.Settings.Default.ClientID);
            //si.sie("DisplayClientID = " + Properties.Settings.Default.ClientID);
            s = proxy.RegisterDisplayClientLogin(Properties.Settings.Default.ClientID);
            si.sil("RegisterDisplayClientWithCore()");
            return s;
        }

        private void StartServiceCheckTimer()
        {
            mediaTimer.Start();
        }

        private void InitializeServices()
        {
            try
            {
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                //if (File.Exists(@appPath+@"\ip.txt"))
                //{
                //    CORESERVICE_IP_ADDRESS = GetDefaultCoreIP(@appPath + @"\ip.txt");
                //    //MessageBox.Show("IP SET TO:" + CORESERVICE_IP_ADDRESS);
                //} else 
                //{
                //    CORESERVICE_IP_ADDRESS = Properties.Settings.Default.ServiceAddress;
                //    CORESERVICE_PORT = Properties.Settings.Default.Port;
                //}

                FTPONLY = (bool) Properties.Settings.Default.FTPCoreMode;

                //CORESERVICE_IP_ADDRESS = "10.0.0.245";

                _DISPLAYID = Properties.Settings.Default.ClientID;
                // Increase binding max sizes so that the image can be retrieved  
                //proxy = new DAL.CoreServiceClient(new BasicHttpBinding(), new EndpointAddress("http://" + CORESERVICE_IP_ADDRESS + ":"+CORESERVICE_PORT+"/iTactixCoreService"));

                // Increase binding max sizes so that the image can be retrieved  
                //if (proxy.Endpoint.Binding is System.ServiceModel.BasicHttpBinding)
                //{
                //    System.ServiceModel.BasicHttpBinding binding =
                //         (System.ServiceModel.BasicHttpBinding)proxy.Endpoint.Binding;

                //    int max = 5000000;  // around 5M  
                //    binding.MaxReceivedMessageSize = max;
                //    binding.MaxBufferSize = max;
                //    binding.ReaderQuotas.MaxArrayLength = max;
                //}
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSaveConfiguration_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.ClientID = tbClientID.Text;
                Properties.Settings.Default.ServiceAddress = tbServiceAddress.Text;
                Properties.Settings.Default.Port = tbPort.Text;
                Properties.Settings.Default.Diagnostic = (bool) cbDiagnostic.IsChecked;
                Properties.Settings.Default.FTPCoreMode = (bool) cbFTPCore.IsChecked;
                Properties.Settings.Default.FTPCoreModeGroupID = tbGroupID.Text;
                Properties.Settings.Default.WeatherLocation = tbWeatherLocation.Text;
                Properties.Settings.Default.Save();
                borderConfiguration.Visibility = Visibility.Collapsed;
                InitializeServices();
                TestService();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Core Service is currently not available - please check the client configuration.");
            }
        }

        private void Window_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Key == Key.F8)
            {
                tbClientID.Text = Properties.Settings.Default.ClientID;
                tbServiceAddress.Text = Properties.Settings.Default.ServiceAddress;
                tbPort.Text = Properties.Settings.Default.Port;
                tbWeatherLocation.Text = Properties.Settings.Default.WeatherLocation;
                cbFTPCore.IsChecked = (Boolean)Properties.Settings.Default.FTPCoreMode;
                tbGroupID.Text = Properties.Settings.Default.FTPCoreModeGroupID;
                tbClientID.Text = Properties.Settings.Default.ClientID;
                tbServiceAddress.Text = Properties.Settings.Default.ServiceAddress;
                cbDiagnostic.IsChecked = (bool)Properties.Settings.Default.Diagnostic;
                cbFTPCore.IsChecked = (bool) Properties.Settings.Default.FTPCoreMode;
                borderConfiguration.Visibility = Visibility.Visible;
            }
        }

        void DisplayTickerNow()
        {
            try
            {
                Boolean firingSting = false;
                if (firingSting == false)
                {
                    //TickerMeta = String.Format("text 0 {0} 1024 {1} {2} {3} {4} {5}", Properties.Settings.Default.TickerTop, Properties.Settings.Default.TickerHeight, Properties.Settings.Default.TickerFontColor, Properties.Settings.Default.TickerFont, Properties.Settings.Default.TickerFontSize, Properties.Settings.Default.TickerSpeed);
                    TickerMeta = "text 0 690 1355 80 clWhite Arial 48 5";
                    ShowTickerHTML(TickerMeta);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private bool IsTickerRunning()
        {
            try
            {
                string processName = "Ticker";
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

        private void ShowTickerHTML(string Meta)
        {
            try
            {
                //string tickerm = "text 0 690 1024 80 clWhite Arial 48 5";
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string TickerString = @"c:\content\_media\ticker.txt " + Meta;
                Process.Start(@appPath+@"\Ticker\Ticker.exe", TickerString);
            }
            catch (Exception)
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
            catch (Exception)
            {
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        	KillTicker();
        }

        private void btnFireTicker_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	KillTicker();
			ShowTickerHTML(TickerMeta);
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
        	// TODO: Add event handler implementation here.
			//this.Topmost = false;
        }

        private void Window_Initialized(object sender, System.EventArgs e)
        {
        	this.Topmost = true; 
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }

        private void Window_Deactivated(object sender, System.EventArgs e)
        {
        	try
            {
                TaskbarHelper t = new TaskbarHelper();
                t.Show();
            }
            catch (Exception)
            {
            }
        }

        private void btnSimulateClick_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }

        private void SimulateMouseClick()
        {
            try
            {
                if (borderConfiguration.Visibility == Visibility.Visible) return;
                MouseOperations mo = new MouseOperations();
                MouseOperations.SetCursorPosition(1023, 1);
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                MouseOperations.SetCursorPosition(2048, 1);
            }
            catch (Exception)
            {
            }
        }

        private void cbFTPCore_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            spGroupID.Visibility = Visibility.Visible;
        }

        private void cbFTPCore_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            spGroupID.Visibility = Visibility.Hidden;
        }
    }
}
