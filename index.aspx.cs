﻿using System;
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
    private static int startNew = 0, endNew = 3, startOld = 0, endOld = 3, lengthNew, lengthOld;

    protected void Page_Load(object sender, EventArgs e)
    {
        scroll(null, null);
        Session["list_projects"] = null;
    }

    /*developer:Ivan
     *description: metoda preusmjerava na stranicu za ispis svih projekata u rubrici 
     */
    protected void showAll(object sender, EventArgs e)
    {
        LinkButton bttn = (LinkButton)sender;
        if (bttn.ID == "showNewer")
        {
            Session["whichToShow"] = "new";
            Response.Redirect("~/projektInfo.aspx");
        }
        else
        {
            Session["whichToShow"] = "old";
            Response.Redirect("~/projektInfo.aspx");
        }
    }

    /*developer:Ivan
     * description: metoda gleda koji je button pritisnut te salje zahtjev za listanje
     */
    protected void scroll(object sender, EventArgs e)
    {
        ImageButton bttn = (ImageButton)sender;

        if (sender == null)
        {
            ListProjects(startNew, endNew, startOld, endOld);
        }
        else
        {
            if (bttn.ID == "scrollNewRight")
            {

                scrollNewLeft.Enabled = true;
                if (lengthNew > (startNew + 3))
                {
                    startNew += 3;
                    if (lengthNew > (startNew + endNew))
                        endNew = 3;
                    else endNew = lengthNew - startNew;
                }
                ListProjects(startNew, endNew, startOld, endOld);
                if ((lengthNew - startNew - endNew) == 0)
                    scrollNewRight.Enabled = false;
            }
            else if (bttn.ID == "scrollNewLeft")
            {

                scrollNewRight.Enabled = true;
                endNew = 3;
                if (0 <= (startNew - 3))
                    startNew -= 3;
                ListProjects(startNew, endNew, startOld, endOld);
                if (startNew == 0)
                    scrollNewLeft.Enabled = false;
            }
            else if (bttn.ID == "scrollOldLeft")
            {

                scrollOldRight.Enabled = true;
                endOld = 3;
                if (0 <= (startOld - 3))
                    startOld -= 3;
                ListProjects(startNew, endNew, startOld, endOld);
                if (startOld == 0)
                    scrollOldLeft.Enabled = false;
            }
            else if (bttn.ID == "scrollOldRight")
            {

                scrollOldLeft.Enabled = true;
                if (lengthOld > (startOld + 3))
                {
                    startOld += 3;
                    if (lengthOld > (startOld + endOld))
                        endOld = 3;
                    else endOld = lengthOld - startOld;
                }
                ListProjects(startNew, endNew, startOld, endOld);
                if ((lengthOld - startOld - endOld) == 0)
                    scrollOldRight.Enabled = false;
            }
        }
    }

    /*developer: Emilio
    description: metoda cita projekte iz baze i prikazuje na index.aspx*/
    //notice:dogovor jer da se ne prcka po tudjim metodama, no buduci da ti delas zavrsni,
    //netko je moral listanje napravit :)
    private void ListProjects(int startNew, int endNew, int startOld, int endOld)
    {
        List<Projects> projects_list = Projects.fetch_all();
        List<Projects> newProjects = new List<Projects>();
        List<Projects> oldProjects = new List<Projects>();

        //privremene liste koje koristimo zbog listanja
        List<Projects> newProjectsTemp;
        List<Projects> oldProjectsTemp;

        string html = null;
        projekti_novi.InnerHtml = "";
        projekti_stari.InnerHtml = "";

        //podjela na stare i nove projekte
        foreach (Projects project in projects_list)
        {
            if (DateTime.Now.AddDays(7) >= project.expiration_date)
                oldProjects.Add(project);
            else newProjects.Add(project);
        }

        //System.Diagnostics.Debug.WriteLine("new: " + newProjects.Count + " old: " + oldProjects.Count+ " start:"+start+" end:"+end);
        lengthNew = newProjects.Count;
        lengthOld = oldProjects.Count;

        naslovNovi.InnerHtml = "Najnoviji projekti (" + newProjects.Count + ")";
        naslovStari.InnerHtml = "Projekti pred istekom vremena za donaciju (" + oldProjects.Count + ")";

        //ako se u listi nalazi do 3 projekta
        if (newProjects.Count <= 3)
        {
            scrollNewRight.Visible = false;
            scrollNewLeft.Visible = false;
            newProjectsTemp = newProjects;
        }
        else
        {
            scrollNewRight.Visible = true;
            newProjectsTemp = newProjects.GetRange(startNew, endNew);
        }

        if (oldProjects.Count <= 3)
        {
            scrollOldRight.Visible = false;
            scrollOldLeft.Visible = false;
            oldProjectsTemp = oldProjects;
        }
        else
        {
            scrollOldRight.Visible = true;
            oldProjectsTemp = oldProjects.GetRange(startOld, endOld);
        }

        //novi projekti
        foreach (Projects project in newProjectsTemp)
        {
            if (project.video_path.Length > 0)
            {
                html = "<a href='projektInfo.aspx?id=" + project.id + "' title='Saznaj više' class='projectLink'><h2>" + project.name + "</h2></a>"
                    + "&nbsp<iframe width='320' height='180' src='" + project.video_path + "' frameborder='0' allowfullscreen></iframe>";
            }
            else
            {
                html = "<a href='projektInfo.aspx?id=" + project.id + "' title='Saznja više' class='projectLink'><h2>" + project.name + "</h2></a>"
                    + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
            }
            html += "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
            + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
            + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
            + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("class", "proj");
            div.InnerHtml = html;
            projekti_novi.Controls.Add(div);
        }

        //stari projekti
        foreach (Projects project in oldProjectsTemp)
        {
            if (project.video_path.Length > 0)
            {
                html = "<a href='projektInfo.aspx?id=" + project.id + "' title='Saznaj više' class='projectLink'><h2>" + project.name + "</h2></a>"
                    + "&nbsp<iframe width='320' height='180' src='" + project.video_path + "' frameborder='0' allowfullscreen></iframe>";
            }
            else
            {
                html = "<a href='projektInfo.aspx?id='" + project.id + "' title='Saznaj više' class='projectLink'><h2>" + project.name + "</h2></a>"
                    + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
            }
            html += "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
            + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
            + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
            + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("class", "proj");
            div.InnerHtml = html;
            projekti_stari.Controls.Add(div);
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
