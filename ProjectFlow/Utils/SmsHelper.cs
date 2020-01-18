using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ProjectFlow.Utils
{
    public class SmsHelper
    {

        private string HOST = "sms.sit.nyp.edu.sg";
        private string POST = "/SMSWebService/sms.asmx/sendMessage";
        private string SMSAccount = "EA01";
        private string PWD = "111827";

        public void SendSMS(string mobile, string message)
        {

            if (message.Length > 0 && message.Length <= 70)
            {

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://sms.sit.nyp.edu.sg/SMSWebService/sms.asmx/sendMessage");
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";
                webRequest.ContentLength = 70;

                webRequest.Headers.Add("SMSAccount", SMSAccount);
                webRequest.Headers.Add("Pwd", PWD);
                webRequest.Headers.Add("Mobile", mobile);
                webRequest.Headers.Add("Message", message);
    
                using (StreamWriter requestStream = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestStream.WriteLine("\nSending SMS...\n");
                }
    
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                using (StreamReader responseStream = new StreamReader(webResponse.GetResponseStream()))
                {
                    Console.WriteLine($"\n{responseStream.ReadToEnd()}\n");
                }

            }


        }
    }
}