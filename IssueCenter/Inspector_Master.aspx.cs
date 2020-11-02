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
using Data;
using DataAccess;
using System.Data.SqlClient;

public partial class IssueCenter_Inspector_Master : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

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
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["dist_id"] != null)
        {
            sid = Session["dist_id"].ToString();
        }
        if (!IsPostBack)
        {
           // string districtid = Session["dist_id"].ToString();
            Fillgrid();
            GetDist();
        }

    }


    public void GetDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Ddldist.DataSource = ds.Tables[0];
                        Ddldist.DataTextField = "district_name";
                        Ddldist.DataValueField = "district_code";
                        Ddldist.DataBind();
                        Ddldist.Items.Insert(0, "--Select--");
                        Ddldist.SelectedValue = Session["dist_id"].ToString();
                        GetMPIssueCentre();
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


    public void GetMPIssueCentre()
    {

       


        string districtid = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = string.Format("select DepotName,DepotID from tbl_MetaData_DEPOT where DistrictId= '23" + Ddldist.SelectedValue.ToString() + "' order by DepotName");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlIC.DataSource = ds.Tables[0];
                    ddlIC.DataTextField = "DepotName";
                    ddlIC.DataValueField = "DepotID";
                    ddlIC.DataBind();
                    ddlIC.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले में कोई भी प्रदाय केंद्र उपलब्ध नहीं है|'); </script> ");
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






    void Fillgrid()
    {
      

        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "  select Inspector_Name, Inspector_desig, IssueCenter_code from Inspector_Master_02017  ";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
         using (con = new SqlConnection(strcon))
             try
             {
                 con.Open();
                 string qrey = "select isnull(max(Inspector_ID),0) as Transporter_ID  from Inspector_Master_02017 where  Distt_ID='" + sid + "' and LEN(Inspector_ID)<8 ";
                 SqlCommand cmdmax = new SqlCommand(qrey, con);
                 SqlDataAdapter dmax = new SqlDataAdapter(cmdmax);
                 //mobj1 = new MoveChallan(ComObj);
                 //string qrey = "select isnull(max(Inspector_ID),0) as Transporter_ID  from Inspector_Master_02017 where  Distt_ID='" + sid + "' and LEN(Inspector_ID)<8 ";
                 DataSet ds = new DataSet();
                 DataRow dr = ds.Tables[0].Rows[0];
                 dmax.Fill(ds);
                 gatepass = dr["Inspector_ID"].ToString();

                 if (gatepass == " ")
                 {
                     gatepass = "17" + sid + "01";
                 }
                 else
                 {
                     getnum = Convert.ToInt32(gatepass);
                     //getnum = gatepass;
                     getnum = getnum + 1;
                     gatepass = getnum.ToString();
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

        using (con = new SqlConnection(strcon))
        try
        {

            con.Open();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string strselect = "insert into Inspector_Master_02017( district, Inspector_ID, Inspector_Name, Inspector_Desig, IssueCenter_code, Created_Date, IP) values ('" + Ddldist.SelectedValue.ToString() + "','" + gatepass + "','" + txt_name.Text + "','" + txt_desig.Text + "','" + ddlIC.SelectedValue.ToString() + "',getdate(),'" + ip + "')";
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
        Fillgrid();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

   
    protected void Ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIC.Items.Clear();
        


        if (Ddldist.SelectedIndex > 0)
        {
            GetMPIssueCentre();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }
    protected void ddlIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIC.SelectedIndex > 0)
        {
            //GetBranch();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया प्रदाय केंद्र चुनें|'); </script> ");
        }
    }
}