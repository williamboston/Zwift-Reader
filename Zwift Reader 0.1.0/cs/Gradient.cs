using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zwift_Reader_0._1._0
{
    class Gradient
    {
        //lists
        public static List<double> gradient_data = new List<double>();
        public static List<double> altitude_step = new List<double>();
        public static List<double> distance_step = new List<double>();

        //variables
        public static double max_gradient = 0;
        public static double max_slope = 0;
        public static double average_gradient = 0;

        //takes the change in distance as hypotenuse and the change is altitude as the opposite and SOH finds the gradient of each individual data point
        public static void Generate_Gradient_Step_List()
        {
            double sin_agent, gradient_rads, gradient_return;

            //lists of differences between each step
            Altitude.Generate_Altitude_Step_List(Altitude.alt_data);
            Distance.Generate_Distance_Step_List(Distance.distance_data);

            for (int i = 0; i < Decoders.data_count - 1; i++)
            {
                //find the RHS of SOH
                sin_agent = altitude_step[i] / distance_step[i];

                //elimininating 0/0 errors
                if (double.IsNaN(sin_agent))
                {
                    sin_agent = 0;
                }

                //reverse the thingy
                gradient_rads = Math.Asin(sin_agent);

                //convert back from radians
                gradient_return = gradient_rads * (180 / Math.PI);


                //add gradient (angle) to the gradient list above
                if (!double.IsNaN(gradient_return))
                {
                    gradient_data.Add(gradient_return);
                }

            }
        }


        //calls get_average_gradient (below)
        public static void Find_Avg_Gradient()
        {
            double cumulative_gradient_coefficient = Get_Cumulative_Gradient_Coefficient();

            average_gradient = Get_Average_Gradient(cumulative_gradient_coefficient, Distance.total_distance);
        }


        //finds the cumulative total for distance*gradient for each step **
        private static double Get_Cumulative_Gradient_Coefficient()
        {
            double cumulative_gradient = 0;

            for (int i = 0; i < Decoders.data_count - 1; i++)
            {
                cumulative_gradient = cumulative_gradient + (gradient_data[i] * distance_step[i]); //**
            }

            return cumulative_gradient;
        }


        //takes the cumualtive total of (each step distance * each step gradient) and divides it by the total ride distance
        //this is done to take distance into account when trying to find 'average'
        //for example 40m at 10% is not the same as 40m at 2% and 150m at 2% might be the same as 20m at 7%
        //data points are captured once per second so using that metric you get completely useless data
        //you need to work out the 'coefficient' of angle AND distance for each step to get an accurate measure of 'effort/difficulty'
        //this may be completely wrong - the output of the calc is not really useful info anyway but hey i did it
        private static double Get_Average_Gradient(double cumulative_gradient, double cumulative_distance)
        {
            double avg_gradient = cumulative_gradient / cumulative_distance;

            return avg_gradient;
        }


        public static void Find_Steepest_Gradient(List<double> gradient_list)
        {
            max_gradient = gradient_list.Max();
        }


        public static void Find_Steepest_Descent(List<double> gradient_list)
        {
            max_slope = gradient_list.Min();
        }
    }
}
