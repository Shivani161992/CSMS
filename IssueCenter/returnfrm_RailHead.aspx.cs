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

public partial class IssueCenter_returnfrm_RailHead : System.Web.UI.Page
{
    SqlDataAdapter da;
    DataSet ds;

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    string distid = "", GetFirstfour = "", result = "";
    int length = 0;

    string issueid = "";

    SqlCommand cmd = new SqlCommand();
    chksql chk = null;
    SqlDataReader dr;
    Commodity_MP comdtobj = null;
    Scheme_MP schobj = null;
    Transporter tobj = null;
    MoveChallan mobj1 = null;
    MoveChallan mobj = null;
    protected Common ComObj = null, cmn = null;
    DistributionCenters distobj = null;
    Districts DObj = null;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["issue_id"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            distid = Session["dist_id"].ToString();
            issueid = Session["issue_id"].ToString();


            txtbagno.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtquant.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

            txtremark.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtremark.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtremark.Attributes.Add("onchange", "return chksqltxt(this)");

            txttruckno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttruckno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttruckno.Attributes.Add("onchange", "return chksqltxt(this)");

            txttrukchlnno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txttrukchlnno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txttrukchlnno.Attributes.Add("onchange", "return chksqltxt(this)");

            txtbagno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtbagno.Attributes.Add("onchange", "return chksqltxt(this)");

            txtquant.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtquant.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate1.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate1.Attributes.Add("onchange", "return chksqltxt(this)");


            if (!IsPostBack)
            {
                ddlcropyear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
                ddlcropyear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
                ddlcropyear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));

                ddlcropyear.SelectedIndex = 1;

