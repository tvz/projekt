/// <summary>
///
/// Klasa Users predstavlja users entitet u bazi podataka
/// </summary>
using System.Data.OleDb;
using System.Configuration;
using System.Web.Security;
using System.Collections;
public class Users
{

    /*varijable koje predstavljaju atribute u bazi*/
    private static int id;
    private static string username;
    private static string password_hash;
    private static string pass_salt;

    /*sve ostale varijable*/
    private static OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
    private static OleDbCommand command = new OleDbCommand();
    private static OleDbDataReader reader;

    public Users()
    {
    }

    /*
     developer: Emilio
     */
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

    /*
    developer: Ivan
    */
    public static void register(string username, string password, string email)
    {
        string prehashed_pass, hashed_pass, created, updated;
        bool userExists=false;
        command.CommandText = "SELECT username FROM users";
        command.Connection = conn;
        conn.Open();
        reader = command.ExecuteReader();

        /*provjerava ako zahtjevani username vec postoji,
          mislim da je jasno kaj se dalje dogadja
         * na formi trenutno javlja da je registracija uspjela (u biti nije)
           kad se napravi custom forma, problem ce bit rijesen
         */
        while (reader.Read())
        {
            if (reader.GetValue(0).ToString().Equals(username))
                userExists = true;
        }

        if (!userExists)
        {
            try
            {
                pass_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(System.DateTime.Now.ToString(), "SHA1").ToLower();
                prehashed_pass = password + pass_salt;
                hashed_pass = FormsAuthentication.HashPasswordForStoringInConfigFile(prehashed_pass, "SHA1").ToLower();

                created = System.DateTime.Now.Date.ToShortDateString();
                updated = System.DateTime.Now.Date.ToShortDateString();

                command.CommandText = "INSERT INTO users ([username], [password_hash], [pass_salt], [email], [created_at], [updated_at]) VALUES (@username, @password_hash, @pass_salt, @email, @created_at, @updated_at)";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password_hash", hashed_pass);
                command.Parameters.AddWithValue("@pass_salt", pass_salt);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@created_at", created);
                command.Parameters.AddWithValue("@updated_at", updated);
                command.Connection = conn;

                conn.Open();
                command.ExecuteNonQuery();

                /*ne znam jel ti ovo treba il ne, uglavnom, ne dela ako se izvrsi*/
                //FormsAuthentication.SetAuthCookie(username, true);
                //FormsAuthentication.RedirectFromLoginPage(username, true);
            }
            catch { }
            finally
            {
                command.Parameters.Clear();           
            }    
        }
        conn.Close();
    }
}
