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
using Data;
using DataAccess;
using System.Data.SqlClient;
public partial class District_TransportOrder_Type : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    DistributionCenters distobj = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string time;
    public string sid = "";
    public string distid = "";
    public string snid = "";
    public string getdatef = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());

            if (!IsPostBack)
            {
                
                GetSource();

            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");


        }

    }
    void GetSource()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.Source_Arrival_Type order by Source_ID";
        DataSet ds = mobj.selectAny(qry);

        ddlsarrival.DataSource = ds.Tables[0];
        ddlsarrival.DataTextField = "Source_Name";
        ddlsarrival.DataValueField = "Source_ID";
        ddlsarrival.DataBind();
        ddlsarrival.Items.Insert(0, "--Select--");
    }
    protected void ddlsarrival_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsarrival.SelectedItem.Text == "From FCI")
        {
            Response.Redirect("~/District/Generate_TransportOrder.aspx");


        }
        else
        {
            string type = ddlsarrival.SelectedItem.Text;
            Session["Source"] = type;
            Session["Sorce_ID"] = ddlsarrival.SelectedValue;
            Response.Redirect("~/District/Transport_Order_Other.aspx");

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
}
