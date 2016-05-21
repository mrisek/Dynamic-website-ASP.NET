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

public partial class pdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        KreiranjePDFIzvjestaja();
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

    protected void KreiranjePDFIzvjestaja()
    {
        //kreiranje PDF dokumenta sa veličinom stranice i marginama
        //mjere su aproksimativno points (1pt = 1/72 incha)
        Document pdfDokument = new Document(PageSize.A4, 50, 50, 20, 50);

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
        p = new Paragraph("POPIS KONTAKATA", naslov);
        p.Alignment = Element.ALIGN_CENTER;
        p.SpacingBefore = 30;
        p.SpacingAfter = 30;
        pdfDokument.Add(p);

        //tablica
        PdfPTable t = new PdfPTable(6);   //kolona
        t.WidthPercentage = 100;    //širina tablice
        t.SetWidths(new float[] { 1, 2, 2, 3, 3, 3 }); //relativni odnosi sirina kolona

        //definiranje zaglavlja tablice
        //poboljsanje - umjesto 5 puta c/p mozda bolje napraviti zasebnu funkciju za formatiranje celije
        PdfPCell c1 = new PdfPCell(new Phrase("R.br.", tekst));
        c1.BackgroundColor = BaseColor.LIGHT_GRAY;
        c1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c1.Padding = 5;

        PdfPCell c2 = new PdfPCell(new Phrase("Ime", tekst));
        c2.BackgroundColor = BaseColor.LIGHT_GRAY;
        c2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c2.Padding = 5;

        PdfPCell c3 = new PdfPCell(new Phrase("Prezime", tekst));
        c3.BackgroundColor = BaseColor.LIGHT_GRAY;
        c3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c3.Padding = 5;


        PdfPCell c4 = new PdfPCell(new Phrase("E-mail", tekst));
        c4.BackgroundColor = BaseColor.LIGHT_GRAY;
        c4.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c4.Padding = 5;

        PdfPCell c5 = new PdfPCell(new Phrase("Telefon", tekst));
        c5.BackgroundColor = BaseColor.LIGHT_GRAY;
        c5.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c5.Padding = 5;

        PdfPCell c6 = new PdfPCell(new Phrase("Adresa", tekst));
        c6.BackgroundColor = BaseColor.LIGHT_GRAY;
        c6.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        c6.Padding = 5;


        //dodajemo kolone
        t.AddCell(c1);
        t.AddCell(c2);
        t.AddCell(c3);
        t.AddCell(c4);
        t.AddCell(c5);
        t.AddCell(c6);

        //dodajemo redove
        int i = 1;  //brojac redova
        foreach (Kontakt k in Imenik)
        {
            //rbr
            PdfPCell c = new PdfPCell(new Phrase((i++).ToString(), tekst));
            t.AddCell(c);
            //ostalo
            c = new PdfPCell(new Phrase(k.Ime, tekst));
            t.AddCell(c);
            c = new PdfPCell(new Phrase(k.Prezime, tekst));
            t.AddCell(c);
            c = new PdfPCell(new Phrase(k.Email, tekst));
            t.AddCell(c);
            c = new PdfPCell(new Phrase(k.Telefon, tekst));
            t.AddCell(c);
            c = new PdfPCell(new Phrase(k.Adresa, tekst));
            t.AddCell(c);
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
}