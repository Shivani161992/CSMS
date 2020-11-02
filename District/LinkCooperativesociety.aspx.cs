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
                    txtmobile.Attributes.Add("onkeypress", "return CheckIsnondecimal(this)");

                    hd_fps.Value = "";
                    distid = Session["dist_id"].ToString();
                     
                    get_block();

                    getTehsil();

                    GetBank();
                    get_fps();
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

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlBlock.SelectedValue == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Block Name ......');</script>");
        }

        else
        {
            btnSave.Enabled = true;

            string blk = ddlBlock.SelectedValue;
            string dist = distid;
            if (con_opdms.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string notin = "SELECT FPSCode FROM [Link_CooperativeSociety]";
            SqlCommand cmdwhr = new SqlCommand(notin, con);
            SqlDataReader sqldr = cmdwhr.ExecuteReader();
            sqldr.Read();
            if (con_opdms.State == ConnectionState.Closed)
            {
                con_opdms.Open();
            }

            string qrygrid = "SELECT * FROM pds.fps_master where district_code='" + dist + "' and block_code='" + blk + "' and del_status='False' and fps_code not in (" + hd_fps.Value + "'') order by fps_Uname ";

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

    protected void getTehsil()
    {
        try
        {
            string dist = distid;
            ddltehsil.Items.Clear();

            string qrytehsil = "select * from  Tehsils where District_code= '23" + dist + "' order by Tehsil_Name";


            if (con_opdms.State == ConnectionState.Closed)
            {
                con_opdms.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(qrytehsil, con_opdms);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddltehsil.DataSource = ds.Tables[0];
                    ddltehsil.DataTextField = "Tehsil_Name";
                    ddltehsil.DataValueField = "TehsilCode";
                    ddltehsil.DataBind();
                    ddltehsil.Items.Insert(0, "--Select--");

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

    protected void GetBank()
    {
        try
        {
            string dist = distid;
            ddlBank.Items.Clear();

            string qrybank = "select * from  Bank_Master_New order by Bank_Name";

            //string qrybank = "select * from  Bank_Master where District_code=" + dist + " order by Bank_Name";

            SqlCommand cmd = new SqlCommand(qrybank, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            SqlDataAdapter da = new SqlDataAdapter(qrybank, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBank.DataSource = ds.Tables[0];
                    ddlBank.DataTextField = "Bank_Name";
                    ddlBank.DataValueField = "Bank_ID";
                    ddlBank.DataBind();
                    ddlBank.Items.Insert(0, "--Select--");

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
    protected void get_fps()
    {
        try
        {
            string dist = distid;
            ddl_fps_name.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT FPSCode FROM [Link_CooperativeSociety]";
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr ;
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

        if (ddltehsil.SelectedIndex == 0)
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Tehsil Name......');</script>");
            return;
        }

        if (ddlBank.SelectedIndex == 0)
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Bank Name......');</script>");
            return;
        }

        if (txtSocName.Text == "")
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Society Name......');</script>");
            return;
        }

        if (txtSocName.Text == "")
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Society Name......');</script>");
            return;
        }

        if (txtmobile.Text == "")
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Contact Number......');</script>");
            return;
        }

        if (txtbranch.Text == "")
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript> alert('Enter Bank Branch Name......');</script>");
            return;
        }

        if (txtaccount.Text == "")
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Bank Account Number......');</script>");
            return;
        }


        string dist = distid;

        string Issue = ddlissuecenter.SelectedValue;

        string SocName = txtSocName.Text.Trim();

        string Socaddress = txtaddress.Text.Trim();

        string Block = ddlBlock.SelectedValue;

        string Contact = txtmobile.Text.Trim();

        string Tehsil = ddltehsil.SelectedValue;

        string BankName = ddlBank.SelectedValue;

        string BranchName = txtbranch.Text.Trim();

        string BankAccount = txtaccount.Text.Trim();

        string IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string user = Session["OperatorIDDM"].ToString();


        string qrey = "select max(CooperativeSoc_Code) as CooperativeSoc_Code from dbo.Link_CooperativeSociety where DistrictId='" + distid + "'";

        SqlCommand cmd_soccode = new SqlCommand(qrey, con);
        con.Open();
        SqlDataReader sqldr = cmd_soccode.ExecuteReader();
        sqldr.Read();

        string soccode = sqldr["CooperativeSoc_Code"].ToString();

        if (soccode == "")
        {
            soccode = 23 + distid + "201";

        }
        else
        {
            Int32 socnum = Convert.ToInt32(soccode);
            socnum = socnum + 1;
            soccode = socnum.ToString();

        }

        sqldr.Close();

        foreach (GridViewRow gr in GridView1.Rows)
        {
           
           CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

           TextBox txtdist = (TextBox)gr.FindControl("txtdistance");

            if (GchkBx.Checked == true)
            {
                string FPSCode = gr.Cells[1].Text;
                string FPSName = gr.Cells[2].Text;

               // string distance = gr.Cells[3].Text;

                string insqry = "Insert into Link_CooperativeSociety (DistrictId ,IssueCenter ,CooperativeSoc_Code,CoperativeSocietyName ,SocAddress,FPSBlock,ContactNumber ,TehsilCode ,BankId ,BranchName ,AccountNumber,FPSCode ,FPSName,CreatedDate,OperatorID ,IP_Address,fps_distance) values ('" + dist + "','" + Issue + "', '" + soccode + "', N'" + SocName + "',N'" + Socaddress + "','" + Block + "','" + Contact + "','" + Tehsil + "','" + BankName + "',N'" + BranchName + "','" + BankAccount + "','" + FPSCode + "',N'" + FPSName + "',getdate(),'" + user + "','" + IP + "','"+txtdist.Text+"')";
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

   

}
