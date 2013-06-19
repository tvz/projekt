using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class noviProjekt : System.Web.UI.Page
{
    private static string name, description, long_description, image_path, video;
    private static int goal;
    private static DateTime expiration;

    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (!User.Identity.IsAuthenticated)
        {
            Response.Redirect("index.aspx");
        }*/
        Session["list_projects"] = null;
        string script = "$(document).ready(function(){$('#" + TextBox_expiration_date.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowDatepicker", script, true);
        this.MultiView1.ActiveViewIndex = 0; 
    }

    /// <summary>
    /// Developer: Emilio
    /// Description: metoda sprema novi projekt u bazu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime expiration_date;
        DateTime.TryParseExact(TextBox_expiration_date.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out expiration_date);
        /*id korisnika je trenutno hardkodiran ali kad rijesimo userpage onda cu to promijeniti*/
        string video_path = TextBox_video_path.Text;
        video_path = video_path.Replace("watch?v=", "embed/");
        video_path = video_path.Replace("watch?feature=player_detailpage&v=", "embed/");
        video_path = video_path.Replace("watch?v=", "embed/");
        video_path = video_path.Replace("&", "?");

        name = TextBox_name.Text;
        description = TextBox_description.Text;
        goal = Convert.ToInt32(TextBox_goal.Text);
        expiration = expiration_date;
        image_path = TextBox_image_path.Text;
        video = video_path;

        this.MultiView1.ActiveViewIndex = 1;
    }

    // javite ako postoji problem oko unosa u bazu
    // inače, js datoteke za editor se mogu preuzeti na računalo, pa se mogu uključiti i dodaci
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Request["long_descriptionBox"] != null)
            long_description = System.Web.HttpUtility.HtmlEncode(Request["long_descriptionBox"].ToString());

        /*id korisnika je trenutno hardkodiran ali kad rijesimo userpage onda cu to promijeniti*/
        if (CheckBoxAccept.Checked)
        {
            bool created = Projects.Create(name, description, long_description, goal, expiration, image_path, video, 1);
            
            if (created)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Info", "alert('Uspješno ste kreirali novi projekt!\\nNakon pregleda sadržaja, Žicalica će odlućiti je li projekt u skladu s Uvjetima korištenja te će tada biti javno vidljiv.');", true);
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Info", "alert('Projekt nije spremljen! \nInterna pogreška, pokušajte ponovno kasnije!');", true);
        }
        else Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Info", "alert('Niste prihvatili uvjete korištenja');", true);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        this.MultiView1.ActiveViewIndex = 0;
    }
}
