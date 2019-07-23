using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zwift_Reader_0._1._0
{
    //Helper functions to move/arrange data
    class Helpers
    {
        // generates the maximum values found in each data field
        public static void Create_Max_Values()
        {
            //creating max values
            Power.Find_Max_Power(Power.power_data);
            Cadence.Find_Max_Cadence(Cadence.cadence_data);
            Speed.Find_Max_Speed(Speed.speed_data);
            Altitude.Find_Max_Altitude(Altitude.alt_data);
            Distance.Find_Total_Ride_Distance(Distance.distance_data);
            Time.Find_Total_Ride_Time(Time.time_data);
        }

        public static void Generate_Averages()
        {
            //POWER
            Power.Find_Total_Average_Power(Power.power_data);
            Power.Find_Moving_Average_Watts(Power.active_power_data);

            //CADENCE
            Cadence.Find_Total_Average_Cadence(Cadence.cadence_data);
            Cadence.Find_Moving_Average_Cadence(Cadence.active_cadence_data);

            //SPEED
            Speed.Find_Total_Average_Speed(Speed.speed_data);
            Speed.Find_Moving_Average_Speed(Speed.active_speed_data);
        }


        public static void Generate_Climb_Metrics()
        {
            //climb and drop data
            Altitude.Find_Altitude_Climbed(Altitude.alt_data);
            Altitude.Find_Altitude_Dropped(Altitude.alt_data);
        }


        public static void Generate_Time_Metrics()
        {
            //generate time difference (in seconds)
            Time.Find_Total_Moving_Time(Speed.active_speed_data_count); // woah
            Time.Find_Total_Inactive_Time();

            //converting the times into readable strings
            Time.total_ride_time_string = Time.Convert_To_Clock_Time(Time.total_ride_time);
            Time.total_move_time_string = Time.Convert_To_Clock_Time(Time.total_move_time);
            Time.total_inactive_time_string = Time.Convert_To_Clock_Time(Time.total_inactive_time);
        }


        public static void Generate_Calorie_Metric()
        {
            Power.Calculate_Calories_Burned(Power.total_avg_watts, Time.total_ride_time);
        }


        public static void Generate_Distance_Metrics()
        {
            Distance.Convert_To_KMS(Distance.total_distance);
        }


        public static void Generate_Gradient_Limits()
        {
            Gradient.Find_Steepest_Gradient(Gradient.gradient_data);
            Gradient.Find_Steepest_Descent(Gradient.gradient_data);
        }


        public static void Get_Device_Name(int dev_code)
        {
            if (dev_code == 1)
            {
                Decoders.device_name_string = "Garmin";
            }
            else if (dev_code == 32)
            {
                Decoders.device_name_string = "Wahoo";
            }
            else if (dev_code == 260)
            {
                Decoders.device_name_string = "Zwift";
            }
            else
            {
                Decoders.device_name_string = "I haven't coded this yet";
            }
        }

        public static int Find_Max_Time(int[] size_array, int file_size)
        {
            int index = 0;

            for (int i = 0; i < size_array.Length; i++)
            {
                if (size_array[i] < file_size)
                {
                    index = i;
                }
            }

            return index;
        }

        public static void Clear_All_Values()
        {
            //clear all variables

            //empty all lists

        }

    }
}
