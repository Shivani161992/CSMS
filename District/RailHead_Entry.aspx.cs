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

public partial class DistrictFood_RailHead_Entry : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    public string distid = "";
    public string tid = "";
    public string hname = "";
    chksql chk = null;
    public string railhead = "";
    public int railnum;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

            txtrailhead.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtrailhead.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrailhead.Attributes.Add("onchange", "return chksqltxt(this)");

            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            ArrayList ctrllist = new ArrayList();
            ctrllist.Add(txtrailhead.Text);
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


            if (!IsPostBack)
            {

                GetName();
                fillgrid();
            }


        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }


    }
    void GetName()
    {
        mobj = new MoveChallan(ComObj);
        string qryd = "select district_name  from pds.districtsmp where district_code='" + distid + "'";
        DataSet dsd = mobj.selectAny(qryd);
        DataRow drd = dsd.Tables[0].Rows[0];
        txtdistrict.Text = drd["district_name"].ToString();
        txtdistrict.ReadOnly = true;
        txtrailhead.Focus();


    }
    void fillgrid()
    {
        con.Open();
        //mobj = new MoveChallan(ComObj);
        string qrey = "select  RailHead_Code , RailHead_Name from dbo.tbl_Rail_Head where district_code='" + distid + "'";
        da = new SqlDataAdapter(qrey, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        //DataSet ds = mobj.selectAny(qrey);

         if (ds.Tables[0].Rows.Count==0)
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
        string state = Session["State_Id"].ToString();
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string opid = Session["OperatorIDDM"].ToString();

                mobj = new MoveChallan(ComObj);
                string qrey = "select max(RailHead_Code) as RailHead_Code  from dbo.tbl_Rail_Head where district_code='" + distid + "'";
                DataSet ds = mobj.selectAny(qrey);
                DataRow dr = ds.Tables[0].Rows[0];
                railhead  = dr["RailHead_Code"].ToString();
                if (railhead  == "")
                {
                    railhead  = distid + "01";

                }
                else
                {
                    railnum = Convert.ToInt32(railhead);
                    railnum = railnum + 1;
                    railhead = railnum.ToString();

               }




        string crdate=DateTime.Today.Date.ToString ();
        string udate="";
        string mrhname=txtrailhead.Text;
        string qryinsert = "Insert into  dbo.tbl_Rail_Head(State_Id,district_code,RailHead_Code,RailHead_Name,Created_Date,Updated_Date,IP,OperatorID)values('" + state +"','"+ distid + "','" + railhead + "','" + mrhname + "',getdate(),'" + udate + "','" + ip + "','"  + opid + "')";
                cmd.Connection = con;
                cmd.CommandText = qryinsert;
                con.Open();
                SqlTransaction trns;
                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                cmd.Transaction = trns;
                try
                {                    
                    cmd.ExecuteNonQuery();
                    trns.Commit();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved  Successfully.....'); </script> ");
                    btnadd.Enabled = false;

                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    Label1.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                    ComObj.CloseConnection();
                }



            }






    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string crdate = "";
        string udate = DateTime.Today.Date.ToString();
        string mrhname = txtrailhead.Text;

        string qryupdate = "Update dbo.tbl_Rail_Head  set RailHead_Name ='" + mrhname + "',Updated_Date='" + udate + "'  where RailHead_Code='" + GridView1.SelectedRow.Cells[1].Text.Trim() + "' and district_code='" + distid + "'";
        cmd.Connection = con;
        cmd.CommandText = qryupdate;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully.....'); </script> ");
            btnadd.Visible = true;
            btnupdate.Visible = false;
            txtrailhead.Text = "";
            fillgrid();

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }

       
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.SelectedRow.BackColor = System.Drawing.Color.Wheat;
        txtrailhead.BackColor = System.Drawing.Color.Wheat;
        hname = GridView1.SelectedRow.Cells[1].Text.Trim();
        txtrailhead.Text= GridView1.SelectedRow.Cells[2].Text.Trim();
        txtrailhead.Focus();
        btnadd.Visible = false;
        btnupdate.Visible = true;
       
       


        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
}
