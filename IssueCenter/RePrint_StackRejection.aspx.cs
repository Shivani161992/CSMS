using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DataAccess;
using Data;

public partial class IssueCenter_RePrint_StackRejection : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    public string gatepass, Masterinsp_ID = "";
    public int getnum, MasterinspNum;
    SqlDataReader dr;
    // protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";
    public string DistId, ICID;
    string MillName, BagsType, Inspector;
    string MSRejection = "";
    string MSAcceptance = "";

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();
                GetCropYearValues();


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
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlCropyear.DataSource = ds.Tables[0];
                    ddlCropyear.DataTextField = "CropYear";
                    ddlCropyear.DataValueField = "CropYear";
                    ddlCropyear.DataBind();
                    ddlCropyear.Items.Insert(0, "--Select--");
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
        ddldist.Items.Clear();
        ddlIC.Items.Clear();
        ddlgodown.Items.Clear();
        ddlstack.Items.Clear();
        ddlaccepReject.Items.Clear();
        if (ddlCropyear.SelectedIndex > 0)
        {
            GetDist();

        }
        else { }
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
                        ddldist.DataSource = ds.Tables[0];
                        ddldist.DataTextField = "district_name";
                        ddldist.DataValueField = "district_code";
                        ddldist.DataBind();
                        ddldist.Items.Insert(0, "--Select--");

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

    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIC.Items.Clear();
        ddlgodown.Items.Clear();
        ddlstack.Items.Clear();
        ddlaccepReject.Items.Clear();
        if (ddldist.SelectedIndex > 0)
        {
            GetMPIssueCentre();
        }
        else
        {

        }
    }

    private void GetMPIssueCentre()
    {
        DistId = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + ddldist.SelectedValue.ToString() + "' order by DepotName");
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
        ddlgodown.Items.Clear();
        ddlstack.Items.Clear();
        ddlaccepReject.Items.Clear();
        if (ddlIC.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {

        }
    }
    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DepotId='" + ddlIC.SelectedValue.ToString() + "' and  DistrictId='23" + ddldist.SelectedValue.ToString() + "'order by Godown_Name";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlgodown.DataSource = ds.Tables[0];
                        ddlgodown.DataTextField = "Godown_Name";
                        ddlgodown.DataValueField = "Godown_ID";
                        ddlgodown.DataBind();
                        ddlgodown.Items.Insert(0, "--Select--");
                        // GetStack();
                        // btnQuilityTested.Enabled = true;

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
                    }
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
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlstack.Items.Clear();
        ddlaccepReject.Items.Clear();
        if (ddlgodown.SelectedIndex > 0)
        {
            GetStack();
        }
        else
        {

        }
    }
    public void GetStack()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = "select Stack_ID, Stack_Name from tbl_MetaData_STACK where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and District_Id='23" + ddldist.SelectedValue.ToString() + "' order by Stack_Name";
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlstack.DataSource = ds.Tables[0];
                        ddlstack.DataTextField = "Stack_Name";
                        ddlstack.DataValueField = "Stack_ID";
                        ddlstack.DataBind();
                        ddlstack.Items.Insert(0, "--Select--");

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' Stack No. is not available'); </script> ");
                    }

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

    protected void ddlstack_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlaccepReject.Items.Clear();
        if (ddlstack.SelectedIndex > 0)
        {
            GetAcceptRejectNo();
        }
        else
        {

        }
    }

    public void GetAcceptRejectNo()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "select distinct BookNumber from tblStackrejection where District_ID='" + ddldist.SelectedValue.ToString() + "' and ICenter_ID='" + ddlIC.SelectedValue.ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'  and Stack_ID='" + ddlstack.SelectedValue.ToString() + "' order by BookNumber ";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlaccepReject.DataSource = ds.Tables[0];
                        ddlaccepReject.DataTextField = "BookNumber";
                        ddlaccepReject.DataValueField = "BookNumber";
                        ddlaccepReject.DataBind();
                        ddlaccepReject.Items.Insert(0, "--Select--");

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' Acceptance/Rejection is not available'); </script> ");
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


    protected void ddlaccepReject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlaccepReject.SelectedIndex > 0)
        {
            bttaccept.Enabled = true;
        }
        else
        {

        }
    }


    protected void bttaccept_Click(object sender, EventArgs e)
    {
        Session["Inspection"] = ddlaccepReject.SelectedValue.ToString();

        string url = "PrintStackNumber.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }


   
}