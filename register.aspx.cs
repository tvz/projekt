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
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        string pass_salt, prehashed_pass, hashed_pass, created, updated;

        try
        {
            pass_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(DateTime.Now.ToString(), "SHA1").ToLower();
            prehashed_pass = CreateUserWizard1.Password.ToString() + pass_salt;
            hashed_pass = FormsAuthentication.HashPasswordForStoringInConfigFile(prehashed_pass, "SHA1").ToLower();

            created = DateTime.Now.Date.ToShortDateString();
            updated = DateTime.Now.Date.ToShortDateString();

            command.CommandText = "INSERT INTO users ([username], [password_hash], [pass_salt], [email], [created_at], [updated_at]) VALUES (@username, @password_hash, @pass_salt, @email, @created_at, @updated_at)";
            command.Parameters.AddWithValue("@username", CreateUserWizard1.UserName.ToString());
            command.Parameters.AddWithValue("@password_hash", hashed_pass);
            command.Parameters.AddWithValue("@pass_salt", pass_salt);
            command.Parameters.AddWithValue("@email", CreateUserWizard1.Email.ToString());
            command.Parameters.AddWithValue("@created_at", created);
            command.Parameters.AddWithValue("@updated_at", updated);

            command.Connection = conn;
            conn.Open();
            command.ExecuteNonQuery();
        }
        catch { }
        finally
        {
            conn.Close();
        }
    }
}