using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SampleCCaptchaMVC.Controllers
{
    public class SampleCCaptchaController : Controller
    {
        public class Info
        {
            public string Token { get; set; }
            public string Secret_Code { get; set; }
        }

        // GET: SampleCCaptcha
        public ActionResult Index()
        {
            Info info = new Info();
            info.Secret_Code = "YOUR SECRET CODE";
            if(Request.Form["ccaptcha_token_input"] != null)
            info.Token = (Request.Form["ccaptcha_token_input"]).ToString();

            // connect  to  webservice
            WebClient objclient = new WebClient();
            objclient.Headers[HttpRequestHeader.ContentType] = "application/json ; charset=utf-8";
            objclient.Encoding = UTF8Encoding.UTF8;

            //for use JsonConvert Serializer please install Newtonsoft package
            string json = JsonConvert.SerializeObject(info);
            string Response = objclient.UploadString("https://api.ccaptcha.com/api/Validate/ValidationPost", json);
            
            if (Response == "\"true\"")
            {
                ViewBag.Response = "true"; // Correctly solved
            }
            else
            {
                ViewBag.Response = "false";
            }

            return View();
        }
    }
}