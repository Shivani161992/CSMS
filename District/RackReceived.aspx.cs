using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class District_RackReceived : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string districtid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                districtid = Session["dist_id"].ToString();

                GetRackNo();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

            txtRecdDate.Text = Request.Form[txtRecdDate.UniqueID];
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetRackNo()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select distinct Racknumber From QtyReceived_RackDispatchPoint where IsReceived='N' and RecDist='" + districtid + "'";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlRackNo.DataSource = ds.Tables[0];
                    ddlRackNo.DataTextField = "Racknumber";
                    ddlRackNo.DataValueField = "Racknumber";
                    ddlRackNo.DataBind();
                    ddlRackNo.Items.Insert(0, "--Select--");

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rack Number Is Not Available'); </script> ");
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


    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    protected void ddlRackNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlChallanNo.Items.Clear();
        hdfSubMO.Value = hdfSendDist.Value = hdfComdty.Value = "";
        txtSendingDist.Text = txtCommodity.Text = txtCropYear.Text = txtSendingQty.Text = txtSendingBags.Text = txtRecdQty.Text = txtRecdBags.Text = txtSendingDate.Text = txtRecdDate.Text = "";

        if (ddlRackNo.SelectedIndex > 0)
        {
            GetChallanNo();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Rack Number'); </script> ");
        }

    }

    public void GetChallanNo()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select TruckChallan From QtyReceived_RackDispatchPoint where IsReceived='N' and RecDist='" + districtid + "' and Racknumber='" + ddlRackNo.SelectedItem.ToString() + "'";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlChallanNo.DataSource = ds.Tables[0];
                    ddlChallanNo.DataTextField = "TruckChallan";
                    ddlChallanNo.DataValueField = "TruckChallan";
                    ddlChallanNo.DataBind();
                    ddlChallanNo.Items.Insert(0, "--Select--");

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan Number Is Not Available'); </script> ");
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
    protected void ddlChallanNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfSubMO.Value = hdfSendDist.Value = hdfComdty.Value = "";
        txtSendingDist.Text = txtCommodity.Text = txtCropYear.Text = txtSendingQty.Text = txtSendingBags.Text = txtRecdQty.Text = txtRecdBags.Text = txtSendingDate.Text = txtRecdDate.Text = "";

        if (ddlChallanNo.SelectedIndex > 0)
        {
            GetChallanDetails();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Challan Number'); </script> ");
        }
    }


    public void GetChallanDetails()
    {
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select (select distinct SMO From DeliveryChallan_MO where DC_MO='" + ddlChallanNo.SelectedItem.ToString() + "' and Rack_No='" + ddlRackNo.SelectedItem.ToString() + "') As SubMONo,(select distinct MoveOrdernum From DeliveryChallan_MO where DC_MO='" + ddlChallanNo.SelectedItem.ToString() + "' and Rack_No='" + ddlRackNo.SelectedItem.ToString() + "') As MONo,(select Commodity_Name From tbl_MetaData_STORAGE_COMMODITY where Commodity_Id= QRP.Commodity) ComdtyName,Commodity,RecdDate,RecdQty,RecdBags,CropYear,RecDist,(select district_name From pds.districtsmp where district_code= QRP.sendingDist) SendDistName,sendingDist From QtyReceived_RackDispatchPoint QRP where RecDist='" + districtid + "' and Racknumber='" + ddlRackNo.SelectedItem.ToString() + "' and TruckChallan='" + ddlChallanNo.SelectedItem.ToString() + "'";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtSendingDist.Text = ds.Tables[0].Rows[0]["SendDistName"].ToString();
                    txtCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    txtSendingQty.Text = ds.Tables[0].Rows[0]["RecdQty"].ToString();
                    txtSendingBags.Text = ds.Tables[0].Rows[0]["RecdBags"].ToString();

                    DateTime SendDate = DateTime.Parse(ds.Tables[0].Rows[0]["RecdDate"].ToString());
                    txtSendingDate.Text = SendDate.ToString("dd/MMM/yyyy");

                    txtCommodity.Text = ds.Tables[0].Rows[0]["ComdtyName"].ToString();

                    txtMoNO.Text = ds.Tables[0].Rows[0]["MONo"].ToString();

                    hdfSubMO.Value = ds.Tables[0].Rows[0]["SubMONo"].ToString();
                    hdfSendDist.Value = ds.Tables[0].Rows[0]["sendingDist"].ToString();
                    hdfComdty.Value = ds.Tables[0].Rows[0]["Commodity"].ToString();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan Number Is Not Available'); </script> ");
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

    protected void btnRecptSubmit_Click(object sender, EventArgs e)
    {
        if (txtCommodity.Text != "" && txtCropYear.Text != "")
        {
            double SendQty = 0, RecQty = 0;
            int SendBags = 0, RecBags = 0;

            SendQty = double.Parse(txtSendingQty.Text);
            RecQty = double.Parse(txtRecdQty.Text);

            SendBags = int.Parse(txtSendingBags.Text);
            RecBags = int.Parse(txtRecdBags.Text);

            if (SendQty < RecQty)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receiving Qty की मात्रा, Sending Qty की मात्रा से ज्यादा है,कृपया Receiving Qty की मात्रा कम करें|'); </script> ");
                return;
            }
            else if (SendBags < RecBags)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receiving Bags की मात्रा, Sending Bags की मात्रा से ज्यादा है,कृपया Receiving Bags की मात्रा कम करें|'); </script> ");
                return;
            }
            else
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            string Update = "";
                            districtid = Session["dist_id"].ToString();

                            ClientIP objClientIP = new ClientIP();
                            string GetIp = (objClientIP.GETIP());

                            ConvertServerDate ServerDate = new ConvertServerDate();
                            string RecDate = ServerDate.getDate_MDY(txtRecdDate.Text);
                            string SendDate = ServerDate.getDate_MDY(txtSendingDate.Text);

                            Update = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                     "Update QtyReceived_RackDispatchPoint set IsReceived='Y' where Racknumber='" + ddlRackNo.SelectedItem.ToString() + "' and sendingDist='" + hdfSendDist.Value + "' and TruckChallan='" + ddlChallanNo.SelectedItem.ToString() + "' and RecDist='" + districtid + "';";

                            Update += "Insert into Rack_ReceivedPoint(Rack_No,DC_MO,MoveOrdernum,SMO,FrmDist,RecDist,Commodity,CropYear,Send_Qty,Recd_Qty,Rem_Qty,Send_Bags,Recd_Bags,Rem_Bags,Send_Date,Recd_Date,CreatedDate,IP) values('" + ddlRackNo.SelectedItem.ToString() + "','" + ddlChallanNo.SelectedItem.ToString() + "','" + txtMoNO.Text + "','" + hdfSubMO.Value + "','" + hdfSendDist.Value + "','" + districtid + "','" + hdfComdty.Value + "','" + txtCropYear.Text + "','" + SendQty + "','" + RecQty + "','" + RecQty + "','" + SendBags + "','" + RecBags + "','" + RecBags + "','" + SendDate + "','" + RecDate + "',GETDATE(),'" + GetIp + "');";

                            Update += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            cmd = new SqlCommand(Update, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved Sucessfully ....'); </script> ");
                                btnRecptSubmit.Enabled = false;
                                txtCommodity.Text = txtCropYear.Text = "";
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
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Alloted All Quantity'); </script> ");
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }
}