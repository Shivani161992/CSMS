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

public partial class District_DeleteOpeningBalance : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    
    string distid = "";
    string issueid = "";
    string version = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            distid = Session["dist_id"].ToString();
          
            version = Session["hindi"].ToString();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (!IsPostBack)
            {
                string issuecentre = "SELECT DepotID ,DepotName FROM tbl_MetaData_DEPOT where DistrictId = '23"+distid+"' ";
                SqlCommand cmd = new SqlCommand(issuecentre, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

               ddlissuecentre.DataSource = ds.Tables[0];

               ddlissuecentre.DataTextField = "DepotName";
               ddlissuecentre.DataValueField = "DepotID";
               ddlissuecentre.DataBind();
               ddlissuecentre.Items.Insert(0, "--Select--"); 
                                
            }
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    void fillGrid()
    {
        issueid = ddlissuecentre.SelectedValue;

        int year = int.Parse(DateTime.Today.Date.Year.ToString());
        int month = int.Parse(DateTime.Today.Date.Month.ToString());
        string qrychk = "select issue_opening_balance.Godown,issue_opening_balance.Source,issue_opening_balance.Scheme_Id,issue_opening_balance.Commodity_Id,convert(decimal(18,5),issue_opening_balance.Current_Balance) as Current_Balance ,issue_opening_balance.Current_Bags,convert(decimal(18,5),issue_opening_balance.Quantity) as Quantity ,issue_opening_balance.Bags,issue_opening_balance.Crop_year,tbl_MetaData_GODOWN.Godown_Name,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name ,tbl_MetaData_SCHEME.Scheme_Name as Scheme_Name,Source_Arrival_Type.Source_Name as Source_Name  from dbo.issue_opening_balance left join dbo.tbl_MetaData_STORAGE_COMMODITY on issue_opening_balance.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.tbl_MetaData_SCHEME on issue_opening_balance.Scheme_Id =tbl_MetaData_SCHEME.Scheme_Id left join dbo.Source_Arrival_Type on issue_opening_balance.Source=Source_Arrival_Type.Source_ID left join dbo.tbl_MetaData_GODOWN on issue_opening_balance.Godown=tbl_MetaData_GODOWN.Godown_ID  where issue_opening_balance.District_Id='" + distid + "'and issue_opening_balance.Depotid='" + issueid + "'";
        SqlCommand cmd = new SqlCommand(qrychk, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet dschk = new DataSet();
        da.Fill(dschk);

        if (dschk.Tables[0].Rows.Count == 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Details Not Found'); </script> ");
            GridView1.DataSource = "";
            GridView1.DataBind();
        }
        
        else
        {
            GridView1.DataSource = dschk.Tables[0];
            GridView1.DataBind();
        }
    }

    protected void ddlissuecentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }

        fillGrid();

        if (con.State == ConnectionState.Open)
        {
            con.Close();

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        // check and delete before Log
        string distid = Session["dist_id"].ToString();
        string issueid = ddlissuecentre.SelectedValue;

        int count = 0;

        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chk = null;
            chk = (CheckBox)row.FindControl("chkDelete");
            if (chk.Checked == true)
            {
              
                string godown = chk.Text.Trim().ToString();

                string mmsource = row.Cells[10].Text;

                string comdty = row.Cells[11].Text;

                string scheme = row.Cells[12].Text;

                string CropYear = row.Cells[9].Text;

                string gdnid = row.Cells[13].Text;

                string opnyear = DateTime.Today.Year.ToString();
                int year = int.Parse(opnyear);
                
                int month = int.Parse(DateTime.Today.Date.Month.ToString());

                # region Issueopeningbalance_table
                string delLogqry = "insert into issue_opening_balance_Log select * from issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "' and Godown='" + godown + "'and Commodity_ID='" + comdty + "' and Scheme_ID='" + scheme + "' and Source='" + mmsource + "' and Crop_year = '" + CropYear + "'";
                SqlCommand delcmd = new SqlCommand(delLogqry, con);
                int log = delcmd.ExecuteNonQuery();


                string query = "delete  from issue_opening_balance where District_Id='" + distid + "'and Depotid='" + issueid + "' and Godown='" + godown + "'and Commodity_ID='" + comdty + "' and Scheme_ID='" + scheme + "' and Source='" + mmsource + "' and Crop_year = '" + CropYear + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                int req = cmd.ExecuteNonQuery();

                # endregion

                # region tbl_Stock_Registor

                string cheprelog = "select count(*) from tbl_Stock_Registor where Commodity_Id ='" + comdty + "' and Scheme_Id='" + scheme + "' and DistrictId ='" + distid + "'and DepotID='" + issueid + "' and Crop_Year = '" + CropYear+ "'";
                SqlCommand cmdprelog = new SqlCommand(cheprelog, con);
                string x = cmdprelog.ExecuteScalar().ToString();

                int chlog = Convert.ToInt16(x);

                if (chlog > 0)
                {
                    string Insert_StockRegister_Log = "Insert into tbl_Stock_Registor_Log(DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Crop_Year,Month,Year,Remarks)  select DistrictId,DepotID,Commodity_ID,Scheme_ID,Opening_Balance,Recieved_Procure,Recieved_Otherg,Recieved_FCI,Received_OtherSch,Recieved_Other_Src,Received_RailHead,Received_CMR,Received_Levy,Sale_Do,Sale_otherg,Transfer_OtherSch,Crop_Year,Month,Year,Remarks from tbl_Stock_Registor where Commodity_Id ='" + comdty + "' and Scheme_Id='" + scheme + "' and DistrictId ='" + distid + "'and DepotID='" + issueid + "' and Crop_Year = '" + CropYear + "'";
                    SqlCommand cmdLog = new SqlCommand(Insert_StockRegister_Log, con);

                    int logdetail = cmdLog.ExecuteNonQuery();

                    string del_tblstock = "Delete from tbl_Stock_Registor where Commodity_Id ='" + comdty + "' and Scheme_Id='" + scheme + "' and DistrictId ='" + distid + "'and DepotID='" + issueid + "' and Crop_Year = '" + CropYear + "'";
                    SqlCommand cmddel = new SqlCommand(del_tblstock, con);

                    int deldetail = cmddel.ExecuteNonQuery();
                }

               # endregion

                # region Current_Godown_Position


                string Qty = row.Cells[5].Text;

                string Bags = row.Cells[6].Text;


                string cheprelog1 = "select count(*) from Current_Godown_Position where District_Id='" + distid + "' and Depotid='" + issueid + "' and Godown='" + godown + "'";
                SqlCommand cmdprelog1 = new SqlCommand(cheprelog1, con);
                string x1 = cmdprelog1.ExecuteScalar().ToString();

                int chlog1 = Convert.ToInt16(x1);

                if (chlog1 > 0)
                {
                    string selectolddata = "Select Current_Balance,Current_Bags,Current_Capacity from Current_Godown_Position where District_Id='" + distid + "' and Depotid='" + issueid + "' and Godown='" + godown + "'";
                    SqlCommand cmd_olddata = new SqlCommand(selectolddata, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd_olddata);
                    DataSet ds_old = new DataSet();
                    da.Fill(ds_old);

                    string oldqTY = ds_old.Tables[0].Rows[0]["Current_Balance"].ToString();
                    string OLDBAGS = ds_old.Tables[0].Rows[0]["Current_Bags"].ToString();

                    string OldCurrentCap = ds_old.Tables[0].Rows[0]["Current_Capacity"].ToString();

                    double oldcapacity = Convert.ToDouble(OldCurrentCap);


                    string GetQty = row.Cells[5].Text;

                    string GetBags = row.Cells[6].Text;

                   
                    Double qtyold = Convert.ToDouble(oldqTY.ToString());

                    Int32 bagold = Convert.ToInt32(OLDBAGS.ToString());

                   
                    Double qtyDel = Convert.ToDouble(GetQty.ToString());

                    Int32 bagdel = Convert.ToInt32(GetBags.ToString());

                    double newqty = qtyold - qtyDel;
                    string newcurrentqty = Convert.ToString(newqty);

                    Int32 newbag = bagold - bagdel;

                    string newcurrentbags = Convert.ToString(newbag);

                    double new_currentCapacity = oldcapacity + qtyDel;

                    string current_Capacity = Convert.ToString(new_currentCapacity);

                    string GodwnPosition_Logqry = "insert into Current_Godown_Position_Log select * from Current_Godown_Position where District_Id='" + distid + "' and Depotid='" + issueid + "' and Godown='" + godown + "'";
                    SqlCommand GodwnPosition_cmd = new SqlCommand(GodwnPosition_Logqry, con);
                    int status = GodwnPosition_cmd.ExecuteNonQuery();


                    string query_GodownPosition = "Update Current_Godown_Position set Current_Balance = '"+newcurrentqty+"' , Current_Bags = '"+newcurrentbags+"' , Current_Capacity = '"+current_Capacity+"' where District_Id='" + distid + "' and Depotid='" + issueid + "' and Godown='" + godown + "'";
                    SqlCommand cmdGodownPosition_ = new SqlCommand(query_GodownPosition, con);
                    int insvalue = cmdGodownPosition_.ExecuteNonQuery();
                }


                

              //  string qreygdnU = "update dbo.Current_Godown_Position set Current_Capacity=Current_Capacity +(" + uqty + "),Current_Bags=Current_Bags-(" + ubags + "),Current_Balance=Current_Balance-(" + uqty + ") where District_Id='" + distid + "' and Depotid='" + issueid + "' and Godown='" + mgodown + "'";

                # endregion

                count = count + 1;

            }   
          
        }

        if (count == 0)
        {
             Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please click on check box before delete'); </script> ");
        }

        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deleted Sucessfully'); </script> ");

            fillGrid();
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dist_Welcome.aspx");
    }

   
}
