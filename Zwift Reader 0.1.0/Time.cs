using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zwift_Reader_0._1._0
{
    class Time
    {
        //lists
        public static List<int> time_data = new List<int>();

        //variables
        public static int total_ride_time = 0;
        public static int total_move_time = 0;
        public static int total_inactive_time = 0;

        public static string total_ride_time_string;
        public static string total_move_time_string;
        public static string total_inactive_time_string;

        public static double inactive_time_percent = 0;
        public static double active_time_percent = 0;

        //datetimes
        public static System.DateTime OriginDateTime = new System.DateTime(1989, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        public static System.DateTime ActivityStartTime = new System.DateTime();
        public static System.DateTime ActivityEndTime = new System.DateTime();
        public static System.TimeSpan RunTime = new System.TimeSpan();



        public static void Find_Total_Ride_Time(List<int> time_data)
        {
            //finds the start and end times of the ride (in seconds)
            int start_time = time_data[0];
            int end_time = time_data[time_data.Count - 1];

            total_ride_time = end_time - start_time;
        }

        public static void Calculate_Active_Percents()
        {
            active_time_percent = (Convert.ToDouble(total_move_time) / Convert.ToDouble(total_ride_time)) * 100;
            inactive_time_percent = (Convert.ToDouble(total_inactive_time) / Convert.ToDouble(total_ride_time)) * 100;
        }

        //takes the data points that recorded some speed and calls that total move time
        public static void Find_Total_Moving_Time(int move_count)
        {
            total_move_time = move_count;
        }

        //takes the difference between total ride time and move time and calls that inactive time
        public static void Find_Total_Inactive_Time()
        {
            total_inactive_time = total_ride_time - total_move_time;
        }


        //slightly complicated way of converting seconds given to a time readable string (does not do days yet)
        public static string Convert_To_Clock_Time(int time)
        {
            int hours, minutes, seconds;
            string ride_time = "";

            if (time > 86400) // mulitple days
            {
                //may god have mercy on your soul
            }
            else if (time > 3600) // multiple hours
            {
                hours = Calc_Hours(time);
                minutes = Calc_Mins(time - (hours * 3600));
                seconds = time - (hours * 3600) - (minutes * 60);
                ride_time = (hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2"));
            }
            else if (time > 60) // multiple minutes
            {
                minutes = Calc_Mins(time);
                seconds = time - (minutes * 60);
                ride_time = ("00:" + minutes.ToString("D2") + ":" + seconds.ToString("D2"));
            }
            else if (time < 60) // mulitple seconds
            {
                ride_time = ("00:00:" + time.ToString("D2"));
            }

            return ride_time;
        }

        //helper functions
        private static int Calc_Hours(int seconds)
        {
            int hours = seconds / 3600;
            return hours;
        }

        private static int Calc_Mins(int seconds)
        {
            int mins = seconds / 60;
            return mins;
        }

        public static void Correct_Time_Zone()
        {
            OriginDateTime = OriginDateTime.ToLocalTime();
        }

        public static void Get_Activity_Start_Time()
        {
            ActivityStartTime = OriginDateTime.AddSeconds(time_data[0]);
        }

        public static void Get_Activity_End_Time()
        {
            ActivityEndTime = OriginDateTime.AddSeconds(time_data[Decoders.data_count - 1]);
        }
    }
}
