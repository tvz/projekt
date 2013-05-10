using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    /*developer:Ivan
     description: metoda kreira korisnika i odmah ga preusmjerava na index.aspx
     notice: kod passworda trazi non-alphanumeric znak, trenutno nije rijesen problem;
     security question i answer su skriveni, a po defaultu su zadani (radi validatora)
     */

    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        Users.register(CreateUserWizard1.UserName, 
            CreateUserWizard1.Password, CreateUserWizard1.Email);
    }
}