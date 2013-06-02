using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.AdaptivePayments;
using PayPal.AdaptivePayments.Model;

public partial class iznosDonacije : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Projects project = Projects.FetchProject(Convert.ToInt32(Session["project_id"]));
        h1_ime_projekta.InnerText = project.name;
    }
  
    /// <summary>
    /// Developer: Emilio
    /// Description: Metoda dohvaca preapproval key za donaciju i preusmjerava donatora na paypal website da potvrdi donaciju
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Projects project = Projects.FetchProject(Convert.ToInt32(Session["project_id"]));
        PreapprovalRequest req = new PreapprovalRequest();
        RequestEnvelope requestEnvelope = new RequestEnvelope("en_US");
        req.requestEnvelope = requestEnvelope;
        req.cancelUrl = "http://zicalica.tvz.hr/index.aspx";
        req.currencyCode = "EUR";
        req.startingDate = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
        req.returnUrl = "http://zicalica.tvz.hr/zahvala.aspx";
        req.endingDate = project.expiration_date.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
        req.maxNumberOfPayments = 1;
        req.maxAmountPerPayment = Convert.ToInt32(Request.Params["iznos"]);
        req.maxTotalAmountOfAllPayments = Convert.ToInt32(Request.Params["iznos"]);
        req.displayMaxTotalAmount = true;
        req.ipnNotificationUrl = "http://zicalica.tvz.hr/IPN.ashx";


        AdaptivePaymentsService service = new AdaptivePaymentsService();
        PreapprovalResponse res = service.Preapproval(req);
        if (res.error.Count == 0)
        {
            Transactions.Prefill(Convert.ToInt32(Session["project_id"]), Convert.ToSingle(req.maxTotalAmountOfAllPayments), res.preapprovalKey);
            Response.Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_ap-preapproval&preapprovalkey=" + res.preapprovalKey);
        }
    }
}
