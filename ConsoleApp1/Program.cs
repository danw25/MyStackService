using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            bla();
        }

        static async void bla()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44305/");


                var res =  client.GetAsync("api/LicenseChecker/CheckOption?optionName=aaa").GetAwaiter().GetResult();
                res.EnsureSuccessStatusCode();
                var str = await res.Content.ReadAsStringAsync();
                
                var json = JsonConvert.DeserializeObject<string>(str);
                //LicenseStatus.TryParse(json, out LicenseStatus newNum);



                //str = str.Replace("\"", "").Replace("\\", "");   VERSION




            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        //private LicenseStatus stringToLicenseStatus(string json)
        //{

        //}

        private Version stringToVersion(string str)
        {
            var res= str.Replace("\"", "").Replace("\\", "");
            return new Version(res);
        }
        private (bool, bool) StringToBoolTuple(string str)
        {
            var res = str.Split(':');
            bool a = bool.Parse(res[0]);
            bool b = bool.Parse(res[1]);

            return (a, b);
        }
    }
    public enum LicenseStatus
    {
        Permanent,
        Disabled,
        Temporary,
        Expired,
        Evaluation,
        Missing,
        Mismatch,
        System_Limited,
    }
}
