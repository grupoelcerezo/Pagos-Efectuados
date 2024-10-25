using System.ServiceProcess;

namespace PagosEfectuados
{
    internal static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Index()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
