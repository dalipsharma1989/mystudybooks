<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
    }
    protected void Application_BeginRequest()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
    }
    protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError(); 
            if (exception != null)
            {
                Server.TransferRequest("~/404.html");
                DAL mad = new DAL();
                mad.InsertErrorLog(exception.Message.ToString() +" | Error In Detail |  "+ exception.StackTrace.ToString(), "0", "0", "Application_Error");
            }
            try
            {
                // This is to stop a problem where we were seeing "gibberish" in the
                // chrome and firefox browsers
                HttpApplication app = sender as HttpApplication;
                app.Response.Filter = null;
            }
            catch
            {
            }
        }
</script>
