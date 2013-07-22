using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class projektInfo : System.Web.UI.Page
{
    private string urlParam;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.UrlReferrer == null)
        //  Response.Redirect("~/index.aspx");

        List<Projects> projects_list = Projects.fetch_all();

        urlParam = Request.QueryString["name"];

        if (urlParam == null)
            Response.Redirect("~/index.aspx");

        showNewOrOldProjects(projects_list, urlParam);
    }

    protected void showNewOrOldProjects(List<Projects> projects_list, string showParam)
    {
        string html;

        if (showParam == "new")
        {
            this.MultiView1.ActiveViewIndex = 0;
            title.InnerText = "NOVI PROJEKTI";

            foreach (Projects project in projects_list)
                if (DateTime.Now.AddDays(7) < project.expiration_date)
                {
                    html = "<a href='projektInfo.aspx?name=" + project.name + "' title='Saznja više' class='projectLink'><h2>" + project.name + "</h2></a>"
                    + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
                    html += "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
                    + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
                    + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
                    + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Attributes.Add("class", "proj");
                    div.InnerHtml = html;
                    projectContainer.Controls.Add(div);
                }
        }
        else if (showParam == "old")
        {
            this.MultiView1.ActiveViewIndex = 0;
            title.InnerText = "PROJEKTI PRED ISTEKOM VREMENA ZA DONACIJU";

            foreach (Projects project in projects_list)
                if (DateTime.Now.AddDays(7) >= project.expiration_date)
                {
                    html = "<a href='projektInfo.aspx?name=" + project.name + "' title='Saznja više' class='projectLink'><h2>" + project.name + "</h2></a>"
                    + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
                    html += "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
                    + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
                    + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
                    + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Attributes.Add("class", "proj");
                    div.InnerHtml = html;
                    projectContainer.Controls.Add(div);
                }
        }
        else
        {
            this.MultiView1.ActiveViewIndex = 1;
            foreach (Projects project in projects_list)
                if (project.name == showParam)
                {
                    string desc = System.Web.HttpUtility.HtmlDecode(project.long_description);
                    string longDesc = desc.Replace("<hr />", "<hr style=\"background-color:#d02552;width:80%;margin-top:3%;margin-bottom:3%;\" />");

                    Button bttn = new Button();
                    bttn.Attributes.Add("class", "gumbDoniraj");
                    bttn.Text = "DONIRAJ";
                    bttn.ID = project.id.ToString();
                    bttn.Click += new EventHandler(MakeDonation);

                    header.InnerHtml = "<h1>" + project.name + "</h1><br/>";
                    long_description.InnerHtml = longDesc + "<br />";
                    if (project.video_path.Length > 0)
                    {
                        multimedia.InnerHtml = "<iframe class='textbox' width='500' height='380' src='" + project.video_path + "' frameborder='0' allowfullscreen></iframe>";
                    }
                    else
                    {
                        multimedia.InnerHtml = "<img  class='textbox' src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
                    }
                    info.InnerHtml = "<h3><b>AUTOR PROJEKTA</b><br />" + project.project_owner_username + "</h3>"
                    + "<h3><b>KRATKI OPIS PROJEKTA</b> <br />" + project.description + " </h3>"
                    + "<h3><b>SAKUPLJENO</b> <br />" + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
                    + "<h3><b>DO KRAJA</b> <br />" + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Attributes.Add("class", "projSingle");
                    //div.InnerHtml = html;
                    //div.Controls.Add(bttn);
                    //projectContainer.Controls.Add(div);
                }
        }
    }

    /// <summary>
    /// Developer: Emilio
    /// Description: metoda salje donaciju preko paypala
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MakeDonation(object sender, EventArgs e)
    {
        /*Uz svaki dinamicki kreirani button vezan je id projekta.
         * Bolje bi bilo da se salju request parametri sa id projekta, ali jednostavnije je bilo implementirati ovako.
         * Planiram to promijenit u buducnosti.*/
        Button button = (Button)sender;
        Session["project_id"] = button.ID;
        Response.Redirect("~/iznosDonacije.aspx");
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.TextBoxObjasnjenje.Visible = true;
        this.ButtonPrijavi.Visible = true;
        this.Label1.Visible = true;
    }

    protected void ButtonPrijavi_Click(object sender, EventArgs e)
    {
        
    }
}