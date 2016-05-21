<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        //GenericPrincipal and GenericIdentity objects represent users who have been authenticated using 
        //Forms authentication or other custom authentication mechanisms. With these objects, 
        //the role list is obtained in a custom manner, typically from a database.
        //FormsIdentity and PassportIdentity objects represent users who have been 
        //authenticated with Forms and Passport authentication respectively.
        
        if (HttpContext.Current.User != null) //ako postoji GenericPrincipal objekt
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated) //ako je korisnik autentificiran
            {
                if (HttpContext.Current.User.Identity is FormsIdentity) //i ako se radi o forms autentifikaciji
                {
                    FormsIdentity id =
                    (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket; //zapamti Identity
                    //procitaj role iz userData i kreiraj ponovno GeneralPrincipal objekt sa rolama
                    string userData = ticket.UserData;
                    string[] roles = userData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                }
            }
        }
    }
       
</script>
