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
public partial class Edit_delivery1 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;
    SqlCommand cmd1 = new SqlCommand();
    SqlDataReader dr1;
    string distid = "";
    string sid = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {        try
        {
            if (Session["dist_id"] == null)
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }        
        distid = Session["dist_id"].ToString();
        sid = Session["issue_id"].ToString();
        if (Page.IsPostBack == false)
        {
            GetDepot();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string comm = "";
        string comm1 = "";
        string schm="";
        string schm1 = "";
        if (DropDownList1.SelectedItem.Value == "1")
        {
            comm = "rice_apl_alloc";
            comm1 = "rice_apl_lift";
            schm = "1,2,3,4,20";
            schm1 = "1";
        }
        if (DropDownList1.SelectedItem.Value == "2")
        {
            comm = "rice_bpl_alloc";
            comm1 = "rice_bpl_lift";
            schm = "1,2,3,4,20";
            schm1 = "2";
        }
        if (DropDownList1.SelectedItem.Value == "3")
        {
            comm = "rice_aay_alloc";
            comm1 = "rice_aay_lift";
            schm = "1,2,3,4,20";
            schm1 = "3";
        }
        if (DropDownList1.SelectedItem.Value == "4")
        {
            comm = "wheat_apl_alloc";
            comm1 = "wheat_apl_lift";
            schm = "5,6,7,22";
            schm1 = "1";
        }
        if (DropDownList1.SelectedItem.Value == "5")
        {
            comm = "wheat_bpl_alloc";
            comm1 = "wheat_bpl_lift";
            schm = "5,6,7,22";
            schm1 = "2";
        }
        if (DropDownList1.SelectedItem.Value == "6")
        {
            comm = "wheat_aay_alloc";
            comm1 = "wheat_aay_lift";
            schm = "5,6,7,22";
            schm1 = "3";
        }
        if (DropDownList1.SelectedItem.Value == "7")
        {
            comm = "sugar_alloc";
            comm1 = "sugar_lift";
            schm = "17,23";         
        }
        
        string distcode = "";
        string issuecode="";
        string blockcode="";
        string amonth="";
        string ayear="";
        string fpscode="";
        string allotqty="";        
        string str2 = "";
        if (DropDownList1.SelectedItem.Value == "7")
        {
            str2 = "select distinct district_code,block_code,issueCentre_code,allotment_month,allotment_year,fps_code,allot_qty from do_fps group by district_code,block_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,allot_qty having commodity in (" + schm + ")";
        }
        else
        {
            str2 = "select distinct district_code,block_code,issueCentre_code,allotment_month,allotment_year,fps_code,allot_qty from do_fps group by district_code,block_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,allot_qty having commodity in (" + schm + ")   and scheme_id=" + schm1 + "";
        }
                cmd.Connection = con;
                cmd.CommandText = str2;
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    distcode = dr["district_code"].ToString();
                    issuecode = dr["issueCentre_code"].ToString();
                    blockcode = dr["block_code"].ToString();
                    amonth = dr["allotment_month"].ToString();
                    ayear = dr["allotment_year"].ToString();
                    fpscode = dr["fps_code"].ToString();
                    allotqty= dr["allot_qty"].ToString();
                    string temp = "NNN";
                    string str1 = "";
                    str1 = "select * from pds.fps_allot_mpsc where district_code='" + distcode + "' and block_code='" + blockcode + "' and  depot_code='" + issuecode + "' and  fps_code='" + fpscode + "' and  month=" + amonth + " and  Year=" + ayear + "";
                    cmd1.Connection = con1;
                    cmd1.CommandText = str1;
                    con1.Open();
                    dr1 = cmd1.ExecuteReader();
                    while (dr1.Read())
                    {
                        temp = "YYY";
                    }
                    dr1.Close();
                    if (temp == "YYY")
                    {
                        str1 = "update pds.fps_allot_mpsc set " + comm + "='" + allotqty + "'," + comm1 + "='" + allotqty + "'  where district_code='" + distcode + "' and block_code='" + blockcode + "' and  depot_code='" + issuecode + "' and  fps_code='" + fpscode + "' and  month=" + amonth + " and  Year=" + ayear + "";
                    }
                    else
                    {
                        str1 = "INSERT INTO pds.fps_allot_mpsc( district_code,block_code, depot_code , fps_code, month, Year," + comm + "," + comm1 + ") values ('"+ distcode +"','"+ blockcode +"','"+ issuecode +"','"+ fpscode +"',"+ amonth +","+ ayear +",'"+ allotqty +"','"+ allotqty +"')";
                    }
                    cmd1.CommandText = str1;
                    cmd1.Connection = con1;
                    cmd1.ExecuteNonQuery();
                    con1.Close();
                }
                Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
                
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string str1 = "";
        //str1 = "SELECT issue_against_do.district_code,issue_against_do.issueCentre_code,delivery_order_mpscsc.commodity_id, delivery_order_mpscsc.scheme_id,issue_against_do.trans_id FROM   issue_against_do INNER JOIN delivery_order_mpscsc ON issue_against_do.delivery_order_no = delivery_order_mpscsc.delivery_order_no AND issue_against_do.district_code = delivery_order_mpscsc.district_code AND  issue_against_do.issueCentre_code = delivery_order_mpscsc.issueCentre_code AND  issue_against_do.allotment_month = delivery_order_mpscsc.allotment_month AND  issue_against_do.allotment_year = delivery_order_mpscsc.allotment_year where issue_against_do.Godown is NULL order by issue_against_do.district_code,issue_against_do.issueCentre_code";
        str1 = "SELECT issue_opening_balance1.District_Id, pds.districtsmp.district_name, issue_opening_balance1.Depotid,tbl_MetaData_DEPOT.DepotName, issue_opening_balance1.Godown, issue_opening_balance1.Source, issue_opening_balance1.Commodity_Id, issue_opening_balance1.Scheme_Id, issue_opening_balance1.Bags, issue_opening_balance1.Quantity FROM   issue_opening_balance1 LEFT JOIN   pds.districtsmp ON issue_opening_balance1.District_Id = pds.districtsmp.district_code left JOIN   tbl_MetaData_DEPOT ON issue_opening_balance1.District_Id = tbl_MetaData_DEPOT.DistrictId AND  issue_opening_balance1.Depotid = tbl_MetaData_DEPOT.DepotID where  issue_opening_balance1.Bags>0 order by issue_opening_balance1.District_Id,issue_opening_balance1.Depotid,issue_opening_balance1.Godown,issue_opening_balance1.Source,  issue_opening_balance1.Commodity_Id, issue_opening_balance1.Scheme_Id";
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);    
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Session["dt"] = dt;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        DataTable dt=(DataTable)Session["dt"];
        string dist = "";
        string depot = "";
        string godown = "";
        string soa="";
        string comm = "";
        string scheme = "";        
        int rcount = 0;
        string str1 = "";
        con.Open();
        str1 = "update  issue_opening_balance set bags=" + 0 + "";
        cmd.CommandText = str1;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();   
        while (rcount < dt.Rows.Count)
        {
            dist = dt.Rows[rcount][0].ToString();
            depot = dt.Rows[rcount][2].ToString();
            godown = dt.Rows[rcount][4].ToString();
            soa = dt.Rows[rcount][5].ToString();
            comm = dt.Rows[rcount][6].ToString();
            scheme = dt.Rows[rcount][7].ToString();
            int bags =CheckNullInt (dt.Rows[rcount][8].ToString());
            str1 = "update  issue_opening_balance set bags=" + bags + " where District_Id='" + dist + "' and Depotid='" + depot + "' and Commodity_Id='" + comm + "' and Scheme_Id='" + scheme + "' and Godown='" + godown + "' and Source='" + soa + "'";
            cmd.CommandText = str1;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();       
            rcount = rcount + 1;
        }
        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
        con.Close();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Edit_delivery1.aspx");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        DataTable dt_tot = new DataTable();
        dt_tot.Columns.Add("District_Id");
        dt_tot.Columns.Add("Depotid");
        dt_tot.Columns.Add("Godown");
        dt_tot.Columns.Add("Godown_Name");
        dt_tot.Columns.Add("Source");
        dt_tot.Columns.Add("Source_Name");
        dt_tot.Columns.Add("Commodity_Id");
        dt_tot.Columns.Add("Commodity_Name");
        dt_tot.Columns.Add("Scheme_Id");
        dt_tot.Columns.Add("Scheme_Name");
        dt_tot.Columns.Add("Ope_Bal");
        dt_tot.Columns.Add("Ope_bags");
        dt_tot.Columns.Add("Rec_qty");
        dt_tot.Columns.Add("Rec_bags");
        dt_tot.Columns.Add("Rec_qty_strans");
        dt_tot.Columns.Add("Rec_bags_strans");
        dt_tot.Columns.Add("Lift_qty_DO");
        dt_tot.Columns.Add("Lift_bags_DO");
        dt_tot.Columns.Add("Lift_qty_TC");
        dt_tot.Columns.Add("Lift_bags_TC");
        dt_tot.Columns.Add("Lift_qty_strans");
        dt_tot.Columns.Add("Lift_bags_strans");
        dt_tot.Columns.Add("Rec_qty_Gain");
        dt_tot.Columns.Add("Rec_bags_Gain");
        dt_tot.Columns.Add("Tot_Rec_Qty");
        dt_tot.Columns.Add("Tot_Lift_Qty");
        dt_tot.Columns.Add("Tot_Rec_Bags");
        dt_tot.Columns.Add("Tot_Lift_Bags");
        dt_tot.Columns.Add("Current_Balance");
        dt_tot.Columns.Add("Current_Bags");
        dt_tot.Columns.Add("Rec_qty_PRC");
        dt_tot.Columns.Add("Rec_bags_PRC");
        DataTable dt1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("SELECT issue_opening_balance.District_Id, issue_opening_balance.Depotid, issue_opening_balance.Godown, tbl_MetaData_GODOWN.Godown_Name, issue_opening_balance.Source, Source_Arrival_Type.Source_Name,  issue_opening_balance.Commodity_Id, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,            issue_opening_balance.Scheme_Id, tbl_MetaData_SCHEME.Scheme_Name, convert(decimal(18,5),issue_opening_balance.Quantity) as Ope_Bal,issue_opening_balance.Bags as Ope_Bags,(SELECT sum(convert(decimal(18,5),tbl_Receipt_Details .Recd_Qty)) as recqty  FROM   tbl_Receipt_Details where tbl_Receipt_Details.Dist_Id =issue_opening_balance.District_Id AND  tbl_Receipt_Details.Depot_ID =issue_opening_balance.Depotid  AND tbl_Receipt_Details.Godown = issue_opening_balance.Godown AND  tbl_Receipt_Details.S_of_arrival = issue_opening_balance.Source  and tbl_Receipt_Details.Commodity =issue_opening_balance.Commodity_Id AND  tbl_Receipt_Details.Scheme = issue_opening_balance.Scheme_Id  )  as Rec_qty,(SELECT  sum(tbl_Receipt_Details .Recieved_Bags) as recbags FROM   tbl_Receipt_Details where tbl_Receipt_Details.Dist_Id =issue_opening_balance.District_Id AND  tbl_Receipt_Details.Depot_ID =issue_opening_balance.Depotid  AND tbl_Receipt_Details.Godown = issue_opening_balance.Godown AND  tbl_Receipt_Details.S_of_arrival = issue_opening_balance.Source  and tbl_Receipt_Details.Commodity =issue_opening_balance.Commodity_Id AND  tbl_Receipt_Details.Scheme = issue_opening_balance.Scheme_Id  )  as Rec_bags,(SELECT sum(convert(decimal(18,5),State_Scheme_Transfer .Quantity)) as recqty_strans  FROM   State_Scheme_Transfer where State_Scheme_Transfer.D_District =issue_opening_balance.District_Id AND  State_Scheme_Transfer.D_Depot =issue_opening_balance.Depotid  AND State_Scheme_Transfer.Godown = issue_opening_balance.Godown AND  State_Scheme_Transfer.To_Source= issue_opening_balance.Source  and State_Scheme_Transfer.Commodity_Id=issue_opening_balance.Commodity_Id AND  State_Scheme_Transfer.D_Scheme_Id = issue_opening_balance.Scheme_Id  )  as Rec_qty_strans,(SELECT  sum(State_Scheme_Transfer .Bags) as recbags FROM   State_Scheme_Transfer where State_Scheme_Transfer.D_District=issue_opening_balance.District_Id AND  State_Scheme_Transfer.D_Depot =issue_opening_balance.Depotid  AND State_Scheme_Transfer.Godown = issue_opening_balance.Godown AND  State_Scheme_Transfer.To_Source = issue_opening_balance.Source  and State_Scheme_Transfer.Commodity_Id =issue_opening_balance.Commodity_Id AND  State_Scheme_Transfer.D_Scheme_Id = issue_opening_balance.Scheme_Id  )  as Rec_bags_strans,(SELECT sum(convert(decimal(18,5),issue_against_do .qty_issue)) as liftqty_do  FROM   issue_against_do where issue_against_do.delivery_order_no in (SELECT delivery_order_mpscsc.delivery_order_no  FROM   delivery_order_mpscsc where delivery_order_mpscsc.district_code =issue_opening_balance.District_Id AND  delivery_order_mpscsc.issueCentre_code =issue_opening_balance.Depotid  and delivery_order_mpscsc.commodity_id=issue_opening_balance.Commodity_Id AND  delivery_order_mpscsc.scheme_id = issue_opening_balance.Scheme_Id ) and  issue_against_do.district_code=issue_opening_balance.District_Id AND  issue_against_do.issueCentre_code =issue_opening_balance.Depotid  AND issue_against_do.Godown = issue_opening_balance.Godown AND  issue_against_do.Source= issue_opening_balance.Source ) as Lift_qty_DO,(SELECT sum(issue_against_do .bags) as liftbags_do  FROM   issue_against_do where issue_against_do.delivery_order_no in (SELECT delivery_order_mpscsc.delivery_order_no  FROM   delivery_order_mpscsc where delivery_order_mpscsc.district_code =issue_opening_balance.District_Id AND  delivery_order_mpscsc.issueCentre_code =issue_opening_balance.Depotid  and delivery_order_mpscsc.commodity_id=issue_opening_balance.Commodity_Id AND  delivery_order_mpscsc.scheme_id = issue_opening_balance.Scheme_Id ) and  issue_against_do.district_code=issue_opening_balance.District_Id AND  issue_against_do.issueCentre_code =issue_opening_balance.Depotid  AND issue_against_do.Godown = issue_opening_balance.Godown AND  issue_against_do.Source= issue_opening_balance.Source ) as Lift_bags_DO,(SELECT sum(convert(decimal(18,5),SCSC_Truck_challan .Qty_send)) as liftqty_tc  FROM   SCSC_Truck_challan where SCSC_Truck_challan.Dist_ID =issue_opening_balance.District_Id AND  SCSC_Truck_challan.Depot_Id=issue_opening_balance.Depotid  AND SCSC_Truck_challan.Dispatch_Godown = issue_opening_balance.Godown AND  SCSC_Truck_challan.Source= issue_opening_balance.Source  and SCSC_Truck_challan.Commodity=issue_opening_balance.Commodity_Id AND  SCSC_Truck_challan.Scheme= issue_opening_balance.Scheme_Id  )  as Lift_qty_TC,(SELECT  sum(SCSC_Truck_challan .Bags) as liftbags_do FROM   SCSC_Truck_challan where SCSC_Truck_challan.Dist_ID=issue_opening_balance.District_Id AND  SCSC_Truck_challan.Depot_Id =issue_opening_balance.Depotid  AND SCSC_Truck_challan.Dispatch_Godown= issue_opening_balance.Godown AND  SCSC_Truck_challan.Source = issue_opening_balance.Source  and SCSC_Truck_challan.Commodity =issue_opening_balance.Commodity_Id AND  SCSC_Truck_challan.Scheme= issue_opening_balance.Scheme_Id  )  as Lift_bags_TC,(SELECT sum(convert(decimal(18,5),State_Scheme_Transfer .Quantity)) as liftqty_strans  FROM   State_Scheme_Transfer where State_Scheme_Transfer.District_Id =issue_opening_balance.District_Id AND  State_Scheme_Transfer.Depotid =issue_opening_balance.Depotid  AND State_Scheme_Transfer.Godown = issue_opening_balance.Godown AND  State_Scheme_Transfer.From_Source= issue_opening_balance.Source  and State_Scheme_Transfer.Commodity_Id=issue_opening_balance.Commodity_Id AND  State_Scheme_Transfer.S_Scheme_Id= issue_opening_balance.Scheme_Id  )  as Lift_qty_strans, (SELECT  sum(State_Scheme_Transfer .Bags) as liftbags FROM   State_Scheme_Transfer where State_Scheme_Transfer.District_Id=issue_opening_balance.District_Id AND  State_Scheme_Transfer.Depotid =issue_opening_balance.Depotid  AND State_Scheme_Transfer.Godown = issue_opening_balance.Godown AND  State_Scheme_Transfer.From_Source = issue_opening_balance.Source  and State_Scheme_Transfer.Commodity_Id =issue_opening_balance.Commodity_Id AND  State_Scheme_Transfer.S_Scheme_Id = issue_opening_balance.Scheme_Id  )  as Lift_bags_strans,(SELECT sum(convert(decimal(18,5),Loss_gain .Quantity)) as recqty_LG FROM   Loss_gain where Loss_gain.District_Id =issue_opening_balance.District_Id AND  Loss_gain.Depotid =issue_opening_balance.Depotid  AND Loss_gain.Godown = issue_opening_balance.Godown AND  Loss_gain.Source= issue_opening_balance.Source  and Loss_gain.Commodity_Id=issue_opening_balance.Commodity_Id AND  Loss_gain.Scheme_Id= issue_opening_balance.Scheme_Id and Loss_gain.stock_type='G')  as Rec_qty_LG, (SELECT  sum(Loss_gain .Bags) as recbagslg FROM   Loss_gain where Loss_gain.District_Id=issue_opening_balance.District_Id AND  Loss_gain.Depotid=issue_opening_balance.Depotid  AND Loss_gain.Godown = issue_opening_balance.Godown AND  Loss_gain.Source = issue_opening_balance.Source  and Loss_gain.Commodity_Id =issue_opening_balance.Commodity_Id AND  Loss_gain.Scheme_Id = issue_opening_balance.Scheme_Id and Loss_gain.stock_type='G' )  as Rec_bags_LG,(SELECT  sum(convert(decimal(18,5),SCSC_Procurement .Recd_Qty)) as recqtyprc FROM   SCSC_Procurement where  SCSC_Procurement.Distt_ID=issue_opening_balance.District_Id AND  SCSC_Procurement.IssueCenter_ID =issue_opening_balance.Depotid  AND SCSC_Procurement.Recd_Godown= issue_opening_balance.Godown  and SCSC_Procurement.Commodity_Id=issue_opening_balance.Commodity_Id and issue_opening_balance.Scheme_Id='101' and issue_opening_balance.Source ='01')  as Rec_Qty_PRC,(SELECT  sum(SCSC_Procurement .Recd_Bags) as recqtybags FROM   SCSC_Procurement where  SCSC_Procurement.Distt_ID=issue_opening_balance.District_Id AND  SCSC_Procurement.IssueCenter_ID =issue_opening_balance.Depotid  AND SCSC_Procurement.Recd_Godown= issue_opening_balance.Godown  and SCSC_Procurement.Commodity_Id=issue_opening_balance.Commodity_Id and issue_opening_balance.Scheme_Id='101' and issue_opening_balance.Source ='01')  as Rec_Bags_PRC  FROM   issue_opening_balance INNER JOIN tbl_MetaData_GODOWN ON issue_opening_balance.District_Id = tbl_MetaData_GODOWN.DistrictId AND issue_opening_balance.Depotid = tbl_MetaData_GODOWN.DepotId AND issue_opening_balance.Godown = tbl_MetaData_GODOWN.Godown_ID INNER JOIN  Source_Arrival_Type ON issue_opening_balance.Source = Source_Arrival_Type.Source_ID INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON issue_opening_balance.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN  tbl_MetaData_SCHEME ON issue_opening_balance.Scheme_Id = tbl_MetaData_SCHEME.Scheme_Id  order by issue_opening_balance.District_Id,issue_opening_balance.Depotid,issue_opening_balance.Godown,issue_opening_balance.Source,issue_opening_balance.Commodity_Id,issue_opening_balance.Scheme_Id", con);    
        da.Fill(dt1);        
        int rcount = 0;
        while(rcount < dt1.Rows.Count)
        {
            decimal tot_rec_qty = System.Math.Round(CheckNull(dt1.Rows[rcount][10].ToString()) + CheckNull(dt1.Rows[rcount][12].ToString()) + CheckNull(dt1.Rows[rcount][14].ToString()) + CheckNull(dt1.Rows[rcount][22].ToString()) + CheckNull(dt1.Rows[rcount][24].ToString()), 5);
            decimal tot_lift_qty = System.Math.Round(CheckNull(dt1.Rows[rcount][16].ToString()) + CheckNull(dt1.Rows[rcount][18].ToString()) + CheckNull(dt1.Rows[rcount][20].ToString()), 5);
            int tot_rec_bags = CheckNullInt(dt1.Rows[rcount][11].ToString()) + CheckNullInt(dt1.Rows[rcount][13].ToString()) + CheckNullInt(dt1.Rows[rcount][15].ToString()) + CheckNullInt(dt1.Rows[rcount][23].ToString()) + CheckNullInt(dt1.Rows[rcount][25].ToString());
            int tot_lift_bags = CheckNullInt(dt1.Rows[rcount][17].ToString()) + CheckNullInt(dt1.Rows[rcount][19].ToString()) + CheckNullInt(dt1.Rows[rcount][21].ToString());
            decimal current_bal = System.Math.Round(tot_rec_qty - tot_lift_qty,5);
            int current_bags = tot_rec_bags - tot_lift_bags;
            dt_tot.Rows.Add(dt1.Rows[rcount][0].ToString(), dt1.Rows[rcount][1].ToString(), dt1.Rows[rcount][2].ToString(), dt1.Rows[rcount][3].ToString(), dt1.Rows[rcount][4].ToString(), dt1.Rows[rcount][5].ToString(), dt1.Rows[rcount][6].ToString(), dt1.Rows[rcount][7].ToString(), dt1.Rows[rcount][8].ToString(), dt1.Rows[rcount][9].ToString(), dt1.Rows[rcount][10].ToString(), dt1.Rows[rcount][11].ToString(), dt1.Rows[rcount][12].ToString(), dt1.Rows[rcount][13].ToString(), dt1.Rows[rcount][14].ToString(), dt1.Rows[rcount][15].ToString(), dt1.Rows[rcount][16].ToString(), dt1.Rows[rcount][17].ToString(), dt1.Rows[rcount][18].ToString(), dt1.Rows[rcount][19].ToString(), dt1.Rows[rcount][20].ToString(), dt1.Rows[rcount][21].ToString(), dt1.Rows[rcount][22].ToString(), dt1.Rows[rcount][23].ToString(), tot_rec_qty.ToString(), tot_lift_qty.ToString(), tot_rec_bags.ToString(), tot_lift_bags.ToString(), current_bal.ToString(), current_bags.ToString(), dt1.Rows[rcount][24].ToString(), dt1.Rows[rcount][25].ToString());
            rcount =rcount+1;
        }
        GridView1.DataSource = dt_tot;
        GridView1.DataBind();
        Session["dt_tot"] = dt_tot;        
    }
    protected decimal CheckNull(string Val)
    {
        decimal rval = 0;
        if (Val == "" || Val.ToLower().Contains("&nbsp;") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = Convert.ToDecimal(Val);
        }
        return rval;
    }
    protected int CheckNullInt(string Val)
    {
        int rval = 0;
        if (Val == "" || Val.ToLower().Contains("&nbsp;") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = int.Parse(Val);
        }
        return rval;
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        DataTable dt_tot = (DataTable)Session["dt_tot"];
        int rcount = 0;
        con.Open();
        while (rcount < dt_tot.Rows.Count)
        {
            string distcode = dt_tot.Rows[rcount][0].ToString();
            string depotcode = dt_tot.Rows[rcount][1].ToString();
            string godown = dt_tot.Rows[rcount][2].ToString();
            string soa = dt_tot.Rows[rcount][4].ToString();
            string comm = dt_tot.Rows[rcount][6].ToString();
            string scheme = dt_tot.Rows[rcount][8].ToString();
            decimal current_bal = System.Math.Round(CheckNull(dt_tot.Rows[rcount][28].ToString()), 5);
            int current_bags = CheckNullInt(dt_tot.Rows[rcount][29].ToString());
            string str1 = "update  issue_opening_balance set Current_Balance=" + current_bal + ",Current_Bags=" + current_bags + " where District_Id='" + distcode + "' and  Depotid='" + depotcode + "' and Commodity_Id='" + comm + "' and Scheme_Id='" + scheme + "' and Godown='" + godown + "' and Source='" + soa +"'";
            cmd.CommandText = str1;
            cmd.Connection = con;
            cmd.ExecuteNonQuery(); 
            rcount = rcount + 1;
         }
        con.Close();
        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from do_fps where delivery_order_no='" + TextBox1.Text.Trim() + "' order by fps_code", con);
            da.Fill(dt1);
            int rcount = 0;
            con.Open();
            while (rcount < dt1.Rows.Count)
            {
                int trnscnt = 0;
                string docount = "";
                string distcode = dt1.Rows[rcount][2].ToString();
                string dono = dt1.Rows[rcount][1].ToString();
                string isssuecode = dt1.Rows[rcount][3].ToString();
                string strqr = "select count(delivery_order_no) as rwcount  from dbo.Issued_do_fps  where delivery_order_no='" + dono + "' and district_code='" + distcode + "'";
                cmd.CommandText = strqr;
                cmd.Connection = con;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    docount = dr["rwcount"].ToString();
                }
                dr.Close();
                if (docount != "")
                {
                    trnscnt = CheckNullInt(docount);
                }
                trnscnt = trnscnt + 1;
                string transid = distcode.ToString() + dono.ToString() + (trnscnt).ToString();
                string str1 = "INSERT INTO dbo.Issued_do_fps(trans_id,delivery_order_no,district_code,issueCentre_code,allotment_month,allotment_year,fps_code,commodity,scheme_id,issue_qty,lift_qty,trans_date,ip_add)VALUES('" + transid + "','" + dono + "','" + distcode + "','" + isssuecode + "'," + dt1.Rows[rcount][4] + "," + dt1.Rows[rcount][5] + ",'" + dt1.Rows[rcount][7] + "','" + dt1.Rows[rcount][8] + "','" + dt1.Rows[rcount][9] + "'," + dt1.Rows[rcount][11] + "," + dt1.Rows[rcount][11] + ",getdate(),'117.199.21.119')";
                cmd.CommandText = str1;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                rcount = rcount + 1;
            }
            GridView1.DataSource = dt1;
            GridView1.DataBind();
            con.Close();
            TextBox1.Text = "";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
        }
        else
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter DONO ...');</script>");
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            string str1 = "update dbo.delivery_order_mpscsc set status='Y' where delivery_order_no='"+ TextBox1.Text.Trim() +"'";
            cmd.CommandText = str1;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();           
            con.Close();
            TextBox1.Text = "";
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
        }
        else
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Please enter DONO ...');</script>");
        }
    }
    void GetDepot()
    {
        string str1 = "select * from tbl_MetaData_DEPOT order by DepotName";
        cmd.CommandText = str1;
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lst = new ListItem();
            lst.Text  = dr["DepotName"].ToString();
            lst.Value = dr["DepotId"].ToString();
            ddlissue.Items.Add(lst);
        }
        ddlissue.Items.Insert(0, "Select");
        con.Close();
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        if (ddlissue.SelectedItem.Text == "Select")
        {
            Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Select Depot ...');</script>");
        }
        else
        {
            string depotcode = ddlissue.SelectedItem.Value;
            DataTable dt_tot = new DataTable();
            dt_tot.Columns.Add("District_Id");
            dt_tot.Columns.Add("Depotid");
            dt_tot.Columns.Add("Godown");
            dt_tot.Columns.Add("Godown_Name");
            dt_tot.Columns.Add("Source");
            dt_tot.Columns.Add("Source_Name");
            dt_tot.Columns.Add("Commodity_Id");
            dt_tot.Columns.Add("Commodity_Name");
            dt_tot.Columns.Add("Scheme_Id");
            dt_tot.Columns.Add("Scheme_Name");
            dt_tot.Columns.Add("Ope_Bal");
            dt_tot.Columns.Add("Ope_bags");
            dt_tot.Columns.Add("Rec_qty");
            dt_tot.Columns.Add("Rec_bags");
            dt_tot.Columns.Add("Rec_qty_strans");
            dt_tot.Columns.Add("Rec_bags_strans");
            dt_tot.Columns.Add("Lift_qty_DO");
            dt_tot.Columns.Add("Lift_bags_DO");
            dt_tot.Columns.Add("Lift_qty_TC");
            dt_tot.Columns.Add("Lift_bags_TC");
            dt_tot.Columns.Add("Lift_qty_strans");
            dt_tot.Columns.Add("Lift_bags_strans");
            dt_tot.Columns.Add("Rec_qty_Gain");
            dt_tot.Columns.Add("Rec_bags_Gain");
            dt_tot.Columns.Add("Tot_Rec_Qty");
            dt_tot.Columns.Add("Tot_Lift_Qty");
            dt_tot.Columns.Add("Tot_Rec_Bags");
            dt_tot.Columns.Add("Tot_Lift_Bags");
            dt_tot.Columns.Add("Current_Balance");
            dt_tot.Columns.Add("Current_Bags");
            dt_tot.Columns.Add("Rec_qty_PRC");
            dt_tot.Columns.Add("Rec_bags_PRC");
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT issue_opening_balance.District_Id, issue_opening_balance.Depotid, issue_opening_balance.Godown, tbl_MetaData_GODOWN.Godown_Name, issue_opening_balance.Source, Source_Arrival_Type.Source_Name,  issue_opening_balance.Commodity_Id, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,issue_opening_balance.Scheme_Id, tbl_MetaData_SCHEME.Scheme_Name, convert(decimal(18,5),issue_opening_balance.Quantity) as Ope_Bal,issue_opening_balance.Bags as Ope_Bags,(SELECT sum(convert(decimal(18,5),tbl_Receipt_Details .Recd_Qty)) as recqty  FROM   tbl_Receipt_Details where tbl_Receipt_Details.Depot_ID ='" + depotcode + "' AND  tbl_Receipt_Details.Dist_Id =issue_opening_balance.District_Id AND  tbl_Receipt_Details.Depot_ID =issue_opening_balance.Depotid  AND tbl_Receipt_Details.Godown = issue_opening_balance.Godown AND  tbl_Receipt_Details.S_of_arrival = issue_opening_balance.Source  and tbl_Receipt_Details.Commodity =issue_opening_balance.Commodity_Id AND  tbl_Receipt_Details.Scheme = issue_opening_balance.Scheme_Id  )  as Rec_qty,(SELECT  sum(tbl_Receipt_Details .Recieved_Bags) as recbags FROM   tbl_Receipt_Details where tbl_Receipt_Details.Depot_ID ='" + depotcode + "' AND tbl_Receipt_Details.Dist_Id =issue_opening_balance.District_Id AND  tbl_Receipt_Details.Depot_ID =issue_opening_balance.Depotid  AND tbl_Receipt_Details.Godown = issue_opening_balance.Godown AND  tbl_Receipt_Details.S_of_arrival = issue_opening_balance.Source  and tbl_Receipt_Details.Commodity =issue_opening_balance.Commodity_Id AND  tbl_Receipt_Details.Scheme = issue_opening_balance.Scheme_Id  )  as Rec_bags,(SELECT sum(convert(decimal(18,5),State_Scheme_Transfer .Quantity)) as recqty_strans  FROM   State_Scheme_Transfer where State_Scheme_Transfer.Depotid ='" + depotcode + "' AND State_Scheme_Transfer.D_District =issue_opening_balance.District_Id AND  State_Scheme_Transfer.D_Depot =issue_opening_balance.Depotid  AND State_Scheme_Transfer.Godown = issue_opening_balance.Godown AND  State_Scheme_Transfer.To_Source= issue_opening_balance.Source  and State_Scheme_Transfer.Commodity_Id=issue_opening_balance.Commodity_Id AND  State_Scheme_Transfer.D_Scheme_Id = issue_opening_balance.Scheme_Id  )  as Rec_qty_strans,(SELECT  sum(State_Scheme_Transfer .Bags) as recbags FROM   State_Scheme_Transfer where State_Scheme_Transfer.Depotid ='" + depotcode + "' AND State_Scheme_Transfer.D_District=issue_opening_balance.District_Id AND  State_Scheme_Transfer.D_Depot =issue_opening_balance.Depotid  AND State_Scheme_Transfer.Godown = issue_opening_balance.Godown AND  State_Scheme_Transfer.To_Source = issue_opening_balance.Source  and State_Scheme_Transfer.Commodity_Id =issue_opening_balance.Commodity_Id AND  State_Scheme_Transfer.D_Scheme_Id = issue_opening_balance.Scheme_Id  )  as Rec_bags_strans,(SELECT sum(convert(decimal(18,5),issue_against_do .qty_issue)) as liftqty_do  FROM   issue_against_do where issue_against_do.delivery_order_no in (SELECT delivery_order_mpscsc.delivery_order_no  FROM   delivery_order_mpscsc where delivery_order_mpscsc.issueCentre_code ='" + depotcode + "' AND delivery_order_mpscsc.district_code =issue_opening_balance.District_Id AND  delivery_order_mpscsc.issueCentre_code =issue_opening_balance.Depotid  and delivery_order_mpscsc.commodity_id=issue_opening_balance.Commodity_Id AND  delivery_order_mpscsc.scheme_id = issue_opening_balance.Scheme_Id ) and  issue_against_do.district_code=issue_opening_balance.District_Id AND  issue_against_do.issueCentre_code =issue_opening_balance.Depotid  AND issue_against_do.Godown = issue_opening_balance.Godown AND  issue_against_do.Source= issue_opening_balance.Source ) as Lift_qty_DO,(SELECT sum(issue_against_do .bags) as liftbags_do  FROM   issue_against_do where issue_against_do.delivery_order_no in (SELECT delivery_order_mpscsc.delivery_order_no  FROM   delivery_order_mpscsc where delivery_order_mpscsc.issueCentre_code ='" + depotcode + "' AND delivery_order_mpscsc.district_code =issue_opening_balance.District_Id AND  delivery_order_mpscsc.issueCentre_code =issue_opening_balance.Depotid  and delivery_order_mpscsc.commodity_id=issue_opening_balance.Commodity_Id AND  delivery_order_mpscsc.scheme_id = issue_opening_balance.Scheme_Id ) and  issue_against_do.district_code=issue_opening_balance.District_Id AND  issue_against_do.issueCentre_code =issue_opening_balance.Depotid  AND issue_against_do.Godown = issue_opening_balance.Godown AND  issue_against_do.Source= issue_opening_balance.Source ) as Lift_bags_DO,(SELECT sum(convert(decimal(18,5),SCSC_Truck_challan .Qty_send)) as liftqty_tc  FROM   SCSC_Truck_challan where SCSC_Truck_challan.Depot_Id ='" + depotcode + "'  AND SCSC_Truck_challan.Dist_ID =issue_opening_balance.District_Id AND  SCSC_Truck_challan.Depot_Id=issue_opening_balance.Depotid  AND SCSC_Truck_challan.Dispatch_Godown = issue_opening_balance.Godown AND  SCSC_Truck_challan.Source= issue_opening_balance.Source  and SCSC_Truck_challan.Commodity=issue_opening_balance.Commodity_Id AND  SCSC_Truck_challan.Scheme= issue_opening_balance.Scheme_Id  )  as Lift_qty_TC,(SELECT  sum(SCSC_Truck_challan .Bags) as liftbags_do FROM   SCSC_Truck_challan where SCSC_Truck_challan.Depot_Id ='" + depotcode + "'  AND SCSC_Truck_challan.Dist_ID=issue_opening_balance.District_Id AND  SCSC_Truck_challan.Depot_Id =issue_opening_balance.Depotid  AND SCSC_Truck_challan.Dispatch_Godown= issue_opening_balance.Godown AND  SCSC_Truck_challan.Source = issue_opening_balance.Source  and SCSC_Truck_challan.Commodity =issue_opening_balance.Commodity_Id AND  SCSC_Truck_challan.Scheme= issue_opening_balance.Scheme_Id  )  as Lift_bags_TC,(SELECT sum(convert(decimal(18,5),State_Scheme_Transfer .Quantity)) as liftqty_strans  FROM   State_Scheme_Transfer where State_Scheme_Transfer.Depotid ='" + depotcode + "' AND  State_Scheme_Transfer.District_Id =issue_opening_balance.District_Id AND  State_Scheme_Transfer.Depotid =issue_opening_balance.Depotid  AND State_Scheme_Transfer.Godown = issue_opening_balance.Godown AND  State_Scheme_Transfer.From_Source= issue_opening_balance.Source  and State_Scheme_Transfer.Commodity_Id=issue_opening_balance.Commodity_Id AND  State_Scheme_Transfer.S_Scheme_Id= issue_opening_balance.Scheme_Id  )  as Lift_qty_strans, (SELECT  sum(State_Scheme_Transfer .Bags) as liftbags FROM   State_Scheme_Transfer where  State_Scheme_Transfer.Depotid ='" + depotcode + "' AND State_Scheme_Transfer.District_Id=issue_opening_balance.District_Id AND  State_Scheme_Transfer.Depotid =issue_opening_balance.Depotid  AND State_Scheme_Transfer.Godown = issue_opening_balance.Godown AND  State_Scheme_Transfer.From_Source = issue_opening_balance.Source  and State_Scheme_Transfer.Commodity_Id =issue_opening_balance.Commodity_Id AND  State_Scheme_Transfer.S_Scheme_Id = issue_opening_balance.Scheme_Id  )  as Lift_bags_strans,(SELECT sum(convert(decimal(18,5),Loss_gain .Quantity)) as recqty_LG FROM   Loss_gain where Loss_gain.Depotid ='" + depotcode + "' and Loss_gain.District_Id =issue_opening_balance.District_Id AND  Loss_gain.Depotid =issue_opening_balance.Depotid  AND Loss_gain.Godown = issue_opening_balance.Godown AND  Loss_gain.Source= issue_opening_balance.Source  and Loss_gain.Commodity_Id=issue_opening_balance.Commodity_Id AND  Loss_gain.Scheme_Id= issue_opening_balance.Scheme_Id and Loss_gain.stock_type='G')  as Rec_qty_LG, (SELECT  sum(Loss_gain .Bags) as recbagslg FROM   Loss_gain where Loss_gain.Depotid ='" + depotcode + "' and Loss_gain.District_Id=issue_opening_balance.District_Id AND  Loss_gain.Depotid=issue_opening_balance.Depotid  AND Loss_gain.Godown = issue_opening_balance.Godown AND  Loss_gain.Source = issue_opening_balance.Source  and Loss_gain.Commodity_Id =issue_opening_balance.Commodity_Id AND  Loss_gain.Scheme_Id = issue_opening_balance.Scheme_Id and Loss_gain.stock_type='G' )  as Rec_bags_LG  ,(SELECT  sum(convert(decimal(18,5),SCSC_Procurement .Recd_Qty)) as recqtyprc FROM   SCSC_Procurement where SCSC_Procurement.IssueCenter_ID ='" + depotcode + "' and SCSC_Procurement.Distt_ID=issue_opening_balance.District_Id AND  SCSC_Procurement.IssueCenter_ID =issue_opening_balance.Depotid  AND SCSC_Procurement.Recd_Godown= issue_opening_balance.Godown  and SCSC_Procurement.Commodity_Id=issue_opening_balance.Commodity_Id and issue_opening_balance.Scheme_Id='101' and issue_opening_balance.Source ='01')  as Rec_Qty_PRC,(SELECT  sum(SCSC_Procurement .Recd_Bags) as recqtybags FROM   SCSC_Procurement where SCSC_Procurement.IssueCenter_ID ='" + depotcode + "' and SCSC_Procurement.Distt_ID=issue_opening_balance.District_Id AND  SCSC_Procurement.IssueCenter_ID =issue_opening_balance.Depotid  AND SCSC_Procurement.Recd_Godown= issue_opening_balance.Godown  and SCSC_Procurement.Commodity_Id=issue_opening_balance.Commodity_Id and issue_opening_balance.Scheme_Id='101' and issue_opening_balance.Source ='01')  as Rec_Bags_PRC FROM   issue_opening_balance INNER JOIN tbl_MetaData_GODOWN ON issue_opening_balance.District_Id = tbl_MetaData_GODOWN.DistrictId AND issue_opening_balance.Depotid = tbl_MetaData_GODOWN.DepotId AND issue_opening_balance.Godown = tbl_MetaData_GODOWN.Godown_ID INNER JOIN  Source_Arrival_Type ON issue_opening_balance.Source = Source_Arrival_Type.Source_ID INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON issue_opening_balance.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN  tbl_MetaData_SCHEME ON issue_opening_balance.Scheme_Id = tbl_MetaData_SCHEME.Scheme_Id  where issue_opening_balance.Depotid='" + depotcode + "' order by issue_opening_balance.District_Id,issue_opening_balance.Depotid,issue_opening_balance.Godown,issue_opening_balance.Source,issue_opening_balance.Commodity_Id,issue_opening_balance.Scheme_Id", con);
            da.Fill(dt1);
            int rcount = 0;
            while (rcount < dt1.Rows.Count)
            {
                decimal tot_rec_qty = System.Math.Round(CheckNull(dt1.Rows[rcount][10].ToString()) + CheckNull(dt1.Rows[rcount][12].ToString()) + CheckNull(dt1.Rows[rcount][14].ToString()) + CheckNull(dt1.Rows[rcount][22].ToString()) + CheckNull(dt1.Rows[rcount][24].ToString()), 5);
                decimal tot_lift_qty = System.Math.Round(CheckNull(dt1.Rows[rcount][16].ToString()) + CheckNull(dt1.Rows[rcount][18].ToString()) + CheckNull(dt1.Rows[rcount][20].ToString()), 5);
                int tot_rec_bags = CheckNullInt(dt1.Rows[rcount][11].ToString()) + CheckNullInt(dt1.Rows[rcount][13].ToString()) + CheckNullInt(dt1.Rows[rcount][15].ToString()) + CheckNullInt(dt1.Rows[rcount][23].ToString()) + CheckNullInt(dt1.Rows[rcount][25].ToString());
                int tot_lift_bags = CheckNullInt(dt1.Rows[rcount][17].ToString()) + CheckNullInt(dt1.Rows[rcount][19].ToString()) + CheckNullInt(dt1.Rows[rcount][21].ToString());
                decimal current_bal = System.Math.Round(tot_rec_qty - tot_lift_qty, 5);
                int current_bags = tot_rec_bags - tot_lift_bags;
                dt_tot.Rows.Add(dt1.Rows[rcount][0].ToString(), dt1.Rows[rcount][1].ToString(), dt1.Rows[rcount][2].ToString(), dt1.Rows[rcount][3].ToString(), dt1.Rows[rcount][4].ToString(), dt1.Rows[rcount][5].ToString(), dt1.Rows[rcount][6].ToString(), dt1.Rows[rcount][7].ToString(), dt1.Rows[rcount][8].ToString(), dt1.Rows[rcount][9].ToString(), dt1.Rows[rcount][10].ToString(), dt1.Rows[rcount][11].ToString(), dt1.Rows[rcount][12].ToString(), dt1.Rows[rcount][13].ToString(), dt1.Rows[rcount][14].ToString(), dt1.Rows[rcount][15].ToString(), dt1.Rows[rcount][16].ToString(), dt1.Rows[rcount][17].ToString(), dt1.Rows[rcount][18].ToString(), dt1.Rows[rcount][19].ToString(), dt1.Rows[rcount][20].ToString(), dt1.Rows[rcount][21].ToString(), dt1.Rows[rcount][22].ToString(), dt1.Rows[rcount][23].ToString(), tot_rec_qty.ToString(), tot_lift_qty.ToString(), tot_rec_bags.ToString(), tot_lift_bags.ToString(), current_bal.ToString(), current_bags.ToString(), dt1.Rows[rcount][24].ToString(), dt1.Rows[rcount][25].ToString());
                rcount = rcount + 1;
            }
            GridView1.DataSource = dt_tot;
            GridView1.DataBind();
            Session["dt_tot"] = dt_tot;
        }
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        DataTable dt_tot = (DataTable)Session["dt_tot"];
        int rcount = 0;
        con.Open();
        while (rcount < dt_tot.Rows.Count)
        {
            string distcode = dt_tot.Rows[rcount][0].ToString();
            string depotcode = dt_tot.Rows[rcount][1].ToString();
            string godown = dt_tot.Rows[rcount][2].ToString();
            string soa = dt_tot.Rows[rcount][4].ToString();
            string comm = dt_tot.Rows[rcount][6].ToString();
            string scheme = dt_tot.Rows[rcount][8].ToString();
            decimal current_bal = System.Math.Round(CheckNull(dt_tot.Rows[rcount][28].ToString()), 5);
            int current_bags = CheckNullInt(dt_tot.Rows[rcount][29].ToString());
            string str1 = "update  issue_opening_balance set Current_Balance=" + current_bal + ",Current_Bags=" + current_bags + " where District_Id='" + distcode + "' and  Depotid='" + depotcode + "' and Commodity_Id='" + comm + "' and Scheme_Id='" + scheme + "' and Godown='" + godown + "' and Source='" + soa + "'";
            cmd.CommandText = str1;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            rcount = rcount + 1;
        }
        con.Close();
        Page.RegisterClientScriptBlock("asdsad", "<script language=javascript > alert('Data Saved Successfully ...');</script>");
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
}