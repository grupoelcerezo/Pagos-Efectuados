using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace PagosEfectuados
{
    internal class SQL
    {
        public static List<Objetos.Listbdd> ListaBases()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "172.16.8.34",
                UserID = "sa",
                Password = "B1Cerezo",
                InitialCatalog = "a_CatalogoMaster",
                TrustServerCertificate = true
            };
            List<Objetos.Listbdd> Listabases = new List<Objetos.Listbdd>();
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "select dbName, cmpName from [SBO-COMMON].dbo.SRGC where dbname like 'SBO%' and dbname in ('SBO_SOG')--dbname not in ('SBO_FMF','SBO_GFM_2023')"; //  dbname = 'SBO_SOG'";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Objetos.Listbdd newItem = new Objetos.Listbdd();
                                newItem.bdd = reader.GetString(0);
                                newItem.nombre = reader.GetString(1);
                                Listabases.Add(newItem);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                LogClass.log("Consulta de bases: " + e.ErrorCode + " - " + e.Message + " - " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            }
            return Listabases;

        }

        public static List<Objetos.QueryPago> ListaFactPagadas(string Pago, string bdd)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "172.16.8.34",
                UserID = "sa",
                Password = "B1Cerezo",
                InitialCatalog = bdd,
                TrustServerCertificate = true
            };
            List<Objetos.QueryPago> ListaDetalle = new List<Objetos.QueryPago>();
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = @"		select 
			T1.DocNum, 
			T1.CardName,
			isnull(t5.E_Mail, 'abraham.madrigal@grupoelcerezo.com') email,
			cast(case when t1.DocCurr = 'MXP' then T1.DocTotal else  T1.DocTotalFC end as numeric(18,2)) Importe_Total,
			T4.NumAtCard 'Factura',
			cast(case when t1.DocCurr = 'MXP' then T2.SumApplied else  T2.AppliedFC end as numeric(18,2)) Importe_Factura,
			T4.U_UUID UUID, 
            (select CompnyName from OADM) Empresa

		from 
			dbo.OVPM T1 inner  JOIN -- ENCABEZADO PAGOS SALIENTES Facturas
			dbo.VPM2 T2 ON T1.DOCNUM = T2.DocNum  inner JOIN	 -- DETALLE PAGOS SALIENTES Facturas
			dbo.OPCH T4 ON T2.DocEntry = T4.DocEntry and T2.InvType = T4.ObjType INNER JOIN --Encabezado COMPRAS 
			a_CatalogoMaster.dbo.OCRD t5 on T1.CardCode = t5.CardCode
		where t1.docnum = " + Pago ; //  dbname = 'SBO_SOG'";

                    
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Objetos.QueryPago newItem = new Objetos.QueryPago();

                                newItem.Folio = reader.GetInt32(0);
                                newItem.Nombre = reader.GetString(1);
                                newItem.email = reader.GetString(2);
                                newItem.Imp_Pago = reader.GetDecimal(3);
                                newItem.Factura =  reader.GetString(4);
                                newItem.Imp_PagFact = reader.GetDecimal(5);
                                newItem.uuid = reader.GetString(6);
                                newItem.empresa = reader.GetString(7);
                                ListaDetalle.Add(newItem);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                LogClass.log("Consulta de Pagos: " + e.ErrorCode + " - " + e.Message + " - " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            }
            return ListaDetalle;

        }

    }
}
