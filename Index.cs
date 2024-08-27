using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PagosEfectuados
{
    partial class Index : ServiceBase
    {
        bool bProceso = false;
        bool bCambio = false;

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
            if (bCambio) return;
            { 
                Proceso.Interval = 120000;
                bCambio = true;
            }
            if (bProceso) return;
            try
            {
                bProceso = true;

                foreach (Objetos.Listbdd empresa in listbdd) { 
                    List<Objetos.ListaPagos> ListaPagos = new List<Objetos.ListaPagos>();
                    Objetos.ListaPagos Pago = new Objetos.ListaPagos();
                    LogClass.log("Base... " + empresa.bdd + "\r\n");

                    var dirs = Directory.EnumerateFiles(ruta + empresa.bdd +@"\", "*.*", SearchOption.AllDirectories)
                        .Where(s => s.EndsWith(".pdf"));

                    foreach (var di in dirs)
                    {
                        Pago.ruta = di;
                        Pago.NoPago = Path.GetFileNameWithoutExtension(di);
                        Pago.bdd = empresa.bdd;
                        ListaPagos.Add(Pago);
                        Pago = new Objetos.ListaPagos();
                    }

                    foreach (Objetos.ListaPagos PagoCursor in ListaPagos){

                        List<Objetos.QueryPago> DetallePago = new List<Objetos.QueryPago>();

                        DetallePago = SQL.ListaFactPagadas(PagoCursor.NoPago, PagoCursor.bdd);

                        Funciones.MailNotificacion(DetallePago, PagoCursor.ruta);

                        File.Move(PagoCursor.ruta, Path.GetDirectoryName(PagoCursor.ruta) + @"\Procesado\" + Path.GetFileName(PagoCursor.ruta));
                        try
                        {
                            LogClass.log("El archivo se movió exitosamente." + Path.GetDirectoryName(PagoCursor.ruta) + @"\Procesado\" + Path.GetFileName(PagoCursor.ruta));
                        }
                        catch (IOException ioEx)
                        {
                            LogClass.log("Error de E/S: " + ioEx.Message);
                        }
                        catch (UnauthorizedAccessException authEx)
                        {
                            LogClass.log("Error de acceso: " + authEx.Message);
                        }
                        catch (Exception ex)
                        {
                            LogClass.log("Error: " + ex.Message);
                        }

                    }



                }

            }
            catch (Exception ex)
            {
                LogClass.log(ex.Message + " - " + EventLogEntryType.Error + " "+ DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n");
            }
            bProceso = false;
        }
    }
}
