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
    protected static List<Projects> search_list = new List<Projects>();
    protected List<Projects> sorted_list = new List<Projects>();

    protected void Page_Load(object sender, EventArgs e)
    {
        string scriptExpirationStart = "$(document).ready(function(){$('#" + TextBoxExpirationDateStart.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowExpirationDateStartpicker", scriptExpirationStart, true);
        string scriptCreatedAtStart = "$(document).ready(function(){$('#" + TextBoxCreatedAtStart.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowCreatedAtStartpicker", scriptCreatedAtStart, true);
        string scriptCreatedAtEnd = "$(document).ready(function(){$('#" + TextBoxCreatedAtEnd.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowCreatedAtEndpicker", scriptCreatedAtEnd, true);
        string scriptExpirationEnd = "$(document).ready(function(){$('#" + TextBoxExpirationDateEnd.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowExpirationDateEndpicker", scriptExpirationEnd, true);
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        this.DropDownListSort.SelectedIndex = 0;
        this.RadioButtonDESC.Checked = true;
    }

    protected void DropDownListSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        search_list = (List<Projects>)Session["list_projects"];
        sorted_list = Sort(search_list);
        showProjects(sorted_list);
    }

    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        this.TextBoxProjectName.Text = "";
        this.TextBoxGoalStart.Text = "";
        this.TextBoxGoalEnd.Text = "";
        this.TextBoxCreatedAtStart.Text = "";
        this.TextBoxCreatedAtEnd.Text = "";
        this.TextBoxExpirationDateStart.Text = "";
        this.TextBoxExpirationDateEnd.Text = "";
        this.LabelSearchResult.Text = "";
    }

    /*developer: Ivan
     * description: metoda salje parametre za pretrazivanje searchProjects metodi
     * u klasi Projects te nazad dobiva listu projekata
     */
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        DateTime createdAtDateStart, expirationDateStart, createdAtDateEnd, expirationDateEnd;
        DateTime.TryParseExact(TextBoxExpirationDateStart.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out expirationDateStart);
        DateTime.TryParseExact(TextBoxCreatedAtStart.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out createdAtDateStart);
        DateTime.TryParseExact(TextBoxCreatedAtEnd.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out createdAtDateEnd);
        DateTime.TryParseExact(TextBoxExpirationDateEnd.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out expirationDateEnd);
        string createdAtStart, expirationStart, createdAtEnd, expirationEnd;
        createdAtStart = createdAtDateStart.ToShortDateString();
        createdAtEnd = createdAtDateEnd.ToShortDateString();
        expirationStart = expirationDateStart.ToShortDateString();
        expirationEnd = expirationDateEnd.ToShortDateString();

        search_list = Projects.searchProjects(TextBoxProjectName.Text, TextBoxGoalStart.Text, TextBoxGoalEnd.Text,
            createdAtStart, createdAtEnd, expirationStart, expirationEnd);

        showProjects(search_list);

        if (search_list.Count > 0)
        {
            DropDownListSort.Enabled = true;
            RadioButtonASC.Enabled = true;
            RadioButtonDESC.Enabled = true;
        }

        Session["list_projects"] = search_list;
    }
    
    /*developer: Ivan
     * description: metoda sortira listu projekata
     */
    private List<Projects> Sort(List<Projects> list)
    {
        if (RadioButtonASC.Checked == true)
        {
            if (DropDownListSort.SelectedIndex == 0)
                sorted_list = search_list.OrderBy(o => o.name).ToList();
            else if (DropDownListSort.SelectedIndex == 1)
                sorted_list = search_list.OrderBy(o => o.goal).ToList();
            else if (DropDownListSort.SelectedIndex == 2)
                sorted_list = search_list.OrderBy(o => o.created_at).ToList();
            else if (DropDownListSort.SelectedIndex == 3)
                sorted_list = search_list.OrderBy(o => o.expiration_date).ToList();
        }
        else if (RadioButtonDESC.Checked == true)
        {
            if (DropDownListSort.SelectedIndex == 0)
                sorted_list = search_list.OrderByDescending(o => o.name).ToList();
            else if (DropDownListSort.SelectedIndex == 1)
                sorted_list = search_list.OrderByDescending(o => o.goal).ToList();
            else if (DropDownListSort.SelectedIndex == 2)
                sorted_list = search_list.OrderByDescending(o => o.created_at).ToList();
            else if (DropDownListSort.SelectedIndex == 3)
                sorted_list = search_list.OrderByDescending(o => o.expiration_date).ToList();
        }
        return sorted_list;
    }

    /*developer: Ivan
     * description: metoda prikazuje projekte iz liste
     */
    private void showProjects(List<Projects> list)
    {
        string html = null;
        projekti_search.InnerHtml = "";
        this.LabelSearchResult.Text = "Pronađeno: <b>"+list.Count+"</b>";
        
        foreach (Projects project in list)
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
                + "<h3><b>POTREBNO:</b> " + project.goal + " Kunića" + " </h3>"
                + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
                + "<h3><b>ZAPOČETO:</b> " +project.created_at.ToShortDateString()+"</h3>"
                + "<h3 style=\"margin-bottom:-50px;\"><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + " dana" + "</h3>";
          
            HtmlGenericControl div = new HtmlGenericControl("div");           
            div.Attributes.Add("style", "background-color: rgba(255,255,255,0.45);width: 345px;border-radius: 10px;float:left;height: 550px;margin-right: 25px;margin-bottom:4%;");
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
