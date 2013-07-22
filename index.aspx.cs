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
    private static int startNew = 0, endNew = 3, startOld = 0, endOld = 3, lengthNew, lengthOld;
    private static bool oldScroll = true, newScroll = true;

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
            Response.Redirect("~/projektInfo.aspx?name=new");
        }
        else
        {
            Session["whichToShow"] = "old";
            Response.Redirect("~/projektInfo.aspx?name=old");
        }
    }

    /// <summary>
    /// Developer: Ivan Perički
    /// Description: metoda gleda koji je button pritisnut te salje zahtjev za listanje
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Developer: Emilio
    /// Description: Metoda cita projekte iz baze i prikazuje na index.aspx
    /// Notice:dogovor jer da se ne prcka po tudjim metodama, no buduci da ti delas zavrsni,
    /// netko je moral listanje napravit (Ivan)
    /// </summary>
    /// <param name="startNew">Indeks pocetnog novog projekta</param>
    /// <param name="endNew">Indeks zavrsnog novog projekta</param>
    /// <param name="startOld">Indeks pocetnog starog projekta</param>
    /// <param name="endOld">Indeks zavrsnog starog projekta</param>
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

        lengthNew = newProjects.Count;
        lengthOld = oldProjects.Count;

        showNewer.Text += " (" + lengthNew.ToString() + ")";
        showOlder.Text += " (" + lengthOld.ToString() + ")";

        //ako se u listi nalazi do 3 projekta
        if (newProjects.Count <= 3)
        {
            scrollNewRight.Visible = false;
            scrollNewLeft.Visible = false;
            newScroll = false;
            newProjectsTemp = newProjects;
        }
        else
        {
            scrollNewRight.Visible = true;
            newScroll = true;
            newProjectsTemp = newProjects.GetRange(startNew, endNew);
        }

        if (oldProjects.Count <= 3)
        {
            scrollOldRight.Visible = false;
            scrollOldLeft.Visible = false;
            oldScroll = false;
            oldProjectsTemp = oldProjects;
        }
        else
        {
            scrollOldRight.Visible = true;
            oldScroll = true;
            oldProjectsTemp = oldProjects.GetRange(startOld, endOld);
        }

        //novi projekti
        foreach (Projects project in newProjectsTemp)
        {
            html = "<a href='projektInfo.aspx?name=" + project.name + "' title='Saznaj više' class='projectLink'><h2>" + project.name + "</h2></a>"
                    + "<img  src=" + "'" + project.image_path + "'" + " alt='Slika trenutno nije dostupna' >";
            html += "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
            + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
            + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
            + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

            HtmlGenericControl div = new HtmlGenericControl("div");
            if (newScroll == false)
                div.Attributes.Add("class", "projNoScroll");
            else div.Attributes.Add("class", "proj");
            div.InnerHtml = html;
            projekti_novi.Controls.Add(div);
        }

        //stari projekti
        foreach (Projects project in oldProjectsTemp)
        {
            html = "<a href='projektInfo.aspx?name=" + project.name + "' title='Saznaj više' class='projectLink'><h2>" + project.name + "</h2></a>"
                    + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
            html += "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
            + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
            + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
            + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

            HtmlGenericControl div = new HtmlGenericControl("div");
            if (oldScroll == false)
                div.Attributes.Add("class", "projNoScroll");
            else div.Attributes.Add("class", "proj");
            div.InnerHtml = html;
            projekti_stari.Controls.Add(div);
        }

    }

    private void Postfill() { }
}
