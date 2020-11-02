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

public partial class State_ExpiredFDR_Updation : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string IC_Id = "", Dist_Id = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;
                GetCropYearValues();

                GridView1.DataSource = "";
                GridView1.DataBind();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        txtMaturityDate.Text = Request.Form[txtMaturityDate.UniqueID];
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
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlCropYear.DataSource = ds.Tables[0];
                    ddlCropYear.DataTextField = "CropYear";
                    ddlCropYear.DataValueField = "CropYear";
                    ddlCropYear.DataBind();
                    ddlCropYear.Items.Insert(0, "--Select--");
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
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMPDist.Items.Clear();
        ddlMillName.Items.Clear();
        if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select CropYear'); </script> ");
            return;
        }
        else
        {
            GetDist();

        }

    }

    public void GetDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMPDist.DataSource = ds.Tables[0];
                        ddlMPDist.DataTextField = "district_name";
                        ddlMPDist.DataValueField = "district_code";
                        ddlMPDist.DataBind();
                        ddlMPDist.Items.Insert(0, "--Select--");


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
    protected void ddlMPDist_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlMillName.Items.Clear();
        if (ddlMPDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select District'); </script> ");
            return;
        }
        else
        {
            GetMPDistMiller();

        }

    }

    public void GetMPDistMiller()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Registration_ID, Mill_Name From Miller_Registration_2017 as MR Where District_Code='" + ddlMPDist.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and State_Code='23' and State='MP' and Registration_ID in ( select distinct MillerID from PM_FDR_and_Cheque_Master where FDR_Maturity<GETDATE() and Miller_Dist='" + ddlMPDist.SelectedValue.ToString() + "'  )Order By Mill_Name";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "Mill_Name";
                    ddlMillName.DataValueField = "Registration_ID";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो जिला चुना है, उस जिले से किसी भी मिलर की FDR की वैधता समाप्त नहीं हुई है|'); </script> ");
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

        if (ddlMillName.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller Name'); </script> ");
            return;
        }
        else
        {
            trgrid.Visible = true;
            Fillgrid();

        }

    }
    void Fillgrid()
    {
        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "select FDR_ChequeID as ID, B.Bank_Name, FDR_IFSC, FDR_Number, FDR_Value,  convert(varchar(10),FDR_Maturity, 103) FDR_Maturity from PM_FDR_and_Cheque_Master as M  inner join Bank_Master_New as B on B.Bank_ID=M.FDR_BankID where FDR_Maturity<GETDATE() and Miller_Dist='" + ddlMPDist.SelectedValue.ToString() + "' and MillerID='" + ddlMillName.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        

        txtID.Text = GridView1.SelectedRow.Cells[1].Text;
        txtBankName.Text = GridView1.SelectedRow.Cells[2].Text;
        txtFDRIFSC.Text = GridView1.SelectedRow.Cells[3].Text;
        txtFDRNumber.Text = GridView1.SelectedRow.Cells[4].Text;
        txtFDRAmount.Text = GridView1.SelectedRow.Cells[5].Text;
        txtMaturityDate.Text = GridView1.SelectedRow.Cells[6].Text;
        btnUpdate.Enabled = true;


    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if(ddlCropYear.SelectedIndex<=0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select CropYear'); </script> ");
            return;
        }
        else if (ddlMPDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select District'); </script> ");
            return;
        }
        else if (ddlMillName.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller Name'); </script> ");
            return;
        }
        else if(txtID.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select any row from details displayed above'); </script> ");
            return;
        }
        else if(txtBankName.Text=="")
        {
         Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Bank name cannot be empty'); </script> ");
            return;
        }
        else if (txtFDRIFSC.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('FDR IFSC Code cannot be empty'); </script> ");
            return;
        }
        else if (txtFDRAmount.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('FDR Amount cannot be empty'); </script> ");
            return;
        }
        else if (txtMaturityDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('FDR Maturity date be empty'); </script> ");
            return;
        }
        else {

            using (con = new SqlConnection(strcon))
                try
                {
                    ConvertServerDate ServerDate = new ConvertServerDate();
                    string ConvertFromDate = ServerDate.getDate_MDY(txtMaturityDate.Text);
                   

                    con.Open();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                     string strselect="";

                     strselect = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                         "insert into PM_FDR_and_Cheque_Master_Log select * from PM_FDR_and_Cheque_Master where CropYear='"+ddlCropYear.SelectedValue.ToString()+"' and Miller_Dist='"+ddlMPDist.SelectedValue.ToString()+"' and MillerID='"+ddlMillName.SelectedValue.ToString()+"' and FDR_ChequeID='"+txtID.Text+"'";

                     strselect += "update PM_FDR_and_Cheque_Master set FDR_Maturity='" + ConvertFromDate + "' where FDR_ChequeID='" + txtID.Text + "' and MillerID='" + ddlMillName.SelectedValue.ToString() + "' and Miller_Dist='" + ddlMPDist.SelectedValue.ToString() + "'";

                    strselect += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                    
                    cmd = new SqlCommand(strselect, con);
                    string check = (string)cmd.ExecuteScalar();

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is updated successfully'); </script> ");
                    btnUpdate.Enabled = false;
                    txtMaturityDate.Enabled = false;

                }
                catch
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
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
}