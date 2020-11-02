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

    public static string distid = "";

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



    protected void btnSave_Click(object sender, EventArgs e)
    {
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
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Controller Name......');</script>");
            return;
        }

        if (txtaddress.Text == "")
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Controller Address......');</script>");
            return;
        }

        if (txtmobile.Text == "")
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Contact Number......');</script>");
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
        Response.Redirect("~/District/DPY_ControllerMaster.aspx");
    }
}