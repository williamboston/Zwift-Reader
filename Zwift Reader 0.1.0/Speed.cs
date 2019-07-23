using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zwift_Reader_0._1._0
{
    class Speed
    {
        //lists
        public static List<double> speed_data = new List<double>();
        public static List<double> active_speed_data = new List<double>();

        //counters
        public static int active_speed_data_count = 0;
        public static double cumulative_speed = 0;
        public static double total_avg_speed = 0;
        public static double moving_avg_speed = 0;
        public static double moving_cumulative_speed = 0;

        //variables
        public static double max_speed = 0;

        public static void Find_Max_Speed(List<double> speed_list)
        {
            //km/h
            max_speed = speed_list.Max();
        }

        //including deadzones
        public static void Find_Total_Average_Speed(List<double> speed_list)
        {
            //find cumulative total
            for (int i = 0; i < speed_list.Count - 1; i++)
            {
                cumulative_speed = cumulative_speed + speed_list[i];
            }

            //find the average
            total_avg_speed = cumulative_speed / Decoders.data_count;
        }


        //not including deadzones
        public static void Find_Moving_Average_Speed(List<double> speed_list)
        {
            //find cumulative total
            for (int i = 0; i < speed_list.Count - 1; i++)
            {
                moving_cumulative_speed = moving_cumulative_speed + speed_list[i];
            }

            //find the average
            moving_avg_speed = moving_cumulative_speed / active_speed_data_count;
        }
    }
}
