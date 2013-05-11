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
    int id;
    public string name;
    public string description;
    public float goal;
    public string image_path;
    public DateTime expiration_date;

    /*sve ostale varijable*/
    public string project_owner_username;

    public Projects()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /*developer: Emilio
     descripion: metoda dohvaca sve projekte i vraca listu projekata*/
    public static List<Projects> fetch_all()
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader = null;
        command.Connection = conn;
        command.CommandText = "SELECT projects.ID,projects.name, projects.description, projects.goal, projects.expiration_date, projects.image_path, users.username FROM (projects INNER JOIN users ON projects.user_id = users.ID) GROUP BY projects.ID, projects.name, projects.description, projects.goal,projects.expiration_date, projects.image_path, users.username";

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
            reader.Close();
            conn.Close();
        }
        return projects_list;
    }
    /*developer: Emilio
     descripion: metoda vraca sumu sonacija za projekt*/
    public float DonationSum()
    {
        return Transactions.PaymentGrossSum(this.id);
    }
    /*developer: Emilio
     descripion: metoda vraca postotak sakupljenih donacija u odnosu na project goal*/
    public float DonationsPercent()
    {
        return (Transactions.PaymentGrossSum(this.id) / this.goal) * 100;
    }
}
