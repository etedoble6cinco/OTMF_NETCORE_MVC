using System.Net;
using System.Net.Mail;

namespace OTMF_NETCORE_MVC.Services
{
    public interface IServicioNotify
    {
         Task<bool> SendNotify(int ot, string maquina, string estado);
    }
    public class ServicioNotify : IServicioNotify   
    {
        private IWebHostEnvironment webHostEnvironment;
        public ServicioNotify(IWebHostEnvironment Environment)
        {
            webHostEnvironment = Environment;
        }
        public async Task<bool> SendNotify(int ot, string maquina, string estado)
        {
            if (await ValidateNotify(ot))
            {
                return true;
            }
            else
            {
                //string to = "oanaya@smimx.net";
                // string to2 = "aplumeda@smimx.net";
                string to3 = "analista.sistemas@smimx.net";
                string from = "analista.sistemas@smimx.net";
                MailMessage message = new MailMessage();
                message.From = new MailAddress(from, "Estado Orden Trabajo");
                message.Subject = "Orden Trabajo" + ot + "a sido detenida en" + maquina + "por el motivo:" + estado;
                message.To.Add(to3);
                // message.To.Add(to2);
                // message.To.Add(to3);


                message.Body = "Orden Trabajo" + ot + "a sido detenida en" + maquina + "por el motivo:" + estado;
                SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(from, "Suq70797");
                try
                {
                    client.Send(message);
                    return false;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("el mensaje no fue enviado" + ex.ToString());
                    return true;
                }
            }
        }
        public async Task<bool> ValidateNotify(int idOrdenTrabajo)
        {  //si el archivo no existe crearlo 
            string FileName = "HistoryNotify.txt";

            if (await ValidateIfExistsInTxt(idOrdenTrabajo, FileName))
            {
                return true;
            }
            else
            {
               
               
                File.AppendAllText(await GetNotifyFilePath(FileName),
                   idOrdenTrabajo.ToString() + Environment.NewLine);
            
           
            }
            
            return false;

        }
        public async Task<string> GetNotifyFilePath(string FileName)
        {
            return Path.Combine(webHostEnvironment.WebRootPath + "\\Uploads\\Notify\\", FileName);
        }
        public async Task<bool> ValidateIfExistsInTxt(int c, string FileName)
        {
            if (new FileInfo(await GetNotifyFilePath(FileName)).Length == 0)
            {
                return false;
            }
            else
            {
                var Lines = File.ReadAllLines( await GetNotifyFilePath(FileName));
                if (Lines.Contains(c.ToString()))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }

            return false;
        }

    
    }
}
