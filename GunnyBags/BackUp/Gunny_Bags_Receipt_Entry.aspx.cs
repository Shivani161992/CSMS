using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GunnyBags_Gunny_Bags_Receipt_Entry : System.Web.UI.Page
{
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;

    double QtyTotal = 0;
    public string sid = "";
    string OwnDist, OtherDist;
    public string ICID, DistId;
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    int RemainingQty;
    string TotalQty;
    string TransporterID, Sendist, SenIC, SenBranch, SenGodown;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {

                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();
                // txtRecDist.Text = Session["dist_name"].ToString();
                // string DistCode = Session["dist_id"].ToString();
                //string fromdate = Request.Form[txtDate_of_Receiving.UniqueID];
                //txtDate_of_Receiving.Text = fromdate;
                txtRecIssueCenter.Text = Session["issue_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //DateTime _date;
                //string day = "";
                //_date = DateTime.Parse("5/MAY/2012");
                //day = _date.ToString("dd-mm-yyyy");
                //GetReceivingID();
                GetBranch();
                GetRecDist();

                string issdate = Request.Form[txtIssuedDate.UniqueID];
                txtIssuedDate.Text = issdate;

                string ReceivedDate = Request.Form[txtIssuedDate.UniqueID];
                txtDate_of_Receiving.Text = ReceivedDate;
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }


    public void GetRecDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string DistCode = Session["dist_id"].ToString();
                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code='" + DistId + "'  Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtRecDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    }
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
    public void GetBranch()
    {
        string Dist_Id = Session["dist_id"].ToString();
        ICID = Session["issue_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + ICID + "'");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlRecBranch.DataSource = ds.Tables[0];
                        ddlRecBranch.DataTextField = "DepotName";
                        ddlRecBranch.DataValueField = "BranchID";
                        ddlRecBranch.DataBind();
                        ddlRecBranch.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + Dist_Id + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlRecBranch.DataSource = ds.Tables[0];
                        ddlRecBranch.DataTextField = "DepotName";
                        ddlRecBranch.DataValueField = "BranchId";
                        ddlRecBranch.DataBind();
                        ddlRecBranch.Items.Insert(0, "--Select--");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }

    protected void ddlRecBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRecGodown.Items.Clear();
        //ddlCMRDONo.Items.Clear();
        ddlDeliveryChallan.Items.Clear();

        //hdfAdjustCMRDO.Value = "";

        if (ddlRecBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें |'); </script> ");
        }

    }

    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='" + ddlRecBranch.SelectedValue.ToString() + "' order by Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlRecGodown.DataSource = ds.Tables[0];
                        ddlRecGodown.DataTextField = "Godown_Name";
                        ddlRecGodown.DataValueField = "Godown_ID";
                        ddlRecGodown.DataBind();
                        ddlRecGodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch Name'); </script> ");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }
    protected void ddlRecGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRecGodown.SelectedIndex > 0)
        {
            GetDeliveryChallan();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
        }

    }

    public void GetDeliveryChallan()
    {
        string Dist_Id = Session["dist_id"].ToString();
        string IC_Id = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "select DC_Number_ID, DC_BookNumber from GunnyBags_DeliveryChallan where IsReceived='N' and Receiving_District='" + Dist_Id + "' and Receiving_IC='" + IC_Id + "' and Receiving_Branch='" + ddlRecBranch.SelectedValue.ToString() + "' and Receiving_Godown='" + ddlRecGodown.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDeliveryChallan.DataSource = ds.Tables[0];
                    ddlDeliveryChallan.DataTextField = "DC_BookNumber";
                    ddlDeliveryChallan.DataValueField = "DC_BookNumber";
                    ddlDeliveryChallan.DataBind();
                    ddlDeliveryChallan.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके गोदाम में Delivery Challan उपलब्ध नहीं है|'); </script> ");
                    return;
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
    protected void ddlDeliveryChallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeliveryChallan.SelectedIndex > 0)
        {
            GetDeliveryChallanData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Delivery Challan Number'); </script> ");
        }

    }

    public void GetDeliveryChallanData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "select D.district_name as Sending_District, Sending_District as SenDist, CropYear, RR_Receive_ID, BagsType, TC_Number, convert(varchar(10), Date_of_Issue, 103) as Date_of_Issue , T.Transporter_Name as Transporter_Name_HLRT, Transporter_Name_HLRT as TransID, Truck_Number, Qty_Issued, IC.DepotName, B.DepotName as Branch, G.Godown_Name from GunnyBags_DeliveryChallan as DC inner join pds.districtsmp as D on D.district_code=DC.Sending_District inner join tbl_MetaData_DEPOT as IC on Ic.DepotID=DC.SenIC inner join Warehouse_Branch as B on B.BranchId=DC.SenBranch inner join tbl_MetaData_GODOWN as G on G.Godown_ID=DC.SenGodown inner join Transporter_Table as T on T.Transporter_ID=DC.Transporter_Name_HLRT where DC_BookNumber='" + ddlDeliveryChallan.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCropyear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    txt_receiveID.Text = ds.Tables[0].Rows[0]["RR_Receive_ID"].ToString();
                    txtIssuedDate.Text = ds.Tables[0].Rows[0]["Date_of_Issue"].ToString();
                    txtQuantity.Text = ds.Tables[0].Rows[0]["Qty_Issued"].ToString();
                    txtbagsType.Text = ds.Tables[0].Rows[0]["BagsType"].ToString();
                    txtTcNumber.Text = ds.Tables[0].Rows[0]["TC_Number"].ToString();
                    txtSenDist.Text = ds.Tables[0].Rows[0]["Sending_District"].ToString();
                    Sendist = ds.Tables[0].Rows[0]["SenDist"].ToString();
                    txtTransporter_Name.Text = ds.Tables[0].Rows[0]["Transporter_Name_HLRT"].ToString();
                    TransporterID = ds.Tables[0].Rows[0]["TransID"].ToString();
                    txtTruckNumber.Text = ds.Tables[0].Rows[0]["Truck_Number"].ToString();
                    txtSenIC.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    txtSenBranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                    txtSenGodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके गोदाम में Delivery Challan उपलब्ध नहीं है|'); </script> ");
                    return;
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
    protected void bttprint_Click(object sender, EventArgs e)
    {

    }
    protected void bttNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void bttClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Gunny_BagsHome.aspx");
    }

    protected void bttSubmit_Click(object sender, EventArgs e)
    {

        if (ddlRecBagsType.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags type'); </script> ");
            return;
        }
        else if (txtRecQuantity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Receiving Quantity'); </script> ");
            return;
        }
        else if (txtDate_of_Receiving.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Date of Quantity Received '); </script> ");
            return;
        }

        else if (txtRecTruckNumber.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Truck Number '); </script> ");
            return;
        }
        else
        {
            using (con = new SqlConnection(strcon))
                try
                {
                    ICID = Session["issue_id"].ToString();
                    DistId = Session["dist_id"].ToString();
                    con.Open();
                    string qrey = "select max(Receipt_ID) as Receipt_ID from Gunny_bags_Receipt_Entry where LEN(Receipt_ID)<15 ";
                    da = new SqlDataAdapter(qrey, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    //mobj1 = new MoveChallan(ComObj);
                    //string qrey = "select isnull(max(Inspector_ID),0) as Transporter_ID  from Inspector_Master_02017 where  Distt_ID='" + sid + "' and LEN(Inspector_ID)<8 ";
                    //DataSet ds = new DataSet();
                    // dmax.Fill(ds);
                    // DataTable dt = ds.Tables[""];
                    DataRow dr = ds.Tables[0].Rows[0];
                    //gatepass = dr["Inspector_ID"].ToString();
                    gatepass = ds.Tables[0].Rows[0]["Receipt_ID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "1717" + "0";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }
                    string BookNumber = "GBREC" + gatepass;

                    string select = "select D.district_name as Sending_District, Sending_District as SenDist, CropYear, RR_Receive_ID, BagsType, TC_Number, convert(varchar(10), Date_of_Issue, 103) as Date_of_Issue , T.Transporter_Name as Transporter_Name_HLRT, Transporter_Name_HLRT as TransID, Truck_Number, Qty_Issued, SenIC, SenBranch, SenGodown from GunnyBags_DeliveryChallan as DC inner join pds.districtsmp as D on D.district_code=DC.Sending_District inner join Transporter_Table as T on T.Transporter_ID=DC.Transporter_Name_HLRT where DC_BookNumber='" + ddlDeliveryChallan.SelectedValue.ToString() + "'";
                    da = new SqlDataAdapter(select, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Sendist = ds.Tables[0].Rows[0]["SenDist"].ToString();
                        TransporterID = ds.Tables[0].Rows[0]["TransID"].ToString();
                        SenIC = ds.Tables[0].Rows[0]["SenIC"].ToString();
                        SenBranch = ds.Tables[0].Rows[0]["SenBranch"].ToString();
                        SenGodown = ds.Tables[0].Rows[0]["SenGodown"].ToString();


                    }
                    ConvertServerDate ServerIssuedDate = new ConvertServerDate();
                    string IssuedDate = ServerIssuedDate.getDate_MDY(txtIssuedDate.Text);
                    ConvertServerDate ServerReceiveDate = new ConvertServerDate();
                    string ReceiveDate = ServerReceiveDate.getDate_MDY(txtDate_of_Receiving.Text);
                    string instr = "";

                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                        "update GunnyBags_DeliveryChallan set IsReceived='Y' where DC_BookNumber='"+ddlDeliveryChallan.SelectedValue.ToString()+"';";
                     instr += "insert into Gunny_bags_Receipt_Entry(Receipt_ID, DC_num_ID, RR_ReceiveID, CropYear, Issued_Date, Sen_Quantity, Sen_Bagstype,Tc_Number, Truck_number, Transporter_Name,  Sen_Dist, Sen_IC, Sen_Branch, Sen_Godown, Rec_quantity, Rec_BagsType, Date_Of_receiving, Rec_TruckNumber, Rec_Dist, Rec_IC, Rec_Branch, Rec_Godown, CreatedDate, IP, BookNumber_RecNum, IsReceived) values ('" + gatepass + "','" + ddlDeliveryChallan.SelectedValue.ToString() + "','" + txt_receiveID.Text + "','" + txtCropyear.Text + "','" + IssuedDate + "','" + txtQuantity.Text + "','" + txtbagsType.Text + "','" + txtTcNumber.Text + "','" + txtTruckNumber.Text + "','" + TransporterID + "','" + Sendist + "','" + SenIC + "','" + SenBranch + "','" + SenGodown + "','" + txtRecQuantity.Text + "','" + ddlRecBagsType.SelectedValue.ToString() + "','" + ReceiveDate + "','" + txtRecTruckNumber.Text + "','" + DistId + "','" + ICID + "','" + ddlRecBranch.SelectedValue.ToString() + "','" + ddlRecGodown.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + BookNumber + "','Y')";
                     instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                    cmd = new SqlCommand(instr, con);
                    string check = (string)cmd.ExecuteScalar();

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                    txtRecQuantity.Enabled = false;
                    ddlRecBagsType.Enabled = false;
                    ddlDeliveryChallan.Enabled = false;
                    ddlRecBranch.Enabled = false;
                    ddlRecGodown.Enabled = false;

                    txtDate_of_Receiving.Enabled = false;
                    txtRecTruckNumber.Enabled = false;
                    bttSubmit.Visible = false;
                    bttprint.Visible = true;
                    bttprint.Enabled = true;
                    trID.Visible = true;
                    Label1.Text = "Receipt Entry ID is:-" + BookNumber;








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

    protected void ddlRecBagsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bttSubmit.Enabled = true;
    }
}