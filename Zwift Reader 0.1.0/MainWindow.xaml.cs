using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Dynastream.Fit;

namespace Zwift_Reader_0._1._0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_File(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();

            // Check if the file exists and is valid
            if (result == true && (openFileDlg.FileName.EndsWith(".fit")))
            {
                //clear the old values - NOT IMPLEMENTED YET
                Helpers.Clear_All_Values();

                // calls the master decode process on the given fit file
                Master.Main(openFileDlg.FileName);
                this.run_time.Text = "In " + Time.RunTime.ToString("s\\.ffff")+ " Seconds";
                Frame.Content = new Overview();
            }
            else
            {
                MessageBox.Show("Please select a .fit file", "Invalid File");
            }
        }

        //directions for opening 'pages'
        private void Open_Overview(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Overview();
        }

        private void Open_Detailed(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Detailed();
        }
        /*
        private void Open_CP(object sender, RoutedEventArgs e)
        {
            Frame.Content = new CP();
        }
        */
        //see frame .xaml.cs pages for indiviudal page calls/functions
    }
}
