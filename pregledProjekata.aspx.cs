using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

public partial class pregledProjekata : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string scriptExpiration = "$(document).ready(function(){$('#" + TextBoxExpirationDate.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowExpirationDatepicker", scriptExpiration, true);
        string scriptCreatedAt = "$(document).ready(function(){$('#" + TextBoxCreatedAt.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowCreatedAtDatepicker", scriptCreatedAt, true);
    }

    /*developer: Ivan
     * description: metoda pretrazuje te prikazuje projekte s trazenim atributima
     */
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        
    }

}
