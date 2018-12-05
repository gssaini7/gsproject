using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using R.BusinessEntities;

namespace gsproject
{
    public static class StaticData
    {

       public static ImageFileStatus CheckFile(HttpPostedFileBase fup)
       {
           if (fup.ContentLength == 0)
           {
               return ImageFileStatus.NoFile;
           }
          
           if (System.IO.Path.GetExtension(fup.FileName) != ".jpg")
           {
               return ImageFileStatus.NotFormat;
           }
           int size_gp_pdf = fup.ContentLength;
           if (size_gp_pdf > 2100000 && size_gp_pdf < 0)
           {
               return ImageFileStatus.NotSize;
           }
           return ImageFileStatus.Success;

       }

        public enum ImageFileStatus
        {
            Success,
            NotSize,
            NotFormat,
            NoFile
        }

        public static void ResizeImage(string loc, int width, int height)
        {
            Image image = System.Drawing.Image.FromFile(loc);
            if (width == 0 && height == 0) {
                
                width = image.Width;
                height = image.Height;
                return;
            }

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            image.Dispose();
            Image im = (Image)destImage;

            //im.Save(loc, ImageFormat.Jpeg);
            im.Save(loc);

        }

        public static void ResizeImage(string srcloc, string destloc, int width, int height)
        {
            Image image = System.Drawing.Image.FromFile(srcloc);
            if (width == 0 && height == 0)
            {
                width = image.Width;
                height = image.Height;
            }

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            image.Dispose();
            Image im = (Image)destImage;

            //im.Save(destloc, ImageFormat.Jpeg);
            im.Save(destloc);


        }
     
        public static string RandomData()
       {
           string name = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();

           int len = 6;
           string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

           Random r = new Random();
           while ((len--) > 0)
           {
               sb.Append(str[(int)(r.NextDouble() * str.Length)]);
           }

           name = sb.ToString() + DateTime.Now.ToShortDateString().Replace("/", "").Replace("-", "");
           return name;
       }

      

        public static bool SendMail(string mailto, string mail_subject, string mail_body, string AdminText = "Web Admin")
        {

            bool success = false;
            try
            {
                string AdminUser = "";
                string AdminP = "";

                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();

                MailAddress fromAddress = new MailAddress(AdminUser, AdminText);
                message.From = fromAddress;
                message.To.Add(mailto);

                message.Subject = mail_subject;
                message.IsBodyHtml = true;
                message.Body = mail_body;
                smtpClient.Host = "smtp.gmail.com";
                //smtpClient.Host = "mail.usofts.net";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;

                smtpClient.Credentials = new System.Net.NetworkCredential(AdminUser, AdminP);

                smtpClient.Send(message);
                success = true;

            }
            catch(Exception ex) {
                success = false;
            }
            return success;
        }

        public static bool SendMail(string mailto, string mail_subject, string mail_body, MailModel mailmodel, string AdminText = "Web Admin")
        {

            bool success = false;
            try
            {
                string AdminUser = mailmodel.username;
                string AdminP = mailmodel.password;

                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();

                MailAddress fromAddress = new MailAddress(AdminUser, AdminText);
                message.From = fromAddress;
                message.To.Add(mailto);

                message.Subject = mail_subject;
                message.IsBodyHtml = true;
                message.Body = mail_body;
                smtpClient.Host = mailmodel.smtp;
                //smtpClient.Host = "mail.usofts.net";
                smtpClient.Port =Convert.ToInt32(mailmodel.port);
                smtpClient.EnableSsl = true;

                smtpClient.Credentials = new System.Net.NetworkCredential(AdminUser, AdminP);

                smtpClient.Send(message);
                success = true;

            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
        public static bool sendsmsany(string message, string mobileno)
        {
            bool result = false;
            string userName = "";
            string msgToken = "";
            string senderID = "";
            //string message = "One Time Password For Vehicle Bazar Registration is 123455. Please Input This to Complete the Registraton. ";
            string mobile = mobileno;
            
            try
            {
                WebRequest req = WebRequest.Create(api);
                WebResponse resp = req.GetResponse();
                Stream strm = resp.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(strm, encode);
                System.Diagnostics.Debug.Print(sr.ReadToEnd());
                sr.Close();
                strm.Close();
                resp.Close();
                result = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
            return result;
        }

        public static bool sendsmsany(string message, string mobileno, SMSModel smsmodel)
        {
            bool result = false;
            string userName = smsmodel.username;
            string msgToken = smsmodel.msgtoken;
            string senderID = smsmodel.senderid;
            string apiurl = smsmodel.apiurl;
            //string message = "One Time Password For Vehicle Bazar Registration is 123455. Please Input This to Complete the Registraton. ";
            string mobile = mobileno;
            string api = apiurl+"?username=" + userName + "&msg_token=" + msgToken + "&sender_id=" + senderID + "&message=" + message + "&mobile=" + mobile;
            try
            {
                WebRequest req = WebRequest.Create(api);
                WebResponse resp = req.GetResponse();
                Stream strm = resp.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(strm, encode);
                System.Diagnostics.Debug.Print(sr.ReadToEnd());
                sr.Close();
                strm.Close();
                resp.Close();
                result = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
            return result;
        }

        public static string Sanitizeurl(string keywords)
        {
            keywords = keywords.ToLower().Trim();
            Regex rgx = new Regex("[^a-zA-Z0-9-]");
            keywords = rgx.Replace(keywords, "-");
            keywords = Regex.Replace(keywords, @"(\W)+", "$1");
            var firstchar = keywords[0];
            if (firstchar == '-')
                keywords = keywords.Remove(0, 1);
            var lastchar = keywords[keywords.Length - 1];
            if (lastchar == '-')
                keywords = keywords.Remove(keywords.Length - 1);
            return keywords;
        }

        public static string SanitizeAlphanumeric(string keywords)
        {
            keywords = keywords.ToUpper().Trim();
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            keywords = rgx.Replace(keywords, "");
            keywords = Regex.Replace(keywords, @"(\W)+", "$1");
            return keywords;
        }

        public static string RandomNumber(int len=5)
        {
            string number = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
           
            string str = "1234567890";

            Random r = new Random();
            while ((len--) > 0)
            {
                sb.Append(str[(int)(r.NextDouble() * str.Length)]);
            }

            number = sb.ToString();
            return number;
        }
    }

}
