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

public partial class District_RackDispatch_IC : System.Web.UI.Page
{
    string distid = "",GetFirstfour = "",result="";
   
    SqlCommand cmd = new SqlCommand();
    MoveChallan mobj = null;
    chksql chk = null;
    static string issueid = "";
    protected Common ComObj = null, cmn = null;
    SqlDataReader dr;

    SqlDataAdapter da;
    DataSet ds;

    int length = 0;

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());

            distid = Session["dist_id"].ToString();

            txtRecdBags.Attributes.Add("onkeypress", "return CheckIsnonDecimal(this)");
            txtRecdQty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                       
            txtRecdBags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtRecdBags.Attributes.Add("onchange", "return chksqltxt(this)");

            txtRecdQty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtRecdQty.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate1.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            DaintyDate1.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            DaintyDate1.Attributes.Add("onchange", "return chksqltxt(this)");

            if (!IsPostBack)
            {
                GetRack();
                lbldistrict.Text = Session["dist_name"].ToString();
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    void GetRack()
    {
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());

        string dist = distid;
        ddlracknumber.Items.Insert(0, "--Select--");
        string qreyrac = "(select Rack_No from dbo.tbl_RackMaster where district_code='" + dist + "'" +
            " Union All " +
            " SELECT distinct Rack_No FROM DeliveryChallan_MO where Rack_No is not Null and Rack_No!='00' and FrmDist='" + dist + "' and IsReceived='N' and ModeofDispatch='13') order by Rack_No";
        cmd.Connection = con;
        cmd.CommandText = qreyrac;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand cmdrack = new SqlCommand(qreyrac, con);
        SqlDataAdapter darack = new SqlDataAdapter(cmdrack);
        DataSet dsrack = new DataSet();

        darack.Fill(dsrack);

        if (dsrack.Tables[0].Rows.Count == 0)
        {

        }

        else
        {
            ddlracknumber.DataSource = dsrack.Tables[0];
            ddlracknumber.DataTextField = "Rack_No";
            ddlracknumber.DataValueField = "Rack_No";
            ddlracknumber.DataBind();
            ddlracknumber.Items.Insert(0, "--Select--");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    void GetICenter()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        mobj = new MoveChallan(ComObj);
        string qry = "SELECT * FROM dbo.tbl_MetaData_DEPOT where DistrictId = '23" + distid + "' order by DepotID ";
        DataSet ds = mobj.selectAny(qry);

        ddlIssuecenter.DataSource = ds.Tables[0];
        ddlIssuecenter.DataTextField = "DepotName";
        ddlIssuecenter.DataValueField = "DepotID";
        ddlIssuecenter.DataBind();
        ddlIssuecenter.Items.Insert(0, "--Select--");

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected String getDate_MDY(string inDate)
    {
        string dat = "";
        if (inDate != "")
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dat = (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
        }
        return dat;
    }

    protected void ddlIssuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Enabled = true;

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlIssuecenter.SelectedValue == "0" || ddlIssuecenter.SelectedItem.Text == "--Select--")
        {

        }
        else
        {
            if (ddlracknumber.SelectedValue == "0")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Rack Number ....'); </script> ");
                return;
            }

            issueid = ddlIssuecenter.SelectedValue;

            string getTC = "Select Challan_No from SCSC_RailHead_TC where Rack_NO = '" + ddlracknumber.SelectedItem.Text + "' and Dist_ID = '" + distid + "' and Depot_Id = '" + issueid + "' and SCSC_RailHead_TC.Challan_No not in (Select QtyReceived_RackDispatchPoint.TruckChallan from QtyReceived_RackDispatchPoint where sendingDist = '" + distid + "' and sendingIssueCenter = '" + issueid + "') ";
            SqlCommand cmdtc = new SqlCommand(getTC, con);
            SqlDataAdapter datc = new SqlDataAdapter(cmdtc);

            DataSet dstc = new DataSet();

            datc.Fill(dstc);

            ddlchallan.DataSource = dstc.Tables[0];
            ddlchallan.DataTextField = "Challan_No";
            ddlchallan.DataValueField = "Challan_No"; 
            ddlchallan.DataBind();
            ddlchallan.Items.Insert(0, "--Select--");

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Enabled = true;

        if (ddlchallan.SelectedValue == "0")
        {

        }
        else
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string getdata = "SELECT Sendto_District,Challan_Date,CONVERT(nvarchar,Challan_Date,103)Challan_Date1 ,Commodity ,Bags ,Qty_send , Truck_no ,Cropyear FROM SCSC_RailHead_TC where Challan_No = '" + ddlchallan.SelectedValue + "'";

            SqlCommand cmddata = new SqlCommand(getdata, con);
            SqlDataAdapter dadata = new SqlDataAdapter(cmddata);

            DataSet dsData = new DataSet();
            dadata.Fill(dsData);

            string Trucknum = dsData.Tables[0].Rows[0]["Truck_no"].ToString();
            string TCdate = dsData.Tables[0].Rows[0]["Challan_Date1"].ToString();
            string sendbags = dsData.Tables[0].Rows[0]["Bags"].ToString();
            string sendqty = dsData.Tables[0].Rows[0]["Qty_send"].ToString();
            string crop_year = dsData.Tables[0].Rows[0]["Cropyear"].ToString();
            hdfChallanDate.Value = dsData.Tables[0].Rows[0]["Challan_Date"].ToString();
            hdfComdty.Value = dsData.Tables[0].Rows[0]["Commodity"].ToString();
            hdfRecDist.Value = dsData.Tables[0].Rows[0]["Sendto_District"].ToString();

            lbltruck.Text = Trucknum;
            lblchallanDate.Text = TCdate;
            txtsendbags.Text = sendbags;
            txtsendQty.Text = sendqty;
            lblcropyear.Text = crop_year;

            txtRecdBags.Text = "";
            txtRecdQty.Text = "";
            DaintyDate1.Text = "";

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlracknumber.SelectedValue == "0" || ddlracknumber.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Rack Number ....'); </script> ");
            return;
        }

        if (ddlIssuecenter.SelectedValue == "0" || ddlIssuecenter.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Issue Center ....'); </script> ");
            return;
        }

       

        if (txtRecdQty.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Receiving Quantity ....'); </script> ");
            return;
        }

        if (txtRecdBags.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Receiving Bags ....'); </script> ");
            return;
        }

        if (DaintyDate1.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Receiving Date ....'); </script> ");
            return;
        }

        if (ddlchallan.SelectedValue == "0" || ddlchallan.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Challan Number ....'); </script> ");
            return;
        }

        double sendqty = Convert.ToDouble(txtsendQty.Text);
        Int32 sendbags = Convert.ToInt32(txtsendbags.Text);

        Int32 recdbags = Convert.ToInt32(txtRecdBags.Text);
        double recdQty = Convert.ToDouble(txtRecdQty.Text);


        if (recdQty > sendqty || recdbags > sendbags)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Received Bags or Quantity will not more than Sending Bags or Quantity ....'); </script> ");
            return;
        }
        
        string racknum = ddlracknumber.SelectedItem.Text;
        string sendIC = ddlIssuecenter.SelectedValue;
        string challan = ddlchallan.SelectedItem.Text;

        string std = getDate_MDY(DaintyDate1.Text);

        //DateTime std = DateTime.Parse(getDate_MDY(DaintyDate1.Text));

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string opid = Session["OperatorIDDM"].ToString();

        GetFirstfour = result = "";
        length = 0;

        GetFirstfour = ddlchallan.SelectedItem.ToString();
        length = GetFirstfour.Length;
        if (length > 4)
        {
            result = GetFirstfour.Substring(0, 4);
        }

        if (result == "MORK")
        {
            string Update = "";
            distid = Session["dist_id"].ToString();
            try
            {
                Update = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                          "Update DeliveryChallan_MO Set IsReceived='Y',RSDRecd_Qty='" + txtRecdQty.Text + "',RSDRecd_Bags='" + txtRecdBags.Text + "',RSDRecd_Date='" + std + "',RSDCreatedDate=GETDATE() where FrmDist='" + distid + "' and DC_MO='" + ddlchallan.SelectedItem.ToString() + "' and Rack_No='" + ddlracknumber.SelectedItem.ToString() + "' and CropYear='" + lblcropyear.Text + "' and Issued_Date='" + hdfChallanDate.Value + "';";

                Update += "Insert into QtyReceived_RackDispatchPoint (Racknumber ,sendingDist ,sendingIssueCenter ,TruckChallan ,CropYear ,sendBags ,SendQty ,RecdBags ,RecdQty ,RecdDate ,IP_Address ,Created_date ,OperatorID,Commodity,RecDist,IsReceived) values('" + racknum + "','" + distid + "','" + sendIC + "','" + challan + "','" + lblcropyear.Text + "','" + txtsendbags.Text + "','" + txtsendQty.Text + "','" + txtRecdBags.Text + "','" + txtRecdQty.Text + "','" + std + "','" + ip + "',getdate(),'" + opid + "','"+hdfComdty.Value+"','"+hdfRecDist.Value+"','N');";
                
                Update += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                cmd = new SqlCommand(Update, con);
                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved Sucessfully ....'); </script> ");
                    DaintyDate1.Text = "";
                    txtRecdQty.Text = "";
                    txtRecdBags.Text = "";
                    btnSave.Enabled = false;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Not Save For Movement Order'); </script> ");
                }
            }
            catch
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Not Save For Movement Order'); </script> ");
            }

            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        else
        {
            string insquy = "Insert into QtyReceived_RackDispatchPoint (Racknumber ,sendingDist ,sendingIssueCenter ,TruckChallan ,CropYear ,sendBags ,SendQty ,RecdBags ,RecdQty ,RecdDate ,IP_Address ,Created_date ,OperatorID) values('" + racknum + "','" + distid + "','" + sendIC + "','" + challan + "','" + lblcropyear.Text + "','" + txtsendbags.Text + "','" + txtsendQty.Text + "','" + txtRecdBags.Text + "','" + txtRecdQty.Text + "','" + std + "','" + ip + "',getdate(),'" + opid + "')";

            SqlCommand cmdins = new SqlCommand(insquy, con);

            try
            {
                int x = cmdins.ExecuteNonQuery();

                if (x == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Not Saved, Might be Some Error ....'); </script> ");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved Sucessfully ....'); </script> ");

                    DaintyDate1.Text = "";
                    txtRecdQty.Text = "";
                    txtRecdBags.Text = "";
                    btnSave.Enabled = false;
                }
            }

            catch
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Not Saved, Might be Some Error ....'); </script> ");
            }

            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        DaintyDate1.Text = "";
        txtRecdQty.Text = "";
        txtRecdBags.Text = "";
        btnSave.Enabled = false;

        lbltruck.Text = "";
        lblchallanDate.Text = "";
        txtsendbags.Text = "";
        txtsendQty.Text = "";
        lblcropyear.Text = "";

        GetICenter();
        GetRack();

        ddlchallan.SelectedIndex = 0;


    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void ddlracknumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetICenter();
    }
}


