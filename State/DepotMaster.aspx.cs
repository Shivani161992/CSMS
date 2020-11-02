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
using System.Text;

public partial class State_DepotProfile : System.Web.UI.Page
{


  
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    
  
   // public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["connect_warehouse"].ToString());
    //public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    SqlCommand cmd = new SqlCommand();
    string stid = "";
    public string DepoBelongs = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDistricts();
            if (Session["st_id"] != null)
            {
                stid = Session["st_id"].ToString();
              
              
            }
        }
      
     
    }

    private void GetFromdepot()
    {

        try
        {


            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("MPSCSC_Get_IssueCenter", con2);
            cmd.Parameters.Add("@DepotID", SqlDbType.NVarChar, 20);
            cmd.Parameters["@DepotID"].Value = ddlDepoType.SelectedValue;
               
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            ds.Clear();
            da.Fill(ds);
            cmd.Dispose();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DepoBelongs = ds.Tables[0].Rows[0]["DepoBelongs"].ToString();
                txtLocationAddress.Text = ds.Tables[0].Rows[0]["DepotAddress"].ToString();
                txtLocationFaxNo.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString();
                txtLocationPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
               
            }
            else
            {

                DepoBelongs = "";
                txtLocationAddress.Text = "";
                txtLocationFaxNo.Text = "";
                txtLocationPhoneNo.Text = "";
                txtLocationEMailAddress.Text = "";
            }

        }
        catch (Exception ex)
        {
            StringBuilder str1 = new StringBuilder();
            str1.Append("<script>");
            str1.Append("alert('" + "Some error has occured" + "," + "Error:-" + ex.Message + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str1.ToString());
        }
        finally
        {
            if (con2.State == ConnectionState.Open)
            {
                con2.Close();
            }


        }
        
    }



    private void GetMPWLCDetails()
    {

        try
        {


            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("MPSCSC_IssueCenter", con2);
            cmd.Parameters.Add("@DistrictId", SqlDbType.NVarChar, 20);
            cmd.Parameters["@DistrictId"].Value = ddlDistrictName.SelectedValue;
           
            cmd.CommandType = CommandType.StoredProcedure;


            da.SelectCommand = cmd;
            ds.Clear();
            da.Fill(ds);
            cmd.Dispose();


            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepoType.DataSource = null;
                    ddlDepoType.DataSource = ds.Tables[0];
                    ddlDepoType.DataTextField = "DepotName";
                    ddlDepoType.DataValueField = "DepotID";
                    ddlDepoType.DataBind();
                    ddlDepoType.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddlDepoType.Items.Clear();
                    ddlDepoType.DataSource = null;
                    ddlDepoType.DataBind();

                }
            }

            else
            {
                ddlDepoType.Items.Clear();
                ddlDepoType.DataSource = null;
                ddlDepoType.DataBind();

            }

        }
        catch (Exception ex)
        {
            StringBuilder str1 = new StringBuilder();
            str1.Append("<script>");
            str1.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str1.ToString());
        }
        finally
        {
            if (con2.State == ConnectionState.Open)
            {
                con2.Close();
            }


        }

    }



    private void GetDistricts()
    {

        try
        {


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("select * from dbo.tbl_MetaData_DISTRICT order by District_Name", con);
            cmd.CommandType = CommandType.Text;
            da.SelectCommand = cmd;
            ds.Clear();
            da.Fill(ds);
            cmd.Dispose();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrictName.DataSource = null;
                ddlDistrictName.DataSource = ds.Tables[0];
                ddlDistrictName.DataTextField = "District_Name";
                ddlDistrictName.DataValueField = "District_Id";
                ddlDistrictName.DataBind();
                ddlDistrictName.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlDistrictName.DataSource = null;
                ddlDistrictName.DataBind();

            }

        }
        catch (Exception ex)
        {
            StringBuilder str1 = new StringBuilder();
            str1.Append("<script>");
            str1.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str1.ToString());
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }


        }
        

     
    }

  


    
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlDistrictName.Text != "--Select--")
            {

                string query = "";
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string isstid = ddlDepoType.SelectedValue;
                string issName = ddlDepoType.SelectedItem.Text.ToString();
                string stateid = "23";
                string distid = ddlDistrictName.SelectedValue;

                string address = txtLocationAddress.Text.Trim();
                string phoneno = txtLocationPhoneNo.Text.Trim();
                string FaxNo = txtLocationFaxNo.Text.Trim();
                string email = txtLocationEMailAddress.Text.Trim();


                if (phoneno != "" || phoneno != string.Empty)
                {

                    try
                    {

                        Int64 ph = Convert.ToInt64(phoneno);


                    }

                    catch (Exception exx)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid phoneno '); </script> ");

                        return;

                    }

                }


                if (FaxNo != "" || FaxNo != string.Empty)
                {

                    try
                    {
                        Int64 ph = Convert.ToInt64(FaxNo);


                    }

                    catch (Exception exx)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid FaxNo '); </script> ");

                        return;

                    }

                }
                SqlTransaction trans = null;

                string queryiss = "Insert into dbo.tbl_MetaData_DEPOT(DepotID,StateId,DistrictId,DepotName,DepotAddress,PhoneNo,FaxNo,DepoBelongs,CreatedBy,CreatedDate) values('" + isstid + "','" + stateid + "','" + distid + "','" + issName + "','" + address + "','" + phoneno + "','" + FaxNo + "','" + DepoBelongs + "','" + ip + "',GetDate())";

                string queryloginIns = "Insert into [DCLogin_MP]([District_ID],[DC_ID],[DC_name],[Password]) values('" + distid + "','" + isstid + "','" + issName + "','nic')";
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    cmd.Connection = con;
                    if (con != null)
                    {
                        cmd.CommandText = queryiss;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = queryloginIns;
                        cmd.ExecuteNonQuery();

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully '); </script> ");

                    }

                    trans.Commit();
                    trans.Dispose();
                    btnupdate.Enabled = false;



                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    lblMsg.Text = ex.Message.ToString();
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {

                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Select Center Name" + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());

            }
        }
        catch (Exception exxx)
        {

            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + exxx.Message.ToString()+ "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        
        }
     
    }
    Int32 CheckInt(string Val)
    {

        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        Int32 ValF = int.Parse(ValS);
        return ValF;

    }
    float CheckFloat(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }



    protected void ddlDepoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFromdepot();
    }
    protected void ddlDistrictName_SelectedIndexChanged(object sender, EventArgs e)
    {
      

            GetMPWLCDetails();

       

    }
}
