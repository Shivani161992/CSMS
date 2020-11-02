using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Threading;
using System.Collections.Generic;


public partial class PcGdn_Insp_QC_PC : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_PPMS2015_16"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["Appconstr_MPMS2015_16"].ToString());

    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2016"].ToString());

    string usertype;
    public string GenerateOTP = "", OTPSMS = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["District_id"] != null)
        {
             usertype = Session["User_id"].ToString();

            if (!IsPostBack)
            {
                filldistrict();

                fillTehsil();

                txtInsp_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
                txtInsp_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtInsp_date.Attributes.Add("onchange", "return chksqltxt(this)");
                txtInsp_date.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtProc_date.Attributes.Add("onkeypress", "return CheckCalDate(this)");
                txtProc_date.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtProc_date.Attributes.Add("onchange", "return chksqltxt(this)");
                txtProc_date.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txt_paymentDate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
                txt_paymentDate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txt_paymentDate.Attributes.Add("onchange", "return chksqltxt(this)");
                txt_paymentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txt_accept_recddate.Attributes.Add("onkeypress", "return CheckCalDate(this)");
                txt_accept_recddate.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txt_accept_recddate.Attributes.Add("onchange", "return chksqltxt(this)");
                txt_accept_recddate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtMob_one.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                txtMob_two.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                txtMob_three.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                txtMob_four.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                txtMob_five.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

                txtPC_Mobile.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                txtNodal_Mobile.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

                txtInspDate_Kharidi.Attributes.Add("onkeypress", "return IsNumericProcQty(event,this)");

                txt_totalkharidi.Attributes.Add("onkeypress", "return IsNumericProcQty(event,this)");

                txt_totalTransportation.Attributes.Add("onkeypress", "return IsNumericProcQty(event,this)");

                txt_remainTransport.Attributes.Add("onkeypress", "return IsNumericProcQty(event,this)");

                txt_totalBardana.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                txt_usedBardana.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                txt_remainBardana_Gathan.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                txt_remain_bardana.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                txt_totalbags_allparameter.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
                txtregister_Farmer.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

                txt_totalreject_qty.Attributes.Add("onkeypress", "return IsNumericProcQty(event,this)");

                hlinkpdo.Attributes.Add("onclick", "window.open('Print_PCInsp.aspx',null,'left=800, top=800, height=900, width= 800, status=n o, resizable= yes, scrollbars= yes, toolbar= no,location= no, menubar= no');");

            }
        }
    }

    void filldistrict()
    {
        string qrydist = "select district_code , district_name from [pds].[districtsmp]";
        SqlCommand cmd = new SqlCommand(qrydist, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.DataSource = ds.Tables[0];

            ddlDistrict.DataTextField = "district_name";
            ddlDistrict.DataValueField = "district_code";

            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "-Select-");

            ddlDistrict.SelectedValue = Session["District_id"].ToString();

            ddlDistrict.Enabled = false;

        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    void fillTehsil()
    {
        string distid = Session["District_id"].ToString();

        string qrydist = "SELECT  [TehsilCode] ,[Tehsil_Name] FROM [pds].[Tehsilmp] where District_Code = '23"+distid+"'";
        SqlCommand cmd = new SqlCommand(qrydist, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlTehsil.DataSource = ds.Tables[0];

            ddlTehsil.DataTextField = "Tehsil_Name";
            ddlTehsil.DataValueField = "TehsilCode";

            ddlTehsil.DataBind();
            ddlTehsil.Items.Insert(0, "-Select-");
                       
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCrop.SelectedItem.Text == "-चुने-")
        {

        }

        else
        {
            if (ddlCrop.SelectedItem.Value == "22")  // wheat
            {
                getWhtUparjncntr();
            }

            else if (ddlCrop.SelectedItem.Value == "13")  // paddy
            {
                getpadyUparjncntr();
            }

            else if (ddlCrop.SelectedItem.Value == "12")    // mota anaj
            {
                getcorgrnUparjncntr();
            }
        }
    }
    
    void getpadyUparjncntr()
    {
        string distid = Session["District_id"].ToString();

        try
        {
            if (con_paddy != null)
            {
                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }

                string qrysel = "select Society_Id , Society_Name + ' (' + Society_Id + ')' Society_Name from society where DistrictId = '23" + distid + "' order by Society_Id";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlPCName.DataSource = ds.Tables[0];
                        ddlPCName.DataTextField = "Society_Name";
                        ddlPCName.DataValueField = "Society_Id";
                        ddlPCName.DataBind();
                        ddlPCName.Items.Insert(0, "--Select--");

                    }
                }

            }
            else
            {

            }
        }

        catch (Exception)
        {
            if (con_paddy.State == ConnectionState.Open)
            {
                con_paddy.Close();
            }
        }
        finally
        {

            if (con_paddy.State == ConnectionState.Open)
            {
                con_paddy.Close();
            }

        }

    }

    void getcorgrnUparjncntr()
    {

        string distid = Session["District_id"].ToString();

        try
        {
            if (con_Maze != null)
            {
                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }

                string qrysel = "select Society_Id , Society_Name + ' (' + Society_Id + ')' Society_Name from society where DistrictId = '23" + distid + "' order by Society_Id";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlPCName.DataSource = ds.Tables[0];
                        ddlPCName.DataTextField = "Society_Name";
                        ddlPCName.DataValueField = "Society_Id";
                        ddlPCName.DataBind();
                        ddlPCName.Items.Insert(0, "--Select--");

                    }
                }

            }
            else
            {

            }
        }

        catch (Exception)
        {
            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }
        }
        finally
        {

            if (con_Maze.State == ConnectionState.Open)
            {
                con_Maze.Close();
            }

        }

    }

    void getWhtUparjncntr()
    {
        string distid = Session["District_id"].ToString();

        try
        {
            if (con_WPMS != null)
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }

                string qrysel = "select Society_Id , Society_Name + ' (' + Society_Id + ')' Society_Name from society where DistrictId = '23" + distid + "' order by Society_Id";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlPCName.DataSource = ds.Tables[0];
                        ddlPCName.DataTextField = "Society_Name";
                        ddlPCName.DataValueField = "Society_Id";
                        ddlPCName.DataBind();
                        ddlPCName.Items.Insert(0, "--Select--");

                    }
                }

            }
            else
            {

            }
        }

        catch (Exception)
        {
            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }
        }
        finally
        {

            if (con_WPMS.State == ConnectionState.Open)
            {
                con_WPMS.Close();
            }

        }


    }

    protected void ddlPCName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCrop.SelectedItem.Text == "-चुने-")
        {

        }

        else
        {
            if (ddlCrop.SelectedItem.Value == "22")  // wheat
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }

                string qrysel = "select COUNT(farmer_Id)count_farmer,ManagerName , MgrMobileNo from Initial inner join FarmerRegistration on FarmerRegistration.Procured_SocietyID = Initial.Society_Id and FarmerRegistration.District_Id = Initial.District_ID where society_id = '" + ddlPCName.SelectedValue + "' group by ManagerName , MgrMobileNo";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_WPMS);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string mngrname = ds.Tables[0].Rows[0]["ManagerName"].ToString();

                    string mngrmobile = ds.Tables[0].Rows[0]["MgrMobileNo"].ToString();

                    string countfarmer = ds.Tables[0].Rows[0]["count_farmer"].ToString();

                    txtPC_manager.Text = mngrname;

                    txtPC_Mobile.Text = mngrmobile;

                    txtregister_Farmer.Text = countfarmer;
                }

                else
                {
                    txtPC_manager.Text = "";

                    txtPC_Mobile.Text = "";

                    txtregister_Farmer.Text = "";
                }

            }

            else if (ddlCrop.SelectedItem.Value == "13")  // paddy
            {
                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }

                string qrysel = "select COUNT(farmer_Id)count_farmer,ManagerName , MgrMobileNo from Initial inner join FarmerRegistration on FarmerRegistration.Procured_SocietyID = Initial.Society_Id and FarmerRegistration.District_Id = Initial.District_ID where society_id = '" + ddlPCName.SelectedValue + "' group by ManagerName , MgrMobileNo";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string mngrname = ds.Tables[0].Rows[0]["ManagerName"].ToString();

                    string mngrmobile = ds.Tables[0].Rows[0]["MgrMobileNo"].ToString();

                    string countfarmer = ds.Tables[0].Rows[0]["count_farmer"].ToString();

                    txtPC_manager.Text = mngrname;

                    txtPC_Mobile.Text = mngrmobile;

                    txtregister_Farmer.Text = countfarmer;

                }

                else
                {
                    txtPC_manager.Text = "";

                    txtPC_Mobile.Text = "";

                    txtregister_Farmer.Text = "";
                }
            }

            else if (ddlCrop.SelectedItem.Value == "12")    // mota anaj
            {
                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }

                string qrysel = "select COUNT(farmer_Id)count_farmer,ManagerName , MgrMobileNo from Initial inner join FarmerRegistration on FarmerRegistration.Procured_SocietyID = Initial.Society_Id and FarmerRegistration.District_Id = Initial.District_ID where society_id = '" + ddlPCName.SelectedValue + "' group by ManagerName , MgrMobileNo";
                SqlDataAdapter da = new SqlDataAdapter(qrysel, con_Maze);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string mngrname = ds.Tables[0].Rows[0]["ManagerName"].ToString();

                    string mngrmobile = ds.Tables[0].Rows[0]["MgrMobileNo"].ToString();

                    string countfarmer = ds.Tables[0].Rows[0]["count_farmer"].ToString();

                    txtPC_manager.Text = mngrname;

                    txtPC_Mobile.Text = mngrmobile;

                    txtregister_Farmer.Text = countfarmer;
                }

                else
                {
                    txtPC_manager.Text = "";

                    txtPC_Mobile.Text = "";

                    txtregister_Farmer.Text = "";
                }
            }
        }

    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txtName_one.Text == string.Empty || txtMob_one.Text == string.Empty || txtdesig_one.Text == string.Empty)
        {
            txtName_one.Focus();

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('निरीक्षण अधिकारी की जानकारी पूर्ण भरी जायेगी |'); </script> ");
            return;
        }

        if (txtInsp_date.Text == string.Empty)
        {
            txtInsp_date.Focus();

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('निरीक्षण दिनाँक चुने |'); </script> ");
            return;
        }

        try
        {
            string nameone = txtName_one.Text;
            string nametwo = txtName_two.Text;
            string namethree = txtName_three.Text;
            string namefour = txtName_four.Text;
            string namefive = txtName_five.Text;

            string mobone = txtMob_one.Text;
            string mobtwo = txtMob_two.Text;
            string mobthree = txtMob_three.Text;
            string mobfour = txtMob_four.Text;
            string mobfive = txtMob_five.Text;

            string desigone = txtdesig_one.Text;
            string desigtwo = txtdesig_two.Text;
            string desigthree = txtdesig_three.Text;
            string desigfour = txtdesig_four.Text;
            string desigfive = txtdesig_five.Text;

            string inspectdate = getDate_MDY(txtInsp_date.Text);
            string insp_district = Session["District_id"].ToString();
            string Tehsil = ddlTehsil.SelectedValue;

            string croptype = ddlCrop.SelectedItem.Value;
            string PCname = ddlPCName.SelectedValue;
            string PCmanager = txtPC_manager.Text;
            string pcmanager_mobile = txtPC_Mobile.Text;

            string Nodalofficer = txtNodal_officer.Text;
            string Nodaldesig = txtNodal_desig.Text;
            string nodalmobile = txtNodal_Mobile.Text;

            string regsfarmer = txtregister_Farmer.Text;
            string procdate = getDate_MDY(txtProc_date.Text);

            string proc_onInspdate = txtInspDate_Kharidi.Text;

            string totalproc = txt_totalkharidi.Text;
            string Transport = txt_totalTransportation.Text;
            string remainTransport = txt_remainTransport.Text;

            string ISFaqSample = ddl_FaqSample.SelectedItem.Value;
            string ISParkhi = ddl_Parkhi.SelectedItem.Value;
            string ISElectronicWeigh = ddl_electronicBalance.SelectedItem.Value;
            string ISPlasticBag = ddl_plasticbag.SelectedItem.Value;
            string ISeal = ddl_seal.SelectedItem.Value;
            string ISMoist_Machine = ddl_moistMachine.SelectedItem.Value;
            string IsenamelPlate = ddl_enamelplate.SelectedItem.Value;
            string IsFilter = ddl_filter.SelectedItem.Value;

            string IsBlower = ddl_blower.SelectedItem.Value;

            string IsTirpal = ddl_TirplaSilai.SelectedItem.Value;
            
            string IsDouble_Silai = ddl_doubleSilai.SelectedItem.Value;
            string Double_RejectTruck = txt_truckreject_dueDoubleSilai.Text;

            string IsRej_DSilai = ddl_reject_doubleSilai.SelectedItem.Value;
            string IsStich = ddl_stencilTag.SelectedItem.Value;
            string TotalTruck_RejectdueStencil = txt_truckreject_duestencil.Text;
            string IsRejectSample = ddl_rejectSample.SelectedItem.Value;
            string IsRegisterEntry = ddl_registerEntry.SelectedItem.Value;
            string RejectQty = txt_rejectQty.Text;
            string TotalBardana = txt_totalBardana.Text;
            string UsedBardana = txt_usedBardana.Text;
            string RemainBardana_Gathan = txt_remainBardana_Gathan.Text;
            string RemainBardana_Loos = txt_remain_bardana.Text;

            string IsManakBharti = ddl_ManikBharti.SelectedItem.Value;

            string reason_manakBharti = txt_reason_manakbharti.Text;
            string PaymenttoFamrerDate = getDate_MDY(txt_paymentDate.Text);
            string IsLatePayment = ddl_last3days.SelectedItem.Value;

            string IsVerify_Taulkaata = ddl_verify_taulkanta.SelectedItem.Value;
            string Isreverse_bagBharti = ddl_paddyBharti_return.SelectedItem.Value;

            string rejectTruck_Inspdate = txt_totalreject_truck.Text;
            string rejectQty_Inspdate = txt_totalreject_qty.Text;

            string AcceptDate_Recd = getDate_MDY(txt_accept_recddate.Text);
            string ReadyBags_Transport = txt_totalbags_allparameter.Text;

            string OtherPoint = txt_otherpoint.Text;

            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string insqry = "Insert into PC_Inspect_2016Paddy ([District]  ,[Tehsil]  ,[PC_Crop] ,[Insp_Pc] ,[Officer_one] ,[Officer_two] ,[Officer_three] ,[Officer_four] ,[Officer_five] ,[OfficerMobile_One] ,[OfficerMobile_Two] ,[OfficerMobile_Three] ,[OfficerMobile_Four] ,[OfficerMobile_Five] ,[OfficerDesig_One] ,[OfficerDesig_Two] ,[OfficerDesig_Three] ,[OfficerDesig_Four] ,[OfficerDesig_Five]  ,[Inspection_Date] ,[PC_ManagerName] ,[PC_ManagerMobile] ,[NodalOfficer_Name] ,[NodalOfficer_Desig] ,[NodalOfficer_Mobile] ,[PC_regsiterFarmer] ,[PC_ProcDate] ,[Proc_onInspection] ,[Proc_totalKharidi] ,[Proc_totalTransport] ,[Proc_remainQty] ,[FAQ_Sample] ,[Parkhi] ,[Electronic_Balance] ,[Plastic_Bags] ,[Peetal_Seal] ,[Moisture_Machine] ,[Enamel_Plate] ,[Filter] ,[Thresar_Blower] ,[Tirpal_Silai] ,[Double_Silai] ,[RejectTruck_dueDoubleSilai] ,[Stencil_Tags] ,[RejectTruck_DuestencilTags] ,[RejectSample_Entry] ,[AllRegister_Entry] ,[RejectStock_dueQualityCheck] ,[Totalrecd_Bardana] ,[UsedBardana] ,[RemainBardana_Gathan] ,[LoosBardana]  ,[ManakBharti_Weight] ,[Reason_ManakBharti_Weight] ,[Payment_toFarmerDate] ,[PaymenT_Late3Days] ,[Verify_TaulKanta]  ,[ReverseBags_Packing] ,[Totalreject_trucks] ,[TotalReject_Qty] ,[AcceptanceRecd_toDate] ,[TotalBags_allParameter] ,[OtherReason] ,[CreatedDate]  ,[IPAddress] ,[InspLevel],RejectTruck_DoubleSilai)  Values ('" + insp_district + "' , '" + Tehsil + "' ,'" + croptype + "', '" + PCname + "' , '" + nameone + "' , '" + nametwo + "' , '" + namethree + "','" + namefour + "','" + namefive + "','" + mobone + "','" + mobtwo + "','" + mobthree + "','" + mobfour + "','" + mobfive + "' ,'" + desigone + "' ,'" + desigtwo + "' , '" + desigthree + "' , '" + desigfour + "' , '" + desigfive + "' , '" + inspectdate + "' , '" + PCmanager + "' , '" + pcmanager_mobile + "' , '" + Nodalofficer + "' , '" + Nodaldesig + "' , '" + nodalmobile + "' ,'" + regsfarmer + "' , '" + procdate + "' , '" + proc_onInspdate + "' , '" + totalproc + "' , '" + Transport + "' , '" + remainTransport + "' , '" + ISFaqSample + "' , '" + ISParkhi + "' , '" + ISElectronicWeigh + "' , '" + ISPlasticBag + "' , '" + ISeal + "' , '" + ISMoist_Machine + "' , '" + IsenamelPlate + "' , '" + IsFilter + "' , '" + IsBlower + "' , '" + IsTirpal + "' ,'" + IsDouble_Silai + "' , '" + IsRej_DSilai + "' , '" + IsStich + "' ,'" + TotalTruck_RejectdueStencil + "' ,'" + IsRejectSample + "','" + IsRegisterEntry + "','" + RejectQty + "' ,'" + TotalBardana + "','" + UsedBardana + "' ,'" + RemainBardana_Gathan + "' , '" + RemainBardana_Loos + "' , '" + IsManakBharti + "' , '" + reason_manakBharti + "' , '" + PaymenttoFamrerDate + "' , '" + IsLatePayment + "' , '" + IsVerify_Taulkaata + "' , '" + Isreverse_bagBharti + "' , '" + rejectTruck_Inspdate + "' ,'" + rejectQty_Inspdate + "' , '" + AcceptDate_Recd + "' , '" + ReadyBags_Transport + "' , '" + OtherPoint + "' , getDate() , '" + ip + "','" + usertype + "','"+Double_RejectTruck+"')";

            SqlCommand cmdins = new SqlCommand(insqry,con);

            int x = cmdins.ExecuteNonQuery();

            if (x > 0)
            {
                Label3.Text = "";
                btnsave.Enabled = false;
                clearall();
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('डाटा सुरक्षित किया जा चुका है |'); </script> ");

                Session["InspDate"] = getDate_MDY(txtInsp_date.Text);
                Session["CropType"] = croptype;
                Session["PCName"] = ddlPCName.SelectedValue;
                Session["DistrictId"] = Session["District_id"].ToString();
                Session["UserType"] = usertype;
                hlinkpdo.Visible = true;
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Not Saved, Pls Check All Entries.'); </script> ");
                btnsave.Enabled = true;
            }
        }
        catch(Exception ex)
        {
            Label3.Text = ex.Message;
            btnsave.Enabled = true;
        }

        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
 
    }

    void clearall()
    {
        txtName_one.Text = "";
        txtName_two.Text = "";
        txtName_three.Text = "";
        txtName_four.Text = "";
        txtName_five.Text = "";

        txtMob_one.Text = "";
        txtMob_two.Text = "";
        txtMob_three.Text = "";
        txtMob_four.Text = "";
        txtMob_five.Text = "";

        txtdesig_one.Text = "";
        txtdesig_two.Text = "";
        txtdesig_three.Text = "";
        txtdesig_four.Text = "";
        txtdesig_five.Text = "";
        
        txtPC_manager.Text = "";
        txtPC_Mobile.Text = "";

        txtNodal_officer.Text = "";
        txtNodal_desig.Text = "";
        txtNodal_Mobile.Text = "";

        txtregister_Farmer.Text = "";

        txtInspDate_Kharidi.Text = "";

        txt_totalkharidi.Text = "";
        txt_totalTransportation.Text = "";
        txt_remainTransport.Text = "";
        txt_truckreject_duestencil.Text = "";

        txt_rejectQty.Text = "";
        txt_totalBardana.Text = "";
        txt_usedBardana.Text = "";
        txt_remainBardana_Gathan.Text = "";
        txt_remain_bardana.Text = "";
        
        txt_reason_manakbharti.Text = "";

        txt_totalreject_truck.Text = "";
        txt_totalreject_qty.Text = "";
        
        txt_totalbags_allparameter.Text = "";

        txt_otherpoint.Text = "";
    }

    protected void btnOTP_Click(object sender, EventArgs e)
    {
        hdfOTP.Value = "";

        if (txtMob_one.Text != "")
        {
            txtOTP.Text = "";

            GenerateUniqueOTP();

            btnOTP.Disabled = true;
            txtOTP.Enabled = true;
            ChkOTP.Disabled = false;
            txtOTP.Focus();
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:TimerFunc(); ", true);
        }

        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Mobile Number For OTP'); </script> ");
            return;
        }

        
    }

    protected void GenerateUniqueOTP()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";

        string characters = numbers;

        characters += alphabets + small_alphabets + numbers;

        
         int length = 5;
       

        //int length = int.Parse(ddlMvmtNo.SelectedItem.Value);
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }
        
        GenerateOTP = otp;

        string user = Session["user"].ToString();

        OTPSMS = "'"+user+"' Your OTP Is '" + otp + "'";
        hdfOTP.Value = "";
        hdfOTP.Value = otp;

        SMS Message = new SMS();

        string MobileNo = "";
        
        MobileNo = txtMob_one.Text;
        Message.SendSMS(MobileNo, OTPSMS);
    }
}
