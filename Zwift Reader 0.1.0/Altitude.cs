using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zwift_Reader_0._1._0
{
    class Altitude
    {
        //lists
        public static List<double> alt_data = new List<double>();

        //variables
        public static double max_alt = 0;
        public static double min_alt = 0;
        public static double meters_climbed = 0;
        public static double meters_descended = 0;

        public static void Find_Max_Altitude(List<double> alt_list)
        {
            //meters
            max_alt = alt_list.Max();
            min_alt = alt_list.Min();
        }


        //obvs
        public static void Find_Altitude_Climbed(List<double> alt_list)
        {
            //set a local altitude to check against
            double old_alt = alt_list[0];
            //set up a counter that we will return at the end
            double alt_gained = 0;

            //check each altitude point against the previous point and add it to alt_gained if it went up
            for (int i = 1; i < alt_list.Count; i++)
            {
                if (alt_list[i] > old_alt)
                {
                    alt_gained = alt_gained + (alt_list[i] - old_alt);
                }

                old_alt = alt_list[i];
            }

            meters_climbed = alt_gained;
        }


        //obvs again
        public static void Find_Altitude_Dropped(List<double> alt_list)
        {
            //set a local altitude to check against
            double old_alt = alt_list[0];
            //set up a counter that we will return at the end
            double alt_dropped = 0;

            //check each altitude point against the previous point and add it to alt_dropped if it went down
            for (int i = 1; i < alt_list.Count; i++)
            {
                if (alt_list[i] < old_alt)
                {
                    alt_dropped = alt_dropped + (old_alt - alt_list[i]);
                }

                old_alt = alt_list[i];
            }

            meters_descended = alt_dropped;
        }

        //creates a list of the differences in alt between each data point (effectively the 'opposite' of the triangle)
        public static void Generate_Altitude_Step_List(List<double> alt_list)
        {
            //set a first point
            double old_alt = alt_list[0];

            //for each point in the list, compare it to the one prior
            for (int i = 1; i < alt_list.Count; i++)
            {
                Gradient.altitude_step.Add(alt_list[i] - old_alt);
                old_alt = alt_list[i];
            }
        }

    }
}
