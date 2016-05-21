using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text;


public partial class Pocetna : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Obrada();

        gvImenik.DataSource = Imenik;
        gvImenik.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //ako je forma validirana upisujemo podatke u bazu
        if (Page.IsValid)
        {
            //klasično pisanje u bazu
            //UpisiAnketu();

            UpisiAnketu();

            //redirect
            Response.Redirect("Potvrda.aspx");
        }
    }

    //Vježba 05 - Upis u bazu klasično
    protected void UpisiAnketu()
    {
        //klasičan upis u bazu podataka
        //konekcija   
        MySqlConnection sqlConn = new MySqlConnection(
            ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString);
        //sql upit
        string cmdstr = "insert into praksa " +
                        "(imePraksa, emailPraksa, oibPraksa, spolPraksa, adresaPraksa, godinaPraksa, drzavaPraksa, posaoPraksa) " +
                        "values (@ime, @email, @oib, @spol, @adresa, @godina, @drzava, @posao)";
        //komanda
        MySqlCommand cmd = new MySqlCommand(cmdstr, sqlConn);
        cmd.CommandType = CommandType.Text;
        //parametri komande
        cmd.Parameters.AddWithValue("@ime", tbImeIPrezime.Text);
        cmd.Parameters.AddWithValue("@email", tbEmail.Text);
        cmd.Parameters.AddWithValue("@oib", tbOIB.Text);
        cmd.Parameters.AddWithValue("@spol", rbSpol.SelectedValue);
        cmd.Parameters.AddWithValue("@adresa", tbAdresa.Text);
        cmd.Parameters.AddWithValue("@godina", ddGodina.SelectedValue);
        //obrada liste hobija
        string s = String.Empty;
        foreach (System.Web.UI.WebControls.ListItem li in lbHobiji.Items)
            if (li.Selected)
                s += li.Value + "; ";
        cmd.Parameters.AddWithValue("@drzava", s);
        //obrada liste boja
        s = String.Empty;
        foreach (System.Web.UI.WebControls.ListItem li in cbBoje.Items)
            if (li.Selected)
                s += li.Value + "; ";
        cmd.Parameters.AddWithValue("@posao", s);
        //otvaramo konekciju
        sqlConn.Open();
        //izvrsavamo upit
        cmd.ExecuteNonQuery();
        //zatvaramo konekciju
        sqlConn.Close();
    }


    //Vježba 02 - server-side validacija OIB-a
    protected void vlOIB_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = oib.CheckOIB(args.Value);
    }

    //Vježba 01 - kreiranje i obrada forme
    protected void Obrada()
    {
        StringBuilder sb = new StringBuilder();
        string s;
        sb.AppendLine("Upisali ste sljedeće podatke:");
        //ime i prezime
        if (tbImeIPrezime.Text != String.Empty)
            sb.AppendLine("Ime i prezime: " + tbImeIPrezime.Text);
        //e-mail
        if (tbEmail.Text != String.Empty)
            sb.AppendLine("E-mail: " + tbEmail.Text);
        //oib
        if (tbOIB.Text != String.Empty)
            sb.AppendLine("OIB: " + tbOIB.Text);
        //spol
        if (rbSpol.SelectedItem != null)
            sb.AppendLine("Spol: " + rbSpol.SelectedItem.Text);
        //adresa
        if (tbAdresa.Text != String.Empty)
            sb.AppendLine("Adresa: " + tbAdresa.Text.Replace(Environment.NewLine, ", "));
        //godina studija
        if (ddGodina.SelectedItem != null)
            sb.AppendLine("Godina studija: " + ddGodina.SelectedItem.Text);
        //omiljeni hobiji
        s = String.Empty;
        foreach (System.Web.UI.WebControls.ListItem li in lbHobiji.Items)
            if (li.Selected)
            {
                if (s == String.Empty)
                    s = "Država: ";
                else
                    s += ", ";
                s += li.Value.ToString();
            }
        if (s != String.Empty)
            sb.AppendLine(s);
        //preferirane boje
        s = String.Empty;
        foreach (System.Web.UI.WebControls.ListItem li in cbBoje.Items)
            if (li.Selected)
            {
                if (s == String.Empty)
                    s = "Praksa: ";
                else
                    s += ", ";
                s += li.Text;
            }
        if (s != String.Empty)
            sb.AppendLine(s);
        //vracamo napomenu
        tbNapomena.Text = sb.ToString();
    }

    protected List<Kontakt> Imenik
    {
        get
        {
            List<Kontakt> imenik = new List<Kontakt>();
            imenik.Add(new Kontakt("Matija", "Risek", "mrisek@mev.hr", "+385 489765221", "Varaždin"));
            imenik.Add(new Kontakt("Krešimir", "Kolac", "kkolac@ipc.hr", "+385 98745632", "Mursko Središće"));
            imenik.Add(new Kontakt("Bruno", "Trstenjak", "btrstenja@mev.hr", "+385 6412346789", "Čakovec"));
            imenik.Add(new Kontakt("Ivan", "Novak", "inovak@erasmus.hr", "+385 846521379", "Čakovec"));
            return imenik;
        }
    }

}