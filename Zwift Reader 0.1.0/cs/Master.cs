using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Dynastream.Fit;

namespace Zwift_Reader_0._1._0
{
    class Master
    {
        //init - DynaStream(c)
        readonly static Dictionary<ushort, int> mesgCounts = new Dictionary<ushort, int>();
        static FileStream fitSource;


        public static void Main(string fit_file)
        {
            // marks the beginning time of processing
            System.DateTime starttime = System.DateTime.Now;

            //Open, Read, Decode and Close the .fit File
            Decode_Fit_File(fit_file);

            //FIND MAX VALUES:
            Helpers.Create_Max_Values();

            //CREATE AVERAGES:
            Helpers.Generate_Averages();

            //CREATE MISC DATA POINTS:
            Time.Correct_Time_Zone();
            Helpers.Get_Device_Name(Decoders.device_name_int);
            Time.Get_Activity_Start_Time();
            Time.Get_Activity_End_Time();
            Helpers.Generate_Climb_Metrics();
            Helpers.Generate_Time_Metrics();
            Helpers.Generate_Calorie_Metric();
            Helpers.Generate_Distance_Metrics();
            Gradient.Generate_Gradient_Step_List();
            Helpers.Generate_Gradient_Limits();
            Gradient.Find_Avg_Gradient();
            Time.Calculate_Active_Percents();

            //SET VALUES:
            Power.Set_CP_Values();
            Power.Calc_FTP(Power.twenty_minute_max_pwr);


            //end of processing time stamp
            System.DateTime endtime = System.DateTime.Now;
            System.TimeSpan run_time = endtime - starttime;
            Time.RunTime = run_time;
        }


        private static void Decode_Fit_File(string file)
        {
            ///// Attempt to open .FIT file
            fitSource = new FileStream(file, FileMode.Open);
            //Console.WriteLine("Opening {0}", file);

            //construction
            Decode decoder = new Decode();
            MesgBroadcaster mesgBroadcaster = new MesgBroadcaster();

            // Connect the Broadcaster to the event source
            decoder.MesgEvent += mesgBroadcaster.OnMesg;
            decoder.MesgDefinitionEvent += mesgBroadcaster.OnMesgDefinition;

            //take the raw data
            mesgBroadcaster.FileIdMesgEvent += Decoders.OnFileIDMesg;
            mesgBroadcaster.RecordMesgEvent += Decoders.OnRecordMessage;

            //check and init for processing
            bool status = decoder.IsFIT(fitSource);
            status &= decoder.CheckIntegrity(fitSource);

            //the decode (here all data is created)
            if (status)
            {
                //Console.WriteLine("Decoding...");
                decoder.Read(fitSource);
                //Console.WriteLine("Successfully decoded file: {0}", file);
            }

            //close off the decoding process
            fitSource.Close();
        }
    }
}
