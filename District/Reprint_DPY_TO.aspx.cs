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
using System.Data.SqlClient;

public partial class District_Reprint_DPT_TO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public string ToNo = "";
    public string distid = "";
    public string comdty = "";
    public string sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            ToNo = Session["TO"].ToString();

            distid = Session["dist_id"].ToString();

            sid = Session["sid"].ToString();



            if (!IsPostBack)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                fillgrid();

                filldata();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }

    protected void fillgrid()
    {
        string qry = "SELECT  convert(nvarchar,TransportDate,103)ToDate ,RouteNumber,feed_no, tbl_rootchart_master.fps_name + ' (' + tbl_rootchart_master.fps_code + ')' as FPS , Payment_mode , isnull(WheatAllot,'')Wheat , isnull(RiceAllot,'')Rice ,isnull(SugarAllot,'')Sugar ,isnull(SaltAllot,'')Salt , isnull(MaizeAllot,'')Maize  FROM DPY_TranportOrder inner join tbl_rootchart_master on tbl_rootchart_master.fps_code = DPY_TranportOrder.FPSCode where TransportOrder = '" + ToNo + "' and DPY_TranportOrder.DistrictId = '" + distid + "' and DPY_TranportOrder.IssueCenter = '" + sid + "'";
        SqlCommand cmd = new SqlCommand(qry, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];

        GridView1.DataBind();
    }

    protected void filldata()
    {
        string qry = "select Transporter_Table.Transporter_Name , pds.districtsmp.district_name , tbl_MetaData_DEPOT.DepotName , TransportOrder, CONVERT(nvarchar,DPY_TranportOrder.CreatedDate ,103)as dDate, DATENAME(month, AllotMonth) AS AllotMonth from Transporter_Table inner join DPY_TranportOrder on Transporter_Table.Transporter_ID =  DPY_TranportOrder.TransporterId inner join pds.districtsmp on pds.districtsmp.district_code = DPY_TranportOrder.DistrictId inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = DPY_TranportOrder.IssueCenter where DPY_TranportOrder.TransportOrder = '" + ToNo + "' and DPY_TranportOrder.DistrictId = '" + distid + "' and DPY_TranportOrder.IssueCenter = '" + sid + "'";

        SqlCommand cmd = new SqlCommand(qry, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblTransName.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();

            lblIssueCenter.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();

            lblallotmonth.Text = ds.Tables[0].Rows[0]["AllotMonth"].ToString();

            lblper.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();

            Label1.Text = ds.Tables[0].Rows[0]["dDate"].ToString();

            lbldist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();

            Label3.Text = ds.Tables[0].Rows[0]["dDate"].ToString();

            lbldistr.Text = ds.Tables[0].Rows[0]["district_name"].ToString();

            lblissue.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();

            lbltranorder.Text = ds.Tables[0].Rows[0]["TransportOrder"].ToString();
        }

    }
}
