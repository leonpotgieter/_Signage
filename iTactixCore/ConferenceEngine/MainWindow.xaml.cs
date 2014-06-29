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
using Mindscape.LightSpeed;

namespace ConferenceEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
           // SDBService1 proxy = new SDBService1();
            ServiceReference1.CoreServiceClient proxy = new ServiceReference1.CoreServiceClient();
             string s = proxy.ReturnHey("l");
            MessageBox.Show(s);
            try
            {
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
