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

public partial class Commodity_Master : System.Web.UI.Page

{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    Districts DObj = null;
    DistributionCenters distobj = null;
    Transporter tobj = null;
    protected Common ComObj = null, cmn = null;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    public string distid = "";
    public string stid = "";
    string roqty = null;
    MoveChallan mobj = null;
    public string getdatef = "";
    DataTable dt = new DataTable();
    float disqty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

        if (Session["st_id"] != null)
        {
            stid = Session["st_id"].ToString();





            if (!IsPostBack)
            {


                GetCommodity();
                GetCommodityCS();



            }

        }
        else
        {

            Response.Redirect("~/MainLogin.aspx");
        }

    }



    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll(" order by Commodity_Name  desc");
        GridView1.DataSource = ds;
        GridView1.DataBind();
       

    }
    void GetCommodityCS()
    {
        comdtobj = new Commodity_MP(ComObj);
        string comd = "SELECT * FROM dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name  desc";
        DataSet ds = comdtobj.selectAny(comd); 
        GridView2.DataSource = ds;
        GridView2.DataBind();

        GridView2.Columns[1].Visible = false;
    }


    float CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        GridView2.Columns[1].Visible = true;
         if (GridView2.Rows.Count == 0)
        {
        }
        else
        {

            foreach (GridViewRow gr in GridView2.Rows)
            {
                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

                if (GchkBx.Checked == true)
                {
                    string cmdtid = gr.Cells[1].Text;

                    string updcmdty = "Update dbo.tbl_MetaData_STORAGE_COMMODITY set Status='Y' where Commodity_ID='" + cmdtid + "'";
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = updcmdty;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Commodity Added Successfully...'); </script> ");
                        GchkBx.Checked = false;
                        GetCommodity();

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        con.Close();
                    }



                }
            }
        }
        GridView2.Columns[1].Visible = false;
    }
       

    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/State/State_Welcome.aspx");
    }
    protected void ddlcommodityd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnremove_Click(object sender, EventArgs e)
    {
        GridView2.Columns[1].Visible = true;
        if (GridView2.Rows.Count == 0)
        {
        }
        else
        {

            foreach (GridViewRow gr in GridView2.Rows)
            {
                CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

                if (GchkBx.Checked == true)
                {
                    string cmdtid = gr.Cells[1].Text;

                    string updcmdty = "Update dbo.tbl_MetaData_STORAGE_COMMODITY set Status='N' where Commodity_ID='" + cmdtid + "'";
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = updcmdty;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Commodity Removed Successfully...'); </script> ");
                        GchkBx.Checked = false;
                        GetCommodity();

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        con.Close();
                    }



                }
            }
        }
        GridView2.Columns[1].Visible = false;
    }
    public void FooterPagerClickCS(object sender, CommandEventArgs e)
    {
        if (GridView1.PageCount == 0)
        {
        }
        else
        {
            //Used by external paging
            string arg;
            arg = e.CommandArgument.ToString();

            switch (arg)
            {
                case "next":
                    //The next Button was Clicked
                    if ((GridView1.PageIndex < (GridView1.PageCount - 1)))
                    {
                        GridView1.PageIndex += 1;
                    }

                    break;

                case "prev":
                    //The prev button was clicked
                    if ((GridView1.PageIndex > 0))
                    {
                        GridView1.PageIndex -= 1;
                    }

                    break;

                case "last":
                    //The Last Page button was clicked
                    GridView1.PageIndex = (GridView1.PageCount - 1);
                    break;

                default:
                    //The First Page button was clicked
                    GridView1.PageIndex = Convert.ToInt32(arg);
                    break;
            }
            GetCommodity();
        }
    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        if (GridView2.PageCount == 0)
        {
        }
        else
        {
            //Used by external paging
            string arg;
            arg = e.CommandArgument.ToString();

            switch (arg)
            {
                case "next":
                    //The next Button was Clicked
                    if ((GridView2.PageIndex < (GridView2.PageCount - 1)))
                    {
                        GridView2.PageIndex += 1;
                    }

                    break;

                case "prev":
                    //The prev button was clicked
                    if ((GridView2.PageIndex > 0))
                    {
                        GridView2.PageIndex -= 1;
                    }

                    break;

                case "last":
                    //The Last Page button was clicked
                    GridView2.PageIndex = (GridView2.PageCount - 1);
                    break;

                default:
                    //The First Page button was clicked
                    GridView2.PageIndex = Convert.ToInt32(arg);
                    break;
            }
            GetCommodityCS();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
}
