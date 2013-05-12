using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class noviProjekt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (!User.Identity.IsAuthenticated)
        {
            Response.Redirect("index.aspx");
        }*/
        string script = "$(document).ready(function(){$('#" + TextBox_expiration_date.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowDatepicker", script, true);
     }

    /*developer: Emilio
     description: metoda sprema novi projekt*/
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime expiration_date;
        DateTime.TryParseExact(TextBox_expiration_date.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out expiration_date);
        /*id korisnika je trenutno hardkodiran ali kad rijesimo userpage onda cu to promijeniti*/
        bool created = Projects.Create(TextBox_name.Text,
                         TextBox_description.Text,
                         Convert.ToSingle(TextBox_goal.Text),
                         expiration_date,
                         TextBox_image_path.Text,
                         TextBox_video_path.Text, 1);
        if(created)
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Info", "alert('Uspješno ste kreirali novi projekt!');", true);
        else
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Info", "alert('Projekt nije spremljen! \nInterna pogreška, pokušajte ponovno kasnije!');", true);
    }
}
