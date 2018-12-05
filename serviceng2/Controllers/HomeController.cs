using Newtonsoft.Json;
using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace USoftEducation.Controllers
{
    public class HomeController : Controller
    {
        iSettingsService _settingsobj;

        public HomeController(iSettingsService isettingsobj)
        {
            this._settingsobj = isettingsobj;
        }

        //public ActionResult Index(string id)
        //{
        //    string apiUrl = "http://localhost:58764/api/values";

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(apiUrl);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage response = await client.GetAsync(apiUrl);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var data = await response.Content.ReadAsStringAsync();
        //            var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

        //        }


        //    }

        //ViewBag.Title = "Home Page";
        //    return View();
        //}

        //public async Task<ActionResult> Index(string id)
        //{
        //    string apiUrl = "http://localhost:51131/api/page/GetPage?id="+id;

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(apiUrl);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage response = await client.GetAsync(apiUrl);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var data = await response.Content.ReadAsStringAsync();
        //            var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
        //        }
        //    }
        //    return View();
        //}

        public ActionResult Manager()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        //public ActionResult ResetPassword()
        //{
        //    return View();
        //}

        //public ActionResult Error()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Return(FormCollection form)
        {
            try
            {
                string transaction_id = string.Empty;
                string order_id = string.Empty;

                string[] keys = Request.Form.AllKeys;
                Array.Sort(keys);

                var paymentmodelmain = _settingsobj.GetSettingByType(SettingsType.OnlinePayment.ToString(), form["udf2"]);
                var paymentmodel = JsonConvert.DeserializeObject<OnlinePaymentModel>(paymentmodelmain.SettingsContent);

                string hash = gethash(keys, form, paymentmodel.salt);
                if (form["hash"] == hash)
                {
                    if (form["response_code"] == "0")
                    {
                        transaction_id = Request.Form["transaction_id"];
                        order_id = Request.Form["order_id"];

                        ViewData["Message"] = "Transaction is successful. Your tranaction ID is: " + transaction_id + " and order id is: " + order_id + ". Please save it for future reference.";
                        //HelpingMethods hm = new HelpingMethods();

                    }
                    else if (form["response_message"] == "Transaction Failed")
                    {
                        ViewData["Message"] = "Transaction is unsuccessful. Please try again.";
                    }
                    else
                    {
                        string response_message = Request.Form["response_message"];
                        int startIndex = response_message.IndexOf(" - ") + 2;
                        int length = response_message.Length - startIndex;
                        response_message = response_message.Substring(startIndex, length);
                        //Response.Write("Correct the below error <br />");
                        //Response.Write(response_message);
                        ViewData["Message"] = "To continue to transaction, please correct the error: " + response_message;
                    }

                }
                else
                {
                    ViewData["Message"] = "Transaction is unsuccessful. Please try again.";

                    //Response.Write("<br/>Hash value Not matched");

                }

            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
            }
            return View();
        }

        private string gethash(string[] hash_columns, FormCollection requests, string salt)
        {

            string checksumString;
            checksumString = salt;
            foreach (string column in hash_columns)
            {
                if (requests.Get(column) != null && column != "hash")
                {
                    if (!string.IsNullOrEmpty(requests[column]))
                    {
                        checksumString += "|" + requests[column];
                    }
                }

            }
            string result = Generatehash512(checksumString);
            return result;
        }


        public string Generatehash512(string text)
        {

            byte[] message = System.Text.Encoding.UTF8.GetBytes(text);

            System.Text.UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex.ToUpper();

        }
    }
}
