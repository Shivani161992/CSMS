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
using Data;
using DataAccess;
public partial class edit_do_adm : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;    
    protected Common ComObj = null, cmn = null;
    string distid = "";
    string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dist_id"] == null)
            {
                Response.Redirect("~/Session_Expire_Dist.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }     
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        SqlDataAdapter da = new SqlDataAdapter("SELECT delivery_order_mpscsc.district_code,delivery_order_mpscsc.issueCentre_code,delivery_order_mpscsc.delivery_order_no,  delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year, delivery_order_mpscsc.quantity,  Sum( issue_against_do.qty_issue)as Lifted_QTY FROM          delivery_order_mpscsc  INNER JOIN  issue_against_do ON delivery_order_mpscsc.delivery_order_no = issue_against_do.delivery_order_no AND   delivery_order_mpscsc.district_code = issue_against_do.district_code AND    delivery_order_mpscsc.issueCentre_code = issue_against_do.issueCentre_code AND      delivery_order_mpscsc.allotment_month = issue_against_do.allotment_month AND        delivery_order_mpscsc.allotment_year = issue_against_do.allotment_year    group by delivery_order_mpscsc.delivery_order_no,delivery_order_mpscsc.quantity,issue_against_do.qty_issue,delivery_order_mpscsc.district_code,delivery_order_mpscsc.issueCentre_code,delivery_order_mpscsc.allotment_month,delivery_order_mpscsc.allotment_year having Sum( issue_against_do.qty_issue)>delivery_order_mpscsc.quantity", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "do_fps");
        GridView3.DataSource = ds.Tables["do_fps"];
        GridView3.DataBind();
    }

    protected void get_do_data()
    {
        save.Enabled = true;
        SqlDataAdapter da = new SqlDataAdapter("SELECT issue_against_do.trans_id, issue_against_do.delivery_order_no, issue_against_do.district_code,issue_against_do.issueCentre_code, issue_against_do.allotment_month, issue_against_do.allotment_year,issue_against_do.issue_to, CONVERT(decimal(18, 5), issue_against_do.qty_issue) AS qty_issue, issue_against_do.bags, issue_against_do.Source,issue_against_do.Godown, issue_against_do.issue_date, issue_against_do.gate_pass, issue_against_do.created_date,  issue_against_do.updated_date, issue_against_do.swc_status, issue_against_do.ip_add,delivery_order_mpscsc.commodity_id, delivery_order_mpscsc.scheme_id, delivery_order_mpscsc.quantity FROM   issue_against_do INNER JOIN   delivery_order_mpscsc ON issue_against_do.delivery_order_no = delivery_order_mpscsc.delivery_order_no AND    issue_against_do.district_code = delivery_order_mpscsc.district_code AND    issue_against_do.issueCentre_code = delivery_order_mpscsc.issueCentre_code AND       issue_against_do.allotment_month = delivery_order_mpscsc.allotment_month AND    issue_against_do.allotment_year = delivery_order_mpscsc.allotment_year where issue_against_do.delivery_order_no='" + tx_dono.Text.Trim() + "' and issue_against_do.issueCentre_code='" + tx_issue.Text.Trim() + "'", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "do_fps");
        GridView1.DataSource = ds.Tables["do_fps"];
        GridView1.DataBind();
        SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM   Issued_do_fps where delivery_order_no='" + tx_dono.Text.Trim() + "' and issueCentre_code='" + tx_issue.Text.Trim() + "'", con);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "do_fps1");
        GridView2.DataSource = ds1.Tables["do_fps1"];
        GridView2.DataBind();
    }
    private void GetSelected()
    {        
        foreach (GridViewRow di in GridView1.Rows)
        {           
            string dist = "";
            string issue_centre_code = "";
            string comm = "";
            string scheme = "";
            string dono = "";
            string soa = "";
            string godown = "";
            string transid = "";
            decimal qty = 0;
            int bags = 0;
            HtmlInputCheckBox chkBx = (HtmlInputCheckBox)di.FindControl("chkBoxId");            
            if (chkBx != null && chkBx.Checked)
            {
                transid = di.Cells[1].Text; 
                dono  = di.Cells[2].Text;
                dist = di.Cells[3].Text;
                issue_centre_code = di.Cells[4].Text;
                comm = di.Cells[18].Text;
                scheme = di.Cells[19].Text;
                soa = di.Cells[10].Text;
                godown = di.Cells[11].Text;
                qty = decimal.Parse(di.Cells[8].Text);
                bags = int.Parse(di.Cells[9].Text);
                 //////////////////////////////////////////////////
                string strr = "";                
                strr = "update dbo.tbl_Stock_Registor set Sale_Do=round(convert(decimal(18,5),Sale_Do)+" + qty  + ",5) where Commodity_Id ='" + comm  + "' and Scheme_Id ='" + scheme  + "' and DistrictId='" + dist + "'and DepotID='" + issue_centre_code + "'and Month=" + DateTime.Today.Month + "and Year=" + DateTime.Today.Year;
                cmd.CommandText = strr;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                strr = "Update dbo.issue_opening_balance set Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + qty  + ",5),Current_Bags=Current_Bags-" + bags  + " where District_Id='" + dist + "'and Depotid='" + issue_centre_code + "'and Commodity_Id='" + comm  + "'and Scheme_Id='" + scheme  + "' and Godown='" + godown  + "' and Source='" + soa + "'";
                cmd.CommandText = strr;
                cmd.Connection = con;            
                cmd.ExecuteNonQuery();
                strr = "update dbo.Current_Godown_Position set Current_Bags=Current_Bags-" + bags  + ",Current_Balance=round(convert(decimal(18,5),Current_Balance)-" + qty  + ",5),Current_Capacity=round(convert(decimal(18,5),Current_Capacity)+" + qty  + ",5) where District_Id='" + dist + "' and Depotid='" + sid + "' and Godown='" + godown  + "'";
                cmd.CommandText = strr;
                cmd.Connection = con;               
                cmd.ExecuteNonQuery();
                strr = "delete from issue_against_do where trans_id='"+ transid +"'";
                cmd.CommandText = strr;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }           
    }
    private void GetSelected1()
    {      
        foreach (GridViewRow di in GridView2.Rows)
        {
            string transid ="";
            HtmlInputCheckBox chkBx = (HtmlInputCheckBox)di.FindControl("chkBoxId1");
            if (chkBx != null && chkBx.Checked)
            {               
                //////////////////////////////////////////////////
                transid = di.Cells[1].Text; 
                string strr = "";
                strr = "delete from Issued_do_fps where trans_id='"+ transid +"'";
                cmd.CommandText = strr;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }        
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/edit_do_adm.aspx");
    }
    protected void save_Click(object sender, EventArgs e)
    {
        GetSelected();
        GetSelected1();
        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Updated Successfully ...');</script>");
    }    
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (tx_dono.Text == "")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter DO Number ...');</script>");
        }
        else
        {
            get_do_data();
        }
    }
}
    

