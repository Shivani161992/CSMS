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

public partial class District_Bank_Master : System.Web.UI.Page
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
    chksql chk = null;
    public int banknum;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["dist_id"] != null)
         {

             txtbankname.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
             txtbankname.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
             txtbankname.Attributes.Add("onchange", "return chksqltxt(this)");

             txtbadds.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
             txtbadds.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
             txtbadds.Attributes.Add("onchange", "return chksqltxt(this)");
            
            distid = Session["dist_id"].ToString();
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtbankname.Text);
            ctrllist.Add(txtbadds.Text);
            if (chk == null)
            {
            }
            else
            {
                bool chkstr = chk.chksql_server(ctrllist);
                if (chkstr == true)
                {
                    Page.Server.Transfer(HttpContext.Current.Request.Path);
                }
            }


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
        string qrey = "select  * from dbo.Bank_Master where district_code='" + distid + "' and issueCenter_code='"+distid +"'";
        DataSet ds = mobj.selectAny(qrey);
         if (ds==null)
        {
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }


    }
    protected void btnadd_Click(object sender, EventArgs e)
    {

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string opid = Session["OperatorIDDM"].ToString();
        string state = Session["State_Id"].ToString();


        mobj = new MoveChallan(ComObj);
        string qrey = "select max(Bank_ID) as Bank_ID from dbo.Bank_Master where District_Code='" + distid + "'and issueCenter_code='" + distid + "'";
        DataSet ds = mobj.selectAny(qrey);
        DataRow dr = ds.Tables[0].Rows[0];
        bankid= dr["Bank_ID"].ToString();
        if (bankid == "")
        {
            bankid = distid  + "2001";


        }
        else
        {
            banknum = Convert.ToInt32(bankid);
            banknum = banknum + 1;
            bankid = banknum.ToString();


        }


        string mbname=txtbankname.Text ;
        string mbadd=txtbadds .Text;
        string remark = "";
        string insert = "insert into dbo.Bank_Master(State_Id,District_Code,issueCenter_code,Bank_ID,Bank_Name,Remarks,IP,OperatorID)values('" +state +"','"+ distid + "','" + distid + "','" + bankid + "','" + mbname + "','" + remark + "','"+ ip+"','"+ opid +"')";
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
        bid = GridView1.SelectedRow.Cells[1].Text;
        txtbankname.Text = GridView1.SelectedRow.Cells[2].Text;

        btnadd.Visible = false;
        btnUpdate.Visible = true;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
               


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string bank_id = GridView1.SelectedRow.Cells[1].Text;
        string bname = txtbankname.Text;
        string Update = "Update dbo.Bank_Master set Bank_Name='" + bname + "'where District_Code='" + distid + "' and issueCenter_code='" + distid  + "'and Bank_ID='" + bank_id + "'";
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

    }
}
