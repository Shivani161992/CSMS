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
public partial class Admin_UpdateReceipt : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string adminid = "";
    public string tid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            adminid = Session["st_id"].ToString();



            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (Page.IsPostBack == false)
            {
               
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        fillgrid(); 
    }
    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qryro = "Select  tbl_receipt_details.Dist_id as Rdist,tbl_receipt_details.RO_no as Rrono,tbl_receipt_details.Receipt_Id ,tbl_receipt_details.Commodity as Rcomm,tbl_receipt_details.Scheme as Rsch,RO_of_FCI.Commodity as ROcomm ,RO_of_FCI.Scheme as ROsch,RO_of_FCI.RO_no as ROrono,RO_of_FCI.Distt_ID as ROdist from RO_of_FCI inner  join tbl_receipt_details on  RO_of_FCI.Distt_Id=tbl_receipt_details.Dist_id and RO_of_FCI.RO_no= tbl_receipt_details.RO_no and  RO_of_FCI.Commodity=tbl_receipt_details.Commodity and RO_of_FCI.Scheme<>tbl_receipt_details.Scheme ";
        DataSet dsro = mobj.selectAny(qryro);
        GridView2.DataSource = dsro.Tables[0];
        GridView2.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView2.Rows)
        {
            CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

            if (GchkBx.Checked == true)
            {
                string recdid = gr.Cells[3].Text;
                string rono = gr.Cells[2].Text;
                string dist = gr.Cells[1].Text;
                string scheme = gr.Cells[9].Text;
                try
                {

                    cmd.Connection = con;
                    string updt = "Update dbo.tbl_receipt_details set Scheme='" + scheme + "'  where Dist_ID='" + dist + "' and Receipt_Id='" + recdid + "'";
                    cmd.CommandText = updt;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception ex)
                {
                    Label1.Visible = true;
                    Label1.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                }



            }
            else
            {
                break;
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        
    }
}
