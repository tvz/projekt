/// <summary>
/// Klasa Projects predstavlja projects entitet u bazi podataka
/// </summary>
/// 
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

    /*developer: Emilio
     description: metoda kreira novi projekt i sprema u bazu.*/
    public static bool Create(string name, string description, float goal, DateTime expiration_date, string image_path, string video_path, int user_id)
    {   
        bool created = false;
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        DateTime created_at = DateTime.Now; 
        DateTime updated_at = DateTime.Now; 
        command.CommandText = "INSERT INTO projects ([name], [description], [goal], [expiration_date], [created_at], [updated_at], [image_path], [video_path], [user_id]) VALUES (@name, @description, @goal, @expiration_date, @created_at, @updated_at, @image_path, @video_path, @user_id)";
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@goal", goal);
        command.Parameters.AddWithValue("@expiration_date", expiration_date.ToShortDateString());
        command.Parameters.AddWithValue("@created_at", created_at.ToShortDateString());
        command.Parameters.AddWithValue("@updated_at", updated_at.ToShortDateString());
        command.Parameters.AddWithValue("@image_path", image_path);
        command.Parameters.AddWithValue("@video_path", video_path);
        command.Parameters.AddWithValue("@user_id", user_id);
        command.Connection = conn;
        try
        {
            conn.Open();
            if (command.ExecuteNonQuery() == 1)
                created = true;
            else
            {
                /*nisam siguran da li treba rollback ako se unosi samo jedan red ali nek tu bude za svaki slucaj*/
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
        return created;
    }

    /*developer:Emilio
     description: metoda dohvaca jedan project iz baze*/
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

    /*developer: Emilio
     descripion: metoda dohvaca sve projekte i vraca listu projekata*/
    public static List<Projects> fetch_all()
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader;
        command.Connection = conn;
        command.CommandText = "SELECT projects.ID,projects.name, projects.description, projects.goal, projects.expiration_date, projects.image_path, projects.video_path, users.username FROM (projects INNER JOIN users ON projects.user_id = users.ID) GROUP BY projects.ID, projects.name, projects.description, projects.goal,projects.expiration_date, projects.image_path, projects.video_path, users.username";

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
                project.project_owner_username = reader.GetValue(++column).ToString();
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
    /*developer: Emilio
     descripion: metoda vraca sumu sonacija za projekt*/
    public float DonationSum()
    {
        return Transactions.PaymentAmountSum(this.id);
    }
    /*developer: Emilio
     descripion: metoda vraca postotak sakupljenih donacija u odnosu na project goal*/
    public float DonationsPercent()
    {
        return (Transactions.PaymentAmountSum(this.id) / this.goal) * 100;
    }

    /*developer: Ivan
     * description: metoda pretrazuje projekte i vraca listu projekata
     */   
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
            "projects.created_at,projects.expiration_date,projects.image_path,users.username FROM" +
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
            "projects.created_at,projects.expiration_date,projects.image_path,users.username FROM" +
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
                project.project_owner_username = reader.GetValue(++column).ToString();
                search_list.Add(project);
            }
        }
        catch (System.Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace); }
        finally
        {
            command.Dispose();
            conn.Close();
        }
        return search_list;
    }
}
