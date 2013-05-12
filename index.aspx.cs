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

//jer ima promjena test

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       list_projects();
    }

    /*developer: Emilio
    description: metoda cita projekte iz baze i prikazuje na index.aspx*/
    private void list_projects()
    {
        List<Projects> projects_list = Projects.fetch_all();
        string html_new_projects = null;
        string html_old_projects = null;

        foreach (Projects project in projects_list)
        {
            if (DateTime.Now.AddDays(7) >= project.expiration_date)
            {
                html_old_projects += "<div class='proj'>"
                + "<h2>" + project.name + "</h2>"
                + "<img src=" + project.image_path + " alt=" + project.name + "> "
                + "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
                + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
                + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
                + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>"
                + "<input type='submit' name='Button4' value='DONIRAJ!' class='gumb' />"
                + "</div>";
            }
            else
            {
                html_new_projects += "<div class='proj'>"
            + "<h2>" + project.name + "</h2>"
            + "<img src=" + project.image_path + " alt=" + project.name + "> "
            + "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
            + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
            + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
            + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>"
            + "<input type='submit' name='Button4' value='DONIRAJ!' class='gumb' />"
            + "</div>";
            }
        }
        projekti_novi.InnerHtml = html_new_projects;
        projekti_stari.InnerHtml = html_old_projects;
    }
}
