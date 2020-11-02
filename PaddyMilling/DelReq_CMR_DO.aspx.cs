using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class PaddyMilling_DelReq_CMR_DO : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                txtDistManager.Text = Session["dist_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCropYearValues();
                GetMillName();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetCropYearValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

    public void GetMillName()
    {
        ddlMillName.Items.Clear();

        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement As PM Left Join Miller_Registration MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + DistCode + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "MillName";
                    ddlMillName.DataValueField = "MillCode";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर ने अनुबंध नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlMillName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string DistCode = Session["dist_id"].ToString();

        ddlAgtmtNumber.Items.Clear();
        ddlDONo.Items.Clear();
        btnDelete.Enabled = false;
        txtDODate.Text = txtDOLastDate.Text = txtIC.Text = txtGodown.Text = "";
        hdfAcpt.Value = hdfReject.Value = hdfLotNo.Value = hdfMillID.Value = hdfAgrmtID.Value =hdfIC.Value =hdfGodown.Value= hdfDelReq.Value = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";

                    select = "Select Agreement_ID From PaddyMilling_Agreement where District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y'  order by Agreement_ID";

                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAgtmtNumber.DataSource = ds.Tables[0];
                        ddlAgtmtNumber.DataTextField = "Agreement_ID";
                        ddlAgtmtNumber.DataValueField = "Agreement_ID";
                        ddlAgtmtNumber.DataBind();
                        ddlAgtmtNumber.Items.Insert(0, "--Select--");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल का नाम चुनें|'); </script> ");
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

    protected void ddlAgtmtNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDONo.Items.Clear();
        txtDODate.Text = txtDOLastDate.Text = txtIC.Text = txtGodown.Text = "";
        hdfAcpt.Value = hdfReject.Value = hdfLotNo.Value = hdfMillID.Value = hdfAgrmtID.Value =hdfIC.Value =hdfGodown.Value= hdfDelReq.Value = "";
        btnDelete.Enabled = false;

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetAgrmtData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Agreement Number'); </script> ");
        }
    }

    public void GetAgrmtData()
    {
        string DistCode = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select CMR_DO From CMR_DepositOrder  where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and CMR_RecdDist='" + DistCode + "' and CropYear='" + txtYear.Text + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDONo.DataSource = ds.Tables[0];
                    ddlDONo.DataTextField = "CMR_DO";
                    ddlDONo.DataValueField = "CMR_DO";
                    ddlDONo.DataBind();
                    ddlDONo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Deposit Order Number Is Not Available'); </script> ");
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

    protected void ddlDONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDODate.Text = txtDOLastDate.Text = txtIC.Text = txtGodown.Text = "";
        hdfAcpt.Value = hdfReject.Value = hdfLotNo.Value = hdfMillID.Value = hdfAgrmtID.Value =hdfIC.Value =hdfGodown.Value= hdfDelReq.Value = "";
        btnDelete.Enabled = false;

        if (ddlDONo.SelectedIndex > 0)
        {
            GetDOData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR DO Number'); </script> ");
        }
    }

    public void GetDOData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select1 = "Select * From CMR_DepositOrder_DelReq Where CMR_DO='" + ddlDONo.SelectedItem.ToString() + "' and IsAccepted='P'";
                da = new SqlDataAdapter(select1, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfDelReq.Value = "Y";
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Delete Request भेजी जा चुकी है, इसलिए आप इसके लिए फिर से Delete Request नहीं भेज सकते|'); </script> ");
                    return;
                }
                else
                {
                    string select = "Select Agreement_ID,Mill_ID,Lot_No,IsAccepted,IsRejected,IssueCenter,Godown_id,CreatedDate,CMR_DODate From CMR_DepositOrder Where CMR_DO='" + ddlDONo.SelectedItem.ToString() + "' ";
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        hdfAcpt.Value = ds.Tables[0].Rows[0]["IsAccepted"].ToString();
                        hdfReject.Value = ds.Tables[0].Rows[0]["IsRejected"].ToString();
                        hdfLotNo.Value = ds.Tables[0].Rows[0]["Lot_No"].ToString();
                        hdfMillID.Value = ds.Tables[0].Rows[0]["Mill_ID"].ToString();
                        hdfAgrmtID.Value = ds.Tables[0].Rows[0]["Agreement_ID"].ToString();

                        if (hdfAcpt.Value == "Y")
                        {
                            btnDelete.Enabled = false;
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल जमा किया जा चूका है, इसलिए आप इसके लिए Delete Request नहीं भेज सकते|'); </script> ");
                        }
                        else if (hdfReject.Value == "Y")
                        {
                            btnDelete.Enabled = false;
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल को रिजेक्ट किया जा चूका है, इसलिए आप इसके लिए Delete Request नहीं भेज सकते|'); </script> ");
                        }
                        else
                        {
                            btnDelete.Enabled = true;
                        }

                        DateTime DODate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                        txtDODate.Text = DODate.ToString("dd-MM-yyyy");

                        DateTime DOLastDate = DateTime.Parse(ds.Tables[0].Rows[0]["CMR_DODate"].ToString());
                        txtDOLastDate.Text = DOLastDate.ToString("dd-MM-yyyy");

                        txtIC.Text = hdfIC.Value = ds.Tables[0].Rows[0]["IssueCenter"].ToString();
                        txtGodown.Text = hdfGodown.Value = ds.Tables[0].Rows[0]["Godown_id"].ToString();

                        GetGodownName();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('No Data Found'); </script> ");
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

    public void GetGodownName()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                string GodownName = "", ICName = "";

                GodownName = txtGodown.Text;
                ICName = txtIC.Text;

                con_MPStorage.Open();
                string select = string.Format("select (select DepotName from tbl_MetaData_DEPOT where DepotId='" + ICName + "') As ICName,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID='" + GodownName + "') As Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtGodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                        txtIC.Text = ds.Tables[0].Rows[0]["ICName"].ToString();
                    }
                }
                else
                {
                    txtGodown.Text = "";
                    txtIC.Text = "";
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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Session["DelReq_CMRDO"] = "";
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlDONo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR DO Number'); </script> ");
            return;
        }

        if (hdfAcpt.Value == "Y")
        {
            btnDelete.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल जमा किया जा चूका है, इसलिए आप इसके लिए Delete Request नहीं भेज सकते|'); </script> ");
            return;
        }
        else if (hdfReject.Value == "Y")
        {
            btnDelete.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल को रिजेक्ट किया जा चूका है, इसलिए आप इसके लिए Delete Request नहीं भेज सकते|'); </script> ");
            return;
        }
        else if (hdfDelReq.Value == "Y")
        {
            btnDelete.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध Delete Request भेजी जा चुकी है, इसलिए आप इसके लिए फिर से Delete Request नहीं भेज सकते|'); </script> ");
            return;
        }

        ClientIP objClientIP = new ClientIP();
        string GetIp = (objClientIP.GETIP());

        if (txtYear.Text != "")
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        con.Open();

                        string SubDCDate = "", DCDate = "", instr = "", CMRDO_ID = "";

                        string selectmax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                        da = new SqlDataAdapter(selectmax, con);
                        ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                            SubDCDate = DCDate.Substring(2);
                        }

                        string DistCode = Session["dist_id"].ToString();
                        string useragent = Session["DistrictManager"].ToString();

                        if (SubDCDate != "")
                        {
                            CMRDO_ID = "DRCDO" + SubDCDate;
                            instr = "Insert Into CMR_DepositOrder_DelReq(DelReq_No,CMR_DO,District,CropYear,CreatedDate,IP_Address,IsAccepted,DelReq_ByUser,Mill_ID,Agreement_ID,Lot_No,IssueCenter,Godown_id) Values('" + CMRDO_ID + "','" + ddlDONo.SelectedItem.ToString() + "','" + DistCode + "','" + txtYear.Text + "',GETDATE(),'" + GetIp + "','P','" + useragent + "','"+hdfMillID.Value+"','"+hdfAgrmtID.Value+"','"+hdfLotNo.Value+"','"+hdfIC.Value+"','"+hdfGodown.Value+"')";
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnDelete.Enabled = false;
                            btnPrint.Enabled = true;
                            Label2.Visible = true;
                            Label2.Text = "Your Delete Request No. Is : " + CMRDO_ID + "";
                            Session["DelReq_CMRDO"] = "CMRDO_ID";
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Deposit Order के लिए Delete Request भेज दिया गया है|'); </script> ");
                            txtYear.Text = txtDistManager.Text = "";
                            ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlDONo.Enabled = false;
                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry, Delete Request Not Allow'); </script> ");
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
            else
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Session["DelReq_CMRDO"] = "";
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print/Print_CMRDO_DelReq.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
}