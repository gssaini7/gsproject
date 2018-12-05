using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace USoftEducation.Controllers
{
    public class MainController : Controller
    {
        //iPageService _mainobj;

        //public MainController(iPageService imainobj)
        //{
        //    this._mainobj = imainobj;
        //}

       // public ActionResult Index(string id)
       //{
       //     //PageController pg = new PageController();
       //     //var page = pg.GetByID(id);

       //     //var page = _mainobj.GetByURL(id);
       //     //ViewBag.Title = "Main Page";

       //     return View();
       // }

        public async Task<ActionResult> Index(string id)
        {
            //string url = Request.Url.AbsoluteUri;
            //// http://localhost:1302/TESTERS/Default6.aspx

            //string path = Request.RawUrl;
            //// /TESTERS/Default6.aspx

            //string host = Request.Url.OriginalString;
            //string host2 = Request.Url.ToString();
            //if (id == null)
            //    id = "home";
            var host = Request.Url.Authority;
            string apiUrl = "http://"+ host +"/api/page/GetPageDetail?url=" + id;

            //// localhost
            ////string apiUrl = "http://localhost:51131/api/page/GetPageDetail?url=" + id;

            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(apiUrl);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage response = await client.GetAsync(apiUrl);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var data = await response.Content.ReadAsStringAsync();
            //        //var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
            //        var pagedetail = Newtonsoft.Json.JsonConvert.DeserializeObject<PageModelToDisplay>(data);

            //        var pagecontentobj = Newtonsoft.Json.JsonConvert.DeserializeObject<PageContentMainObjToDisplay>(pagedetail.pagecontent);
            //        StringBuilder sb = new StringBuilder();
            //        foreach (var content in pagecontentobj.pcary) {
            //            sb.Append(content.PContent);
            //        }

            //        pagedetail.pagecontent = sb.ToString();

            //        return View(pagedetail);
            //    }
            //}
            return View("Error");
        }

        //public ActionResult TopMenu(string id)//layout id
        //{
        //    return PartialView("_TopMenu");
        //}

        public ActionResult Manager()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }
    }
}
