using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynastream.Fit;

namespace Zwift_Reader_0._1._0
{
    class Decoders
    {
        public static int data_count = 0;
        public static int device_name_int = 0;
        public static string device_name_string = "Zwift";


        //called once per data message recieved by the main reader
        public static void OnRecordMessage(object sender, MesgEventArgs e)
        {
            //init the message to be given to the decoder
            var recordMessage = (RecordMesg)e.mesg;

            //call the decoder on the message - we're looking for power, cadence, speed, alt, distance, time
            int power_value = ReturnFieldValue(recordMessage, RecordMesg.FieldDefNum.Power);
            int cadence_value = ReturnFieldValue(recordMessage, RecordMesg.FieldDefNum.Cadence);
            double speed_value = ReturnFieldValueDouble(recordMessage, RecordMesg.FieldDefNum.Speed) * 3.6; //converting m/s to km/h because m/s is useless info
            double alt_value = ReturnFieldValueDouble(recordMessage, RecordMesg.FieldDefNum.Altitude);
            double distance_value = ReturnFieldValueDouble(recordMessage, RecordMesg.FieldDefNum.Distance);
            int time_value = ReturnFieldValue(recordMessage, RecordMesg.FieldDefNum.Timestamp);


            
            //add the given data values to their corresponding lists
            Power.power_data.Add(power_value);
            Cadence.cadence_data.Add(cadence_value);
            Speed.speed_data.Add(speed_value);
            Altitude.alt_data.Add(alt_value);
            Distance.distance_data.Add(distance_value);
            Time.time_data.Add(time_value);

            /*
            // obsolete
            //contruct a new data point given the data pulled from the decoder above
            Data_Point new_point = new Data_Point(power_value, cadence_value, speed_value, alt_value, distance_value, time_value);

            //add that point to the list of all data points
            Data_Point.data_point_list.Add(new_point);
            */

            //add to the counter (so we know how many points of data have been collected)
            data_count++;

            
            //now we need to collect data through various filters (i.e. deadzones, descents etc)
            //power
            if (power_value > 0)
            {
                Power.active_power_data_count++;
                Power.active_power_data.Add(power_value);
            }

            //cadence
            if (cadence_value > 0)
            {
                Cadence.active_cadence_data_count++;
                Cadence.active_cadence_data.Add(cadence_value);
            }

            //speed
            if (speed_value > 0)
            {
                Speed.active_speed_data_count++;
                Speed.active_speed_data.Add(speed_value);
            }
            
        }

        //takes general data messages (once)
        public static void OnFileIDMesg(object sender, MesgEventArgs e)
        {
            FileIdMesg myFileId = (FileIdMesg)e.mesg;

            device_name_int = Convert.ToInt32(myFileId.GetManufacturer());
        }



        // INTEGER VERSION
        //decoder function called once per data point to create the data lists
        private static int ReturnFieldValue(Mesg message, byte fitfield)
        {
            //create a profile field
            Field profileField = Profile.GetField(message.Num, fitfield);

            IEnumerable<FieldBase> fields = message.GetOverrideField(fitfield);

            //init a tracker
            int newint = 0;

            //loop over fields collecting relevant data
            foreach (FieldBase field in fields)
            {
                newint = Convert.ToInt32(field.GetValue());
            }

            //return data requested in the call
            return newint;
        }



        // DOUBLE VERSION
        //decoder function called once per data point to create the data lists
        private static double ReturnFieldValueDouble(Mesg message, byte fitfield)
        {
            //create a profile field
            Field profileField = Profile.GetField(message.Num, fitfield);

            IEnumerable<FieldBase> fields = message.GetOverrideField(fitfield);

            //init a tracker
            double newdouble = 0;

            //loop over fields collecting relevant data
            foreach (FieldBase field in fields)
            {
                newdouble = Convert.ToDouble(field.GetValue());
            }

            //return data requested in the call
            return newdouble;
        }

    }
}
