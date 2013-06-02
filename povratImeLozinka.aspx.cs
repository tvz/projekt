using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Timers;

public partial class povratImeLozinka : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
    }

    protected void LinkButtonPassword_Click(object sender, EventArgs e)
    {
        this.MultiViewRetrieve.ActiveViewIndex = 0;
    }

    protected void LinkButtonUsername_Click(object sender, EventArgs e)
    {
        this.MultiViewRetrieve.ActiveViewIndex = 1;
    }

    /// <summary>
    /// Developer: Ivan
    /// Description: metoda prima te šalje argumente i javlja korisniku status
    /// u vezi s procesom vracanja lozinke
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSubmitPassword_Click(object sender, EventArgs e)
    {
        string email, question;
        email = this.TextBoxEmailPassword.Text;
        question = this.TextBoxQuestion.Text;

        if (TextBoxQuestion.Text.Length == 0)
        {
            question = Users.checkAndGeneratePassword(null, email, null);
            if (question != null)
            {
                this.TextBoxQuestion.Text = question;
                this.TextBoxQuestion.Visible = true;
                this.LabelQuestion.Visible = true;
                this.LabelAnswer.Visible = true;
                this.TextBoxAnswer.Visible = true;
                this.TextBoxEmailPassword.Enabled = false;
            }
            else this.LabelStatus.Text = "Korisnik s upisanom e-mail adresom ne postoji.";
        }
        else if (TextBoxQuestion.Text.Length > 0)
        {
            string answer = Users.checkAndGeneratePassword(null, email, this.TextBoxAnswer.Text);
            if (answer != null)
            {
                Panel1.Controls.Clear();
                this.LabelStatus.Text = "Provjerite e-mail pretinac.";
            }
            else
                this.LabelStatus.Text = "Unesena kombinacija sigurnosnog pitanja i odgovora ne postoji.";
        }

    }

    /// <summary>
    /// Developer: Ivan
    /// Description: Funkcija provjerava postojanje korisnika i s obzirom na postojanje radi slijedece.
    /// U slucaju da korisnik postoji salje korisniku email sa korisnickim podacima.
    /// Ako ne postoji, obavjestava kako navedeni korisnik ne postoji.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSubmitUsername_Click(object sender, EventArgs e)
    {
        bool status = Users.checkAndGiveUsername(this.TextBoxEmail.Text);
        if (status == true)
        {
            Panel1.Controls.Clear();
            this.LabelStatus.Text = "Provjerite e-mail pretinac.";
            // TODO: Kad se implementira email server, omogućiti slanje maila korisniku
        }
        else
            this.LabelStatus.Text = "Korisnik s upisanom e-mail adresom ne postoji.";
    }

}