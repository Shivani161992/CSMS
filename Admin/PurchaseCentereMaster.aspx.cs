using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;

public partial class Admin_PurchaseCentereMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    //public SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());
    SqlCommand cmd = new SqlCommand(); 
    
    Districts DObj = null;
    chksql chk = null;
    Division DivObj = null;
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
        if (!IsPostBack)
        {

          
            GetDistOff();
           

        }
    }
    void GetDistOff()
    {
        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAll(" order by district_name");
        if (ds == null)
        {
        }
        else
        {
            ddldistoff.DataSource = ds.Tables[0];
            ddldistoff.DataTextField = "district_name";
            ddldistoff.DataValueField = "District_Code";

            ddldistoff.DataBind();
            ddldistoff.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlcomdty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddldistoff_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "SELECT Max(PcId) as PcId  FROM tbl_MetaData_Purchase_Center";

        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {
        }
        else

        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtpcid.Text = dr["PcId"].ToString();
            Int64 pcid = Int64.Parse(dr["PcId"].ToString());
            pcid = pcid + 1;
            txtpcid.Text = pcid.ToString();



        }




    }
    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlseason_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        string qryc = "SELECT * FROM tbl_MetaData_Purchase_Center where DistrictId='"+ddldistoff.SelectedValue.ToString ()+"' and PurchaseCenterName='"+ txtCentreName.Text+"'" ;

        DObj = new Districts(ComObj);
        DataSet ds = DObj.selectAny(qryc);
        if (ds == null)
        {

        }
        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                string qry = "INSERT INTO tbl_MetaData_Purchase_Center(PcId,PCCode,CommodityId,DistrictId,RegionId,StateId,CropYearId,MarketingSeason,PurchaseCenterName,DateOfIssue,Address,Phone,PC_CategoryID,PCType,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,DeletedBy ,DeletedDate,NodalOff,Block,Remark,Status,Master_PCID)VALUES('" + txtpcid.Text + "','','" + ddlcomdty.SelectedValue.ToString() + "','23" + ddldistoff.SelectedValue + "','2300','23','" + ddlseason.SelectedValue.ToString() + "','" + ddlcropyear.SelectedItem.Text.ToString() + "','" + txtCentreName.Text + "','','" + txtAddress.Text + "','" + txtPhone.Text + "','1','','',getdate(),'','','','','" + txtNodalOff.Text + "','" + txtBlock.Text + "','" + txtRemarks.Text + "','4','" + txtpcid.Text + "')";
                cmd.Connection = con;
                cmd.CommandText = qry;

                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //cmd.Connection = con1;
                    //cmd.CommandText = qry;
                    //con1.Open();
                    //cmd.ExecuteNonQuery();
                    //con1.Close();
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Inserted Successfully.....'); </script> ");
                    btnsubmit.Enabled = false;


                }
                catch (Exception ex)
                {

                    Label1.Visible = true;
                    Label1.Text = ex.Message;
                }
                finally
                {
                    con.Close();

                }

            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Purchase Centre Already Exist.....'); </script> ");

            }


          



        }










      
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {

    }
   
}
