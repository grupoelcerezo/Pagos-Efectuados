using System;
using System.IO;


namespace PagosEfectuados
{
    internal class LogClass
    {
        public static void log(string log)
        {
            try
            {
                TextWriter stream = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Log.txt", true);
                stream.Write(log);

                stream.Flush();
                stream.Close();
            }
            catch (Exception ex)
            {
                LogClass.log("Ha ocurrido el siguente error al crear el log: " + ex.Message + " - " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            }

        }


    }
}
