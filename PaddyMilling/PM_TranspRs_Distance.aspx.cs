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

public partial class PM_TranspRs_Distance : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["fdjfhxncdfh"] = null;

                lblmsg.Visible = lblMaster.Visible = lblNewAdd.Visible = lblRs.Visible = chkRs.Visible = false;
                txtDistManager.Text = Session["dist_name"].ToString();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCropYearValues();
                SectorName();
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

    public void SectorName()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select SectorName,SectorId From District_SectorMaster where DistrictId='" + DistCode + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlSector.DataSource = ds.Tables[0];
                    ddlSector.DataTextField = "SectorName";
                    ddlSector.DataValueField = "SectorId";
                    ddlSector.DataBind();
                    ddlSector.Items.Insert(0, "--Select--");
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

    public void LeadDistance()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select Lead_Name,Lead_ID From Lead_Distance";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlLead.DataSource = ds.Tables[0];
                    ddlLead.DataTextField = "Lead_Name";
                    ddlLead.DataValueField = "Lead_ID";
                    ddlLead.DataBind();
                    ddlLead.Items.Insert(0, "--Select--");
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

    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        string DistCode = Session["dist_id"].ToString();

        double MillCharges = 0;

        if (txtMillCharges.Text != "")
        {
            MillCharges = double.Parse(txtMillCharges.Text);
        }


        if (GridView1.Rows.Count <= 0 && hdfMappingNo.Value == "" && chkRs.Checked==false)
        {
            btnRecptSubmit.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Add At Least One Distance'); </script> ");
            return;
        }
        else if (txtMillCharges.Text.Trim() == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Milling Charges'); </script> ");
            return;
        }
        else if (ddlSector.SelectedIndex <= 0)
        {
            btnRecptSubmit.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Sector Name'); </script> ");
            return;
        }
        else if (MillCharges == 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Milling Charges Not Allow To 0'); </script> ");
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
                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                            if (hdfMappingNo.Value != "" && chkRs.Checked == false)
                            {
                                instr += "Insert Into PM_Transp_Rs_Log(District,CropYear,Mapping_No,SectorId,Lead_ID,Milling_Rs,Transp_Rs,CreatedDate,IP_Address,DeletedDate,DeletedIP,Operation) select District,CropYear,Mapping_No,SectorId,Lead_ID,Milling_Rs,Transp_Rs,CreatedDate,IP_Address,GETDATE(),'" + GetIp + "','D' From PM_Transp_Rs where Mapping_No IN (" + hdfMappingNo.Value + ") ";
                                instr += "Delete From PM_Transp_Rs where Mapping_No IN (" + hdfMappingNo.Value + ") ";
                            }
                            else if (chkRs.Checked == true && hdfMappingNo.Value == "")
                            {
                                instr += "Insert Into PM_Transp_Rs_Log(District,CropYear,Mapping_No,SectorId,Lead_ID,Milling_Rs,Transp_Rs,CreatedDate,IP_Address,DeletedDate,DeletedIP,Operation) select District,CropYear,Mapping_No,SectorId,Lead_ID,Milling_Rs,Transp_Rs,CreatedDate,IP_Address,GETDATE(),'" + GetIp + "','U' From PM_Transp_Rs where District='"+DistCode+"' and CropYear='"+txtYear.Text+"' and SectorId ='"+ddlSector.SelectedValue.ToString()+"' ";
                                instr += "Update PM_Transp_Rs set Milling_Rs='" + MillCharges + "' where District='" + DistCode + "' and CropYear='" + txtYear.Text + "' and SectorId ='" + ddlSector.SelectedValue.ToString() + "' ";
                            }
                            else
                            {
                                DataTable dt = adddetails();
                                if (dt != null && GridView1.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        string MaxMappingNo = SubDCDate + (i + 1);
                                        instr += "Insert Into PM_Transp_Rs(District,CropYear,Mapping_No,SectorId,Lead_ID,Milling_Rs,Transp_Rs,CreatedDate,IP_Address) values('" + DistCode + "','" + txtYear.Text + "','" + MaxMappingNo + "','" + dt.Rows[i]["SectorID"] + "','" + dt.Rows[i]["LeadID"] + "','" + MillCharges + "','" + dt.Rows[i]["TranspRs"] + "',GETDATE(),'" + GetIp + "') ; ";
                                    }
                                }
                            }
                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                        }

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            btnRecptSubmit.Enabled = false;
                            Session["fdjfhxncdfh"] = null;
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Distance Master Data Saved Successfully'); </script> ");
                            lblmsg.Visible = true;
                            lblmsg.Text = "Distance Master Data Saved Successfully";
                            ddlSector.Items.Clear();
                            ddlLead.Items.Clear();
                            txtMillCharges.Enabled = txtTranpRs.Enabled = btnAdd.Enabled = false;
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        double MillCharges = double.Parse(txtMillCharges.Text);
        double TranspRs = double.Parse(txtTranpRs.Text);

        if (ddlSector.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Sector Name'); </script> ");
            return;
        }
        else if (ddlLead.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Distance'); </script> ");
            return;
        }
        else if (txtMillCharges.Text.Trim() == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Milling Charges Rs.'); </script> ");
            return;
        }
        else if (txtTranpRs.Text.Trim() == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Transportation Rs.'); </script> ");
            return;
        }
        else if (MillCharges == 0 || TranspRs == 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('0 Is Not Allow'); </script> ");
            return;
        }
        else
        {
            int checkDuplicate = 0;
            chkRs.Checked = false;
            hdfMappingNo.Value = "";

            if (GridView2.Rows.Count > 0)
            {
                GridView2.Columns[4].Visible = chkRs.Visible =lblRs.Visible  = false;
                
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string Lead = GridView1.Rows[i].Cells[2].Text;
                if (Lead == ddlLead.SelectedItem.ToString())
                {
                    checkDuplicate = 1;
                    break;
                }
            }

            if (checkDuplicate == 0)
            {
                ddlSector.Enabled = false;
                lblNewAdd.Visible = true;

                DataTable dt = adddetails();
                if (dt == null)
                {
                    dt = new DataTable("aadqty");
                    dt.Columns.Add("SectorName");
                    dt.Columns.Add("SectorID");
                    dt.Columns.Add("LeadName");
                    dt.Columns.Add("LeadID");
                    dt.Columns.Add("TranspRs");
                }

                DataRow dr = dt.NewRow();

                dr["SectorName"] = ddlSector.SelectedItem.ToString();
                dr["SectorID"] = ddlSector.SelectedValue.ToString();

                dr["LeadName"] = ddlLead.SelectedItem.ToString();
                dr["LeadID"] = ddlLead.SelectedValue.ToString();

                dr["TranspRs"] = (float.Parse(txtTranpRs.Text)).ToString("0.00");

                dt.Rows.Add(dr);
                Session["fdjfhxncdfh"] = dt;
                fillgrid();

                txtTranpRs.Text = "";

                btnRecptSubmit.Enabled = true;
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आप उस Lead/Distance को फिर से Add नहीं कर सकते, जिस Lead/Distance को आपने पहले से Add करके रखा हैं|'); </script> ");
                return;
            }
        }
    }

    public DataTable adddetails()
    {
        DataTable dt = (DataTable)Session["fdjfhxncdfh"];
        return dt;
    }

    public void fillgrid()
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (GridView1.Rows.Count <= 1)
        {
            btnRecptSubmit.Enabled = false;
        }

        DataTable dt = adddetails();
        if (dt == null)
        {
            dt = new DataTable("aadqty");
            dt.Columns.Add("SectorName");
            dt.Columns.Add("SectorID");
            dt.Columns.Add("LeadName");
            dt.Columns.Add("LeadID");
            dt.Columns.Add("TranspRs");
        }
        else
        {
            dt.Rows.RemoveAt(e.RowIndex);
        }
        Session["fdjfhxncdfh"] = dt;
        fillgrid();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }


    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlLead.Enabled = txtMillCharges.Enabled = txtTranpRs.Enabled = false;
        txtMillCharges.Text = txtTranpRs.Text = "";
        hdfMappingNo.Value = hdfMillingRs.Value =  "";
        GridView2.Visible = lblMaster.Visible = lblNewAdd.Visible = lblRs.Visible = chkRs.Visible  = false;
        ddlLead.Items.Clear();

        GridView2.DataSource = "";
        GridView2.DataBind();

        if (ddlSector.SelectedIndex > 0)
        {
            LeadDistance();
            ddlLead.Enabled = txtMillCharges.Enabled = txtTranpRs.Enabled = true;
            GetTranspRsData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Sector Name'); </script> ");
            return;
        }
    }

    public void GetTranspRsData()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select Rs.Mapping_No,Rs.Milling_Rs,Rs.Transp_Rs,Lead.Lead_Name,Lead.Lead_ID,Sector.SectorName From PM_Transp_Rs As Rs Left Join Lead_Distance As Lead ON(Lead.Lead_ID = Rs.Lead_ID) Left Join District_SectorMaster As Sector ON(Rs.District=Sector.DistrictId and Rs.SectorId=Sector.SectorId) Where Rs.District='" + DistCode + "' and Rs.CropYear='" + txtYear.Text + "' and Rs.SectorId='" + ddlSector.SelectedValue.ToString() + "'  order by Lead.Lead_ID";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.Visible = lblMaster.Visible = lblRs.Visible = chkRs.Visible = true;
                    txtMillCharges.Enabled = false;
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                    txtMillCharges.Text = hdfMillingRs.Value = ds.Tables[0].Rows[0]["Milling_Rs"].ToString();

                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        string Lead = GridView2.Rows[i].Cells[6].Text;
                        ddlLead.Items.FindByValue(Lead).Enabled = false;
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

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        chkRs.Checked = false;

        if (GridView2.Rows.Count > 0)
        {
            btnRecptSubmit.Enabled = true;
            ddlSector.Enabled = false;
            hdfMappingNo.Value += ((hdfMappingNo.Value == "") ? "" : " , ") + "'" + GridView2.Rows[e.RowIndex].Cells[5].Text + "'";

            GridView2.Rows[e.RowIndex].Visible = false;
            btnAdd.Visible = lblRs.Visible = chkRs.Visible = false;
            txtTranpRs.Enabled = false;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    protected void chkRs_CheckedChanged(object sender, EventArgs e)
    {
        hdfMappingNo.Value = "";
        if (chkRs.Checked)
        {
            btnRecptSubmit.Enabled = txtMillCharges.Enabled = true;
            txtTranpRs.Enabled = false;
            btnAdd.Visible = false;
            GridView2.Columns[4].Visible = false;
        }
        else
        {
            btnRecptSubmit.Enabled = txtMillCharges.Enabled = false;
            txtTranpRs.Enabled = true;
            btnAdd.Visible = true;
            GridView2.Columns[4].Visible = true;
            txtMillCharges.Text = hdfMillingRs.Value;
        }
    }
}