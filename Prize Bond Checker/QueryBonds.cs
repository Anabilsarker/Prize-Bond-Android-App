using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Android.App;
using Prize_Bond_Checker.Database;

namespace Prize_Bond_Checker
{
    public class QueryBonds
    {
        private string ResponseValue { get; set; }
        private string RequestValue { get; set; }
        public async Task<bool> PostRequest(string value)
        {
            try
            {
                RequestValue = value;
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
        public async Task<bool> ParseInput(string value)
        {
            bool isRange;
            var parts = value.Split(',');
            foreach(var part in parts)
            {
                var ranges = part.Split('~');
                if (ranges.Length == 1) isRange = false;
                else if (ranges.Length == 2) isRange = true;
                else { return false; }
                //foreach(var range in ranges)
                {
                    if(ranges[0].Length == 7)
                    {
                        int bondNum = int.Parse(ranges[0]);
                        if (isRange)
                        {
                            while (bondNum <= int.Parse(ranges[1]))
                            {
                                if (!await SQLiteDatabase.BondExists(bondNum))
                                {
                                    await SQLiteDatabase.AddBonds(bondNum);
                                    bondNum++;
                                }
                            }
                        }
                        else
                        {
                            if (!await SQLiteDatabase.BondExists(bondNum))
                            {
                                await SQLiteDatabase.AddBonds(bondNum);
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}