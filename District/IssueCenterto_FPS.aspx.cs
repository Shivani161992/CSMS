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

public partial class District_LinkCooperativesociety : System.Web.UI.Page
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
             
                    distid = Session["dist_id"].ToString();

                    hd_fps.Value = "";
                    

                    //get_fps();
                 
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

   


    protected void get_fps()
    {
        try
        {
            string dist = distid;
            ddl_fps_name.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT FPSCode FROM [Distance_ICtoFPS] where DistrictId='" + distid + "'";
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["FPSCode"].ToString();
                lstitem.Value = dr["FPSCode"].ToString();
                //ddl_block.SelectedValue = dr["block_code"].ToString();
                ddl_fps_name.Items.Add(lstitem);
            }

            dr.Close();

            for (int i = 0; i < ddl_fps_name.Items.Count; i++)
            {

                hd_fps.Value = hd_fps.Value + ddl_fps_name.Items[i].Value + ",";

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

 

  



        string dist = distid;

        string Issue = ddlissuecenter.SelectedValue;


     

        string IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string user = Session["OperatorIDDM"].ToString();


       

      

        foreach (GridViewRow gr in GridView1.Rows)
        {
           
           CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

           TextBox txtdist = (TextBox)gr.FindControl("txtdistance");
           DropDownList ddlgodown = (DropDownList)gr.FindControl("ddl_godown");
            if (GchkBx.Checked == true)
            {
                string FPSCode = gr.Cells[1].Text;
                string FPSName = gr.Cells[2].Text;

               // string distance = gr.Cells[3].Text;

                string insqry = "Insert into Distance_ICtoFPS (DistrictId ,IssueCenter ,FPSCode ,FPSName,CreatedDate,OperatorID ,IP_Address,fps_distance) values ('" + dist + "','" + Issue + "', '" + FPSCode + "',N'" + FPSName + "',getdate(),'" + user + "','" + IP + "','" + txtdist.Text + "')";
                SqlCommand cmdins = new SqlCommand(insqry, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                try
                {
                    int x = cmdins.ExecuteNonQuery();

                   Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Sucessfuly...'); </script> ");
                  
                    
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
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }




    protected void ddlissuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlissuecenter.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select IssueCenter Name ......');</script>");
        }

        else
        {
            get_fps();
            btnSave.Enabled = true;

            string blk =ddlissuecenter.SelectedValue;
            string dist = distid;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string notin = "SELECT FPSCode FROM [Distance_ICtoFPS]";
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
}
