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
using System.Text;

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

    //By :Andor,metoda vraca userID na temelju username-a
    


    public static int getUserId(string uName)
    {   int returnVar=-1;
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader;
        command.Connection = conn;
        command.CommandText = "SELECT ID FROM users WHERE username = @uName";
        command.Parameters.AddWithValue("@uName", uName);
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                int p = -1;
                 returnVar=(int)reader.GetValue(++p);
               
                    
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
        return returnVar;
       
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
    public static string register(string username, string password, string question, string answer, string email)
    {
        string prehashed_pass, hashed_pass, pass_salt, hashed_answer, created, updated, info = "";
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
                hashed_answer = FormsAuthentication.HashPasswordForStoringInConfigFile(answer, "SHA1").ToLower();

                created = System.DateTime.Now.Date.ToShortDateString();
                updated = System.DateTime.Now.Date.ToShortDateString();

                command.CommandText = "INSERT INTO users ([username], [password_hash], [pass_salt], [sec_question], [sec_answer], [email], [created_at], [updated_at]) VALUES (@username, @password_hash, @pass_salt, @sec_question, @sec_answer, @email, @created_at, @updated_at)";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password_hash", hashed_pass);
                command.Parameters.AddWithValue("@pass_salt", pass_salt);
                command.Parameters.AddWithValue("@sec_question", question);
                command.Parameters.AddWithValue("@sec_answer", hashed_answer);
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

                command.CommandText = "INSERT INTO confirmation ([user_ID], [confirmation_number]) VALUES(@user_id, @confirm_number)";
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@confirm_number", confirm_number);
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                sendMail("http://www.zicalica.hr/user.aspx?confirmID="+confirm_number, email, "activation");

                info = "Registracija je uspješno obavljena.\nMolimo provjerite pretinac e-mail pošte i potvrdite registraciju.1";
            }
            catch { }
            finally
            {
                command.Parameters.Clear();
                command.Dispose();
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
     * ili s privremenim passwordom
     */
    public static void sendMail(string link, string email, string identificator)
    {
        MailMessage msg = new MailMessage();
        //kad se odlucimo za mail, onda ce SmtpClient konstruktor imat ispravan atribut
        SmtpClient smtp = new SmtpClient("");

        if (identificator == "activation")
            msg.Subject = "Potvrda registracije";
        else if (identificator == "pass")
            msg.Subject = "Privremena lozinka";
        else msg.Subject = "Povrat korisničkog imena";

        try
        {
            /*trebamo se dogovorit oko maila i stranice na koju ce link usmjeravati
            link ce ic na user stranicu ukoliko se radi o aktivaciji
            u suprotnom ce sadrzavati privremeni pass i link prema loginu
            msg.Body = "" + link;
            msg.To.Add(email);
            smtp.Send(msg);*/
        }
        catch { }
    }

    /*developer: Ivan
     * description: metoda sluzi za kreiranje privremene lozinke u slucaju zaborava
     */
    public static string checkAndGeneratePassword(string username, string email, string answer)
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbCommand commandPass = new OleDbCommand();
        string status = null, tempHashPass, pass_salt, tempPrehashedPass, hashed_answer;
        StringBuilder temp_password = new StringBuilder();
        Random random = new Random();

        //ako se salje email
        if (answer == null)
        {
            command.CommandText = "SELECT sec_question FROM users WHERE email=@email";
            command.Parameters.AddWithValue("@email", email);
            command.Connection = conn;
            try
            {
                conn.Open();
                status = command.ExecuteScalar().ToString();
            }
            catch { }
            finally
            {
                conn.Close();
                command.Dispose();
            }
        }
        //ako se salje odgovor 
        else
        {
            System.Diagnostics.Debug.WriteLine(email + " " + answer);
            hashed_answer = FormsAuthentication.HashPasswordForStoringInConfigFile(answer, "SHA1");
            command.CommandText = "SELECT username FROM users WHERE sec_answer=@sec_answer AND email=@email";
            command.Parameters.AddWithValue("@sec_answer", hashed_answer);
            command.Parameters.AddWithValue("@email", email);
            commandPass.CommandText = "UPDATE users SET [password_hash]=@password_hash, [pass_salt]=@pass_salt WHERE email=@email";
            commandPass.Connection = conn;
            command.Connection = conn;

            try
            {
                conn.Open();
                status = command.ExecuteScalar().ToString();

                //generiranje privremene lozinke
                if (status != null)
                {
                    pass_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(System.DateTime.Now.ToString(), "SHA1").ToLower();
                    char ch;
                    for (int i = 0; i < 4; i++)
                    {
                        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                        temp_password.Append(ch);
                        temp_password.Append(random.Next(0, 99));
                    }
                    System.Diagnostics.Debug.WriteLine(temp_password.ToString());
                    tempPrehashedPass = temp_password.ToString() + pass_salt;
                    tempHashPass = FormsAuthentication.HashPasswordForStoringInConfigFile(tempPrehashedPass, "SHA1").ToLower();

                    commandPass.Parameters.AddWithValue("@password_hash", tempHashPass);
                    commandPass.Parameters.AddWithValue("@pass_salt", pass_salt);
                    commandPass.Parameters.AddWithValue("@email", email);
                    commandPass.ExecuteNonQuery();

                    string msgBody = temp_password.ToString() + "\nlink prema loginu" +
                        "\nNakon prijave, molimo Vas da promijenite lozinku.";
                    sendMail(msgBody, email, "pass");
                }
            }
            catch { }
            finally
            {
                conn.Close();
                command.Dispose();
                commandPass.Dispose();
            }
        }
        return status;
    }

    /*developer: Ivan
     * description: metoda vraca korisnicko ime
     */
    public static bool checkAndGiveUsername(string email)
    {
        string username = null;
        bool status = false;
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();

        command.CommandText = "SELECT username FROM users WHERE email=@email";
        command.Parameters.AddWithValue("@email", email);
        command.Connection = conn;

        try
        {
            conn.Open();
            username = Convert.ToString(command.ExecuteScalar());
            if (username != null)
            {
                status = true;
                string msgBody = "Vaše korisničko ime je: " + username;
                sendMail(msgBody, email, "username");
            }
        }
        catch { }
        finally
        {
            conn.Close();
            command.Dispose();
        }

        return status;
    }
}
