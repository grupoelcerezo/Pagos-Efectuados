using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;

namespace PagosEfectuados
{
    partial class Index : ServiceBase
    {
        bool bProceso = false;

        List<Objetos.Listbdd> listbdd = new List<Objetos.Listbdd>();
        string ruta = @"\\scerezo25\Pagos\Comprobantes\";

        public Index()
        {
            LogClass.log("Arranco Servicio... " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n");
            listbdd = SQL.ListaBases();

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            Proceso.Start();

        }

        protected override void OnStop()
        {
            Proceso.Stop();
        }

        private void Proceso_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (bProceso) return;
            try
            {
                if (Proceso.Interval != 120000)
                {
                    Proceso.Interval = 120000;
                    LogClass.log("Cambio de tempororizador a 2 minutos... \r\n");
                }
                bProceso = true;
                foreach (Objetos.Listbdd empresa in listbdd)
                {
                    List<Objetos.ListaPagos> ListaPagos = new List<Objetos.ListaPagos>();
                    Objetos.ListaPagos Pago = new Objetos.ListaPagos();

                    var dirs = Directory.EnumerateFiles(ruta + empresa.bdd + @"\", "*.*", SearchOption.TopDirectoryOnly)
                        .Where(s => s.EndsWith(".pdf"));
                    if (dirs.Count() == 0)
                    {
                        LogClass.log("Base: " + empresa.bdd + " Archivos sin procesar... " + DateTime.Now.ToString("yyyy / MM / dd HH: mm:ss") + "\r\n");
                    }
                    else
                    {
                        LogClass.log("Base: " + empresa.bdd + " Archivos: " + dirs.Count().ToString() + "...\r\n");
                    }

                    foreach (var di in dirs)
                    {
                        Pago.ruta = di;
                        Pago.NoPago = Path.GetFileNameWithoutExtension(di);
                        Pago.bdd = empresa.bdd;
                        ListaPagos.Add(Pago);
                        Pago = new Objetos.ListaPagos();
                    }
                    foreach (Objetos.ListaPagos PagoCursor in ListaPagos)
                    {


                        List<Objetos.QueryPago> DetallePago = new List<Objetos.QueryPago>();

                        DetallePago = SQL.ListaFactPagadas(PagoCursor.NoPago, PagoCursor.bdd);

                        if (DetallePago.Count != 0)
                        {

                            LogClass.log("Base: " + empresa.bdd + " Pago : " + PagoCursor.NoPago + "...\r\n");

                            Funciones.MailNotificacion(DetallePago, PagoCursor.ruta);
                            string SPath = Path.GetDirectoryName(PagoCursor.ruta) + @"\Procesado\" + Path.GetFileName(PagoCursor.ruta);

                            if (File.Exists(SPath))
                            {
                                File.Replace(PagoCursor.ruta, SPath, null);
                            }
                            else
                            {

                                File.Move(PagoCursor.ruta, SPath);
                            }
                            try
                            {
                                LogClass.log("El archivo se movió exitosamente." + SPath + "...\r\n");
                            }
                            catch (IOException ioEx)
                            {
                                LogClass.log("Error de E/S: " + ioEx.Message + "...\r\n");
                            }
                            catch (UnauthorizedAccessException authEx)
                            {
                                LogClass.log("Error de acceso: " + authEx.Message + "...\r\n");
                            }
                            catch (Exception ex)
                            {
                                LogClass.log("Error: " + ex.Message + "...\r\n");
                            }
                        }
                        else
                        {
                            LogClass.log("Pago a cuenta no se envia: " + "...\r\n");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogClass.log(ex.Message + " - " + EventLogEntryType.Error + " " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n");
            }
            bProceso = false;
            LogClass.log("Termino Ciclo.... " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n");


        }
    }
}
