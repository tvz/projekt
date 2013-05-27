using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class Administrator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList1_SelectedIndexChanged(new object(), new EventArgs());
            DropDownList2_SelectedIndexChanged(new object(), new EventArgs());
        }
    }
    /*Emilio
     metoda izvlaci projekte iz baze i puni gridview ovisno o izboru u dropdownu*/
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        SqlDataSource1.ProviderName = ConfigurationManager.ConnectionStrings["MyConnection"].ProviderName;

        switch (DropDownList1.SelectedValue)
        {
            case "Svi":
                SqlDataSource1.SelectCommand = "SELECT * FROM projects";
                break;
            case "Neodobren":
                SqlDataSource1.SelectCommand = "SELECT * FROM projects WHERE enabled = 'False'";
                break;
            case "Odobren":
                SqlDataSource1.SelectCommand = "SELECT * FROM projects WHERE enabled = 'True'";
                break;
        }
    }

    /*Emilio
     metoda vezana uz button update*/
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;

        SqlDataSource1.UpdateParameters.Clear();
        SqlDataSource1.UpdateCommand = "UPDATE projects SET [name]=@name, [description]=@description, [goal]=@goal ,expiration_date=@expiration_date, [updated_at]=NOW(), [image_path]=@image_path, [video_path]=@video_path, [enabled]=@enabled WHERE [ID] = @id";
        SqlDataSource1.UpdateParameters.Add("@name", e.NewValues["name"].ToString());
        SqlDataSource1.UpdateParameters.Add("@description", e.NewValues["description"].ToString());
        SqlDataSource1.UpdateParameters.Add("@goal", e.NewValues["goal"].ToString());
        SqlDataSource1.UpdateParameters.Add("@expiration_date", e.NewValues["expiration_date"].ToString());
        SqlDataSource1.UpdateParameters.Add("@image_path", e.NewValues["image_path"].ToString());
        SqlDataSource1.UpdateParameters.Add("@video_path", e.NewValues["video_path"].ToString());
        SqlDataSource1.UpdateParameters.Add("@enabled", e.NewValues["enabled"].ToString());
        SqlDataSource1.UpdateParameters.Add("@id", GridView1.DataKeys[e.RowIndex]["ID"].ToString());
        DropDownList1_SelectedIndexChanged(new object(), new EventArgs());
    }

    /*Emilio
     metoda vezana uz button edit*/
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        DropDownList1_SelectedIndexChanged(new object(), new EventArgs());
    }

    /*Emilio
     metoda vezana uz button delete*/
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        SqlDataSource1.DeleteParameters.Clear();
        SqlDataSource1.DeleteCommand = "DELETE FROM projects WHERE ID = @id";
        SqlDataSource1.DeleteParameters.Add("@id", GridView1.DataKeys[e.RowIndex]["ID"].ToString());
        DropDownList1_SelectedIndexChanged(new object(), new EventArgs());
    }
    /*Emilio
     metoda vezana uz button cancel*/
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DropDownList1_SelectedIndexChanged(new object(), new EventArgs());
    }

    /*Emilio
     metoda izvlaci korisnike iz baze i puni gridview ovisno o izboru u dropdownu*/
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        SqlDataSource2.ProviderName = ConfigurationManager.ConnectionStrings["MyConnection"].ProviderName;

        switch (DropDownList2.SelectedValue)
        {
            case "Svi":
                SqlDataSource2.SelectCommand = "SELECT * FROM users";
                break;
            case "Blokiran":
                SqlDataSource2.SelectCommand = "SELECT * FROM users WHERE enabled = 'False'";
                break;
            case "Omogucen":
                SqlDataSource2.SelectCommand = "SELECT * FROM users WHERE enabled = 'True'";
                break;
        }
    }

    /*Emilio
     metoda vezana uz button update*/
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;

        SqlDataSource2.UpdateParameters.Clear();
        SqlDataSource2.UpdateCommand = "UPDATE users SET [username]=@username,[email]=@email,[enabled]=@enabled, [updated_at]=NOW() WHERE [ID] = @id";
        SqlDataSource2.UpdateParameters.Add("@username", e.NewValues["username"].ToString());
        SqlDataSource2.UpdateParameters.Add("@email", e.NewValues["email"].ToString());
        SqlDataSource2.UpdateParameters.Add("@enabled", e.NewValues["enabled"].ToString());
        SqlDataSource2.UpdateParameters.Add("@id", GridView2.DataKeys[e.RowIndex]["ID"].ToString());
        DropDownList2_SelectedIndexChanged(new object(), new EventArgs());
    }
    /*Emilio
     metoda vezana uz button edit*/
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        DropDownList2_SelectedIndexChanged(new object(), new EventArgs());
    }

    /*Emilio
     metoda vezana uz button delete*/
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        SqlDataSource2.DeleteParameters.Clear();
        SqlDataSource2.DeleteCommand = "DELETE FROM users WHERE ID = @id";
        SqlDataSource2.DeleteParameters.Add("@id", GridView2.DataKeys[e.RowIndex]["ID"].ToString());
        DropDownList2_SelectedIndexChanged(new object(), new EventArgs());
    }

    /*Emilio
     metoda vezana uz button edit*/
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DropDownList2_SelectedIndexChanged(new object(), new EventArgs());
    }
}
