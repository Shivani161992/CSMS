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

public partial class MillerMappingWithSociety : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
   // public SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());

    public static string distid = "";
    SqlDataAdapter da3;
    DataSet ds3;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dist_id"] != null)
            {
                if (!IsPostBack)
                {
                    distid = Session["dist_id"].ToString();

                    FillMillerDistrict();                  
                    get_SocietyDistrict();
                   // get_Society();
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


    public void FillMillerDistrict()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            string qry = "SELECT district_code ,district_name FROM pds.districtsmp  order by district_name";
            SqlCommand cmd = new SqlCommand(qry, con);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlMillerDistrict.DataSource = ds.Tables[0];
                ddlMillerDistrict.DataTextField = "district_name";
                ddlMillerDistrict.DataValueField = "district_code";
                ddlMillerDistrict.DataBind();
                ddlMillerDistrict.Items.Insert(0, "---Select---");
                ddlMillerDistrict.SelectedValue = distid;

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        catch (Exception ex)
        {

        }
    }

    public void getMiller()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string cropYear = ddlyear.SelectedValue.ToString();
            string qry = "select  distinct PMA.Mill_Name MillerID, MR.Mill_Name from PaddyMilling_Agreement_2017 PMA left join Miller_Registration_2017 MR on PMA.Mill_Name = MR.Registration_ID and PMA.Mill_Addr_District = MR.District_Code  where PMA.IsAccepted ='Y' and pma.CropYear ='" + cropYear + "'";
            SqlCommand cmd = new SqlCommand(qry, con);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlMiller.DataSource = ds.Tables[0];
                ddlMiller.DataTextField = "Mill_Name";
                ddlMiller.DataValueField = "MillerID";
                ddlMiller.DataBind();
                ddlMiller.Items.Insert(0, "---Select---");
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        catch (Exception ex)
        {

        }
    }

    public void get_SocietyDistrict()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            string qry = "SELECT district_code ,district_name FROM pds.districtsmp  order by district_name";
            SqlCommand cmd = new SqlCommand(qry, con);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSocietyDistrict.DataSource = ds.Tables[0];
                ddlSocietyDistrict.DataTextField = "district_name";
                ddlSocietyDistrict.DataValueField = "district_code";
                ddlSocietyDistrict.DataBind();
                ddlSocietyDistrict.Items.Insert(0, "---Select---");
               
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        catch (Exception ex)
        {

        }
    }

 
   
    private void get_Society()
    {
        try
        {
            string District_Id = ddlSocietyDistrict.SelectedValue.ToString();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "select Society_Id,Society_Name+','+SocPlace+' ('+Society_Id+')' as Society_Name from Society_Kharif17 where  DistrictId='23" + District_Id + "'  order by Society_Id ";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSociety.DataSource = ds.Tables[0];
                ddlSociety.DataTextField = "Society_Name";
                ddlSociety.DataValueField = "Society_Id";
                ddlSociety.DataBind();
                ddlSociety.Items.Insert(0, "---Select---");
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ex)
        {
        }
    }


    protected void ddlSocietyDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        get_Society();
       
    }

  
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getMiller();
        ddlSocietyDistrict.SelectedValue = "---Select---";
        ddlSociety.SelectedValue = "---Select---";
        txtFromDate.Text = "";
        txtToDate.Text = "";
    }

    protected void ddlSociety_SelectedIndexChanged(object sender, EventArgs e)
    {
        string districtIdMiller = ddlMillerDistrict.SelectedValue.ToString();
        string millerId = ddlMiller.SelectedValue.ToString();
        string districtIdSociety = ddlSocietyDistrict.SelectedValue.ToString();
        string societyId = ddlSociety.SelectedValue.ToString();
        string Crop_Year = ddlyear.SelectedValue.ToString();
        string CommodityId = "13";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        string query = "Select * from MillerToSocietyMapping2017 where MillerDistrict='" + districtIdMiller + "' and MillerId='" + millerId + "' and SocietyDistrictId='" + districtIdSociety + "' and SocietyID ='" + societyId + "'  and Commodity='" + CommodityId + "'   and CropYear='" + Crop_Year + "'  ";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFromDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy").Replace('/', '-');   
                txtToDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy").Replace('/', '-');
            }

    }
    
    protected void ddlMiller_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        get_SocietyDistrict();
        ddlSociety.SelectedValue = "---Select---";
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlMillerDistrict.SelectedValue.ToString() == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Miller District......');</script>");
            return;
        }       

        if (ddlMiller.SelectedValue.ToString() == "0")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Miller');</script>");
            return;
        }

        if (ddlSocietyDistrict.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Select Society District'); </script> ");
            return;
        }

        if (ddlSociety.SelectedIndex == 0)
        {
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Select Society'); </script> ");
            return;
        }

        if (txtFromDate.Text == "" )
        {
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Select From Date'); </script> ");
            return;
        }

        if (txtToDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Select To Date'); </script> ");
            return;
        }

        string districtIdMiller = ddlMillerDistrict.SelectedValue.ToString();
        string millerId = ddlMiller.SelectedValue.ToString();
        string districtIdSociety = ddlSocietyDistrict.SelectedValue.ToString();
        string societyId = ddlSociety.SelectedValue.ToString();
        string fromDate = getDate_MDY(txtFromDate.Text);
        string toDate = getDate_MDY(txtToDate.Text);
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string Crop_Year = ddlyear.SelectedValue.ToString();
        string CommodityId = "13";


        string CheckduplicateRec = "Select * from MillerToSocietyMapping2017 where MillerDistrict='" + districtIdMiller + "' and MillerId='" + millerId + "' and SocietyDistrictId='" + districtIdSociety + "' and SocietyID ='" + societyId + "'  and Commodity='" + CommodityId + "'   and CropYear='" + Crop_Year + "'  ";
        SqlDataAdapter dadup = new SqlDataAdapter(CheckduplicateRec, con);
        con.Open();
        DataSet dsDup = new DataSet();
        dadup.Fill(dsDup);
        SqlCommand cmd;
        if (dsDup.Tables[0].Rows.Count > 0)
        {
            string getMappingId = dsDup.Tables[0].Rows[0]["MappingId"].ToString();
            string getdistrictIdMiller = dsDup.Tables[0].Rows[0]["MillerDistrict"].ToString();
            string getmillerId = dsDup.Tables[0].Rows[0]["MillerId"].ToString();
            string getdistrictIdSociety = dsDup.Tables[0].Rows[0]["SocietyDistrictId"].ToString();
            string getsocietyId = dsDup.Tables[0].Rows[0]["SocietyID"].ToString();
            DateTime? getFromDate = Convert.ToDateTime(dsDup.Tables[0].Rows[0]["FromDate"]);
            DateTime? getToDate = Convert.ToDateTime(dsDup.Tables[0].Rows[0]["ToDate"]);
            DateTime? getCreatedDate = Convert.ToDateTime(dsDup.Tables[0].Rows[0]["CreatedDate"]);
            DateTime? getUpdatedDate = Convert.ToDateTime(dsDup.Tables[0].Rows[0]["UpdatedDate"]);
            string getUserID = dsDup.Tables[0].Rows[0]["UserID"].ToString();
            string getIP = dsDup.Tables[0].Rows[0]["IP"].ToString();
            string getCropYear = dsDup.Tables[0].Rows[0]["CropYear"].ToString();
            string getCommodity = dsDup.Tables[0].Rows[0]["Commodity"].ToString();

           

           SqlCommand cd;
           string InsertQuery = "insert into MillerToSocietyMapping2017_Log (MappingId,MillerDistrict,MillerId,SocietyDistrictId,SocietyID,FromDate,ToDate,CreatedDate,UpdatedDate,UserID,IP,Commodity,CropYear) values('" + getMappingId + "','" + getdistrictIdMiller + "','" + getmillerId + "','" + getdistrictIdSociety + "','" + getsocietyId + "','" + getFromDate + "','" + getToDate + "','" + getCreatedDate + "','" + getUpdatedDate + "','" + getUserID + "','" + getIP + "','" + getCommodity + "','" + getCropYear + "')";
           cmd = new SqlCommand(InsertQuery, con);
           cmd.ExecuteNonQuery();

           string UpdateQry = "update MillerToSocietyMapping2017 set FromDate='" + fromDate + "',ToDate='" + toDate + "'  ,UpdatedDate='" + System.DateTime.Now + "',IP='" + ip + "' where MillerDistrict='" + districtIdMiller + "' and MillerId='" + millerId + "' and SocietyDistrictId='" + districtIdSociety + "' and SocietyID ='" + societyId + "'  and Commodity='" + CommodityId + "'   and CropYear='" + Crop_Year + "'  ";
            cd = new SqlCommand(UpdateQry, con);
           int y= cd.ExecuteNonQuery();
           Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Sucessfuly...'); </script> ");
         
        }
        else
        {
            string InsertQuery = "insert into MillerToSocietyMapping2017 (MillerDistrict,MillerId,SocietyDistrictId,SocietyID,FromDate,ToDate,CreatedDate,UpdatedDate,IP,Commodity,CropYear) values('" + districtIdMiller + "','" + millerId + "','" + districtIdSociety + "','" + societyId + "','" + fromDate + "','" + toDate + "','" + System.DateTime.Now + "','" + System.DateTime.Now + "','" + ip + "','" + CommodityId + "','" + Crop_Year + "')";
            cmd = new SqlCommand(InsertQuery, con);
            cmd.ExecuteNonQuery();

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Saved Sucessfuly...'); </script> ");
        }

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}