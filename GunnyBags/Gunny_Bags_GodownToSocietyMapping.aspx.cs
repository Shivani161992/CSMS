using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GunnyBags_Gunny_Bags_GodownToSocietyMappings : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;


    public string sid = "";

    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    string New;
    string CutTorn;
    string Old;
    string Crop_Year;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["fdjfhxncdfh"] = null;
                ViewState["Row"] = "0";

                string DistCode = Session["dist_id"].ToString();               
                GetCropYear();
                GetGodownDistrict();
                GetSocietyDistrict();
                FillGodown();
                getPriority();
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }



    public void GetGodownDistrict()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string dist = Session["dist_id"].ToString();

                string qry = "SELECT district_code ,district_name FROM pds.districtsmp  order by district_name";
                SqlCommand cmd = new SqlCommand(qry, con);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistrictGodown.DataSource = ds.Tables[0];
                    ddlDistrictGodown.DataTextField = "district_name";
                    ddlDistrictGodown.DataValueField = "district_code";
                    ddlDistrictGodown.DataBind();
                    ddlDistrictGodown.Items.Insert(0, "Select");
                    ddlDistrictGodown.SelectedValue = dist;

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
    }

    public void GetSocietyDistrict()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string dist = Session["dist_id"].ToString();

                string qry = "SELECT district_code ,district_name FROM pds.districtsmp  order by district_name";
                SqlCommand cmd = new SqlCommand(qry, con);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistrictSociety.DataSource = ds.Tables[0];
                    ddlDistrictSociety.DataTextField = "district_name";
                    ddlDistrictSociety.DataValueField = "district_code";
                    ddlDistrictSociety.DataBind();
                    ddlDistrictSociety.Items.Insert(0, "Select");
                    //ddlDistrictSociety.SelectedValue = dist;

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
    }

    public void GetSociety()
    {
        string DistCode = ddlDistrictSociety.SelectedValue.ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                //string DistCode = Session["dist_id"].ToString();

                string select = string.Format("select Society_Id, '('+ Society_Id +')'+',' + Society_Name_Eng as Society_Name_Eng from Society2017 where  DistrictId='23" + DistCode + "' order by Society_Name_Eng ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataSource = ds.Tables[0];
                        ddlSociety.DataTextField = "Society_Name_Eng";
                        ddlSociety.DataValueField = "Society_Id";
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, "Select");
                    }
                }

                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Society Is not available'); </script> ");
                    return;
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

    protected void ddlDistrictSociety_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSociety();
        txtDistance.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
       
    }
    protected void ddlGodown_selected(object sender, EventArgs e)
    {
        LoadGridData();

        
    }
    public void FillGodown()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select god.Godown_Name+' ('+god.Godown_ID+')'  as Godown , god.Godown_ID  from tbl_MetaData_GODOWN god  where god.DistrictId ='" + ddlDistrictGodown.SelectedValue.ToString() + "' and (god.Remarks <> 'N' or god.Remarks is null)  order by god.Godown_Name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "Select");
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

    private void LoadGridData()
    {
        using (con = new SqlConnection(strcon))
        {
            using (SqlCommand cmd = new SqlCommand("getGodownToSocietyMappingDetails", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@fromdistrictid", SqlDbType.VarChar).Value = ddlDistrictGodown.SelectedValue.ToString();
            cmd.Parameters.Add("@fromgodownid", SqlDbType.VarChar).Value = ddlGodown.SelectedValue.ToString();
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Panel1.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                Panel1.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        }
    }
   
    public void GetCropYear()
    {
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
        ddlCropYear.SelectedIndex = 1;
    }

    protected void ddlSociety_selected(object sender, EventArgs e)
    {

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string DistrictGodown = ddlDistrictGodown.SelectedValue.ToString();
                string godown = ddlGodown.SelectedValue.ToString();
                string DistrictSociety = ddlDistrictSociety.SelectedValue.ToString();
                string Society = ddlSociety.SelectedValue.ToString();
                string CropYear = ddlCropYear.SelectedValue.ToString();
                string rabiKharif = ddlMSeason.SelectedValue.ToString();
                con.Open();
                //string DistCode = Session["dist_id"].ToString();

                string select = string.Format("select distance from Distance_Master_Godown Di where di.Distance_For ='PC' and di.PCCodeOrRailheadcode = '" + ddlSociety.SelectedValue.ToString() + "' and di.Godown_id = '" + ddlGodown.SelectedValue.ToString() + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtDistance.Text = ds.Tables[0].Rows[0]["distance"].ToString();
                }
                else
                {
                    txtDistance.Text = "";
                }
                
                ds.Clear();

                //Fill textboxes if details already exist in mapping table
                select = string.Format("select * from GunnyBags_GodownToSocietyMapping where FromGodownDistrict='" + DistrictGodown + "' and FromGodown='" + godown + "' and ToSocietyDistrict='" + DistrictSociety + "' and ToSociety='" + Society + "' and CropYear ='" + CropYear + "' and RabiKharif='" + rabiKharif + "'  ");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                   if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlMSeason.Enabled = false;                      
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        ddlPriority.Enabled = false;
                        bttSubmit.Enabled = false;

                        ddlMSeason.SelectedValue = ds.Tables[0].Rows[0]["RabiKharif"].ToString();                      
                        txtFromDate.Text = (ds.Tables[0].Rows[0]["FromDate"].ToString() == "" || ds.Tables[0].Rows[0]["FromDate"] == null) ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy").Replace('/', '-');
                        txtToDate.Text = (ds.Tables[0].Rows[0]["ToDate"].ToString() == "" || ds.Tables[0].Rows[0]["ToDate"] == null) ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy").Replace('/', '-');
                        ddlPriority.SelectedValue = ds.Tables[0].Rows[0]["Priority"].ToString();
                    }
                    else 
                    {
                        ddlMSeason.Enabled = true;
                        txtFromDate.Enabled = true;
                        txtToDate.Enabled = true;
                        ddlPriority.Enabled = false;
                        bttSubmit.Enabled = true;
                        txtFromDate.Text = "";
                        txtToDate.Text = "";
                        bttSubmit.Enabled = true;


                        ds.Clear();
                        string qrey = "select  * from GunnyBags_GodownToSocietyMapping where  FromGodownDistrict='" + DistrictGodown + "' and CropYear ='" + CropYear + "'   ";
                        da = new SqlDataAdapter(qrey, con);
                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            qrey = "select  * from GunnyBags_GodownToSocietyMapping where  FromGodownDistrict='" + DistrictGodown + "' and FromGodown ='" + godown + "' and CropYear ='" + CropYear + "'   ";
                            ds.Clear();
                            da = new SqlDataAdapter(qrey, con);
                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //get existing godown priority and insert record for that entry
                                ds.Clear();
                                qrey = "select distinct Priority from GunnyBags_GodownToSocietyMapping where FromGodownDistrict='" + DistrictGodown + "' and FromGodown='" + godown + "' and CropYear = '" + CropYear + "' ";
                                da = new SqlDataAdapter(qrey, con);
                                ds = new DataSet();
                                da.Fill(ds);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    string SetPriority = ds.Tables[0].Rows[0]["Priority"].ToString();
                                    ddlPriority.SelectedValue = SetPriority;
                                }


                            }
                            else
                            {
                                //get max priority +1  and insert record
                                ds.Clear();
                                qrey = "select MAX(Priority) MaxPriority from GunnyBags_GodownToSocietyMapping where FromGodownDistrict='" + DistrictGodown + "' and FromGodown !='" + godown + "' and CropYear ='" + CropYear + "'";
                                da = new SqlDataAdapter(qrey, con);
                                ds = new DataSet();
                                da.Fill(ds);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    string SetPriority = (Convert.ToInt32(ds.Tables[0].Rows[0]["MaxPriority"]) + 1).ToString();
                                    ddlPriority.SelectedValue = SetPriority;
                                }

                            }
                        }
                        else
                        {
                            //set priority =1 for new entery                            
                            string SetPriority = "1";
                            ddlPriority.SelectedValue = SetPriority;
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

    private void getPriority()
    {
        try
        {
            ddlPriority.Items.Clear();

            con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "select Count(Godown_ID) as TotalGodown from tbl_MetaData_GODOWN where (right([DistrictId],2)='" + ddlDistrictGodown.SelectedValue + "' or [DistrictId]='23" + ddlDistrictGodown.SelectedValue + "')   and (Remarks <> 'N' or Remarks is null )";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int count = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalGodown"]);
                int i = 0;
                ddlPriority.Items.Insert(0, "Select");

                for (i = 1; i <= count; i++)
                {
                    ddlPriority.Items.Add(i.ToString());
                }

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
    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        if (ddlDistrictGodown.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select godown district'); </script> ");
            return;
        }
        else if (ddlGodown.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select godown'); </script> ");
            return;
        }

        else if (ddlDistrictSociety.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select society district'); </script> ");
            return;
        }

        else if (ddlSociety.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select society'); </script> ");
            return;
        }

        else if (ddlMSeason.SelectedValue == "Select")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Marketing Season'); </script> ");
            return;
        }
        else if (txtDistance.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter distance from Distance Master'); </script> ");
            return;
        }

       else if (txtFromDate.Text == "")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter from date'); </script> ");
            return;
        }
        else if (txtToDate.Text == "")
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter ToDate'); </script> ");
            return;
        }
        else if (ddlPriority.SelectedValue == "Select")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select priority'); </script> ");
            return;
        }
        else
        {
            using (con = new SqlConnection(strcon))
                try
                {
                    string DistCode = Session["dist_id"].ToString();
                    string DistrictGodown = ddlDistrictGodown.SelectedValue.ToString();
                    string godown = ddlGodown.SelectedValue.ToString();
                    string DistrictSociety = ddlDistrictSociety.SelectedValue.ToString();
                    string Society = ddlSociety.SelectedValue.ToString();
                    string CropYear = ddlCropYear.SelectedValue.ToString();
                    string rabiKharif = ddlMSeason.SelectedValue.ToString();
                    string FromDate = getDate_MDY(txtFromDate.Text);
                    string ToDate = getDate_MDY(txtToDate.Text);
                   // string Priority = ddlPriority.SelectedValue.ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    con.Open();
                    string qrey = "select max(MappingID) as MappingID from GunnyBags_GodownToSocietyMapping ";
                    da = new SqlDataAdapter(qrey, con);

                    ds = new DataSet();
                    da.Fill(ds);

                    DataRow dr = ds.Tables[0].Rows[0];
                    gatepass = ds.Tables[0].Rows[0]["MappingID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "12" + DistCode + "0";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }

                   ds.Clear();
                    qrey = "select  * from GunnyBags_GodownToSocietyMapping where  FromGodownDistrict='"+DistrictGodown+"' and CropYear ='" + CropYear + "'   ";
                    da = new SqlDataAdapter(qrey, con);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        qrey = "select  * from GunnyBags_GodownToSocietyMapping where  FromGodownDistrict='" + DistrictGodown + "' and FromGodown ='"+godown+"' and CropYear ='" + CropYear + "'   ";
                        ds.Clear();
                        da = new SqlDataAdapter(qrey, con);
                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //get existing godown priority and insert record for that entry
                            ds.Clear();
                            qrey = "select distinct Priority from GunnyBags_GodownToSocietyMapping where FromGodownDistrict='" + DistrictGodown + "' and FromGodown='" + godown + "' and CropYear = '" + CropYear + "' ";
                            da = new SqlDataAdapter(qrey, con);
                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string SetPriority = ds.Tables[0].Rows[0]["Priority"].ToString();
                                qrey = "INSERT INTO [GunnyBags_GodownToSocietyMapping] ([MappingID],[FromGodownDistrict],[FromGodown],[ToSocietyDistrict],[ToSociety],[FromDate],[ToDate],[CropYear],[RabiKharif],[Priority],[CreatedDate],[UpdatedDate],[UserID],[IP]) values('" + gatepass + "','" + DistrictGodown + "','" + godown + "','" + DistrictSociety + "','" + Society + "','" + FromDate + "','" + ToDate + "','" + CropYear + "','" + rabiKharif + "','" + SetPriority + "', '" + System.DateTime.Now + "','" + System.DateTime.Now + "','" + DistCode + "','" + ip + "') ";
                                cmd = new SqlCommand(qrey, con);
                                cmd.ExecuteNonQuery();
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Record saved succesfully.'); </script> ");
                            }


                        }
                        else
                        { 
                         //get max priority +1  and insert record
                            ds.Clear();
                            qrey = "select MAX(Priority) MaxPriority from GunnyBags_GodownToSocietyMapping where FromGodownDistrict='" + DistrictGodown + "' and FromGodown !='" + godown + "' and CropYear ='" + CropYear + "'";
                            da = new SqlDataAdapter(qrey, con);
                            ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string SetPriority = (Convert.ToInt32(ds.Tables[0].Rows[0]["MaxPriority"]) + 1).ToString();
                                qrey = "INSERT INTO [GunnyBags_GodownToSocietyMapping] ([MappingID],[FromGodownDistrict],[FromGodown],[ToSocietyDistrict],[ToSociety],[FromDate],[ToDate],[CropYear],[RabiKharif],[Priority],[CreatedDate],[UpdatedDate],[UserID],[IP]) values('" + gatepass + "','" + DistrictGodown + "','" + godown + "','" + DistrictSociety + "','" + Society + "','" + FromDate + "','" + ToDate + "','" + CropYear + "','" + rabiKharif + "','" + SetPriority + "', '" + System.DateTime.Now + "','" + System.DateTime.Now + "','" + DistCode + "','" + ip + "') ";
                                cmd = new SqlCommand(qrey, con);
                                cmd.ExecuteNonQuery();
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Record saved succesfully.'); </script> ");
                            }

                        }
                    }
                    else
                    {
                        //insert for first record set priority =1 for new entery
                        ds.Clear();
                        string SetPriority = "1";
                        qrey = "INSERT INTO [GunnyBags_GodownToSocietyMapping] ([MappingID],[FromGodownDistrict],[FromGodown],[ToSocietyDistrict],[ToSociety],[FromDate],[ToDate],[CropYear],[RabiKharif],[Priority],[CreatedDate],[UpdatedDate],[UserID],[IP]) values('" + gatepass + "','" + DistrictGodown + "','" + godown + "','" + DistrictSociety + "','" + Society + "','" + FromDate + "','" + ToDate + "','" + CropYear + "','" + rabiKharif + "','" + SetPriority + "', '" + System.DateTime.Now + "','" + System.DateTime.Now + "','" + DistCode + "','" + ip + "') ";
                        da = new SqlDataAdapter(qrey, con);
                        ds = new DataSet();
                        da.Fill(ds);
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Record saved succesfully.'); </script> ");
                    }

                    ddlMSeason.SelectedValue = "Select";
                    ddlGodown.SelectedValue = "Select";
                    ddlDistrictSociety.SelectedValue = "Select";
                    ddlSociety.SelectedValue = "Select";
                    txtDistance.Text = "";
                    txtFromDate.Text = "";
                    txtToDate.Text = "";
                    ddlPriority.SelectedValue = "Select";
                    Panel1.Visible = false;
                    
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

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
      Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
  
}