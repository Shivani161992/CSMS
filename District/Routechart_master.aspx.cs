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


public partial class District_Rootchart_master : System.Web.UI.Page
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


                    hd_fps.Value = "";
                    distid = Session["dist_id"].ToString();

                  
                    transporter();
                    getIssue();
                    fillgrid();
                    get_fps();
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





    protected void transporter()
    {
        try
        {
            string dist = distid;
        

            string qryblock = "select * from  Transporter_Table where Transport_ID='7' and Distt_ID='" + dist + "' order by Transporter_Name";


            if (con_opdms.State == ConnectionState.Closed)
            {
                con.Open();
            }


            SqlDataAdapter da = new SqlDataAdapter(qryblock, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_transporter.DataSource = ds.Tables[0];
                    ddl_transporter.DataTextField = "Transporter_Name";
                    ddl_transporter.DataValueField = "Transporter_ID";
                    ddl_transporter.DataBind();
                    ddl_transporter.Items.Insert(0, "--Select--");

                }
            }


            if (con_opdms.State == ConnectionState.Open)
            {
                con.Close();
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

        string qrey = "select  dbo.tbl_rootchart_master.*,Transporter_Table.Transporter_Name, tbl_MetaData_DEPOT.DepotName  from tbl_rootchart_master join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID=tbl_rootchart_master.IssueCenter join Transporter_Table on Transporter_Table.Transporter_ID=tbl_rootchart_master.Transporter_id where tbl_rootchart_master.DistrictId='" + distid + "' and tbl_rootchart_master.IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "' and Transport_ID='7'";
        SqlDataAdapter da = new SqlDataAdapter(qrey, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {

            grd_rootchart.DataSource = ds.Tables[0];
            grd_rootchart.DataBind();
        }


    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        if (ddl_transporter.SelectedIndex == 0 || ddl_transporter.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Transporter Name');</script>");
            return;
        }

        if (ddlBlock.SelectedIndex == 0 || ddlBlock.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select block name');</script>");
            return;
        }

      


        btnSave.Enabled = false;

        if (ddlissuecenter.SelectedIndex == 0)
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Issue Center Name......');</script>");
            return;
        }

        string dist = distid;

        string Issue = ddlissuecenter.SelectedValue;

        string IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string user = Session["OperatorIDDM"].ToString();


        foreach (GridViewRow gr in GridView1.Rows)
        {

            CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

            DropDownList ddlpayment = (DropDownList)gr.FindControl("ddl_payment");

           
            if (GchkBx.Checked == true)
            {
                string FPSCode = gr.Cells[2].Text;
                string FPSName = gr.Cells[3].Text;
                string qrey = "select *from dbo.tbl_rootchart_master where root_no='" + ddl_rootno.SelectedValue.ToString() + "' and IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "' and feed_no='" + ddlfeedno.SelectedValue.ToString() + "' and fps_code='" + FPSCode + "' ";
                SqlDataAdapter da = new SqlDataAdapter(qrey, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                //if (ds != null)
                //{
                //    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Entry for this issueCentre for root and feed  is already Done');</script>");
                //    return;



                //}


                if (ds.Tables[0].Rows.Count > 0)
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Entry for this issueCentre for root and feed  is already Done');</script>");
                    return;
                }


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                // string distance = gr.Cells[3].Text;

                string insqry = "Insert into tbl_rootchart_master (DistrictId ,IssueCenter ,Transporter_id ,root_no,feed_no,fps_name,fps_code,OperatorID ,IP_Address,block_code,Payment_mode,duration_time) values ('" + dist + "','" + Issue + "', '" + ddl_transporter.SelectedValue.ToString() + "', '" + ddl_rootno.SelectedValue.ToString() + "', '" + ddlfeedno.SelectedValue.ToString() + "',N'" + FPSName + "','" + FPSCode + "','" + user + "','" + IP + "','" + ddlBlock.SelectedValue.ToString() + "','" + ddlpayment.SelectedValue.ToString() + "','" + ddl_days.SelectedValue.ToString() + "')";
                SqlCommand cmdins = new SqlCommand(insqry, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                try
                {
                    int x = cmdins.ExecuteNonQuery();

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Succesfuly...'); </script> ");
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


            else
            {
                //Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please tick chkbx...'); </script> ");
            }

        }
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Routechart_master.aspx");
    }
 
protected void get_fps()
    {
        try
        {
            string dist = distid;
            ddl_fps_name.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT fps_code FROM [tbl_rootchart_master] where DistrictId='" + distid + "'";
            cmd.Connection = con;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["fps_code"].ToString();
                lstitem.Value = dr["fps_code"].ToString();
                //ddl_block.SelectedValue = dr["block_code"].ToString();
                ddl_fps_name.Items.Add(lstitem);
            }

            dr.Close();

            for (int i = 0; i < ddl_fps_name.Items.Count; i++)
            {

                hd_fps.Value =  hd_fps.Value + "'" + ddl_fps_name.Items[i].Value+"'" + ",";

            }


        }
        catch (Exception)
        {

        }
    }
    protected void ddlissuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBlock.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Block Name ......');</script>");
        }

        else
        {
            get_fps();
            fillgrid();
           get_block ();
            btnSave.Enabled = true;

            string blk = ddlissuecenter.SelectedValue;
            string dist = distid;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string notin = "SELECT fps_code FROM [tbl_rootchart_master]";
            SqlCommand cmdwhr = new SqlCommand(notin, con);
            SqlDataReader sqldr = cmdwhr.ExecuteReader();
            sqldr.Read();


            if (con_opdms.State == ConnectionState.Closed)
            {
                con_opdms.Open();
            }

            string qrygrid = "SELECT tbl_MpngIssuToFps.[district_code],tbl_MpngIssuToFps.[block_code],[DepotID],tbl_MpngIssuToFps.[fps_code],pds.fps_master.fps_Uname FROM tbl_MpngIssuToFps inner join pds.fps_master on pds.fps_master.fps_code=tbl_MpngIssuToFps.fps_code where tbl_MpngIssuToFps.district_code='" + distid + "' and DepotID='" + ddlissuecenter.SelectedValue.ToString() + "' and tbl_MpngIssuToFps.fps_code not in (" + hd_fps.Value + "'')   ";

            SqlCommand cmdgrid = new SqlCommand(qrygrid, con_opdms);

            SqlDataAdapter da = new SqlDataAdapter(cmdgrid);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];

                GridView1.DataBind();

            }

            else
            {
                // Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('FPS Name Not Found ......');</script>");

                GridView1.DataSource = "";

                GridView1.DataBind();
            }

            if (con_opdms.State == ConnectionState.Open)
            {
                con_opdms.Close();
            }

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

    protected void grd_rootchart_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_fps.Visible = true;
        lbl_fps_valuename.Visible = true;
        lbl_pay.Visible = true;
        Panel4.Visible = false;
        ddl_paymentfps.Visible = true;
        btn_update.Visible = true;
        btnSave.Visible = false;

        lbl_fps_valuename.Text = grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["fps_name"].ToString();
        ddl_paymentfps.SelectedValue = grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["Payment_mode"].ToString();
        ddlissuecenter.SelectedValue = grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["IssueCenter"].ToString();
        ddlBlock.SelectedValue = grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["block_code"].ToString();
        ddl_rootno.SelectedValue = grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["root_no"].ToString();
        ddlfeedno.SelectedValue = grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["feed_no"].ToString();
        ddl_transporter.SelectedValue = grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["Transporter_id"].ToString();
        if (grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["duration_time"].ToString() != null)
        {
            ddl_days.SelectedValue = grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["duration_time"].ToString();
        }

   
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        string qry = "Update  dbo.tbl_rootchart_master set duration_time='" + ddl_days.SelectedValue.ToString() + "', Payment_mode  ='" + ddl_paymentfps.SelectedValue.ToString() + "', block_code='" + ddlBlock.SelectedValue.ToString() + "', IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "',root_no= '" + ddl_rootno.SelectedValue.ToString() + "',feed_no='" + ddlfeedno.SelectedValue.ToString() + " ',Transporter_id='" + ddl_transporter.SelectedValue.ToString() + "' where IssueCenter='" + grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["IssueCenter"].ToString() + "' and block_code='" + grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["block_code"].ToString() + "' and root_no='" + grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["root_no"].ToString() + "' and feed_no='" + grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["feed_no"].ToString() + "' and fps_code='" + grd_rootchart.DataKeys[grd_rootchart.SelectedIndex].Values["fps_code"].ToString() + "'  ";
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
        lbl_pay.Visible = false;
        ddl_paymentfps.Visible = false;
        lbl_fps.Visible = false;
        lbl_fps_valuename.Visible = false;
       
    }
}