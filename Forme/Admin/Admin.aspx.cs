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
using AjaxControlToolkit;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MySqlConnection sqlConn = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["usersConnectionString"].ConnectionString);

        string cmdstr = "select * from korisnici";
        MySqlCommand cmd = new MySqlCommand(cmdstr, sqlConn);
        cmd.CommandType = CommandType.Text;
        sqlConn.Open();
        MySqlDataReader reader = cmd.ExecuteReader();
        gvKorisnici.DataSource = reader;
        gvKorisnici.DataBind();
        sqlConn.Close();
    }

    protected void KreiranjePDFIzvjestaja()
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
        p = new Paragraph("ADMINISTRACIJA KORISNIKA", naslov);
        p.Alignment = Element.ALIGN_CENTER;
        p.SpacingBefore = 30;
        p.SpacingAfter = 30;
        pdfDokument.Add(p);

        //tablica
        PdfPTable t = new PdfPTable(9);   //kolona
        t.WidthPercentage = 100;    //širina tablice
        t.SetWidths(new float[] { 1, 2, 2, 3, 2, 2, 1, 3, 2 }); //relativni odnosi sirina kolona

        //definiranje zaglavlja tablice
        PdfPCell c1 = new PdfPCell(new Phrase("ID.", tekst));
        c1.BackgroundColor = BaseColor.LIGHT_GRAY;
        c1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c1.Padding = 5;

        PdfPCell c2 = new PdfPCell(new Phrase("Prezime", tekst));
        c2.BackgroundColor = BaseColor.LIGHT_GRAY;
        c2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c2.Padding = 5;

        PdfPCell c3 = new PdfPCell(new Phrase("Ime", tekst));
        c3.BackgroundColor = BaseColor.LIGHT_GRAY;
        c3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c3.Padding = 5;


        PdfPCell c4 = new PdfPCell(new Phrase("E-mail", tekst));
        c4.BackgroundColor = BaseColor.LIGHT_GRAY;
        c4.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c4.Padding = 5;

        PdfPCell c5 = new PdfPCell(new Phrase("Lozinka", tekst));
        c5.BackgroundColor = BaseColor.LIGHT_GRAY;
        c5.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c5.Padding = 5;

        PdfPCell c6 = new PdfPCell(new Phrase("Telefon", tekst));
        c6.BackgroundColor = BaseColor.LIGHT_GRAY;
        c6.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c6.Padding = 5;

        PdfPCell c7 = new PdfPCell(new Phrase("Vrsta", tekst));
        c7.BackgroundColor = BaseColor.LIGHT_GRAY;
        c7.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c7.Padding = 5;

        PdfPCell c8 = new PdfPCell(new Phrase("Adresa", tekst));
        c8.BackgroundColor = BaseColor.LIGHT_GRAY;
        c8.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c8.Padding = 5;

        PdfPCell c9 = new PdfPCell(new Phrase("Titula", tekst));
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
                string cmdstr = "select * from korisnici";

                MySqlCommand cmd = new MySqlCommand(cmdstr, sqlConn);
                cmd.CommandType = CommandType.Text;

                sqlConn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr1 in dt.Rows)
                {

                    PdfPCell c = new PdfPCell();

                    c = new PdfPCell(new Phrase(dr1["idKorisnici"].ToString(), tekst));
                    t.AddCell(c);
                    c = new PdfPCell(new Phrase(dr1["prezime"].ToString(), tekst));
                    t.AddCell(c);
                    c = new PdfPCell(new Phrase(dr1["ime"].ToString(), tekst));
                    t.AddCell(c);
                    c = new PdfPCell(new Phrase(dr1["mail"].ToString(), tekst));
                    t.AddCell(c);
                    c = new PdfPCell(new Phrase(dr1["lozinka"].ToString(), tekst));
                    t.AddCell(c);
                    c = new PdfPCell(new Phrase(dr1["tel"].ToString(), tekst));
                    t.AddCell(c);
                    c = new PdfPCell(new Phrase(dr1["vrsta"].ToString(), tekst));
                    t.AddCell(c);
                    c = new PdfPCell(new Phrase(dr1["adresa"].ToString(), tekst));
                    t.AddCell(c);
                    c = new PdfPCell(new Phrase(dr1["titula"].ToString(), tekst));
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

        if (File.Exists(put))
        {
            Response.Redirect("~/PDFs/" + nazivDokumenta, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        KreiranjePDFIzvjestaja();
    }

}