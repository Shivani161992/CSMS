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

public partial class District_PCcenterto_godown_distance : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2015"].ToString());
    public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    public static string distid = "";
    string PCorrail = "";
    string distance_type = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dist_id"] != null)
            {
                if (!IsPostBack)
                {
                    txt_distance.Attributes.Add("onkeypress", "return onlyNumbers(this)");
                    distid = Session["dist_id"].ToString();
                    //rd_pccenter.Checked = true;
                    //rd_pccenter_CheckedChanged( sender,  e);
                    //ddl_relatedDistrict.SelectedValue = distid;
                    //ddl_relatedDistrict_SelectedIndexChanged(sender, e);

                    //string qryissue = "select * from  PDS.districtsmp order by district_name where district_code='"+ distid+"'";


                    //if (con.State == ConnectionState.Closed)
                    //{
                    //    con.Open();
                    //}

                    //SqlDataAdapter da = new SqlDataAdapter(qryissue, con);
                    //DataSet ds = new DataSet();
                    //da.Fill(ds);
                    lbl_dist.Text = Session["dist_name"].ToString();

                    fill_Distance_for();
                    hd_fps.Value = "";

                    getrelatedDist();
                    //get_fps();

                    //getIssue();
                    //fillgrid();


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

    protected void get_Pccenter()
    {
        try
        {
            string dist = distid;
            ddl_fps_name.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PCCodeOrRailheadcode FROM [Distance_Master_Godown] where DistrictId='" + Session["dist_id"].ToString() + "'";
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem lstitem = new ListItem();
                lstitem.Text = dr["PCCodeOrRailheadcode"].ToString();
                lstitem.Value = dr["PCCodeOrRailheadcode"].ToString();
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
    protected void get_othergodown()
    {
        if (ddl_distancefrom.SelectedValue == "11")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string qrysel = "Select distinct Registration_ID,Mill_Name From Miller_Registration where CropYear='2015-2016' and Status='1' order by Mill_Name";
            SqlDataAdapter da = new SqlDataAdapter(qrysel, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_PCeneter.DataSource = ds.Tables[0];
                    ddl_PCeneter.DataTextField = "Mill_Name";
                    ddl_PCeneter.DataValueField = "Registration_ID";
                    ddl_PCeneter.DataBind();
                    ddl_PCeneter.Items.Insert(0, "--select--");
                }
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        else
        {
            if (cons.State == ConnectionState.Closed)
            {
                cons.Open();
            }

            string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where DistrictId='23" + Session["dist_id"].ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_PCeneter.DataSource = ds.Tables[0];
                    ddl_PCeneter.DataTextField = "Godown_Name";
                    ddl_PCeneter.DataValueField = "Godown_ID";
                    ddl_PCeneter.DataBind();
                    ddl_PCeneter.Items.Insert(0, "--select--");
                }
            }

            cons.Close();
        }

    }
    protected void get_otherstorage()
    {
        try
        {
            string qryissue = "select distinct StorageCenter_Godown_map.SC_Id as DepotID,Storage_Center_Name as DepotName from  StorageCenter_Godown_map join Storage_Center_Master on Storage_Center_Master.SC_Id=StorageCenter_Godown_map.SC_Id where StorageCenter_Godown_map.DistrictId= '" + Session["dist_id"].ToString() + "' order by StorageCenter_Godown_map.SC_Id";
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
                    ddl_PCeneter.DataSource = ds.Tables[0];
                    ddl_PCeneter.DataTextField = "DepotName";
                    ddl_PCeneter.DataValueField = "DepotID";
                    ddl_PCeneter.DataBind();
                    ddl_PCeneter.Items.Insert(0, "--Select--");

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
            string qryissue = "";
            SqlDataAdapter da = new SqlDataAdapter(qryissue, con);
            DataSet ds = new DataSet();

            if (ddl_distancefrom.SelectedValue == "2" || ddl_distancefrom.SelectedValue == "5" || ddl_distancefrom.SelectedValue == "7")
            {
                qryissue = "select distinct StorageCenter_Godown_map.SC_Id as DepotID,Storage_Center_Name as DepotName from  StorageCenter_Godown_map join Storage_Center_Master on Storage_Center_Master.SC_Id=StorageCenter_Godown_map.SC_Id where StorageCenter_Godown_map.DistrictId= '" + ddl_relatedDistrict.SelectedValue.ToString() + "' order by StorageCenter_Godown_map.SC_Id";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da = new SqlDataAdapter(qryissue, con);
                ds = new DataSet();
                da.Fill(ds);
            }
            else
            {
                qryissue = "select * from  tbl_MetaData_DEPOT where DistrictId= '23" + ddl_relatedDistrict.SelectedValue.ToString() + "' order by DepotName";
                if (cons.State == ConnectionState.Closed)
                {
                    cons.Open();
                }
                da = new SqlDataAdapter(qryissue, cons);
                ds = new DataSet();
                da.Fill(ds);

            }



            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlissuecenter.DataSource = ds.Tables[0];
                    ddlissuecenter.DataTextField = "DepotName";
                    ddlissuecenter.DataValueField = "BranchId";
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

    protected void getRailhead()
    {
        try
        {
            //string dist = distid;
            //ddl_railheadno.Items.Clear();
            string qryissue = "";
            if (ddl_distancefrom.SelectedValue == "3")//From Purchase Center to Railhead
            {
                qryissue = "select RailHead_Code,RailHead_Name  from dbo.tbl_Rail_Head where district_code='" + ddl_relatedDistrict.SelectedValue.ToString() + "'";

            }
            else
            {
                qryissue = "select RailHead_Code,RailHead_Name  from dbo.tbl_Rail_Head where district_code='" + Session["dist_id"].ToString() + "'";

            }


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
                    if (ddl_distancefrom.SelectedValue == "3")//From Purchase Center to Railhead
                    {
                        ddlissuecenter.DataSource = ds.Tables[0];
                        ddlissuecenter.DataTextField = "RailHead_Name";
                        ddlissuecenter.DataValueField = "RailHead_Code";
                        ddlissuecenter.DataBind();
                        ddlissuecenter.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        ddl_PCeneter.DataSource = ds.Tables[0];
                        ddl_PCeneter.DataTextField = "RailHead_Name";
                        ddl_PCeneter.DataValueField = "RailHead_Code";
                        ddl_PCeneter.DataBind();
                        ddl_PCeneter.Items.Insert(0, "--Select--");
                    }

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
    void fill_otherpccenter()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }


        string qrey = "select Society_Id,Society_Name+','+SocPlace+'('+ Society_Id+')'as Society_Name from Society where (IsPaddy='Y' or IsWheat='Y')   and DistrictId='23" + ddl_relatedDistrict.SelectedValue.ToString() + "'  order by Society_Id";


        SqlDataAdapter da = new SqlDataAdapter(qrey, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddlissuecenter.DataSource = ds.Tables[0];
                ddlissuecenter.DataTextField = "Society_Name";
                ddlissuecenter.DataValueField = "Society_Id";
                ddlissuecenter.DataBind();
                ddlissuecenter.Items.Insert(0, "--select--");

            }

        }


    }

    void fillpccenter()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string qrey = "";

        qrey = "select Society_Id,Society_Name+','+SocPlace+'('+ Society_Id+')'as Society_Name from Society where  (IsPaddy='Y' OR IsWheat='Y')  and DistrictId='23" + Session["dist_id"].ToString() + "' and Society_Id not in(" + hd_pc.Value + "'') order by Society_Id";


        SqlDataAdapter da = new SqlDataAdapter(qrey, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddl_PCeneter.DataSource = ds.Tables[0];
                ddl_PCeneter.DataTextField = "Society_Name";
                ddl_PCeneter.DataValueField = "Society_Id";
                ddl_PCeneter.DataBind();
                ddl_PCeneter.Items.Insert(0, "--select--");


            }

        }


    }


    void fill_Distance_for()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        //distance_type = "PC";
        string qrey = "select Distance_Id,Distance_for from Distance_Type ";
        SqlDataAdapter da = new SqlDataAdapter(qrey, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_distancefrom.DataSource = ds.Tables[0];
                ddl_distancefrom.DataTextField = "Distance_for";
                ddl_distancefrom.DataValueField = "Distance_Id";
                ddl_distancefrom.DataBind();
                ddl_distancefrom.Items.Insert(0, "--select--");
            }
        }


    }



    protected void GetGodown()
    {
        cons.Open();

        string qrysel = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where BranchID='" + ddlissuecenter.SelectedValue.ToString() + "' and Remarks='Y'";
        SqlDataAdapter da = new SqlDataAdapter(qrysel, cons);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_Godown.DataSource = ds.Tables[0];
                ddl_Godown.DataTextField = "Godown_Name";
                ddl_Godown.DataValueField = "Godown_ID";
                ddl_Godown.DataBind();
                ddl_Godown.Items.Insert(0, "--select--");
            }
        }
        cons.Close();
    }
    protected void get_otherrailhead()
    {
        string qryissue = "select RailHead_Code,RailHead_Name  from dbo.tbl_Rail_Head where district_code='" + ddl_relatedDistrict.SelectedValue.ToString() + "'";
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
                ddlissuecenter.DataTextField = "RailHead_Name";
                ddlissuecenter.DataValueField = "RailHead_Code";
                ddlissuecenter.DataBind();
                ddlissuecenter.Items.Insert(0, "--Select--");


            }
        }


        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }




    }

    void getWhtUparjncntr()
    {
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                // string qrysel = "select (Society_Name+','+SocPlace)as Society_Name,Society_Id from Society where DistrictId='" + ddldistpdy.SelectedValue.ToString() + "' and IsWheat='Y' order by Society_Name";
                //string qrysel = "select Society_Id,Society_Name+','+SocPlace+'('+Society_Id+')' as Society_Name from Society where IsWheat='Y'  and DistrictId='23" + distid + "' and Society_Id not in(" + hd_pc_name.Value + "'') order by Society_Id";
                string qrysel = "select Society_Id,Society_Name+','+SocPlace as Society_Name from Society where IsWheat='Y'  and DistrictId='23" + Session["dist_id"].ToString() + "' and Society_Id not in(" + hd_pc.Value + "'') order by Society_Id ";

                SqlDataAdapter da = new SqlDataAdapter(qrysel, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_PCeneter.DataSource = ds.Tables[0];
                        ddl_PCeneter.DataTextField = "Society_Name";
                        ddl_PCeneter.DataValueField = "Society_Id";
                        ddl_PCeneter.DataBind();
                        ddl_PCeneter.Items.Insert(0, "--Select--");
                    }
                }

            }
            else
            {
            }
        }

        catch (Exception)
        {

            con.Close();
        }
        finally
        {
            con.Close();
        }


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddl_distancefrom.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Distance from...');</script>");
            return;
        }
        if (ddl_relatedDistrict.SelectedIndex == 0)
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select District Name.....');</script>");
            return;
        }
        if(ddl_distancefrom.SelectedValue.ToString()!="12")
        {
        if (ddlissuecenter.SelectedIndex == 0)
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Issue Center Name......');</script>");
            return;
        }
        }
        if (ddl_distancefrom.SelectedValue.ToString() != "12")
        {
            if (ddl_Godown.SelectedIndex == 0)
            {
                btnSave.Enabled = true;
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Godown......');</script>");
                return;
            }
        }
        if (txt_distance.Text == "" || txt_distance.Text == "0")
        {
            btnSave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter distance......');</script>");
            return;
        }
        string pc_id = "";
        if (ddl_distancefrom.SelectedValue.ToString() != "12")
        {
            pc_id = ddl_PCeneter.SelectedValue.ToString();
        }
        else
        {
            pc_id = ddl_relatedDistrict.SelectedValue.ToString();
        }
        con.Close();
        string dist = distid;

        string Issue = ddlissuecenter.SelectedValue;

        ClientIP objClientIP = new ClientIP();
        string IP = objClientIP.GETIP();

        string user = Session["OperatorIDDM"].ToString();

        if (ddl_distancefrom.SelectedValue == "11") //From Paddy Mill To Godown
        {
            if (ddl_PCeneter.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please Select Mill Name...');</script>");
                return;
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string Check = "Select * From Distance_Master_Godown where DistrictId='" + ddl_relatedDistrict.SelectedValue.ToString() + "' and PCCodeOrRailheadcode='" + ddl_PCeneter.SelectedValue.ToString() + "' and IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "' and Godown_id='" + ddl_Godown.SelectedValue.ToString() + "' and Distance_For='" + ddl_distancefrom.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(Check, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    try
                    {
                        
                        string insqry = "Insert into Distance_Master_Godown (DistrictId ,PCCodeOrRailheadcode ,IssueCenter ,Godown_id,distance,Distance_For,CreatedDate,IP_Address) values ('" + ddl_relatedDistrict.SelectedValue.ToString() + "','" + ddl_PCeneter.SelectedValue.ToString() + "','" + ddlissuecenter.SelectedValue.ToString() + "', '" + ddl_Godown.SelectedValue.ToString() + "','" + txt_distance.Text + "','" + ddl_distancefrom.SelectedValue.ToString() + "',getdate(),'" + IP + "')";
                        SqlCommand cmdins = new SqlCommand(insqry, con);
                        int count = cmdins.ExecuteNonQuery();

                        if (count > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Sucessfully....'); </script> ");
                            btnSave.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        btnSave.Enabled = false;
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
                else
                {
                    try
                    {
                        string insqry = "Update Distance_Master_Godown set distance= '" + txt_distance.Text + "' where DistrictId='" + ddl_relatedDistrict.SelectedValue.ToString() + "' and PCCodeOrRailheadcode='" + ddl_PCeneter.SelectedValue.ToString() + "' and IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "' and Godown_id='" + ddl_Godown.SelectedValue.ToString() + "' and Distance_For='" + ddl_distancefrom.SelectedValue.ToString() + "'";
                        SqlCommand cmdins = new SqlCommand(insqry, con);
                        int count = cmdins.ExecuteNonQuery();

                        if (count > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Sucessfully....'); </script> ");
                            //btnSave.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        //btnSave.Enabled = false;
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
            catch (Exception ex)
            {
                //btnSave.Enabled = false;
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
        else
        {
            if (btnSave.Text == "Save")
            {
                string mystr = "";
                if (ddl_distancefrom.SelectedValue.ToString() == "PC")
                {
                    mystr = "SELECT *FROM [Distance_Master_Godown] where [PCCodeOrRailheadcode]='" + ddl_PCeneter.SelectedValue.ToString() + "' and Godown_id='" + ddl_Godown.SelectedValue.ToString() + "' ";
                }
                else if (ddl_distancefrom.SelectedValue.ToString() == "12")
                {
                    mystr = "SELECT *FROM [Distance_Master_Godown] where [DistrictId]='" + Session["dist_id"].ToString() + "' and PCCodeOrRailheadcode='" + ddl_relatedDistrict.SelectedValue.ToString() + "' and transport_mode='"+ ddl_transportmode.SelectedValue.ToString() +"' ";

                }
                 else 
                {
                    mystr = "SELECT *FROM [Distance_Master_Godown] where [PCCodeOrRailheadcode]='" + ddl_PCeneter.SelectedValue.ToString() + "' and IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "' ";

                }
                con.Open();
                SqlCommand cmdwhr = new SqlCommand(mystr, con);
                SqlDataReader sqldr = cmdwhr.ExecuteReader();
                sqldr.Read();

                if (sqldr.HasRows)
                {
                    Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Distance already save kindly Update it from the list......');</script>");
                    return;

                }
                con.Close();

                // string distance = gr.Cells[3].Text;

                string insqry = "Insert into Distance_Master_Godown (transport_mode,DistrictId ,PCCodeOrRailheadcode ,IssueCenter ,Godown_id,distance,Distance_For,CreatedDate,IP_Address) values ('" + ddl_transportmode.SelectedValue.ToString() + "','" + Session["dist_id"].ToString() + "','" + pc_id + "','" + ddlissuecenter.SelectedValue.ToString() + "', '" + ddl_Godown.SelectedValue.ToString() + "','" + txt_distance.Text + "','" + ddl_distancefrom.SelectedValue.ToString() + "',getdate(),'" + IP + "')";
                SqlCommand cmdins = new SqlCommand(insqry, con);


                //string upsqry = "Update  dbo.TO_for_Purchesecenter set distance_pc_godown='" + txt_distance.Text + "'  where pc_id='" + ddl_PCeneter.SelectedValue.ToString() + "'and Godownid='" + ddl_Godown.SelectedValue.ToString() + "'";
                //SqlCommand cmdups = new SqlCommand(upsqry, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                try
                {
                    int x = cmdins.ExecuteNonQuery();
                    //int z = cmdups.ExecuteNonQuery();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Sucessfuly...'); </script> ");
                    //btnSave.Enabled = false;

                    //Response.Redirect("~/District/PCenterto_godown_distance.aspx");


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
                try
                {
                    string insertdistance = "";

                    if (ddl_distancefrom.SelectedValue.ToString() == "2")
                    {

                        insertdistance = "Update Distance_Master_Godown set distance= '" + txt_distance.Text + "' where PCCodeOrRailheadcode='" + ddl_PCeneter.SelectedValue.ToString() + "' and IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "'";

                    }
                    else if (ddl_distancefrom.SelectedValue.ToString() == "12")
                    {
                        insertdistance = "Update Distance_Master_Godown set distance= '" + txt_distance.Text + "' where PCCodeOrRailheadcode='" + ddl_relatedDistrict.SelectedValue.ToString() + "' and DistrictId='" + Session["dist_id"].ToString() + "' and transport_mode='" + ddl_transportmode.SelectedValue.ToString() + "'";

                    }
                      else 
                    {
                        insertdistance = "Update Distance_Master_Godown set distance= '" + txt_distance.Text + "' where PCCodeOrRailheadcode='" + ddl_PCeneter.SelectedValue.ToString() + "' and Godown_id='" + ddl_Godown.SelectedValue.ToString() + "'";

                    }
                    SqlCommand cmdins = new SqlCommand(insertdistance, con);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    int x = cmdins.ExecuteNonQuery();
                    con.Close();

                    string qry = "Update  dbo.TO_for_Purchesecenter set distance_pc_godown='" + txt_distance.Text + "'  where pc_id='" + ddl_PCeneter.SelectedValue.ToString() + "'and Godownid='" + ddl_Godown.SelectedValue.ToString() + "' ";
                    SqlCommand cmsd = new SqlCommand(qry, con);
                    con.Open();
                    int y = cmsd.ExecuteNonQuery();


                    try
                    {
                        //con.Open();
                        cmsd.ExecuteNonQuery();
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated successfully...'); </script> ");
                        btnSave.Enabled = false;

                    }
                    catch (Exception ex)
                    {
                        //Label1.Visible = true;
                        //Label1.Text = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                        //ComObj.CloseConnection();
                    }


                    fillgrid();
                    //Panel1.Visible = false;

                }
                catch
                {


                }
            }
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        txt_distance.Text = "";
        txt_distance.Enabled = true;
        btnSave.Enabled = true;
        GetGodown();
        ddl_Godown_SelectedIndexChanged(sender, e);
        //Response.Redirect("~/District/godown_distance_Master.aspx");
    }




    protected void ddl_relatedDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "";

        if (ddl_distancefrom.SelectedValue.ToString() == "4")
        {
            fill_otherpccenter();
        }
        else if (ddl_distancefrom.SelectedValue.ToString() == "3")
        {
            getRailhead();
        }
        else if (ddl_distancefrom.SelectedValue == "6")//From Railhead to Railhead
        {
            get_otherrailhead();
        }
        else if (ddl_distancefrom.SelectedValue == "12")
        {
            ddl_transportmode_SelectedIndexChanged( sender,  e);
        }
            
      
        else
        {
            getIssue();
        }
    }


    protected void ddlissuecenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_distancefrom.SelectedValue.ToString() == "2")
        {
            fillgrid();

        }
        else
        {
            if (ddlissuecenter.SelectedIndex <= 0)
            {
                ddl_Godown.Items.Clear();
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Branch....'); </script> ");
                return;
            }
            else
            {
                GetGodown();
            }
        }

    }

    void fillgrid()
    {
        try
        {
            string qrey = "";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (ddl_distancefrom.SelectedValue.ToString() == "2")
            {
                qrey = "select distinct  IssueCenter,Storage_Center_Master.Storage_Center_Name as DepotName, PCCodeOrRailheadcode ,Society_Name,Distance_Master_Godown.Godown_id,distance,Distance_Master_Godown.Godown_id as Godown_Name from Distance_Master_Godown join Society on Society.Society_Id=Distance_Master_Godown.PCCodeOrRailheadcode join Storage_Center_Master on Storage_Center_Master.SC_Id=Distance_Master_Godown.IssueCenter where Distance_Master_Godown.DistrictId= '" + Session["dist_id"].ToString() + "' and PCCodeOrRailheadcode='" + ddl_PCeneter.SelectedValue.ToString() + "' and Distance_Master_Godown.IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "'";

            }
            else if (ddl_distancefrom.SelectedValue == "11") //From Paddy Mill To Godown
            {
                grd_distance.DataSource = "";
                grd_distance.DataBind();
                txt_distance.Text = "";
                qrey = "Select distance From Distance_Master_Godown where DistrictId='" + ddl_relatedDistrict.SelectedValue.ToString() + "' and PCCodeOrRailheadcode='" + ddl_PCeneter.SelectedValue.ToString() + "' and IssueCenter='" + ddlissuecenter.SelectedValue.ToString() + "' and Godown_id='" + ddl_Godown.SelectedValue.ToString() + "' and Distance_For='" + ddl_distancefrom .SelectedValue.ToString()+ "'";
            }
     
            else
            {
                qrey = "select distinct  IssueCenter as DepotName, PCCodeOrRailheadcode ,Society_Name,Distance_Master_Godown.Godown_id,tbl_MetaData_GODOWN.Godown_Name,distance from Distance_Master_Godown join Society on Society.Society_Id=Distance_Master_Godown.PCCodeOrRailheadcode join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID=Distance_Master_Godown.Godown_id  where Distance_Master_Godown.DistrictId= '" + Session["dist_id"].ToString() + "' and PCCodeOrRailheadcode='" + ddl_PCeneter.SelectedValue.ToString() + "' and Distance_Master_Godown.Godown_id='" + ddl_Godown.SelectedValue.ToString() + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(qrey, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                txt_distance.Enabled = true;
                btnSave.Text = "Save";
            }
            else
            {
                if (ddl_distancefrom.SelectedValue == "11") //From Paddy Mill To Godown
                {
                    txt_distance.Text = ds.Tables[0].Rows[0]["distance"].ToString();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने मिल से गोदाम की दुरी डाल रखी है, आप इसे Update कर सकते है|'); </script> ");
                    txt_distance.Enabled = true;
                    btnSave.Text = "Update";
                    grd_distance.DataSource = "";
                    grd_distance.DataBind();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Distance already entered.Kindly Edit it from the list below...'); </script> ");
                    grd_distance.DataSource = ds.Tables[0];
                    grd_distance.DataBind();
                    txt_distance.Enabled = false;
                    btnSave.Text = "Update";
                    if (ddl_distancefrom.SelectedValue.ToString() == "2")
                    {
                        grd_distance.Columns[3].HeaderText = "";
                        grd_distance.Columns[2].HeaderText = "Storage Center";
                    }
                }

            }
        }
        catch
        {
        }


    }
    protected void grd_distance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string qrey = "select *  from dbo.Distance_Master_Godown where   PCCodeOrRailheadcode='" + grd_distance.DataKeys[grd_distance.SelectedIndex].Values["PCCodeOrRailheadcode"].ToString() + "' and Godown_id='" + grd_distance.DataKeys[grd_distance.SelectedIndex].Values["Godown_id"].ToString() + "' ";
            SqlCommand sqlcmd = new SqlCommand(qrey, con);
            SqlDataReader sqldr = sqlcmd.ExecuteReader();
            sqldr.Read();
            if (sqldr.HasRows)
            {
                txt_distance.Enabled = true;

                ddl_PCeneter.SelectedValue = sqldr["PCCodeOrRailheadcode"].ToString();
                ddl_Godown.SelectedValue = sqldr["Godown_id"].ToString();

                ddl_relatedDistrict.SelectedValue = sqldr["DistrictId"].ToString();

                ddlissuecenter.SelectedValue = sqldr["IssueCenter"].ToString();

                txt_distance.Text = sqldr["distance"].ToString();



                fillgrid();

                //txt_reviseddate.Text =  getDate_MDY(sqldr["revised_date"].ToString());


            }

            else
            {


            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        catch
        {

        }



        //ddlissuecenter.SelectedValue = grd_distance.DataKeys[grd_distance.SelectedIndex].Values["RuralorUrben"].ToString();
        //ddl_PCeneter.SelectedValue = grd_distance.DataKeys[grd_distance.SelectedIndex].Values["Payment_mode"].ToString();
        //ddl_Godown.SelectedValue =  grd_distance.DataKeys[grd_distance.SelectedIndex].Values["block_code"].ToString();
        //txt_distance.Text = grd_distance.DataKeys[grd_distance.SelectedIndex].Values["block_code"].ToString();
    }
    protected void ddl_Godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddl_distancefrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_PCeneter.Items.Clear();
        ddlissuecenter.Items.Clear();
        ddl_Godown.Items.Clear();
        txt_distance.Text = "";
        lbl_mode.Visible = false;
        ddl_transportmode.Visible = false;

        if (ddl_distancefrom.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Distance From.....'); </script> ");
            return;
        }
        else
        {
            lbl_issue.Text = "Branch";
            lbl_godown.Visible = true;
            ddl_Godown.Visible = true;
            getIssue();

            if (ddl_distancefrom.SelectedValue == "PC")//From Purchase Center to Godown
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_purchase.Text = "Purchase Center";
                fillpccenter();
            }
            else if (ddl_distancefrom.SelectedValue == "2")//From Purchese Center to Storage Center
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_purchase.Text = "Purchase Center";
                lbl_issue.Text = "Storage Center";
                lbl_godown.Visible = false;
                ddl_Godown.Visible = false;
                getIssue();
                fillpccenter();

            }
            else if (ddl_distancefrom.SelectedValue == "3")//From Purchase Center to Railhead
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_purchase.Text = "Purchase Center";
                lbl_issue.Text = "Railhead Name";
                fillpccenter();
                getRailhead();
                lbl_godown.Visible = false;
                ddl_Godown.Visible = false;
                ddl_Godown.Text = "NA";

            }

            else if (ddl_distancefrom.SelectedValue == "RH")//From Railhead to Godown
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_purchase.Text = "Railhead Name";
                getRailhead();
            }
            else if (ddl_distancefrom.SelectedValue == "4")//From Purchase Center to Purchase Center
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_purchase.Text = "Purchase Center";
                lbl_issue.Text = "Other Purchese Center";
                fillpccenter();
                //getRailhead();

                lbl_godown.Visible = false;
                ddl_Godown.Visible = false;
            }
            else if (ddl_distancefrom.SelectedValue == "5")//From Railhead to Storage Center
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_purchase.Text = "Railhead Name";
                lbl_issue.Text = "Storage Center";
                getRailhead();
                getIssue();
                lbl_godown.Visible = false;
                ddl_Godown.Visible = false;

            }
            else if (ddl_distancefrom.SelectedValue == "6")//From Railhead to Railhead
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_godown.Visible = false;
                ddl_Godown.Visible = false;
                lbl_purchase.Text = "Railhead Name";
                lbl_issue.Text = "Other Railhead Name";
                getRailhead();

            }
            else if (ddl_distancefrom.SelectedValue == "7")//From Storage Center to Storage Center
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_godown.Visible = false;
                ddl_Godown.Visible = false;
                lbl_purchase.Text = "Storage Center";
                lbl_issue.Text = "Other Storage Center";
                get_otherstorage();


            }
            else if (ddl_distancefrom.SelectedValue == "8")//From Storage Center to Godown
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_purchase.Text = "Storage Center";
                lbl_issue.Text = "Godown Name";
                get_otherstorage();

            }
            else if (ddl_distancefrom.SelectedValue == "10")//From Godown to Godown
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_purchase.Text = "Godown Name";
                lbl_issue.Text = "Branch";
                get_othergodown();
            }
            else if (ddl_distancefrom.SelectedValue == "11") //From Paddy Mill To Godown
            {
                ddl_PCeneter.Visible = true;
                ddl_railheadno.Visible = false;
                lbl_purchase.Text = "Paddy Mill";
                lbl_issue.Text = "Branch";
                get_othergodown();
            }
            else if (ddl_distancefrom.SelectedValue == "12") //From District to District
            {


                ddl_PCeneter.Visible = false;
                ddl_railheadno.Visible = false;
                lbl_godown.Visible = false;
              ddl_Godown.Visible = false;
               lbl_purchase.Visible = false;
               lbl_issue.Visible = false;
               ddlissuecenter.Visible = false;
               lbl_mode.Visible = true;
               ddl_transportmode.Visible = true;
                //lbl_purchase.Text = "Paddy Mill";
                //lbl_issue.Text = "Branch";

            
            }

            ddl_relatedDistrict.SelectedValue = Session["dist_id"].ToString();
            ddl_relatedDistrict_SelectedIndexChanged(sender, e);
        }
    }
    protected void ddl_transportmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_distancefrom.SelectedValue == "12") //From District to district
        {
            grd_distance.DataSource = "";
            grd_distance.DataBind();
            txt_distance.Text = "";
           string qry = "Select distance From Distance_Master_Godown where DistrictId='" + Session["dist_id"].ToString() + "' and PCCodeOrRailheadcode='" + ddl_relatedDistrict.SelectedValue.ToString() + "' and transport_mode='" + ddl_transportmode.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Distance alreatdy entered| you can update it...'); </script> ");

                txt_distance.Enabled = true;
                txt_distance.Text = ds.Tables[0].Rows[0]["distance"].ToString();
                btnSave.Text = "Update";
            }
            else
            {
                txt_distance.Enabled = true;
           
                btnSave.Text = "Save";
            }
         
           

        }
    }
}