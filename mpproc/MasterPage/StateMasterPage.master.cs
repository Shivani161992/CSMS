using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class mpproc_MasterPage_StateMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["StateLog_AgId"] == null)
        {

            Response.Redirect("../sessionexpired.aspx");
          
        }


        if (Session["StateLog_Agency"] == null || Session["StateLog_AgId"] == null || Session["StateLog_MarkID"] == null ||  Session["StateLog_CropY"] == null)
        {
            Response.Redirect("../sessionexpired.aspx");
        }


        lbl_logInfo.Text = Session["StateLog_Agency"].ToString() ;

    }
}
