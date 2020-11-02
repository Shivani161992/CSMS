using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
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

public partial class District_PC_Master : System.Web.UI.Page

{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string tid = "";
    public string tname = "";
    public string bankid = "";
    public string bid = "";
    public int banknum;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();



            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            if (Page.IsPostBack == false)
            {
                fillgrid();

            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }
    public void FooterPagerClick(object sender, CommandEventArgs e)
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
            fillgrid();
        }
    }
    void fillgrid()
    {
        mobj = new MoveChallan(ComObj);
        string qrey = "select  * from dbo.tbl_MetaData_Purchase_Center where DistrictId ='23" + distid + "'";
        DataSet ds = mobj.selectAny(qrey);
         if (ds==null)
        {
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            GridView1.Columns[1].Visible = false;
        }


    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        string stid = "23";
        mobj = new MoveChallan(ComObj);
        string qrey = "select max(PcId) as PC_ID from dbo.tbl_MetaData_Purchase_Center where DistrictId='23" + distid + "'";
        DataSet ds = mobj.selectAny(qrey);
        DataRow dr = ds.Tables[0].Rows[0];
        bankid = dr["PC_ID"].ToString();
        if (bankid == "")
        {
            


        }
        else
        {
            banknum = Convert.ToInt32(bankid);
            banknum = banknum + 1;
            bankid = banknum.ToString();


        }


        string mpcname=txtpcname.Text ;
        //string mbadd=txtbadds .Text;
        string remark = "";
        string insert = "insert into dbo.tbl_MetaData_Purchase_Center(District_Code,PC_ID,PC_Name,Remarks)values('" + distid + "','" + bankid + "','" + mpcname + "','" + remark + "')";
                cmd.Connection = con;
                cmd.CommandText = insert;
                con.Open();
                SqlTransaction trns;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                try
                {
                    
                    cmd.Transaction = trns;
                    cmd.ExecuteNonQuery();
                    trns.Commit();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted Successfully.....'); </script> ");
                    btnadd.Enabled = false;

                }
                catch (Exception ex)
                {
                   trns.Rollback ();
                   Label1.Visible = true;
                    Label1.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                    ComObj.CloseConnection();
                }





                fillgrid();
      


    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.Columns[1].Visible = true ;
        bid = GridView1.SelectedRow.Cells[1].Text;
        txtpcname.Text = GridView1.SelectedRow.Cells[2].Text;

        btnadd.Visible = false;
        btnUpdate.Visible = true;
        GridView1.Columns[1].Visible = false;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
               


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        GridView1.Columns[1].Visible =true ;
        string pc_id = GridView1.SelectedRow.Cells[1].Text;
        string pcname = txtpcname.Text;
        string Update = "Update dbo.tbl_MetaData_Purchase_Center set PC_Name='" + pcname + "'where District_Code='" + distid + "' and PC_ID='" + pc_id + "'";
        cmd.Connection = con;
        cmd.CommandText = Update;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully.....'); </script> ");
            btnadd.Enabled = false;

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
        GridView1.Columns[1].Visible = false;

    }
  
}
