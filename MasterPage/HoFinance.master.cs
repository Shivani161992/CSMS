using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPage_HoFinance : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string str = "";
            if(Request.QueryString["id"]!=null)
            {

                str = Request.QueryString["id"].ToString();
                if (str == "home")
                {
                    home.Attributes.Add("class", "active");


                }
               else if (str == "entry")
                {
                    entry.Attributes.Add("class", "active");
                
                
                }
                else if (str == "rpt")
                {
                    rpt.Attributes.Add("class", "active");


                }
            
            }
        
        }
    }
}
