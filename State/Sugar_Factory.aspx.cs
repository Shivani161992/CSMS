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

public partial class State_Sugar_Factory : System.Web.UI.Page


{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    chksql chk = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string stid = "";
    public string tid = "";
    public string tname = "";
    public string bankid = "";
    public string bid = "";
    public int banknum;
    public int placeidnum;
    public string placeid = "";
    public int factidnum;
    public string factid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            stid = Session["st_id"].ToString();

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            txtplace.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtplace.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtplace.Attributes.Add("onchange", "return chksqltxt(this)");

            txtplacename.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtplacename.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtplacename.Attributes.Add("onchange", "return chksqltxt(this)");

            chk = new chksql();
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtplace.Text);
            ctrllist.Add(txtplacename.Text);
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
               // fillgrid();
                GetState();
                
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }
    void GetState()
    {
        mobj = new MoveChallan(ComObj);
        string qryms = "select *  from dbo.State_Master order by State_Name ";
        DataSet dsms = mobj.selectAny(qryms);
        if (dsms == null)
        {
        }
        else
        {

            ddlstate.DataSource = dsms.Tables[0];
            ddlstate.DataTextField = "State_Name";
            ddlstate.DataValueField = "State_Code";
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, "--Select--");
        }

    }
    void GetPlace()
    {
        string st=ddlstate.SelectedValue ;
        mobj = new MoveChallan(ComObj);
        string qryms = "select distinct(Place_code) as Place_code,Place_Name  from dbo.Sugar_Places where State_code=" + st + " order by Place_name";
        DataSet dsms = mobj.selectAny(qryms);
        if (dsms == null)
        {
        }
        else
        {

            ddlplace.DataSource = dsms.Tables[0];
            ddlplace.DataTextField = "Place_Name";
            ddlplace.DataValueField = "Place_Code";
            ddlplace.DataBind();
            ddlplace.Items.Insert(0, "--Select--");
            ddlplace.Items.Insert(1, "New Place");
        }

    }
  
   
    protected void btnadd_Click(object sender, EventArgs e)
    {
          if (txtplace.Text == "New Place")
        {
            string plid = txtplid.Text;
            mobj = new MoveChallan(ComObj);
            string sid = ddlstate.SelectedValue;
            string qrey = "select max(Factory_Code) as Factory_Code  from dbo.Sugar_Places where State_code='" + sid + "'";
            DataSet ds = mobj.selectAny(qrey);
            if (ds == null)
            {

            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                factid = dr["Factory_Code"].ToString();
                if (factid == "")
                {

                    factid = "1";

                }
                else
                {
                    factidnum = Convert.ToInt32(placeid);
                    factidnum = factidnum + 1;
                    factid = factidnum.ToString();


                }
            }

        }
        else
        {
            string plid = txtplid.Text;
            mobj = new MoveChallan(ComObj);
            string sid = ddlstate.SelectedValue;
            string qrey = "select max(Factory_Code) as Factory_Code  from dbo.Sugar_Places where State_code='" + sid + "'and Place_code='" + plid + "'";
            DataSet ds = mobj.selectAny(qrey);
            if (ds == null)
            {

            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                factid = dr["Factory_Code"].ToString();
                if (factid == "")
                {

                    factid = "1";

                }
                else
                {
                    factidnum = Convert.ToInt32(factid);
                    factidnum = factidnum + 1;
                    factid = factidnum.ToString();


                }
            }
            
        }
        string qreyd = "select *  from dbo.Sugar_Places where State_code='" + ddlstate.SelectedValue + "' and Place_Code='" + ddlplace.SelectedValue + "' and Factory_Name='" + txtplacename.Text.Trim() + "'";
        DataSet dsd = mobj.selectAny(qreyd);
            if (dsd == null)
            {

            }
            else
            {

                if (dsd.Tables[0].Rows.Count == 0)
                {
                    string status = "Y";
                    string mplacename = txtplace.Text;
                    string mfname = txtplacename.Text;

                    string mplaceid = txtplid.Text;
                    string mstate = ddlstate.SelectedValue;

                    string insert = "insert into dbo.Sugar_Places(State_code,Factory_Code,Factory_Name,Place_code,Place_name,Status)values('" + mstate + "'," + factid + ",'" + mfname + "'," + mplaceid + ",'" + mplacename + "','" + status + "')";
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
                        trns.Rollback();
                        Label1.Visible = true;
                        Label1.Text = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                        ComObj.CloseConnection();
                    }


                }



               
                else
                {
                    
                    
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Factory Name Already Exist.....'); </script> ");
                }

                // fillgrid();

            }

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        
        
        ComObj.CloseConnection();
        Response.Redirect("~/State/State_Welcome.aspx");
    }   
   
   
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //GridView1.Columns[1].Visible =true ;
        //string pc_id = GridView1.SelectedRow.Cells[1].Text;
        //string pcname = txtplacename.Text;
        //string Update = "Update dbo.tbl_MetaData_Purchase_Center set PC_Name='" + pcname + "'where District_Code='" + distid + "' and PC_ID='" + pc_id + "'";
        //cmd.Connection = con;
        //cmd.CommandText = Update;
        //try
        //{
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully.....'); </script> ");
        //    btnadd.Enabled = false;

        //}
        //catch (Exception ex)
        //{
        //    Label1.Text = ex.Message;
        //}
        //finally
        //{
        //    con.Close();
        //    ComObj.CloseConnection();
        //}
        //GridView1.Columns[1].Visible = false;

    }
    void GetPlaceID()
    {
        mobj = new MoveChallan(ComObj);
        string sid = ddlstate.SelectedValue;
        string qrey = "select max(Place_code) as Place_code  from dbo.Sugar_Places where State_code='" + sid + "'";
        DataSet ds = mobj.selectAny(qrey);
        if (ds == null)
        {

        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            placeid = dr["Place_code"].ToString();
            if (placeid == "")
            {

                placeid = "1";

            }
            else
            {
                placeidnum = Convert.ToInt32(placeid);
                placeidnum = placeidnum + 1;
                placeid = placeidnum.ToString();


            }
        }
        txtplid.Text = placeid.ToString();
    }
    protected void ddlplace_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlplace.SelectedValue=="New Place")
        {
            lblplace.Visible = true;
            txtplace.Visible = true;
            txtplace.Text = "";
            txtplace.Focus();

        }
        else
        {
            txtplid.Text = ddlplace.SelectedValue;
            txtplace.Text = ddlplace.SelectedItem.Text;
            lblplace.Visible = false;
            txtplace.Visible = false;
        }
        bindlist();
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPlaceID();
        GetPlace();
    }
    void bindlist()
    {

    
        string st=ddlstate.SelectedValue ;
        string plid = ddlplace.SelectedValue;
        mobj = new MoveChallan(ComObj);
        string qryms = "select distinct(Factory_Code) as Factory_Code,Factory_Name  from dbo.Sugar_Places where State_code=" + st + " and Place_code=" + plid + " order by Factory_Name ";
        DataSet dsms = mobj.selectAny(qryms);
        if (dsms == null)
        {
        }
        else
        {
            ListBox1.Items.Clear();
            ListBox1.DataSource = dsms.Tables[0];
            ListBox1.DataTextField = "Factory_Name";
            ListBox1.DataValueField = "Factory_Code";
            ListBox1.DataBind();
            
        }

    }
    

}
