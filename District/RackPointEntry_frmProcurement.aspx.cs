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

public partial class District_RackPointEntry_frmProcurement : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2014"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2014"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());

    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();

    public string gatepass = "";
  
    public string did = "";

    public string distp = "";
    public static string CSMS_Comid;
    chksql chk = null;
    public Int64 getnum;

    
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            did = Session["dist_id"].ToString();


            txtissueqty.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtissubag.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txtrecdbags.Attributes.Add("onkeypress", "return IsNumericProcQtyBag(event,this)");
            txtrecdqty.Attributes.Add("onkeypress", "return IsNumericProcQty(event,this)");

            txtrecdqty.Attributes.Add("onchange", "return ChkNotgrate(this);");

            txtissueqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtissueqty.Attributes.Add("onchange", "return chksqltxt(this)");


            txtrecdbags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecdbags.Attributes.Add("onchange", "return chksqltxt(this)");

            txtrecdqty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtrecdqty.Attributes.Add("onchange", "return chksqltxt(this)");

            txtchlnno.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
            txtchlnno.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            txtchlnno.Attributes.Add("onchange", "return chksqltxt(this)");


            lblchallanDate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
            lblchallanDate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
            lblchallanDate.Attributes.Add("onchange", "return chksqltxt(this)");

            DaintyDate1.Attributes.Add("onkeypress", "return CheckCalDate(this)");

            HyperLink1.Attributes.Add("onclick", "window.open('Print_Receipt_Procurement.aspx',null,'left=50, top=10, height=570, width= 690, status=n o, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');"); 

            if (!IsPostBack)
            {
                
                lblchallanDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                GetRack();

                GetCommodity();

                ddlcropyear.Items.Add(DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
                ddlcropyear.Items.Add(DateTime.Now.Year - 1 + "-" + DateTime.Now.Year);
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 2).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 3).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 2).ToString());
                ddlcropyear.Items.Add((int.Parse(DateTime.Now.Year.ToString()) - 4).ToString() + "-" + (int.Parse(DateTime.Now.Year.ToString()) - 3).ToString());
                


            }
       
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }


    void GetCommodity()
    {

        try
        {
            if (con_paddy != null)
            {

                if (con_WPMS.State == ConnectionState.Closed)
                {

                    con_WPMS.Open();   /// con_paddy karna hai
                }

                string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('8')";

                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);   /// con_paddy karna hai 
              
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlcomdty.DataSource = ds.Tables[0];
                        ddlcomdty.DataTextField = "crop";
                        ddlcomdty.DataValueField = "crpcode";
                        ddlcomdty.DataBind();
                        ddlcomdty.Items.Insert(0, "--Select--");

                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {
            if (con_WPMS.State == ConnectionState.Open)
            {

                con_WPMS.Close();   /// con_paddy karna hai
            }

           
        }
        finally
        {
            if (con_WPMS.State == ConnectionState.Open)
            {

                con_WPMS.Close();   /// con_paddy karna hai
            }
        }
    }

    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlcomdty.SelectedIndex == 0)
        {
            ddldistrict.Items.Clear();
            ddldistrict.DataSource = null;
            ddldistrict.DataBind();
            ddluparjan.Items.Clear();
            ddluparjan.DataSource = null;
            ddluparjan.DataBind();
            dlissuClear();
        }
        else
        {
            ddluparjan.Items.Clear();
            ddluparjan.DataSource = null;
            ddluparjan.DataBind();

            lblchallanDate.Text = "";
           
           
            txtrecdbags.Text = "";
            txtrecdqty.Text = "";
            txtchlnno.Text = "";
          
            txtissubag.Text = "";
            txtissueqty.Text = "";
           
           
            dlissuClear();

            if (ddlcomdty.SelectedValue.ToString() == "1")
            {

                
                getDistWht();
                btnSave.Visible = true;

                //getcsms_Commdty();
            }
            else if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {

                // No need to Get Paddy / Coarse Grain Data in Wheat Season, already taken Opening Balance in 01/April/2014, Mail by RM Sharma, 5 April 2014

                //lblNameDepot.Text = "प्रदाय केन्द";
                //getpadyDist();
                //btnSave.Visible = true;

                //getcsms_Commdty();
            }
            else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
            {



                //lblNameDepot.Text = "प्रदाय केन्द";
                //getDistCorcgrn();
                //btnSave.Visible = true;

                //getcsms_Commdty();
            }
        }
    }

    void getDistWht()
    {
        try
        {
            if (con_WPMS != null)
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }
                string qrysel = "select District_Name,District_Code from Districts order by District_Name";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        ddldistrict.DataSource = ds.Tables[0];
                        ddldistrict.DataTextField = "District_Name";
                        ddldistrict.DataValueField = "District_Code";
                        ddldistrict.DataBind();
                        ddldistrict.Items.Insert(0, "--चुनें--");
                        
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {
            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }
            
        }
        finally
        {
            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }
        }


    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        if (ddluparjan.SelectedIndex > 0)
        {
            btnSave.Enabled = true;
                      
            DaintyDate1.Text = "";
           
            txtrecdbags.Text = "";
            txtrecdqty.Text = "";
            txtchlnno.Text = "";
            txtissueId.Text = "";
            txttrucknopady.Text = "";
            txtissubag.Text = "";
            txtissueqty.Text = "";
            getpaddyIssueid();

            txtrec_tcnumber.Text = "";

            txtRec_TruckNumber.Text = "";

            lblchallanDate.Text = "";

            HyperLink1.Visible = false;
        }
        else
        {
            Response.Redirect("~/IssueCenter/CSC_Procurement.aspx");
        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {

        if (con_WPMS.State == ConnectionState.Open)
        {
            con_WPMS.Close();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    protected void ddlIssuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddluparjan.SelectedIndex == 0)
        {
            txtissueId.Text = "";
                      
            dgridchallan.DataSource = null;
            dgridchallan.DataBind();
            pnlgrd.Visible = false;
            dlissuClear();
        }
        else
        {
            dlissuClear();
            pnlgrd.Visible = true;

            getcsms_Commdty();
            getpaddyIssueid();

        }
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistrict.SelectedIndex == 0)
        {
            ddluparjan.Items.Clear();
            ddluparjan.DataSource = null;
            ddluparjan.DataBind();
            dgridchallan.DataSource = null;
            dgridchallan.DataBind();
            pnlgrd.Visible = false;
          
        }
        else
        {
           
            dgridchallan.DataSource = null;
            dgridchallan.DataBind();
            pnlgrd.Visible = false;
         
            ddluparjan.Items.Clear();
            ddluparjan.DataSource = null;
            ddluparjan.DataBind();
            txtchlnno.Text = "";
          
            lblchallanDate.Text = "";
            txtissubag.Text = "";
            txtissueqty.Text = "";
            txtissueId.Text = "";
            if (ddlcomdty.SelectedValue.ToString() == "1")
            {

                getWhtUparjncntr();
            }
            else if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
            {

                getpadyUparjncntr();

            }
            else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
            {

                getcorgrnUparjncntr();
            }
            else
            {
            }
        }
    }

    private void dlissuClear()
    {
        txtrecdbags.Text = "";
        txtrecdqty.Text = "";
        txtissueId.Text = "";
        txtchlnno.Text = "";
      
        lblchallanDate.Text = "";
        txtissubag.Text = "";
        txtissueqty.Text = "";
        dgridchallan.DataSource = null;
        dgridchallan.DataBind();
        pnlgrd.Visible = false;
       
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlracknumber.SelectedItem.Text == "--Select--" || ddlracknumber.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Rack Number...'); </script> ");
            return;
        }

        if (txtrecdqty.Text == "" || txtrecdqty.Text == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Received Quantity...'); </script> ");
            return;
        }

        if (txtrecdbags.Text == "" || txtrecdbags.Text == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Received Bags...'); </script> ");
            return;
        }


        if (DaintyDate1.Text == "" || DaintyDate1.Text == "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Date...'); </script> ");
            return;
        }


        if (Convert.ToDecimal(txtrecdbags.Text) > 0 && Convert.ToDecimal(txtrecdqty.Text) > 0)
        {
            decimal chk_RquintyPr = CheckNull(txtissueqty.Text) * 40;
            decimal chk_Rquinty = chk_RquintyPr / 100;

            decimal mqty1 = CheckNull(txtissueqty.Text) + chk_Rquinty;
            decimal mrecdqty1 = CheckNull(txtrecdqty.Text);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string tcnum = txtchlnno.Text;
            string trucknumber = txttrucknopady.Text;
            decimal recdqty = CheckNull(txtrecdqty.Text);
            string recdate = getDate_MDY(DaintyDate1.Text);
            string issueid = txtissueId.Text;

            string CheckduplicateRec = "Select * from SCSC_Procurement where Distt_ID='" + did + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + tcnum + "' and Truck_Number = '" + trucknumber + "' and Recd_Date = '" + recdate + "' and Recd_Qty = '" + recdqty + "' and Receipt_Id = '" + issueid + "' ";

            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);

            SqlDataReader drduplicate;

            drduplicate = cmdduplirec.ExecuteReader();

            if (drduplicate.Read())
            {
                return;
            }

            else
            {

                if (mrecdqty1 >= mqty1)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please check Recd_Qty it should not be greater than issue_Qty....'); </script> ");
                    
                    drduplicate.Close();
                }

                else
                {
                    drduplicate.Close();

                    # region wheat
                    if (ddlcomdty.SelectedValue.ToString() == "1")
                    {

                        if (con_WPMS.State == ConnectionState.Closed)
                        {
                            con_WPMS.Open();
                        }
                        SqlTransaction trns1;
                        cmd1.Connection = con_WPMS;
                        trns1 = con_WPMS.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmd1.Transaction = trns1;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlTransaction trns;
                        cmd.Connection = con;
                        trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmd.Transaction = trns;

                        SqlDataAdapter daP;
                        DataSet dsP = new DataSet();

                        //string select = "Select TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement where Distt_ID='" + did + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and TC_Number='" + txtchlnno.Text + "' and Commodity_Id='" + ddlcomdty.SelectedValue.ToString() + "' and YEAR=Year(GETDATE())";
                        string select = "Select Receipt_Id,TC_Number,Recd_Date,Truck_Number  from SCSC_Procurement where Distt_ID='" + did + "'  and Purchase_Center='" + ddluparjan.SelectedValue.ToString() + "' and Receipt_Id='" + txtissueId.Text + "' ";
                        cmd.CommandText = select;
                        daP = new SqlDataAdapter(cmd);

                        daP.Fill(dsP);

                        if (dsP.Tables[0].Rows.Count == 0)
                        {
                            gatepass = txtissueId.Text.Trim().ToString();
                            distp = ddldistrict.SelectedValue.ToString().Substring(2, 2);
                            string mpcdist = distp;
                            string mpcic = ddluparjan.SelectedValue;
                            string mdispdate = getDate_MDY(lblchallanDate.Text);
                            string mchallan = txtchlnno.Text;
                            string mtruckno = txttrucknopady.Text;

                            
                            string mcomdty = CSMS_Comid;
                            string mcropy = ddlcropyear.SelectedItem.ToString();
                            int mbags = CheckNullInt(txtissubag.Text);
                            decimal mqty = CheckNull(txtissueqty.Text);
                            string macno = txtaccptno.Text;

                            string mstatus = "N";

                            string mudate = "";
                            string mddate = "";
                            string mfyear = DateTime.Today.Year.ToString();
                            string mbookno = txtbookno.Text;


                            string accpno = mfyear + mbookno + txtaccptno.Text;
                            int month = int.Parse(DateTime.Today.Month.ToString());
                            int year = int.Parse(DateTime.Today.Year.ToString());
                            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            string state = Session["State_Id"].ToString();
                            string anst = "N";
                            int mrecdbags = CheckNullInt(txtrecdbags.Text);
                            decimal mrecdqty = CheckNull(txtrecdqty.Text);



                            string mrecddate = getDate_MDY(DaintyDate1.Text);

                            string opid = Session["OperatorIDDM"].ToString();
                            string notrans = "N";

                            DateTime dispdate = Convert.ToDateTime(lblchallanDate.Text);


                            DateTime Recdate = Convert.ToDateTime(DateTime.ParseExact(mrecddate, "MM/dd/yyyy", null).ToString("MM/dd/yyyy"));

                            string todaydate = DateTime.Now.ToString("dd/MM/yyyy");

                            DateTime currentdate = Convert.ToDateTime(DateTime.ParseExact(todaydate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));

                            int result = DateTime.Compare(Recdate, dispdate);

                            if (result == -1)
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Receiving Date will not Less than Dispatch Date...'); </script> ");
                                return;
                            }

                            int greaterdate = DateTime.Compare(currentdate, Recdate);

                            if (greaterdate == -1)
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Receiving Date will not greater than.To Day Date'); </script> ");
                                return;
                            }

                            if (ddlcomdty.SelectedItem.Text == "--Select--" || ddluparjan.SelectedItem.Text == "--Select--")
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Commodity/Purchase Center...'); </script> ");
                            }

                            else
                            {
                                string checkrcid = "Select count(Receipt_Id) from SCSC_Procurement where Receipt_Id = '" + gatepass + "' and Distt_ID='" + did + "'";
                                
                                cmd.CommandText = checkrcid;
                               
                                string str1 = cmd.ExecuteScalar().ToString();

                                if (Convert.ToInt16(str1) == 0)   // not Found, Insert start
                                {
                                    try
                                    {

                                        string qryinsert = "insert into dbo.SCSC_Procurement(State_Id,Distt_ID,RackNumber,Sending_District,Purchase_Center,Dispatch_Date,TC_Number,Truck_Number,Commodity_Id,Crop_Year,No_of_Bags,Quantity,Acceptance_No,Acceptance_Date,Book_No,Recd_Bags,Recd_Qty,Recd_Date,Receipt_Id,Month,Year,Created_Date,Updates_Date,Deleted_Date,IP_Address,AN_status,OperatorID,NoTransaction,IssueCenter_ID,Recd_Godown,Branch_Id,Status_Deposit,Transporter_ID)values('" + state + "','" + did + "','" + ddlracknumber.SelectedItem.Text + "','" + mpcdist + "','" + mpcic + "','" + mdispdate + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mcomdty + "','" + mcropy + "'," + mbags + "," + mqty + ",'" + macno + "',getdate(),'" + mbookno + "'," + mrecdbags + "," + mrecdqty + ",'" + mrecddate + "','" + gatepass + "'," + month + "," + mfyear + ",getdate(),'" + mudate + "','" + mddate + "','" + ip + "','" + anst + "','" + opid + "','" + notrans + "','BYRack','','','N','" + lbltranporterID.Text + "')";
                                        cmd.CommandText = qryinsert;
                                                                             
                                        string issuid = txtissueId.Text.Trim().ToString();
                                        string socity = ddluparjan.SelectedValue.ToString();

                                        string str = " Select * from IssueToSangrahanaKendra where IssueID='" + issuid + "' and SocietyID='" + socity + "'";
                                        cmd1.CommandText = str;

                                        SqlDataAdapter daP1 = new SqlDataAdapter(cmd1);
                                        DataSet dsP1 = new DataSet();
                                        daP1.Fill(dsP1);
                                        string Issuid = dsP1.Tables[0].Rows[0]["IssueID"].ToString();
                                        string disid = dsP1.Tables[0].Rows[0]["DistrictId"].ToString();
                                        string Socid = dsP1.Tables[0].Rows[0]["SocietyID"].ToString();
                                        string Crpyr = dsP1.Tables[0].Rows[0]["CropYear"].ToString();
                                        string mrktson = dsP1.Tables[0].Rows[0]["MarketingSeasonId"].ToString();
                                        string issuedt2 = dsP1.Tables[0].Rows[0]["DateOfIssue"].ToString();

                                        string comdty = dsP1.Tables[0].Rows[0]["CommodityId"].ToString();
                                        string bags = dsP1.Tables[0].Rows[0]["Bags"].ToString();
                                        string qty = dsP1.Tables[0].Rows[0]["QtyTransffer"].ToString();
                                        string taulptrk = dsP1.Tables[0].Rows[0]["TaulPtrakNo"].ToString();
                                        string trnsid = dsP1.Tables[0].Rows[0]["TransporterId"].ToString();
                                        string tcno = dsP1.Tables[0].Rows[0]["TruckChalanNo"].ToString();
                                        string truckno = dsP1.Tables[0].Rows[0]["TruckNo"].ToString();


                                        string udate = "";
                                        string status = "N";

                                        string checkpre = "Select count(IssueID) from IssueCenterReceipt_Online where IssueID = '" + Issuid + "'and SocietyID = '" + Socid + "'";
                                        //cmd1 = new SqlCommand(checkpre, con_paddy);
                                        cmd1.CommandText = checkpre;
                                        //cmd1.Connection = con_WPMS;

                                        string str12 = cmd1.ExecuteScalar().ToString();

                                        if (Convert.ToInt16(str12) == 0)   // not Found, Insert start
                                        {
                                            string inserttotest = "INSERT INTO IssueCenterReceipt_Online ([IssueID] ,[DistrictId],[RackNumber],[SocietyID],[PCID],[Sending_District],[CropYear],[MarketingSeasonId],[DateOfIssue],[CommodityId],[Bags],[QtyTransffer],[TaulPtrakNo],[TransporterId],[TruckChalanNo],[TruckNo],[Recv_Qty],[Receipt_Id],[AN_Status],[CreatedDate],[UpdatedDate],[Recd_Date],[Recd_Bags],IssueCenter_ID,Recd_Godown,Branch_Id)  VALUES('" + Issuid + "','23" + did + "','" + ddlracknumber.SelectedItem.Text + "','" + Socid + "','" + Socid + "','" + disid + "','" + Crpyr + "','" + mrktson + "','" + mdispdate + "','" + comdty + "','" + bags + "','" + qty + "','" + taulptrk + "','" + trnsid + "','" + txtrec_tcnumber.Text + "','" + txtRec_TruckNumber.Text + "','" + mrecdqty + "','" + gatepass + "','" + status + "',getdate(),'" + udate + "','" + mrecddate + "','" + mrecdbags + "','ByRack','','')";

                                            cmd1.CommandText = inserttotest;
                                            //cmd1.Connection = con_WPMS;
                                            //cmd1.CommandTimeout = 4600;
                                            int x = cmd1.ExecuteNonQuery();

                                        }
                                        //paddy end

                                        int count = cmd.ExecuteNonQuery();

                                        if (count >= 1)
                                        {
                                            trns1.Commit();
                                            con_WPMS.Close();
                                            trns.Commit();
                                            con.Close();                                          
                                        }

                                        HyperLink1.Visible = true;

                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully....'); </script> ");
                                        btnSave.Enabled = false;
                                       
                                        Session["Receipt_ID"] = gatepass;
                                        Session["Commodity_ID"] = ddlcomdty.SelectedValue.ToString();
                                      
                                        txtissubag.Text = "";
                                        txtissueqty.Text = "";
                                        txtaccptno.Text = "";
                                        txtbookno.Text = "";
                                        txtchlnno.Text = "";
                                        txttrucknopady.Text = "";
                                        ddldistrict.Focus();

                                    }

                                    catch (Exception ex)
                                    {

                                        trns1.Rollback();
                                        Label9.Visible = true;
                                        Label9.Text = "error:6" + ex.Message;

                                    }
                                    finally
                                    {
                                        con.Close();
                                        con_WPMS.Close();
                                    }

                                }

                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again....'); </script> ");
                                }

                            }

                        }

                        else
                        {
                            string tcc = dsP.Tables[0].Rows[0]["TC_Number"].ToString();
                            string pdat = dsP.Tables[0].Rows[0]["Recd_Date"].ToString();
                            string pdat1 = getdate(pdat);
                            string Tr_Nu = dsP.Tables[0].Rows[0]["Truck_Number"].ToString();
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Challan Number Exist...." + tcc + "..,Truck No..." + Tr_Nu + "... Recd_Date..." + pdat1 + "'); </script> ");


                        }

                    }

                    # endregion

                }
            }
        }
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    } 

    protected void dgridchallan_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgridchallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }

    protected void dgridchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrecdbags.Text = "";
        txtrecdqty.Text = "";
     
        txtissueId.Enabled = false;
        txtissueId.Text = dgridchallan.SelectedRow.Cells[2].Text;
        txtchlnno.Enabled = false;
        txtchlnno.Text = dgridchallan.SelectedRow.Cells[3].Text;

        txttrucknopady.Text = dgridchallan.SelectedRow.Cells[4].Text;

        txtissubag.Enabled = false;
        txtissueqty.Enabled = false;
        txtissubag.Text = dgridchallan.SelectedRow.Cells[5].Text;
        txtissueqty.Text = dgridchallan.SelectedRow.Cells[6].Text;
    
        lblchallanDate.Enabled = false;
        lblchallanDate.Text = dgridchallan.SelectedRow.Cells[1].Text;

       lbltranporterID.Text = dgridchallan.SelectedRow.Cells[8].Text;       

        txtrec_tcnumber.Text = dgridchallan.SelectedRow.Cells[3].Text;

        txtRec_TruckNumber.Text = dgridchallan.SelectedRow.Cells[4].Text;

    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

   
    decimal CheckNull(string Val)
    {

        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;


    }
    Int32 CheckNullInt(string Val)
    {

        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }

      void getpadyUparjncntr()
    {
        try
        {
            if (con_paddy != null)
            {
                con_paddy.Open();

                //string qrysel = "select (Society_Name+','+SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and Status='Y' order by Society_Name";
                string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + ddldistrict.SelectedValue.ToString() + "' order by ic.SocietyID";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddluparjan.DataSource = ds.Tables[0];
                        ddluparjan.DataTextField = "Society_Name";
                        ddluparjan.DataValueField = "Society_Id";
                        ddluparjan.DataBind();
                        ddluparjan.Items.Insert(0, "--Select--");

                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            con_paddy.Close();
        }
        finally
        {
            con_paddy.Close();
        }


    }
    void getcorgrnUparjncntr()
    {
        try
        {
            if (con_Maze != null)
            {
                con_Maze.Open();
                //string qrysel = "select (Society_Name+','+SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and Status='Y' order by Society_Name";
                string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + ddldistrict.SelectedValue.ToString() + "' order by ic.SocietyID";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddluparjan.DataSource = ds.Tables[0];
                        ddluparjan.DataTextField = "Society_Name";
                        ddluparjan.DataValueField = "Society_Id";
                        ddluparjan.DataBind();
                        ddluparjan.Items.Insert(0, "--Select--");

                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            con_Maze.Close();
        }
        finally
        {
            con_Maze.Close();
        }


    }
    void getWhtUparjncntr()
    {
        try
        {
            if (con_WPMS != null)
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }
               // string qrysel = "select (Society_Name+','+SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and IsWheat='Y' order by Society_Name";
                string qrysel = "select distinct ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')')as Society_Name from IssueToSangrahanaKendra ic inner join Society on Society.Society_Id = ic.SocietyID where ic.DistrictId='" + ddldistrict.SelectedValue.ToString() + "' order by SocietyID";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                DataSet ds = new DataSet();
                da.Fill(ds);
                  if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddluparjan.DataSource = ds.Tables[0];
                        ddluparjan.DataTextField = "Society_Name";
                        ddluparjan.DataValueField = "Society_Id";
                        ddluparjan.DataBind();
                        ddluparjan.Items.Insert(0, "--Select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            con_WPMS.Close();
        }
        finally
        {
            con_WPMS.Close();
        }


    }

    void getpaddyIssueid()
    {
        distp = ddldistrict.SelectedValue.ToString().Substring(2, 2);

        if (ddlcomdty.SelectedValue.ToString() == "1")
        {
            try
            {
                if (con_WPMS != null)
                {
                    if (con_WPMS.State == ConnectionState.Closed)
                    {
                        con_WPMS.Open();
                    }


                    string qrysel = " Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and DistrictId = '" + ddldistrict.SelectedValue + "' order by DateOfIssue desc ";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                          
                                lber.Text = "Data not available for this Purchase Center";
                            }
                        } 
                }
                else
                {
                }
            }

            catch (Exception)
            {

                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }
            }
            finally
            {
                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }

            }
        }

        else if (ddlcomdty.SelectedValue.ToString() == "2" || ddlcomdty.SelectedValue.ToString() == "3")
        {

            try
            {
                if (con_paddy != null)
                {
                    if (distp == did)
                    {
                        //string qrysel = "select (IssueToSangrahanaKendra.IssueID+'/'+IssueToSangrahanaKendra.TruckChalanNo)as Name,IssueToSangrahanaKendra.IssueID from IssueToSangrahanaKendra where IssueToSangrahanaKendra.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and IssueToSangrahanaKendra.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and convert(varchar,DateOfIssue,101)='" + getDate_MDY(txtisdate.Text) + "' and IssueTo not in('OD') ";
                        string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and IssueTo not in('OD') order by DateOfIssue desc ";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;

                                lber.Text = "Data not available for this Purchase Center";
                            }
                        }
                    }
                    else
                    {
                        // string qrysel = "select (IssueToSangrahanaKendra.IssueID+'/'+IssueToSangrahanaKendra.TruckChalanNo)as Name,IssueToSangrahanaKendra.IssueID from IssueToSangrahanaKendra where IssueToSangrahanaKendra.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and IssueToSangrahanaKendra.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and convert(varchar,DateOfIssue,101)='" + getDate_MDY(txtisdate.Text) + "' and SendingDistId ='23" + did + "'";
                        string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and SendingDistId ='23" + did + "' order by DateOfIssue desc";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                             
                                lber.Text = "Data not available for this Purchase Center";
                            }
                        }
                    }

                }
                else
                {
                }
            }

            catch (Exception)
            {

                con_paddy.Close();

            }
            finally
            {
                con_paddy.Close();

            }
        }
        else if (ddlcomdty.SelectedValue.ToString() == "4" || ddlcomdty.SelectedValue.ToString() == "5" || ddlcomdty.SelectedValue.ToString() == "6" || ddlcomdty.SelectedValue.ToString() == "7" || ddlcomdty.SelectedValue.ToString() == "8")
        {
            try
            {
                if (con_Maze != null)
                {
                    con_Maze.Open();
                    if (distp == did)
                    {
                        string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and  ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and IssueTo not in('OD') order by DateOfIssue desc ";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                               
                                lber.Text = "Data not available for this Purchase Center";
                            }
                        }
                    }
                    else
                    {
                        string qrysel = "Select ist.IssueID,ist.TruckChalanNo,ist.TruckNo,CONVERT(varchar,ist.DateOfIssue,106)DateOfIssue,ist.Bags,ist.QtyTransffer,tm.Transporter_Name,ist.TransporterId from IssueToSangrahanaKendra ist left join Crop_Master on Crop_Master.crpcode = ist.CommodityId left join TransportMaster tm on tm.Transporter_ID=ist.TransporterId and ist.SocietyID = tm.SocietyCode where ist.IssueID not in (select IssueCenterReceipt_Online.IssueID from IssueCenterReceipt_Online) and ist.SocietyID='" + ddluparjan.SelectedValue.ToString() + "' and ist.CommodityId='" + ddlcomdty.SelectedValue.ToString() + "' and SendingDistId ='23" + did + "' order by DateOfIssue desc";
                        SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dgridchallan.DataSource = ds.Tables[0];
                                dgridchallan.DataBind();
                                lber.Visible = false;
                                lber.Text = "";
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not available for this Purchase Center for selected date....'); </script> ");
                                dlissuClear();
                                lber.Visible = true;
                          
                                lber.Text = "Data not available for this Purchase Center";
                            }
                        }
                    }

                }
                else
                {
                }
            }
            catch (Exception)
            {
                con_Maze.Close();
            }
            finally
            {
                con_Maze.Close();
            }
        }
    }

    void GetRack()
    {
        int month = int.Parse(DateTime.Today.Month.ToString());
        int year = int.Parse(DateTime.Today.Year.ToString());

        string dist = did;
        ddlracknumber.Items.Insert(0, "--Select--");
        string qreyrac = "select Rack_No  from dbo.tbl_RackMaster where district_code='" + dist + "'";
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

    void getcsms_Commdty() // Wheat
    {
      
        string qry = " SELECT Commodity_Id FROM Procurement_COMMODITY WHERE Proc_Commodity_Id='" + ddlcomdty.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry,con);

        SqlDataAdapter daqry = new SqlDataAdapter(cmd);
        DataSet dsqry = new DataSet();

        daqry.Fill(dsqry);

        if (dsqry.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsqry.Tables[0].Rows[0];
            CSMS_Comid = dr["Commodity_Id"].ToString();
        }
        else
        {
            CSMS_Comid = "";
        }

    }
}
