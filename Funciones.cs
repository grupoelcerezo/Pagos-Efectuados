using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;


namespace PagosEfectuados
{
    internal class Funciones
    {
        public static void MailNotificacion(List<Objetos.QueryPago> log, string path)
        {
            String Cadenamail = "";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("sbo_mailer@grupoelcerezo.com", "qjwujybftvwkmcou"),
                EnableSsl = true
            };

            for (int x1 = 0; x1 < log.Count; x1++)
            {

                Cadenamail += @" <tr style='height: 18px;'>
                <td style='width: 450px; height: 18px;'><span style='color: #fffff;'> <strong><strong>Factura:</strong></strong> " + log[x1].Factura.ToString() + @" </span> &nbsp;
                    <!--?=error.mensaje?-->
                </td>
            </tr>
            <tr style='height: 18px;'>
                <td style='width: 450px; height: 18px;'><span style='color: #fffff;'> <strong><strong>Importe Pagado:</strong></strong>" + log[x1].Imp_PagFact.ToString() + @"</span> &nbsp;
                    <!--?=error.mensaje?-->
                </td>
            </tr>
            <tr style='height: 18px;'>
                <td style='width: 450px; height: 18px;'><span style='color: #fffff;'> <strong><strong>UUID:</strong></strong>" + log[x1].uuid + @"</span> &nbsp;
                    <!--?=error.mensaje?-->
                </td>
            </tr>
";


            }
            string cuerpo = @"
<div style='background-color: #ececec; padding: 0; margin: 0 auto; font-weight: 200; width: 100%!important;'>
    <p>&nbsp;</p>
    <table style='width: 500px; margin-left: auto; margin-right: auto; background-color: #ffffff; height: 150px;'>
        <tbody>
            <tr style='height: 62px;'>
                <td style='width: 450px; background-color: #3b693c; text-align: center; height: 62px;'>
                    <h2><span style='color: #ffffff;'><strong>Proveedor: "  + log[0].Nombre + @" </strong></span></h2>
                </td>
            </tr>
            <tr style='height: 34px;'>
                <td style='width: 450px; background-color: #3b693c; text-align: center; height: 34px;'>
                    <h3><span style='color: #ffffff;'> %1% </span></h3>
                </td>
            </tr>
            <tr style='height: 34px;'>
                <td style='width: 450px; background-color: #cccccc; text-align: center; height: 34px;'>
                    <h3><span style='color: #000000;'> Detalle </span></h3>
                </td>
            </tr>
            %2%
        </tbody>
    </table>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
</div> ";
    
            string Mail = cuerpo.Replace("%1%", " Pago: " + log[0].Folio.ToString()+ " | Importe: $" + log[0].Imp_Pago.ToString() + " | Fecha: " + DateTime.Now.ToString("yyyy/MM/dd"));
            Mail = Mail.Replace("%2%", Cadenamail);
            MailMessage message = new MailMessage();
            message.From = new MailAddress("sbo_mailer@grupoelcerezo.com");
            Attachment attachment = new Attachment(path, MediaTypeNames.Application.Pdf);
            message.Attachments.Add(attachment);
            message.To.Add("abraham.madrigal@grupoelcerezo.com");
            message.To.Add(log[0].email);
            message.Subject = "Pago " + log[0].empresa +" Fecha: " + DateTime.Now.ToString("yyyy/MM/dd");
            message.Body = Mail;
            message.IsBodyHtml = true;
            smtpClient.Send(message);

            attachment.Dispose();
            message.Dispose();

        }

         
    }
}
