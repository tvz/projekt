/// <summary>
/// Klasa Transactions predstavlja transactions entitet u bazi podataka
/// </summary>
/// 
using System.Data.OleDb;
using System.Configuration;
using System.Collections.Generic;
using System;

public class Transactions
{
    /*varijable koje predstavljaju atribute u bazi*/
    int project_id;
    float amount;

    /*sve ostale varijable*/
    

    public Transactions()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Developer: Emilio
    /// Description: upisuje nepotvrdenu transakciju u bazu
    /// </summary>
    /// <param name="project_id">ID projekta</param>
    /// <param name="amount">kolicina novca za donaciju</param>
    /// <param name="preapproval_key">PayPalov preapproval key</param>
    /// <returns>Uspjesnost upisa transakcije</returns>
    public static bool Prefill(int project_id, float amount, string preapproval_key)
    {
        bool created = false;
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        DateTime created_at = DateTime.Now; 
        DateTime updated_at = DateTime.Now; 
        
        command.CommandText = "INSERT INTO transactions ([project_id], [amount], [preapproval_key], [created_at], [updated_at]) VALUES (@project_id, @amount, @preapproval_key, @created_at, @updated_at)";
        command.Parameters.AddWithValue("@project_id", project_id);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@preaproval_key", preapproval_key);
        command.Parameters.AddWithValue("@created_at", created_at.ToShortDateString());
        command.Parameters.AddWithValue("@updated_at", updated_at.ToShortDateString());
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
         
    /// <summary>
    /// Developer:Emilio
    /// Description: nakon sto paypal vrati potvrdu transakcije popunimo transakciju sa ostalim podacima i oznacimo ju kao potvrdenu
    /// </summary>
    /// <param name="preapproval_key">PayPalov preapproval key</param>
    /// <param name="memo"></param>
    /// <param name="preapproval_approved"></param>
    /// <param name="preapproval_starting_date">Pocetni datum</param>
    /// <param name="preapproval_ending_date">Datum isteka</param>
    /// <param name="sender_email"></param>
    /// <returns>Uspjesnost upisa potvrdjene transakcije</returns>
    public static bool Postfill(string preapproval_key,
                                string memo,
                                string preapproval_approved, 
                                DateTime preapproval_starting_date,
                                DateTime preapproval_ending_date, 
                                string sender_email)
    {
        bool created = false;
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        DateTime updated_at = DateTime.Now;

        command.CommandText = "UPDATE transactions SET updated_at= @updated_at, memo = @memo, preapproval_approved=@approved, sender_email=@sender_email WHERE preapproval_key=@preapproval_key";
        
        command.Parameters.AddWithValue("@updated_at", updated_at.ToShortDateString());
        command.Parameters.AddWithValue("@memo", memo);
        command.Parameters.AddWithValue("@preaproval_key", preapproval_key);
        command.Parameters.AddWithValue("@preapproval_approved", preapproval_approved);
        command.Parameters.AddWithValue("@sender_email", sender_email);
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

    /// <summary>
    /// Developer:Emilio
    /// Description: Metoda dohvaca sumu donacija za odredeni projekt
    /// </summary>
    /// <param name="project_id">ID projekta</param>
    /// <returns>Sumu donacija</returns>
    public static float PaymentAmountSum(int project_id)
    {
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        OleDbDataReader reader = null;

        command.Connection = conn;
        command.CommandText = "SELECT SUM(amount)as amount_sum FROM transactions WHERE project_id = @project_id";
        command.Parameters.AddWithValue("@project_id", project_id);
        float amount_sum = 0;

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                amount_sum = Convert.ToSingle(reader.GetValue(0));
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
        return amount_sum;
    }
}
