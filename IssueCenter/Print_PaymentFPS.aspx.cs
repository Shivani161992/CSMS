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

public partial class IssueCenter_Print_PaymentFPS : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con_opdms = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_opdms"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    void Fillgrid()
    {
        try
        {
            string query1 = "SELECT pds.districtsmp.district_name , tbl_MetaData_DEPOT.DepotName ,tbl_rootchart_master.fps_name  + ',' + tbl_rootchart_master.fps_code as fps_name ,  Bank_Master_New.Bank_Name ,allotMonth ,Allotyear  ,WhatAllot ,RiceAllot  ,SugarAllot ,SaltAllot ,WRate ,RRate ,SugarRate ,SaltRate ,TotalAmount ,DDNum ,convert(nvarchar,DD_Date,103)DD_Date ,DDAmount   FROM  DPY_FPS_Payment inner join pds.districtsmp on pds.districtsmp.district_code = DPY_FPS_Payment.DistrictId  inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = DPY_FPS_Payment.IssueCenter inner join tbl_rootchart_master on tbl_rootchart_master.fps_code = DPY_FPS_Payment.FpsCode inner join Bank_Master_New on Bank_Master_New.Bank_ID = DPY_FPS_Payment.BankName where DDNum = '123456'";
            SqlCommand cmd = new SqlCommand(query1, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }

            else
            {
                GridView1.DataSource = "";
                GridView1.DataBind();
            }

        }
        catch (Exception)
        {

        }
    }
}
