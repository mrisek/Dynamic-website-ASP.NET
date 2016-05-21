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
using System.Web.Services;
using System.Web.Script.Services;


public partial class Pregled : System.Web.UI.Page
{

    private DataTable dt1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //klasično sa datareaderom ili datatable
        //UcitajAnkete();
        dt1 = UcitajPraksu();

        //čitanje pomoću zajedničke klase PristupPodacima
        UcitajAnkete2();

        //ukupan broj rezultata pretraživanja
        lbBrojZapisa.Text = "Broj rezultata: " + 
            PristupPodacima.VratiPodatak("Select count(1) from studenti"+SqlWhere(), paramTrazi);
    }


    protected DataTable UcitajPraksu()
    {
        DataTable dt = new DataTable();

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString))
        {
            string cmdstr = "select * from praksa";
            MySqlCommand cmd = new MySqlCommand(cmdstr, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            da.Fill(dt);
            gvTablica.DataSource = dt;
            gvTablica.DataBind();
        }

        return dt;
    }

    protected DataTable UcitajPraksuSortirano()
    {
        DataTable dt = new DataTable();

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString))
        {
            string cmdstr = "select * from praksa where godinaPraksa='1'";
            MySqlCommand cmd = new MySqlCommand(cmdstr, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            da.Fill(dt);
            gvTablica.DataSource = dt;
            gvTablica.DataBind();
        }

        return dt;
    }

    protected DataTable UcitajPraksuSortirano2()
    {
        DataTable dt = new DataTable();

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString))
        {
            string cmdstr = "select * from praksa where godinaPraksa='2'";
            MySqlCommand cmd = new MySqlCommand(cmdstr, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            da.Fill(dt);
            gvTablica.DataSource = dt;
            gvTablica.DataBind();
        }

        return dt;
    }

    protected DataTable UcitajPraksuSortirano3()
    {
        DataTable dt = new DataTable();

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString))
        {
            string cmdstr = "select * from praksa where godinaPraksa='3'";
            MySqlCommand cmd = new MySqlCommand(cmdstr, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            da.Fill(dt);
            gvTablica.DataSource = dt;
            gvTablica.DataBind();
        }

        return dt;
    }


    protected void UcitajAnkete()
    {
        //klasično čitanje sa datareaderom ili u datatable/dataset
        MySqlConnection conn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString);
        string cmdstr = "select * from studenti";
        MySqlCommand cmd = new MySqlCommand(cmdstr, conn);
        cmd.CommandType = CommandType.Text;
        conn.Open();
        
        //datareader
        MySqlDataReader reader = cmd.ExecuteReader();
        
        gvStudenti.DataSource = reader;
    }

    //čitanje korištenjem zajedničke klase PristupPodacima
    protected void UcitajAnkete2()
    {
            string cmdstr = "select * from studenti " + SqlWhere();
            gvStudenti.DataSource = PristupPodacima.VratiDataTable(cmdstr, paramTrazi);
            gvStudenti.DataBind();
    }

    //parametri pretaživanja
    protected object[] paramTrazi
    {
        get
        {
            object[] o = { "%" + tbTrazi.Text.ToUpper() + "%" };
            return o;
        }
    }

    //where sql upita
    protected string SqlWhere()
    {
        return " where upper(ime_i_prezime) like @p1 "+
               " or upper(email) like @p1 or godina like @p1 or upper(spol) like @p1 " +
               " or upper(adresa) like @p1 or upper(hobiji) like @p1 or upper(boje) like @p1 or oib like @p1";
    }


    //pretraživanje grida
    protected void btnTrazi_Click(object sender, EventArgs e)
    {
        UcitajAnkete2();
    }


    //brisanje ankete
    protected void lbObrisati_Click(object sender, EventArgs e)
    {
        //dohvat id-a ankete
        GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent;
        string id = row.Cells[0].Text;
        //brisanje ankete
        object[] parametri = { Convert.ToInt16(id) }; 
        PristupPodacima.IzvrsiUpit("delete from studenti where id = @p1", parametri);
        //refresh
        UcitajAnkete2();
    }

    protected void KreiranjePDFIzvjestajaPraksa()
    {
        //kreiranje PDF dokumenta sa veličinom stranice i marginama
        //mjere su aproksimativno points (1pt = 1/72 incha)
        Document pdfDokument = new Document(PageSize.A4, 50, 50, 20, 50);
        //neka bude landscape jer ima puno podataka
        pdfDokument.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

        //kreiranje datoteke sa jedinstvenim nazivom
        string nazivDokumenta = Guid.NewGuid() + ".pdf";

        //dokumenti se spremaju u mapu PDFs
        string put = Path.Combine(Server.MapPath("~/PDFs"), nazivDokumenta);

        //kreiranje pdf dokumenta na disku
        PdfWriter.GetInstance(pdfDokument, new FileStream(put, FileMode.Create));

        //otvaranje dokumenta
        pdfDokument.Open();


        //definiranje fontova
        BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, false);
        //Font header = FontFactory.GetFont("Times", 10, Font.BOLD);  //ALTERNATIVNO
        Font header = new Font(font, 12, Font.NORMAL, BaseColor.DARK_GRAY);
        Font naslov = new Font(font, 14, Font.BOLDITALIC, BaseColor.BLACK);
        Font tekst = new Font(font, 10, Font.NORMAL, BaseColor.BLACK);

        var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/LOGO-MEV.jpg"));
        //logo.SetAbsolutePosition(440, 800); //apsolutno pozicioniranje
        logo.Alignment = Element.ALIGN_LEFT;    //relativno pozicioniranje
        logo.ScaleAbsoluteWidth(100);
        logo.ScaleAbsoluteHeight(100);
        pdfDokument.Add(logo);


        //header - teks generiramo pomocu objekata Chunk, Phrase i Paragraph
        Paragraph p = new Paragraph("Međimursko veleučilište u Čakovcu\nStudentski Zbor", header);
        pdfDokument.Add(p);

        //naslov
        p = new Paragraph("PRIJAVLJENI STUDENTI ZA STRUČNU PRAKSU", naslov);
        p.Alignment = Element.ALIGN_CENTER;
        p.SpacingBefore = 30;
        p.SpacingAfter = 30;
        pdfDokument.Add(p);

        //tablica
        PdfPTable t = new PdfPTable(9);   //kolona
        t.WidthPercentage = 100;    //širina tablice
        t.SetWidths(new float[] { 1, 2, 2, 2, 1, 2, 1, 1, 2 }); //relativni odnosi sirina kolona

        //definiranje zaglavlja tablice
        PdfPCell c1 = new PdfPCell(new Phrase("ID.", tekst));
        c1.BackgroundColor = BaseColor.LIGHT_GRAY;
        c1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c1.Padding = 5;

        PdfPCell c2 = new PdfPCell(new Phrase("Ime i prezime", tekst));
        c2.BackgroundColor = BaseColor.LIGHT_GRAY;
        c2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c2.Padding = 5;

        PdfPCell c3 = new PdfPCell(new Phrase("E-mail", tekst));
        c3.BackgroundColor = BaseColor.LIGHT_GRAY;
        c3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c3.Padding = 5;


        PdfPCell c4 = new PdfPCell(new Phrase("OIB", tekst));
        c4.BackgroundColor = BaseColor.LIGHT_GRAY;
        c4.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c4.Padding = 5;

        PdfPCell c5 = new PdfPCell(new Phrase("Spol", tekst));
        c5.BackgroundColor = BaseColor.LIGHT_GRAY;
        c5.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c5.Padding = 5;

        PdfPCell c6 = new PdfPCell(new Phrase("Adresa", tekst));
        c6.BackgroundColor = BaseColor.LIGHT_GRAY;
        c6.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c6.Padding = 5;

        PdfPCell c7 = new PdfPCell(new Phrase("Godina", tekst));
        c7.BackgroundColor = BaseColor.LIGHT_GRAY;
        c7.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c7.Padding = 5;

        PdfPCell c8 = new PdfPCell(new Phrase("Država", tekst));
        c8.BackgroundColor = BaseColor.LIGHT_GRAY;
        c8.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c8.Padding = 5;

        PdfPCell c9 = new PdfPCell(new Phrase("Posao", tekst));
        c9.BackgroundColor = BaseColor.LIGHT_GRAY;
        c9.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c9.Padding = 5;



        //dodajemo kolone
        t.AddCell(c1);
        t.AddCell(c2);
        t.AddCell(c3);
        t.AddCell(c4);
        t.AddCell(c5);
        t.AddCell(c6);
        t.AddCell(c7);
        t.AddCell(c8);
        t.AddCell(c9);


        //dodajemo redove
        using (MySqlConnection sqlConn = new MySqlConnection(
         ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString))
        {
            string cmdstr = "select * from praksa";

            MySqlCommand cmd = new MySqlCommand(cmdstr, sqlConn);
            cmd.CommandType = CommandType.Text;

            sqlConn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr1 in dt.Rows)
            {

                PdfPCell c = new PdfPCell();

                c = new PdfPCell(new Phrase(dr1["idPraksa"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["imePraksa"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["oibPraksa"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["emailPraksa"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["spolPraksa"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["adresaPraksa"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["godinaPraksa"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["drzavaPraksa"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["posaoPraksa"].ToString(), tekst));
                t.AddCell(c);
            }
            sqlConn.Close();
        }

        //dodajemo tablicu na dokument
        pdfDokument.Add(t);

        //mjesto i vrijeme
        p = new Paragraph("Čakovec, " + System.DateTime.Now.ToString("F"), header);
        p.Alignment = Element.ALIGN_RIGHT;
        p.SpacingBefore = 30;
        pdfDokument.Add(p);

        //zatvaranje dokumenta
        pdfDokument.Close();
        //return put;

        if (File.Exists(put))
        {
            Response.Redirect("~/PDFs/" + nazivDokumenta, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }





    protected void KreiranjePDFIzvjestajaStudij()
    {
        //kreiranje PDF dokumenta sa veličinom stranice i marginama
        //mjere su aproksimativno points (1pt = 1/72 incha)
        Document pdfDokument = new Document(PageSize.A4, 50, 50, 20, 50);
        //neka bude landscape jer ima puno podataka
        pdfDokument.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

        //kreiranje datoteke sa jedinstvenim nazivom
        string nazivDokumenta = Guid.NewGuid() + ".pdf";

        //dokumenti se spremaju u mapu PDFs
        string put = Path.Combine(Server.MapPath("~/PDFs"), nazivDokumenta);

        //kreiranje pdf dokumenta na disku
        PdfWriter.GetInstance(pdfDokument, new FileStream(put, FileMode.Create));

        //otvaranje dokumenta
        pdfDokument.Open();


        //definiranje fontova
        BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, false);
        //Font header = FontFactory.GetFont("Times", 10, Font.BOLD);  //ALTERNATIVNO
        Font header = new Font(font, 12, Font.NORMAL, BaseColor.DARK_GRAY);
        Font naslov = new Font(font, 14, Font.BOLDITALIC, BaseColor.BLACK);
        Font tekst = new Font(font, 10, Font.NORMAL, BaseColor.BLACK);

        var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/LOGO-MEV.jpg"));
        //logo.SetAbsolutePosition(440, 800); //apsolutno pozicioniranje
        logo.Alignment = Element.ALIGN_LEFT;    //relativno pozicioniranje
        logo.ScaleAbsoluteWidth(100);
        logo.ScaleAbsoluteHeight(100);
        pdfDokument.Add(logo);


        //header - teks generiramo pomocu objekata Chunk, Phrase i Paragraph
        Paragraph p = new Paragraph("Međimursko veleučilište u Čakovcu\nStudentski Zbor", header);
        pdfDokument.Add(p);

        //naslov
        p = new Paragraph("PRIJAVLJENI STUDENTI ZA STRUČNI STUDIJ", naslov);
        p.Alignment = Element.ALIGN_CENTER;
        p.SpacingBefore = 30;
        p.SpacingAfter = 30;
        pdfDokument.Add(p);

        //tablica
        PdfPTable t = new PdfPTable(9);   //kolona
        t.WidthPercentage = 100;    //širina tablice
        t.SetWidths(new float[] { 1, 2, 2, 2, 1, 2, 1, 2, 1 }); //relativni odnosi sirina kolona

        //definiranje zaglavlja tablice
        PdfPCell c1 = new PdfPCell(new Phrase("ID.", tekst));
        c1.BackgroundColor = BaseColor.LIGHT_GRAY;
        c1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c1.Padding = 5;

        PdfPCell c2 = new PdfPCell(new Phrase("Ime i prezime", tekst));
        c2.BackgroundColor = BaseColor.LIGHT_GRAY;
        c2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c2.Padding = 5;

        PdfPCell c3 = new PdfPCell(new Phrase("OIB", tekst));
        c3.BackgroundColor = BaseColor.LIGHT_GRAY;
        c3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c3.Padding = 5;


        PdfPCell c4 = new PdfPCell(new Phrase("E-mail", tekst));
        c4.BackgroundColor = BaseColor.LIGHT_GRAY;
        c4.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c4.Padding = 5;

        PdfPCell c5 = new PdfPCell(new Phrase("Spol", tekst));
        c5.BackgroundColor = BaseColor.LIGHT_GRAY;
        c5.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c5.Padding = 5;

        PdfPCell c6 = new PdfPCell(new Phrase("Adresa", tekst));
        c6.BackgroundColor = BaseColor.LIGHT_GRAY;
        c6.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c6.Padding = 5;

        PdfPCell c7 = new PdfPCell(new Phrase("Godina", tekst));
        c7.BackgroundColor = BaseColor.LIGHT_GRAY;
        c7.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c7.Padding = 5;

        PdfPCell c8 = new PdfPCell(new Phrase("Hobiji", tekst));
        c8.BackgroundColor = BaseColor.LIGHT_GRAY;
        c8.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c8.Padding = 5;

        PdfPCell c9 = new PdfPCell(new Phrase("Boje", tekst));
        c9.BackgroundColor = BaseColor.LIGHT_GRAY;
        c9.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c9.Padding = 5;



        //dodajemo kolone
        t.AddCell(c1);
        t.AddCell(c2);
        t.AddCell(c3);
        t.AddCell(c4);
        t.AddCell(c5);
        t.AddCell(c6);
        t.AddCell(c7);
        t.AddCell(c8);
        t.AddCell(c9);


        //dodajemo redove
        using (MySqlConnection sqlConn = new MySqlConnection(
         ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString))
        {
            string cmdstr = "select * from studenti";

            MySqlCommand cmd = new MySqlCommand(cmdstr, sqlConn);
            cmd.CommandType = CommandType.Text;

            sqlConn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr1 in dt.Rows)
            {

                PdfPCell c = new PdfPCell();

                c = new PdfPCell(new Phrase(dr1["id"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["ime_i_prezime"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["oib"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["email"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["spol"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["adresa"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["godina"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["hobiji"].ToString(), tekst));
                t.AddCell(c);
                c = new PdfPCell(new Phrase(dr1["boje"].ToString(), tekst));
                t.AddCell(c);
            }
            sqlConn.Close();
        }


        //dodajemo tablicu na dokument
        pdfDokument.Add(t);

        //mjesto i vrijeme
        p = new Paragraph("Čakovec, " + System.DateTime.Now.ToString("F"), header);
        p.Alignment = Element.ALIGN_RIGHT;
        p.SpacingBefore = 30;
        pdfDokument.Add(p);

        //zatvaranje dokumenta
        pdfDokument.Close();
        //return put;

        if (File.Exists(put))
        {
            Response.Redirect("~/PDFs/" + nazivDokumenta, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }



    //Tipka za PDF o praktikantima
    protected void Button1_Click(object sender, EventArgs e)
    {
        KreiranjePDFIzvjestajaPraksa();
    }

    //Tipka za PDF o studentima
    protected void Button2_Click(object sender, EventArgs e)
    {
        KreiranjePDFIzvjestajaStudij();
    }


    //Tipka za filtriranje Spola pomoću odabira sa radio-buttona
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (RadioButton1.Checked) 
        {
            string cmdstr = "";
            try
            {
                tbTrazi.Text = "";
                lbBrojZapisa.Text = "";
                cmdstr = "select * from user.studenti where spol='m'";
                gvStudenti.DataSource = PristupPodacima.VratiDataTable(cmdstr, paramTrazi);
                gvStudenti.DataBind();
            }
            catch
            {
                tbTrazi.Text = "Nema studenata!";
            }

        }
        if (RadioButton2.Checked) 
        {
            string cmdstr = "";
            try
            {
                tbTrazi.Text = "";
                lbBrojZapisa.Text = "";
                cmdstr = "select * from user.studenti where spol='z'";
                gvStudenti.DataSource = PristupPodacima.VratiDataTable(cmdstr, paramTrazi);
                gvStudenti.DataBind(); 
            }
            catch
            {
                tbTrazi.Text = "Nema studentica!";
            }

        }
    }

    //tipka za filtriranje Godine
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (RadioButton3.Checked)
        {
            string cmdstr = "";
            try
            {
                tbTrazi.Text = "";
                lbBrojZapisa.Text = "";

                cmdstr = "select * from user.studenti where godina='1'";
                gvStudenti.DataSource = PristupPodacima.VratiDataTable(cmdstr, paramTrazi);
                gvStudenti.DataBind();
            }
            catch 
            {
                tbTrazi.Text = "Nema 1. godine!";
            }
        }


        if (RadioButton4.Checked) 
        {
            string cmdstr = "";
            try
            {
                tbTrazi.Text = "";
                lbBrojZapisa.Text = "";

                cmdstr = "select * from user.studenti where godina='2'";
                gvStudenti.DataSource = PristupPodacima.VratiDataTable(cmdstr, paramTrazi);
                gvStudenti.DataBind();
            }
            catch 
            {
                tbTrazi.Text = "Nema 2. godine!";
            }
        }



        if (RadioButton5.Checked) 
        {
            string cmdstr = "";
            try
            {
                tbTrazi.Text = "";
                lbBrojZapisa.Text = "";

                cmdstr = "select * from user.studenti where godina='3'";
                gvStudenti.DataSource = PristupPodacima.VratiDataTable(cmdstr, paramTrazi);
                gvStudenti.DataBind();
            }
            catch 
            {
                tbTrazi.Text = "Nema 3. godine!";
            }
        }

    }

    //tipka Osvjezi
    protected void Button5_Click(object sender, EventArgs e)
    {
        RadioButton1.Checked = false;
        RadioButton2.Checked = false;
        RadioButton3.Checked = false;
        RadioButton4.Checked = false;
        RadioButton5.Checked = false;
    }



    //Tipka za kombinaciju uvjeta filtriranja omogućava
    //filtriranje prema unesenoj riječi i prema spolu
    protected void Button6_Click(object sender, EventArgs e)
    {
        using(MySqlConnection sqlConn = new MySqlConnection(
            ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString))
        {
            string cmdstr="";
            try
            {
                lbBrojZapisa.Text = "";
                cmdstr = "select * from user.studenti where studenti.spol='" + tbTrazi.Text + "';";
                gvStudenti.DataSource = PristupPodacima.VratiDataTable(cmdstr, paramTrazi);
                gvStudenti.DataBind();
            }
            catch
            {
                tbTrazi.Text="Nema rezultata!";
            }
        }
    }


    //tipka za kombinaciju uvjeta filtriranja omogućava
    //filtriranje prema unesenoj riječi i prema godini studija
    protected void Button7_Click(object sender, EventArgs e)
    {
        using (MySqlConnection sqlConn = new MySqlConnection(
            ConfigurationManager.ConnectionStrings["defaultConnectionString"].ConnectionString))
        {
            string cmdstr = "";
            try
            {
                lbBrojZapisa.Text = "";
                cmdstr = "select * from user.studenti where studenti.godina='" + tbTrazi.Text + "';";
                gvStudenti.DataSource = PristupPodacima.VratiDataTable(cmdstr, paramTrazi);
                gvStudenti.DataBind();
            }
            catch
            {
                tbTrazi.Text = "Nema rezultata!";
            }
        }
    }



    //Tipka za sortiranje praktikanata (samo prva godina)
    protected void Button8_Click(object sender, EventArgs e)
    {
        UcitajPraksuSortirano();
    }

    //Tipka za sortiranje praktikanata (samo druga godina)
    protected void Button9_Click(object sender, EventArgs e)
    {
        UcitajPraksuSortirano2();
    }

    //Tipka za sortiranje praktikanata (samo treća godina)
    protected void Button10_Click(object sender, EventArgs e)
    {
        UcitajPraksuSortirano3();
    }

    //Tipka za prikaz svih praktikanata
    protected void Button11_Click(object sender, EventArgs e)
    {
        UcitajPraksu();
    }
}