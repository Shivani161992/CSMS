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
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class District_DPY_ControllerMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());
    MoveChallan mobj1 = null;
    protected Common ComObj = null, cmn = null;
    public static string distid = "";
    Transporter tobj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dist_id"] != null)
            {
                if (!IsPostBack)
                {
                    txtmobile.Attributes.Add("onkeypress", "return CheckIsnondecimal(this)");

             
                    distid = Session["dist_id"].ToString();

                    get_block();
                
                    getIssue();
                    fillgrid();
                }
            }

            else
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }

        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void get_block()
    {
        try
        {
            string dist = distid;
            ddlBlock.Items.Clear();

            string qryblock = "select * from  pds.block_master where District_code=" + dist + " order by Block_name";


            if (con_opdms.State == ConnectionState.Closed)
            {
                con_opdms.Open();
            }


            SqlDataAdapter da = new SqlDataAdapter(qryblock, con_opdms);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBlock.DataSource = ds.Tables[0];
                    ddlBlock.DataTextField = "Block_Uname";
                    ddlBlock.DataValueField = "block_code";
                    ddlBlock.DataBind();
                    ddlBlock.Items.Insert(0, "--Select--");

                }
            }


            if (con_opdms.State == ConnectionState.Open)
            {
                con_opdms.Close();
            }
        }
        catch (Exception)
        {

        }
    }

  

    

    protected void getIssue()
    {
        try
        {
            string dist = distid;
            ddlissuecenter.Items.Clear();

            string qryissue = "select * from  tbl_MetaData_DEPOT where DistrictId= '23" + dist + "' order by DepotName";


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(qryissue, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlissuecenter.DataSource = ds.Tables[0];
                    ddlissuecenter.DataTextField = "DepotName";
                    ddlissuecenter.DataValueField = "DepotID";
                    ddlissuecenter.DataBind();
                    ddlissuecenter.Items.Insert(0, "--Select--");

                }
            }


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception)
        {

        }
    }
    void fillgrid()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string qrey = "select  dbo.DPY_ControllerMaster.*, tbl_MetaData_DEPOT.DepotName from DPY_ControllerMaster join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID=DPY_ControllerMaster.IssueCenter where DPY_ControllerMaster.DistrictId='" + distid + "'";
        SqlDataAdapter da = new SqlDataAdapter(qrey, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {

            grd_prabhari.DataSource = ds.Tables[0];
            grd_prabhari.DataBind();
        }


    }
 

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string qrey = "select *from dbo.DPY_ControllerMaster where FPSBlock='" + ddlBlock.SelectedValue.ToString() + "' and IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(qrey, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds != null)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Entry for this issueCentre and block is already Done');</script>");
            return;
           

            con.Close();
        }
        else
        {

        }
        con.Close();
        btnSave.Enabled = false;
      
        if (ddlissuecenter.SelectedIndex == 0)
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Issue Center Name......');</script>");
            return;
        }

        if (ddlBlock.SelectedIndex == 0)
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select FPS Block Name......');</script>");
            return;
        }



        if (txtContName.Text == "")
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Transporter Name......');</script>");
            return;
        }


        if (txtmobile.Text == "")
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Moblie Number......');</script>");
            return;
        }
         string Issue = ddlissuecenter.SelectedValue;

           

            string Socaddress = txtaddress.Text.Trim();

            string Block = ddlBlock.SelectedValue;

            string Contact = txtmobile.Text.Trim();


            string IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

            string user = Session["OperatorIDDM"].ToString();
    

      

                string insqry = "INSERT INTO dbo.DPY_ControllerMaster(DistrictId,IssueCenter,DPYCName,Address,FPSBlock,ContactNumber,CreatedDate,OperatorID,IP_Address) VALUES('" + distid + "','" + Issue + "', '" + txtContName.Text  + "', '" + txtaddress.Text + "','" + Block + "','" + txtmobile.Text + "',getdate(),'" + user + "','" + IP + "')";
                SqlCommand cmdins = new SqlCommand(insqry, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                try
                {
                    int x = cmdins.ExecuteNonQuery();

                    btnSave.Enabled = false;

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Sucessfuly...'); </script> ");

                    btnSave.Enabled = false;
                    fillgrid();
                }

                catch
                {
                    btnSave.Enabled = true;
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Problem Arise, pls try again...'); </script> ");
                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

            }

        
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dwarpraday_prabhari.aspx");
    }
    protected void grd_prabhari_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlissuecenter.SelectedValue = grd_prabhari.DataKeys[grd_prabhari.SelectedIndex].Values["IssueCenter"].ToString();
        ddlBlock.SelectedValue = grd_prabhari.DataKeys[grd_prabhari.SelectedIndex].Values["FPSBlock"].ToString();
        txtContName.Text = grd_prabhari.SelectedRow.Cells[2].Text.Trim();
        txtmobile.Text = grd_prabhari.SelectedRow.Cells[3].Text.Trim();
        txtaddress.Text = grd_prabhari.SelectedRow.Cells[4].Text.Trim();
        btnSave.Visible = false;
        btn_update.Visible = true;
        
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
       
        string qry = "Update  dbo.DPY_ControllerMaster set FPSBlock='" + ddlBlock.SelectedValue.ToString() + "', IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "',DPYCName= '" + txtContName.Text + "',Address='" + txtaddress.Text + " ',ContactNumber='" + txtmobile.Text + "' where FPSBlock='" + grd_prabhari.DataKeys[grd_prabhari.SelectedIndex].Values["FPSBlock"].ToString() + "' and IssueCenter='" + grd_prabhari.DataKeys[grd_prabhari.SelectedIndex].Values["IssueCenter"].ToString() + "' ";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = qry;
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated successfully...'); </script> ");

        }
        catch (Exception ex)
        {
          
          lbl_error.Text = ex.Message;
        }
        finally
        {
            con.Close();
         
        }

        fillgrid();
        btn_update.Visible = false;
       
          
    }
}