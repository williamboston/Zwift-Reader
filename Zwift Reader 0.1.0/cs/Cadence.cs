using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zwift_Reader_0._1._0
{
    class Cadence
    {
        //lists
        public static List<int> cadence_data = new List<int>();
        public static List<int> active_cadence_data = new List<int>();

        //counters
        public static int active_cadence_data_count = 0;

        //variables
        public static int max_cadence = 0;
        public static int total_cumulative_cadence = 0;
        public static double total_avg_cadence = 0;
        public static int moving_cumulative_cadence = 0;
        public static double moving_avg_cadence = 0;



        public static void Find_Max_Cadence(List<int> cadence_list)
        {
            //RPM
            max_cadence = cadence_list.Max();
        }


        //including deadzones
        public static void Find_Total_Average_Cadence(List<int> cadence_data)
        {
            //first we find (and save) the cumulative cadence total
            for (int i = 0; i < cadence_data.Count - 1; i++)
            {
                total_cumulative_cadence = total_cumulative_cadence + cadence_data[i];
            }

            //then find the average using the total number of data points
            total_avg_cadence = (double)total_cumulative_cadence / Decoders.data_count;
        }


        //not including deadzones
        public static void Find_Moving_Average_Cadence(List<int> cadence_data)
        {
            //first we find (and save) the cumulative cadence when moving
            for (int i = 0; i < cadence_data.Count - 1; i++)
            {
                moving_cumulative_cadence = moving_cumulative_cadence + cadence_data[i];
            }

            //then find the average using the total number of data points
            moving_avg_cadence = (double)moving_cumulative_cadence / active_cadence_data_count;
        }
    }
}
