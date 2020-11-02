using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;  

public partial class District_RecFromRack : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string districtid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                districtid = Session["dist_id"].ToString();

                GetMONumber();
                GetFrmRackPoint();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }

            txtRecdDate.Text = Request.Form[txtRecdDate.UniqueID];
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GetMONumber()
    {
        txtRecdBags.Text = txtRecdDate.Text = txtRecdQty.Text = txtConsNo.Text = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select distinct MoveOrdernum From StateMovementOrder where ToDist='" + districtid + "' and ModeofDispatch='13' and (DATEADD(DAY,210,CreatedDate))>=Getdate() and ModeofDist IN('Both','Other','Self') and IsAccepted='Y' order by MoveOrdernum";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlTONo.DataSource = ds.Tables[0];
                    ddlTONo.DataTextField = "MoveOrdernum";
                    ddlTONo.DataValueField = "MoveOrdernum";
                    ddlTONo.DataBind();
                    ddlTONo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('MO Number Is Not Available'); </script> ");
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

    public void GetFrmRackPoint()
    {
        ddlFrmRack.Items.Clear();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select RailHead_Name,RailHead_Code From tbl_Rail_Head where district_code='" + districtid + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlFrmRack.DataSource = ds.Tables[0];
                    ddlFrmRack.DataTextField = "RailHead_Name";
                    ddlFrmRack.DataValueField = "RailHead_Code";
                    ddlFrmRack.DataBind();
                    ddlFrmRack.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी रेल हेड उपलब्ध नहीं है|'); </script> ");
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

    protected void ddlTONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRecdBags.Text = txtRecdDate.Text = txtRecdQty.Text = txtConsNo.Text = txtSendDist.Text = txtComdty.Text = txtRemMOQty.Text = txtGunnyType.Text = "";
        lblQtls.Text = lblQtls0.Text = "";

        if (ddlTONo.SelectedIndex > 0)
        {
            GetMODetails();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select MO Number'); </script> ");
        }
    }

    public void GetMODetails()
    {
        txtRecdBags.Text = txtRecdDate.Text = txtRecdQty.Text = txtConsNo.Text = "";
        hdfCommodity.Value = hdfFrm_RackDist.Value =  "";
        districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select *,(Quantity-TotalQty) As MORemQty From (Select SMO.GunnyType,SMO.FrmDist,SMO.Commodity,SMO.Quantity,DMP.district_name,CMDTY.Commodity_Name,SUM(Isnull(RR.Rec_Qty,0)) As TotalQty" +
                                 " From StateMovementOrder SMO" +
                                  " left join pds.districtsmp DMP on(SMO.FrmDist = DMP.district_code)" +
                                   " left join tbl_MetaData_STORAGE_COMMODITY CMDTY on (SMO.Commodity=CMDTY.Commodity_Id)" +
                                    " left join RackReceived RR on(SMO.MoveOrdernum=RR.MoveOrdernum and SMO.FrmDist=RR.Frm_RackDist and SMO.ToDist=RR.To_RackDist)" +
                                     " where SMO.MoveOrdernum='" + ddlTONo.SelectedItem.ToString() + "' and SMO.ToDist='" + districtid + "'" +
                                      " Group by SMO.GunnyType,SMO.FrmDist,SMO.Commodity,SMO.Quantity,DMP.district_name,CMDTY.Commodity_Name) As Dummy";

                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtSendDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
                    txtComdty.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    txtRemMOQty.Text = ds.Tables[0].Rows[0]["MORemQty"].ToString();
                    hdfFrm_RackDist.Value = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                    hdfCommodity.Value = ds.Tables[0].Rows[0]["Commodity"].ToString();

                    if (txtComdty.Text == "Gunny")
                    {
                        string GunnyType = ds.Tables[0].Rows[0]["GunnyType"].ToString();
                        lblQtls.Text = lblQtls0.Text = "(Bales)";
                        txtRecdBags.Text = "00";

                        if (GunnyType == "JUTE")
                        {
                            txtGunnyType.Text = "Jute(SBT)";
                        }
                        else
                        {
                            txtGunnyType.Text = "PP";
                        }
                        
                        txtRecdBags.Enabled = false;
                    }
                    else
                    {
                        lblQtls.Text = lblQtls0.Text = "(Qtls)";
                        txtRecdBags.Text = txtGunnyType.Text = "";
                        txtRecdBags.Enabled = true;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('MO Details Is Not Available'); </script> ");
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
        if (txtRecdQty.Text != "" && txtRecdBags.Text != "")
        {
            if (ddlTONo.SelectedIndex <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select MO Number'); </script> ");
                return;
            }
            else if (ddlFrmRack.SelectedIndex<=0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Received Rail Head'); </script> ");
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

                            string Consinment_No = "Select Consinment_No from RackReceived where Consinment_No='" + txtConsNo.Text + "'";
                            da1 = new SqlDataAdapter(Consinment_No, con);
                            ds1 = new DataSet();
                            da1.Fill(ds1);
                            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Consinment Number Already Exist...'); </script> ");
                                return;
                            }

                            //With TO_NO in DB
                            //Update += "Insert Into RackReceived(Consinment_No,TO_No,MoveOrdernum,Frm_RackDist,To_RackDist,Rec_Qty,Rem_Qty,Rec_Bags,Rem_Bags,Commodity,Rec_Date,CreatedDate,IP,IsIssued) values('" + txtConsNo.Text + "','" + ddlTONo.SelectedItem.ToString() + "','" + ViewState["hdfMoveOrdernum"].ToString()   + "','" + ddlDist.SelectedValue.ToString() + "','" + districtid + "','" + txtRecdQty.Text + "','" + txtRecdQty.Text + "','" + txtRecdBags.Text + "','" + txtRecdBags.Text + "','" + ddlCommodity.SelectedValue.ToString() + "','" + RecDate + "',GETDATE(),'" + GetIp + "','N');";

                            // Without TO_NO in DB
                            Update += "Insert Into RackReceived(Consinment_No,MoveOrdernum,Frm_RackDist,To_RackDist,Rec_Qty,Rem_Qty,Rec_Bags,Rem_Bags,Commodity,Rec_Date,CreatedDate,IP,IsIssued,ToRailHaid) values('" + txtConsNo.Text + "','" + ddlTONo.SelectedItem.ToString() + "','" + hdfFrm_RackDist.Value + "','" + districtid + "','" + txtRecdQty.Text + "','" + txtRecdQty.Text + "','" + txtRecdBags.Text + "','" + txtRecdBags.Text + "','" + hdfCommodity.Value + "','" + RecDate + "',GETDATE(),'" + GetIp + "','N','" + ddlFrmRack.SelectedValue.ToString()+ "');";

                            Update += "";
                            cmd = new SqlCommand(Update, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved Sucessfully ....'); </script> ");
                                btnRecptSubmit.Enabled = false;
                                txtRecdQty.Text = txtRecdBags.Text = "";
                                ddlTONo.Enabled = ddlFrmRack.Enabled = false;
                                txtRecdQty.Enabled = txtRecdBags.Enabled = txtConsNo.Enabled = false;

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
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Quantity Or Bags'); </script> ");
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

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }


}