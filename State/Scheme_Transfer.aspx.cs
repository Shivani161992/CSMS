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

public partial class State_Scheme_Transfer : System.Web.UI.Page
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


            txtqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtqty.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
         

            if (!IsPostBack)
            {

                GetScheme();
                GetCommodity();
                GetCommodityT();
                GetSchemeT();
                GetDist();
                GetDistDest();

            }

        }
        else
        {

            Response.Redirect("~/MainLogin.aspx");
        }

    }
    void GetDist()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddldistrict.DataSource = ds.Tables[0];
        ddldistrict.DataTextField = "district_name";
        ddldistrict.DataValueField = "District_Code";

        ddldistrict.DataBind();
        ddldistrict.Items.Insert(0, "--Select--");
    }
    void GetDistDest()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");

        ddldistrictd.DataSource = ds.Tables[0];
        ddldistrictd.DataTextField = "district_name";
        ddldistrictd.DataValueField = "District_Code";

        ddldistrictd.DataBind();
        ddldistrictd.Items.Insert(0, "--Select--");
    }
    void GetDCName()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistrict.SelectedValue.ToString() + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlissue.DataSource = ds.Tables[0];
        ddlissue.DataTextField = "DepotName";
        ddlissue.DataValueField = "DepotId";

        ddlissue.DataBind();
        ddlissue.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void GetDCNameDest()
    {

        distobj = new DistributionCenters(ComObj);
        string ord = "Districtid='23" + ddldistrictd.SelectedValue.ToString() + "' order by DepotName";
        DataSet ds = distobj.select(ord);

        ddlissued.DataSource = ds.Tables[0];
        ddlissued.DataTextField = "DepotName";
        ddlissued.DataValueField = "DepotId";

        ddlissued.DataBind();
        ddlissued.Items.Insert(0, "--Select--");

        // ddDistId.Items.Insert(0, "--चुनिये--");
    }
    void GetCommodity()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet ds = comdtobj.selectAll("order by Commodity_Name  desc");
        ddlcomdty.DataSource = ds.Tables[0];

        ddlcomdty.DataTextField = "Commodity_Name";
        ddlcomdty.DataValueField = "Commodity_Id";
        ddlcomdty.DataBind();
        ddlcomdty.Items.Insert(0, "--Select--");


    }
    void GetCommodityT()
    {
        comdtobj = new Commodity_MP(ComObj);
        DataSet dst = comdtobj.selectAll(" order by Commodity_Name  desc");

        ddlcommodityd.DataSource = dst.Tables[0];
        ddlcommodityd.DataTextField = "Commodity_Name";
        ddlcommodityd.DataValueField = "Commodity_Id";
        ddlcommodityd.DataBind();
        ddlcommodityd.Items.Insert(0, "--Select--");


    }
    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("  order by Scheme_Id");      
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");

    }
    void GetSchemeT()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("  order by Scheme_Id");

        ddlschemetrs.DataSource = ds.Tables[0];
        ddlschemetrs.DataTextField = "Scheme_Name";
        ddlschemetrs.DataValueField = "Scheme_Id";
        ddlschemetrs.DataBind();
        ddlschemetrs.Items.Insert(0, "--Select--");

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
        string mstate = "23";
        string mcomid = ddlcomdty.SelectedValue;

        string mscheme = ddlscheme.SelectedValue;
        string mdscheme = ddlschemetrs.SelectedValue;
        string msdist = ddldistrict.SelectedValue;
        string msissue = ddlissue.SelectedValue;
        string mddist = ddldistrictd.SelectedValue;
        string mdissue = ddlissued.SelectedValue;

        int mmonth = int.Parse(DateTime.Today.Month.ToString());
        int myear = int.Parse(DateTime.Today.Year.ToString());
        float qty = CheckNull(txtqty.Text);
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string udate = "";
        string ddate = "";
        string cdate = DateTime.Today.Date.ToString();

        if (ddlcomdty.SelectedItem.Text == "--Select--" || ddlscheme.SelectedItem.Text == "--Select--" || ddlschemetrs.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please  Select Commodity/Scheme......'); </script> ");
        }
        else
        {


            string qrey = "insert into dbo.State_Scheme_Transfer(State_Id,District_Id,Depotid,Commodity_Id,S_Scheme_Id,D_District,D_Depot,D_Scheme_Id,Quantity,Month,Year,CreatedDate,UpdatedDate,DeletedDate,IP_Address) values('" + mstate + "','" + msdist + "','" + msissue + "','" + mcomid + "','" + mscheme + "','" + mddist + "','" + mdissue +"','" + mdscheme +"'," + qty + "," + mmonth + "," + myear + ",getdate(),'" + udate + "','" + ddate + "','" + ip + "')";
            cmd.CommandText = qrey;
            cmd.Connection = con;
            con.Open();
            SqlTransaction trns;
            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            cmd.Transaction = trns;
            try
            {
                if (qty == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity Should not be 0 ......'); </script> ");

                }
                else
                {
                    if (ddlscheme.SelectedValue == ddlschemetrs.SelectedValue)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Source and Destination Scheme Should not be Same ......'); </script> ");
                    }
                    else
                    {
                      
                        cmd.ExecuteNonQuery();
                      
                        float bqty = CheckNull(txtbalqty.Text);
                        float tqty = CheckNull(txtqty.Text);
                        float uqty = (bqty - tqty);
                        string uqry = "Update dbo.issue_opening_balance  set Current_Balance=" + uqty + " where District_Id='" + msdist  + "'and Depotid='" + msissue  + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "'";
                        cmd.CommandText = uqry;
                        cmd.Connection = con;
                        cmd.Transaction = trns;
                        cmd.ExecuteNonQuery();
                        //txtbalqty.ReadOnly = false;
                        //txtbalqty.Text = uqty.ToString ();
                     

                        float bqtyd = CheckNull(txtbalqtyd.Text);
                        float tqtyd = CheckNull(txtqty.Text);
                        float uqtyd = (bqtyd + tqtyd);

                        string uqryd = "Update dbo.issue_opening_balance  set Current_Balance=" + uqtyd + "where District_Id='" + mddist  + "'and Depotid='" + mdissue  + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mdscheme + "'";
                        cmd.CommandText = uqryd;
                        cmd.Connection = con;
                        cmd.Transaction = trns;
                        cmd.ExecuteNonQuery();

                       
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Transfer Succesfully ......'); </script> ");
                        txtbalqty.ReadOnly = false;
                        txtbalqty.Text = uqty.ToString();
                        txtbalqtyd.ReadOnly = false;
                        txtbalqtyd.Text = uqtyd.ToString();



                        btnsubmit.Enabled = false;
                    }
                }
                trns.Commit();
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
    }
    protected void ddldistric_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCName();
    }
    protected void ddldistrictd_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDCNameDest();
    }
    protected void ddlissue_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBalance();
    }
    protected void ddlschemetrs_SelectedIndexChanged(object sender, EventArgs e)
    {
        string distid = ddldistrictd.SelectedValue; ;
        string issueid = ddlissued.SelectedValue;
        string mcomid = ddlcommodityd.SelectedValue;
        string mscheme = ddlschemetrs.SelectedValue;
        comdtobj = new Commodity_MP(ComObj);
        string qry = "Select Round(Sum(Current_Balance),5) as Current_Balance  from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "'";
        DataSet ds = comdtobj.selectAny(qry);

         if (ds.Tables[0].Rows.Count==0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  comodity....'); </script> ");
            lblbalqtyd.Visible = false;
            txtbalqtyd.Visible = false;
            Label2.Visible = false;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtbalqtyd.Text = dr["Current_Balance"].ToString();
            lblbalqtyd.Visible = true;
            txtbalqtyd.Visible = true;
            Label2.Visible = true;
            txtbalqtyd.BackColor = System.Drawing.Color.Wheat;
            txtbalqtyd.ReadOnly = true;
        }
    }
    void GetBalance()
    {
        string distid = ddldistrict.SelectedValue;
        string issueid = ddlissue.SelectedValue;
        string mcomid = ddlcomdty.SelectedValue;
        string mscheme = ddlscheme.SelectedValue;
        comdtobj = new Commodity_MP(ComObj);
         string qry = "Select Round(Sum(Current_Balance),5) as Current_Balance  from dbo.issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "'and Commodity_Id='" + mcomid + "'and Scheme_Id='" + mscheme + "'";
         DataSet ds = comdtobj.selectAny(qry);

         if (ds.Tables[0].Rows.Count==0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There is No opening for selected  comodity....'); </script> ");
            lblbalqty.Visible = false;
            txtbalqty.Visible = false;
            Label2.Visible = false;
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtbalqty.Text = dr["Current_Balance"].ToString();
            lblbalqty . Visible = true;
            txtbalqty.Visible = true;
            Label2.Visible = true;
            txtbalqty.BackColor = System.Drawing.Color.Wheat;
            txtbalqty.ReadOnly = true;
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ComObj.CloseConnection();
        Response.Redirect("~/State/State_Welcome.aspx");
    }
    protected void ddlcommodityd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
