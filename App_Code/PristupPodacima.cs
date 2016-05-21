using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

public class PristupPodacima
{
    // vraća konekciju na mysql bazu
    public static MySqlConnection Konekcija()
    {
        MySqlConnection conn = new MySqlConnection(
            ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString);
        try
        {
            conn.Open();
            return conn;
        }
        catch (Exception)
        {
            return null;
        }        
    }

    // kreira komandu i pridružuje parametre
    private static MySqlCommand VratiKomandu(string sql, MySqlConnection conn, object[] paramArray)
    {
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.CommandType = CommandType.Text;
        int index = 1;
        if (paramArray != null)
        {
            foreach (object par in paramArray)
            {
                cmd.Parameters.Add(new MySqlParameter("@p" + index, par));
                index++;
            }
        }
        return cmd;
    }

    // izvršava sql upit (non-query) - za insert, update, delete
    // povratni argument je broj ažuriranih slogova
    public static int IzvrsiUpit(string sql, object[] paramArray)
    {
        try
        {
            int koliko = 0;
            using (MySqlConnection conn = Konekcija())
            {
                MySqlCommand cmd = VratiKomandu(sql, conn, paramArray);
                koliko = cmd.ExecuteNonQuery();
            }
            return koliko;
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    // vraća datatable iz baze
    public static DataTable VratiDataTable(string sql, object[] paramArray)
    {
        try
        {
            using (MySqlConnection conn = Konekcija())
            {
                MySqlCommand cmd = VratiKomandu(sql, conn, paramArray);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    // vraća podatak iz baze podataka (scalar)
    public static string VratiPodatak(string sql, object[] paramArray)
    {
        try
        {
            using (MySqlConnection conn = Konekcija())
            {
                MySqlCommand cmd = VratiKomandu(sql, conn, paramArray);
                return cmd.ExecuteScalar().ToString(); 
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
}