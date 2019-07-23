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
    /// Interaction logic for Detailed.xaml
    /// </summary>
    public partial class Detailed : Page
    {
        public Detailed()
        {
            InitializeComponent();

            //headers
            ride_type.Text = Decoders.device_name_string;
            start_time.Text = Time.ActivityStartTime.ToString("hh:mm tt");
            ride_date.Text = Time.ActivityStartTime.ToString("dd/MM/yyyy");
            data_points.Text = Decoders.data_count + " Data Points";

            //Averages
            total_avg_power.Text = Power.total_avg_watts.ToString("n2");
            moving_avg_power.Text = Power.moving_avg_watts.ToString("n2");
            total_avg_cadence.Text = Cadence.total_avg_cadence.ToString("n2") + " RPM";
            moving_avg_cadence.Text = Cadence.total_avg_cadence.ToString("n2") + " RPM";
            total_avg_speed.Text = Speed.total_avg_speed.ToString("n2") + " KM/H";
            moving_avg_speed.Text = Speed.moving_avg_speed.ToString("n2") + " KM/H";

            //Time
            start_time_block.Text = Time.ActivityStartTime.ToString("hh:mm tt");
            end_time_block.Text = Time.ActivityEndTime.ToString("hh:mm tt");
            total_ride_time.Text = Time.total_ride_time_string;
            active_ride_time.Text = Time.total_move_time_string;
            inactive_ride_time.Text = Time.total_inactive_time_string;
            percent_active.Text = Time.active_time_percent.ToString("n2")+ " %";
            percent_inactive.Text = Time.inactive_time_percent.ToString("n2")+ " %";

            //Altitude
            alt_gained.Text = Altitude.meters_climbed.ToString("n2")+ " m";
            alt_dropped.Text = Altitude.meters_descended.ToString("n2")+ " m";
            avg_gradient.Text = Gradient.average_gradient.ToString("n6") + "%";
            high_alt.Text = Altitude.max_alt.ToString("n2") + " m";
            low_alt.Text = Altitude.min_alt.ToString("n2") + " m";

            //CP
            //seconds
            one_second.Text = "1:     " + Power.one_second_max_pwr.ToString("n2");
            two_second.Text = "2:     " + Power.two_second_max_pwr.ToString("n2");
            five_second.Text = "5:     " + Power.five_second_max_pwr.ToString("n2");
            ten_second.Text = "10:    " + Power.ten_second_max_pwr.ToString("n2");
            twenty_second.Text = "20:    " + Power.twenty_second_max_pwr.ToString("n2");
            thirty_second.Text = "30:    " + Power.thirty_second_max_pwr.ToString("n2");
            sixty_second.Text = "60:    " + Power.one_minute_max_pwr.ToString("n2");

            //mins
            one_minute.Text = "1:     " + Power.one_minute_max_pwr.ToString("n2");
            two_minute.Text = "2:     " + Power.two_minute_max_pwr.ToString("n2");
            five_minute.Text = "5:     " + Power.five_minute_max_pwr.ToString("n2");
            ten_minute.Text = "10:    " + Power.ten_minute_max_pwr.ToString("n2");
            twenty_minute.Text = "20:    " + Power.twenty_minute_max_pwr.ToString("n2");
            thirty_minute.Text = "30:    " + Power.thirty_minute_max_pwr.ToString("n2");
            sixty_minute.Text = "60:    " + Power.sixty_minute_max_pwr.ToString("n2");

            //hour
            one_hour.Text = "1:     " + Power.sixty_minute_max_pwr.ToString("n2");
            one_five_hour.Text = "1.5:   " + Power.ninety_minute_max_pwr.ToString("n2");
            two_hour.Text = "2:     " + Power.two_hour_max_pwr.ToString("n2");
            three_hour.Text = "3:     " + Power.three_hour_max_pwr.ToString("n2");
            four_hour.Text = "4:     " + Power.four_hour_max_pwr.ToString("n2");
            five_hour.Text = "5:     " + Power.five_hour_max_pwr.ToString("n2");

        }
    }
}
