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

namespace Zwift_Reader_0._1._0
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Page
    {
        public Overview()
        {
            InitializeComponent();

            //headers
            ride_type.Text = Decoders.device_name_string;
            start_time.Text = Time.ActivityStartTime.ToString("hh:mm tt");
            ride_date.Text = Time.ActivityStartTime.ToString("dd/MM/yyyy");

            //averages
            avg_power_val.Text = Power.total_avg_watts.ToString("n2") + " Watts";
            avg_cadence_val.Text = Cadence.total_avg_cadence.ToString("n2") + " RPM";
            avg_speed_val.Text = Speed.total_avg_speed.ToString("n2") + "KM/H";


            //max data
            ride_time_val.Text = Time.total_ride_time_string;
            max_power_val.Text = Power.max_power.ToString();
            max_cadence_val.Text = Cadence.max_cadence.ToString();
            max_speed_val.Text = Speed.max_speed.ToString("n2") + " KM/H";
            alt_gained_val.Text = Altitude.meters_climbed.ToString("n2") + " m";
            calories_burned_val.Text = Power.calories_burned.ToString("n2") + " kCal";
            max_incline_val.Text = Gradient.max_gradient.ToString("n2") + " %";
            max_decline_val.Text = Gradient.max_slope.ToString("n2") + " %";


            //ftp
            ftp.Text = Power.ftp.ToString("n2");

            //CP
            one_cp_val.Text = Power.one_second_max_pwr.ToString("n2");
            ten_second_cp.Text = Power.ten_second_max_pwr.ToString("n2");
            sixty_second_cp.Text = Power.one_minute_max_pwr.ToString("n2");
            five_min_cp.Text = Power.five_minute_max_pwr.ToString("n2");
            twenty_min_cp.Text = Power.twenty_minute_max_pwr.ToString("n2");
            sixty_min_cp.Text = Power.sixty_minute_max_pwr.ToString("n2");
        }
    }
}
