/// <summary>
///
/// Klasa Users predstavlja users entitet u bazi podataka
/// </summary>
using System.Data.OleDb;
using System.Configuration;
using System.Web.Security;
public class Users
{
    
    /*varijable koje predstavljaju atribute u bazi*/
    private static int id;
    private static string username;
    private static string  password_hash;
    private static string  pass_salt;



    /*sve ostale varijable*/
    private static OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
    private static OleDbCommand command = new OleDbCommand();
    private static OleDbDataReader reader;

    public Users()
    {
    }

    public static bool login(string username, string password)
    {

        string prehashed_password;
        string password_hash2;
        bool login_status = false;

        command.Connection = conn;
        command.CommandText = "SELECT password_hash, pass_salt FROM users WHERE username = @username";
        command.Parameters.AddWithValue("@username", username);

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {

                pass_salt = reader.GetValue(1).ToString();
                password_hash = reader.GetValue(0).ToString();
                prehashed_password = password + pass_salt;
                password_hash2 = FormsAuthentication.HashPasswordForStoringInConfigFile(prehashed_password, "SHA1").ToLower();
                if (password_hash == password_hash2)
                {
                    login_status = true;
                    FormsAuthentication.SetAuthCookie(username, true);
                    FormsAuthentication.RedirectFromLoginPage(username, true);
                }
            }
        }
        catch
        {

        }
        finally
        {
            //command.Dispose();
            command.Parameters.Clear();
            reader.Close();
            conn.Close();
        }
        return login_status;
    }
}
