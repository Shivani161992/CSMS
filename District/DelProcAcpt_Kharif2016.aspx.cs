using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class District_DelProcAcpt_Kharif2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string IC_Id = "", Dist_Id = "";

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    string Con_Paddy = ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString(); //PPMS 2016
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString());

    string Con_Maze = ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString(); //MPMS 2016
    public SqlConnection con_maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                DaintyDate3.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                DaintyDate3.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                DaintyDate3.Attributes.Add("onchange", "return chksqltxt(this)");

                Dist_Id = Session["dist_id"].ToString();

                GetCommodity();
                GetMPIssueCentre();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    private void GetCommodity()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "";

                select = "SELECT Commodity_Name ,Commodity_Id,(CONVERT(VARCHAR,GETDATE(), 105)) As AcptDate  FROM tbl_MetaData_STORAGE_COMMODITY where  Commodity_Id in ('13','14','8','11','12','40') Order By Commodity_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DaintyDate3.Text = ds.Tables[0].Rows[0]["AcptDate"].ToString();

                    ddlcommodtiy.DataSource = ds.Tables[0];
                    ddlcommodtiy.DataTextField = "Commodity_Name";
                    ddlcommodtiy.DataValueField = "Commodity_Id";
                    ddlcommodtiy.DataBind();

                    ddlcommodtiy.SelectedIndex = 4;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    protected void ddlcommodtiy_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIC.SelectedIndex = 0;
        ddlbranchwlc.Items.Clear();
        ddlgodown.Items.Clear();
        ddlAcceptanceNo.Items.Clear();
        ddlTCNo.Items.Clear();
        GridView2.DataSource = "";
        GridView2.DataBind();
        btnRecptSubmit.Enabled = false;
        hdfTruckNo.Value = hdfWHRNo.Value = hdfIssueID.Value = "";
    }

    public void GetMPIssueCentre()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + Session["dist_id"].ToString() + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIC.DataSource = ds.Tables[0];
                    ddlIC.DataTextField = "DepotName";
                    ddlIC.DataValueField = "DepotID";
                    ddlIC.DataBind();
                    ddlIC.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlbranchwlc.Items.Clear();
        ddlgodown.Items.Clear();
        ddlAcceptanceNo.Items.Clear();
        ddlTCNo.Items.Clear();
        GridView2.DataSource = "";
        GridView2.DataBind();
        btnRecptSubmit.Enabled = false;
        hdfTruckNo.Value = hdfWHRNo.Value = hdfIssueID.Value = "";

        if (ddlIC.SelectedIndex > 0)
        {
            Getdepo();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Centre'); </script> ");
            return;
        }

    }

    private void Getdepo()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + ddlIC.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlbranchwlc.DataSource = ds.Tables[0];
                        ddlbranchwlc.DataTextField = "DepotName";
                        ddlbranchwlc.DataValueField = "BranchID";
                        ddlbranchwlc.DataBind();
                        ddlbranchwlc.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + Dist_Id + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlbranchwlc.DataSource = ds.Tables[0];
                        ddlbranchwlc.DataTextField = "DepotName";
                        ddlbranchwlc.DataValueField = "BranchId";
                        ddlbranchwlc.DataBind();
                        ddlbranchwlc.Items.Insert(0, "--Select--");
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

    protected void ddlbranchwlc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlgodown.Items.Clear();
        ddlAcceptanceNo.Items.Clear();
        ddlTCNo.Items.Clear();
        GridView2.DataSource = "";
        GridView2.DataBind();
        btnRecptSubmit.Enabled = false;
        hdfTruckNo.Value = hdfWHRNo.Value = hdfIssueID.Value = "";

        if (ddlbranchwlc.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
            return;
        }
    }

    private void GetGodown()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                Dist_Id = Session["dist_id"].ToString();

                con.Open();
                string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DistrictId= '23" + Dist_Id + "' and BranchId='" + ddlbranchwlc.SelectedValue.ToString() + "' and Remarks = 'Y'";
                da = new SqlDataAdapter(qrysel, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];
                    ddlgodown.DataTextField = "Godown_Name";
                    ddlgodown.DataValueField = "Godown_ID";
                    ddlgodown.DataBind();
                    ddlgodown.Items.Insert(0, "--select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAcceptanceNo.Items.Clear();
        ddlTCNo.Items.Clear();
        GridView2.DataSource = "";
        GridView2.DataBind();
        btnRecptSubmit.Enabled = false;
        hdfTruckNo.Value = hdfWHRNo.Value = hdfIssueID.Value = "";

        if (ddlgodown.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlgodown.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Acceptance Date'); </script> ");
                return;
            }
            else
            {
                string accdate = getDate_MDY(DaintyDate3.Text);
                GetACNo(accdate);
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
    }

    private void GetACNo(string date)
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "Select distinct Acceptance_No from Acceptance_Note_Kharif2016 where convert(varchar(10),Acceptance_Date,105) = convert(varchar(10),'" + date + "',105) and IssueCenter_ID = '" + ddlIC.SelectedValue.ToString() + "' and CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "' and godown='" + ddlgodown.SelectedValue.ToString() + "' order by  Acceptance_No";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAcceptanceNo.DataSource = ds.Tables[0];
                    ddlAcceptanceNo.DataTextField = "Acceptance_No";
                    ddlAcceptanceNo.DataValueField = "Acceptance_No";
                    ddlAcceptanceNo.DataBind();
                    ddlAcceptanceNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Acceptance Number Is Not Available'); </script> ");
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

    protected void ddlAcceptanceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTCNo.Items.Clear();
        GridView2.DataSource = "";
        GridView2.DataBind();
        btnRecptSubmit.Enabled = false;
        hdfTruckNo.Value = hdfWHRNo.Value = hdfIssueID.Value = "";

        if (ddlAcceptanceNo.SelectedIndex > 0)
        {
            GetTCNumber();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Acceptance Number'); </script> ");
            return;
        }
    }

    private void GetTCNumber()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "Select TC_Number from Acceptance_Note_Kharif2016 where IssueCenter_ID = '" + ddlIC.SelectedValue.ToString() + "' and CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "' and godown='" + ddlgodown.SelectedValue.ToString() + "' and Acceptance_No='" + ddlAcceptanceNo.SelectedValue.ToString() + "' order by  TC_Number";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlTCNo.DataSource = ds.Tables[0];
                    ddlTCNo.DataTextField = "TC_Number";
                    ddlTCNo.DataValueField = "TC_Number";
                    ddlTCNo.DataBind();
                    ddlTCNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('TC Number Is Not Available'); </script> ");
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

    protected void ddlTCNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView2.DataSource = "";
        GridView2.DataBind();
        btnRecptSubmit.Enabled = false;
        hdfTruckNo.Value = hdfWHRNo.Value = hdfIssueID.Value = "";

        if (ddlTCNo.SelectedIndex > 0)
        {
            if (ddlcommodtiy.SelectedValue.ToString() == "13" || ddlcommodtiy.SelectedValue.ToString() == "14")
            {
                GetPaddyDetails();
            }
            else
            {
                GetMotaAnaajDetails();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select TC Number'); </script> ");
            return;
        }
    }

    private void GetPaddyDetails()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = "select IssueID, Truck_No , Society_Name +','+ SocPlace as Society,Accept_Qty,Bags  from Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center where Acceptance_Note_Detail.Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "' and Acceptance_Note_Detail.Distt_ID = '23" + Dist_Id + "' and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and TC_Number = '" + ddlTCNo.SelectedValue.ToString() + "' and Acceptance_Note_Detail.godown='" + ddlgodown.SelectedValue.ToString() + "' ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfTruckNo.Value = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    hdfIssueID.Value = ds.Tables[0].Rows[0]["IssueID"].ToString();
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                    CheckDepositorData();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Details Not Available'); </script> ");
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

    private void GetMotaAnaajDetails()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                con.Open();

                string select = "select IssueID, Truck_No , Society_Name +','+ SocPlace as Society,Accept_Qty,Bags  from Acceptance_Note_Detail inner join Society on Society.Society_Id = Acceptance_Note_Detail.Purchase_Center where Acceptance_Note_Detail.Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "' and Acceptance_Note_Detail.Distt_ID = '23" + Dist_Id + "' and IssueCenter_ID = '" + ddlIC.SelectedValue + "' and TC_Number = '" + ddlTCNo.SelectedValue.ToString() + "' and Acceptance_Note_Detail.godown='" + ddlgodown.SelectedValue.ToString() + "' ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfTruckNo.Value = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    hdfIssueID.Value = ds.Tables[0].Rows[0]["IssueID"].ToString();

                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                    CheckDepositorData();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Details Not Available'); </script> ");
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

    private void CheckDepositorData()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                Dist_Id = Session["dist_id"].ToString();
                string challan = ddlTCNo.SelectedValue.ToString();
                string acceptnum = ddlAcceptanceNo.SelectedValue.ToString();
                string trucknum = hdfTruckNo.Value;

                con.Open();
                string qrysel = "Select WHR_Request From Acceptance_Note_Kharif2016 Where CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "' and TC_Number='" + challan + "' and Truck_No='" + trucknum + "' and Acceptance_No='" + acceptnum + "' and IssueID='" + hdfIssueID.Value + "'";
                da = new SqlDataAdapter(qrysel, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string CheckWhrRequest = "";
                    CheckWhrRequest = ds.Tables[0].Rows[0]["WHR_Request"].ToString();
                    if (CheckWhrRequest == "")
                    {
                        hdfWHRNo.Value = "0";
                        btnRecptSubmit.Enabled = true;
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Depositer Form Created! Entry Cannot Deleted'); </script> ");
                        return;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Depositer Form Created! Entry Cannot Deleted'); </script> ");
                    return;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                return;
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

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("dd-MM-yyyy"));
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (ddlgodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Receiving Godown'); </script> ");
            return;
        }
        else if (ddlAcceptanceNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Acceptance Number'); </script> ");
            return;
        }
        else if (ddlTCNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select TC Number'); </script> ");
            return;
        }
        else if (hdfTruckNo.Value == "" || hdfIssueID.Value == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Truck No Or Issue Id Is Not Available'); </script> ");
            return;
        }
        else if (GridView2.Rows.Count <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Details Not Available, Can Not Delete This Entry'); </script> ");
            return;
        }
        else if (hdfWHRNo.Value != "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Depositer Form Created! Entry Cannot Deleted'); </script> ");
            return;
        }
        else
        {
            if (ddlcommodtiy.SelectedValue.ToString() == "13" || ddlcommodtiy.SelectedValue.ToString() == "14")
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            Dist_Id = Session["dist_id"].ToString();
                            string issuecenerid = ddlIC.SelectedValue.ToString();
                            string challan = ddlTCNo.SelectedValue.ToString();
                            string acceptnum = ddlAcceptanceNo.SelectedValue.ToString();
                            string trucknum = hdfTruckNo.Value;

                            if (cons.State == ConnectionState.Closed)
                            {
                                cons.Open();
                            }

                            SqlTransaction trns1;
                            SqlTransaction trns;

                            string mystr = "SELECT * FROM [tbl_Storage_Arrival_Stock] where [DepotId]='" + issuecenerid + "' and AcceptanceNo='" + acceptnum + "' and IssueID='" + hdfIssueID.Value + "' and Commodity_Id='" + ddlcommodtiy.SelectedValue + "' and Truck_No='" + trucknum + "' and Challan_No='" + challan + "'";
                            SqlCommand cmdwhr = new SqlCommand(mystr, cons);
                            SqlDataReader sqldr = cmdwhr.ExecuteReader();
                            sqldr.Read();

                            if (sqldr.HasRows)
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR Created!Entry Cannot Deleted '); </script> ");
                                return;
                            }
                            else
                            {
                                //ClientIP objClientIP = new ClientIP();
                                //string GetIp = (objClientIP.GETIP());

                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }
                                string inslog = "Insert into Acceptance_Note_Kharif2016_Log(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,whrType,CropYear,WHR_Date,DeletedDate,DeletedIP) select Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,whrType,CropYear,WHR_Date,GETDATE(),'" + ip + "' from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                SqlCommand cmd = new SqlCommand(inslog, con);
                                cmd.CommandText = inslog;
                                cmd.Connection = con;
                                int Clog = cmd.ExecuteNonQuery();

                                if (con_paddy.State == ConnectionState.Closed)
                                {
                                    con_paddy.Open();
                                }
                                string inslog1 = "Insert into Acceptance_Note_Detail_DelLog(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,ReferenceNO,TransactionID,Proc_Flag,PaymentDate,TotalAmount,PaymentIP,RemQty) select Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,ReferenceNO,TransactionID,Proc_Flag,PaymentDate,TotalAmount,PaymentIP,RemQty from Acceptance_Note_Detail where Distt_ID = '23" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                SqlCommand cmd1 = new SqlCommand(inslog1, con_paddy);
                                cmd1.CommandText = inslog1;
                                cmd1.Connection = con_paddy;
                                int y = cmd1.ExecuteNonQuery();

                                if (con_paddy.State == ConnectionState.Closed)
                                {
                                    con_paddy.Open();
                                }

                                SqlCommand cmdC = new SqlCommand();
                                SqlCommand cmdW = new SqlCommand();

                                cmdW.Connection = con_paddy;
                                trns1 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmdW.Transaction = trns1;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                cmdC.Connection = con;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmdC.Transaction = trns;

                                string delcon = "delete from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                cmdC.CommandText = delcon;
                                cmdC.Connection = con;

                                string delwpms = "delete from Acceptance_Note_Detail where Distt_ID = '23" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                cmdW.CommandText = delwpms;
                                cmdW.Connection = con_paddy;

                                try
                                {
                                    int b = cmdW.ExecuteNonQuery();
                                    int a = cmdC.ExecuteNonQuery();

                                    if (a >= 1)
                                    {
                                        trns1.Commit();
                                        trns.Commit();

                                        string update = "Update SCSC_Procurement_Kharif2016 set Acceptance_No = '' , AN_Status = 'N' where Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_Number = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                        SqlCommand updcmd = new SqlCommand(update, con);
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        updcmd.ExecuteNonQuery();

                                        string updateWpms = "Update IssueCenterReceipt_Online set  AN_Status = 'N' where DistrictId = '23" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TruckChalanNo = '" + challan + "' and TruckNo = '" + trucknum + "' and IssueID = '" + hdfIssueID.Value + "'";
                                        SqlCommand uptwpms = new SqlCommand(updateWpms, con_paddy);
                                        if (con_paddy.State == ConnectionState.Closed)
                                        {
                                            con_paddy.Open();
                                        }
                                        uptwpms.ExecuteNonQuery();
                                    }

                                    btnRecptSubmit.Enabled = false;
                                    ddlcommodtiy.Enabled = ddlIC.Enabled = ddlgodown.Enabled = ddlbranchwlc.Enabled = ddlAcceptanceNo.Enabled = ddlTCNo.Enabled = false;
                                    ddlgodown.Items.Clear();
                                    ddlAcceptanceNo.Items.Clear();
                                    ddlTCNo.Items.Clear();

                                    Label2.Visible = true;
                                    Label2.Text = "Data Deleted Sucessfully";

                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Deleted Sucessfully'); </script> ");
                                }

                                catch
                                {
                                    trns1.Rollback();
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
                                    return;
                                }

                                finally
                                {
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }

                                    if (con_paddy.State == ConnectionState.Open)
                                    {
                                        con_paddy.Close();
                                    }

                                    if (cons.State == ConnectionState.Open)
                                    {
                                        cons.Close();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                            return;
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
                else
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            else
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            Dist_Id = Session["dist_id"].ToString();
                            string issuecenerid = ddlIC.SelectedValue.ToString();
                            string challan = ddlTCNo.SelectedValue.ToString();
                            string acceptnum = ddlAcceptanceNo.SelectedValue.ToString();
                            string trucknum = hdfTruckNo.Value;

                            if (cons.State == ConnectionState.Closed)
                            {
                                cons.Open();
                            }

                            SqlTransaction trns1;
                            SqlTransaction trns;

                            string mystr = "SELECT * FROM [tbl_Storage_Arrival_Stock] where [DepotId]='" + issuecenerid + "' and AcceptanceNo='" + acceptnum + "' and IssueID='" + hdfIssueID.Value + "' and Commodity_Id='" + ddlcommodtiy.SelectedValue + "' and Truck_No='" + trucknum + "' and Challan_No='" + challan + "'";
                            SqlCommand cmdwhr = new SqlCommand(mystr, cons);
                            SqlDataReader sqldr = cmdwhr.ExecuteReader();
                            sqldr.Read();

                            if (sqldr.HasRows)
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR created!Entry Cannot Deleted '); </script> ");
                                return;
                            }
                            else
                            {
                                //ClientIP objClientIP = new ClientIP();
                                //string GetIp = (objClientIP.GETIP());

                                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                string inslog = "Insert into Acceptance_Note_Kharif2016_Log(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,whrType,CropYear,WHR_Date,DeletedDate,DeletedIP) select Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,whrType,CropYear,WHR_Date,GETDATE(),'" + ip + "' from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                SqlCommand cmd = new SqlCommand(inslog, con);
                                cmd.CommandText = inslog;
                                cmd.Connection = con;
                                int Clog = cmd.ExecuteNonQuery();

                                if (con_maze.State == ConnectionState.Closed)
                                {
                                    con_maze.Open();
                                }

                                string inslog1 = "Insert into Acceptance_Note_Detail_DelLog(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,ReferenceNO,TransactionID,Proc_Flag,PaymentDate,TotalAmount,PaymentIP,RemQty) select Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,ReferenceNO,TransactionID,Proc_Flag,PaymentDate,TotalAmount,PaymentIP,RemQty from Acceptance_Note_Detail where Distt_ID = '23" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                SqlCommand cmd1 = new SqlCommand(inslog1, con_maze);
                                cmd1.CommandText = inslog1;
                                cmd1.Connection = con_maze;
                                int y = cmd1.ExecuteNonQuery();

                                SqlCommand cmdC = new SqlCommand();
                                SqlCommand cmdW = new SqlCommand();

                                cmdW.Connection = con_maze;
                                trns1 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmdW.Transaction = trns1;

                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                cmdC.Connection = con;
                                trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                cmdC.Transaction = trns;

                                string delcon = "delete from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                cmdC.CommandText = delcon;
                                cmdC.Connection = con;

                                string delwpms = "delete from Acceptance_Note_Detail where Distt_ID = '23" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_No = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                cmdW.CommandText = delwpms;
                                cmdW.Connection = con_maze;

                                try
                                {
                                    int b = cmdW.ExecuteNonQuery();
                                    int a = cmdC.ExecuteNonQuery();

                                    if (a >= 1)
                                    {
                                        trns1.Commit();
                                        trns.Commit();

                                        string update = "Update SCSC_Procurement_Kharif2016 set Acceptance_No = '' , AN_Status = 'N' where Distt_ID = '" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TC_Number = '" + challan + "' and Truck_Number = '" + trucknum + "' and Acceptance_No = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                        SqlCommand updcmd = new SqlCommand(update, con);
                                        if (con.State == ConnectionState.Closed)
                                        {
                                            con.Open();
                                        }
                                        updcmd.ExecuteNonQuery();

                                        string updateWpms = "Update IssueCenterReceipt_Online set  AN_Status = 'N' where DistrictId = '23" + Dist_Id + "' and IssueCenter_ID = '" + issuecenerid + "' and TruckChalanNo = '" + challan + "' and TruckNo = '" + trucknum + "' and IssueID = '" + hdfIssueID.Value + "'";
                                        SqlCommand uptwpms = new SqlCommand(updateWpms, con_maze);
                                        if (con_maze.State == ConnectionState.Closed)
                                        {
                                            con_maze.Open();
                                        }
                                        uptwpms.ExecuteNonQuery();
                                    }

                                    btnRecptSubmit.Enabled = false;
                                    ddlcommodtiy.Enabled = ddlIC.Enabled = ddlgodown.Enabled = ddlbranchwlc.Enabled = ddlAcceptanceNo.Enabled = ddlTCNo.Enabled = false;
                                    ddlgodown.Items.Clear();
                                    ddlAcceptanceNo.Items.Clear();
                                    ddlTCNo.Items.Clear();

                                    Label2.Visible = true;
                                    Label2.Text = "Data Deleted Sucessfully";

                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Deleted Sucessfully'); </script> ");
                                }

                                catch
                                {
                                    trns1.Rollback();
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
                                    return;
                                }

                                finally
                                {
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }

                                    if (con_maze.State == ConnectionState.Open)
                                    {
                                        con_maze.Close();
                                    }

                                    if (cons.State == ConnectionState.Open)
                                    {
                                        cons.Close();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                            return;
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
                else
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

}