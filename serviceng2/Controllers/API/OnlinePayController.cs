using gsproject;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace USoftEducation.Controllers
{
    [Authorize]

    [RoutePrefix("api/onlinepay")]
    public class OnlinePayController : BaseAPIController
    {
        iSettingsService _settingsobj;
        iStudentService _studentobj;


        public OnlinePayController(iSettingsService isettingsobj, iStudentService istudentobj)
        {
            this._settingsobj = isettingsobj;
            this._studentobj = istudentobj;
        }

        [Route("Pay")]
        [HttpPost]
        public async Task<IHttpActionResult> PayNow(OnlinePaymentAppDetailModel model) //student id
        {
            try
            {
                var dbcodeid = GetDataBaseCode();
                var studentid = Convert.ToInt32(model.StudentModelID);
                var studentdetail = _studentobj.GetById(studentid, dbcodeid);

                if (studentdetail == null) {
                    ModelState.AddModelError("", "An error occured please contact administrator.");
                    return BadRequest(ModelState);
                }


                var paymentmodelmain = _settingsobj.GetSettingByType(SettingsType.OnlinePayment.ToString(), dbcodeid);
                var paymentmodel = JSONGSTRING<OnlinePaymentModel>(paymentmodelmain.SettingsContent);
                if (paymentmodel == null)
                {
                    ModelState.AddModelError("", "An error occured please contact administrator.");
                    return BadRequest(ModelState);
                }
                var result=  OnlinePayment(paymentmodel, studentdetail, model, dbcodeid);
                return Ok(result);

                //if (webmanager != null)
                //{

                //    var deserializedProduct = JSONGS<IEnumerable<NotificationScheduleModel>>(webmanager);
                //    return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
                //}
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "An error occured please contact administrator.");
                return BadRequest(ModelState);
            }
           

        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("ReturnAddress")]
        //public async Task<IHttpActionResult> Return(Dictionary<string,string> form)
        //{
        //    try
        //    {



        //        //string transaction_id = string.Empty;
        //        //string order_id = string.Empty;

        //        //string[] keys = Request.Form.AllKeys;
        //        //Array.Sort(keys);


        //        //string hash = gethash(keys, form);
        //        //if (form["hash"] == hash)
        //        //{
        //        //    if (form["response_code"] == "0")
        //        //    {
        //        //        transaction_id = Request.Form["transaction_id"];
        //        //        order_id = Request.Form["order_id"];

        //        //        ViewData["Message"] = "Transaction is successful. Your tranaction ID is: " + transaction_id + " and order id is: " + order_id + ". Please save it for future reference.";
        //        //        HelpingMethods hm = new HelpingMethods();

        //        //        //var resulttovisitor = hm.SendMailSales(Request.Form["email"], "eBrickKiln- Payment Successful", mailcontent(transaction_id, order_id), "eBrickKiln- Sales");

        //        //        //  Response.Write("<br/>Transaction is successful. Hash value is matched");

        //        //    }
        //        //    else if (form["response_message"] == "Transaction Failed")
        //        //    {
        //        //        ViewData["Message"] = "Transaction is unsuccessful. Please try again.";
        //        //    }
        //        //    else
        //        //    {
        //        //        string response_message = Request.Form["response_message"];
        //        //        int startIndex = response_message.IndexOf(" - ") + 2;
        //        //        int length = response_message.Length - startIndex;
        //        //        response_message = response_message.Substring(startIndex, length);
        //        //        //Response.Write("Correct the below error <br />");
        //        //        //Response.Write(response_message);
        //        //        ViewData["Message"] = "To continue to transaction, please correct the error: " + response_message;
        //        //    }

        //        //}
        //        //else
        //        //{
        //        //    ViewData["Message"] = "Transaction is unsuccessful. Please try again.";

        //        //    //Response.Write("<br/>Hash value Not matched");

        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        //Response.Write("<span style='color:red'>" + ex.Message + "</span>");

        //    }
        //    return Ok();
        //}

        private string OnlinePayment(OnlinePaymentModel payment, StudentModel student, OnlinePaymentAppDetailModel appdetail,string dbcodeid)
        {
           
            string[] hash_columns = {
            "address_line_1",
            //"address_line_2",
            "amount",
            "api_key",
            "city",
            "country",
            "currency",
            "description",
            "email",
            "mode",
            "name",
            "order_id",
            "phone",
            "return_url",
            //"state",
            "udf1",
            "udf2",
            //"udf3",
            //"udf4",
            //"udf5",
            "zip_code"
            };

            var request = new Dictionary<string, string>();
            var amount = appdetail.feeamount;
            var mode = "TEST";
            var description = "Online payment of fees "+student.OfClass.ClassName+"-"+student.OfSection.SectionName+"(Roll No.: "+student.strRollNo+")";
            //var mode = "LIVE";
            var currenturl = ConfigurationManager.AppSettings["CurrentWebsite"];
            var returnurl= currenturl + "/home/Return";
            var rndmdata =student.StudentModelID+ DateTime.Now.ToShortDateString().Replace("/", "").Replace("-", "");
            request["api_key"] = payment.api_key;
            request["return_url"] = returnurl;
            request["currency"] = "INR";
            request["country"] = "IND";
            request["name"] = student.strStudentName;
            request["description"] = description;
            request["address_line_1"] = student.strAddress;
            //request["address_line_2"] = request["pcAddress"];
            request["phone"] = student.numMobileNoForSms;
            request["city"] ="test" ;
            request["zip_code"] ="111111" ;
            request["email"] ="test@test.com" ;
            request["amount"] = amount.ToString();
            request["order_id"] = rndmdata;
            request["mode"] = mode;
            request["udf1"] = appdetail.remarks;
            request["udf2"] = dbcodeid;


            RemotePost remotepost = new RemotePost();
            remotepost.Url = "https://biz.traknpay.in/v1/paymentrequest";
            remotepost.Add("api_key", request["api_key"]);
            remotepost.Add("return_url", request["return_url"]);
            remotepost.Add("mode", request["mode"]);
            remotepost.Add("order_id", request["order_id"]);
            remotepost.Add("amount", request["amount"]);
            remotepost.Add("name", request["name"]);
            remotepost.Add("currency", request["currency"]);
            remotepost.Add("description", request["description"]);
            remotepost.Add("address_line_1", request["address_line_1"]);
            //remotepost.Add("address_line_2", request["address_line_2"]);
            remotepost.Add("phone", request["phone"]);
            remotepost.Add("email", request["email"]);
            remotepost.Add("city", request["city"]);
            //remotepost.Add("state", request["state"]);
            remotepost.Add("country", request["country"]);
            remotepost.Add("zip_code", request["zip_code"]);
            remotepost.Add("udf1", request["udf1"]);
            remotepost.Add("udf2", request["udf2"]);
            //remotepost.Add("udf3", request["udf3"]);
            //remotepost.Add("udf4", request["udf4"]);
            //remotepost.Add("udf5", request["udf5"]);
            remotepost.Add("hash", gethash(hash_columns, request,payment.salt));
            //remotepost.Post();

            

            return remotepost.PostString();
        }

        private string gethash(string[] hash_columns,  Dictionary<string,string> requests, string salt)
        {

            string checksumString;
            checksumString = salt;
            foreach (string column in hash_columns)
            {
                if (requests[column] != null && column != "hash")
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

        public class RemotePost
        {
            private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();


            public string Url = "";
            public string Method = "post";
            public string FormName = "form1";

            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }

            public void Post()
            {
                System.Web.HttpContext.Current.Response.Clear();

                System.Web.HttpContext.Current.Response.Write("<html><head>");

                System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                }
                System.Web.HttpContext.Current.Response.Write("</form>");
                System.Web.HttpContext.Current.Response.Write("</body></html>");

                System.Web.HttpContext.Current.Response.End();
            }

            public string PostString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append("<html><head>");

                //sb.Append(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                sb.Append(string.Format("</head><body onload='document.{0}.submit()'>", FormName));


                //sb.Append(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
                sb.Append(string.Format("<form name='{0}' method='{1}' action='{2}' >", FormName, Method, Url));

                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    //sb.Append(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                    sb.Append(string.Format("<input name='{0}' type='hidden' value='{1}'>", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));

                }
                sb.Append("</form>");
                sb.Append("</body></html>");


                return sb.ToString();
            }
        }



    }



}
