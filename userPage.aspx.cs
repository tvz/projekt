using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Query.Dynamic;

public partial class userPage : System.Web.UI.Page
{


    public static int i = -1;
    public static string dOGlobal = "";
    public static int IDGlobal = 0;

    private void Page_LoadComplete(object sender, System.EventArgs e)
    {



    }





    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {

        if (i == -1)
        {
            
            int position = ++i;

            string uName = (string)Session["username"];
            writeOut(uName, position);
        }

    }



    protected void writeOut(string uName, int positionSent)
    {
      

        int userId = Users.getUserId(uName);
        List<Projects> projects_list = Projects.fetch_all_projects(userId);

        int length = projects_list.Count();
        if ((positionSent < length) && (length > 0))
        {




            dOGlobal = projects_list[positionSent].description;
            LabelImeProjekta.Text = projects_list[positionSent].name;
            LabelOpisProjekta.Text = "opis projekta:" + projects_list[positionSent].description;
            IDGlobal = projects_list[positionSent].id;



        }
        if ((positionSent >= length) || (length < 1))
        {
            LabelImeProjekta.Text = "Nema projekata koje bi ste mogli mijenjati " + uName;
            LabelOpisProjekta.Visible = false;
            TextBoxPromjena.Visible = false;
            i = -1;
            LabelUnesiPromjenu.Visible = false;
            ButtonSubmit.Visible = false;
            ButtonNext.Visible = false;
        }



    }















    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {

        string description = TextBoxPromjena.Text;
        string descriptionOld = dOGlobal;
        int ID = IDGlobal;



        bool success = Projects.storeChange(description, descriptionOld, ID);
        if (success == true)
        {
            LabelOpisProjekta.Text = "Promjena je uspjesno pohranjena";
            LabelUnesiPromjenu.Visible = false;
            TextBoxPromjena.Text = "";
            TextBoxPromjena.Visible = false;
            ButtonSubmit.Visible = false;
        }







    }
    protected void ButtonNext_Click(object sender, EventArgs e)
    {


        TextBoxPromjena.Text = "";
        LabelUnesiPromjenu.Visible = true;

        TextBoxPromjena.Visible = true;
        ButtonSubmit.Visible = true;

        int position = ++i;
        string uName = (string)Session["username"];
        writeOut(uName, position);
    }
}




       

