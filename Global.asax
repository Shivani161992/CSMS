<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        Application["appCtr"] = 0;
        Application["noOfUsers"] = 0;


    }
    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        Application.Lock();
        Application["appCtr"] = (int)Application["appCtr"] + 1;
        Application.UnLock();
    }

    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        Application.Lock();
        Application["appCtr"] = (int)Application["appCtr"] -1;
        Application.UnLock();

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        

    }

    void Session_Start(object sender, EventArgs e) 
    {
        Application.Add("sPath", "http://" + Request.Url.Host + Request.ApplicationPath);
        Application.Add("sPathU", "http://" + Request.Url.Host );
        // Code that runs when a new session is started
        Application.Lock();
        Application["noOfUsers"] = (int)Application["noOfUsers"] + 1;
        Application.UnLock(); 


    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session.Clear();
        Session.Abandon();
      
      
        Application.Lock();
        Application["noOfUsers"] = (int)Application["noOfUsers"] -1;
        Application.UnLock();
        
              
       

    }
    
       
</script>
