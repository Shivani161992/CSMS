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
public partial class District_basedepo_toIC : System.Web.UI.Page
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
                    txt_distance.Attributes.Add("onkeypress", "return CheckIsnondecimal(this)");
                    distid = Session["dist_id"].ToString();
                    //string qryissue = "select * from  PDS.districtsmp order by district_name where district_code='"+ distid+"'";


                    //if (con.State == ConnectionState.Closed)
                    //{
                    //    con.Open();
                    //}

                    //SqlDataAdapter da = new SqlDataAdapter(qryissue, con);
                    //DataSet ds = new DataSet();
                    //da.Fill(ds);
                    lbl_dist.Text = Session["dist_name"].ToString();


                    hd_fps.Value = "";

                    getrelatedDist();
                    //get_fps();

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



    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {


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
    protected void getrelatedDist()
    {

        try
        {
           
            ddlissuecenter.Items.Clear();

            string qryissue = "select * from  PDS.districtsmp order by district_name";


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
                    ddl_relatedDistrict.DataSource = ds.Tables[0];
                    ddl_relatedDistrict.DataTextField = "district_name";
                    ddl_relatedDistrict.DataValueField = "district_code";
                    ddl_relatedDistrict.DataBind();
                    ddl_relatedDistrict.Items.Insert(0, "--Select--");

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

    protected void getIssue()
    {
        try
        {
            string dist = distid;
            ddlissuecenter.Items.Clear();

            string qryissue = "select * from  tbl_MetaData_DEPOT where DistrictId= '23" + ddl_relatedDistrict.SelectedValue.ToString() + "' order by DepotName";


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

        string qrey = " select pds.districtsmp.Dist_name, dbo.Distance_ICtoFPS.*,tbl_MetaData_DEPOT.DepotName  from Distance_ICtoFPS join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID=Distance_ICtoFPS.IssueCenter join pds.districtsmp on pds.districtsmp.district_code=Distance_ICtoFPS.DistrictId where Distance_ICtoFPS.DistrictId= '" + distid + "'";
        SqlDataAdapter da = new SqlDataAdapter(qrey, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {

            grd_distance.DataSource = ds.Tables[0];
            grd_distance.DataBind();
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
        string mystr = "SELECT *FROM [Distance_ICtoFPS] where [DistrictId]='" + distid + "' and IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "' ";
        con.Open();
        SqlCommand cmdwhr = new SqlCommand(mystr, con);
        SqlDataReader sqldr = cmdwhr.ExecuteReader();
        sqldr.Read();

        if (sqldr.HasRows)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('District to this issuecenter already mapped......');</script>");
            return;

        }
        con.Close();
        string dist = distid;

        string Issue = ddlissuecenter.SelectedValue;

        string IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string user = Session["OperatorIDDM"].ToString();
                // string distance = gr.Cells[3].Text;

        string insqry = "Insert into Distance_ICtoFPS (DistrictId ,IssueCenter ,FPSCode ,FPSName,CreatedDate,OperatorID ,IP_Address,fps_distance) values ('" + dist + "','" + Issue + "', 'disttoIC','disttoIC',getdate(),'" + user + "','" + IP + "','" + txt_distance.Text + "')";
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

          


    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/basedepo_toIC.aspx");
    }




    protected void ddl_relatedDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        getIssue();
    }


}