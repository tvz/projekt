/// <summary>
///
/// Klasa Users predstavlja users entitet u bazi podataka
/// </summary>
/// 
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Net.Mail;
using System.Web.Security;

public class Users
{

    /*varijable koje predstavljaju atribute u bazi*/
    public  int id;
    public string username;
    public string password_hash;
    public string pass_salt;
    public DateTime created_at;
    public DateTime updated_at;
    public int role_id;
    public string paypal_id;
    public string email;
    public int status_id;


    /*sve ostale varijable*/
    

    public Users()
    {
    }

    /*
     developer: Emilio*/
    public static Users FetchUser(int user_id)
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader;

        command.Connection = conn;
        command.CommandText = "SELECT * FROM users WHERE ID = @user_id";
        command.Parameters.AddWithValue("@user_id", user_id);

        Users user = new Users();
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                int column = -1;

                user.id = Convert.ToInt32(reader.GetValue(++column));
                user.username = reader.GetValue(++column).ToString();
                user.password_hash = reader.GetValue(++column).ToString();
                user.pass_salt = reader.GetValue(++column).ToString();
                user.created_at = Convert.ToDateTime(reader.GetValue(++column));
                user.updated_at = Convert.ToDateTime(reader.GetValue(++column));
                user.role_id = Convert.ToInt32(reader.GetValue(++column));
                user.paypal_id = reader.GetValue(++column).ToString();
                user.email = reader.GetValue(++column).ToString();
                user.status_id = Convert.ToInt32(reader.GetValue(++column));
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
        return user;
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
        string password_hash;
        string userPassword_hash;
        string userPass_salt;
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

                userPass_salt = reader.GetValue(1).ToString();
                userPassword_hash = reader.GetValue(0).ToString();
                prehashed_password = password + userPass_salt;
                password_hash = FormsAuthentication.HashPasswordForStoringInConfigFile(prehashed_password, "SHA1").ToLower();
                if (password_hash == userPassword_hash)
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
        string prehashed_pass, hashed_pass,pass_salt, created, updated, info = "", link;
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
