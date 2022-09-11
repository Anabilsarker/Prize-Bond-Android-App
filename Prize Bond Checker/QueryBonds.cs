using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prize_Bond_Checker
{
    public class QueryBonds
    {
        private string ResponseValue { get; set; }
        public async Task<bool> PostRequest(string value)
        {
            try
            {
                var values = new Dictionary<string, string>
                {
                    { "gsearch", $"{value}" }
                };
                var content = new FormUrlEncodedContent(values);

                using (HttpClient client = new HttpClient())
                {
                    using (var response = await client.PostAsync("https://www.bb.org.bd/en/index.php/investfacility/prizebond", content))
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        ResponseValue = responseString;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); return false; }
        }
        public string BondResult()
        {
            if (ResponseValue.Contains("Congratulations! "))
            {
                string str1 = ResponseValue[ResponseValue.IndexOf("Congratulations! ") + 17].ToString();
                string str2 = ResponseValue[ResponseValue.IndexOf("Congratulations! ") + 18].ToString();
                string str3 = ResponseValue[ResponseValue.IndexOf("Congratulations! ") + 19].ToString();

                if (str2 == " ")
                {
                    return str1;
                }
                else if (str3 == " ")
                {
                    return str1 + str2;
                }
                else
                {
                    return str1 + str2 + str3;
                }
            }
            else
            {
                return "0";
            }
        }
    }
}