/// <summary>
/// Klasa Projects predstavlja projects entitet u bazi podataka
/// </summary>
/// 
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections.Generic;
using System;
public class Projects
{
    
    /*varijable koje predstavljaju atribute u bazi*/
    public int id;
    public string name;
    public string description;
    public float goal;
    public string image_path;
    public string video_path;
    public DateTime expiration_date;
    public DateTime created_at;
    public DateTime updated_at;
    public int user_id;

    /*sve ostale varijable*/
    public string project_owner_username;

    public Projects()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Projects(string name, string description, float goal, DateTime expiration_date, string image_path, string video_path, int user_id)
    {
        this.name = name;
        this.description = description;
        this.goal = goal;
        this.expiration_date = expiration_date;
        this.image_path = image_path;
        this.video_path = video_path;
        this.user_id = user_id;
    }

    /// <summary>
    /// Developer: Andor
    /// Azuriranje opisa projekta iz projekata korisnika.
    /// </summary>
    /// <param name="description"></param>
    /// <param name="descriptionOld"></param>
    /// <param name="ID"></param>
    /// <returns></returns>
    public static bool storeChange(string description/*,string descriptionOld*/,int ID)
    {
        bool success = false;
             
        int projectID = ID;
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        // By: Vladimir Ivančev; ako već postoji jedinstveni ID onda nam ne treba provjera po starom descriptionu
        // command.CommandText = "UPDATE projects SET description=@description1 WHERE description=@descriptionOld1 AND ID=@projectID";
        command.CommandText = "UPDATE projects SET description=@description1 WHERE ID=@projectID";
        command.Parameters.AddWithValue("@description1",description.ToString());
        // command.Parameters.AddWithValue("@descriptionOld1",descriptionOld.ToString());
        command.Parameters.AddWithValue("@projectID",ID);
        command.Connection = conn;
        try
        {
            conn.Open();

            if (command.ExecuteNonQuery() == 1)
            {
                success = true;
            }
            else
            {            
                command.Transaction.Rollback();
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
    return success;     
    }

    /// <summary>
    /// Developer: Emilio
    /// Description: metoda kreira novi projekt i sprema ga u bazu.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="goal"></param>
    /// <param name="expiration_date"></param>
    /// <param name="image_path"></param>
    /// <param name="video_path"></param>
    /// <param name="user_id"></param>
    /// <returns></returns>
    public static bool Create(string name, string description, float goal, DateTime expiration_date, string image_path, string video_path, int user_id)
    {   
        bool created = false;
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        command.CommandText = "INSERT INTO projects ([name], [description], [goal], [expiration_date], [created_at], [updated_at], [image_path], [video_path], [user_id], [enabled]) VALUES (@name, @description, @goal, @expiration_date, NOW(), NOW(), @image_path, @video_path, @user_id, 'False')";
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@goal", goal);
        command.Parameters.AddWithValue("@expiration_date", expiration_date.ToShortDateString());
        command.Parameters.AddWithValue("@image_path", image_path);
        command.Parameters.AddWithValue("@video_path", video_path);
        command.Parameters.AddWithValue("@user_id", user_id);
        command.Connection = conn;
        try
        {
            conn.Open();
            command.Transaction = conn.BeginTransaction();
            if (command.ExecuteNonQuery() == 1)
            {
                created = true;
                command.Transaction.Commit();
            }
            else
            {
                /*nisam siguran da li treba rollback ako se unosi samo jedan red ali nek tu bude za svaki slucaj*/
                command.Transaction.Rollback();
            }
        }
        catch
        {
            command.Transaction.Rollback();
        }
        finally
        {
            command.Dispose();
            conn.Close();
        }
        return created;
    }

    /// <summary>
    /// Developer: Andor
    /// Description: Metoda vraca projekt iz liste projekata usera  
    /// predstavljenog sa userId na odredenoj poziciji
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Projects returnSeq(int userId,int position)
    { 
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader;
        command.Connection = conn;
        command.CommandText = "SELECT projects.ID,projects.name, projects.description, projects.goal, projects.expiration_date, projects.image_path, projects.video_path,projects.user_id FROM projects WHERE user_id=@userId";
        command.Parameters.AddWithValue("@userId", userId);
        List<Projects> projects_list = new List<Projects>();
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int column = -1;
                Projects project = new Projects();
                project.id = Convert.ToInt32(reader.GetValue(++column));
                project.name = reader.GetValue(++column).ToString();
                project.description = reader.GetValue(++column).ToString();
                project.goal = Convert.ToSingle(reader.GetValue(++column));
                project.expiration_date = Convert.ToDateTime(reader.GetValue(++column));
                project.image_path = reader.GetValue(++column).ToString();
                project.video_path = reader.GetValue(++column).ToString();
                project.user_id = Convert.ToInt32(reader.GetValue(++column));
                projects_list.Add(project);
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
        return projects_list[position];
    }

    /// <summary>
    /// Developer: Emilio
    /// Description: Metoda brise projekt iz baze
    /// </summary>
    /// <param name="id">ID projekta</param>
    /// <returns>bool koji označava je li brisanje uspjelo</returns>
    public static bool Delete(int id)
    {
        bool obrisan = false;
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        command.CommandText = "DELETE FROM projects WHERE ID = @id";
        command.Parameters.AddWithValue("@id", id);
        command.Connection = conn;
        try
        {
            conn.Open();
            command.Transaction = conn.BeginTransaction();
            if (command.ExecuteNonQuery() == 1)
            {
                obrisan = true;
                command.Transaction.Commit();
            }
            else
                command.Transaction.Rollback();
        }
        catch
        {
            command.Transaction.Rollback();
        }
        finally
        {
            command.Dispose();
            conn.Close();
        }
        return obrisan;
    }

    /// <summary>
    /// Developer:Emilio
    /// Description: Metoda dohvaca jedan project iz baze
    /// </summary>
    /// <param name="project_id">ID projekta</param>
    /// <returns>Projekt</returns>
    public static Projects FetchProject(int project_id)
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader;

        command.Connection = conn;
        command.CommandText = "SELECT * FROM projects WHERE ID = @project_id";
        command.Parameters.AddWithValue("@project_id", project_id);

        Projects project = new Projects();
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                int column = -1;
                
                project.id = Convert.ToInt32(reader.GetValue(++column));
                project.name = reader.GetValue(++column).ToString();
                project.description = reader.GetValue(++column).ToString();
                project.goal = Convert.ToSingle(reader.GetValue(++column));
                project.expiration_date = Convert.ToDateTime(reader.GetValue(++column));
                project.created_at = Convert.ToDateTime(reader.GetValue(++column));
                project.updated_at = Convert.ToDateTime(reader.GetValue(++column));
                project.image_path = reader.GetValue(++column).ToString();
                project.video_path = reader.GetValue(++column).ToString();
                project.user_id = Convert.ToInt32(reader.GetValue(++column));
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
        return project;
    }

    /// <summary>
    /// Developer: Andor
    /// Description: Metoda vraca listu projekata koje je odredeni korisnik predstavljen sa user_id napravio   
    /// </summary>
    /// <param name="userId">ID korisnika</param>
    /// <returns>Lista projekata od zadanog korisnika</returns>
    public static List<Projects> fetch_all_projects(int userId)
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader;
        command.Connection = conn;
        command.CommandText = "SELECT projects.ID,projects.name, projects.description, projects.goal, projects.expiration_date, projects.image_path, projects.video_path,projects.user_id FROM projects WHERE user_id=@userId";
        command.Parameters.AddWithValue("@userId", userId);
        List<Projects> projects_list = new List<Projects>();
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int column = -1;
                Projects project = new Projects();
                project.id = Convert.ToInt32(reader.GetValue(++column));
                project.name = reader.GetValue(++column).ToString();
                project.description = reader.GetValue(++column).ToString();
                project.goal = Convert.ToSingle(reader.GetValue(++column));
                project.expiration_date = Convert.ToDateTime(reader.GetValue(++column));
                project.image_path = reader.GetValue(++column).ToString();
                project.video_path = reader.GetValue(++column).ToString();
                project.user_id = Convert.ToInt32(reader.GetValue(++column));
                projects_list.Add(project);
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
        return projects_list;
    }

