using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /*developer:Emilio
     description: metoda logira korisnika i preusmjerava ga na index page(zasada)
     */
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        bool status = Users.login(Login1.UserName, Login1.Password);
        if (status)
            e.Authenticated = true;
    }
}
