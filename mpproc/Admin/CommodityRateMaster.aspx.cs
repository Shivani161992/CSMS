using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;
public partial class mpproc_Admin_CommodityRateMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    SqlString SqlObj = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] != null)
        {
            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());
            txt_MSPRate.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txt_commission.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txt_Bonus.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txt_Incidental.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            txt_GFCap.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
            btn_Save.Attributes.Add("onclick", "return CheckIsCapacityt();");


            if (!IsPostBack)
            {

                GetMarkSeas();
                GetCrpyear();
                GetCommodity(DDL_MarSeas.SelectedValue.ToString());


            }

        }
    }

    private void GetCommodity(string comdity)
    {
        SqlObj = new SqlString(ComObj);
        ds = new DataSet();
        try
        {

            if (DDL_MarSeas.SelectedItem.Text != "--Select--")
            {
              
                string strSql = "select * from CommodityMaster where MarkSeasId='" + comdity + "'";
                ds = SqlObj.selectAny(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DDL_Commodity.DataSource = ds.Tables[0];
                    DDL_Commodity.DataTextField = "CommodityName";
                    DDL_Commodity.DataValueField = "CommodityId";
                    DDL_Commodity.DataBind();
                    DDL_Commodity.Items.Insert(0, "--Select--");

                }

            }

            else
            {
                DDL_Commodity.Items.Clear();

            }
        }
        catch (Exception ex)
        {
            DivMsg.InnerText = ex.Message;

        }

        finally
        {
            SqlObj = null;
            ds = null;

        }


    }

    private void GetCrpyear()
    {
        try
        {
            SqlObj = new SqlString(ComObj);
            string str = "SELECT CropId,CropYear FROM CropYearMaster  where CropId in('8') ";
            DataSet ds = SqlObj.selectAny(str);
            if (ds == null)
            {
            }
            else
            {

                DDL_CropYear.DataSource = ds.Tables[0];
                DDL_CropYear.DataTextField = "CropYear";
                DDL_CropYear.DataValueField = "CropId";
                DDL_CropYear.DataBind();
                //DDL_CropYear.Items.Insert(0, "--Select--");
                DDL_CropYear.SelectedValue = "8";
            }
        }
        catch (Exception ex)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");

        }

        
       

    }

    private void GetMarkSeas()
    {
        SqlObj = new SqlString(ComObj);
        ds = new DataSet();
        try
        {
            string strSql = "SELECT MarkSeasId,MarkSeaon FROM MarketingSeasonMaster where MarkSeasId in('r') ";
            //string strSql = "select * from MarketingSeasonMaster";
            ds = SqlObj.selectAny(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {

                DDL_MarSeas.DataSource = ds.Tables[0];
                DDL_MarSeas.DataTextField = "MarkSeaon";
                DDL_MarSeas.DataValueField = "MarkSeasId";
                DDL_MarSeas.DataBind();
                DDL_CropYear.SelectedValue="r";
            }


        }
        catch (Exception ex)
        {
            DivMsg.InnerText = ex.Message;

        }

        finally
        {
            SqlObj = null;
            ds = null;

        }

    }

    protected void DDL_MarSeas_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetCommodity(DDL_MarSeas.SelectedValue.ToString());
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        SqlObj = new SqlString(ComObj);
        ds = new DataSet();

        try
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (DDL_MarSeas.SelectedItem.Text != "--Select--" && DDL_CropYear.SelectedItem.Text != "--Select--" && DDL_Commodity.SelectedItem.Text != "--Select--" && txt_MSPRate.Text != "" && txt_Bonus.Text != "" && txt_GFCap.Text != "")
            {

                string strchk = "select count(*) as count from CommodityRate where MarketingSeasonId='" + DDL_MarSeas.SelectedValue + "' and CommodityId='" + DDL_Commodity.SelectedValue + "' and CropYear='" + DDL_CropYear.SelectedItem.Text + "'";
                ds = SqlObj.selectAny(strchk);
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["count"]) == 0)
                {

                    string strqsl = "INSERT INTO CommodityRate(MarketingSeasonId,CommodityId,CropYear,Commission,Bonus,Incidental,GunnyFillingCapacity,Rate ,CreatedDate,ip) VALUES('" + DDL_MarSeas.SelectedValue + "','" + DDL_Commodity.SelectedValue + "','" + DDL_CropYear.SelectedItem.Text + "','" + txt_commission.Text + "','" + txt_Bonus.Text + "','" + txt_Incidental.Text + "','" + txt_GFCap.Text + "','" + txt_MSPRate.Text + "',getDate(),'" + ip + "')";
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd = new SqlCommand(strqsl, con);
                    cmd.ExecuteNonQuery();
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saves successfully.............'); </script> ");
                    btn_Save.Enabled = false;
                    if (con.State == ConnectionState.Open)
                    {

                        con.Close();

                    }
                }

                else
                {

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('MSP Rate Allready Exist.. Select For Update..'); </script> ");

                }
            }
            else
            {

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Fill All Required  Entry...'); </script> ");


            }
        }
        catch (Exception ex)
        {
            DivMsg.InnerText = ex.Message;

        }

        finally {

            SqlObj = null;
            ds = null;

        }
    }
    protected void DDL_Commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();

    }

    private void fillGrid()
    { 
        ds = new DataSet();
       
        try
        {
            if (DDL_MarSeas.SelectedItem.Text != "--Select--" && DDL_CropYear.SelectedItem.Text != "--Select--" && DDL_Commodity.SelectedItem.Text != "--Select--")
            {
                //SqlObj = new SqlString(ComObj);
                string str = "SELECT MarketingSeasonMaster.MarkSeaon as MS,CommodityRate.MarketingSeasonId as MarkID,CommodityRate.CommodityId as ComID,CommodityRate.CommodityRate_ID as CommodityRate_ID,CommodityRate.Rate as Rate, CommodityMaster.CommodityName as CN,CommodityRate.Rate as Rate,CommodityRate.CropYear as CY,CommodityRate.Commission as Commission,CommodityRate.Bonus as bonus ,CommodityRate.Incidental as Incidental,CommodityRate.GunnyFillingCapacity as GP from  CommodityRate left join MarketingSeasonMaster on CommodityRate .MarketingSeasonId=MarketingSeasonMaster.MarkSeasId left join CommodityMaster on CommodityRate.CommodityId=CommodityMaster.CommodityId left join CropYearMaster on CommodityRate.CropYear=CropYearMaster.CropYear where CommodityRate.MarketingSeasonId='" + DDL_MarSeas.SelectedValue + "'and CommodityRate.CommodityId='" + DDL_Commodity.SelectedValue + "'and CommodityRate.CropYear='" + DDL_CropYear.SelectedItem.Text + "'";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //DataSet ds = SqlObj.selectAny(str); 
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(ds, "tab1");
                if (ds == null)
                {

                }
                else
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        GridView_MSPRate.DataSource = ds.Tables[0];
                        GridView_MSPRate.DataBind();

                    }
                }
            }
        }
        catch (Exception ex)
        {
            DivMsg.InnerText = ex.Message;

        }
        finally {

            ds = null;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        
        }
    }
    protected void GridView_MSPRate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            btn_Save.Visible = false;
            DDL_MarSeas.Enabled = false;
            DDL_CropYear.Enabled = false;
            DDL_Commodity.Enabled = false;

            DDL_MarSeas.SelectedValue = GridView_MSPRate.SelectedRow.Cells[9].Text;
            GetCommodity(GridView_MSPRate.SelectedRow.Cells[8].Text.ToString());
            DDL_CropYear.SelectedItem.Text = GridView_MSPRate.SelectedRow.Cells[3].Text;
            DDL_Commodity.SelectedValue = GridView_MSPRate.SelectedRow.Cells[10].Text;
            txt_MSPRate.Text = GridView_MSPRate.SelectedRow.Cells[4].Text;
            txt_commission.Text = GridView_MSPRate.SelectedRow.Cells[5].Text;
            txt_Incidental.Text = GridView_MSPRate.SelectedRow.Cells[6].Text;
            txt_Bonus.Text = GridView_MSPRate.SelectedRow.Cells[7].Text;
            txt_GFCap.Text = GridView_MSPRate.SelectedRow.Cells[8].Text;
            
            btn_update.Visible = true;
        }
        catch (Exception ex)
        {
            DivMsg.InnerText = ex.Message;
        
        }

    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            string comrateid = GridView_MSPRate.SelectedRow.Cells[11].Text;
            string strUp = "UPDATE CommodityRate SET Commission ='" + txt_commission.Text + "',Bonus = '" + txt_Bonus.Text + "', Incidental ='" + txt_Incidental.Text + "',GunnyFillingCapacity = '" + txt_GFCap.Text + "',Rate = '" + txt_MSPRate.Text + "',UpdatedDate = getDate() where MarketingSeasonId='" + comrateid + "'";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd = new SqlCommand(strUp, con);
            cmd.ExecuteNonQuery();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Updated successfully.............'); </script> ");
            btn_update.Visible = false;
            fillGrid();

        }
        catch (Exception ex)
        {

            DivMsg.InnerText = ex.Message;

        }
        finally
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

        }


    }

}
