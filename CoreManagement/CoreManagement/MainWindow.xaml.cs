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
using System.Windows.Shapes;
using DevComponents.WpfEditors;
using DevComponents.WPF;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Chilkat;
using LP.Shared;
using System.ServiceModel;
using System.ServiceModel.Description;
using SmartInspectHelper;
using DevComponents.WpfSchedule.Model;

namespace CoreManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage bitImg;
        BackgroundWorker _worker;
        byte[] bytes;
        string UserAddEditMode;
        string TemplateAddEditMode;
        string ZoneAddEditMode;
        string ContentAddEditMode;
        string ScheduleAddEditMode;
        Ftp2 _ftp;
        string ftpFilenameSource = "";
        string ftpFilenameDestination = "";
        DAL.User userEdit;
        DAL.Screen screenEdit;
        DAL.Template templateEdit;
        DAL.Content mediaFile;
        DAL.Content ticker;
        DAL.Schedule scheduleItem;
        //DAL.ScreengroupsCollection screenGroupCollection;
        ObservableCollection<DAL.Screengroups> screenGroupCollection;
        Queue<DAL.Content> ftpQ = new Queue<DAL.Content>();
        ObservableCollection<DAL.Content> tickerCollection = new ObservableCollection<DAL.Content>();
        ObservableCollection<DAL.Content> contentCollection = new ObservableCollection<DAL.Content>();
        ObservableCollection<DAL.Content> allcontentCollection = new ObservableCollection<DAL.Content>();
        //ObservableCollection<DAL.Loop> _loopCollection = new DAL.LoopCollection();
        ObservableCollection<DAL.Loop> _loopCollection = new ObservableCollection<DAL.Loop>();
        //DAL.CoreServiceClient proxy = new DAL.CoreServiceClient();
        System.Windows.Threading.DispatcherTimer _ftpTimer;
        System.Windows.Threading.DispatcherTimer _snapshotTimer;
        System.Windows.Threading.DispatcherTimer _cTimer;

        DAL.RDB proxy = new DAL.RDB();

        string userlevel = "none";
        string CORESERVICE_IP_ADDRESS = "";
        string CORESERVICE_PORT = "";
        //string FTP_IP_ADDRESS = "127.0.0.1";
        //string FTP_USERNAME = "ftpuser";
        //string FTP_PASSWORD = "bugaboo";
        int snapshotTry = 0;
        Boolean _tickerEditMode = false;
        Boolean isMediaOpened = false;

        Boolean ftpBusy = false;

        Boolean Scheduling = true;
        Boolean Conferencing = false;

        public CalendarModel _Model = new CalendarModel();

        public MainWindow()
        {
            
            AdjustProxySettings();
            InitializeComponent();
            si.EnableSmartInspect("CM", true);
            
            ConfigureSnapshotTimer();
            ConfigureInitialViews();
            ConfigureConfirmationTimer();

            devCal.CalendarModel = _Model;

            btnSchedule.Visibility = Visibility.Collapsed;
            btnConference.Visibility = Visibility.Collapsed;

            if (Scheduling) btnSchedule.Visibility = Visibility.Visible;
            if (Conferencing) btnConference.Visibility = Visibility.Visible;

            CheckExpiry();

            TestService();
        }

        private void ConfigureSnapshotTimer()
        {
            try
            {
                _snapshotTimer = new System.Windows.Threading.DispatcherTimer();
                _snapshotTimer.Interval = new TimeSpan(0, 0, 1);
                _snapshotTimer.Tick += new EventHandler(_snapshotTimer_Tick);
            }
            catch (Exception ex)
            {
            }
        }

        public void CheckExpiry()
        {
            DateTime expire = new DateTime(2014, 07, 15);
            DateTime dtNow = DateTime.Now;
            long ticksexpire = expire.Ticks;
            long ticksnow = dtNow.Ticks;
            if (ticksnow > ticksexpire)
            {
                labelLicense.Content = "This limited Demo version has Expired - Please enquire about licensing";
                spNavigation.IsEnabled = false;
            }
        }

        private void ConfigureConfirmationTimer()
        {
            try
            {
                _cTimer = new System.Windows.Threading.DispatcherTimer();
                _cTimer.Interval = new TimeSpan(0, 0, 2);
                _cTimer.Tick += new EventHandler(_cTimer_Tick);

            }
            catch (Exception ex)
            {
            }
        }

        void _cTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _cTimer.Stop();
                imageConfirmation.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
            }
        }

        void showWait()
        {
            try
            {
                imageConfirmation.Visibility = Visibility.Collapsed;
                imageWait.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
            }
        }

        void showConfirmation()
        {
            try
            {
                imageWait.Visibility = Visibility.Collapsed;
                imageConfirmation.Visibility = Visibility.Visible;
                _cTimer.Start();
            }
            catch (Exception ex)
            {
            }
        }

        void _snapshotTimer_Tick(object sender, EventArgs e)
        {   
            _snapshotTimer.Stop();
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            File.Delete(@appPath + @"\Snapshot.jpg");
            try
            {
                SnapMediaElementPreview();
            }
            catch (Exception ex)
            {
                _snapshotTimer.Start();
            }
            try
            {   
                System.IO.FileInfo fi = new System.IO.FileInfo(@appPath + @"\Snapshot.jpg");
                if (fi.Length < 1024 && snapshotTry < 9 || File.Exists(@appPath + @"\Snapshot.jpg") == false)
                {
                    snapshotTry++;
                    _snapshotTimer.Start();
                };
            }
            catch (Exception ex)
            {
                snapshotTry++;
                _snapshotTimer.Start();
            }
        }

        private void ConfigureInitialViews()
        {
            si.sie("ConfigureInitialViews");
            lbRubbishBin.Visibility = Visibility.Collapsed;
            try
            {
                imageConfirmation.Visibility = Visibility.Collapsed;
                ConfigureTickerSizes();
                radMediaFilter.WatermarkContent = "Quick Filter";
                tbBuild.Text = GetBuildVersionAndDate.GetVersion();
                gridConfiguration.Visibility = Visibility.Collapsed;
                gridLogin.Visibility = Visibility.Visible;
                gridConferencing.Visibility = Visibility.Collapsed;
                gridMedia.Visibility = Visibility.Collapsed;
                gridStatus.Visibility = Visibility.Collapsed;
                gridScheduling.Visibility = Visibility.Collapsed;
                gbAddEditMedia.Visibility = Visibility.Collapsed;
                gbAddScreen.Visibility = Visibility.Collapsed;
                gbAddEditLoop.Visibility = Visibility.Collapsed;
                gbAddAction.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                si.six("Configure intialviews", ex);
            }
            si.sil("ConfigureInitialViews");
        }

        private void ConfigureTickerSizes()
        {
            string s = "10";
            cbTickerSize.Items.Add(s);
            s = "16";
            cbTickerSize.Items.Add(s);
            s = "24";
            cbTickerSize.Items.Add(s);
            s = "32";
            cbTickerSize.Items.Add(s);
            s = "48";
            cbTickerSize.Items.Add(s);
            s = "60";
            cbTickerSize.Items.Add(s);
            //s = "80";
            //cbTickerSize.Items.Add(s);
            //s = "96";
            //cbTickerSize.Items.Add(s);
            //s = "128";
            //cbTickerSize.Items.Add(s);
            //s = "150";
            //cbTickerSize.Items.Add(s);
            //s = "190";
            //cbTickerSize.Items.Add(s);
        }

        public void PrepareForLogin()
        {
            si.sie("PrepareForLogin()");
            try
            {
                spNavigation.IsEnabled = false;
                tbSectionHeading.Text = "User Login";
                tbLoginFailed.Visibility = Visibility.Collapsed;
                gridLogin.Visibility = Visibility.Visible;
                gridConfiguration.Visibility = Visibility.Collapsed;
                tbUserLoggedIn.Text = "";
                tbUsername.Focus();
            }
            catch (Exception ex)
            {
            }
            si.sil("PrepareForLogin()");
        }

        private void AdjustProxySettings()
        {
            si.sie("AdjustProxySettings");
            
            //CORESERVICE_IP_ADDRESS = Properties.Settings.Default.ServiceAddress;
            //CORESERVICE_PORT = Properties.Settings.Default.Port;
            // Increase binding max sizes so that the image can be retrieved  
            //proxy = new DAL.CoreServiceClient(new BasicHttpBinding(), new EndpointAddress("http://"+CORESERVICE_IP_ADDRESS+":"+CORESERVICE_PORT+"/iTactixCoreService"));
            
            //ServiceHost serviceHost = new ServiceHost(singletonInstance, "http://localhost:1234/MyService/xml");

            //// Create Meta Behavior
            //ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            //serviceHost.Description.Behaviors.Add(new );
           
            //SB.ServiceModel.Pool.ServicePoolBehavior behavior = new SB.ServiceModel.Pool.ServicePoolBehavior();
            //behavior.IncrementSize = 2;
            //behavior.MaxPoolSize = 10;
            //behavior.MinPoolSize = 2;
            //host.Description.Behaviors.Add(behavior);

            // Increase binding max sizes so that the image can be retrieved  
            //if (proxy.Endpoint.Binding is System.ServiceModel.BasicHttpBinding)
            //{
            //    //ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            //    //ServiceDebugBehavior dbg = new ServiceDebugBehavior();
            //    //dbg.HttpHelpPageEnabled = true;
            //    //proxy.Endpoint.Behaviors.Add(new ServiceDebugBehavior())
            //    System.ServiceModel.BasicHttpBinding binding =
            //         (System.ServiceModel.BasicHttpBinding)proxy.Endpoint.Binding;
            //    int max = 5000000;  // around 5M  
            //    binding.MaxReceivedMessageSize = max;
            //    binding.MaxBufferSize = max;
            //    binding.ReaderQuotas.MaxArrayLength = max;
                
            //}  
            si.sil("AdjustProxySettings");
        }

        public void Busy(string s)
        {
            gridView.Opacity = 0.5;
            busy.Visibility = Visibility.Visible;
            busy.IsBusy = true;
            if (s != "")
            {
                busy.BusyContent = s;
            }
            else
            {
                busy.BusyContent = "Loading...";
            }
            busy.IsBusy = true;
            busy.UpdateLayout();
        }

        public void NotBusy()
        {
            gridView.Opacity = 1;
            busy.IsBusy = false;
        }


        public void btnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            PerformLogin(e);
        }

        private void PerformLogin(System.Windows.RoutedEventArgs e)
        {   
            si.sie("PerformLogin");

            DAL.RDB proxy = new DAL.RDB();

            Busy("test");
            try
            {
                tbLoginFailed.Visibility = Visibility.Collapsed;
                DAL.User _currentUser = new DAL.User();
                _currentUser = proxy.VerifyLogin(tbUsername.Text, tbPassword.Password);
                if (_currentUser.Fullname == "failed")
                {
                    userlevel = "none";
                    animationControl.StartStateTransition();
                    tbLoginFailed.Visibility = Visibility.Visible;
                    tbUsername.Focus();
                    animationControl.AnimateStateTransition();
                }
                else
                {
                    userlevel = _currentUser.Groupid;
                    animationControl.StartStateTransition();
                    SetupUserLevel();
                    tbLoginFailed.Visibility = Visibility.Collapsed;
                    gridLogin.Visibility = Visibility.Collapsed;
                    tbUserLoggedIn.Text = _currentUser.Fullname;
                    tbLogout.Visibility = Visibility.Visible;
                    spNavigation.IsEnabled = true;
                    btnConfiguration_Click(this,e);
                    animationControl.AnimateStateTransition();
                    FetchMediaCollection();
                    InitializeFTPTimer();
                }
            }
            catch (Exception ex)
            {
            }
            NotBusy();
            si.sil("PerformLogin");
            CheckExpiry();
        }

        public void SetupUserLevel()
        {
            if (userlevel == "Administrator")
            {
                btnConfiguration.IsEnabled = true;
                btnMedia.IsEnabled = true;
                btnSchedule.IsEnabled = true;
                radexpanderScreenGroups.IsEnabled = true;
                radexpanderScreens.IsEnabled = true;
                radexpanderTemplates.IsEnabled = true;
                radexpanderUserAccounts.IsEnabled = true;
            } 
            else
            if (userlevel == "Scheduler")
            {
                btnConfiguration.IsEnabled = true;
                btnMedia.IsEnabled = true;
                btnSchedule.IsEnabled = true;
                radexpanderScreenGroups.IsEnabled = false;
                radexpanderScreens.IsEnabled = true;
                radexpanderTemplates.IsEnabled = false;
                radexpanderUserAccounts.IsEnabled = false;
            }
            else
            if (userlevel == "Monitor")
            {
                btnConfiguration.IsEnabled = true;
                btnMedia.IsEnabled = false;
                btnSchedule.IsEnabled = false;
                radexpanderScreenGroups.IsEnabled = false;
                radexpanderScreens.IsEnabled = true;
                radexpanderTemplates.IsEnabled = false;
                radexpanderUserAccounts.IsEnabled = false;
                
            }
        }

        private void InitializeFTPTimer()
        {
            try
            {
                _ftpTimer = new System.Windows.Threading.DispatcherTimer();
                _ftpTimer.Interval = new TimeSpan(0, 0, 2);
                _ftpTimer.Tick += new EventHandler(_ftpTimer_Tick);
                _ftpTimer.Start();
            }
            catch (Exception ex)
            {
            }
        }

        void _ftpTimer_Tick(object sender, EventArgs e)
        {
            if (ftpBusy == false) CheckFTPQueue();
        }

        private void btnStatus_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            lbRubbishBin.Visibility = Visibility.Collapsed;
            tbSectionHeading.Text = "System Status";
            animationControl.StartStateTransition();
            gridConfiguration.Visibility = Visibility.Collapsed;
            gridLogin.Visibility = Visibility.Collapsed;
            gridMedia.Visibility = Visibility.Collapsed;
            gridStatus.Visibility = Visibility.Visible;
            gridScheduling.Visibility = Visibility.Collapsed;
            animationControl.AnimateStateTransition();
        }

        private void btnSchedule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            lbRubbishBin.Visibility = Visibility.Collapsed;
            e.Handled = true;
            radexpanderScheduling.IsExpanded = false;

            InitializeSchedulingPage();

            //radexpanderLoopComposition.IsExpanded = true;
            tbSectionHeading.Text = "Scheduling";
            animationControl.StartStateTransition();
            cbLoopTemplate.DataContext = proxy.CollectTemplates();
            //radSchedulingCalender.SelectedDate = DateTime.Now;
           // RefreshMediaLoops();
            //RefreshMediaCollection();
            FetchMediaCollection();
            RefreshAllMedia();
            RefreshTickers();
            gridConfiguration.Visibility = Visibility.Collapsed;
            gridLogin.Visibility = Visibility.Collapsed;
            gridMedia.Visibility = Visibility.Collapsed;
            gridStatus.Visibility = Visibility.Collapsed;
            gridConferencing.Visibility = Visibility.Collapsed;
            gridScheduling.Visibility = Visibility.Visible;
            //si.sii("Collect Ticker information - Core Service dataset exception - exceeds 1024 byte buffer : Ticker count is 0");
            //si.sii("Collect Schedule pointers - Core Service dataset exception - exceeds 1024 byte buffer : Schedule count is 0");
            DisableTickerChanges();
            animationControl.AnimateStateTransition();

            rbDisplay.IsChecked = true;
            _Model.Appointments.Clear();
            devCal.DisplayedOwners.Clear();
            AddScreensToCal();
        }

        private void InitializeSchedulingPage()
        {
            try
            {
                spDisplay.Visibility = Visibility.Collapsed;
                spGroup.Visibility = Visibility.Collapsed;
                InitComboBoxesOnSchedulingPage();
            }
            catch (Exception ex)
            {
            }
        }

        private void InitComboBoxesOnSchedulingPage()
        {
            try
            {
                //rbDisplay.IsChecked = false;
                //rbGroup.IsChecked = false;
                try
                {
                    cbDisplay.DataContext = GetScreenCollection();
                    cbDisplay.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnMedia_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            lbRubbishBin.Visibility = Visibility.Collapsed;
            Busy("");
            tbSectionHeading.Text = "Manage Media";
            //FetchMediaCollection();
            animationControl.StartStateTransition();
            gridConfiguration.Visibility = Visibility.Collapsed;
            gridLogin.Visibility = Visibility.Collapsed;
            gridMedia.Visibility = Visibility.Visible;
            gridStatus.Visibility = Visibility.Collapsed;
            gridConferencing.Visibility = Visibility.Collapsed;
            gridScheduling.Visibility = Visibility.Collapsed;
            animationControl.AnimateStateTransition();
            NotBusy();
        }

        private void FetchMediaCollection()
        {
            si.sie("FetchMediaCollection");

            tickerCollection.Clear();
            contentCollection.Clear();
            allcontentCollection.Clear();
            listBoxMedia.DataContext = contentCollection;
            foreach (DAL.Content item in proxy.CollectMedia())
            {
                contentCollection.Add(item);
                allcontentCollection.Add(item);
                if (item.Contenttype == "Ticker") tickerCollection.Add(item);
            }
            si.sil("FetchMediaCollection");
        }

        private void btnConfiguration_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            lbRubbishBin.Visibility = Visibility.Collapsed;
            Busy("");
            lbScreens.DataContext = GetScreenCollection();
            RefreshScreenGroupCollection();
            gbAddGroup.Visibility = Visibility.Collapsed;
            cbGroups.Visibility = Visibility.Collapsed;
            btnApplyScreenChanges.Visibility = Visibility.Collapsed;
            btnCancelScreenChanges.Visibility = Visibility.Collapsed;
            RefreshUserCollection();
            RefreshTemplateCollection();
            tbSectionHeading.Text = "Configuration";
            animationControl.StartStateTransition();
            gridConfiguration.Visibility = Visibility.Visible;
            gridLogin.Visibility = Visibility.Collapsed;
            gridMedia.Visibility = Visibility.Collapsed;
            gridStatus.Visibility = Visibility.Collapsed;
            gridConferencing.Visibility = Visibility.Collapsed;
            gridScheduling.Visibility = Visibility.Collapsed;
            animationControl.AnimateStateTransition();
            NotBusy();
        }

        private void RefreshTemplateCollection()
        {
            si.sie("RefreshTemplateCollection");
            
            gbAddEditTemplate.Visibility = Visibility.Collapsed;
            gbAddEditZone.Visibility = Visibility.Collapsed;
            lbTemplates.DataContext = proxy.CollectTemplates();
            si.sil("RefreshTemplateCollection");
        }

        private void ConfigureUserSection()
        {
            cbUserGroup.Items.Clear();
            string s = "Administrator";
            cbUserGroup.Items.Add(s);
            s = "Scheduler";
            cbUserGroup.Items.Add(s);
            s = "Monitor";
            cbUserGroup.Items.Add(s);
            gbAddEditUser.Visibility = Visibility.Collapsed;
        }

        private void RefreshUserCollection()
        {
            ConfigureUserSection();
            lbUsers.DataContext = proxy.CollectUsers();
        }

        private void RefreshScreenGroupCollection()
        {
            try
            {
                screenGroupCollection = new ObservableCollection<DAL.Screengroups>();
                screenGroupCollection = GetGroupCollection();
                lbGroups.DataContext = screenGroupCollection;
                cbGroups.DataContext = screenGroupCollection;
                cbScreenGroupsForAdd.DataContext = screenGroupCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private ObservableCollection<DAL.Screengroups> GetGroupCollection()
        {
            ObservableCollection<DAL.Screengroups> scrc = new ObservableCollection<DAL.Screengroups>();
            try
            {
                scrc = proxy.CollectScreenGroups();
            }
            catch (Exception ex)
            {
            }
            return scrc;
        }



        private ObservableCollection<DAL.Screen> GetScreenCollection()
        {
            //DAL.ScreenCollection scrc = new DAL.ScreenCollection();
            ObservableCollection<DAL.Screen> scrc = new ObservableCollection<DAL.Screen>();
            try
            {
                scrc = proxy.CollectScreens();
            }
            catch (Exception ex)
            {
            }
            return scrc;
        }



        private void btnAddGroup_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            gbAddGroup.Visibility = Visibility.Visible;
            tbScreenGroup.Focus();
        }

        private void btnCancelScreenGroup_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            tbScreenGroup.Text = "";
            gbAddGroup.Visibility = Visibility.Collapsed;
        }

        private void btnApplyScreenGroup_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            showWait();
            DAL.Screengroups scrg = new DAL.Screengroups();
            scrg.Name = tbScreenGroup.Text;
            gbAddGroup.Visibility = Visibility.Collapsed;
            proxy.InsertScreenGroup(scrg);
			showConfirmation();
            RefreshScreenGroupCollection();
        }

        private void btnRemoveGroup_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            DAL.Screengroups selectedGroup = new DAL.Screengroups();
            selectedGroup = (DAL.Screengroups)lbGroups.SelectedItem;
            proxy.RemoveGroup(selectedGroup);
            RefreshScreenGroupCollection();
        }

        private void radexpanderScreenGroups_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            //radexpanderScreenGroups.IsExpanded = false;
            radexpanderUserAccounts.IsExpanded = false;
            radExpanderSystemDefaults.IsExpanded = false;
            radexpanderTemplates.IsExpanded = false;
            radexpanderScreens.IsExpanded = false;
        }

        private void btnApplyScreenChanges_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            showWait();
            DAL.Screen screen = new DAL.Screen();
            screen = (DAL.Screen)lbScreens.SelectedItem;
            DAL.Screengroups group = new DAL.Screengroups();
            group = (DAL.Screengroups)cbGroups.SelectedItem;
            screen.Groupid = group.Name;
            proxy.ChangeScreenGroup(screen);
			showConfirmation();
            cbGroups.Visibility = Visibility.Collapsed;
            btnApplyScreenChanges.Visibility = Visibility.Collapsed;
            btnCancelScreenChanges.Visibility = Visibility.Collapsed;
        }

        private void btnCancelScreenChanges_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            cbGroups.Visibility = Visibility.Collapsed;
            btnApplyScreenChanges.Visibility = Visibility.Collapsed;
            btnCancelScreenChanges.Visibility = Visibility.Collapsed;
        }

        private void btnEditScreen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            cbGroups.Visibility = Visibility.Visible;
            cbGroups.IsDropDownOpen = true;
            cbGroups.Focus();
            btnApplyScreenChanges.Visibility = Visibility.Visible;
            btnCancelScreenChanges.Visibility = Visibility.Visible;
        }

        private void btnApplyAddEditUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            if (UserAddEditMode == "edit")
            {
                showWait();
                userEdit.Groupid = (string)cbUserGroup.SelectedValue;
                userEdit.Password = tbUserPassword.Password;
                proxy.ChangeUser(userEdit);
				showConfirmation();
            }
            else
            {
                showWait();
                userEdit.Groupid = (string)cbUserGroup.SelectedValue;
                userEdit.Password = tbUserPassword.Password;
                proxy.InsertUser(userEdit);
				showConfirmation();
            }
            RefreshUserCollection();
        }

        private void btnCancelAddEditUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            gbAddEditUser.Visibility = Visibility.Collapsed;
        }

        private void btnEditUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            UserAddEditMode = "edit";
            userEdit = new DAL.User();
            userEdit = (DAL.User)lbUsers.SelectedItem;
            tbUserPassword.Password = userEdit.Password;
            gbAddEditUser.DataContext = userEdit;
            try
            {
                cbUserGroup.SelectedIndex = cbUserGroup.Items.IndexOf(userEdit.Groupid.ToString());
            }
            catch (Exception ex)
            {
            }
            gbAddEditUser.Visibility = Visibility.Visible;
            tbLoginID.Focus();
        }

        private void btnAddUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            UserAddEditMode = "add";
            userEdit = new DAL.User();
            tbUserPassword.Password = "";
            gbAddEditUser.DataContext = userEdit;
            gbAddEditUser.Visibility = Visibility.Visible;
            tbLoginID.Focus();
        }

        private void btnRemoveUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            userEdit = new DAL.User();
            userEdit = (DAL.User)lbUsers.SelectedItem;
            proxy.RemoveUser(userEdit);
            RefreshUserCollection();
        }

        private void radexpanderScreens_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            radexpanderScreenGroups.IsExpanded = false;
            radexpanderUserAccounts.IsExpanded = false;
            radExpanderSystemDefaults.IsExpanded = false;
            radexpanderTemplates.IsExpanded = false;
            //radexpanderScreens.IsExpanded = false;
        }

        private void radexpanderUserAccounts_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            radexpanderScreenGroups.IsExpanded = false;
            //radexpanderUserAccounts.IsExpanded = false;
            radExpanderSystemDefaults.IsExpanded = false;
            radexpanderTemplates.IsExpanded = false;
            radexpanderScreens.IsExpanded = false;
        }

        private void radExpanderSystemDefaults_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            radexpanderScreenGroups.IsExpanded = false;
            radexpanderUserAccounts.IsExpanded = false;
            radexpanderTemplates.IsExpanded = false;
            //radExpanderSystemDefaults.IsExpanded = false;
            radexpanderScreens.IsExpanded = false;
        }

        private void radexpanderTemplates_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
        	radexpanderScreenGroups.IsExpanded = false;
            radexpanderUserAccounts.IsExpanded = false;
            radExpanderSystemDefaults.IsExpanded = false;
            //radexpanderTemplates.IsExpanded = false;
            radexpanderScreens.IsExpanded = false;
        }

        private void btnRemoveZone_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            templateEdit = new DAL.Template();
            templateEdit = (DAL.Template)lbZones.SelectedItem;
            if (templateEdit.Zonename == "Background")
            {
                MessageBox.Show("The Background zone cannot be removed...");
            }
            else
            {
                proxy.RemoveTemplate(templateEdit);
                lbTemplates_UpdateZones();
            }
        }

        

        private void btnAddTemplate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            TemplateAddEditMode = "add";
            templateEdit = new DAL.Template();
            gbAddEditTemplate.DataContext = templateEdit;
            gbAddEditTemplate.Visibility = Visibility.Visible;
            tbTemplateDescription.Focus();
        }

        private void btnEditTemplate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            TemplateAddEditMode = "edit";
            templateEdit = new DAL.Template();
            templateEdit = (DAL.Template)lbTemplates.SelectedItem;
            gbAddEditTemplate.DataContext = templateEdit;
            gbAddEditTemplate.Visibility = Visibility.Visible;
            tbTemplateDescription.Focus();
        }

        private void btnRemoveTemplate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            templateEdit = new DAL.Template();
            templateEdit = (DAL.Template)lbTemplates.SelectedItem;
            proxy.RemoveTemplate(templateEdit);
            RefreshTemplateCollection();
        }

        private void btnEditZone_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            lbTemplates.IsEnabled = false;
            ZoneAddEditMode = "edit";
            if (lbZones.SelectedIndex < 0) lbZones.SelectedIndex = 0;
            templateEdit = new DAL.Template();
            templateEdit = (DAL.Template)lbZones.SelectedItem;
            templateEdit.Created = DateTime.Now;
            gbAddEditZone.DataContext = templateEdit;
            gbAddEditZone.Visibility = Visibility.Visible;
        }

        private void btnAddZone_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            lbTemplates.IsEnabled = false;
            ZoneAddEditMode = "add";
            templateEdit = new DAL.Template();
            DAL.Template template2 = new DAL.Template();
            if (lbTemplates.SelectedIndex < 0)
            {
                lbTemplates.SelectedIndex = 0;
            }
            template2 = (DAL.Template)lbTemplates.SelectedItem;
            templateEdit.Name = template2.Name;
            templateEdit.Created = DateTime.Now;
            gbAddEditZone.DataContext = templateEdit;
            gbAddEditZone.Visibility = Visibility.Visible;
        }

        private void btnApplyAddEditTemplate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            if (TemplateAddEditMode == "edit")
            {
                showWait();
                templateEdit.Zonename = "_template";
                proxy.ChangeTemplate(templateEdit);
				showConfirmation();
            }
            else
            {
                showWait();
                templateEdit.Zonename = "_template";
                proxy.InsertTemplate(templateEdit);
				showConfirmation();
            }
            RefreshTemplateCollection();
            
        }

        private void btnCancelAddEditTemplate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            gbAddEditTemplate.Visibility = Visibility.Collapsed;
        }

        private void lbTemplates_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            e.Handled = true;
            lbTemplates_UpdateZones();    
        }

        private void lbTemplates_UpdateZones()
        {
            try
            {
                DAL.Template selectedTemplate = new DAL.Template();
                selectedTemplate = (DAL.Template)lbTemplates.SelectedItem;
                lbZones.DataContext = proxy.CollectZonesForTemplate(selectedTemplate);
                //DAL.TemplateCollection tc = new DAL.TemplateCollection();
                ObservableCollection<DAL.Template> tc = new ObservableCollection<DAL.Template>();
                tc = proxy.CollectZonesForTemplate(selectedTemplate);
            }
            catch (Exception ex)
            {
            }
            gbAddEditZone.Visibility = Visibility.Collapsed;
        }

        private void btnCancelAddEditZone_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            gbAddEditZone.Visibility = Visibility.Collapsed;
            lbTemplates.IsEnabled = true;
        }

        private void btnApplyAddEditZone_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            if (ZoneAddEditMode == "edit")
            {
                showWait();
                proxy.ChangeTemplate(templateEdit);
				showConfirmation();
            }
            else
            {
                showWait();
                proxy.InsertTemplate(templateEdit);
				showConfirmation();
            }
            lbTemplates_UpdateZones();
            lbTemplates.IsEnabled = true;
        }

        private void Click_Logout(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            tbLogout.Visibility = Visibility.Collapsed;
            spNavigation.IsEnabled = false;
            tbSectionHeading.Text = "User Login";
            tbUserLoggedIn.Text = "";
            tbPassword.Password = "";
            tbUsername.Text = "";
            ConfigureInitialViews();
            tbUsername.Focus();
        }

        private Boolean openMediaFile()
        {
            si.sii("openMediaFile()");
            mediaElementPreview.Stop();
            mediaElementPreview.Source = null;
            mediaElementPreview.Visibility = Visibility.Collapsed;
            imagePreview.Visibility = Visibility.Collapsed;
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (File.Exists(@appPath + @"\reduced.jpg")) File.Delete(@appPath + @"\reduced.jpg");
            if (File.Exists(@appPath + @"\Snapshot1.jpg")) File.Delete(@appPath + @"\Snapshot1.jpg");
            if (File.Exists(@appPath + @"\Snapshot2.jpg")) File.Delete(@appPath + @"\Snapshot2.jpg");
            if (File.Exists(@appPath + @"\Snapshot3.jpg")) File.Delete(@appPath + @"\Snapshot3.jpg");
            string FilenameAndPath = "";
            string FilenameOnly = "";
            string FileExtensionOnly = "";
            Boolean imageLoaded = false;
            try
            {
                mediaFile.Metadata2 = "";
                mediaFile.Metadata3 = "";
                mediaFile.Metadata4 = "";
                mediaFile.Metadata5 = "";
                mediaFile.Metadata6 = "";
                mediaFile.Metadata7 = "";
                mediaFile.Metadata8 = "";
                mediaFile.Metadata9 = "0.75";

                si.sii("Opening File Dialog");
                OpenFileDialog openFileDialog1 = new OpenFileDialog { /* Set filter options and filter index.*/Filter = "Media Files|*.jpg;*.png;*.wmv;*.avi;*.mpg;*.swf", FilterIndex = 1, Multiselect = false };
                if (openFileDialog1.ShowDialog() == true)
                {
                    FilenameOnly = System.IO.Path.GetFileName(openFileDialog1.FileName);
                    FileExtensionOnly = System.IO.Path.GetExtension(openFileDialog1.FileName);
                    FilenameAndPath = openFileDialog1.FileName;
                    tbImportFileName.Text = FilenameOnly;
                    tbTemplateName4.Text = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    if (FileExtensionOnly.ToLower() == ".jpg" || FileExtensionOnly.ToLower() == ".png" || FileExtensionOnly.ToLower() == ".bmp")
                    {
                        spVolume.Visibility = Visibility.Collapsed;
                        tbContenttype.Text = "Image";
                        mediaFile.Contenttype = "Image";
                        FileInfo finfo = new FileInfo(FilenameAndPath);
                        tbFilesize.Text = finfo.Length.ToString();
                        ImageSourceConverter imgConv = new ImageSourceConverter();
                        ImageSource imageSource = (ImageSource)imgConv.ConvertFromString(@FilenameAndPath);
                        imagePreview.Source = imageSource;
                        imagePreview.Visibility = Visibility.Visible;
                        appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        LPImageLib.ResizeImage(@FilenameAndPath, appPath + @"\reduced.jpg", 75, 55, true);
                        mediaFile.Filelocation = FilenameAndPath;
                        mediaFile.Metadata1 = "10";
                        mediaFile.Name = tbTemplateName4.Text;
                        tbDuration.IsEnabled = true;
                       
                    }
                    else
                    if (FileExtensionOnly.ToLower() == ".mpg" || FileExtensionOnly.ToLower() == ".avi" || FileExtensionOnly.ToLower() == ".wmv" || FileExtensionOnly.ToLower() == ".mp4")
                    {
                        si.sii("Opening video media");
                        try 
	                    {	        
		                    isMediaOpened = false;
                            spVolume.Visibility = Visibility.Visible;
                            tbContenttype.Text = "Video";
                            mediaFile.Contenttype = "Video";
                            FileInfo finfo = new FileInfo(FilenameAndPath);
                            tbFilesize.Text = finfo.Length.ToString();
                            Uri _mediafile = new Uri(@FilenameAndPath);
                            btnApplyMediaImport.IsEnabled = false;
                            mediaElementPreview.Source = _mediafile;
                            mediaElementPreview.Visibility = Visibility.Visible;
                            appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                            mediaFile.Filelocation = FilenameAndPath;
                            mediaFile.Metadata1 = mediaElementPreview.NaturalDuration.ToString();
                            mediaFile.Name = tbTemplateName4.Text;
                            tbDuration.IsEnabled = false;
                            mediaElementPreview.Play();
                            si.sii("Playing video media");
                            btnApplyMediaImport.IsEnabled = true;
	                    }
	                    catch (Exception x)
	                    {
		                    si.six(x);
	                    }
                    }
                    else
                    if (FileExtensionOnly.ToLower() == ".swf")
                    {
                        spVolume.Visibility = Visibility.Collapsed;
                        tbContenttype.Text = "Flash";
                        mediaFile.Contenttype = "Flash";
                        FileInfo finfo = new FileInfo(FilenameAndPath);
                        tbFilesize.Text = finfo.Length.ToString();
                        Uri _mediafile = new Uri(@FilenameAndPath);
                        mediaElementPreview.Source = _mediafile;
                        mediaElementPreview.Visibility = Visibility.Visible;
                        appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        mediaFile.Filelocation = FilenameAndPath;
                        mediaFile.Metadata1 = "n/a";
                        mediaFile.Name = tbTemplateName4.Text;
                        tbDuration.IsEnabled = false;
                    }
                }
                imageLoaded = true;
            }
            catch (Exception ex)
            {
                imageLoaded = false;
                si.six(ex);
                //MessageBox.Show(ex.Message);
            }
            return imageLoaded;
        }

        private void btnOpenTestImage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            FetchMediaCollection();
        }

        private void btnImportMedia_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckExpiry();
            e.Handled = true;
            try
            {
                mediaElementPreview.Stop();
                mediaElementPreview.Visibility = Visibility.Collapsed;
                sliderVolume.Value = 0.75;
                ContentAddEditMode = "add";
                mediaFile = new DAL.Content();
                mediaFile.Importdate = (DateTime)DateTime.Now;
                gbAddEditMedia.DataContext = mediaFile;
                gbAddEditMedia.Visibility = Visibility.Visible;
                btnOpenMedia_Click(sender, e);
            }
            catch (Exception x)
            {
                si.six(x);
            }
        }

        private void btnBusyOn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void listBoxMedia_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DAL.Content _cnt = new DAL.Content();
                _cnt = (DAL.Content)listBoxMedia.SelectedItem;
				
                imageMediaTypeFlash.Visibility = Visibility.Collapsed;
                imageMediaTypeImage.Visibility = Visibility.Collapsed;
                imageMediaTypeVideo.Visibility = Visibility.Collapsed;
                imageMediaTypeTicker.Visibility = Visibility.Collapsed;

                if (_cnt.Contenttype == "Image") imageMediaTypeImage.Visibility = Visibility.Visible;
                if (_cnt.Contenttype == "Video") imageMediaTypeVideo.Visibility = Visibility.Visible;
                if (_cnt.Contenttype == "Flash") imageMediaTypeFlash.Visibility = Visibility.Visible;
                if (_cnt.Contenttype == "Ticker") imageMediaTypeTicker.Visibility = Visibility.Visible;

                borderMediaDetail.DataContext = _cnt;
            }
            catch (Exception ex)
            {
            }
        }

        private void btnOpenMedia_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            si.sii("btnOpenMedia_Click - openMediaFile about to be called");
            openMediaFile();
        }

        private void btnCancelMediaImport_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            mediaElementPreview.Source = null;
            mediaElementPreview.Stop();
            gbAddEditMedia.Visibility = Visibility.Collapsed;
        }

        private void btnApplyMediaImport_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            si.sii("Apply Media Import Now");
            mediaElementPreview.Source = null;
            mediaElementPreview.Stop();
            mediaFile.Filesize = Convert.ToInt64(tbFilesize.Text);
            if (ContentAddEditMode == "add")
            {
                showWait();
                mediaFile.Metadata9 = sliderVolume.Value.ToString();
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                try
                {
                    if (mediaFile.Contenttype == "Flash")
                    {
                        mediaFile.Snapshot = LPImageLib.GetPhoto(@appPath + @"\Images\flash.jpg");
                    }
                    else
                    {
                        mediaFile.Snapshot = LPImageLib.GetPhoto(appPath + @"\reduced.jpg");
                    }
                }
                catch (Exception ex)
                {
                    si.sie("ApplyMediaImport Sec1:" + ex.Message);
                    try
                    {
                        if (mediaFile.Contenttype == "Flash")
                        {
                            mediaFile.Snapshot = LPImageLib.GetPhoto(@appPath + @"\Images\flash.jpg");
                        }
                        else
                        {
                            mediaFile.Snapshot = LPImageLib.GetPhoto(appPath + @"\reduced.jpg");
                        }
                    }
                    catch (Exception ex2)
                    {
                        si.sie("ApplyMediaImport Sec2" + ex2.Message);
                    }
                }
                
                DateTime dt = DateTime.Now;
                string dy = dt.DayOfYear.ToString();
                string dh = dt.Minute.ToString();
                string dm = dt.Second.ToString();
                string mfn = mediaFile.Filelocation;
                string mfnPath = System.IO.Path.GetDirectoryName(mfn);
                string mfnExt = System.IO.Path.GetExtension(mfn);
                string mfnFile = @mfnPath + @"\" + @System.IO.Path.GetFileNameWithoutExtension(mfn) + "_" + dy + dh + dm + mfnExt;
                mediaFile.Metadata8 = mfn;
                mediaFile.Filelocation = mfnFile;
                try
                {
                    mediaFile.Id = proxy.InsertMedia(mediaFile);
                }
                catch (Exception ex)
                {
                    si.sie("ApplyMediaImport Sec3" + ex.Message);
                    try
                    {
                        System.Threading.Thread.Sleep(250);
                        mediaFile.Id = proxy.InsertMedia(mediaFile);
                    }
                    catch (Exception ex3)
                    {
                        si.sie("ApplyMediaImport Sec4" + ex3.Message);
                        try
                        {
                            System.Threading.Thread.Sleep(250);
                            mediaFile.Id = proxy.InsertMedia(mediaFile);
                        }
                        catch (Exception ex4)
                        {
                            si.sie("ApplyMediaImport Sec5" + ex4.Message);
                        }
                    }
                }

                try
                {
                    ftpQ.Enqueue(mediaFile);
                    contentCollection.Add(mediaFile);
                    allcontentCollection.Add(mediaFile);
                }
                catch (Exception ex)
                {
                    si.sie("ApplyMediaImport Sec6" + ex.Message);
                }
				//showConfirmation();
            }
            else
            {
                showWait();
                mediaFile.Metadata9 = sliderVolume.Value.ToString();
                proxy.ChangeMedia(mediaFile);
				showConfirmation();
            };
            gbAddEditMedia.Visibility = Visibility.Collapsed;
        }

        private void btnEditMedia_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                e.Handled = true;
                ContentAddEditMode = "edit";
                mediaFile = new DAL.Content();
                mediaFile = (DAL.Content)listBoxMedia.SelectedItem;
                Double vol = 0;
                try
                {
                    vol = Convert.ToDouble(mediaFile.Metadata9);
                }
                catch (Exception ex)
                {
                    vol = 0;
                }
                sliderVolume.Value = vol;
                if (mediaFile.Contenttype != "Video")
                {
                    spVolume.Visibility = Visibility.Collapsed;
                }
                else
                {
                    spVolume.Visibility = Visibility.Visible;
                }
                if (mediaFile.Contenttype == "Image") tbDuration.IsEnabled = true;
                gbAddEditMedia.DataContext = mediaFile;
                gbAddEditMedia.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                si.sie("Edit Media:" + ex.Message);
            }
        }

        private void tbPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                PerformLogin(e);
            }
        }

        private void CheckFTPQueue()
        {
            try
            {
                if (ftpQ.Count() > 0)
                {
                    si.sii("FTP Q = " + ftpQ.Count.ToString());
                    DAL.Content _file = new DAL.Content();
                    _file = ftpQ.Dequeue();
                    SendFileViaFTP(_file);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SendFileViaFTP(DAL.Content fileToFTP)
        {
            si.sii("SENDING FILE VIA FTP");
            ftpBusy = true;
            btnImportMedia.IsEnabled = false;
            //System.Threading.Thread.Sleep(250);
            showWait();
            try
            {
                Boolean success;
                
                string subFolder = "";
                //if (fileToFTP.Contenttype.ToLower() == "video")
                
                string localFilename;
                localFilename = fileToFTP.Metadata8;

                proxy.StoreAttachment(localFilename, fileToFTP.Contenttype.ToLower(), fileToFTP.Id);

                // The application is now free to do anything else
                // while the file is uploading.
                // For this example, we'll simply sleep and periodically
                // check to see if the transfer if finished.  While checking
                // however, we'll report on the progress in both number
                // of bytes tranferred and performance in bytes/second.
                //while (_ftp.AsyncFinished != true)
                //{

                //    //textBox1.Text += _ftp.AsyncBytesSent + " bytes sent" + "\r\n";
                //    //textBox1.Refresh();
                //    //textBox1.Text += _ftp.UploadRate + " bytes per second" + "\r\n";
                //    //textBox1.Refresh();

                //    // Sleep 1 second.
                //    //_ftp.SleepMs(1000);
                //}

                //// Did the upload succeed?
                //if (_ftp.AsyncSuccess == true)
                //{
                //    //MessageBox.Show("File Uploaded!");
                //}
                //else
                //{
                //    // The error information for asynchronous ops
                //    // is in AsyncLog as opposed to LastErrorText
                //    //MessageBox.Show(_ftp.AsyncLog);
                //}

                _ftp.Disconnect();
            }
            catch (Exception ex)
            {
                si.six(ex);
            }
            try
            {
                _ftp.Disconnect();
            }
            catch (Exception)
            {
            }
            showConfirmation();
            ftpBusy = false;
            btnImportMedia.IsEnabled = true;
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgress(10);
        }

        void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public byte[] GetScreenShot(double scale, int quality)
        {
            Byte[] imageArray = null;
            try
            {
                mediaElementPreview.Visibility = Visibility.Visible;
                MediaElement source;
                source = mediaElementPreview;

                double actualHeight = 79;//source.RenderSize.Height;
                double actualWidth = 123;//source.RenderSize.Width;
                double renderHeight = actualHeight * scale;
                double renderWidth = actualWidth * scale;

                RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth,
                    (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
                VisualBrush sourceBrush = new VisualBrush(source);

                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                using (drawingContext)
                {
                    drawingContext.PushTransform(new ScaleTransform(scale, scale));
                    drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0),
                        new Point(actualWidth, actualHeight)));
                }
                renderTarget.Render(drawingVisual);

                JpegBitmapEncoder jpgEncoder = new JpegBitmapEncoder();
                jpgEncoder.QualityLevel = quality;
                jpgEncoder.Frames.Add(BitmapFrame.Create(renderTarget));

                using (MemoryStream outputStream = new MemoryStream())
                {
                    jpgEncoder.Save(outputStream);
                    imageArray = outputStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                
            }
            return imageArray;
        }

        #region Capture Screenshot
        private void SnapMediaElementPreview()
        {
            string snap = "";
            byte[] screenshot = GetScreenShot(1, 75);
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FileStream fileStream = new FileStream(@appPath+@"\Snapshot"+snap+".jpg", FileMode.Create, System.IO.FileAccess.ReadWrite);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(screenshot);
            binaryWriter.Close();
            //if (snap == "1")
            //{
            LPImageLib.ResizeImage(@appPath + @"\Snapshot"+snap+".jpg", @appPath + @"\reduced.jpg", 87, 65, true);
            //}
            
            //LPImageLib.ResizeImage(@appPath + @"\Snapshot" + snap + ".jpg", @appPath + @"\reduced" + snap + ".jpg", 87, 65, true);
        }
        
        #endregion
		
		private void btnSnap_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }

		private void mediaElementPreview_MediaOpened(object sender, System.Windows.RoutedEventArgs e)
		{
            mediaElementPreview.Play();
            isMediaOpened = true;
            snapshotTry = 0;
            btnApplyMediaImport.IsEnabled = true;
            //_snapshotTimer.Interval = new TimeSpan(0, 0, 2);
            _snapshotTimer.Start();
		}

		private void btnSaveConfiguration_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            e.Handled = true;
            try
            {
                CoreManagement.Properties.Settings.Default.ServiceAddress = tbServiceAddress.Text;
                CoreManagement.Properties.Settings.Default.Port = tbPort.Text;
                CoreManagement.Properties.Settings.Default.Save();
                borderConfiguration.Visibility = Visibility.Collapsed;
                System.Threading.Thread.Sleep(250);
                //AdjustProxySettings();
                TestService();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Core Service is currently not available - please check the client configuration.");
            }
		}

        private void TestService()
        {
            si.sie("TestService");
            Boolean allok = true;
            try
            {
                //DAL.UserCollection _users = new DAL.UserCollection();
                ObservableCollection<DAL.User> _users = new ObservableCollection<DAL.User>();
                _users = proxy.CollectUsers();
                //if (_users.Count <= 0)
                //{
                //    MessageBox.Show(_users.Count.ToString());
                //    allok = false;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: "+ex.Message);
                allok = false;
            }
            if (allok == false)
            {
                si.sii("allok = false");
                si.sii("collapsed");
                tbLoginFailed.Visibility = Visibility.Collapsed;
                gridLogin.Visibility = Visibility.Collapsed;
                gridConfiguration.Visibility = Visibility.Collapsed;
                tbServiceAddress.Text = CoreManagement.Properties.Settings.Default.ServiceAddress;
                tbPort.Text = CoreManagement.Properties.Settings.Default.Port;
                borderConfiguration.Visibility = Visibility.Visible;
                si.sii("done");
            }
            else
            {
                borderConfiguration.Visibility = Visibility.Collapsed;
                PrepareForLogin();
            }
            si.sil("TestService");
        }

        private void btnDeleteMedia_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                showWait();
                mediaFile = new DAL.Content();
                mediaFile = (DAL.Content)listBoxMedia.SelectedItem;
                proxy.RemoveMedia(mediaFile);
                contentCollection.Remove(mediaFile);
				showConfirmation();
            }
            catch (Exception ex)
            {
            }
        }



        private void btnAddScreen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                screenEdit = new DAL.Screen();
                gbAddScreen.DataContext = screenEdit;
                gbAddScreen.Visibility = Visibility.Visible;
                tbScreenID.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnApplyAddScreen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            showWait();
            screenEdit.Description = tbScreenDescription.Text;
            DAL.Screengroups scg = new DAL.Screengroups();
            scg = (DAL.Screengroups)cbScreenGroupsForAdd.SelectedItem;
            screenEdit.Groupid = (String)scg.Name;
            proxy.InsertScreen(screenEdit);
			showConfirmation();
            lbScreens.DataContext = null;
            lbScreens.DataContext = GetScreenCollection();
            gbAddScreen.Visibility = Visibility.Collapsed;
        }

        private void btnCancelAddScreen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            gbAddScreen.Visibility = Visibility.Collapsed;
        }

        private void gbAddScreen_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (gbAddScreen.Visibility == Visibility.Visible)
            {
                tbScreenID.Focus();
            }
        }

        private void btnRemoveScreen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                DAL.Screen _screen = new DAL.Screen();
                _screen = (DAL.Screen)lbScreens.SelectedItem;
                proxy.RemoveScreen(_screen);
                lbScreens.DataContext = null;
                lbScreens.DataContext = GetScreenCollection();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnAddLoop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            gbAddEditLoop.Visibility = Visibility.Visible;
            DateTime dt = DateTime.Now;
            tbLoopName.Text = "";
            tbLoopDateID.Text = "";
            if (dt.Day<10) 
            {
                tbLoopDateID.Text+= "0"+dt.Day.ToString();
            }
            else 
            {
                tbLoopDateID.Text+= dt.Day.ToString();
            }
            if (dt.Month<10) 
            {
                tbLoopDateID.Text+= "0"+dt.Month.ToString();
            }
            else 
            {
                tbLoopDateID.Text+= dt.Month.ToString();
            }
            if (dt.Year<10) 
            {
                tbLoopDateID.Text += "0" + dt.Year.ToString();
            }
            else 
            {
                tbLoopDateID.Text+= dt.Year.ToString();
            }
            try
            {
                cbLoopTemplate.SelectedIndex = 0;
            }
            catch (Exception ex)
            {   
            }
            tbLoopDateID.Text += "_" + dt.Second.ToString();
            tbLoopName.Focus();
        }

        private void btnRemoveLoop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                DAL.Loop _loop = new DAL.Loop();
                _loop = (DAL.Loop)lbMediaLoops.SelectedItem;
                //MessageBox.Show(_loop.Id.ToString());
                proxy.RemoveLoop(_loop);
            }
            catch (Exception ex)
            {
            }
            RefreshMediaLoops();
        }

        private void btnApplyAddLoop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            showWait();
            gbAddEditLoop.Visibility = Visibility.Collapsed;
            DAL.Loop _loop = new DAL.Loop();
            _loop.Name = tbLoopName.Text + "/" + tbLoopDateID.Text;
            _loop.Description = tbLoopDescription.Text;
            DAL.Template _template = new DAL.Template();
            _template = (DAL.Template)cbLoopTemplate.SelectedItem;
            _loop.Templateid = _template.Id;
            _loop.Templatename = _template.Name;
            proxy.InsertLoop(_loop);
            RefreshMediaLoops();
            //_loopCollection.Add(_loop);
            cbMediaLoop.DataContext = null;
            cbMediaLoop.DataContext = _loopCollection;
            cbMediaLoop.SelectedItem = _loop;
            lbMediaLoops.DataContext = null;
            lbMediaLoops.DataContext = _loopCollection;
            foreach (DAL.Loop item in _loopCollection)
            {
                if (item.Name == _loop.Name)
                {
                    lbMediaLoops.SelectedItem = item;
                }
            }
            showConfirmation();
            //lbMediaLoops.SelectedItem = _loop;
        }

        private void btnCancelAddLoop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            gbAddEditLoop.Visibility = Visibility.Collapsed;
        }

        private void RefreshMediaLoops()
        {
            _loopCollection = new ObservableCollection<DAL.Loop>();
            _loopCollection = proxy.CollectLoops();
            cbMediaLoop.DataContext = null;
            cbMediaLoop.DataContext = _loopCollection;
            lbMediaLoops.DataContext = null;
            lbMediaLoops.DataContext = _loopCollection;
            try
            {
                if (lbMediaLoops.SelectedIndex < 0) lbMediaLoops.SelectedIndex = 0;
                if (cbMediaLoop.SelectedIndex < 0) cbMediaLoop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }
        }

        private void ChangeZoneGrid()
        {
            DAL.Loop _loop = new DAL.Loop();
            ObservableCollection<DAL.Template> _zoneCollection = new ObservableCollection<DAL.Template>();
            ObservableCollection<DAL.Template> _tempzoneCollection = new ObservableCollection<DAL.Template>();
            //DAL.TemplateCollection _tempzoneCollection = new DAL.TemplateCollection();
            _loop = (DAL.Loop)lbMediaLoops.SelectedItem;
            //DAL.TemplateCollection _templateC = new DAL.TemplateCollection();
            ObservableCollection<DAL.Template> _templateC = new ObservableCollection<DAL.Template>();
            _templateC = proxy.CollectTemplates();
            DAL.Template _template = new DAL.Template();
            foreach (DAL.Template item in _templateC)
            {
                try
                {
                    if (item.Id == _loop.Templateid)
                    {
                        _template = item;
                        lbLoopZones.DataContext = null;
                        _tempzoneCollection = proxy.CollectZonesForTemplate(_template);
                        foreach (DAL.Template z in _tempzoneCollection)
                        {
                            if (cbfShowBackgroundZone.IsChecked == false)
                            {
                                if (z.Zonename != "Background") _zoneCollection.Add(z);
                            }
                            else
                            {
                                _zoneCollection.Add(z);
                            }

                        }

                        //lbLoopZones.DataContext = proxy.CollectZonesForTemplate(_template);
                        lbLoopZones.DataContext = _zoneCollection;
                    }
                }
                catch (Exception ex)
                {
                    lbLoopZones.DataContext = null;
                }
            }
            try
            {
                if (lbLoopZones.SelectedIndex < 0) lbLoopZones.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }
        }

        private void lbMediaLoops_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            e.Handled = true;
            ChangeZoneGrid();
        }

        //private void RefreshZoneMedia(DAL.Loop loop, string zoneid)
        //{

        //}

        private void RefreshAllMedia()
        {
            lbAllMedia.Items.Clear();
            foreach (var item in contentCollection)
            {
                lbAllMedia.Items.Add(item);
            }
            ApplyMediaFilterForLoops();
        }

        private void btnApplyMediatoZone_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            lbRubbishBin.Items.Clear();
            ApplyMediatoZone();
        }

        private void ApplyMediatoZone()
        {
            try
            {
                //LbMediaLoops
                showWait();
                DAL.Loop sLoop = new DAL.Loop();
                sLoop = (DAL.Loop)lbMediaLoops.SelectedItem;
                //MessageBox.Show(sLoop.Id + "/" + sLoop.Name);
                //lbLoopZones
                DAL.Template sTemplateZone = new DAL.Template();
                sTemplateZone = (DAL.Template)lbLoopZones.SelectedItem;
                //MessageBox.Show(sTemplateZone.Id + "/" + sTemplateZone.Zonename);
                //lbZoneMedia - applyMediatoZone
                ObservableCollection<DAL.Loop> loopCollection = new ObservableCollection<DAL.Loop>();
                //DAL.LoopCollection loopCollection = new DAL.LoopCollection();
                ObservableCollection<DAL.LoopContent> loopContentCollection = new ObservableCollection<DAL.LoopContent>();
                //DAL.LoopContentCollection loopContentCollection = new DAL.LoopContentCollection();
                //ObservableCollection<DAL.Content> contentForZone = new ObservableCollection<DAL.Content>();
                int _order = 1;
                foreach (DAL.Content item in lbZoneMedia.Items)
                {
                    //MessageBox.Show(item.Name);
                    DAL.LoopContent newLoopContent = new DAL.LoopContent();
                    newLoopContent.Loopid = sLoop.Id;
                    newLoopContent.Loopname = sLoop.Name;
                    newLoopContent.Templateid = sTemplateZone.Id;
                    newLoopContent.Templatename = sTemplateZone.Name;
                    newLoopContent.Mediaid = item.Id;
                    newLoopContent.Medianame = item.Name;
                    newLoopContent.Zoneid = sTemplateZone.Id;
                    newLoopContent.Zonename = sTemplateZone.Zonename;
                    newLoopContent.Order = _order;
                    _order++;
                    loopContentCollection.Add(newLoopContent);
                }
                proxy.ApplyLoopContentCollection(loopContentCollection);
				showConfirmation();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void radexpanderScheduling_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (rbDisplay.IsChecked == true)
            {
                GetDisplaySchedule();
            }
            else
            {
                GetGroupSchedule();
            }
            
			lbRubbishBin.Visibility = Visibility.Hidden;
            radexpanderLoopComposition.IsExpanded = false;
            radexpanderTickerManagement.IsExpanded = false;
        }

        private void radexpanderLoopComposition_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            lbRubbishBin.Items.Clear();
            lbRubbishBin.Visibility = Visibility.Visible;
			radexpanderTickerManagement.IsExpanded = false;
            radexpanderScheduling.IsExpanded = false;
        }

        private void lbLoopZones_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CollectMediaForLoopAndZone();
        }

        private void CollectMediaForLoopAndZone()
        {
            try
            {
                //LbMediaLoops
                DAL.Loop sLoop = new DAL.Loop();
                sLoop = (DAL.Loop)lbMediaLoops.SelectedItem;
                //lbLoopZones
                DAL.Template sTemplateZone = new DAL.Template();
                sTemplateZone = (DAL.Template)lbLoopZones.SelectedItem;

                ObservableCollection<DAL.Content> contentToAdd = new ObservableCollection<DAL.Content>();
                //DAL.LoopContentCollection loopContent = new DAL.LoopContentCollection();
                ObservableCollection<DAL.LoopContent> loopContent = new ObservableCollection<DAL.LoopContent>();
                loopContent = proxy.CollectLoopContentForZone(sTemplateZone, sLoop);
                var loopContentOrdered = from x in loopContent
                                         orderby x.Order ascending
                                         select x;
                lbZoneMedia.Items.Clear();
                RefreshAllMedia();
                foreach (DAL.Content item in lbAllMedia.Items)
                {
                    foreach (var lc in loopContentOrdered)
                    {
                        if (lc.Mediaid == item.Id)
                        {
                            item.Metadata9 = lc.Order.ToString();
                            contentToAdd.Add(item);
                        }
                    }
                }
                for (int i = 1; i < 100; i++)
                {
                    foreach (DAL.Content item in contentToAdd)
                    {   if (item.Metadata9 == i.ToString())
                           lbZoneMedia.Items.Add(item);
                    }
                }
                
                
            }
            catch (Exception ex)
            {   
            }
        }

        private void ApplyMediaFilter()
        {
            try
            {
                contentCollection.Clear();
                foreach (var item in allcontentCollection)
                {
                    if ((bool)cbfilterImages.IsChecked && item.Contenttype == "Image") contentCollection.Add(item);
                    if ((bool)cbfilterVideo.IsChecked && item.Contenttype == "Video") contentCollection.Add(item);
                    if ((bool)cbfilterFlash.IsChecked && item.Contenttype == "Flash") contentCollection.Add(item);
                    if ((bool)cbfilterTickers.IsChecked && item.Contenttype == "Ticker") contentCollection.Add(item);

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ApplyMediaFilterForLoops()
        {
            try
            {
                lbAllMedia.Items.Clear();
                //contentCollection.Clear();
                foreach (var item in allcontentCollection)
                {
                    if ((bool)cbfImage.IsChecked && item.Contenttype == "Image") lbAllMedia.Items.Add(item);
                    if ((bool)cbfVideo.IsChecked && item.Contenttype == "Video") lbAllMedia.Items.Add(item);// contentCollection.Add(item);
                    if ((bool)cbfFlash.IsChecked && item.Contenttype == "Flash") lbAllMedia.Items.Add(item);// contentCollection.Add(item);
                    if ((bool)cbfTicker.IsChecked && item.Contenttype == "Ticker") lbAllMedia.Items.Add(item);// contentCollection.Add(item);
                    if (item.Contenttype == "Action") lbAllMedia.Items.Add(item);// contentCollection.Add(item);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cbfilterImages_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterImages_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterVideo_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterFlash_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterTickers_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterLive_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterSelectDeselect_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterVideo_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterFlash_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterTickers_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterLive_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void cbfilterSelectDeselect_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplyMediaFilter();
        }

        private void ApplyRadMediaQuickFilter()
        {
            string medianame = "";
            string filename = "";
            string description = "";
            string allsearch = "";
            try
            {
                string searchterm = radMediaFilter.Text.ToLower();
                if (searchterm == "")
                {
                    ApplyMediaFilter();
                }
                else
                {
                    contentCollection.Clear();
                    foreach (DAL.Content item in allcontentCollection)
                    {
                        allsearch = "";
                        try
                        {
                            DAL.Content _content = new DAL.Content();
                            _content = item;
                            //MessageBox.Show(_content.Name);
                            try
                            {
                                filename = _content.Filelocation.ToLower();
                                allsearch += filename;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("fn" + ex.Message);
                            }
                            try
                            {
                                medianame = _content.Name.ToLower();
                                allsearch += medianame;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("mn" + ex.Message);
                            }
                            try
                            {
                                description = _content.Description.ToLower();
                                allsearch += description;
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show("de" + ex.Message);
                            }

                            if ((bool)cbfilterImages.IsChecked == true && item.Contenttype == "Image" && allsearch.Contains(searchterm)==true) contentCollection.Add(item);
                            if ((bool)cbfilterVideo.IsChecked == true && item.Contenttype == "Video" && allsearch.Contains(searchterm)==true) contentCollection.Add(item);
                            if ((bool)cbfilterFlash.IsChecked == true && item.Contenttype == "Flash" && allsearch.Contains(searchterm)==true) contentCollection.Add(item);
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
        }
        private void radMediaFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ApplyRadMediaQuickFilter();
        }

        private void radexpanderTickerManagement_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
			lbRubbishBin.Visibility = Visibility.Hidden;
			radexpanderLoopComposition.IsExpanded = false;
			radexpanderScheduling.IsExpanded = false;
        }

        private void RefreshTickers()
        {
            tickerCollection.Clear();
            foreach (var item in allcontentCollection)
            {
                if (item.Contenttype == "Ticker")
                {
                    tickerCollection.Add(item);
                    //MessageBox.Show(item.Name);
                }
            }
            lbTickerTapes.DataContext = tickerCollection;
        }

        private void btnAddNewTicker_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            si.sie("btnAddNewTicker_Click");
            try
            {
                cbTickerFont.SelectedIndex = 0;
                cbTickerSize.SelectedIndex = 0;
                ticker = new DAL.Content();
                ticker.Contenttype = "Ticker";
                borderTickerInformation.DataContext = ticker;
                EnableTickerChanges();
                tbTickerName.Focus();
            }
            catch (Exception ex)
            {
            }
            si.sil("btnAddNewTicker_Click");
        }

        private void EnableTickerChanges()
        {
            borderTickerInformation.IsEnabled = true;
            btnApplyTickerChanges.IsEnabled = true;
            btnCancelTickerChanges.IsEnabled = true;
            _tickerEditMode = true;
        }

        private void DisableTickerChanges()
        {
            _tickerEditMode = false;
            borderTickerInformation.IsEnabled = false;
            btnApplyTickerChanges.IsEnabled = false;
            btnCancelTickerChanges.IsEnabled = false;
            //FetchMediaCollection();
            RefreshTickers();
        
        }

        private void btnRemoveTicker_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                showWait();
                DAL.Content sTicker = new DAL.Content();
                sTicker = (DAL.Content)lbTickerTapes.SelectedItem;
                proxy.RemoveTicker(sTicker);
                try
                {
                    contentCollection.Remove(sTicker);
                }
                catch (Exception ex)
                {
                }
                try
                {
                    allcontentCollection.Remove(sTicker);
                }
                catch (Exception ex)
                {
                }
				showConfirmation();
            }
            catch (Exception ex)
            {
            }
            DisableTickerChanges();
            RefreshTickers();
        }

        private void btnEditTicker_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                ticker = new DAL.Content();
                ticker = (DAL.Content)lbTickerTapes.SelectedItem;
                borderTickerInformation.DataContext = ticker;
                cbTickerFont.Text = (string)ticker.Metadata2;
                cbTickerSize.SelectedValue = (string)ticker.Metadata3; 
                Color c = new Color();
                c = (Color)ColorConverter.ConvertFromString(ticker.Metadata4);
                cbTickerColour.SelectedColor = c;
                Color tbColor = new Color();
                try
                {
                    tbColor = (Color)ColorConverter.ConvertFromString(ticker.Metadata6);
                    cbTickerBackgroundColour.SelectedColor = tbColor;
                    //MessageBox.Show(ticker.Metadata5);
                    si.sii("TICKER SPEED=" + ticker.Metadata5);
                    sliderTickerSpeed.Value = Convert.ToDouble(ticker.Metadata5);
                    sliderTickerBackgroundOpacity.Value = Convert.ToDouble(ticker.Metadata7);
                }
                catch (Exception ex)
                {
                }
                EnableTickerChanges();
                tbTickerName.Focus();
            }
            catch (Exception ex)
            {
                DisableTickerChanges();
            }
        }

        private void btnCancelTickerChanges_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            DisableTickerChanges();
        }

        private void btnApplyTickerChanges_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            showWait();
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            ticker.Snapshot = LPImageLib.GetPhoto(@appPath + @"\Images\Ticker.jpg");
            proxy.InsertOrUpdateTicker(ticker);
            InsertOrUpdateTickerInContentCollections(ticker);
            DisableTickerChanges();
            FetchMediaCollection();
            showConfirmation();
            RefreshTickers();
        }

        private void InsertOrUpdateTickerInContentCollections(DAL.Content ticker)
        {
            Boolean found = false;
            try
            {
                foreach (var item in allcontentCollection)
                {
                    if (item.Name == ticker.Name)
                    {
                        found = true;
                    }
                    else
                    {
                    }
                }
                if (found == false) //insert new
                {
                    contentCollection.Add(ticker);
                    allcontentCollection.Add(ticker);
                    RefreshAllMedia();
                }
                else //update ticker in the collection
                {
                    foreach (var item in allcontentCollection)
                    {
                        if (item.Name == ticker.Name)
                        {
                            item.Metadata1 = ticker.Metadata1;
                            item.Metadata2 = ticker.Metadata2;
                            item.Metadata3 = ticker.Metadata3;
                            item.Metadata4 = ticker.Metadata4;
                            item.Metadata5 = ticker.Metadata5;
                            item.Metadata6 = ticker.Metadata6;
                            item.Metadata7 = ticker.Metadata7;
                            item.Metadata8 = ticker.Metadata8;
                            item.Metadata9 = ticker.Metadata9;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void lbTickerTapes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DAL.Content sTicker = new DAL.Content();
                sTicker = (DAL.Content)lbTickerTapes.SelectedItem;

                borderTickerInformation.DataContext = sTicker;
                cbTickerFont.Text = (string)sTicker.Metadata2;
                cbTickerSize.SelectedValue = (string)sTicker.Metadata3;
                Color c = new Color();
                c = (Color)ColorConverter.ConvertFromString(sTicker.Metadata4);
                cbTickerColour.SelectedColor = c;
                Color tbColor = new Color();
                try
                {
                    tbColor = (Color)ColorConverter.ConvertFromString(sTicker.Metadata6);
                    cbTickerBackgroundColour.SelectedColor = tbColor;
                    //MessageBox.Show(ticker.Metadata5);
                    si.sii("TICKER SPEED=" + sTicker.Metadata5);
                    sliderTickerSpeed.Value = Convert.ToDouble(sTicker.Metadata5);
                    sliderTickerBackgroundOpacity.Value = Convert.ToDouble(sTicker.Metadata7);
                }
                catch (Exception ex)
                {
                }

                UpdateSampleTicker(sTicker);
            }
            catch (Exception ex)
            {
                
            }
        }




        private void UpdateSampleTicker(DAL.Content sticker)
        {
            try
            {
                tbTickerSample.Text = sticker.Metadata8;
                FontFamilyConverter ffc = new FontFamilyConverter();
                FontFamily ff = new FontFamily();
                ff = (FontFamily)ffc.ConvertFromString(sticker.Metadata2);
                tbTickerSample.FontFamily = ff;
                tbTickerSample.FontSize = Convert.ToDouble(sticker.Metadata3);
                //Brush b = new 
                //Color c = new Color();
                BrushConverter bc = new BrushConverter();
                tbTickerSample.Foreground = (Brush)bc.ConvertFromString(sticker.Metadata4);
                gridTickerBackground.Background = (Brush)bc.ConvertFromString(sticker.Metadata6);
                gridTickerBackground.Opacity = Convert.ToDouble(sticker.Metadata7);
            }
            catch (Exception ex)
            {   
            }
        }

        private void applyChangesToTickerObject()
        {
            try
            {
                
                if (_tickerEditMode == true)
                {
                    ticker.Metadata5 = "10";
                    ticker.Metadata2 = cbTickerFont.SelectedValue.ToString();
                    ticker.Metadata3 = cbTickerSize.SelectedValue.ToString();
                    ticker.Metadata4 = cbTickerColour.SelectedColor.ToString();
                    //ticker.Metadata5 = sliderTickerSpeed.Value.ToString();
                    
                    ticker.Metadata6 = cbTickerBackgroundColour.SelectedColor.ToString();
                    ticker.Metadata7 = sliderTickerBackgroundOpacity.Value.ToString();
                    UpdateSampleTicker(ticker);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbTickerFont_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        	applyChangesToTickerObject();
        }

        private void cbTickerColour_SelectedColorChanged(object sender, C1.WPF.PropertyChangedEventArgs<System.Windows.Media.Color> e)
        {
            applyChangesToTickerObject();
        }

        private void cbTickerBackgroundColour_SelectedColorChanged(object sender, C1.WPF.PropertyChangedEventArgs<System.Windows.Media.Color> e)
        {
            applyChangesToTickerObject();
        }

        private void sliderTickerBackgroundOpacity_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            applyChangesToTickerObject();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            applyChangesToTickerObject();
        }

        private void cbTickerSize_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        	applyChangesToTickerObject();
        }

        private void rbDisplay_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (rbDisplay.IsChecked == true)
                {
                    GetDisplaySchedule();
                }
                else
                {
                    GetGroupSchedule();
                }

                lbRubbishBin.Visibility = Visibility.Hidden;
                radexpanderLoopComposition.IsExpanded = false;
                radexpanderTickerManagement.IsExpanded = false;
            }
            catch (Exception ex)
            {
            }
        }

        private void rbGroup_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (rbDisplay.IsChecked == true)
            {
                GetDisplaySchedule();
            }
            else
            {
                GetGroupSchedule();
            }

            lbRubbishBin.Visibility = Visibility.Hidden;
            radexpanderLoopComposition.IsExpanded = false;
            radexpanderTickerManagement.IsExpanded = false;
        }

        private void btnApplySchedule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ApplySelectedSchedule();
        }

        private void ApplySelectedSchedule()
        {
            try
            {
                Boolean errors = false;
                if (ScheduleAddEditMode == "edit")
                //Edit ScheduleAddEditMode
                {
                }
                else
                //Add New
                {
                    scheduleItem = new DAL.Schedule();
                    DateTime datestart = new DateTime();
                    DateTime schedstart = new DateTime();
                    DateTime schedend = new DateTime();
                    DateTime timestart = new DateTime();
                    DateTime starts = new DateTime();
                    DateTime ends = new DateTime();
                    //datestart = (DateTime)radSchedulingCalender.SelectedDate;
                    //schedstart = (DateTime)radtimePickerStart.SelectedValue;
                    //schedend = (DateTime)radtimePickerEnd.SelectedValue;
                    DateTime? t1 = new DateTime(datestart.Year, datestart.Month, datestart.Day, schedstart.Hour, schedstart.Minute, schedstart.Second);
                    DateTime? t2 = new DateTime(datestart.Year, datestart.Month, datestart.Day, schedend.Hour, schedend.Minute, schedend.Second);
                    starts = (DateTime)t1;
                    ends = (DateTime)t2;
                    //MessageBox.Show(starts.ToString());

                    //configureScheduleItem
                    scheduleItem.Loopstart = starts;
                    scheduleItem.Loopend= ends;
                    try
                    {
                        DAL.Loop loop = new DAL.Loop();
                        //loop = (DAL.Loop)cbMediaLoop.SelectedItem;
                        scheduleItem.Loopid = loop.Id;
                        scheduleItem.Loopname = loop.Name;
                        scheduleItem.Createdon = (DateTime)DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        errors = true;
                    }
                    try
                    {
                        if (rbGroup.IsChecked == true)
                        {
                            scheduleItem.Screenid = "0";
                            scheduleItem.Screenname = "";
                            DAL.Screengroups scrg = new DAL.Screengroups();
                            scrg = (DAL.Screengroups)cbGroup.SelectedItem;
                            scheduleItem.Groupid = scrg.Id;
                            scheduleItem.Groupname = scrg.Name;
                        }
                        else
                        {
                            scheduleItem.Groupid = "0";
                            scheduleItem.Groupname = "";
                            DAL.Screen scr = new DAL.Screen();
                            scr = (DAL.Screen)cbDisplay.SelectedItem;
                            scheduleItem.Screenid = scr.Id;
                            scheduleItem.Screenname = scr.Screenname;
                        }
                    }
                    catch (Exception ex)
                    {
                        si.sii("Error creating schedule item");
                        si.six(ex);
                        errors = true;
                    }
                    if (errors == false)
                    {
                        si.sii("Creating OK, inserting Schedule Item");
                        proxy.InsertScheduleItem(scheduleItem);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnAddToSchedule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                DAL.Loop newLoop = new DAL.Loop();
                newLoop = (DAL.Loop)cbMediaLoop.SelectedItem;
                AddToSchedule(newLoop);  
            }
            catch (Exception ex)
            {       
            }
        }

        private void btnEditSchedule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            ScheduleAddEditMode = "edit";
        }

        private void btnDeleteSchedule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            DAL.RDB proxy = new DAL.RDB();
            try
            {
                Collection<Appointment> appTD = new Collection<Appointment>();
                Appointment appToDelete = new Appointment();
                foreach (var item in devCal.SelectedAppointments)
                {
                    appTD.Add(item.Appointment);
                }
                foreach (var item in appTD)
                {
                    _Model.Appointments.Remove(item);
                    proxy.DeleteScheduleWithID(item.ImageSource);
                }
                devCal.CalendarModel = null;
                devCal.CalendarModel = _Model;
            }
            catch (Exception ex)
            {
            }
        }

        private void cbGroup_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (rbGroup.IsChecked == true)
                {
                }
            }
            catch (Exception ex)
            {       
            }
        }

        private void cbDisplay_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            checkForDisplaySchedule();
        }

        private void checkForDisplaySchedule()
        {
            try
            {
                if (rbDisplay.IsChecked == true)
                {
                    DAL.Screen scr = new DAL.Screen();
                    scr = (DAL.Screen)cbDisplay.SelectedItem;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void AddScreensToCal()
        {
            devCal.DisplayedOwners.Clear();
            try
            {
                //DAL.ScreenCollection sc = new DAL.ScreenCollection();

                ObservableCollection<DAL.Screen> sc = new ObservableCollection<DAL.Screen>();

                sc = proxy.CollectScreens();
                foreach (DAL.Screen screenName in sc)
                {
                    Boolean found = false;
                    foreach (var item in devCal.DisplayedOwners)
                    {
                        if (item == screenName.Screenname) found = true;
                    }
                    if (found == false) devCal.DisplayedOwners.Add(screenName.Screenname);
                }
            }
            catch (Exception ex)
            {                
            }
        }

        private void AddGroupsToCal()
        {
            try
            {
                devCal.DisplayedOwners.Clear();
                ObservableCollection<DAL.Screengroups> sc = new ObservableCollection<DAL.Screengroups>();
                sc = proxy.CollectScreenGroups();
                foreach (DAL.Screengroups screenName in sc)
                {
                    Boolean found = false;
                    foreach (var item in devCal.DisplayedOwners)
                    {
                        if (item == screenName.Name) found = true;
                    }
                    if (found == false) devCal.DisplayedOwners.Add(screenName.Name);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void AddToSchedule(DAL.Loop selectedLoop)
        {
            try
            {
                Appointment appointment = new Appointment();
                appointment.Subject = selectedLoop.Name;
                appointment.Id = 0;
                
                appointment.StartTime = (DateTime)devCal.DateSelectionStart;
                appointment.EndTime = (DateTime)devCal.DateSelectionEnd;
                appointment.OwnerKey = devCal.SelectedOwnerKey;

                appointment.TimeMarkedAs = Appointment.TimerMarkerBusy;
                appointment.Description = selectedLoop.Description;

                _Model.Appointments.Add(appointment);
                devCal.EnsureVisible(appointment);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnTest1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(devCal.SelectedOwnerKey);
        }

        private void devCalInterval_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                devCal.TimeSlotDuration = (int)devCalInterval.Value;
            }
            catch (Exception ex)
            {   
            }
        }

        private void lbMediaLoops_DropDownOpened(object sender, System.EventArgs e)
        {
            try
            {
                if (lbMediaLoops.Items.Count <= 0) RefreshMediaLoops();
            }
            catch (Exception ex)
            {    
            }
        }

        private void lbRubbishBin_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lbRubbishBin.SelectedIndex >= 0) lbRubbishBin.SelectedIndex = -1;
        }

        private void cbMediaLoop_DropDownOpened(object sender, System.EventArgs e)
        {
            try
            {
                if (cbMediaLoop.Items.Count <= 0)
                {
                    RefreshMediaLoops();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GetDisplaySchedule()
        {
            //DAL.ScheduleCollection scheduleCollection = new DAL.ScheduleCollection();
            ObservableCollection<DAL.Schedule> scheduleCollection = new ObservableCollection<DAL.Schedule>();

            scheduleCollection = proxy.CollectScheduleForScreen();
            _Model.Appointments.Clear();
            devCal.DisplayedOwners.Clear();
            AddScreensToCal();
            foreach (var item in scheduleCollection)
            {
                if (item.Screenname != "")
                {
                    Appointment appointment = new Appointment();
                    appointment.EndTime = (DateTime)item.Loopend;
                    appointment.StartTime = (DateTime)item.Loopstart;
                    appointment.Subject = item.Loopname;
                    appointment.OwnerKey = item.Screenname;
                    appointment.ImageSource = item.Id;
                    _Model.Appointments.Add(appointment);
                }
            }

            scheduleCollection.Clear();
        }

        private void GetGroupSchedule()
        {
            //DAL.ScheduleCollection scheduleCollection = new DAL.ScheduleCollection();
            ObservableCollection<DAL.Schedule> scheduleCollection = new ObservableCollection<DAL.Schedule>();
            scheduleCollection = proxy.CollectScheduleForScreen();
            _Model.Appointments.Clear();
            devCal.DisplayedOwners.Clear();
            AddGroupsToCal();
            foreach (var item in scheduleCollection)
            {
                if (item.Groupname != "")
                {
                    Appointment appointment = new Appointment();
                    appointment.EndTime = (DateTime)item.Loopend;
                    appointment.StartTime = (DateTime)item.Loopstart;
                    appointment.Subject = item.Loopname;
                    appointment.OwnerKey = item.Groupname;
                    _Model.Appointments.Add(appointment);
                }
            }
            scheduleCollection.Clear();
        }

        private void PublishDisplaySchedule()
        {
            DAL.RDB proxy = new DAL.RDB();
            si.sii("Publishing Display Schedule");
            //ObservableCollection<DAL.Schedule> scheduleCollection = new ObservableCollection<DAL.Schedule>();
            try
            {
                //Build Schedule
                showWait();

                si.sii("Appointments = " + _Model.Appointments.Count().ToString());

                foreach (var item in _Model.Appointments)
                {
                    DAL.Schedule newSchedule = new DAL.Schedule();
                    newSchedule.Groupid = "0";
                    newSchedule.Groupname = "";
                    newSchedule.Loopname = item.Subject;
                    newSchedule.Screenname = item.OwnerKey;
                    newSchedule.Loopstart = item.StartTime;
                    newSchedule.Loopend = item.EndTime;
                    //scheduleCollection.Add(newSchedule);
                    try //imageSource is not null (imageSource is the schedule ID in RDB), try update first, if that fails then the ID wasn't set, do an insert
                    {
                        si.sii("Update Schedule Now");
                        proxy.UpdateScheduleForScreen(newSchedule, item.ImageSource);
                    }
                    catch (Exception)
                    {
                        si.sii("Inserting New Schedule Now");
                        item.ImageSource = proxy.InsertNewScheduleForScreen(newSchedule);
                    }
                }
                
            }
            catch (Exception ex)
            {
                si.six(ex);
            }

            showConfirmation();
            
        }



        private void PublishGroupSchedule()
        {
            //DAL.ScheduleCollection scheduleCollection = new DAL.ScheduleCollection();
            ObservableCollection<DAL.Schedule> scheduleCollection = new ObservableCollection<DAL.Schedule>();
            try
            {
                showWait();
                foreach (var item in _Model.Appointments)
                {
                    DAL.Schedule newSchedule = new DAL.Schedule();
                    newSchedule.Groupid = "0";
                    newSchedule.Groupname = item.OwnerKey;
                    newSchedule.Loopname = item.Subject;
                    newSchedule.Screenname = "";
                    newSchedule.Loopstart = item.StartTime;
                    newSchedule.Loopend = item.EndTime;
                    scheduleCollection.Add(newSchedule);
                }
                showConfirmation();
            }
            catch (Exception ex)
            {
            }
            if (scheduleCollection.Count > 0) proxy.UpdateScheduleForGroups(scheduleCollection);
        }



        private void btnPublishSchedule_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            CheckExpiry();
            try
            {
                if (rbDisplay.IsChecked == true)
                {
                    PublishDisplaySchedule();
                }
                else
                {
                    PublishGroupSchedule();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnTestFetch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (rbDisplay.IsChecked == true)
            {
                GetDisplaySchedule();
            }
            else
            {
                GetGroupSchedule();
            }
        }

        private void cbfImage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	ApplyMediaFilterForLoops();
        }

        private void cbfVideo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	ApplyMediaFilterForLoops();
        }

        private void cbfFlash_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	ApplyMediaFilterForLoops();
        }

        private void cbfTicker_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	ApplyMediaFilterForLoops();
        }

        private void gbAddEditLoop_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (gbAddEditLoop.Visibility == Visibility.Visible)
                {
                    tbLoopName.Focus();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cbfShowBackgroundZone_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	ChangeZoneGrid();
        }

        private void datePickerScheduling_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            devCal.TimelineViewStartDate = (DateTime)datePickerScheduling.SelectedDate;
            devCal.DayViewDate = (DateTime)datePickerScheduling.SelectedDate;
            devCal.WeekViewStartDate = (DateTime)datePickerScheduling.SelectedDate;
            devCal.MonthViewStartDate = (DateTime)datePickerScheduling.SelectedDate;
        }

        private void radexpanderLoopComposition_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lbRubbishBin.Items.Clear();
            lbRubbishBin.Visibility = Visibility.Visible;
        }

        private void btnCreateAction_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PopulateAddActionCombos();
            tbActionName.Text = "";
            tbActionDescription.Text = "";
            ContentAddEditMode = "add";
            mediaFile = new DAL.Content();
            mediaFile.Importdate = (DateTime)DateTime.Now;
        	gbAddAction.Visibility = Visibility.Visible;
        }

        private void btnCancelAddAction_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            gbAddAction.Visibility = Visibility.Collapsed;
        }

        private void btnApplyAction_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            e.Handled = true;
            mediaFile.Filesize = 0;
            mediaFile.Contenttype = "Action";
            if (ContentAddEditMode == "add")
            {
                showWait();
                //mediaFile.Metadata9 = sliderVolume.Value.ToString();
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (mediaFile.Contenttype == "Action")
                {
                    mediaFile.Snapshot = LPImageLib.GetPhoto(@appPath + @"\Images\action.jpg");
                }
                else
                {
                    mediaFile.Snapshot = LPImageLib.GetPhoto(appPath + @"\reduced.jpg");
                }
                DateTime dt = DateTime.Now;
                string dy = dt.DayOfYear.ToString();
                string dh = dt.Minute.ToString();
                string dm = dt.Second.ToString();
                //string mfn = mediaFile.Filelocation;
                //string mfnPath = System.IO.Path.GetDirectoryName(mfn);
                //string mfnExt = System.IO.Path.GetExtension(mfn);
                //string mfnFile = @mfnPath + @"\" + @System.IO.Path.GetFileNameWithoutExtension(mfn) + "_" + dy + dh + dm + mfnExt;
                //mediaFile.Metadata8 = mfn;
                mediaFile.Name = tbActionName.Text;
                mediaFile.Filelocation = tbActionNetworkIP.Text;
                mediaFile.Description = tbActionDescription.Text;
                //mediaFile.Filelocation = "Action";
                mediaFile.Id = proxy.InsertMedia(mediaFile);
                mediaFile.Metadata2 = (string)cbActionType.SelectedValue;
                mediaFile.Metadata3 = (string)cbActionVar1.SelectedValue;
                mediaFile.Metadata4 = (string)cbActionVar2.SelectedValue;
                mediaFile.Metadata5 = (string)tbActionNetworkIP.Text;
                //ftpQ.Enqueue(mediaFile);
                contentCollection.Add(mediaFile);
                allcontentCollection.Add(mediaFile);
				showConfirmation();
            };
            
            gbAddAction.Visibility = Visibility.Collapsed;
        }

        private void PopulateAddActionCombos()
        {
            try
            {
                string s = "";
                cbActionType.Items.Clear();
                s = " Stream Media"; cbActionType.Items.Add(s);
                s = " IR Channel Change"; cbActionType.Items.Add(s);
                cbActionVar1.Visibility = Visibility.Hidden;
                cbActionVar2.Visibility = Visibility.Hidden;
                tbActionNetworkIP.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
            }
        }

        private void cbActionType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbActionType.SelectedIndex < 0)
            {
                cbActionVar1.Visibility = Visibility.Hidden;
                cbActionVar2.Visibility = Visibility.Hidden;
                tbActionNetworkIP.Visibility = Visibility.Hidden;
            }
            if (cbActionType.SelectedIndex == 0)
            {
                string s = "";
                cbActionVar1.Items.Clear();
                s = " Local Capture Card"; cbActionVar1.Items.Add(s);
                s = " Network Source"; cbActionVar1.Items.Add(s);
                cbActionVar1.Visibility = Visibility.Visible;
               
            }
            if (cbActionType.SelectedIndex == 1)
            {
                string s = "";
                cbActionVar1.Items.Clear();
                for (int i = 0; i < 350; i++)
                {
                    s = " Channel " + i.ToString();
                    cbActionVar1.Items.Add(s);
                }
                cbActionVar1.Visibility = Visibility.Visible;
            }
        }

        private void cbActionVar1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbActionVar1.SelectedIndex < 0)
            {
                //cbActionVar1.Visibility = Visibility.Hidden;
                cbActionVar2.Visibility = Visibility.Hidden;
                tbActionNetworkIP.Visibility = Visibility.Hidden;
            }
            else
            if (cbActionVar1.SelectedIndex == 0 && cbActionType.SelectedIndex == 0)
            {
                string s = "";
                tbActionNetworkIP.Text = "127.0.0.1";
                tbActionNetworkIP.Visibility = Visibility.Hidden;

            }
            else
            if (cbActionVar1.SelectedIndex == 0 && cbActionType.SelectedIndex == 1)
            {
                string s = "";
                tbActionNetworkIP.Text = "127.0.0.1";
                tbActionNetworkIP.Visibility = Visibility.Visible;
                tbActionNetworkIP.Focus();
            }
            else
            {
                string s = "";
                tbActionNetworkIP.Text = "127.0.0.1";
                tbActionNetworkIP.Visibility = Visibility.Visible;
                tbActionNetworkIP.Focus();
            };
           

        }

        private void btnConference_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	e.Handled = true;
            lbRubbishBin.Visibility = Visibility.Collapsed;
            //radexpanderScheduling.IsExpanded = false;

            //InitializeConferencingPage();

            //radexpanderLoopComposition.IsExpanded = true;
            tbSectionHeading.Text = "Conferencing";
            animationControl.StartStateTransition();

            //cbLoopTemplate.DataContext = proxy.CollectTemplates();
            //FetchMediaCollection();
            //RefreshAllMedia();
            //RefreshTickers();
            gridConfiguration.Visibility = Visibility.Collapsed;
            gridLogin.Visibility = Visibility.Collapsed;
            gridMedia.Visibility = Visibility.Collapsed;
            gridStatus.Visibility = Visibility.Collapsed;
            gridScheduling.Visibility = Visibility.Collapsed;
            gridConferencing.Visibility = Visibility.Visible;
            //si.sii("Collect Ticker information - Core Service dataset exception - exceeds 1024 byte buffer : Ticker count is 0");
            //si.sii("Collect Schedule pointers - Core Service dataset exception - exceeds 1024 byte buffer : Schedule count is 0");
            //DisableTickerChanges();
            animationControl.AnimateStateTransition();

            //rbDisplay.IsChecked = true;
            //_Model.Appointments.Clear();
            //devCal.DisplayedOwners.Clear();
            //AddScreensToCal();
        }

        private void btnSaveAttachment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //proxy.GetAndSaveAttachment(@"c:\tmp\test\file.wmv", "contents/161");
            proxy.DeleteAllAtachments();
            
        }

       

    }
}
