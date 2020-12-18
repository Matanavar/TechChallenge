using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WooliesX.TechChallenge.Services
{
    public class HttpClientUtil
    {
        private static HttpClient httpClient = new HttpClient();

        public static async Task<T> Get<T>(string url)
        {
            try
            {              
                using (var httpResponse = await httpClient.GetAsync(url))
                {
                    httpResponse.EnsureSuccessStatusCode();
                    var json = httpResponse.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static async Task<T> Post<T>(string url, HttpContent content)
        {
            //var jsondata = JsonConvert.SerializeObject(data);
            //var content = new StringContent(jsondata, Encoding.UTF8, "application/json");

            try
            {
                using (var httpResponse = await httpClient.PostAsync(url, content))
                {
                    httpResponse.EnsureSuccessStatusCode();
                    var json =  httpResponse.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (HttpRequestException ex)
            {
                //Retry logic if needed. 
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
