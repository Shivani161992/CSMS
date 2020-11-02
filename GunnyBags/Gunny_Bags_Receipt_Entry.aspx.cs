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
    string Railhead, TransporterIDName = "";
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
       
        txtCropyear.Text = txt_receiveID.Text = txtIssuedDate.Text = txtQuantity.Text = txtbagsType.Text = txtTcNumber.Text = txtIN.Text = txtRailHead.Text = txtTransporter_Name.Text = txtTruckNumber.Text = "";

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
        ddlDeliveryChallan.Items.Clear();
        txtCropyear.Text = txt_receiveID.Text = txtIssuedDate.Text = txtQuantity.Text = txtbagsType.Text = txtTcNumber.Text = txtIN.Text = txtRailHead.Text = txtTransporter_Name.Text = txtTruckNumber.Text = "";

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
                string select = "select BookNumber from GunnyBags_DeliveryChallan_Dist where RcdDist='" + Dist_Id + "'and  RcdGodown='" + ddlRecGodown.SelectedValue.ToString() + "' and IsReceived='N'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDeliveryChallan.DataSource = ds.Tables[0];
                    ddlDeliveryChallan.DataTextField = "BookNumber";
                    ddlDeliveryChallan.DataValueField = "BookNumber";
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
      
        txtCropyear.Text = txt_receiveID.Text = txtIssuedDate.Text = txtQuantity.Text = txtbagsType.Text = txtTcNumber.Text = txtIN.Text = txtRailHead.Text = txtTransporter_Name.Text = txtTruckNumber.Text = "";


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
                string select = "select RailHead, R.RailHead_Name, IndentNumber,  TruckNumber, CropYear, CONVERT(varchar(10),IssueDate, 103 ) as IssueDate, RR_Number, IssueQty, BagsType, TruckChallan, Transporter_Number, TT.Transporter_Name from GunnyBags_DeliveryChallan_Dist as DC inner join tbl_Rail_Head as R on R.RailHead_Code=DC.RailHead inner join Transporter_Table as TT on TT.Transporter_ID= DC.Transporter_Number where BookNumber='" + ddlDeliveryChallan.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCropyear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    txt_receiveID.Text = ds.Tables[0].Rows[0]["RR_Number"].ToString();
                    txtIssuedDate.Text = ds.Tables[0].Rows[0]["IssueDate"].ToString();

                    txtQuantity.Text = ds.Tables[0].Rows[0]["IssueQty"].ToString();
                    txtbagsType.Text = ds.Tables[0].Rows[0]["BagsType"].ToString();
                    txtTcNumber.Text = ds.Tables[0].Rows[0]["TruckChallan"].ToString();
                    txtIN.Text = ds.Tables[0].Rows[0]["IndentNumber"].ToString();
                    txtRailHead.Text = ds.Tables[0].Rows[0]["RailHead_Name"].ToString();
                    //Sendist = ds.Tables[0].Rows[0]["SenDist"].ToString();
                    txtTransporter_Name.Text = ds.Tables[0].Rows[0]["Transporter_Name"].ToString();
                    //TransporterID = ds.Tables[0].Rows[0]["TransID"].ToString();
                    txtTruckNumber.Text = ds.Tables[0].Rows[0]["IndentNumber"].ToString();
                    
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

        //if (ddlRecBagsType.SelectedIndex < 0)
        //{

        //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags type'); </script> ");
        //    return;
        //}
         if (txtRecQuantity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Receiving Quantity'); </script> ");
            txtRecQuantity.Focus();
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
            txtRecTruckNumber.Focus();
            return;
        }
         else if (txtToul.Text == "")
         {
             Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Toul Receipt'); </script> ");
             txtToul.Focus();
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
                    string qrey = "select max(Receipt_ID) as Receipt_ID from Gunny_bags_Receipt_Entry_Dist where LEN(Receipt_ID)<15 ";
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
                        gatepass = "171819" + "0";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }
                    string BookNumber = "GBREC" + DistId + gatepass;
                    string select = "select Dist, RailHead, R.RailHead_Name, IndentNumber,  TruckNumber, CropYear, CONVERT(varchar(10),IssueDate, 103 ) as IssueDate, RR_Number, IssueQty, BagsType, TruckChallan, Transporter_Number, TT.Transporter_Name from GunnyBags_DeliveryChallan_Dist as DC inner join tbl_Rail_Head as R on R.RailHead_Code=DC.RailHead inner join Transporter_Table as TT on TT.Transporter_ID= DC.Transporter_Number where BookNumber='" + ddlDeliveryChallan.SelectedValue.ToString() + "'";
                    da = new SqlDataAdapter(select, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Railhead = ds.Tables[0].Rows[0]["RailHead"].ToString();
                        TransporterIDName = ds.Tables[0].Rows[0]["Transporter_Number"].ToString();
                        Sendist = ds.Tables[0].Rows[0]["Dist"].ToString();
                    }
                   
                    ConvertServerDate ServerIssuedDate = new ConvertServerDate();
                    string IssuedDate = ServerIssuedDate.getDate_MDY(txtIssuedDate.Text);
                    ConvertServerDate ServerReceiveDate = new ConvertServerDate();
                    string ReceiveDate = ServerReceiveDate.getDate_MDY(txtDate_of_Receiving.Text);
                    string instr = "";

                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                        "update GunnyBags_DeliveryChallan_Dist set IsReceived='Y' where BookNumber='" + ddlDeliveryChallan.SelectedValue.ToString() + "';";
                    instr += "insert into Gunny_bags_Receipt_Entry_Dist(Receipt_ID, DC_num_ID, RR_ReceiveID, CropYear, Issued_Date, Sen_Quantity, Sen_Bagstype,Tc_Number, Truck_number, Transporter_Name,  Sen_Dist,  Rec_quantity, Date_Of_receiving, Rec_TruckNumber, Rec_Dist, Rec_IC, Rec_Branch, Rec_Godown, CreatedDate, IP, BookNumber_RecNum, IsReceived, IndentNumber, RailHead, ToulReceipt) values ('" + gatepass + "','" + ddlDeliveryChallan.SelectedValue.ToString() + "','" + txt_receiveID.Text + "','" + txtCropyear.Text + "','" + IssuedDate + "','" + txtQuantity.Text + "','" + txtbagsType.Text + "','" + txtTcNumber.Text + "','" + txtTruckNumber.Text + "','" + TransporterIDName + "','" + Sendist + "','" + txtRecQuantity.Text + "','" + ReceiveDate + "','" + txtRecTruckNumber.Text + "','" + DistId + "','" + ICID + "','" + ddlRecBranch.SelectedValue.ToString() + "','" + ddlRecGodown.SelectedValue.ToString() + "',getdate(),'" + ip + "','" + BookNumber + "','Y', '" + txtIN.Text + "','" + Railhead + "','" + txtToul.Text + "')";
                     instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                    cmd = new SqlCommand(instr, con);
                    string check = (string)cmd.ExecuteScalar();

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");
                    txtRecQuantity.Enabled = false;
                    //ddlRecBagsType.Enabled = false;
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

    //protected void ddlRecBagsType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    bttSubmit.Enabled = true;
    //}
    protected void txtRecQuantity_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtQuantity.Text) >= Convert.ToInt32(txtRecQuantity.Text))
        {
            bttSubmit.Enabled = true;
        }

        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('प्राप्त की हुई मात्रा भेजी गयी मात्रा के बराबर याकम होगी|'); </script> ");
            txtRecQuantity.Text = "";
            txtRecQuantity.Focus();
            return;
        }
    }
}