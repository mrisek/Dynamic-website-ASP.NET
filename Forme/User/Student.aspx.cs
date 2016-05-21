using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

public partial class Student : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Obrada();

        gvImenik.DataSource = Imenik;
        gvImenik.DataBind();

        if (!IsPostBack)
        {
            //prihvat parametara ako se radi o editiranju postojece forme
            string editId = Request.QueryString["editId"];
            if (editId != null)
            {
                //dohvat i popunjavanje ankete
                object[] parametri = { editId };
                string cmdstr = "select ime_i_prezime, email, oib, spol, adresa, hobiji, boje from studenti ";
                cmdstr += "where id = " + editId;
                DataTable dt = PristupPodacima.VratiDataTable(cmdstr, parametri);
                //popunjavanje forme
                tbId.Text = editId;
                tbImeIPrezime.Text = dt.Rows[0]["ime_i_prezime"].ToString();
                tbEmail.Text = dt.Rows[0]["email"].ToString();
                tbOIB.Text = dt.Rows[0]["oib"].ToString();
                rbSpol.SelectedValue = dt.Rows[0]["spol"].ToString();
                tbAdresa.Text = dt.Rows[0]["adresa"].ToString();
                //dodati još petlje za hobije i boje
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //ako je forma validirana upisujemo podatke u bazu
        if (Page.IsValid)
        {
            //klasično pisanje u bazu
            //UpisiAnketu();

            //upis u bazu korištenjem klase PristupPodacima
            UpisiAnketu2();

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
        string cmdstr = "insert into studenti " +
                        "(ime_i_prezime, email, oib, spol, adresa, godina, hobiji, boje) " +
                        "values (@ime, @email, @oib, @spol, @adresa, @godina, @hobiji, @boje)";
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
        foreach (ListItem li in lbHobiji.Items)
            if (li.Selected)
                s += li.Value + "; ";
        cmd.Parameters.AddWithValue("@hobiji", s);
        //obrada liste boja
        s = String.Empty;
        foreach (ListItem li in cbBoje.Items)
            if (li.Selected)
                s += li.Value + "; ";
        cmd.Parameters.AddWithValue("@boje", s);
        //otvaramo konekciju
        sqlConn.Open();
        //izvrsavamo upit
        cmd.ExecuteNonQuery();
        //zatvaramo konekciju
        sqlConn.Close();
    }

    //Vježba 05 - Upis u bazu koristeći zajedničku klasu PristupPodacima
    protected void UpisiAnketu2()
    {
        //obrada liste hobija
        string hobiji = String.Empty;
        foreach (ListItem li in lbHobiji.Items)
            if (li.Selected)
                hobiji += li.Value + "; ";
        //obrada liste boja
        string boje = String.Empty;
        foreach (ListItem li in cbBoje.Items)
            if (li.Selected)
                boje += li.Value + "; ";
        //parametri
        object[] parametri = { tbImeIPrezime.Text, 
                               tbEmail.Text,
                               tbOIB.Text,
                               rbSpol.SelectedValue,
                               tbAdresa.Text,
                               ddGodina.SelectedValue,
                               hobiji,
                               boje
                             };
        string cmdstr = "insert into studenti " +
                        "(ime_i_prezime, email, oib, spol, adresa, godina, hobiji, boje) " +
                        "values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)";
        //ako je forma u update modu
        if (tbId.Text != String.Empty)
        {
            cmdstr = "update studenti set ime_i_prezime = @p1, email = @p2, oib = @p3, " +
                     "spol = @p4, adresa = @p5, godina = @p6, hobiji = @p7, boje = @p8 " +
                     "where id = @p9";
            //daodajemo id u listu parametara
            List<object> l = parametri.ToList();
            l.Add(tbId.Text);
            parametri = l.ToArray();
        }
        //vraca broj upisanih redova
        int koliko = PristupPodacima.IzvrsiUpit(cmdstr, parametri);
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
        foreach (ListItem li in lbHobiji.Items)
            if (li.Selected)
            {
                if (s == String.Empty)
                    s = "Omiljeni hobiji: ";
                else
                    s += ", ";
                s += li.Value.ToString();
            }
        if (s != String.Empty)
            sb.AppendLine(s);
        //preferirane boje
        s = String.Empty;
        foreach (ListItem li in cbBoje.Items)
            if (li.Selected)
            {
                if (s == String.Empty)
                    s = "Preferirane boje: ";
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


    protected void pdfButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("pdf.aspx", false);
        Context.ApplicationInstance.CompleteRequest();
    }
}