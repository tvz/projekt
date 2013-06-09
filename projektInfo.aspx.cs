using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class projektInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UrlReferrer == null)
            Response.Redirect("~/index.aspx");

        List<Projects> projects_list = Projects.fetch_all();
        string urlParam = null, whatToShow = null;

        urlParam = Request.QueryString["name"];

        if (Session["whichToShow"] != null && urlParam == null)
        {
            whatToShow = Session["whichToShow"].ToString();
            showNewOrOldProjects(projects_list, whatToShow);
        }
        else
        {
            showNewOrOldProjects(projects_list, urlParam);
        }
    }

    protected void showNewOrOldProjects(List<Projects> projects_list, string showParam)
    {
        string html;

        if (showParam == "new")
        {
            foreach (Projects project in projects_list)
                if (DateTime.Now.AddDays(7) < project.expiration_date)
                {
                    if (project.video_path.Length > 0)
                    {
                        html = "<a href='projektInfo.aspx?name=" + project.name + "' title='Saznaj više' class='projectLink'><h2>" + project.name + "</h2></a>"
                            + "&nbsp<iframe width='300' height='180' src='" + project.video_path + "' frameborder='0' allowfullscreen></iframe>";
                    }
                    else
                    {
                        html = "<a href='projektInfo.aspx?name=" + project.name + "' title='Saznja više' class='projectLink'><h2>" + project.name + "</h2></a>"
                            + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
                    }
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
            foreach (Projects project in projects_list)
                if (DateTime.Now.AddDays(7) >= project.expiration_date)
                {
                    if (project.video_path.Length > 0)
                    {
                        html = "<a href='projektInfo.aspx?name=" + project.name + "' title='Saznaj više' class='projectLink'><h2>" + project.name + "</h2></a>"
                            + "&nbsp<iframe width='300' height='180' src='" + project.video_path + "' frameborder='0' allowfullscreen></iframe>";
                    }
                    else
                    {
                        html = "<a href='projektInfo.aspx?name=" + project.name + "' title='Saznja više' class='projectLink'><h2>" + project.name + "</h2></a>"
                            + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
                    }
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
            foreach (Projects project in projects_list)
                if (project.name == showParam)
                {
                    Button bttn = new Button();
                    bttn.Attributes.Add("class", "gumbDoniraj");
                    bttn.Text = "DONIRAJ";
                    bttn.ID = project.id.ToString();
                    bttn.Click += new EventHandler(MakeDonation);
                    if (project.video_path.Length > 0)
                    {
                        html = "<h2>" + project.name + "</h2>"
                            + "&nbsp<iframe width='300' height='180' src='" + project.video_path + "' frameborder='0' allowfullscreen></iframe>";
                    }
                    else
                    {
                        html = "<h2>" + project.name + "</h2>"
                            + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
                    }
                    html += "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
                    + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
                    + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
                    + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Attributes.Add("class", "projSingle");
                    div.InnerHtml = html;
                    div.Controls.Add(bttn);
                    projectContainer.Controls.Add(div);
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

}