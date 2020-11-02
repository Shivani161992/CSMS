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

public partial class State_PM_Depart_Master : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            // string districtid = Session["dist_id"].ToString();
            Fillgrid();
            Label3.Text = "Department Master";
            //GetDist();
            //GetMPIssueCentre();
        }
    }
    void Fillgrid()
    {


        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "  select Department_Name from PM_Depart_Master  ";
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
                string qrey = "select max(Depart_ID) as Depart_ID  from PM_Depart_Master where  LEN(Depart_ID)<8 ";
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
                gatepass = ds.Tables[0].Rows[0]["Depart_ID"].ToString();

                if (gatepass == "")
                {
                    gatepass = "17"  + "01";
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
                string strselect = "insert into PM_Depart_Master( Depart_ID, Department_Name, IP, Created_Date) values ('" + gatepass + "','" + txt_Departname.Text + "','" + ip + "' ,getdate())";
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
        
        txt_Departname.Text = "";
       
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}