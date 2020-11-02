using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PaddyMilling_PaddyMilling_Approved : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Session["AgrmtNo"] = "";
                Session["CropYear"] = "";
                GetAgrmtNumber();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetAgrmtNumber()
    {
        string DistID = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";
                //if (Session["DistrictManager"].ToString() == "DM" || Session["DistrictManager"].ToString() == "OP")
                //{
                //    select = string.Format("Select distinct Agreement_ID From PaddyMilling_Agreement Where District='" + DistID + "' and (IsAccepted='F' and ((DATEADD(DAY,20,Current_DateTime))>=Getdate())) and User_Agent!='DDMO' ");
                //}
                //else if (Session["DistrictManager"].ToString() == "DDMO")
                //{
                //    select = string.Format("Select distinct Agreement_ID From PaddyMilling_Agreement Where District='" + DistID + "' and (IsAccepted='F' and ((DATEADD(DAY,20,Current_DateTime))>=Getdate())) and User_Agent='DDMO'");
                //}

                select = string.Format("Select distinct Agreement_ID From PaddyMilling_Agreement_2017 Where District='" + DistID + "' and (IsAccepted='F' and ((DATEADD(DAY,20,Current_DateTime))>=Getdate()))");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAgrmtNo.DataSource = ds.Tables[0];
                        ddlAgrmtNo.DataTextField = "Agreement_ID";
                        ddlAgrmtNo.DataValueField = "Agreement_ID";
                        ddlAgrmtNo.DataBind();
                        ddlAgrmtNo.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('अनुबंध का नंबर उपलब्ध नहीं हैं|'); </script> ");
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

    protected void ddlAgrmtNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAccept.Enabled = btnReject.Enabled =btnPrint.Enabled = false;
        FCI.Visible = false;
        lblDist.Text = "";
        txtCropYear.Text = TxtDist.Text = txtDMName.Text = txtMobileNo.Text = txtMillName.Text = txtFrmDate.Text = txtToDate.Text = txtAgrmtLot.Text = txtSecurityLot.Text = txtCDahn.Text = txtGDhan.Text = txtTotalDhan.Text = txtMillingType.Text = "";
        if (ddlAgrmtNo.SelectedIndex > 0)
        {
            GetAgrmtDetails();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध का नंबर चुने|'); </script> ");
        }
    }

    public void GetAgrmtDetails()
    {
        string DistID = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "", DeliverdToFCI = "", StateCode="",GetState="";
                select = "Select PM.State_Code,PM.DeliverdToFCI,PM.CropYear,MP.district_name,PM.Dist_Manager_Name,PM.MobileNO,MR.Mill_Name,PM.DhanLot As SecurityLot,PM.DhanAmountDetails As AgrmtLot,PM.Common_Dhan,PM.GradeA_Dhan,PM.Total_Dhan,PM.Milling_Type,PM.From_Date,PM.To_Date From PaddyMilling_Agreement_2017 As PM left join pds.districtsmp As MP ON(PM.District=MP.district_code) left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear and PM.State_Code=MR.State_Code) where PM.District='" + DistID + "' and PM.Agreement_ID='" + ddlAgrmtNo.SelectedItem.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                        TxtDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                        txtMobileNo.Text = ds.Tables[0].Rows[0]["MobileNO"].ToString();
                        txtDMName.Text = ds.Tables[0].Rows[0]["Dist_Manager_Name"].ToString();
                        txtMillName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();

                        DateTime FrmDate = DateTime.Parse(ds.Tables[0].Rows[0]["From_Date"].ToString());
                        txtFrmDate.Text = FrmDate.ToString("dd-MMM-yyyy");

                        DateTime ToDate = DateTime.Parse(ds.Tables[0].Rows[0]["To_Date"].ToString());
                        txtToDate.Text = ToDate.ToString("dd-MMM-yyyy");

                        txtAgrmtLot.Text = ds.Tables[0].Rows[0]["AgrmtLot"].ToString();
                        txtSecurityLot.Text = ds.Tables[0].Rows[0]["SecurityLot"].ToString();
                        txtCDahn.Text = ds.Tables[0].Rows[0]["Common_Dhan"].ToString();
                        txtGDhan.Text = ds.Tables[0].Rows[0]["GradeA_Dhan"].ToString();
                        txtTotalDhan.Text = ds.Tables[0].Rows[0]["Total_Dhan"].ToString();
                        txtMillingType.Text = ds.Tables[0].Rows[0]["Milling_Type"].ToString();

                        btnAccept.Enabled = btnReject.Enabled = true;

                        DeliverdToFCI = ds.Tables[0].Rows[0]["DeliverdToFCI"].ToString();
                        StateCode = ds.Tables[0].Rows[0]["State_Code"].ToString();
                        if (DeliverdToFCI == "Y")
                        {
                            FCI.Visible = true;
                            GetState = "Select State_Name From State_Master where State_Code='" + StateCode + "'";
                            da1 = new SqlDataAdapter(GetState, con);
                            ds1 = new DataSet();
                            da1.Fill(ds1);
                            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                lblDist.Text = ds1.Tables[0].Rows[0]["State_Name"].ToString();
                            }
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('अन्य जानकारी उपलब्ध नहीं हैं|'); </script> ");
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

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        string DistID = Session["dist_id"].ToString();

        if (ddlAgrmtNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध का नंबर चुने|'); </script> ");
            return;
        }
        else
        {
            if (txtAgrmtLot.Text != "" && txtSecurityLot.Text != "")
            {
                ClientIP objClientIP = new ClientIP();
                string GetIp = (objClientIP.GETIP());

                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            string instr = "";

                            instr = "Update PaddyMilling_Agreement_2017 Set IsAccepted='Y', AcceptedIP='" + GetIp + "',AcceptedDate=GETDATE() where District='" + DistID + "' and CropYear='" + txtCropYear.Text + "' and Agreement_ID='" + ddlAgrmtNo.SelectedItem.ToString() + "' and Total_Dhan='" + txtTotalDhan.Text + "' and DhanLot='" + txtSecurityLot.Text + "' and DhanAmountDetails='" + txtAgrmtLot.Text + "'";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnAccept.Enabled = btnReject.Enabled = ddlAgrmtNo.Enabled = false;
                                //btnPrint.Enabled = true;
                                Session["AgrmtNo"] = ddlAgrmtNo.SelectedItem.ToString();
                                Session["CropYear"] = txtCropYear.Text;

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Milling Agreement Is Approved'); </script> ");
                                txtAgrmtLot.Text = txtSecurityLot.Text = "";
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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

    protected void btnReject_Click(object sender, EventArgs e)
    {
        string DistID = Session["dist_id"].ToString();
            
        if (ddlAgrmtNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध का नंबर चुने|'); </script> ");
            return;
        }
        else
        {
            if (txtAgrmtLot.Text != "" && txtSecurityLot.Text != "")
            {
                ClientIP objClientIP = new ClientIP();
                string GetIp = (objClientIP.GETIP());

                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            string instr = "";

                            instr = "Update PaddyMilling_Agreement_2017 Set IsAccepted='N', AcceptedIP='" + GetIp + "',AcceptedDate=GETDATE() where District='" + DistID + "' and CropYear='" + txtCropYear.Text + "' and Agreement_ID='" + ddlAgrmtNo.SelectedItem.ToString() + "' and Total_Dhan='" + txtTotalDhan.Text + "' and DhanLot='" + txtSecurityLot.Text + "' and DhanAmountDetails='" + txtAgrmtLot.Text + "'";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnAccept.Enabled = btnReject.Enabled = ddlAgrmtNo.Enabled = false;
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Milling Agreement Is Rejected'); </script> ");
                                txtAgrmtLot.Text = txtSecurityLot.Text = "";
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print/PMillingAgreement.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
}