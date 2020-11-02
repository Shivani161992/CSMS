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

public partial class PaddyMilling_Paddy_Adjust_DO : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                lblmsg.Visible = false;
                txtDistManager.Text = Session["dist_name"].ToString();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCropYearValues();
               // GetMillName();
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
                    ddlCropyear.DataSource = ds.Tables[0];
                    ddlCropyear.DataTextField = "CropYear";
                    ddlCropyear.DataValueField = "CropYear";
                    ddlCropyear.DataBind();
                    ddlCropyear.Items.Insert(0, "--Select--");
                    //txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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
    protected void ddlCropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCropyear.SelectedIndex > 0)
        {
            GetMillName();
        }
        else
        {
            return;

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

                select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + DistCode + "' and PM.CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and PM.IsAccepted='Y' order by MillName Asc";
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
        ddlAgtmtNumber.Items.Clear();
        ddlDONo.Items.Clear();

        txtLotNO.Text = txtDOQty.Text = txtAllotedDOQty.Text = txtRemDOQty.Text = "";
        btnRecptSubmit.Enabled = false;
        hdfAllotedDOQty.Value = hdfLotNO.Value = "";

        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";

                    select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and IsAccepted='Y' order by Agreement_ID";

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
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mill Name'); </script> ");
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

        txtLotNO.Text = txtDOQty.Text = txtAllotedDOQty.Text = txtRemDOQty.Text = "";
        btnRecptSubmit.Enabled = false;
        hdfAllotedDOQty.Value = hdfLotNO.Value = "";

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

                string select = "Select distinct Check_DO From PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and District='" + DistCode + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDONo.DataSource = ds.Tables[0];
                    ddlDONo.DataTextField = "Check_DO";
                    ddlDONo.DataValueField = "Check_DO";
                    ddlDONo.DataBind();
                    ddlDONo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस अनुबंध नंबर के विरुद्ध कोई भी DO जारी नहीं किया गया है, इसलिए DO नंबर उपलब्ध नहीं हैं|'); </script> ");
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
        txtLotNO.Text = txtDOQty.Text = txtAllotedDOQty.Text = txtRemDOQty.Text = "";
        btnRecptSubmit.Enabled = false;
        hdfAllotedDOQty.Value = hdfLotNO.Value = hdfAdjustCMRDO.Value = "";

        if (ddlDONo.SelectedIndex > 0)
        {
            GetDOData();
            if (hdfAllotedDOQty.Value == "")
            {
                AdjustCMRDO();
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select DO Number'); </script> ");
        }
    }

    public void GetDOData()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select MAX(DhanLot) As DhanLot,SUM(Alloted_CommonDhan) As DOQty,SUM(ISNULL(Return_CommonRice,0)) As AllotedDoQty,SUM(ROUND((Alloted_CommonDhan-(ISNULL(Return_CommonRice,0))),2)) As RemDOQty,MAX(Milling_Type) As Milling_Type From PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and District='" + DistCode + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Check_DO='" + ddlDONo.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string MillingType = ds.Tables[0].Rows[0]["Milling_Type"].ToString();
                    txtLotNO.Text = "Lot" + ds.Tables[0].Rows[0]["DhanLot"].ToString();
                    hdfLotNO.Value = ds.Tables[0].Rows[0]["DhanLot"].ToString();
                    txtDOQty.Text = ds.Tables[0].Rows[0]["DOQty"].ToString();
                    txtAllotedDOQty.Text = ds.Tables[0].Rows[0]["AllotedDoQty"].ToString();
                    txtRemDOQty.Text = ds.Tables[0].Rows[0]["RemDOQty"].ToString();

                    if (float.Parse(txtAllotedDOQty.Text) <= 0)
                    {
                        hdfAllotedDOQty.Value = "1";
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आप इस DO Number के विरुद्ध Adjustment CMR Deposit Order नहीं बना सकते क्यों कि Alloted DO Qty 0 Qtls हैं|'); </script> ");
                        return;
                    }
                    else if (float.Parse(txtAllotedDOQty.Text) > 402 && MillingType == "अरवा")
                    {
                        hdfAllotedDOQty.Value = "1";
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आप इस DO Number के विरुद्ध Adjustment Delivery Order नहीं बना सकते क्यों कि Alloted DO Qty 402 Qtls या उससे ज्यादा हैं|'); </script> ");
                        return;
                    }
                    else if (float.Parse(txtAllotedDOQty.Text) > 396 && MillingType == "उसना")
                    {
                        hdfAllotedDOQty.Value = "1";
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आप इस DO Number के विरुद्ध Adjustment Delivery Order नहीं बना सकते क्यों कि Alloted DO Qty 396 Qtls या उससे ज्यादा हैं|'); </script> ");
                        return;
                    }
                    else
                    {
                        btnRecptSubmit.Enabled = true;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('No Data Found'); </script> ");
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

    public void AdjustCMRDO()
    {
        hdfAdjustCMRDO.Value = "";
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                select = "Select * From Paddy_Adjust_DO Where CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Agreement_ID='"+ddlAgtmtNumber.SelectedItem.ToString()+"' and DO_No='"+ddlDONo.SelectedItem.ToString()+"' ";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfAdjustCMRDO.Value = "1";
                    btnRecptSubmit.Enabled = false;
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DO Number के विरुद्ध Adjustment Delivery Order बनाया जा चूका हैं|'); </script> ");
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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        string DistCode = Session["dist_id"].ToString();

        if (hdfAllotedDOQty.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आप इस DO Number के विरुद्ध Adjustment Delivery Order नहीं बना सकते क्यों कि Alloted DO Qty 402 Qtls या उससे ज्यादा हैं|'); </script> ");
            return;
        }
        else if (hdfAdjustCMRDO.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DO Number के विरुद्ध Adjustment Delivery Order बनाया जा चूका हैं|'); </script> ");
            return;
        }
        else if (ddlDONo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select DO Number'); </script> ");
            return;
        }
        else
        {
            if (ddlCropyear.SelectedIndex>0 && txtLotNO.Text != "")
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            ClientIP objClientIP = new ClientIP();
                            string GetIp = (objClientIP.GETIP());

                            con.Open();

                            string SubDCDate = "", DCDate = "", instr = "";
                            string selectmax = "Select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                            da = new SqlDataAdapter(selectmax, con);
                            ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                                SubDCDate = DCDate.Substring(2);
                            }

                            if (SubDCDate != "")
                            {
                                instr = "Insert Into Paddy_Adjust_DO(District,CropYear,Mill_ID,Agreement_ID,DO_No,Lot_No,Mapping_No,CreatedDate,IP_Address) Values('" + DistCode + "','" + ddlCropyear.SelectedValue.ToString() + "','" + ddlMillName.SelectedValue.ToString() + "','" + ddlAgtmtNumber.SelectedValue.ToString() + "','" + ddlDONo.SelectedValue.ToString() + "','" + hdfLotNO.Value + "','" + SubDCDate + "',GETDATE(),'" + GetIp + "')";
                            }

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnRecptSubmit.Enabled = false;
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Adjustment Delivery Order of Paddy Is Created Successfully'); </script> ");
                                lblmsg.Visible = true;
                                lblmsg.Text = "Adjustment Delivery Order of Paddy Is Created Successfully";
                                //txtYear.Text =
                                ddlCropyear.ClearSelection();
                                 txtLotNO.Text = "";
                                ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlDONo.Enabled = false;
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    
}