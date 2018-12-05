using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace crudgenerator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //string dirpath = Directory.GetCurrentDirectory();
            //var a = HostingEnvironment.ApplicationPhysicalPath;
            //XmlDocument doc = new XmlDocument();
            //IDictionary<int, string> dict = new Dictionary<int, string>();
            GenerateXML();
            
            return View();
        }

        public void GenerateXML()
        {
            var mainelement = "BlogTitle";
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("BlogTitleName", "string");
            dict.Add("isPublished", "bool");

            var path = Server.MapPath("~") + "t4Templates\\test.xml";
            XmlWriter xmlWriter = XmlWriter.Create(path);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");
            xmlWriter.WriteAttributeString("Value", mainelement);

            foreach (var item in dict)
            {
                xmlWriter.WriteStartElement("Element");
                xmlWriter.WriteAttributeString("Name", item.Key);
                xmlWriter.WriteAttributeString("Type", item.Value);
                xmlWriter.WriteEndElement();
            }

            //xmlWriter.WriteStartElement("Element");
            //xmlWriter.WriteAttributeString("Name", "BlogTitleName");
            //xmlWriter.WriteAttributeString("Type", "string");

            ////xmlWriter.WriteString("John Doe");
            //xmlWriter.WriteEndElement();

            //xmlWriter.WriteStartElement("Element");
            //xmlWriter.WriteAttributeString("Name", "isPublished");
            //xmlWriter.WriteAttributeString("Type", "bool");

            //xmlWriter.WriteString("Jane Doe");

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}