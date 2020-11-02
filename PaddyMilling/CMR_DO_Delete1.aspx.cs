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
using System.Drawing;

public partial class PaddyMilling_CMR_DO_Delete : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                DelReqData();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void DelReqData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select DelReq_No,CMR_DO From CMR_DepositOrder_DelReq Where IsAccepted='P' ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDelReqNo.DataSource = ds.Tables[0];
                    ddlDelReqNo.DataTextField = "DelReq_No";
                    ddlDelReqNo.DataValueField = "CMR_DO";
                    ddlDelReqNo.DataBind();
                    ddlDelReqNo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('किसी भी जिले द्वारा Delete Request नहीं भेजी गयी हैं, इसलिए Delete Request Number उपलब्ध नहीं है|'); </script> ");
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlDelReqNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR Delete Request Number'); </script> ");
            return;
        }

        if (hdfAcpt.Value == "Y")
        {
            btnDelete.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल जमा किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
            return;
        }
        else if (hdfReject.Value == "Y")
        {
            btnDelete.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल को रिजेक्ट किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
            return;
        }

        ClientIP objClientIP = new ClientIP();
        string GetIp = (objClientIP.GETIP());

        ConvertServerDate ServerDate = new ConvertServerDate();
        string ConvertFromDate = ServerDate.getDate_MDY(txtDODate.Text);
        string ConvertToDate = ServerDate.getDate_MDY(txtDOLastDate.Text);

        if (txtYear.Text != "")
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        con.Open();

                        string instr = "";

                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                       // instr += "Insert Into PaddyMilling_DO_Log select *,GETDATE(),'" + GetIp + "','U' From PaddyMilling_DO where Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and CropYear='" + txtYear.Text + "' and DhanLot='" + hdfLotNo.Value + "' and Check_DO='" + hdfDONo.Value + "'; ";
                       // instr += "Update PaddyMilling_DO Set DispatchDhan_IC='N' where Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and CropYear='" + txtYear.Text + "' and DhanLot='" + hdfLotNo.Value + "' and Check_DO='" + hdfDONo.Value + "'; ";

                        instr += "Insert Into CMR_DepositOrder_Log Select *,GETDATE(),'" + GetIp + "','D' From CMR_DepositOrder where CMR_DO='" + ddlDelReqNo.SelectedValue.ToString() + "' ; ";
                        instr += "Delete From CMR_DepositOrder where CMR_DO='" + ddlDelReqNo.SelectedValue.ToString() + "' ; ";

                        instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnDelete.Enabled = ddlDelReqNo.Enabled = false;
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Deposit Order Deleted Successfully'); </script> ");
                            txtYear.Text = txtFrmDist.Text = "";
                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry, Deletion Not Allow'); </script> ");
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
        Response.Redirect("~/State/PaddyMillingHome.aspx");
    }

    protected void ddlDelReqNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDODate.Text = txtDOLastDate.Text = txtIC.Text = txtGodown.Text = txtFrmDist.Text = txtMillName.Text = txtAgrmtNo.Text = txtCMRDO.Text ="";
        hdfAcpt.Value = hdfReject.Value = hdfLotNo.Value = hdfDONo.Value = "";
        btnDelete.Enabled = false;

        if (ddlDelReqNo.SelectedIndex > 0)
        {
            GetCMRDOData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR Delete Request Number'); </script> ");
        }
    }

    public void GetCMRDOData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select CropYear,CMR_DO,Agreement_ID,DO_No,Lot_No,IsAccepted,IsRejected,IssueCenter,Godown_id,CreatedDate,CMR_DODate From CMR_DepositOrder Where CMR_DO='" + ddlDelReqNo.SelectedValue.ToString() + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfAcpt.Value = ds.Tables[0].Rows[0]["IsAccepted"].ToString();
                    hdfReject.Value = ds.Tables[0].Rows[0]["IsRejected"].ToString();
                    hdfLotNo.Value = ds.Tables[0].Rows[0]["Lot_No"].ToString();
                    hdfDONo.Value = ds.Tables[0].Rows[0]["DO_No"].ToString();

                    if (hdfAcpt.Value == "Y")
                    {
                        btnDelete.Enabled = false;
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल जमा किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
                    }
                    else if (hdfReject.Value == "Y")
                    {
                        btnDelete.Enabled = false;
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DEPOSIT ORDER के विरुद्ध चावल को रिजेक्ट किया जा चूका है, इसलिए आप इसे Delete नहीं कर सकते|'); </script> ");
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                    }

                    DateTime DODate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtDODate.Text = DODate.ToString("dd-MM-yyyy");

                    DateTime DOLastDate = DateTime.Parse(ds.Tables[0].Rows[0]["CMR_DODate"].ToString());
                    txtDOLastDate.Text = DOLastDate.ToString("dd-MM-yyyy");

                    txtIC.Text = ds.Tables[0].Rows[0]["IssueCenter"].ToString();
                    txtGodown.Text = ds.Tables[0].Rows[0]["Godown_id"].ToString();
                    txtAgrmtNo.Text = ds.Tables[0].Rows[0]["Agreement_ID"].ToString();
                    txtCMRDO.Text = ds.Tables[0].Rows[0]["CMR_DO"].ToString();
                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    txtFrmDist.Text = ds.Tables[0].Rows[0][""].ToString();
                    txtMillName.Text = ds.Tables[0].Rows[0][""].ToString();

                    GetGodownName();
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

}