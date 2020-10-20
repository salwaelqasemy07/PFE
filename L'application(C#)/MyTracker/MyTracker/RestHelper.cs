using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyTracker
{
    class RestHelper
    {
        /******************************code pour envoyer les donnees au API*********************************************/
        private static readonly string baseURL = "http://localhost:8080/gps/api/";
        
        public static void create(Adresse adr)
        {
            HttpClient client = new HttpClient();
            var ad1 = JsonConvert.SerializeObject(adr);
            var buffer = Encoding.UTF8.GetBytes(ad1);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.PostAsync(baseURL+"create.php", byteContent);
            
        }
    }
}
