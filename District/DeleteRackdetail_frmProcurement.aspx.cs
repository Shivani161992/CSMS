using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class District_DeleteRackdetail_frmProcurement : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());

    public static string distid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (!IsPostBack)
            {
          
                distid = Session["dist_id"].ToString();

                Getrack();

            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void Getrack()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string rack = "SELECT distinct RackNumber FROM SCSC_Procurement where Distt_ID = '" + distid + "' and RackNumber != '' ";
        SqlCommand cmd = new SqlCommand(rack, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlracknumber.DataSource = ds.Tables[0];

        ddlracknumber.DataTextField = "RackNumber";
        ddlracknumber.DataValueField = "RackNumber";
        ddlracknumber.DataBind();
        ddlracknumber.Items.Insert(0, "--Select--");

        lblRecBags.Text = "";

        lblTruckNo.Text = "";

        lblRecDate.Text = "";

        lblRecQty.Text = "";

        lbloperation.Text = "";

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    protected void ddlracknumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlracknumber.SelectedItem.Text == "--Select--")
        {
            ddlchallan.DataSource = "";
            ddlchallan.DataBind();

            lblRecBags.Text = "";

            lblTruckNo.Text = "";

            lblRecDate.Text = "";

            lblRecQty.Text = "";

            lbloperation.Text = "";
        }

        else
        {
            Bindchallan();

            lblRecBags.Text = "";

            lblTruckNo.Text = "";

            lblRecDate.Text = "";

            lblRecQty.Text = "";

            lbloperation.Text = "";
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddlchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlchallan.SelectedItem.Text == "--Select--")
        {
            ddlIssueID.DataSource = "";
            ddlIssueID.DataBind();

            lblerror.Text = "";

        }

        else
        {
            lblerror.Text = "";

            string tcqry = "SELECT Receipt_Id FROM SCSC_Procurement where Distt_ID = '" + distid + "' and RackNumber = '" + ddlracknumber.SelectedItem.Text + "' and TC_Number = '" + ddlchallan.SelectedItem.Text + "' ";
            SqlCommand cmdqry = new SqlCommand(tcqry, con);
            SqlDataAdapter daqry = new SqlDataAdapter(cmdqry);
            DataSet dsqry = new DataSet();
            daqry.Fill(dsqry);

            ddlIssueID.DataSource = dsqry.Tables[0];

            ddlIssueID.DataTextField = "Receipt_Id";
            ddlIssueID.DataValueField = "Receipt_Id";
            ddlIssueID.DataBind();
            ddlIssueID.Items.Insert(0, "--Select--");


            lblRecBags.Text = "";

            lblTruckNo.Text = "";

            lblRecDate.Text = "";

            lblRecQty.Text = "";

            lbloperation.Text = "";

        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddlIssueID_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblerror.Text = "";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddlracknumber.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Rack Number'); </script> ");
            return; 
        }

        if (ddlchallan.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Challan Number'); </script> ");
          
        }

        else
        {
            try
            {

                string accepcheck = "select count(IssueID) from Acceptance_Note_Detail where IssueID = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '" + distid + "'";
                SqlCommand cmdc_Accep = new SqlCommand(accepcheck, con);
                string ss = cmdc_Accep.ExecuteScalar().ToString();

                int chAcc = Convert.ToInt16(ss);

                if (chAcc > 0)  // record exist
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Acceptance Note Issued, Click Delete if You want to Delete'); </script> ");
                     
                }

                string tcqry = "SELECT convert(nvarchar,Recd_Date,103)Recd_Date , Truck_Number , Recd_Bags , Recd_Qty FROM SCSC_Procurement where Distt_ID = '" + distid + "' and RackNumber = '" + ddlracknumber.SelectedItem.Text + "' and TC_Number = '" + ddlchallan.SelectedItem.Text + "'";
                SqlCommand cmdqry = new SqlCommand(tcqry, con);
                SqlDataAdapter daqry = new SqlDataAdapter(cmdqry);
                DataSet dsqry = new DataSet();
                daqry.Fill(dsqry);

                if (dsqry.Tables[0].Rows.Count > 0)
                {
                    btnDelete.Enabled = true;

                    lblRecDate.Text = dsqry.Tables[0].Rows[0]["Recd_Date"].ToString();

                    lblTruckNo.Text = dsqry.Tables[0].Rows[0]["Truck_Number"].ToString();

                    lblRecBags.Text = dsqry.Tables[0].Rows[0]["Recd_Bags"].ToString();

                    lblRecQty.Text = dsqry.Tables[0].Rows[0]["Recd_Qty"].ToString();
                }
            }

            catch (Exception ex)
            {
                lblerror.Text = ex.Message;
            }
                         
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        lblerror.Text = "";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (con_WPMS.State == ConnectionState.Closed)
        {
            con_WPMS.Open();
        }

        if (ddlracknumber.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Rack Number'); </script> ");
            return; 
        }

        if (ddlchallan.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Challan Number'); </script> ");
            return; 
        }

        if (ddlIssueID.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Issue ID Number'); </script> ");
            return; 
        }

        try
        {
            
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            # region checkacceptance

            string accepcheck = "select count(*) from Acceptance_Note_Detail where IssueID = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '" + distid + "'";
            SqlCommand cmdcheck_Accep = new SqlCommand(accepcheck, con);
            string s = cmdcheck_Accep.ExecuteScalar().ToString();

                int chAcc = Convert.ToInt16(s);

                if (chAcc > 0)  // record exist
                {
                    // del frm WPMS 
                    string insWlog1 = "Insert into Acceptance_Note_Detail_DelLog select * from Acceptance_Note_Detail where IssueID = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '23" + distid + "'";

                    SqlCommand cmdWlog1 = new SqlCommand(insWlog1, con_WPMS);

                    int x1 = cmdWlog1.ExecuteNonQuery();
                    
                    
                    string delWqry1 = "delete from Acceptance_Note_Detail where IssueID = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '23" + distid + "' ";

                    SqlCommand cmdWdel1 = new SqlCommand(delWqry1, con_WPMS);

                    int y1 = cmdWdel1.ExecuteNonQuery();


                    // del frm CSMS

                    string insClog2 = "Insert into Acceptance_Note_Detail_DelLog select * from Acceptance_Note_Detail where IssueID = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '" + distid + "'";

                    SqlCommand cmdClog2 = new SqlCommand(insClog2, con);

                    int x2 = cmdClog2.ExecuteNonQuery();


                    string delCqry2 = "delete from Acceptance_Note_Detail where IssueID = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '" + distid + "' ";

                    SqlCommand cmdCdel2 = new SqlCommand(delCqry2, con);

                    int y2 = cmdCdel2.ExecuteNonQuery();
                }

            # endregion

            string insWlog = "Insert into IssueCenterReceipt_Online_Log select * from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedItem.Text + "' and DistrictId = '23" + distid + "' and RackNumber = '" + ddlracknumber.SelectedItem.Text + "'";

            SqlCommand cmdWlog = new SqlCommand(insWlog, con_WPMS);

            int x = cmdWlog.ExecuteNonQuery();
            
            string delWqry = "delete from IssueCenterReceipt_Online where IssueID = '" + ddlIssueID.SelectedItem.Text + "' and DistrictId = '23" + distid + "' and RackNumber = '" + ddlracknumber.SelectedItem.Text + "'";

            SqlCommand cmdWdel = new SqlCommand(delWqry, con_WPMS);

            int y = cmdWdel.ExecuteNonQuery();

            if (y == 0)
            {
                lblwp_oper.Text = "Not Del from WPMS";
            }

            else if (y > 0)
            {

                string selcheck = "select count(*) from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '" + distid + "' and RackNumber = '" + ddlracknumber.SelectedItem.Text + "'";
                SqlCommand cmdcheck = new SqlCommand(selcheck, con);
                string z = cmdcheck.ExecuteScalar().ToString();

                int chlog = Convert.ToInt16(z);

                if (chlog > 0)
                {
                    string inslog = "Insert into SCSC_Procurement_dellog select * from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '" + distid + "' and RackNumber = '" + ddlracknumber.SelectedItem.Text + "'";

                    SqlCommand cmdlog = new SqlCommand(inslog, con);

                    int a = cmdlog.ExecuteNonQuery();

                    string delqry = "delete from SCSC_Procurement where Receipt_Id = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '" + distid + "' and RackNumber = '" + ddlracknumber.SelectedItem.Text + "'";

                    SqlCommand cmddel = new SqlCommand(delqry, con);

                    int b = cmddel.ExecuteNonQuery();

                    if (b == 0)
                    {
                        lbloperation.Text = "Not Del from CSMS";
                    }

                    else
                    {
                        lbloperation.Text = "";

                        string updateqry = "Update SCSC_Procurement_dellog set Deleted_Date = GETDATE(),IP_Address = '" + ip + "'  where Receipt_Id = '" + ddlIssueID.SelectedItem.Text + "' and Distt_ID = '" + distid + "' and RackNumber = '" + ddlracknumber.SelectedItem.Text + "'";

                        SqlCommand cmdup = new SqlCommand(updateqry, con);

                        int c = cmdup.ExecuteNonQuery();

                       Bindchallan();

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Entry Saved'); </script> ");
                        
                    }
                }

            }

            else
            {
                lblwp_oper.Text = "";
            }     
        }

        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }

        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/DeleteRackdetail_frmProcurement.aspx");
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

    private void Bindchallan()
    {

        string tcqry = "SELECT TC_Number FROM SCSC_Procurement where Distt_ID = '" + distid + "' and RackNumber = '" + ddlracknumber.SelectedItem.Text + "'";
        SqlCommand cmdqry = new SqlCommand(tcqry, con);
        SqlDataAdapter daqry = new SqlDataAdapter(cmdqry);
        DataSet dsqry = new DataSet();
        daqry.Fill(dsqry);

        ddlchallan.DataSource = dsqry.Tables[0];

        ddlchallan.DataTextField = "TC_Number";
        ddlchallan.DataValueField = "TC_Number";
        ddlchallan.DataBind();
        ddlchallan.Items.Insert(0, "--Select--");
    }
}
