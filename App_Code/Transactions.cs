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

    /*developer: Emilio
     description: upisuje nepotvrdenu transakciju u bazu*/
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
    /*developer:Emilio
     description: nakon sto paypal vrati potvrdu transakcije popunimo transakciju sa ostalim podacima i oznacimo ju kao potvrdenu*/
    public static bool Postfill(string memo,
                                string preapproval_key,
                                string preapproval_approved, 
                                DateTime preapproval_starting_date,
                                DateTime preapproval_ending_date,
                                string pay_status, 
                                string pay_sender_email, 
                                string pay_receiver_email, 
                                string pay_key)
    {
        bool created = false;
        OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        OleDbCommand command = new OleDbCommand();
        DateTime updated_at = DateTime.Now;

        command.CommandText = "UPDATE transactions SET updated_at= @updated_at, memo = @memo, preaproval_key = @preaproval_key, pay_status = @pay_status, pay_sender_email = @pay_sender_email, pay_receiver_email = @pay_receiver_email, status= @status, payer_email= @payer_email, pay_key = @pay_key)";
        
        command.Parameters.AddWithValue("@updated_at", updated_at.ToShortDateString());
        command.Parameters.AddWithValue("@memo", memo);
        command.Parameters.AddWithValue("@preaproval_key", preaproval_key);
        command.Parameters.AddWithValue("@pay_status", pay_status);
        command.Parameters.AddWithValue("@pay_sender_email", pay_sender_email);
        command.Parameters.AddWithValue("@pay_receiver_email", pay_receiver_email);
        command.Parameters.AddWithValue("@status", status);
        command.Parameters.AddWithValue("@payer_email", payer_email);
        command.Parameters.AddWithValue("@pay_key", pay_key);
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
     description: Metoda dohvaca sumu donacija za odredeni projekt*/
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
