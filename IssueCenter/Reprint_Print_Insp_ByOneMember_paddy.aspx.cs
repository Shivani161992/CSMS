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


public partial class IssueCenter_Reprint_Print_Insp_ByOneMember_paddy : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    // protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";
    public string DistId, ICID;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ICID = Session["issue_id"].ToString();
            DistId = Session["dist_id"].ToString();
           // ddlCropYear.Items.Insert(0, "--Select--");
            //ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
           // ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
            //ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));

           // GetDist();
           // GetCropYearValues();
            //GetMillerDistrict();
            GetInspector();
            //GetGodown();
        }

    }
    public void GetInspector()
    {

        //IC_Id = Session["issue_id"].ToString();
        ICID = Session["issue_id"].ToString();
        DistId = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select Inspector_ID,Inspector_Name, Inspector_desig from Inspector_Master_02017 where  IssueCenter_code='" + ICID + "' and district='" + DistId + "'  order by Inspector_Name");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_Insp.DataSource = ds.Tables[0];
                        ddl_Insp.DataTextField = "Inspector_Name";
                        ddl_Insp.DataValueField = "Inspector_ID";
                        ddl_Insp.DataBind();
                        ddl_Insp.Items.Insert(0, "--Select--");
                        // txtdesig.Text = ds.Tables[0].Rows[0]["Inspector_desig"].ToString();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Inspector Name is Not available'); </script> ");
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

    public void GetGodown()
    {
        ICID = Session["issue_id"].ToString();
        DistId = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "select distinct G.Godown_Name, PIO.Godown_ID from PM_Inspection_ByOnemember  as PIO inner join tbl_MetaData_GODOWN as G on  G.DepotId=PIO.ICenter_ID and G.Godown_ID=PIO.Godown_ID and G.DistrictId=PIO.District_ID where District_ID='" + DistId + "' and ICenter_ID='"+ICID+"' and Inspector_Name='"+ddl_Insp.SelectedValue.ToString()+"' order by Godown_Name";
                da = new SqlDataAdapter(select, strcon);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlgd.DataSource = ds.Tables[0];
                        ddlgd.DataTextField = "Godown_Name";
                        ddlgd.DataValueField = "Godown_ID";
                        ddlgd.DataBind();
                        ddlgd.Items.Insert(0, "--Select--");
                        // GetStack();
                        //btnQuilityTested.Enabled = true;

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
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

    public void GetStack()
    {
        ICID = Session["issue_id"].ToString();
        DistId = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "select distinct Stack_ID, Stack_Name from PM_Inspection_ByOnemember  as PIO where Godown_ID='"+ddlgd.SelectedValue.ToString()+"' order by Stack_Name";
                da = new SqlDataAdapter(select, strcon);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSK.DataSource = ds.Tables[0];
                        ddlSK.DataTextField = "Stack_Name";
                        ddlSK.DataValueField = "Stack_ID";
                        ddlSK.DataBind();
                        ddlSK.Items.Insert(0, "--Select--");

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
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected void bttprint_Click(object sender, EventArgs e)
    {
        
        string url = "Print_Insp_ByOMenber.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
    protected void ddlgd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgd.SelectedIndex > 0)
        {

            GetStack();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown'); </script> ");
        }
    }
    protected void ddlSK_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSK.SelectedIndex > 0)
        {

            //GetStack();
            GetAcptRejt();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Stack No.'); </script> ");
        }
    }

    public void GetAcptRejt()
    {

        ICID = Session["issue_id"].ToString();
        DistId = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "select Acceptance_NO, Rejection_NO, InspectionID from PM_Inspection_ByOnemember where District_ID='" + DistId + "' and ICenter_ID='" + ICID + "' and Godown_ID='" + ddlgd.SelectedValue.ToString() + "' and Stack_ID='" + ddlSK.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
               
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string Accept, Reject, Value;
                        Accept = ds.Tables[0].Rows[0]["Acceptance_NO"].ToString();
                        Reject = ds.Tables[0].Rows[0]["Rejection_NO"].ToString();
                        if (Accept == "0")
                        {
                            Value = Reject;
                            ddlacptRej.DataSource = ds.Tables[0];
                            ddlacptRej.DataTextField = "Rejection_NO";
                            ddlacptRej.DataValueField = "Rejection_NO";
                            ddlacptRej.DataBind();
                            ddlacptRej.Items.Insert(0, "--Select--");
                            Session["Inspection"] = ds.Tables[0].Rows[0]["InspectionID"].ToString();
                        }
                        else
                        {
                            Value = Accept;
                            ddlacptRej.DataSource = ds.Tables[0];
                            ddlacptRej.DataTextField = "Acceptance_NO";
                            ddlacptRej.DataValueField = "Acceptance_NO";
                            ddlacptRej.DataBind();
                            ddlacptRej.Items.Insert(0, "--Select--");
                            Session["Inspection"] = ds.Tables[0].Rows[0]["InspectionID"].ToString();
                        }
                       
                        // GetStack();
                        //btnQuilityTested.Enabled = true;

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch'); </script> ");
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
    protected void ddl_Insp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Insp.SelectedIndex > 0)
        {

            //GetStack();
            GetGodown();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Inspector Name'); </script> ");
        }
    }
}