using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class District_DelProcDepositor_Kharif2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string IC_Id = "", Dist_Id = "";
    public string getdatef = "";
    decimal aceqty = 0;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
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
        hdfWHRNo.Value = "";

        ddlIC.SelectedIndex = 0;
        ddlbranchwlc.Items.Clear();
        ddlgodown.Items.Clear();
        ddlAcceptanceNo.Items.Clear();
        dgridchallan.DataSource = "";
        dgridchallan.DataBind();
        btnRecptSubmit.Enabled = false;
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
        hdfWHRNo.Value = "";

        ddlbranchwlc.Items.Clear();
        ddlgodown.Items.Clear();
        ddlAcceptanceNo.Items.Clear();
        dgridchallan.DataSource = "";
        dgridchallan.DataBind();
        btnRecptSubmit.Enabled = false;

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
        hdfWHRNo.Value = "";

        ddlgodown.Items.Clear();
        ddlAcceptanceNo.Items.Clear();
        dgridchallan.DataSource = "";
        dgridchallan.DataBind();
        btnRecptSubmit.Enabled = false;

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
        hdfWHRNo.Value = "";

        ddlAcceptanceNo.Items.Clear();
        dgridchallan.DataSource = "";
        dgridchallan.DataBind();
        btnRecptSubmit.Enabled = false;

        if (ddlgodown.SelectedIndex > 0)
        {
            if (DaintyDate3.Text == "")
            {
                ddlgodown.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Depositer Date'); </script> ");
                return;
            }
            else
            {
                string accdate = getDate_MDY(DaintyDate3.Text);
                GetDepositerNo(accdate);
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
            return;
        }
    }

    private void GetDepositerNo(string date)
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                Dist_Id = Session["dist_id"].ToString();

                con.Open();

                string select = " ";

                select = "Select distinct WHR_Request From Acceptance_Note_Kharif2016 Where Distt_ID='" + Dist_Id + "' and IssueCenter_ID='" + ddlIC.SelectedValue.ToString() + "' and CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "'and convert(varchar(10),Acceptance_Date,105) = convert(varchar(10),'" + date + "',105) and godown='" + ddlgodown.SelectedValue.ToString() + "' and Accept_Qty!='0' and WHR_Request IS NOT NULL and WHR_Request !='0' Order by WHR_Request";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAcceptanceNo.DataSource = ds.Tables[0];
                    ddlAcceptanceNo.DataTextField = "WHR_Request";
                    ddlAcceptanceNo.DataValueField = "WHR_Request";
                    ddlAcceptanceNo.DataBind();
                    ddlAcceptanceNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Depositer Number Is Not Available'); </script> ");
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
        hdfWHRNo.Value = "";

        dgridchallan.DataSource = "";
        dgridchallan.DataBind();
        btnRecptSubmit.Enabled = false;

        if (ddlAcceptanceNo.SelectedIndex > 0)
        {
            fillgrid();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Depositer Number'); </script> ");
            return;
        }
    }

    private void fillgrid()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();

                string select = "SELECT Acceptance_Note_Kharif2016.*,tbl_MetaData_GODOWN.Godown_Name,tbl_MetaData_DEPOT.DepotName, tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,Society.Society_Name +','+ SocPlace +',' + Society_Id as PurchaseCenterName FROM dbo.Acceptance_Note_Kharif2016 left join dbo.tbl_MetaData_STORAGE_COMMODITY on Acceptance_Note_Kharif2016.CommodityId=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join SocietyKharif2016 As Society on Acceptance_Note_Kharif2016.Purchase_Center= Society.Society_Id inner join tbl_MetaData_DEPOT on Acceptance_Note_Kharif2016.IssueCenter_ID=tbl_MetaData_DEPOT.DepotID inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID=Acceptance_Note_Kharif2016.godown  where Distt_ID='" + Dist_Id + "'  and IssueCenter_ID='" + ddlIC.SelectedValue.ToString() + "'  and CommodityId='" + ddlcommodtiy.SelectedValue.ToString() + "' and godown='" + ddlgodown.SelectedValue.ToString() + "' and  WHR_Request='" + ddlAcceptanceNo.SelectedValue.ToString() + "' order by Acceptance_Date desc";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dgridchallan.DataSource = ds.Tables[0];
                    dgridchallan.DataBind();
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
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                Dist_Id = Session["dist_id"].ToString();

                con.Open();
                string qrysel = " SELECT * FROM [tbl_Storage_Arrival_Stock] where  AcceptanceNo='" + ddlAcceptanceNo.SelectedValue.ToString() + "'  and Commodity_Id='" + ddlcommodtiy.SelectedValue.ToString() + "' ";
                da = new SqlDataAdapter(qrysel, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR Created! Entry Cannot Deleted'); </script> ");
                    return;
                }
                else
                {
                    hdfWHRNo.Value = "0";
                    btnRecptSubmit.Enabled = true;
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

    protected void dgridchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Acceptance_Date"));
            getdatef = getdate(griddate);

            Label lbl = (Label)e.Row.FindControl("lblChallan");
            lbl.Text = getdatef;
            aceqty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Accept_Qty"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblqty1 = (Label)e.Row.FindControl("lbl_acqt");
            lblqty1.Text = aceqty.ToString();
        }
    }

    public void FooterPagerClick(object sender, CommandEventArgs e)
    {
        string arg;
        arg = e.CommandArgument.ToString();

        switch (arg)
        {
            case "next":

                if ((dgridchallan.PageIndex < (dgridchallan.PageCount - 1)))
                {
                    dgridchallan.PageIndex += 1;
                }

                break;

            case "prev":

                if ((dgridchallan.PageIndex > 0))
                {
                    dgridchallan.PageIndex -= 1;
                }

                break;

            case "last":

                dgridchallan.PageIndex = (dgridchallan.PageCount - 1);
                break;

            default:

                dgridchallan.PageIndex = Convert.ToInt32(arg);
                break;
        }
        fillgrid();
    }

    protected void chk_del_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in dgridchallan.Rows)
        {
            if (((CheckBox)row.FindControl("chk_del")).Checked == true)
            {
                //((CheckBox)row.FindControl("chk_receipt")).Enabled = true;
            }
            else
            {
                //((CheckBox)row.FindControl("chk_receipt")).Enabled = false;
            }

        }

    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
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
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Depositer Number'); </script> ");
            return;
        }
        else if (dgridchallan.Rows.Count <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Details Not Available, Can Not Delete This Entry'); </script> ");
            return;
        }
        else if (hdfWHRNo.Value != "0")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR Created! Entry Cannot Deleted'); </script> ");
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
                            string acceptnum = ddlAcceptanceNo.SelectedValue.ToString();

                            //ClientIP objClientIP = new ClientIP();
                            //string GetIp = (objClientIP.GETIP());

                            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                            for (int i = 0; i < dgridchallan.Rows.Count; i++)
                            {
                                if (((CheckBox)dgridchallan.Rows[i].FindControl("chk_del")).Checked == true)
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    string challan = dgridchallan.Rows[i].Cells[2].Text.ToString();
                                    string trucknum = dgridchallan.Rows[i].Cells[4].Text.ToString();
                                    string acnote = dgridchallan.Rows[i].Cells[1].Text.ToString();
                                    string Commodity_Grid = dgridchallan.DataKeys[i].Values[0].ToString();

                                    string inslog = "Insert into Acceptance_Note_Kharif2016_Log(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,whrType,CropYear,WHR_Date,DeletedDate,DeletedIP) select Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,whrType,CropYear,WHR_Date,GETDATE(),'" + ip + "' from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "'  and Acceptance_No='" + acnote + "' and  TC_Number = '" + challan + "' and  WHR_Request = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                    SqlCommand cmd = new SqlCommand(inslog, con);

                                    string delcon = "Update Acceptance_Note_Kharif2016 set WHR_Request = NULL , Updates_Date =  getdate() where Distt_ID = '" + Dist_Id + "'  and Acceptance_No='" + acnote + "' and TC_Number = '" + challan + "' and  WHR_Request = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                    SqlCommand cmdcon = new SqlCommand(delcon, con);

                                    if (con_paddy.State == ConnectionState.Closed)
                                    {
                                        con_paddy.Open();
                                    }
                                    string inslog1 = "Insert into Acceptance_Note_Detail_DelLog(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,ReferenceNO,TransactionID,Proc_Flag,PaymentDate,TotalAmount,PaymentIP,RemQty) select Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,ReferenceNO,TransactionID,Proc_Flag,PaymentDate,TotalAmount,PaymentIP,RemQty from Acceptance_Note_Detail where Distt_ID = '23" + Dist_Id + "' and Acceptance_No='" + acnote + "' and TC_Number = '" + challan + "'  and WHR_Request = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                    SqlCommand cmd1 = new SqlCommand(inslog1, con_paddy);

                                    string delwpms = "Update Acceptance_Note_Detail set WHR_Request = NULL , Updates_Date =  getdate() where Distt_ID = '23" + Dist_Id + "'  and Acceptance_No='" + acnote + "' and TC_Number = '" + challan + "' and WHR_Request = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                    SqlCommand cmdwpms = new SqlCommand(delwpms, con_paddy);

                                    try
                                    {
                                        int x = cmd.ExecuteNonQuery();
                                        int y = cmd1.ExecuteNonQuery();
                                        int a = cmdcon.ExecuteNonQuery();
                                        int b = cmdwpms.ExecuteNonQuery();

                                        btnRecptSubmit.Enabled = false;
                                        ddlcommodtiy.Enabled = ddlIC.Enabled = ddlgodown.Enabled = ddlbranchwlc.Enabled = ddlAcceptanceNo.Enabled = false;
                                        Label2.Visible = true;
                                        Label2.Text = "Depositor Form Deleted Sucessfully";

                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Depositor Form Deleted Sucessfully'); </script> ");
                                    }
                                    catch (Exception ex)
                                    {
                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
                                        return;
                                    }

                                    finally
                                    {
                                        if (con_paddy.State == ConnectionState.Open)
                                        {
                                            con_paddy.Close();
                                        }

                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }
                                    }
                                }
                            }
                            dgridchallan.DataBind();
                        }
                        catch (Exception ex)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                            return;
                        }

                        finally
                        {
                            btnRecptSubmit.Enabled = false;
                            ddlcommodtiy.Enabled = ddlIC.Enabled = ddlgodown.Enabled = ddlbranchwlc.Enabled = ddlAcceptanceNo.Enabled = false;

                            ddlgodown.Items.Clear();
                            ddlAcceptanceNo.Items.Clear();

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
                            string acceptnum = ddlAcceptanceNo.SelectedValue.ToString();

                            //ClientIP objClientIP = new ClientIP();
                            //string GetIp = (objClientIP.GETIP());

                            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                            for (int i = 0; i < dgridchallan.Rows.Count; i++)
                            {
                                if (((CheckBox)dgridchallan.Rows[i].FindControl("chk_del")).Checked == true)
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    string challan = dgridchallan.Rows[i].Cells[2].Text.ToString();
                                    string trucknum = dgridchallan.Rows[i].Cells[4].Text.ToString();
                                    string acnote = dgridchallan.Rows[i].Cells[1].Text.ToString();
                                    string Commodity_Grid = dgridchallan.DataKeys[i].Values[0].ToString();

                                    string inslog = "Insert into Acceptance_Note_Kharif2016_Log(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,whrType,CropYear,WHR_Date,DeletedDate,DeletedIP) select Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,whrType,CropYear,WHR_Date,GETDATE(),'" + ip + "' from Acceptance_Note_Kharif2016 where Distt_ID = '" + Dist_Id + "'  and Acceptance_No='" + acnote + "' and  TC_Number = '" + challan + "' and  WHR_Request = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                    SqlCommand cmd = new SqlCommand(inslog, con);

                                    string delcon = "Update Acceptance_Note_Kharif2016 set WHR_Request = NULL , Updates_Date =  getdate() where Distt_ID = '" + Dist_Id + "'  and Acceptance_No='" + acnote + "' and TC_Number = '" + challan + "' and  WHR_Request = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                    SqlCommand cmdcon = new SqlCommand(delcon, con);

                                    if (con_maze.State == ConnectionState.Closed)
                                    {
                                        con_maze.Open();
                                    }
                                    string inslog1 = "Insert into Acceptance_Note_Detail_DelLog(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,ReferenceNO,TransactionID,Proc_Flag,PaymentDate,TotalAmount,PaymentIP,RemQty) select Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,State_Id,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,Stiching_bags_Good,Stiching_bags_Bad,Stencile_bags_Good,Stencile_bags_Bad,godown,WHR_Request,reject_Bags,WhrNumber,ReferenceNO,TransactionID,Proc_Flag,PaymentDate,TotalAmount,PaymentIP,RemQty from Acceptance_Note_Detail where Distt_ID = '23" + Dist_Id + "' and Acceptance_No='" + acnote + "' and TC_Number = '" + challan + "'  and WHR_Request = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                    SqlCommand cmd1 = new SqlCommand(inslog1, con_maze);

                                    string delwpms = "Update Acceptance_Note_Detail set WHR_Request = NULL , Updates_Date =  getdate() where Distt_ID = '23" + Dist_Id + "'  and Acceptance_No='" + acnote + "' and TC_Number = '" + challan + "' and WHR_Request = '" + ddlAcceptanceNo.SelectedValue.ToString() + "'";
                                    SqlCommand cmdwpms = new SqlCommand(delwpms, con_maze);

                                    try
                                    {
                                        int x = cmd.ExecuteNonQuery();
                                        int y = cmd1.ExecuteNonQuery();
                                        int a = cmdcon.ExecuteNonQuery();
                                        int b = cmdwpms.ExecuteNonQuery();

                                        btnRecptSubmit.Enabled = false;
                                        ddlcommodtiy.Enabled = ddlIC.Enabled = ddlgodown.Enabled = ddlbranchwlc.Enabled = ddlAcceptanceNo.Enabled = false;
                                        Label2.Visible = true;
                                        Label2.Text = "Depositor Form Deleted Sucessfully";

                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Depositor Form Deleted Sucessfully'); </script> ");
                                    }
                                    catch (Exception ex)
                                    {
                                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error! To Delete This Entry'); </script> ");
                                        return;
                                    }

                                    finally
                                    {
                                        if (con_maze.State == ConnectionState.Open)
                                        {
                                            con_maze.Close();
                                        }

                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }
                                    }
                                }
                            }
                            dgridchallan.DataBind();
                        }
                        catch (Exception ex)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                            return;
                        }

                        finally
                        {
                            btnRecptSubmit.Enabled = false;
                            ddlcommodtiy.Enabled = ddlIC.Enabled = ddlgodown.Enabled = ddlbranchwlc.Enabled = ddlAcceptanceNo.Enabled = false;

                            ddlgodown.Items.Clear();
                            ddlAcceptanceNo.Items.Clear();

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