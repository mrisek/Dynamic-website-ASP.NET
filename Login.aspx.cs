using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btLogin_Click(object sender, EventArgs e)
    {
        string uloga = ProvjeraKorisnika(tbUsername.Text, tbPassword.Text);
        if (uloga != String.Empty)
        {
            if (uloga.Substring(0, 1) == "?") //greška u pristupu podacima
                lbStatus.Text = "Pogreška kod logiranja: " + uloga.Substring(1);
            else
            {
                //kreiranje autentifikacijskog cookie-a                
                FormsAuthentication.SetAuthCookie(tbUsername.Text.Trim(), false); //username, persistent
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,  //verzija
                        tbUsername.Text.Trim(), //username
                        DateTime.Now, //vrijeme kreiranja
                        DateTime.Now.AddMinutes(10), //vrijeme isteka
                        false, //persistent cookie
                        uloga //rola
                        );
                HttpCookie cookie = new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(ticket));
                Response.Cookies.Add(cookie);
                //redirect
                String povratniUrl = Request.QueryString["ReturnUrl"];
                if (povratniUrl == null)
                {
                    povratniUrl = "~/Forme/User/Pocetna.aspx";
                }
                Response.Redirect(povratniUrl);
            }
        }
        else
            lbStatus.Text = "Pogrešno korisničko ime ili zaporka. Pokušajte ponovno..."; //ne postoji korisnik
    }

    //funkcija provjerava da li korisnik postoji u bazi i vraca njegove role
    public string ProvjeraKorisnika(string _username, string _password)
    {
        string uloga = String.Empty;
        try
        {
            MySqlConnection sqlConn = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["usersConnectionString"].ConnectionString);
            string cmdstr = "select ime, prezime, vrsta from korisnici where mail = @username and lozinka = @password";
            MySqlCommand cmd = new MySqlCommand(cmdstr, sqlConn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@username", _username);
            cmd.Parameters.AddWithValue("@password", _password);
            sqlConn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //rola
                if (reader["vrsta"].ToString() == "1")
                    uloga = "Admin";
                else if (reader["vrsta"].ToString() == "2")
                    uloga = "Prof";
                else if (reader["vrsta"].ToString() == "4")
                    uloga = "Admin,Prof";
                else
                    uloga = "User";
            }
            sqlConn.Close();
        }
        catch (Exception e)
        {
            uloga = "?" + e.Message;
        }
        return uloga;
    }
}