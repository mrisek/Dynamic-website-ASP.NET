using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Kontakt
/// </summary>
public class Kontakt
{
	public Kontakt(string ime, string prezime, string email, string telefon, string adresa)
	{
        Ime = ime;
        Prezime = prezime;
        Email = email;
        Telefon = telefon;
        Adresa = adresa;
	}

    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public string Adresa { get; set; }
}