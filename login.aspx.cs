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
using System.Data.OleDb;

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
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader;
        string prehashed_password;
        string password_hash;
        Users user = new Users();

        command.Connection = conn;
        command.CommandText = "SELECT * FROM users WHERE username = @username";
        command.Parameters.AddWithValue("@username", Login1.UserName); 
        
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                
                user.pass_salt = reader.GetValue((int)Users.databaseColumnName.pass_salt).ToString();
                user.password_hash = reader.GetValue((int)Users.databaseColumnName.password_hash).ToString();
                prehashed_password = Login1.Password + user.pass_salt;
                password_hash = FormsAuthentication.HashPasswordForStoringInConfigFile(prehashed_password, "SHA1").ToLower();
                if (password_hash == user.password_hash)
                {
                    e.Authenticated = true;
                    FormsAuthentication.SetAuthCookie(Login1.UserName, true);
                    FormsAuthentication.RedirectFromLoginPage(Login1.UserName, true);
                }
            }
        }
        catch
        {
           
        }
        command.Dispose();
        conn.Close();
    }
}
