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
public partial class IssueCenter_DeleteDispatch : System.Web.UI.Page
{
    public string depotid = "";
    public string districtid = "";

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            depotid = Session["issue_id"].ToString();
            districtid = Session["dist_id"].ToString();
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        } 
    }

    public void rack_Number()
    {
        string challan = "SELECT distinct Rack_NO FROM SCSC_RailHead_TC where Dist_ID = '" + districtid + "' and Depot_Id = '" + depotid + "' and Challan_No Not Like 'MOR%' ";
        SqlCommand cmd = new SqlCommand(challan, con);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            btnDelete.Enabled = false;
            ddlracknumber.DataSource = "";
            ddlracknumber.DataBind();
        }

        else
        {
            ddlracknumber.DataSource = ds.Tables[0];
            ddlracknumber.DataTextField = "Rack_NO";
            ddlracknumber.DataValueField = "Rack_NO";
            ddlracknumber.DataBind();
            ddlracknumber.Items.Insert(0, "--Select--");

            btnDelete.Enabled = true;
        }

    }

    public void road_challan()
    {
        string challan = " select distinct challan_No  FROM SCSC_Truck_challan where Depot_Id = '" + depotid + "' and Dist_ID = '" + districtid + "' and  Challan_No not like '%No%' and Challan_No Not Like 'MOR%' ";
        SqlCommand cmd = new SqlCommand(challan, con);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            btnDelete.Enabled = false;

            ddlroadchallan.DataSource = "";
            ddlroadchallan.DataBind();
        }

        else
        {
            ddlroadchallan.DataSource = ds.Tables[0];
            ddlroadchallan.DataTextField = "challan_No";
            ddlroadchallan.DataValueField = "challan_No";
            ddlroadchallan.DataBind();
            ddlroadchallan.Items.Insert(0, "--Select--");

            btnDelete.Enabled = true;
        }
    }

    public void Rack_Challan()
    {
        string challan = "SELECT Challan_No FROM SCSC_RailHead_TC where Dist_ID = '" + districtid + "' and Depot_Id = '" + depotid + "' and Rack_NO = '" + ddlracknumber.SelectedItem.Text + "' ";
        SqlCommand cmd = new SqlCommand(challan, con);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            btnDelete.Enabled = false;

            ddlrackChallan.DataSource = "";
            ddlrackChallan.DataBind();
            ddlrackChallan.Items.Insert(0, "--Select--");

        }

        else
        {
            ddlrackChallan.DataSource = ds.Tables[0];
            ddlrackChallan.DataTextField = "Challan_No";
            ddlrackChallan.DataValueField = "Challan_No";
            ddlrackChallan.DataBind();
            ddlrackChallan.Items.Insert(0, "--Select--");

            btnDelete.Enabled = true;
        }
    }

    protected void RdbyRoad_CheckedChanged(object sender, EventArgs e)
    {
        LblCommodity.Text = "";
        LblScheme.Text = "";
        LblQty.Text = "";
        LblBags.Text = "";
        lbldate.Text = "";
        Label4.Text = "";

        lbldepot.Text = "From Depot";

        lblroadchallan.Visible = true;
        ddlroadchallan.Visible = true;

        lblracknum.Visible = false;
        ddlracknumber.Visible = false;

        lblrackchallan.Visible = false;
        ddlrackChallan.Visible = false;
        road_challan();
    }

    protected void RdRack_CheckedChanged(object sender, EventArgs e)
    {
        LblCommodity.Text = "";
        LblScheme.Text = "";
        LblQty.Text = "";
        LblBags.Text = "";
        lbldate.Text = "";
        Label4.Text = "";

        lbldepot.Text = "Send to District";

        lblroadchallan.Visible = false;
        ddlroadchallan.Visible = false;

        lblracknum.Visible = true;
        ddlracknumber.Visible = true;

        lblrackchallan.Visible = true;
        ddlrackChallan.Visible = true;

        rack_Number();        
    }

    protected void ddlroadchallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlroadchallan.SelectedItem.Text != "--Select--")
        {
            string query = "select  pds.districtsmp.district_name ,tbl_MetaData_DEPOT.DepotName as Dist_Depot,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name , tbl_MetaData_SCHEME.Scheme_Name ,Bags ,Qty_send ,Truck_no , CONVERT(nvarchar,Challan_Date,103)Challan_Date,Source ,Dispatch_Godown,Commodity,Scheme,DispatchType.DispatchName ,CONVERT(nvarchar, Created_date,101)Created_date from SCSC_Truck_challan inner join pds.districtsmp on pds.districtsmp.district_code = SCSC_Truck_challan.Sendto_District inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.DepotID = SCSC_Truck_challan.Depot_Id inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = SCSC_Truck_challan.Commodity inner join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.Scheme_Id = SCSC_Truck_challan.Scheme inner join DispatchType on DispatchType.DispatchId = SCSC_Truck_challan.DispatchID where Dist_ID = '" + districtid + "' and Depot_Id = '" + depotid + "' and Challan_No = '" + ddlroadchallan.SelectedItem.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Not Available for Selected Challan No'); </script> ");
                return;
            }
            else
            {
                string sendtodist = ds.Tables[0].Rows[0]["district_name"].ToString();
                string depot = ds.Tables[0].Rows[0]["Dist_Depot"].ToString();
                string Commodity = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                string Scheme = ds.Tables[0].Rows[0]["Scheme_Name"].ToString();
                string Bags = ds.Tables[0].Rows[0]["Bags"].ToString();
                string Qty = ds.Tables[0].Rows[0]["Qty_send"].ToString();
                string challandate = ds.Tables[0].Rows[0]["Challan_Date"].ToString();
                string Truck_no = ds.Tables[0].Rows[0]["Truck_no"].ToString();
                string DispatchName = ds.Tables[0].Rows[0]["DispatchName"].ToString();

                Label3.Text = sendtodist;
                lblDepotName.Text = depot;
                LblCommodity.Text = Commodity;
                LblScheme.Text = Scheme;
                LblQty.Text = Qty;
                LblBags.Text = Bags;
                lbldate.Text = challandate;
                Label4.Text = Truck_no;
                LblDispatch.Text = DispatchName;

                LblSource.Text = ds.Tables[0].Rows[0]["Source"].ToString();
                lbldispatchgodown.Text = ds.Tables[0].Rows[0]["Dispatch_Godown"].ToString();
                comid.Text = ds.Tables[0].Rows[0]["Commodity"].ToString();
                schId.Text = ds.Tables[0].Rows[0]["Scheme"].ToString();
                Label2.Text = ds.Tables[0].Rows[0]["Created_date"].ToString();
            }
        }

        else
        {

        }
        
    }

    protected void ddlracknumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlracknumber.SelectedItem.Text == "--Select--")
        {
            ddlrackChallan.DataSource = "";
            ddlrackChallan.DataBind();
        }
        else
        {
            Rack_Challan();
        }
        
    }

    protected void ddlrackChallan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrackChallan.SelectedItem.Text == "--Select--")
        {

        }
        else
        {
            string selState = "Select State_Id from SCSC_RailHead_TC where Challan_No = '"+ddlrackChallan.SelectedItem.Text+"'";

            SqlCommand cmdstate = new SqlCommand(selState, con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(cmdstate);

            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Challan Number Not Exist'); </script> ");
                return;
            }
            else
            {
              string state = ds.Tables[0].Rows[0]["State_Id"].ToString();

              if (state == "23")
              {
                  string data = "select pds.districtsmp.district_name , tbl_MetaData_STORAGE_COMMODITY.Commodity_Name , tbl_MetaData_SCHEME.Scheme_Name , SCSC_RailHead_TC.Qty_send , SCSC_RailHead_TC.Bags ,convert(nvarchar,SCSC_RailHead_TC.Challan_Date,103)Challan_Date , SCSC_RailHead_TC.Truck_no ,DispatchType.DispatchName , CONVERT(nvarchar,SCSC_RailHead_TC.Created_date,101)Created_date ,SCSC_RailHead_TC.Commodity,SCSC_RailHead_TC.Scheme ,SCSC_RailHead_TC.Dispatch_Godown , SCSC_RailHead_TC.Source from SCSC_RailHead_TC inner join pds.districtsmp on pds.districtsmp.district_code = SCSC_RailHead_TC.Sendto_District inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = SCSC_RailHead_TC.Commodity inner join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.Scheme_Id = SCSC_RailHead_TC.Scheme inner join DispatchType on DispatchType.DispatchId = SCSC_RailHead_TC.DispatchID where Challan_No = '" + ddlrackChallan.SelectedItem.Text + "' ";
                  SqlCommand cmdData = new SqlCommand(data, con);

                  if (con.State == ConnectionState.Closed)
                  {
                      con.Open();
                  }

                  SqlDataAdapter daData = new SqlDataAdapter(cmdData);

                  DataSet dsData = new DataSet();

                  daData.Fill(dsData);

                  if (dsData.Tables[0].Rows.Count == 0)
                  {
                      Label3.Text = "";
                      lblDepotName.Text = "";

                      LblCommodity.Text = "";
                      LblScheme.Text = "";
                      LblQty.Text = "";
                      LblBags.Text = "";
                      lbldate.Text = "";
                      Label4.Text = "";
                      LblDispatch.Text = "";
                      Label2.Text = "";

                      btnDelete.Enabled = false;
                  }

                  else
                  {
                      // Get Own Dist Data
                      btnDelete.Enabled = true;

                      Label3.Text = "Madhya Pradesh";
                      lblDepotName.Text = dsData.Tables[0].Rows[0]["district_name"].ToString();

                      LblCommodity.Text = dsData.Tables[0].Rows[0]["Commodity_Name"].ToString();
                      LblScheme.Text = dsData.Tables[0].Rows[0]["Scheme_Name"].ToString();
                      LblQty.Text = dsData.Tables[0].Rows[0]["Qty_send"].ToString();
                      LblBags.Text = dsData.Tables[0].Rows[0]["Bags"].ToString();
                      lbldate.Text = dsData.Tables[0].Rows[0]["Challan_Date"].ToString();
                      Label4.Text = dsData.Tables[0].Rows[0]["Truck_no"].ToString();
                      LblDispatch.Text = dsData.Tables[0].Rows[0]["DispatchName"].ToString();
                      Label2.Text = dsData.Tables[0].Rows[0]["Created_date"].ToString();

                      LblSource.Text = dsData.Tables[0].Rows[0]["Source"].ToString();
                      lbldispatchgodown.Text = dsData.Tables[0].Rows[0]["Dispatch_Godown"].ToString();
                    

                      comid.Text = dsData.Tables[0].Rows[0]["Commodity"].ToString();
                      schId.Text = dsData.Tables[0].Rows[0]["Scheme"].ToString();

                      // Data Displayed fro query
                  }
              }

              else
              {
// data for Other State

                  string getowndist = "select State_Master.State_Name , OtherState_DistrictCode.district_name , tbl_MetaData_STORAGE_COMMODITY.Commodity_Name , tbl_MetaData_SCHEME.Scheme_Name ,SCSC_RailHead_TC.Qty_send , SCSC_RailHead_TC.Bags ,  SCSC_RailHead_TC.Truck_no , convert(nvarchar,SCSC_RailHead_TC.Challan_Date,103)Challan_Date ,DispatchType.DispatchName,SCSC_RailHead_TC.Dispatch_Godown , SCSC_RailHead_TC.Source ,CONVERT(nvarchar,SCSC_RailHead_TC.Created_date,101)Created_date,SCSC_RailHead_TC.Commodity,SCSC_RailHead_TC.Scheme from SCSC_RailHead_TC inner join OtherState_DistrictCode on  OtherState_DistrictCode.district_code = SCSC_RailHead_TC.Sendto_District inner join State_Master on State_Master.State_Code = SCSC_RailHead_TC.State_Id inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = SCSC_RailHead_TC.Commodity inner join tbl_MetaData_SCHEME on tbl_MetaData_SCHEME.Scheme_Id = SCSC_RailHead_TC.Scheme inner join DispatchType on DispatchType.DispatchId = SCSC_RailHead_TC.DispatchID where Challan_No = '" + ddlrackChallan.SelectedItem.Text + "'";
                  SqlCommand cmdowndis = new SqlCommand(getowndist, con);
                  SqlDataAdapter dadata = new SqlDataAdapter(cmdowndis);
                  DataSet dsowndata = new DataSet();
                  dadata.Fill(dsowndata);

                  if (dsowndata.Tables[0].Rows.Count == 0)
                  {
                      Label3.Text = "";
                      lblDepotName.Text = "";

                      LblCommodity.Text = "";
                      LblScheme.Text = "";
                      LblQty.Text = "";
                      LblBags.Text = "";
                      lbldate.Text = "";
                      Label4.Text = "";
                      LblDispatch.Text = "";
                      Label2.Text = "";
                  }

                  else
                  {
                      Label3.Text = dsowndata.Tables[0].Rows[0]["State_Name"].ToString();

                      lblDepotName.Text = dsowndata.Tables[0].Rows[0]["district_name"].ToString();

                      LblCommodity.Text = dsowndata.Tables[0].Rows[0]["Commodity_Name"].ToString();
                      LblScheme.Text = dsowndata.Tables[0].Rows[0]["Scheme_Name"].ToString();
                      LblQty.Text = dsowndata.Tables[0].Rows[0]["Qty_send"].ToString();
                      LblBags.Text = dsowndata.Tables[0].Rows[0]["Bags"].ToString();
                      lbldate.Text = dsowndata.Tables[0].Rows[0]["Challan_Date"].ToString();
                      Label4.Text = dsowndata.Tables[0].Rows[0]["Truck_no"].ToString();
                      LblDispatch.Text = dsowndata.Tables[0].Rows[0]["DispatchName"].ToString();
                      //
                      Label2.Text = dsowndata.Tables[0].Rows[0]["Created_date"].ToString();
                      LblSource.Text = dsowndata.Tables[0].Rows[0]["Source"].ToString();
                      lbldispatchgodown.Text = dsowndata.Tables[0].Rows[0]["Dispatch_Godown"].ToString();

                      comid.Text = dsowndata.Tables[0].Rows[0]["Commodity"].ToString();
                      schId.Text = dsowndata.Tables[0].Rows[0]["Scheme"].ToString();
                  }   
              }
            }

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        if (RdbyRoad.Checked)
        {
            if (ddlroadchallan.SelectedItem.Text == "--Select--")
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Challan Number'); </script> ");
                return;
            }
            try
            {
                string query = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance+" + LblQty.Text + ",Current_Bags=Current_Bags+" + LblBags.Text + " where District_Id= '" + districtid + "' and Depotid= '" + depotid + "' and Commodity_Id= '" + comid.Text + "' and Scheme_Id= '" + schId.Text + "' and Godown='" + lbldispatchgodown.Text + "'";
                SqlCommand cmdOpening = new SqlCommand(query, con);
                int x = cmdOpening.ExecuteNonQuery();


                string chelog = "select count(*) FROM SCSC_Sale_Details where District_Id = '" + districtid + "' and Depotid = '" + depotid + "' and Commodity_Id = '" + comid.Text + "' and Scheme_Id = '" + schId.Text + "' and Cast(datediff(day, 0, CreatedDate) as datetime) = '" + Label2.Text + "'";

                SqlCommand cmd1 = new SqlCommand(chelog, con);
                int L = Convert.ToInt16(cmd1.ExecuteScalar().ToString());

                if (L > 0)
                {
                    string inlog = "Insert into SCSC_Sale_Details_Log select * from SCSC_Sale_Details where District_Id = '" + districtid + "' and Depotid = '" + depotid + "' and Commodity_Id = '" + comid.Text + "' and Scheme_Id = '" + schId.Text + "' and Cast(datediff(day, 0, CreatedDate) as datetime) = '" + Label2.Text + "'";
                    SqlCommand cmdInsLog = new SqlCommand(inlog, con);
                    int logIns = cmdInsLog.ExecuteNonQuery();

                    string delsales = "delete FROM SCSC_Sale_Details where District_Id = '" + districtid + "' and Depotid = '" + depotid + "' and Commodity_Id = '" + comid.Text + "' and Scheme_Id = '" + schId.Text + "' and Cast(datediff(day, 0, CreatedDate) as datetime) = '" + Label2.Text + "'";
                    SqlCommand cmdsale = new SqlCommand(delsales, con);
                    int y = cmdsale.ExecuteNonQuery();
                }   

                string InsLog = "Insert into SCSC_Truck_challan_Log select * from SCSC_Truck_challan where  Dist_ID = '" + districtid + "' and Depot_Id = '" + depotid + "' and Challan_No = '" + ddlroadchallan.SelectedItem.Text + "'";
                SqlCommand cmdIsLog = new SqlCommand(InsLog, con);
                int log = cmdIsLog.ExecuteNonQuery();

                string delchallan = "delete FROM SCSC_Truck_challan where  Dist_ID = '" + districtid + "' and Depot_Id = '" + depotid + "' and Challan_No = '" + ddlroadchallan.SelectedItem.Text + "'";
                SqlCommand cmdDelchallan = new SqlCommand(delchallan, con);
                int z = cmdDelchallan.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                throw ex;
            }


            road_challan();
        }

        else
            if (RdRack.Checked)
            {
                if (ddlrackChallan.SelectedItem.Text == "--Select--")
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Select Challan Number'); </script> ");
                    return;
                }
                try
                {
                    string query = "Update dbo.issue_opening_balance set Current_Balance=Current_Balance+" + LblQty.Text + ",Current_Bags=Current_Bags+" + LblBags.Text + " where District_Id= '" + districtid + "' and Depotid= '" + depotid + "' and Commodity_Id= '" + comid.Text + "' and Scheme_Id= '" + schId.Text + "' and Godown='" + lbldispatchgodown.Text + "'";
                    SqlCommand cmdOpening = new SqlCommand(query, con);
                    int x = cmdOpening.ExecuteNonQuery();

                    string chelog = "select count(*) FROM SCSC_Sale_Details where District_Id = '" + districtid + "' and Depotid = '" + depotid + "' and Commodity_Id = '" + comid.Text + "' and Scheme_Id = '" + schId.Text + "' and Cast(datediff(day, 0, CreatedDate) as datetime) = '" + Label2.Text + "'";

                    SqlCommand cmd1 = new SqlCommand(chelog, con);
                    int L = Convert.ToInt16(cmd1.ExecuteScalar().ToString());

                    if (L > 0)
                    {
                        string inlog = "Insert into SCSC_Sale_Details_Log select * from SCSC_Sale_Details where District_Id = '" + districtid + "' and Depotid = '" + depotid + "' and Commodity_Id = '" + comid.Text + "' and Scheme_Id = '" + schId.Text + "' and Cast(datediff(day, 0, CreatedDate) as datetime) = '" + Label2.Text + "'";
                        SqlCommand cmdInsLog = new SqlCommand(inlog, con);
                        int logIns = cmdInsLog.ExecuteNonQuery();

                        string delsales = "delete FROM SCSC_Sale_Details where District_Id = '" + districtid + "' and Depotid = '" + depotid + "' and Commodity_Id = '" + comid.Text + "' and Scheme_Id = '" + schId.Text + "' and Cast(datediff(day, 0, CreatedDate) as datetime) = '" + Label2.Text + "'";
                        SqlCommand cmdsale = new SqlCommand(delsales, con);
                        int y = cmdsale.ExecuteNonQuery();
                    }

                    string InsLog = "Insert into SCSC_RailHead_TC_Log select * from SCSC_RailHead_TC where  Dist_ID = '" + districtid + "' and Depot_Id = '" + depotid + "' and Challan_No = '" + ddlrackChallan.SelectedItem.Text + "'";
                    SqlCommand cmdIsLog = new SqlCommand(InsLog, con);
                    int log = cmdIsLog.ExecuteNonQuery();


                    string delchallan = "delete FROM SCSC_RailHead_TC where  Dist_ID = '" + districtid + "' and Depot_Id = '" + depotid + "' and Challan_No = '"+ddlrackChallan.SelectedItem.Text+"'";
                    SqlCommand cmdDelchallan = new SqlCommand(delchallan, con);
                    int z = cmdDelchallan.ExecuteNonQuery();

                }

                catch(Exception ex)
                {
                    throw ex;
                }
                Rack_Challan();
                rack_Number();                 
            }

            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Select Option Road or Rack....'); </script> ");
            }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Deleted'); </script> ");

        Label3.Text = "";
        lblDepotName.Text = "";

        LblCommodity.Text = "";
        LblScheme.Text = "";
        LblQty.Text = "";
        LblBags.Text = "";
        lbldate.Text = "";
        Label4.Text = "";
        LblDispatch.Text = "";
        Label2.Text = "";
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("issue_welcome.aspx");
    }
}
