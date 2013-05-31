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

//jer ima promjena test2

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ListProjects();
        Session["list_projects"] = null;
        
    }

    /*developer: Emilio
    description: metoda cita projekte iz baze i prikazuje na index.aspx*/
    private void ListProjects()
    {
        List<Projects> projects_list = Projects.fetch_all();

        string html = null;

        projekti_novi.InnerHtml = "";
        projekti_stari.InnerHtml = "";


        foreach (Projects project in projects_list)
        {
            HtmlButton button = new HtmlButton();
            button.Attributes.Add("class", "gumb");
            button.InnerText = "DONIRAJ";
            button.ID = project.id.ToString();
            button.ServerClick += new EventHandler(MakeDonation);
            if (project.video_path.Length > 0)
            {
                html = "<h2>" + project.name + "</h2>"
                    + "&nbsp;&nbsp;&nbsp;<iframe width='320' height='180' src='"+project.video_path+"' frameborder='0' allowfullscreen></iframe>";
            }
            else 
            {
                html = "<h2>" + project.name + "</h2>"
                    + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
            }
            html+="<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
            + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
            + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
            + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("class", "proj");
            div.InnerHtml = html;
            div.Controls.Add(button);

            if (DateTime.Now.AddDays(7) >= project.expiration_date)
            {
                projekti_stari.Controls.Add(div);
            }
            else//tu ce jos ici provjera za stare projekte. jos nije odredeno kada postaju stari :-)
            {
                projekti_novi.Controls.Add(div);
            }
        }
    }
    /*developer: Emilio
     description: metoda salje donaciju preko paypala
     metoda ce se u buducnosti izmijeniti i bolje strukturirati, sad sluzi samo kao test.*/
    private void MakeDonation(object sender, EventArgs e)
    {
        /*Uz svaki dinamicki kreirani button vezan je id projekta.
         * Bolje bi bilo da se salju request parametri sa id projekta, ali jednostavnije je bilo implementirati ovako.
         * Planiram to promijenit u buducnosti.*/
        HtmlButton button = (HtmlButton)sender;
        Session["project_id"] = button.ID;
        Response.Redirect("iznosDonacije.aspx");

    }
    private void Postfill() { }
}
