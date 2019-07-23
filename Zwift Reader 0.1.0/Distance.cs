using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zwift_Reader_0._1._0
{
    class Distance
    {
        public static double total_distance = 0;
        public static double total_distance_in_kms = 0;
        public static List<double> distance_data = new List<double>();


        public static void Find_Total_Ride_Distance(List<double> distance_list)
        {
            //meters
            total_distance = distance_list.Max();
        }

        public static void Convert_To_KMS(double distance)
        {
            total_distance_in_kms = distance / 1000;
        }

        //creates a list of the differences in distance between each data point (effectively the 'hypotenuse' of the triangle)
        public static void Generate_Distance_Step_List(List<double> distance_list)
        {
            //set a first point
            double old_alt = distance_list[0];

            //for each point in the list, compare it to the one prior
            for (int i = 1; i < distance_list.Count; i++)
            {
                Gradient.distance_step.Add(distance_list[i] - old_alt);
                old_alt = distance_list[i];
            }
        }
    }
}
