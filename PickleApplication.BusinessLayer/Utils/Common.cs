using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Web;

namespace PickleApplication.BusinessLayer.Utils
{
    public static class Common
    {
        public static void CreateCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetCookie(string name)
        {
            if (HttpContext.Current.Response.Cookies.AllKeys.Contains(name))
            {
                return HttpContext.Current.Response.Cookies[name].Value;
            }
            return null;
        }

        public static void DeleteCookie(string name)
        {
            if (GetCookie(name) != null)
            {
                HttpContext.Current.Response.Cookies.Remove(name);
                HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddDays(-1);
            }
        }

        public static string FromObjectWithCamelCasePropertyNames(object obj) {
            if (obj == null) return null;
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public static string GetDomainName()
        {
           return string.Format("{0}://{1}/", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
        }
        
        public static void SendMail(string to, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "srvm02.turhost.com";
            client.Port = 465;

            //setup smtp authentication
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential("info@ulustursucum.com", "Ulus.1234");
            client.UseDefaultCredentials = false;
            client.Credentials = credential;

            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@ulustursucum.com");
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}
