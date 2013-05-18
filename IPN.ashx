<%@ WebHandler Language="C#" Class="IPN" %>

using System;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Threading;

public class IPN : IHttpHandler {
    
    /*developer: Emilio
     description: Metoda osluskuje paypal IPN. Ukoliko je korisnik potvrdio donaciju, transakcija se upisuje u bazu.*/
    public void ProcessRequest (HttpContext context) {
        /*context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");*/

        //Post back to either sandbox or live
        string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
        //string strLive = "https://www.paypal.com/cgi-bin/webscr";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strSandbox);
        //Set values for the request back
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        byte[] param = context.Request.BinaryRead(context.Request.ContentLength);
        string strRequest = Encoding.ASCII.GetString(param);

        string strResponse_copy = strRequest;  //Save a copy of the initial info sent by PayPal
        strRequest += "&cmd=_notify-validate";
        req.ContentLength = strRequest.Length;

        //for proxy
        //WebProxy proxy = new WebProxy(new Uri("http://url:port#"));
        //req.Proxy = proxy;
        //Send the request to PayPal and get the response
        StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
        streamOut.Write(strRequest);
        streamOut.Close();
        StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
        string strResponse = streamIn.ReadToEnd();
        streamIn.Close();

        if (strResponse == "VERIFIED")
        {
            NameValueCollection args = HttpUtility.ParseQueryString(strResponse_copy);
            string approved = args["approved"];
            string preapproval_key = args["preapproval_key"];
            if (approved.Equals("true"))
            {
                /*upisivanje u bazu se vrsi u zasebnom threadu. Postoji mogucnost da ipn handler propusti ipn poruku ukoliko se vise donacija 
                 odvija u isto vrijeme. Na ovaj nacin je povecana responzivnost handlera.*/
                Thread obj = new Thread(IPN.ProcessPostfill);
                obj.IsBackground = true;
                obj.Start(args);
            }
        }
        else if (strResponse == "INVALID")
        {
            //log for manual 

        }
        else
        {
            //log response/ipn data for manual investigation

        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    /*developer Emilio*/
    private static void ProcessPostfill(object obj)
    {
        NameValueCollection args = new NameValueCollection();
        Array.ForEach(obj.GetType().GetProperties(), pi => args.Add(pi.Name, pi.GetValue(obj,null).ToString()));
        Transactions.Postfill(args["preapproval_key"],
                              args["memo"],
                              args["approved"],
                              Convert.ToDateTime(args["starting_date"]),
                              Convert.ToDateTime(args["ending_date"]),
                              args["sender_email"]);
    }
}