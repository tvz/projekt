using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Globalization;

public partial class pregledProjekata : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string scriptExpiration = "$(document).ready(function(){$('#" + TextBoxExpirationDate.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowExpirationDatepicker", scriptExpiration, true);
        string scriptCreatedAt = "$(document).ready(function(){$('#" + TextBoxCreatedAt.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowCreatedAtDatepicker", scriptCreatedAt, true);
    }

    /*developer: Ivan
     * description: metoda salje parametre za pretrazivanje searchProjects metodi
     * te nazad dobiva listu projekata koji zadovoljavaju trazene parametre
     */
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        DateTime createdAtDate;
        DateTime expirationDate;

        DateTime.TryParseExact(TextBoxCreatedAt.Text, "dd/MM/yy", null, DateTimeStyles.None, out createdAtDate);
        DateTime.TryParseExact(TextBoxExpirationDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out expirationDate);
        
        List<Projects> search_list = Projects.searchProjects(TextBoxName.Text, TextBoxGoal.Text, 
            createdAtDate, expirationDate);

        string html = null;

        projekti_search.InnerHtml="";


        foreach (Projects project in search_list)
        {
            HtmlButton button = new HtmlButton();
            button.Attributes.Add("class", "gumb");
            button.InnerText = "DONIRAJ";
            button.ID = project.id.ToString();
            button.ServerClick += new EventHandler(MakeDonation);
            html = "<h2>" + project.name + "</h2>"
                + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> "
                + "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
                + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
                + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
                + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("class", "proj");
            div.InnerHtml = html;
            div.Controls.Add(button);

            projekti_search.Controls.Add(div);
        }
    }

    /*developer: Emilio
     description: metoda salje donaciju preko paypala
     */
    private void MakeDonation(object sender, EventArgs e)
    {
        /*Uz svaki dinamicki kreirani button vezan je id projekta.
         * Bolje bi bilo da se salju request parametri sa id projekta, ali jednostavnije je bilo implementirati ovako.
         * Planiram to promijenit u buducnosti.*/
        HtmlButton button = (HtmlButton)sender;
        Projects projekt = Projects.FetchProject(Convert.ToInt32(button.ID));
        Users user = Users.FetchUser(projekt.user_id);
        string paypalParams = "cmd=_donations"
                             + "&business=tvz@tvz.tvz"//trenutno hardkodirana vrijednost
                             + "&lc=US"
                             + "&item_name=" + projekt.name
                             + "&amount=10"//trenutno hardkodirana vrijednost
                             + "&currency_code=EUR"
                             + "&no_note=0"
                             + "&return=" + HttpContext.Current.Request.Url;
        Response.Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?" + paypalParams);
    }

}
