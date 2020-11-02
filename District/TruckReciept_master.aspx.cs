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
public partial class IssueCenter_Godown_master : System.Web.UI.Page
{
    Transporter tobj = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    string distid = "";
    string issueid = "";
    DataTable dt = new DataTable();
    protected Common ComObj = null, cmn = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (Page.IsPostBack == false)
            {
                Fillgrid();

            }
            

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");

        }



    }
    void Fillgrid()
    {
        tobj = new Transporter(ComObj);
        string query = "select Godown_ID,Godown_Capacity from dbo.tbl_MetaData_GODOWN where DepotID='" + issueid + "'";
        DataSet ds = tobj.selectAny(query);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        Button1.Visible = false;


    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        string mgid = txtgname.Text;
        int mgcap = int.Parse(txtcapacty.Text);
        string mcdate = DateTime.Today.Date.ToString();
        string mudate = "";
        string mcby = issueid;
        string muby = "";
        string mgfdate = DateTime.Today.Date.ToString();
        string mgudate = "";
        string mddate = "";
        string mdby = "";
        string mremarks = "";
        string mstate = "23";

        string qyr = "insert into dbo.tbl_MetaData_GODOWN(Godown_ID,StateId,DistrictId,DepotId,Godown_Name,Godown_Formation_Date,Godown_Updation_Date,Godown_Capacity,Remarks,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,DeletedBy,DeletedDate)values('" + mgid + "','" + mstate + "','" + distid + "','" + issueid + "','" + mgid + "','" + mgfdate + "','" + mgudate + "'," + mgcap + ",'" + mremarks + "','" + mdby + "',getdate(),'" + muby + "','" + mudate + "','" + mdby + "','" + mddate + "'" + ")";
        
        cmd.Connection = con;
        cmd.CommandText = qyr;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;

        }
        finally
        {
            con.Close();
            ComObj.CloseConnection();

        }
        con.Open();
        

    }
    protected void btnaddnew_Click1(object sender, EventArgs e)
    {

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