    /// <summary>
    /// Developer: Emilio
    /// Description: metoda dohvaca sve projekte i vraca listu projekata
    /// By Ivan: isto kak sam napisal i u index.aspx.cs
    /// </summary>
    /// <returns></returns>
    public static List<Projects> fetch_all()
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader;
        command.Connection = conn;
        command.CommandText = "SELECT projects.ID,projects.name, projects.description, projects.goal, projects.expiration_date, projects.image_path, projects.video_path, projects.enabled, users.username FROM (projects INNER JOIN users ON projects.user_id = users.ID) GROUP BY projects.ID, projects.name, projects.description, projects.goal,projects.expiration_date, projects.image_path, projects.video_path, projects.enabled, users.username";

        List<Projects> projects_list = new List<Projects>();
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetValue(7).ToString() == "True")
                {
                    int column = -1;
                    Projects project = new Projects();
                    project.id = Convert.ToInt32(reader.GetValue(++column));
                    project.name = reader.GetValue(++column).ToString();
                    project.description = reader.GetValue(++column).ToString();
                    project.goal = Convert.ToSingle(reader.GetValue(++column));
                    project.expiration_date = Convert.ToDateTime(reader.GetValue(++column));
                    project.image_path = reader.GetValue(++column).ToString();
                    project.video_path = reader.GetValue(++column).ToString();
                    project.project_owner_username = reader.GetValue(column+2).ToString();
                    projects_list.Add(project);
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
        return projects_list;
    }

    /// <summary>
    /// Developer: Emilio
    /// Descripion: metoda vraca sumu donacija za projekt
    /// </summary>
    /// <returns></returns>
    public float DonationSum()
    {
        return Transactions.PaymentAmountSum(this.id);
    }

    /// <summary>
    /// Developer: Emilio
    /// Descripion: metoda vraca postotak sakupljenih donacija u odnosu na project goal
    /// </summary>
    /// <returns>Postotak sakupljenih donacija</returns>
    public float DonationsPercent()
    {
        return (Transactions.PaymentAmountSum(this.id) / this.goal) * 100;
    }

    /*
     * 
     */   

    /// <summary>
    /// Developer: Ivan
    /// Description: metoda pretrazuje projekte i vraca listu projekata
    /// </summary>
    /// <param name="searchParameters">Parametri za pretragu</param>
    /// <returns>Lista projekata</returns>
    public static List<Projects> searchProjects(params string[] searchParameters)
    {
        List<Projects> search_list = new List<Projects>();
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader;
        DateTime createdAtDateStart, expirationDateStart, createdAtDateEnd, expirationDateEnd;
        string projectName = searchParameters[0];
        int goalStart=0, goalEnd=0;

        if(searchParameters[1].Length>0)
            goalStart = Convert.ToInt32(searchParameters[1]);
        if(searchParameters[2].Length>0)
            goalEnd = Convert.ToInt32(searchParameters[2]);

        createdAtDateStart = Convert.ToDateTime(searchParameters[3]);
        createdAtDateEnd = Convert.ToDateTime(searchParameters[4]);
        expirationDateStart = Convert.ToDateTime(searchParameters[5]);
        expirationDateEnd = Convert.ToDateTime(searchParameters[6]);
        System.Diagnostics.Debug.WriteLine(projectName.Length);

        //moglo je i urednije, al jbg :)
        //buduci da LIKE funkcionira tako da na prazan ulaz vraca sve, mora se koristiti if
        if (projectName.Length > 0)
        {
            command.CommandText = "SELECT projects.ID,projects.name,projects.description,projects.goal," +
            "projects.created_at,projects.expiration_date,projects.image_path,projects.video_path,users.username FROM" +
            "(projects INNER JOIN users ON projects.user_id = users.ID) WHERE [projects.name] LIKE @name" +
            " OR [projects.goal] BETWEEN @goalStart AND @goalEnd OR projects.created_at BETWEEN @createdStart AND @createdEnd" +
            " OR projects.expiration_date BETWEEN @expirationStart AND @expirationEnd OR ([projects.name] LIKE @name AND [projects.goal] BETWEEN @goalStart AND @goalEnd)" +
            " OR ([projects.name] LIKE @name AND [projects.goal] BETWEEN @goalStart AND @goalEnd AND projects.created_at BETWEEN @createdStart AND @createdEnd)" +
            " OR ([projects.name] LIKE @name AND [projects.goal] BETWEEN @goalStart AND @goalEnd AND projects.created_at BETWEEN @createdStart AND @createdEnd AND" +
            " projects.expiration_date BETWEEN @expirationStart AND @expirationEnd) OR ([projects.name] LIKE @name AND projects.created_at BETWEEN @createdStart AND @createdEnd)" +
            " OR ([projects.name] LIKE @name AND projects.expiration_date BETWEEN @expirationStart AND @expirationEnd)" +
            " OR ([projects.name] LIKE @name AND projects.created_at BETWEEN @createdStart AND @createdEnd AND projects.expiration_date BETWEEN @expirationStart AND @expirationEnd)" +
            " OR ([projects.name] LIKE @name AND [projects.goal] BETWEEN @goalStart AND @goalEnd AND projects.created_at BETWEEN @createdStart AND @createdEnd)" +
            " OR ([projects.goal] BETWEEN @goalStart AND @goalEnd AND projects.created_at BETWEEN @createdStart AND @createdEnd) OR ([projects.goal] BETWEEN @goalStart AND @goalEnd AND" +
            " projects.expiration_date BETWEEN @expirationStart AND @expirationEnd) OR ([projects.goal] BETWEEN @goalStart AND @goalEnd AND" +
            " projects.created_at BETWEEN @createdStart AND @createdEnd AND projects.expiration_date BETWEEN @expirationStart AND @expirationEnd) OR" +
            " (projects.created_at BETWEEN @createdStart AND @createdEnd AND projects.expiration_date BETWEEN @expirationStart AND @expirationEnd)";
            command.Parameters.AddWithValue("@name", "%"+projectName+"%");
            command.Parameters.AddWithValue("@goalStart", goalStart);
            command.Parameters.AddWithValue("goalEnd", goalEnd);
            command.Parameters.AddWithValue("@createdStart", createdAtDateStart);
            command.Parameters.AddWithValue("@createdEnd", createdAtDateEnd);
            command.Parameters.AddWithValue("@expirationStart", expirationDateStart);
            command.Parameters.AddWithValue("@expirationEnd", expirationDateEnd);
        }
        else
        {
            command.CommandText = "SELECT projects.ID,projects.name,projects.description,projects.goal," +
            "projects.created_at,projects.expiration_date,projects.image_path,projects.video_path,users.username FROM" +
            "(projects INNER JOIN users ON projects.user_id = users.ID) WHERE [projects.name] = @name" +
            " OR [projects.goal] BETWEEN @goalStart AND @goalEnd OR projects.created_at BETWEEN @createdStart AND @createdEnd" +
            " OR projects.expiration_date BETWEEN @expirationStart AND @expirationEnd OR ([projects.name] = @name AND [projects.goal] BETWEEN @goalStart AND @goalEnd)" +
            " OR ([projects.name] = @name AND [projects.goal] BETWEEN @goalStart AND @goalEnd AND projects.created_at BETWEEN @createdStart AND @createdEnd)" +
            " OR ([projects.name] = @name AND [projects.goal] BETWEEN @goalStart AND @goalEnd AND projects.created_at BETWEEN @createdStart AND @createdEnd AND" +
            " projects.expiration_date BETWEEN @expirationStart AND @expirationEnd) OR ([projects.name] = @name AND projects.created_at BETWEEN @createdStart AND @createdEnd)" +
            " OR ([projects.name] = @name AND projects.expiration_date BETWEEN @expirationStart AND @expirationEnd)" +
            " OR ([projects.name] = @name AND projects.created_at BETWEEN @createdStart AND @createdEnd AND projects.expiration_date BETWEEN @expirationStart AND @expirationEnd)" +
            " OR ([projects.name] = @name AND [projects.goal] BETWEEN @goalStart AND @goalEnd AND projects.created_at BETWEEN @createdStart AND @createdEnd)" +
            " OR ([projects.goal] BETWEEN @goalStart AND @goalEnd AND projects.created_at BETWEEN @createdStart AND @createdEnd) OR ([projects.goal] BETWEEN @goalStart AND @goalEnd AND" +
            " projects.expiration_date BETWEEN @expirationStart AND @expirationEnd) OR ([projects.goal] BETWEEN @goalStart AND @goalEnd AND" +
            " projects.created_at BETWEEN @createdStart AND @createdEnd AND projects.expiration_date BETWEEN @expirationStart AND @expirationEnd) OR" +
            " (projects.created_at BETWEEN @createdStart AND @createdEnd AND projects.expiration_date BETWEEN @expirationStart AND @expirationEnd)";
            command.Parameters.AddWithValue("@name", projectName);
            command.Parameters.AddWithValue("@goal", goalStart);
            command.Parameters.AddWithValue("goalEnd", goalEnd);
            command.Parameters.AddWithValue("@createdStart", createdAtDateStart);
            command.Parameters.AddWithValue("@createdEnd", createdAtDateEnd);
            command.Parameters.AddWithValue("@expirationStart", expirationDateStart);
            command.Parameters.AddWithValue("@expirationEnd", expirationDateEnd);
        }
        command.Connection = conn;
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int column = -1;
                Projects project = new Projects();
                project.id = Convert.ToInt32(reader.GetValue(++column));
                project.name = reader.GetValue(++column).ToString();
                project.description = reader.GetValue(++column).ToString();
                project.goal = Convert.ToSingle(reader.GetValue(++column));
                project.created_at = Convert.ToDateTime(reader.GetValue(++column));
                project.expiration_date = Convert.ToDateTime(reader.GetValue(++column));
                project.image_path = reader.GetValue(++column).ToString();
                project.video_path = reader.GetValue(++column).ToString();
                project.project_owner_username = reader.GetValue(++column).ToString();
                search_list.Add(project);
            }
        }
        catch {}
        finally
        {
            command.Dispose();
            conn.Close();
        }
        return search_list;
    }

    /*developer: Ivan
     * description: metoda dohvaca info o projektu
     */
    public static DataTable fetchInfo()
    {
        DataTable news = new DataTable();
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        string command = "SELECT name, description, created_at, image_path FROM projects ORDER BY created_at DESC";

        try
        {
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(command, conn);
            da.Fill(news);
        }
        catch { }
        finally
        {
            conn.Close();
        }

        return news;
    }
}