                GetScheme();
                GetCommodity();
                GetGodown();
                GetRack();
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
        DataSet ds = comdtobj.selectAll("order by Commodity_Name desc");
        ddlcommodity.DataSource = ds.Tables[0];
        ddlcommodity.DataTextField = "Commodity_Name";
        ddlcommodity.DataValueField = "Commodity_Id";
        ddlcommodity.DataBind();
        ddlcommodity.Items.Insert(0, "--Select--");
    }

    void GetGodown()
    {
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + distid + "' and DepotId='" + issueid + "' order by Godown_ID";
        DataSet ds = mobj.selectAny(qry);
        ddlgodown.DataSource = ds.Tables[0];
        ddlgodown.DataTextField = "Godown_Name";
        ddlgodown.DataValueField = "Godown_ID";
        ddlgodown.DataBind();
        ddlgodown.Items.Insert(0, "--Select--");
    }

    void GetScheme()
    {

        schobj = new Scheme_MP(ComObj);
        DataSet ds = schobj.selectAll("order by displayorder");
        ddlscheme.DataSource = ds.Tables[0];
        ddlscheme.DataTextField = "Scheme_Name";
        ddlscheme.DataValueField = "Scheme_Id";
        ddlscheme.DataBind();
        ddlscheme.Items.Insert(0, "--Select--");
    }

    void GetRack()
    {
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());

        string dist = distid;
        ddlrackno.Items.Insert(0, "--Select--");
        // string qreyrac = "select Rack_No  from dbo.tbl_RackMaster where district_code='" + dist + "'";

        string qreyrac = "(select Rack_No from dbo.tbl_RackMaster where district_code='" + dist + "' and (DATEADD(DAY,30,Created_Date))>=Getdate()" +
           " Union All " +
           " SELECT distinct Rack_No FROM DeliveryChallan_MO where Rack_No is not Null and Rack_No!='00' and FrmDist='" + dist + "' and ModeofDispatch='13' and (DATEADD(DAY,30,CreatedDate))>=Getdate()) order by Rack_No";

        cmd.Connection = con;
        cmd.CommandText = qreyrac;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddlrackno.Items.Add(dr["Rack_No"].ToString());
        }
        dr.Close();

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect(Request.Url.AbsoluteUri);

        //DaintyDate1.Text = "";

        //GetGodown();

        //txtbagno.Text = "";

        //txtquant.Text = "";

        //txttrukchlnno.Text = "";

        //txttrukchlnno.Text = "";

        //ddlcropyear.SelectedIndex = 0;

        //txtremark.Text = "";

        //btnsubmit.Enabled = true;

        //ddlrackno.SelectedValue = "0";

        //ddlcommodity.SelectedValue = "0";

        //ddlscheme.SelectedValue = "0";

        //txttruckno.Text = "";

        //txtremark.Text = "";

        //Label11.Text = "";
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlrackno.SelectedValue == "0" || ddlrackno.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Rack Number....'); </script> ");
            return;
        }


        if (ddlcommodity.SelectedValue == "0" || ddlcommodity.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Commodity Type....'); </script> ");
            return;
        }

        if (ddlgodown.SelectedValue == "0" || ddlgodown.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Godown Name....'); </script> ");
            return;
        }

        if (ddlcropyear.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Crop Year..'); </script> ");
            return;
        }

        if (txtbagno.Text == "0" || txtbagno.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Bags Should not be 0 or Blank..'); </script> ");
            return;
        }

        if (txtquant.Text == "0" || txtquant.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Quantity Should not be 0 or Blank..'); </script> ");
            return;
        }

        if (txttruckno.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Truck Number..'); </script> ");
            return;
        }

        //if (txttrukchlnno.Text == "")
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Challan Number.'); </script> ");
        //    return;
        //}

        if (DaintyDate1.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Receiving Date.'); </script> ");
            return;
        }

        if (ddlChallanNo.Visible == true)
        {
            if (ddlChallanNo.SelectedValue == "0" || ddlChallanNo.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Challan Number'); </script> ");
                return;
            }
            else
            {
                txttrukchlnno.Text = ddlChallanNo.SelectedItem.ToString();
            }
        }
        else
        {
            if (txttrukchlnno.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Challan Number.'); </script> ");
                return;
            }
        }

        string duplichallan = "Select DC_MO from Return_from_Rack where DC_MO = '" + txttrukchlnno.Text.Trim() + "' and Issue_Center = '" + issueid + "' and FrmDist = '" + distid + "' ";
        SqlCommand cmdchallan = new SqlCommand(duplichallan, con);
        SqlDataReader drchallan;

        drchallan = cmdchallan.ExecuteReader();

        if (drchallan.Read())
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Duplicate Challan Number Not Accepted..'); </script> ");
            drchallan.Close();
            return;
        }

        drchallan.Close();

        string challannum = txttrukchlnno.Text.Trim();

        string Racknum = ddlrackno.SelectedItem.Text;
        string commodity = ddlcommodity.SelectedValue;
        string scheme = ddlscheme.SelectedValue;
        string GodownName = ddlgodown.SelectedValue;
        string cropyear = ddlcropyear.SelectedItem.Text;
        string Quantity = txtquant.Text.Trim();
        string Bags = txtbagno.Text.Trim();
        string TruckNumber = txttruckno.Text.Trim();
        string Recevingdate = getDate_MDY(DaintyDate1.Text);
        string IPadd = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string opid = Session["OperatorId"].ToString();

        string InseQry = "";

        if (ddlChallanNo.Visible == true)
        {
            GetFirstfour = result = "";
            length = 0;

            GetFirstfour = ddlChallanNo.SelectedItem.ToString();
            length = GetFirstfour.Length;
            if (length > 4)
            {
                result = GetFirstfour.Substring(0, 4);
            }

            if (result == "MORK")
            {
                InseQry = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                          "Update DeliveryChallan_MO Set RRDRecd_Qty='" + Quantity + "',RRDRecd_Bags='" + Bags + "',RRDRecd_Date='" + Recevingdate + "',RRDCreated=GETDATE() where Rack_No='" + ddlrackno.SelectedItem.ToString() + "' and DC_MO='" + ddlChallanNo.SelectedItem.ToString() + "' and FrmDist='" + distid + "' and STO_No='" + hdfSTO_No.Value + "';";

                InseQry += "Insert into Return_From_Rack (DC_MO,Rack_No,STO_No,FrmDist,Commodity,CropYear,Scheme,Issue_Center,Godown,Quantity,Bags,Truck_No,Recd_Date,CreatedDate,IP,OperatorID,Remark) Values ('" + challannum + "','" + Racknum + "','" + hdfSTO_No.Value + "','" + distid + "','" + commodity + "','" + cropyear + "','" + scheme + "','" + issueid + "','" + GodownName + "','" + Quantity + "','" + Bags + "','" + TruckNumber + "','" + Recevingdate + "',GETDATE(),'" + IPadd + "','" + opid + "','" + txtremark.Text + "');";

                InseQry += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
            }
        }
        else
        {
            InseQry += "Insert into Return_From_Rack (DC_MO,Rack_No,STO_No,FrmDist,Commodity,CropYear,Scheme,Issue_Center,Godown,Quantity,Bags,Truck_No,Recd_Date,CreatedDate,IP,OperatorID,Remark) Values ('" + challannum + "','" + Racknum + "','" + hdfSTO_No.Value + "','" + distid + "','" + commodity + "','" + cropyear + "','" + scheme + "','" + issueid + "','" + GodownName + "','" + Quantity + "','" + Bags + "','" + TruckNumber + "','" + Recevingdate + "',GETDATE(),'" + IPadd + "','" + opid + "','" + txtremark.Text + "');";

            //InseQry = "Insert into Return_From_Rack (DistrictId,IssueCenter ,Challan_No,RackNumber ,Commodity ,Scheme ,GodownName ,CropYear ,Quantity,Bags ,TruckNumber  ,ReceivingDate ,IPaddress ,UserId  ,CreatedDate) Values ('" + distid + "','" + issueid + "','" + challannum + "','" + Racknum + "','" + commodity + "','" + scheme + "','" + GodownName + "','" + cropyear + "','" + Quantity + "','" + Bags + "','" + TruckNumber + "','" + Recevingdate + "','" + IPadd + "','" + opid + "',getdate())";
        }

        SqlCommand cmdins = new SqlCommand(InseQry, con);

        try
        {
            int x = cmdins.ExecuteNonQuery();

            if (x > 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved.'); </script> ");
                btnsubmit.Enabled = false;
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Problem Occured.'); </script> ");
                return;
            }
        }

        catch (Exception ex)
        {
            Label11.Text = ex.ToString();
        }
    }

    protected void ddlrackno_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnsubmit.Enabled = true;
        Label11.Text = "";

        txttrukchlnno.Visible = true;
        ddlChallanNo.Visible = false;

        ddlcommodity.SelectedIndex = ddlscheme.SelectedIndex = ddlgodown.SelectedIndex = 0;
        ddlChallanNo.Items.Clear();

        txtbagno.Text = txtquant.Text = txttruckno.Text = txttrukchlnno.Text = DaintyDate1.Text = txtremark.Text = "";
        hdfSTO_No.Value = "";

        if (ddlrackno.SelectedItem.Text == "--Select--" || ddlrackno.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Rack Number....'); </script> ");
            return;
        }

        else
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string rackdata = "SELECT Rack_No, Commodity_ID ,Scheme_Id  FROM tbl_RackMaster where Rack_No = '" + ddlrackno.SelectedItem.Text + "'";
            SqlCommand cmdrackdata = new SqlCommand(rackdata, con);

            SqlDataAdapter da = new SqlDataAdapter(cmdrackdata);

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string commodity = ds.Tables[0].Rows[0]["Commodity_ID"].ToString();

                string scheme = ds.Tables[0].Rows[0]["Scheme_Id"].ToString();

                ddlcommodity.SelectedValue = commodity;

                ddlscheme.SelectedValue = scheme;
            }
            else
            {
                string rackdata1 = "SELECT DC_MO,Commodity FROM DeliveryChallan_MO where Rack_No = '" + ddlrackno.SelectedItem.Text + "'";
                SqlCommand cmdrackdata1 = new SqlCommand(rackdata1, con);

                SqlDataAdapter da1 = new SqlDataAdapter(cmdrackdata1);

                DataSet ds1 = new DataSet();
                da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string commodity = ds1.Tables[0].Rows[0]["Commodity"].ToString();

                    ddlcommodity.SelectedValue = commodity;
                    ddlscheme.SelectedValue = "0";

                    txttrukchlnno.Visible = false;
                    ddlChallanNo.Visible = true;

                    ddlChallanNo.DataSource = ds1.Tables[0];
                    ddlChallanNo.DataTextField = "DC_MO";
                    ddlChallanNo.DataValueField = "DC_MO";
                    ddlChallanNo.DataBind();
                    ddlChallanNo.Items.Insert(0, "--Select--");
                }

                else
                {
                    ddlcommodity.SelectedValue = "0";
                    ddlscheme.SelectedValue = "0";

                    txttrukchlnno.Visible = true;
                    ddlChallanNo.Visible = false;
                }
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    protected void ddlChallanNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfSTO_No.Value = "";

        if (ddlChallanNo.SelectedIndex > 0)
        {
            GetSubTransporter();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Challan Number'); </script> ");
        }
    }

    public void GetSubTransporter()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select STO_No From DeliveryChallan_MO where Rack_No='" + ddlrackno.SelectedItem.ToString() + "' and DC_MO='" + ddlChallanNo.SelectedItem.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfSTO_No.Value = ds.Tables[0].Rows[0]["STO_No"].ToString();
                }
                else
                {
                    hdfSTO_No.Value = "";
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
}

