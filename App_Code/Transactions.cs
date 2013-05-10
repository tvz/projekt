﻿/// <summary>
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
    float payment_gross;

    /*sve ostale varijable*/
    private static OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
    private static OleDbCommand command = new OleDbCommand();
    private static OleDbDataReader reader;

    public Transactions()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /*developer:Emilio
     description: Metoda dohvaca sumu donacija za odredeni projekt*/
    public static float PaymentGrossSum(int project_id)
    {
        command.Connection = conn;
        command.CommandText = "SELECT SUM(payment_gross)as gross_sum FROM transactions WHERE project_id = @project_id";
        command.Parameters.AddWithValue("@project_id", project_id);
        float gross_sum = 0;

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                gross_sum = Convert.ToSingle(reader.GetValue(0));
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
        return gross_sum;
    }
}
