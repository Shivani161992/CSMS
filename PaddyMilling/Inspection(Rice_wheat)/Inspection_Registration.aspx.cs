using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

public partial class PaddyMilling_Inspection__Rice_wheat_Inspection_Registration : System.Web.UI.Page
{
    public string Inspector_ID = "";
    public string gatepass = "";
    public int getnum;
    SqlDataReader dr;
    protected Common ComObj = null;
    //MoveChallan mobj1 = null;

    public string sid = "";

    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ddlCropYear.Items.Insert(0, "Crop Year");
            //ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
            //ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);

            //ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
            GetDistName();
            txtDate_of_Joining.Text = DateTime.Now.ToString("dd-MM-yyyy");

            string Month = DateTime.Now.ToString("MM");

//if (Month == "01" || Month == "02" || Month == "04" || Month == "06" || Month == "09" || Month == "11")
           // {
               // txtFromDate.Text = DateTime.Now.AddDays(-31).ToString("dd-MM-yyyy");
            //}
           // else
           // {
               // txtFromDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
           // }
        }
        

        //GetDivisionName();
       
    }

    public void GetDistName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT district_name,district_code FROM pds.districtsmp Order By district_name");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlDist.DataSource = ds.Tables[0];
                        ddlDist.DataTextField = "district_name";
                        ddlDist.DataValueField = "district_code";
                        ddlDist.DataBind();
                        // ddlDist.Items.Insert(0, "All");
                        ddlDist.Items.Insert(0, "Posting Place");
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

    protected void bttRegister_Click(object sender, EventArgs e)
    {
        if (ddlDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Posting Place '); </script> ");
            return;
        }
        else if (txtname.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter your name'); </script> ");
            return;
        }
        else if (txtDesig.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Designation'); </script> ");
            return;
        }
        else if (txtDate_of_Joining.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' Please Enter Date of joining'); </script> ");
            return;
        }
        else if (txtPasswd.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert(' Please Enter Password'); </script> ");
            return;
        }
        else if (txtEmailID.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Email ID'); </script> ");
            return;
        }
        else if (txtConfirmPass.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter confirm Password'); </script> ");
            return;
        }
        else if (txtConfirmPass.Text != txtPasswd.Text)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Correct confirm Password'); </script> ");
            return;
        }
        
        else
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();
                    string qrey = "select max(INS_RegisterID) as INS_RegisterID  from PM_Inspector_Register_2017 where  LEN(INS_RegisterID)<15 ";
                da = new SqlDataAdapter(qrey, con);

                ds = new DataSet();
                da.Fill(ds);
                //mobj1 = new MoveChallan(ComObj);
                //string qrey = "select isnull(max(Inspector_ID),0) as Transporter_ID  from Inspector_Master_02017 where  Distt_ID='" + sid + "' and LEN(Inspector_ID)<8 ";
                //DataSet ds = new DataSet();
                // dmax.Fill(ds);
                // DataTable dt = ds.Tables[""];
                DataRow dr = ds.Tables[0].Rows[0];
                //gatepass = dr["Inspector_ID"].ToString();
                gatepass = ds.Tables[0].Rows[0]["INS_RegisterID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "170"  + "01";
                }
                else
                {
                    getnum = Convert.ToInt32(gatepass);
                    //getnum = gatepass;
                    getnum = getnum + 1;
                    gatepass = getnum.ToString();
                }
                 Inspector_ID = "INSR" + gatepass;
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
                using (con = new SqlConnection(strcon))
                    try
                    {

                        string todate = Request.Form[txtDate_of_Joining.UniqueID];
                        txtDate_of_Joining.Text = todate;

                        ConvertServerDate ServerDate = new ConvertServerDate();
                       
                        string ConvertToDate = ServerDate.getDate_MDY(todate.ToString()) + " 23:59:59";
                        string IsApproved = "N"; 
                        
                        con.Open();
                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string strselect = "insert into PM_Inspector_Register_2017( INS_RegisterID, Inspector_Name, Ins_Designaion, INS_PostingPlace, INS_Date_of_joining, INS_Email_ID, INS_Password, INS_Confirm_Password, IP, Created_date, IsApproved) values ('" + Inspector_ID + "','" + txtname.Text + "','" + txtDesig.Text + "','" + ddlDist.SelectedValue.ToString() + "','" + ConvertToDate + "','" + txtEmailID.Text + "','" + txtPasswd.Text + "','" + txtConfirmPass.Text + "','" + ip + "' ,getdate(),'" + IsApproved + "')";
                        cmd = new SqlCommand(strselect, con);
                        string check = (string)cmd.ExecuteScalar();

                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is Saved successfully'); </script> ");

                    }
                    catch
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please check and Enter valid data...'); </script> ");
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
    }
}