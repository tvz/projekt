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

//jer ima promjena test

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ListProjects();
        //if(Request.Params.Keys.Get())
        //int v = Request.Params.Count;
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
            html = "<h2>" + project.name + "</h2>"
                + "<img src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> "
                + "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
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
    private void MakeDonation (object sender, EventArgs e)
    {
        /*Uz svaki dinamicki kreirani button vezan je id projekta.
         * Bolje bi bilo da se salju request parametri sa id projekta, ali jednostavnije je bilo implementirati ovako.
         * Planiram to promijenit u buducnosti.*/
        HtmlButton button = (HtmlButton)sender;
        Projects projekt = Projects.FetchProject(Convert.ToInt32(button.ID));
        Users user = Users.FetchUser(projekt.user_id);
        /*uuid je rand string koji saljemo paypalu kao parametar i koji nam on vraca u potvrdi transakcije. 
         * Time osiguravamo autenticnost potvrde transakcije.*/
        string uuid = FormsAuthentication.HashPasswordForStoringInConfigFile(DateTime.Now.Ticks.ToString(), "SHA1").ToLower(); //zamijenit sa generatorom random stringa

        string paypalParams = "cmd=_donations"
                             + "&business=tvz@tvz.tvz"//trenutno hardkodirana vrijednost
                             + "&lc=US"
                             + "&item_name=" + projekt.name
                             + "&item_number=" + projekt.id
                             + "&custom=" + uuid
                             + "&amount=10"//trenutno hardkodirana vrijednost
                             + "&currency_code=EUR"
                             + "&no_note=0"
                             + "&return="+ HttpContext.Current.Request.Url
                             + "&rm=2"; //trenutno koristim return page za dohvat svih vrijabli, poslje cu preko ipn-a

        Response.Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?"+paypalParams);
    }
}
