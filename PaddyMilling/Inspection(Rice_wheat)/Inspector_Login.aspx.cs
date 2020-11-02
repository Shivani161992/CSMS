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


public partial class PaddyMilling_Inspection__Rice_wheat_Inspector_Login : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public string Name = "";
    public string password = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //public void GetData()
    //{
    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            con.Open();
    //            string select = string.Format("select Inspector_Name, INS_Password from PM_Inspector_Register_2017 where Inspector_Name='" + txtname.Text + "'");
    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds != null)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    Name = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();
    //                    password = ds.Tables[0].Rows[0]["INS_Password"].ToString();
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //        }

    //        finally
    //        {
    //            if (con.State != ConnectionState.Closed)
    //            {
    //                con.Close();
    //            }
    //        }
    //    }
    //}

    protected void bttlogin_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                
                con.Open();
                string select = string.Format("select Inspector_Name, INS_Password from PM_Inspector_Register_2017 where Inspector_Name='" + txtname.Text + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Name = ds.Tables[0].Rows[0]["Inspector_Name"].ToString();
                        password = ds.Tables[0].Rows[0]["INS_Password"].ToString();
                    }
                }
                string qry = "select Inspector_Name from PM_Inspector_Register_2017 where Inspector_Name='" + txtname.Text + "'";
                da = new SqlDataAdapter(qry, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count < 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('There is no such User named as" + txtname.Text + " '); </script> ");
                        return;
                    }
                    else  if(txtname.Text == Name && txtPass.Text == password)
                    {
                        Response.Redirect("~/PaddyMilling/Inspection(Rice_wheat)/PM_Inspector_Approval.aspx");
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
}
    
