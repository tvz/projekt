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

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /*developer:Emilio
    description: metoda logira korisnika i preusmjerava ga na index page(zasada)
    */
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        bool status = Users.Login(Login1.UserName, Login1.Password);
        if (status)
        {
            e.Authenticated = true;
            FormsAuthentication.SetAuthCookie(Login1.UserName, true);
            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, true);
        }
    }

    /* developer: Ivan
     * description: metoda kreira novog korisnika i preusmjerava ga na index page (zasada)
     */
    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        string info;
        info = Users.register(CreateUserWizard1.UserName,
            CreateUserWizard1.Password, CreateUserWizard1.Email);
        CreateUserWizard1.CompleteSuccessText = "" + info.Substring(0, info.Length - 1);

        if (info.Substring(info.Length - 1, 1) == "0")
        {
            CreateUserWizard1.ContinueDestinationPageUrl = "~/login.aspx?param1=123456";
            CreateUserWizard1.ContinueButtonText = "Pokušaj ponovno";
        }
        else
        {
            CreateUserWizard1.ContinueDestinationPageUrl = "~/index.aspx";
            CreateUserWizard1.ContinueButtonText = "Nastavi";
        }
    }
}
