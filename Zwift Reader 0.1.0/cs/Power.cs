using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zwift_Reader_0._1._0
{
    class Power
    {
        //lists
        public static List<int> power_data = new List<int>();
        public static List<int> active_power_data = new List<int>();

        //counts
        public static int active_power_data_count = 0;
        public static double total_avg_watts = 0;

        //variables
        public static double calories_burned = 0;
        public static int max_power = 0;
        public static int total_cumulative_watts = 0;
        public static int moving_cumulative_watts = 0;
        public static double moving_avg_watts = 0;
        public static double ftp = 0;

        //CP data
        public static double one_second_max_pwr = 0; //1
        public static double two_second_max_pwr = 0; //2
        public static double five_second_max_pwr = 0; //5
        public static double ten_second_max_pwr = 0; //10
        public static double twenty_second_max_pwr = 0; //20
        public static double thirty_second_max_pwr = 0; //30

        public static double one_minute_max_pwr = 0; //60
        public static double two_minute_max_pwr = 0; //120
        public static double five_minute_max_pwr = 0; //300
        public static double ten_minute_max_pwr = 0; //600
        public static double twenty_minute_max_pwr = 0; //1200
        public static double thirty_minute_max_pwr = 0; //1800
        public static double fourty_minute_max_pwr = 0; //2400
        public static double sixty_minute_max_pwr = 0; //3600
        public static double ninety_minute_max_pwr = 0; //5400

        public static double two_hour_max_pwr = 0; //7200
        public static double three_hour_max_pwr = 0; //10800
        public static double four_hour_max_pwr = 0; //14400
        public static double five_hour_max_pwr = 0; //18000



        public static void Find_Max_Power(List<int> power_list)
        {
            //watts
            max_power = power_list.Max();
        }


        //including deadzones
        public static void Find_Total_Average_Power(List<int> power_list)
        {
            //first we find (and save) the cumulative power total
            for (int i = 0; i < power_list.Count - 1; i++)
            {
                total_cumulative_watts = total_cumulative_watts + power_list[i];
            }

            //then divide by the total number of data points
            total_avg_watts = (double)total_cumulative_watts / Decoders.data_count;
        }

        public static void Calc_FTP(double twenty_min_pwr)
        {
            ftp = twenty_min_pwr * 0.95;
        }



        //not including deadzones
        public static void Find_Moving_Average_Watts(List<int> power_list)
        {
            //first we find (and save) the cumulative power of the active list
            for (int i = 0; i < power_list.Count - 1; i++)
            {
                moving_cumulative_watts = moving_cumulative_watts + power_list[i];
            }

            //then divide by the total number of data points
            moving_avg_watts = (double)moving_cumulative_watts / active_power_data_count;
        }



        // a miniumum measurement of calories burned based on a low efficiency human measurement (0.25)
        public static void Calculate_Calories_Burned(double power, int seconds)
        {
            calories_burned = ((((power * seconds) / 4.18) / 0.25) / 1000);
        }


        //simple algorithm for finding max efforts for a given time
        public static double Find_Power_Max(List<int> power_data, int time)
        {
            //establish a list to add to when an avg is found
            List<double> avg_power = new List<double>();

            //compare the power taken at the given minute intervals
            for (int i = 0; i < power_data.Count - time; i++)
            {
                double cumulative_power = 0;

                //finds the cumulative power of each 'chunk' of time (i.e. if time = 60 then it will find 60 data points)
                for (int n = i; n < i + time; n++)
                {
                    cumulative_power = cumulative_power + power_data[n];
                }

                //add this average power to the list (by dividing the cumulative found above by the number of points asked for)
                avg_power.Add(cumulative_power / time);
            }

            //return the highest found average power for that time interval
            return avg_power.Max();
        }



        //master function called to find all CP values (if available)
        public static void Set_CP_Values()
        {
            int size_of_file = Decoders.data_count;

            //create a list to hold the CP values temporarily
            double[] cp_values = new double[19];

            //array of time options in seconds (1 second to 5 hours)
            int[] size_options = new int[]
                {1, 2, 5, 10, 20, 30, 60, 120, 300, 600, 1200, 1800, 2400, 3600, 5400, 7200, 10800, 14400, 18000};

            //returns the index position of the biggest possible time frame that can be used
            int index = Helpers.Find_Max_Time(size_options, size_of_file);

            //set as many corresponding CP values as possible
            for (int i = 0; i <= index; i++)
            {
                cp_values[i] = (Find_Power_Max(power_data, size_options[i]));
            }

            //now write the cp_values list to the global variables above
            Write_CP_Values(cp_values);
        }



        //writes the data to globals if it exists - currently leaves non existing data as 0
        private static void Write_CP_Values(double[] cp_vals)
        {
            //one second
            if (cp_vals[0] != 0)
            {
                one_second_max_pwr = cp_vals[0];
            }
            //two seconds
            if (cp_vals[1] != 0)
            {
                two_second_max_pwr = cp_vals[1];
            }
            //five seconds
            if (cp_vals[2] != 0)
            {
                five_second_max_pwr = cp_vals[2];
            }
            //ten seconds
            if (cp_vals[3] != 0)
            {
                ten_second_max_pwr = cp_vals[3];
            }
            //twenty seconds
            if (cp_vals[4] != 0)
            {
                twenty_second_max_pwr = cp_vals[4];
            }
            //thirty seconds
            if (cp_vals[5] != 0)
            {
                thirty_second_max_pwr = cp_vals[5];
            }
            //one minute
            if (cp_vals[6] != 0)
            {
                one_minute_max_pwr = cp_vals[6];
            }
            //two minutes
            if (cp_vals[7] != 0)
            {
                two_minute_max_pwr = cp_vals[7];
            }
            //five minutes
            if (cp_vals[8] != 0)
            {
                five_minute_max_pwr = cp_vals[8];
            }
            //ten minutes
            if (cp_vals[9] != 0)
            {
                ten_minute_max_pwr = cp_vals[9];
            }
            //twenty minutes
            if (cp_vals[10] != 0)
            {
                twenty_minute_max_pwr = cp_vals[10];
            }
            //thirty minutes
            if (cp_vals[11] != 0)
            {
                thirty_minute_max_pwr = cp_vals[11];
            }
            //fourty minutes
            if (cp_vals[12] != 0)
            {
                fourty_minute_max_pwr = cp_vals[12];
            }
            //sixty minutes
            if (cp_vals[13] != 0)
            {
                sixty_minute_max_pwr = cp_vals[13];
            }
            //ninety minutes
            if (cp_vals[14] != 0)
            {
                ninety_minute_max_pwr = cp_vals[14];
            }
            //two hours
            if (cp_vals[15] != 0)
            {
                two_hour_max_pwr = cp_vals[15];
            }
            //three hours
            if (cp_vals[16] != 0)
            {
                three_hour_max_pwr = cp_vals[16];
            }
            //four hours
            if (cp_vals[17] != 0)
            {
                four_hour_max_pwr = cp_vals[17];
            }
            //five hours
            if (cp_vals[18] != 0)
            {
                five_hour_max_pwr = cp_vals[18];
            }
            //could go longer but i couldn't be bothered - can be added at a later date
        }


    }
}
