using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosEfectuados
{
    internal class Objetos
    {
        public class Listbdd
        {
            public string bdd { get; set; }
            public string nombre { get; set; }
        }

        public class ListaPagos
        {
            public string NoPago { get; set; }
            public string ruta { get; set; }
            public string bdd { get; set; }

        }

        public class QueryPago
        {
            public int Folio { get; set; }
            public string Nombre { get; set; }
            public string email { get; set; }
            public Decimal Imp_Pago { get; set; }
            public string Factura { get; set; }
            public Decimal Imp_PagFact { get; set; }
            public string uuid { get; set; }
            public string empresa { get; set; }

        }
        public class Log
        {
            public DateTime Fecha { get; set; }
            public int Empleado { get; set; }
            public string Nombre { get; set; }
            public string Nomina { get; set; }
            public string Zona { get; set; }
            public string Respuesta { get; set; }
        }

    }
}
