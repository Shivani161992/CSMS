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

public partial class District_SiloMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    public string distid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();

               
            if (!IsPostBack)
            {
                txtmobile.Attributes.Add("onkeypress", "return CheckIsnondecimal(this)");

                txtcapacity.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

                txtlicensevalid.Attributes.Add("onkeypress", "return CheckCalDate(this)");

                txtrate.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");

                get_society();

                getpredata();

            }


        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
        
    }

    protected void get_society()
    {
        try
        {
            string dist = distid;


            string qryblock = "SELECT Society_Id ,Society_Name + ','+ SocPlace as Society_Name FROM Society where DistrictId = '23" + dist + "' and Society_Id not in (select proc_code from SiloMaster where DistrictId = '" + dist + "') order by Society_Id";
            
            SqlDataAdapter da = new SqlDataAdapter(qryblock, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                   
                    GridView1.DataBind();
                   
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

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        btnsave.Enabled = false;

        if (txtfirmname.Text  == "")
        {
            btnsave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Firm Name..');</script>");
            return;
        }

        if (txtlicenseno.Text == "")
        {
            btnsave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter License Number..');</script>");
            return;
        }

        if (txtlicensevalid.Text == "")
        {
            btnsave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select License Date from Calender..');</script>");
            return;
        }


        if (txtlocation.Text == "")
        {
            btnsave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Location....');</script>");
            return;
        }

        if (txtowner.Text == "")
        {
            btnsave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Authorised Person Name..');</script>");
            return;
        }

        if (txtmobile.Text == "")
        {
            btnsave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Mobile Num.');</script>");
            return;
        }

        if (txtcapacity.Text == "")
        {
            btnsave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Capacity.');</script>");
            return;
        }

        if (txtrate.Text == "")
        {
            btnsave.Enabled = true;
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Enter Rate per Qunital');</script>");
            return;
        }

        string dist = distid;

        string firmName = txtfirmname.Text.Trim();

        string firmaddress = txtlocation.Text.Trim();

        string owner = txtowner.Text.Trim();

        string licvalid = getDate_MDY(txtlicensevalid.Text.Trim());

        string licnum = txtlicenseno.Text.Trim();

        string silotype = ddltype.SelectedValue;

        string Contact = txtmobile.Text.Trim();

        string capacity = txtcapacity.Text.Trim();

        string rate = txtrate.Text.Trim();

        string IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string user = Session["OperatorIDDM"].ToString();

        string qrey = "select max(Silo_Code) as Silo_Code from dbo.SiloMaster where DistrictId='" + distid + "'";

        SqlCommand cmd_firmcode = new SqlCommand(qrey, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }


        SqlDataReader sqldr = cmd_firmcode.ExecuteReader();
        sqldr.Read();

        string code = sqldr["Silo_Code"].ToString();

        if (code == "")
        {
            code = 23 + distid + "201";

        }
        else
        {
            Int32 socnum = Convert.ToInt32(code);
            socnum = socnum + 1;
            code = socnum.ToString();

        }

        sqldr.Close();

        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox GchkBx = (CheckBox)gr.FindControl("cbSelectAll");

            if (GchkBx.Checked)
            {
                string SocCode = gr.Cells[1].Text;

                string insqry = "Insert into SiloMaster (DistrictId  ,Silo_Code,Firm_Name ,Owner_Name,FirmAddress,LicenseNum ,License_Valid ,ContactNumber ,Proc_Code ,Capacity,RateperQntl ,TypeofSilo,CreatedDate,OperatorID ,IP_Address) values ('" + dist + "','" + code + "', N'" + firmName + "', N'" + owner + "',N'" + firmaddress + "','" + licnum + "','" + licvalid + "','" + Contact + "','" + SocCode + "','" + capacity + "','" + rate + "','" + silotype + "',getdate(),'" + user + "','" + IP + "')";
                SqlCommand cmdins = new SqlCommand(insqry, con);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                try
                {
                    int x = cmdins.ExecuteNonQuery();

                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Sucessfuly...'); </script> ");

                    getpredata();

                    get_society();
                }

                catch
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    btnsave.Enabled = true;
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

        }

    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void getpredata()
    {
        string qry = "select firm_name , licenseNum , ContactNumber , Society.Society_Name , Capacity from SiloMaster inner join Society on Society.Society_Id = SiloMaster.proc_code where SiloMaster.DistrictId = '"+distid+"'";

        SqlCommand cmd = new SqlCommand(qry, con);

        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView2.DataSource = ds.Tables[0];

            GridView2.DataBind();

        }


        else
        {

        }
    }
    
}
