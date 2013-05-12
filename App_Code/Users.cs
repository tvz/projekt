/// <summary>
///
/// Klasa Users predstavlja users entitet u bazi podataka
/// </summary>
using System.Data.OleDb;
using System.Configuration;
using System.Web.Security;
using System.Collections;
using System.Net.Mail;
public class Users
{

    /*varijable koje predstavljaju atribute u bazi*/
    private static int id;
    private static string username;
    private static string password_hash;
    private static string pass_salt;

    /*sve ostale varijable*/
    

    public Users()
    {
    }

    /*
     developer: Emilio
     */
    public static bool Login(string username, string password)
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader;

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
                    
                }
            }
        }
        catch
        {

        }
        finally
        {
            command.Dispose();
            conn.Close();
        }
        return login_status;
    }

    /*
    developer: Ivan
    */
    public static string register(string username, string password, string email)
    {
        string prehashed_pass, hashed_pass, created, updated, info = "", link;
        bool userExists = false;
        int user_id;
        System.Random rnd = new System.Random();
        int confirm_number = rnd.Next(1000000, 10000000);
            
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader = null;

        command.CommandText = "SELECT username FROM users";
        command.Connection = conn;
        conn.Open();
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            string temp = reader.GetValue(0).ToString();
            if (temp == username)
                userExists = true;
        }
        reader.Close();
        conn.Close();

        //ako nema istog korisnickog imena u bazi
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
                conn.Close();
                command.Parameters.Clear();

                command.CommandText = "SELECT ID FROM users WHERE username=@username";
                command.Parameters.AddWithValue("@username", username);
                command.Connection = conn;
                conn.Open();
                user_id = System.Int32.Parse(command.ExecuteScalar().ToString());
                conn.Close();
                command.Parameters.Clear();

                command.CommandText = "INSERT INTO confirmation VALUES(@user_id, @confirm_number)";
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@confirm_number", confirm_number);
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                sendMail("www.xxxxx.hr/xxxxx.aspx?confirmID="+confirm_number, email);

                info = "Registracija je uspješno obavljena.\nMolimo provjerite pretinac e-mail pošte i potvrdite registraciju.1";
            }
            catch {}
            finally
            {
                command.Parameters.Clear();
                conn.Close();
            }
        }
        else
            info = "Registracije je neuspješna. Molimo pokušajte ponovno.0";

        return info;
    }

    /*
     * developer: Ivan
     * description: metoda prima confirm_number preuzet iz url-a (obradjeno na stranici na koju ce url voditi)
     * na temelju te varijable aktivira korisnika
     */
    public static void activaction(int confirm_url)
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        int id;

        try
        {
            command.Connection = conn;
            command.CommandText = "SELECT user_ID FROM confirmation WHERE confirm_number=@confirm_number";
            command.Parameters.AddWithValue("@confirm_number", confirm_url);
            conn.Open();
            id = System.Int32.Parse(command.ExecuteScalar().ToString());
            conn.Close();
            command.Parameters.Clear();

            //naravno, field1 il kak ce se vec zvat ce imat odredjen atribut
            command.CommandText = "UPDATE status SET Field1='' WHERE ID=@id";
            command.Parameters.AddWithValue("@id", id);
            conn.Open();
            command.ExecuteNonQuery();
        }
        catch{}
        finally
        {
            conn.Close();
            command.Parameters.Clear();
        }
    }

    /*developer: Ivan
     * description: metoda salje mail korisniku s linkom za aktivaciju racuna
     */
    private static void sendMail(string link, string email)
    {
        MailMessage msg = new MailMessage();
        //kad se odlucimo za mail, onda ce SmtpClient konstruktor imat ispravan atribut
        SmtpClient smtp = new SmtpClient("");
        
        try
        {       
            //trebamo se dogovorit oko maila i stranice na koju ce link usmjeravati
            //pretpostavljam da cemo imat user stranicu, cisto je logicki, pa bu link vjerojatno na nju
            msg.Subject = "Potvrda registracije";
            msg.Body = ""+link;
            msg.To.Add(email);
            smtp.Send(msg);
        }
        catch { }
    }
}
