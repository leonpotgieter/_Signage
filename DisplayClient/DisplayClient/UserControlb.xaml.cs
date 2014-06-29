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

namespace DisplayClient
{
    /// <summary>
    /// Interaction logic for UserControlb.xaml
    /// </summary>
    public partial class UserControlb : UserControl
    {
        public static int MediaState = 0; //0 inactive, 1 playing, 2 mediaended
        public UserControlb()
        {
            InitializeComponent();
        }

        private void mediaElementB_MediaOpened(object sender, RoutedEventArgs e)
        {
            MediaState = 1;
        }

        private void mediaElementB_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaState = 2;
        }
    }
}
