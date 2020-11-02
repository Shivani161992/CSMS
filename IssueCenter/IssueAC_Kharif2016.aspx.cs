using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;

public partial class IssueCenter_IssueAC_Kharif2016 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd, cmd1;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string IC_Id = "", Dist_Id = "";
    decimal total1 = 0, qty = 0;

    int jutnew = 0, ppbag = 0, juteold = 0;

    string Con_CSMS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string Con_WH = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    string Con_Paddy = ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString(); //PPMS 2016
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMS2016"].ToString());

    string Con_Maze = ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString(); //MPMS 2016
    public SqlConnection con_maze = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2016"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        string opid = Session["OperatorId"].ToString();

        if (Session["issue_id"] != null && (opid == "BM" || opid == "DMO"))
        {
            if (!IsPostBack)
            {
                Session["Acceptance_NO"] = "";
                Session["Godown"] = "";

                txtAccDate.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtAccDate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtAccDate.Attributes.Add("onchange", "return chksqltxt(this)");

                txtYear.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtYear.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtYear.Attributes.Add("onchange", "return chksqltxt(this)");

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                GetICName();
                GetCommodity();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        //DaintyDate3P.Text = Request.Form[DaintyDate3P.UniqueID];
    }

    private void GetICName()
    {
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "select DepotName,(CONVERT(VARCHAR,GETDATE(), 105)) As AcptDate  from dbo.tbl_MetaData_DEPOT where DepotID='" + IC_Id + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //txtissue.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    DaintyDate3P.Text = txtAccDate.Text = ds.Tables[0].Rows[0]["AcptDate"].ToString();
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

    private void GetCommodity()
    {
        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select crop,crpcode from Crop_Master where crpcode IN ('2','3','4','5','6','7')";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlmarksesn.DataSource = ds.Tables[0];
                    ddlmarksesn.DataTextField = "crop";
                    ddlmarksesn.DataValueField = "crpcode";
                    ddlmarksesn.DataBind();
                    ddlmarksesn.Items.Insert(0, "--Select--");

                    //ddlmarksesn.SelectedIndex = 1;
                    getpadyDist();
                    //string distid = ddldistproment.SelectedValue.ToString();
                    //getsociety(distid);
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

    protected void ddlmarksesn_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldistproment.Items.Clear();
        ddlpurchcenterP.Items.Clear();
        ddlgodown.Items.Clear();

        hdfGodownCode.Value = hdfCSMS_Comid.Value = "";

        GridView2.DataSource = null;
        GridView2.DataBind();

        if (DaintyDate3P.Text != "")
        {
            if (ddlmarksesn.SelectedIndex > 0)
            {
                if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
                {
                    getpadyDist();
                    string distid = ddldistproment.SelectedValue.ToString();
                    getsociety(distid);
                }
                else
                {
                    getMotaAnaajDist();
                    string distid = ddldistproment.SelectedValue.ToString();
                    getMotaAnaajsociety(distid);
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
                return;
            }
        }
        else
        {
            ddlmarksesn.SelectedIndex = 0;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Deposit'); </script> ");
            return;
        }

    }

    private void getpadyDist()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select District_Name,District_Code from Districts order by District_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldistproment.DataSource = ds.Tables[0];
                    ddldistproment.DataTextField = "District_Name";
                    ddldistproment.DataValueField = "District_Code";
                    ddldistproment.DataBind();
                    ddldistproment.Items.Insert(0, "--Select--");
                    ddldistproment.Items.FindByValue(23 + Dist_Id).Selected = true;
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

    private void getMotaAnaajDist()
    {
        Dist_Id = Session["dist_id"].ToString();

        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                con.Open();

                string select = "";

                select = "select District_Name,District_Code from Districts order by District_Name";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddldistproment.DataSource = ds.Tables[0];
                    ddldistproment.DataTextField = "District_Name";
                    ddldistproment.DataValueField = "District_Code";
                    ddldistproment.DataBind();
                    ddldistproment.Items.Insert(0, "--Select--");
                    ddldistproment.Items.FindByValue(23 + Dist_Id).Selected = true;
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

    protected void ddldistproment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpurchcenterP.Items.Clear();
        ddlgodown.Items.Clear();

        hdfGodownCode.Value = hdfCSMS_Comid.Value = "";

        GridView2.DataSource = null;
        GridView2.DataBind();

        if (ddlmarksesn.SelectedIndex > 0)
        {
            if (DaintyDate3P.Text != "")
            {
                if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
                {
                    string distid = ddldistproment.SelectedValue.ToString();
                    getsociety(distid);
                }
                else
                {
                    string distid = ddldistproment.SelectedValue.ToString();
                    getMotaAnaajsociety(distid);
                }
            }
            else
            {
                ddldistproment.SelectedIndex = 0;
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Date of Deposit'); </script> ");
                return;
            }

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Sending District'); </script> ");
            return;
        }
    }

    private void getsociety(string distid)
    {
        IC_Id = Session["issue_id"].ToString();
        string pdate = getDate_MDY(DaintyDate3P.Text);

        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                con.Open();

                string qrysel = "";

                qrysel = "select ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')''('+ cast(COUNT(IssueID)as varchar(50)) + ')') as Society_Name from IssueCenterReceipt_Online ic inner join Society on Society.Society_Id = ic.SocietyID where ic.Sending_District='" + ddldistproment.SelectedValue.ToString() + "' and ic.IssueCenter_ID='" + IC_Id + "' and ic.Recd_Date='" + pdate + "' and ic.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and ic.AN_Status='N' and ic.Recd_Godown != 'Rejected'  group by ic.SocietyID ,Society.Society_Name,Society.SocPlace, ic.SocietyID ";
                da = new SqlDataAdapter(qrysel, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlpurchcenterP.DataSource = ds.Tables[0];
                        ddlpurchcenterP.DataTextField = "Society_Name";
                        ddlpurchcenterP.DataValueField = "Society_Id";
                        ddlpurchcenterP.DataBind();
                        ddlpurchcenterP.Items.Insert(0, "--select--");
                    }
                    //else
                    //{
                    //    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ddldistproment.SelectedItem.ToString() + " जिले के लिए " + DaintyDate3P.Text + " की दिनांक में कोई भी Purchase Center उपलब्ध नहीं हैं|'); </script> ");
                    //    return;
                    //}
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

    private void getMotaAnaajsociety(string distid)
    {
        IC_Id = Session["issue_id"].ToString();
        string pdate = getDate_MDY(DaintyDate3P.Text);

        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                con.Open();

                string qrysel = "";

                qrysel = "select ic.SocietyID as Society_Id,(Society.Society_Name+','+Society.SocPlace+'('+ ic.SocietyID +')''('+ cast(COUNT(IssueID)as varchar(50)) + ')') as Society_Name from IssueCenterReceipt_Online ic inner join Society on Society.Society_Id = ic.SocietyID where ic.Sending_District='" + ddldistproment.SelectedValue.ToString() + "' and ic.IssueCenter_ID='" + IC_Id + "' and ic.Recd_Date='" + pdate + "' and ic.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and ic.AN_Status='N' and ic.Recd_Godown != 'Rejected'  group by ic.SocietyID ,Society.Society_Name,Society.SocPlace, ic.SocietyID ";
                da = new SqlDataAdapter(qrysel, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlpurchcenterP.DataSource = ds.Tables[0];
                        ddlpurchcenterP.DataTextField = "Society_Name";
                        ddlpurchcenterP.DataValueField = "Society_Id";
                        ddlpurchcenterP.DataBind();
                        ddlpurchcenterP.Items.Insert(0, "--select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ddldistproment.SelectedItem.ToString() + " जिले के लिए " + DaintyDate3P.Text + " की दिनांक में कोई भी Purchase Center उपलब्ध नहीं हैं|'); </script> ");
                        return;
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

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void ddlpurchcenterP_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfGodownCode.Value = hdfCSMS_Comid.Value = "";
        ddlgodown.Items.Clear();
        GridView2.DataSource = null;
        GridView2.DataBind();

        if (ddlpurchcenterP.SelectedIndex > 0)
        {
            GetGodownData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center'); </script> ");
            return;
        }
    }

    private void GetGodownData()
    {
        string pdate = getDate_MDY(DaintyDate3P.Text);

        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "SELECT distinct Recd_Godown FROM SCSC_Procurement_Kharif2016 where Purchase_Center = '" + ddlpurchcenterP.SelectedValue.ToString() + "' and Recd_Date = '" + pdate + "' and AN_Status = 'N' and Book_No!='Rejected'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        hdfGodownCode.Value += ((hdfGodownCode.Value == "") ? "" : " , ") + "'" + ds.Tables[0].Rows[i]["Recd_Godown"].ToString() + "'";
                    }

                    if (hdfGodownCode.Value != "")
                    {
                        GetGodownName();
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

    public void GetGodownName()
    {
        using (con = new SqlConnection(Con_WH))
        {
            try
            {
                con.Open();
                string select = "select Godown_Name,Godown_ID from tbl_MetaData_GODOWN where Godown_ID IN (" + hdfGodownCode.Value + ")";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlgodown.DataSource = ds.Tables[0];
                        ddlgodown.DataTextField = "Godown_Name";
                        ddlgodown.DataValueField = "Godown_ID";
                        ddlgodown.DataBind();
                        ddlgodown.Items.Insert(0, "--Select--");
                    }
                }
                else
                {

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

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfCSMS_Comid.Value = "";

        GridView2.DataSource = null;
        GridView2.DataBind();

        if (ddlgodown.SelectedIndex > 0)
        {
            getcsms_Commdty();

            if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
            {
                bindgrid();
            }
            else
            {
                bindgridMotaAnaaj();
            }
            txtAccDate.Text = DaintyDate3P.Text;
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
            return;
        }
    }

    private void getcsms_Commdty()
    {
        hdfCSMS_Comid.Value = "";
        using (con = new SqlConnection(Con_CSMS))
        {
            try
            {
                con.Open();
                string select = "SELECT Commodity_Id FROM Procurement_COMMODITY WHERE Proc_Commodity_Id='" + ddlmarksesn.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfCSMS_Comid.Value = ds.Tables[0].Rows[0]["Commodity_Id"].ToString();
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

    private void bindgrid()
    {
        GridView2.DataSource = null;
        GridView2.DataBind();

        using (con = new SqlConnection(Con_Paddy))
        {
            try
            {
                string pdate = getDate_MDY(DaintyDate3P.Text);
                string mpc = ddlpurchcenterP.SelectedValue;

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                con.Open();
                string qrydata = "";

                qrydata = " select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.RecdQty_Faq,IssueCenterReceipt_Online.RecdQty_Urs , IssueCenterReceipt_Online.IssueID, IssueCenterReceipt_Online.RecdBags_JuteNew , IssueCenterReceipt_Online.RecdBags_PP , IssueCenterReceipt_Online.RecdBags_JuteOld ,Crop_Master.crop as Commodity_Name ,IssueCenterReceipt_Online.CropYear from IssueCenterReceipt_Online left join Crop_Master on  Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId  where IssueCenterReceipt_Online.DistrictId='23" + Dist_Id + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + IC_Id + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '" + ddlgodown.SelectedValue + "' and IssueCenterReceipt_Online.CommodityId = '" + ddlmarksesn.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(qrydata, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + Dist_Id + "' and IssueCenter_ID='" + IC_Id + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and godown = '" + ddlgodown.SelectedValue + "'";
                        da1 = new SqlDataAdapter(getacdata, con);
                        ds1 = new DataSet();
                        da1.Fill(ds1);

                        if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            DataRow drac = ds1.Tables[0].Rows[0];
                            string acno = drac["Acceptance_No"].ToString();
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Acceptance Note No..." + acno + "...  Already Issued for this Purchase Center for selected date'); </script> ");
                            return;
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Currently No Record Found'); </script> ");
                            return;
                        }
                    }
                    else
                    {
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
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

    private void bindgridMotaAnaaj()
    {
        GridView2.DataSource = null;
        GridView2.DataBind();

        using (con = new SqlConnection(Con_Maze))
        {
            try
            {
                string pdate = getDate_MDY(DaintyDate3P.Text);
                string mpc = ddlpurchcenterP.SelectedValue;

                IC_Id = Session["issue_id"].ToString();
                Dist_Id = Session["dist_id"].ToString();

                con.Open();
                string qrydata = "";

                qrydata = " select IssueCenterReceipt_Online.TruckChalanNo as TC_Number,IssueCenterReceipt_Online.TruckNo as Truck_Number,IssueCenterReceipt_Online.RecdQty_Faq,IssueCenterReceipt_Online.RecdQty_Urs , IssueCenterReceipt_Online.IssueID, IssueCenterReceipt_Online.RecdBags_JuteNew , IssueCenterReceipt_Online.RecdBags_PP , IssueCenterReceipt_Online.RecdBags_JuteOld ,Crop_Master.crop as Commodity_Name ,IssueCenterReceipt_Online.CropYear from IssueCenterReceipt_Online left join Crop_Master on  Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId  where IssueCenterReceipt_Online.DistrictId='23" + Dist_Id + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + IC_Id + "' and IssueCenterReceipt_Online.SocietyID='" + mpc + "' and IssueCenterReceipt_Online.Recd_Date='" + pdate + "' and IssueCenterReceipt_Online.CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and IssueCenterReceipt_Online.AN_Status='N' and IssueCenterReceipt_Online.Recd_Godown = '" + ddlgodown.SelectedValue + "' and IssueCenterReceipt_Online.CommodityId = '" + ddlmarksesn.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(qrydata, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        string getacdata = "select * from dbo.Acceptance_Note_Detail where  Distt_ID='23" + Dist_Id + "' and IssueCenter_ID='" + IC_Id + "' and Dispatch_Date='" + pdate + "'and Purchase_Center='" + mpc + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "' and godown = '" + ddlgodown.SelectedValue + "'";
                        da1 = new SqlDataAdapter(getacdata, con);
                        ds1 = new DataSet();
                        da1.Fill(ds1);

                        if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            DataRow drac = ds1.Tables[0].Rows[0];
                            string acno = drac["Acceptance_No"].ToString();
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Acceptance Note No..." + acno + "...  Already Issued for this Purchase Center for selected date'); </script> ");
                            return;
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Currently No Record Found'); </script> ");
                            return;
                        }
                    }
                    else
                    {
                        GridView2.DataSource = ds;
                        GridView2.DataBind();
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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RecdQty_Faq"));

            //qty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RecdQty_Urs"));

            jutnew += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RecdBags_JuteNew"));

            ppbag += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RecdBags_PP"));

            juteold += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RecdBags_JuteOld"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmount1 = (Label)e.Row.FindControl("lbl_totfaq");

            lblAmount1.Text = total1.ToString();

            //Label lblqty = (Label)e.Row.FindControl("lbl_toturs");
            //lblqty.Text = qty.ToString();


            Label lbljutnewbag = (Label)e.Row.FindControl("lbl_totjutnew");

            lbljutnewbag.Text = jutnew.ToString();

            Label lblppbags = (Label)e.Row.FindControl("lbl_totpp");

            lblppbags.Text = ppbag.ToString();


            Label lbljutold = (Label)e.Row.FindControl("lbl_totjutold");

            lbljutold.Text = juteold.ToString();
        }
    }

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        DateTime Recdate = Convert.ToDateTime(DateTime.ParseExact(DaintyDate3P.Text.Trim(), "dd-MM-yyyy", null).ToString("MM/dd/yyyy"));
        string todaydate = DateTime.Now.ToString("dd/MM/yyyy");
        DateTime currentdate = Convert.ToDateTime(DateTime.ParseExact(todaydate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
        int greaterdate = DateTime.Compare(currentdate, Recdate);

        if (ddlmarksesn.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Commodity'); </script> ");
            return;
        }
        else if (DaintyDate3P.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Date of Deposit'); </script> ");
            return;
        }
        else if (ddldistproment.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Sending District'); </script> ");
            return;
        }
        else if (ddlpurchcenterP.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Purchase Center Name'); </script> ");
            return;
        }
        else if (ddlgodown.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
            return;
        }
        else if (greaterdate == -1)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Date of Deposit Will Not Greater Than Today Date'); </script> ");
            return;
        }
        else
        {
            if (ddlmarksesn.SelectedValue.ToString() == "2" || ddlmarksesn.SelectedValue.ToString() == "3")
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(Con_CSMS))
                    {
                        try
                        {
                            SqlCommand cmdacno = new SqlCommand();
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            string AcceptDate = getDate_MDY(txtAccDate.Text);
                            int Insyear = int.Parse(DateTime.Today.Year.ToString());

                            IC_Id = Session["issue_id"].ToString();
                            Dist_Id = Session["dist_id"].ToString();

                            cmdacno.Parameters.Clear();
                            cmdacno.Parameters.AddWithValue("@District_ID", Dist_Id);
                            cmdacno.Parameters.AddWithValue("@IssueCenter_ID", IC_Id);
                            cmdacno.Parameters.AddWithValue("@Year", Insyear);

                            cmdacno.Connection = con;
                            cmdacno.CommandType = CommandType.StoredProcedure;
                            cmdacno.CommandText = "prc_getMaxAcceptanceNo_Kharif2016";

                            string Accpt_NO = "";
                            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

                            string opid = Session["OperatorId"].ToString();
                            string state = Session["State_Id"].ToString();

                            string cacno = Accpt_NO;

                            string qrydata = "Select Acceptance_No from Acceptance_Note_Kharif2016  where  Distt_ID='" + Dist_Id + "'and IssueCenter_ID='" + IC_Id + "'and Acceptance_No='" + cacno + "' and godown = '" + ddlgodown.SelectedValue + "'";
                            da = new SqlDataAdapter(qrydata, con);
                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds == null)
                            {

                            }
                            else
                            {
                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    string acptno = Accpt_NO;
                                    string mpc = ddlpurchcenterP.SelectedValue;
                                    string pdatw = getDate_MDY(DaintyDate3P.Text);
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string udate = "";
                                    //ClientIP objClientIP = new ClientIP();
                                    //string ip = (objClientIP.GETIP());

                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    if (GridView2.Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        foreach (GridViewRow gr in GridView2.Rows)
                                        {
                                            string iss_ID = gr.Cells[0].Text;
                                            string challan = gr.Cells[1].Text;
                                            string truckno = gr.Cells[2].Text;

                                            if (truckno == "&nbsp;")
                                            {
                                                truckno = "";
                                            }

                                            string CropYear = gr.Cells[8].Text;

                                            if (CropYear != "")
                                            {
                                                string firstfour = CropYear.Substring(0, 4);
                                                Int32 fir4 = Convert.ToInt32(firstfour);
                                                string lastfour = (fir4 + 1).ToString();
                                                CropYear = firstfour + "-" + lastfour;
                                            }

                                            Label rvcqtyFaq = (Label)gr.FindControl("lblrcfaq");
                                            decimal rcv_QTYfaq = Convert.ToDecimal(CheckNull(rvcqtyFaq.Text));

                                            //Label rvcqtyurs = (Label)gr.FindControl("lblrcurs");
                                            //decimal rcv_QTYurs = Convert.ToDecimal(CheckNull(rvcqtyurs.Text));
                                            // decimal TotlRec = rcv_QTYfaq + rcv_QTYurs;

                                            decimal TotlRec = rcv_QTYfaq;

                                            Label jutenew = (Label)gr.FindControl("lblrcjutnew");
                                            decimal rcv_juteNew = Convert.ToDecimal(CheckNull(jutenew.Text));
                                            Label recpp = (Label)gr.FindControl("lblrcpp");
                                            decimal rcv_ppbags = Convert.ToDecimal(CheckNull(recpp.Text));
                                            Label juteold = (Label)gr.FindControl("lblrcjutold");
                                            decimal rcv_juteold = Convert.ToDecimal(CheckNull(juteold.Text));
                                            decimal TotlRecBags = rcv_juteNew + rcv_ppbags + rcv_juteold;

                                            SqlTransaction trns;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }

                                            //string CheckduplicateRec = "Select *  from Acceptance_Note_Kharif2016 where Distt_ID='" + Dist_Id + "'  and IssueCenter_ID='" + IC_Id + "' and TC_Number='" + challan + "' and Truck_No = '" + truckno + "' and Acceptance_Date = '" + AcceptDate + "' and Accept_Qty = '" + TotlRec + "' and IssueID = '" + iss_ID + "' and godown = '" + ddlgodown.SelectedValue + "' ";
                                            string CheckduplicateRec = "Select *  from Acceptance_Note_Kharif2016 where Distt_ID='" + Dist_Id + "'  and IssueCenter_ID='" + IC_Id + "' and TC_Number='" + challan + "' and Truck_No = '" + truckno + "' and IssueID = '" + iss_ID + "' and godown = '" + ddlgodown.SelectedValue.ToString() + "' and CommodityId='" + hdfCSMS_Comid.Value + "' ";
                                            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);
                                            SqlDataReader drduplicate;
                                            drduplicate = cmdduplirec.ExecuteReader();

                                            if (drduplicate.Read())
                                            {
                                                drduplicate.Close();
                                                return;
                                            }
                                            drduplicate.Close();

                                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmd = new SqlCommand();
                                            cmd.Transaction = trns;
                                            SqlTransaction trns2;

                                            if (con_paddy.State == ConnectionState.Closed)
                                            {
                                                con_paddy.Open();
                                            }

                                            trns2 = con_paddy.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            SqlCommand cmdP = new SqlCommand();
                                            cmdP.Transaction = trns2;
                                            string inst = "insert into dbo.Acceptance_Note_Kharif2016(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown,CropYear)values('" + state + "','" + Dist_Id + "','" + IC_Id + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + TotlRec + "','','" + hdfCSMS_Comid.Value + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "','" + CropYear + "')";
                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;
                                                cmd.ExecuteNonQuery();

                                                string updt = "Update dbo.SCSC_Procurement_Kharif2016 set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + Dist_Id + "' and IssueCenter_ID='" + IC_Id + "' and TC_Number='" + challan + "' and Purchase_Center = '" + mpc + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "'";
                                                cmd.CommandText = updt;
                                                cmd.ExecuteNonQuery();

                                                //procrument
                                                string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown,OperatorID,State_Id)values('23" + Dist_Id + "','" + IC_Id + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + TotlRec + "','','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "','" + opid + "','" + state + "')";
                                                cmdP.Connection = con_paddy;
                                                cmdP.CommandText = inst1;
                                                cmdP.ExecuteNonQuery();

                                                string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + Dist_Id + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "'";
                                                cmdP.CommandText = updt1;
                                                cmdP.ExecuteNonQuery();

                                                trns.Commit();
                                                trns2.Commit();

                                                if (con.State == ConnectionState.Open)
                                                {
                                                    con.Close();
                                                }
                                                if (con_paddy.State == ConnectionState.Open)
                                                {
                                                    con_paddy.Close();
                                                }

                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your Acceptance Note No. Is " + Accpt_NO + "'); </script> ");
                                                btnRecptSubmit.Enabled = false;
                                                btnPrint.Enabled = true;

                                                ddlmarksesn.Enabled = ddldistproment.Enabled = ddlpurchcenterP.Enabled = ddlgodown.Enabled = false;

                                                Label2.Visible = true;
                                                Label2.Text = "Data Saved Successfully and Your Acceptance Note No. Is '" + Accpt_NO + "' ";

                                                Session["Acceptance_NO"] = Accpt_NO;
                                                Session["Godown"] = ddlgodown.SelectedValue;

                                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                trns.Rollback();
                                                trns2.Rollback();
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
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
                            ddlmarksesn.Items.Clear();
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
                            SqlCommand cmdacno = new SqlCommand();
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            string AcceptDate = getDate_MDY(txtAccDate.Text);
                            int Insyear = int.Parse(DateTime.Today.Year.ToString());

                            IC_Id = Session["issue_id"].ToString();
                            Dist_Id = Session["dist_id"].ToString();

                            cmdacno.Parameters.Clear();
                            cmdacno.Parameters.AddWithValue("@District_ID", Dist_Id);
                            cmdacno.Parameters.AddWithValue("@IssueCenter_ID", IC_Id);
                            cmdacno.Parameters.AddWithValue("@Year", Insyear);

                            cmdacno.Connection = con;
                            cmdacno.CommandType = CommandType.StoredProcedure;
                            cmdacno.CommandText = "prc_getMaxAcceptanceNo_Kharif2016";

                            string Accpt_NO = "";
                            Accpt_NO = Convert.ToString(cmdacno.ExecuteScalar());

                            string opid = Session["OperatorId"].ToString();
                            string state = Session["State_Id"].ToString();

                            string cacno = Accpt_NO;

                            string qrydata = "Select Acceptance_No from Acceptance_Note_Kharif2016  where  Distt_ID='" + Dist_Id + "'and IssueCenter_ID='" + IC_Id + "'and Acceptance_No='" + cacno + "' and godown = '" + ddlgodown.SelectedValue + "'";
                            da = new SqlDataAdapter(qrydata, con);
                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds == null)
                            {

                            }
                            else
                            {
                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    string acptno = Accpt_NO;
                                    string mpc = ddlpurchcenterP.SelectedValue;
                                    string pdatw = getDate_MDY(DaintyDate3P.Text);
                                    int month = int.Parse(DateTime.Today.Month.ToString());
                                    int year = int.Parse(DateTime.Today.Year.ToString());
                                    string udate = "";
                                    //ClientIP objClientIP = new ClientIP();
                                    //string ip = (objClientIP.GETIP());

                                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                    if (GridView2.Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        foreach (GridViewRow gr in GridView2.Rows)
                                        {
                                            string iss_ID = gr.Cells[0].Text;
                                            string challan = gr.Cells[1].Text;
                                            string truckno = gr.Cells[2].Text;

                                            if (truckno == "&nbsp;")
                                            {
                                                truckno = "";
                                            }

                                            string CropYear = gr.Cells[8].Text;

                                            if (CropYear != "")
                                            {
                                                string firstfour = CropYear.Substring(0, 4);
                                                Int32 fir4 = Convert.ToInt32(firstfour);
                                                string lastfour = (fir4 + 1).ToString();
                                                CropYear = firstfour + "-" + lastfour;
                                            }

                                            Label rvcqtyFaq = (Label)gr.FindControl("lblrcfaq");
                                            decimal rcv_QTYfaq = Convert.ToDecimal(CheckNull(rvcqtyFaq.Text));

                                            //Label rvcqtyurs = (Label)gr.FindControl("lblrcurs");
                                            //decimal rcv_QTYurs = Convert.ToDecimal(CheckNull(rvcqtyurs.Text));
                                            // decimal TotlRec = rcv_QTYfaq + rcv_QTYurs;

                                            decimal TotlRec = rcv_QTYfaq;

                                            Label jutenew = (Label)gr.FindControl("lblrcjutnew");
                                            decimal rcv_juteNew = Convert.ToDecimal(CheckNull(jutenew.Text));
                                            Label recpp = (Label)gr.FindControl("lblrcpp");
                                            decimal rcv_ppbags = Convert.ToDecimal(CheckNull(recpp.Text));
                                            Label juteold = (Label)gr.FindControl("lblrcjutold");
                                            decimal rcv_juteold = Convert.ToDecimal(CheckNull(juteold.Text));
                                            decimal TotlRecBags = rcv_juteNew + rcv_ppbags + rcv_juteold;

                                            SqlTransaction trns;
                                            if (con.State == ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }

                                            //string CheckduplicateRec = "Select *  from Acceptance_Note_Kharif2016 where Distt_ID='" + Dist_Id + "'  and IssueCenter_ID='" + IC_Id + "' and TC_Number='" + challan + "' and Truck_No = '" + truckno + "' and Acceptance_Date = '" + AcceptDate + "' and Accept_Qty = '" + TotlRec + "' and IssueID = '" + iss_ID + "' and godown = '" + ddlgodown.SelectedValue + "' ";
                                            string CheckduplicateRec = "Select *  from Acceptance_Note_Kharif2016 where Distt_ID='" + Dist_Id + "'  and IssueCenter_ID='" + IC_Id + "' and TC_Number='" + challan + "' and Truck_No = '" + truckno + "' and IssueID = '" + iss_ID + "' and godown = '" + ddlgodown.SelectedValue.ToString() + "' and CommodityId='" + hdfCSMS_Comid.Value + "' ";
                                            SqlCommand cmdduplirec = new SqlCommand(CheckduplicateRec, con);
                                            SqlDataReader drduplicate;
                                            drduplicate = cmdduplirec.ExecuteReader();

                                            if (drduplicate.Read())
                                            {
                                                drduplicate.Close();
                                                return;
                                            }
                                            drduplicate.Close();

                                            trns = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            cmd = new SqlCommand();
                                            cmd.Transaction = trns;
                                            SqlTransaction trns2;

                                            if (con_maze.State == ConnectionState.Closed)
                                            {
                                                con_maze.Open();
                                            }

                                            trns2 = con_maze.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                                            SqlCommand cmdP = new SqlCommand();
                                            cmdP.Transaction = trns2;
                                            string inst = "insert into dbo.Acceptance_Note_Kharif2016(State_Id,Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,OperatorID,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown,CropYear)values('" + state + "','" + Dist_Id + "','" + IC_Id + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + opid + "','" + TotlRec + "','','" + hdfCSMS_Comid.Value + "','" + iss_ID + "',SUBSTRING('" + mpc + "',3,2),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "','" + CropYear + "')";
                                            try
                                            {
                                                cmd.Connection = con;
                                                cmd.CommandText = inst;
                                                cmd.ExecuteNonQuery();

                                                string updt = "Update dbo.SCSC_Procurement_Kharif2016 set AN_Status='Y',Acceptance_No='" + acptno + "',Acceptance_Date = '" + AcceptDate + "' where Distt_ID='" + Dist_Id + "' and IssueCenter_ID='" + IC_Id + "' and TC_Number='" + challan + "' and Purchase_Center = '" + mpc + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "' and Commodity_Id='" + hdfCSMS_Comid.Value + "' ";
                                                cmd.CommandText = updt;
                                                cmd.ExecuteNonQuery();

                                                //procrument
                                                string inst1 = "insert into dbo.Acceptance_Note_Detail(Distt_ID,IssueCenter_ID,Purchase_Center,Dispatch_Date,TC_Number,Truck_No,Acceptance_No,Acceptance_Date,Month,Year,Created_Date,Updates_Date,IP_Address,Accept_Qty,Reject_Qty,CommodityId,IssueID,Sending_District,Bags,godown,OperatorID,State_Id)values('23" + Dist_Id + "','" + IC_Id + "','" + mpc + "','" + pdatw + "','" + challan + "','" + truckno + "','" + acptno + "','" + AcceptDate + "'," + month + "," + year + "," + "getdate(),'" + udate + "','" + ip + "','" + TotlRec + "','','" + ddlmarksesn.SelectedValue.ToString() + "','" + iss_ID + "',LEFT('" + mpc + "',4),'" + TotlRecBags + "','" + ddlgodown.SelectedValue + "','" + opid + "','" + state + "')";
                                                cmdP.Connection = con_maze;
                                                cmdP.CommandText = inst1;
                                                cmdP.ExecuteNonQuery();

                                                string updt1 = "Update dbo.IssueCenterReceipt_Online set AN_Status='Y' where DistrictId='23" + Dist_Id + "' and SocietyID='" + mpc + "' and TruckChalanNo='" + challan + "' and Receipt_Id = '" + iss_ID + "' and Recd_Godown = '" + ddlgodown.SelectedValue + "' and CommodityId='" + ddlmarksesn.SelectedValue.ToString() + "'";
                                                cmdP.CommandText = updt1;
                                                cmdP.ExecuteNonQuery();

                                                trns.Commit();
                                                trns2.Commit();

                                                if (con.State == ConnectionState.Open)
                                                {
                                                    con.Close();
                                                }
                                                if (con_maze.State == ConnectionState.Open)
                                                {
                                                    con_maze.Close();
                                                }

                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your Acceptance Note No. Is " + Accpt_NO + "'); </script> ");
                                                btnRecptSubmit.Enabled = false;
                                                btnPrint.Enabled = true;

                                                ddlmarksesn.Enabled = ddldistproment.Enabled = ddlpurchcenterP.Enabled = ddlgodown.Enabled = false;

                                                Label2.Visible = true;
                                                Label2.Text = "Data Saved Successfully and Your Acceptance Note No. Is '" + Accpt_NO + "' ";

                                                Session["Acceptance_NO"] = Accpt_NO;
                                                Session["Godown"] = ddlgodown.SelectedValue;

                                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                trns.Rollback();
                                                trns2.Rollback();
                                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
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
                            ddlmarksesn.Items.Clear();
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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnPrint_Click1(object sender, EventArgs e)
    {
        string url = "Print_IssueAC_Kharif2016.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/issue_welcome.aspx");
    }

    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;
    }
}