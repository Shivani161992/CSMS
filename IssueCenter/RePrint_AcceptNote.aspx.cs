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

public partial class IssueCenter_RePrint_AcceptNote : System.Web.UI.Page
{
    public SqlConnection con_paddy = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_PPMS2014"].ToString());
    public SqlConnection con_Maze = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_MPMS2014"].ToString());
    public SqlConnection con_WPMS = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2014"].ToString());

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

    MoveChallan mobj1 = null;
    protected Common ComObj = null, cmn = null;
    public static string distname = "";
    public static string dist = "";
    public string sid = "";
    public string ssid = "";
    public static string dname = "";
    public string snid = "";
    public static string distid = "";
    public string dipotid = "";
    public string cdate = "";
    public static string issueid = "";

    int totalSB;

    double totalSQ;

    int totalRB;

    double totalRQ;

    int totalRjB;

    double totalRjQ;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }

                if (!IsPostBack)
                {
                    issueid = Session["issue_id"].ToString();
                    dist = Session["dist_id"].ToString();

                    lblSessionDist.Text = Session["dist_id"].ToString();
                    lblSessionDepot.Text = Session["issue_id"].ToString();
                    
                    string qrysel = "select crop,crpcode from Crop_Master where crpcode not in ('8')";

                    SqlDataAdapter da = new SqlDataAdapter(qrysel, con_paddy);

                    DataSet ds1 = new DataSet();
                    da.Fill(ds1);
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            ddlcrop.DataSource = ds1.Tables[0];
                            ddlcrop.DataTextField = "crop";
                            ddlcrop.DataValueField = "crpcode";
                            ddlcrop.DataBind();
                            ddlcrop.Items.Insert(0, "--Select--");

                        }
                    }
                    else
                    {

                    }

                    string qrey = "select DistrictId,DepotName  from dbo.tbl_MetaData_DEPOT where DepotID='" + lblSessionDepot.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(qrey, con);

                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    DataSet ds = new DataSet();

                    da1.Fill(ds);

                    DataRow dr = ds.Tables[0].Rows[0];
                    distid = dr["DistrictId"].ToString();
                    dname = dr["DepotName"].ToString();
                    //dist = distid.Substring(2, 2);

                    string qrey2 = "select district_name from pds.districtsmp where district_code='" + lblSessionDist.Text + "'";

                    SqlCommand cmddist = new SqlCommand(qrey2, con);

                    SqlDataAdapter dadist = new SqlDataAdapter(cmddist);

                    DataSet dsdist = new DataSet();

                    dadist.Fill(dsdist);


                    DataRow dr2 = dsdist.Tables[0].Rows[0];
                    distname = dr2["district_name"].ToString();

                    
                    lbldepot.Text = dname;
                    lbldistt.Text = distname;

                }
                
            if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

             else
                  {
                     Response.Redirect("~/MainLogin.aspx");
                 }

        }
   
    protected void ddlcrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        

        if (ddlcrop.SelectedValue == "0" || ddlcrop.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            lblgno.Text ="";
            string daten = "";
            string gdaten = "";
            lblgdtae.Text = "";

            grd_viewDepot.DataSource = "";
            grd_viewDepot.DataBind();
            lbldepon.Text = "";


            lblsenddist.Text = "";
            lblpccenter.Text = "";
            cdate = "";
            string gdate = "";


            lblcrop.Text = "";
            lblcomdty.Text = "";

            LblStechingGood.Text = "";
            LblStechingBad.Text = "";
            LblStencileGood.Text = "";

            LblStencileBad.Text = "";


            lblmoisture.Text = "";
            lblwcmno.Text = "";


            if (ddlcrop.SelectedValue == "1" )
            {
                # region wheat

                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }


                Session["Commodity_Id"] = ddlcrop.SelectedValue;

                string qry = "Select distinct Acceptance_No from Acceptance_Note_Detail where Distt_ID = '23" + lblSessionDist.Text + "' and IssueCenter_ID = '" + lblSessionDepot.Text + "' order by Acceptance_No";

                SqlCommand cmdAcc = new SqlCommand(qry, con_WPMS);

                SqlDataAdapter daAcc = new SqlDataAdapter(cmdAcc);

                DataSet dsAcc = new DataSet();
                daAcc.Fill(dsAcc);

                if (dsAcc != null)
                {
                    if (dsAcc.Tables[0].Rows.Count > 0)
                    {
                        ddlAccptNumber.DataSource = dsAcc.Tables[0];
                        ddlAccptNumber.DataTextField = "Acceptance_No";

                        ddlAccptNumber.DataBind();
                        ddlAccptNumber.Items.Insert(0, "--Select--");

                    }

                    else
                    {
                        ddlAccptNumber.DataSource = "";


                        ddlAccptNumber.DataBind();
                        ddlAccptNumber.Items.Insert(0, "--Select--");
                    }
                }
                else
                {

                }

                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }

                # endregion
            }

            else if (ddlcrop.SelectedValue == "2" || ddlcrop.SelectedValue == "3")
            {
                # region paddy

                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }


                Session["Commodity_Id"] = ddlcrop.SelectedValue;

                string qry = "Select distinct Acceptance_No from Acceptance_Note_Detail where Distt_ID = '23" + lblSessionDist.Text + "' and IssueCenter_ID = '" + lblSessionDepot.Text + "' order by Acceptance_No";

                SqlCommand cmdAcc = new SqlCommand(qry, con_paddy);

                SqlDataAdapter daAcc = new SqlDataAdapter(cmdAcc);

                DataSet dsAcc = new DataSet();
                daAcc.Fill(dsAcc);

                if (dsAcc != null)
                {
                    if (dsAcc.Tables[0].Rows.Count > 0)
                    {
                        ddlAccptNumber.DataSource = dsAcc.Tables[0];
                        ddlAccptNumber.DataTextField = "Acceptance_No";

                        ddlAccptNumber.DataBind();
                        ddlAccptNumber.Items.Insert(0, "--Select--");

                    }

                    else
                    {
                        ddlAccptNumber.DataSource = "";


                        ddlAccptNumber.DataBind();
                        ddlAccptNumber.Items.Insert(0, "--Select--");
                    }
                }
                else
                {

                }

                if (con_paddy.State == ConnectionState.Open)
                {
                    con_paddy.Close();
                }




                # endregion
            }

            else if (ddlcrop.SelectedValue == "4" || ddlcrop.SelectedValue == "5" || ddlcrop.SelectedValue == "6" || ddlcrop.SelectedValue == "7" || ddlcrop.SelectedValue == "8")
            {
                # region maize

                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }


                Session["Commodity_Id"] = ddlcrop.SelectedValue;

                string qry = "Select distinct Acceptance_No from Acceptance_Note_Detail where Distt_ID = '23" + lblSessionDist.Text + "' and IssueCenter_ID = '" + lblSessionDepot.Text + "' order by Acceptance_No";

                SqlCommand cmdAcc = new SqlCommand(qry, con_Maze);

                SqlDataAdapter daAcc = new SqlDataAdapter(cmdAcc);

                DataSet dsAcc = new DataSet();
                daAcc.Fill(dsAcc);

                if (dsAcc != null)
                {
                    if (dsAcc.Tables[0].Rows.Count > 0)
                    {
                        ddlAccptNumber.DataSource = dsAcc.Tables[0];
                        ddlAccptNumber.DataTextField = "Acceptance_No";

                        ddlAccptNumber.DataBind();
                        ddlAccptNumber.Items.Insert(0, "--Select--");

                    }

                    else
                    {
                        ddlAccptNumber.DataSource = "";
                       

                        ddlAccptNumber.DataBind();
                        ddlAccptNumber.Items.Insert(0, "--Select--");
                    }
                }
                else
                {

                }

                if (con_Maze.State == ConnectionState.Open)
                {
                    con_Maze.Close();
                }



                # endregion
            }


            
        }

        
        
    }

    protected void ddlAccptNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccptNumber.SelectedItem.Text == "--Select--")
        {

        }

        else
        {
            GetDistt();
        }
    }

    void GetDistt()
    {
        

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        

        lblgno.Text = sid;
        string daten = DateTime.Now.ToString();
        string gdaten = getdate(daten);
        lblgdtae.Text = gdaten;

        # region Wheat

        
        if (Session["Commodity_Id"].ToString() == "1")
        {
            try
            {
                if (con_WPMS.State == ConnectionState.Closed)
                {
                    con_WPMS.Open();
                }

                string str = "SELECT IssueCenterReceipt_Online.*,tbl_MPWLC_Godown_Storage.GodownNO,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name , isnull(Acceptance_Note_Detail.Stiching_bags_Good,0)Stiching_bags_Good ,isnull(Acceptance_Note_Detail.Stiching_bags_Bad,0)Stiching_bags_Bad , isnull(Acceptance_Note_Detail.Stencile_bags_Good,0)Stencile_bags_Good ,ISNULL(Acceptance_Note_Detail.Stencile_bags_Bad,0)Stencile_bags_Bad  FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo and Acceptance_Note_Detail.IssueID = IssueCenterReceipt_Online.IssueID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID  left join tbl_MPWLC_Godown_Storage on IssueCenterReceipt_Online.Recd_Godown = tbl_MPWLC_Godown_Storage.DepotGodownID where IssueCenterReceipt_Online.DistrictId='23" + lblSessionDist.Text + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + lblSessionDepot.Text + "' and Acceptance_Note_Detail.Acceptance_No= '" + ddlAccptNumber.SelectedItem.Text + "'";

                SqlDataAdapter daP = new SqlDataAdapter(str, con_WPMS);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);
                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();
                lbldepon.Text = dname;

                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                string gdate = getdateM(cdate);

                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                lblcomdty.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();

                //lblgodown.Text = dsP.Tables[0].Rows[0]["GodownNO"].ToString();


                LblStechingGood.Text = dsP.Tables[0].Rows[0]["Stiching_bags_Good"].ToString();

                LblStechingBad.Text = dsP.Tables[0].Rows[0]["Stiching_bags_Bad"].ToString();

                LblStencileGood.Text = dsP.Tables[0].Rows[0]["Stencile_bags_Good"].ToString();

                LblStencileBad.Text = dsP.Tables[0].Rows[0]["Stencile_bags_Bad"].ToString();

                lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());
                lblwcmno.Text = dsP.Tables[0].Rows[0]["Acceptance_No"].ToString(); ;
            }
            catch (Exception ex)
            {
                lbleror.Visible = true;
                lbleror.Text = ex.Message;

                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }

            }
            finally
            {

                if (con_WPMS.State == ConnectionState.Open)
                {
                    con_WPMS.Close();
                }
            }
        }
        # endregion

        # region Paddy
        else if (Session["Commodity_Id"].ToString() == "2" || Session["Commodity_Id"].ToString() == "3")
        {
            try
            {
                if (con_paddy.State == ConnectionState.Closed)
                {
                    con_paddy.Open();
                }

                string str = "SELECT IssueCenterReceipt_Online.*,tbl_MPWLC_Godown_Storage.GodownNO,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name , isnull(Acceptance_Note_Detail.Stiching_bags_Good,0)Stiching_bags_Good ,isnull(Acceptance_Note_Detail.Stiching_bags_Bad,0)Stiching_bags_Bad , isnull(Acceptance_Note_Detail.Stencile_bags_Good,0)Stencile_bags_Good ,ISNULL(Acceptance_Note_Detail.Stencile_bags_Bad,0)Stencile_bags_Bad  FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo and Acceptance_Note_Detail.IssueID = IssueCenterReceipt_Online.IssueID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID  left join tbl_MPWLC_Godown_Storage on IssueCenterReceipt_Online.Recd_Godown = tbl_MPWLC_Godown_Storage.DepotGodownID where IssueCenterReceipt_Online.DistrictId='23" + lblSessionDist.Text + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + lblSessionDepot.Text + "' and Acceptance_Note_Detail.Acceptance_No= '" + ddlAccptNumber.SelectedItem.Text + "'";

                //string str = " SELECT IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No = '" + ddlAccptNumber.SelectedItem.Text + "'";
                
                
                SqlDataAdapter daP = new SqlDataAdapter(str, con_paddy);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);
                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();
                lbldepon.Text = dname;


                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                string gdate = getdateM(cdate);


                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                lblcomdty.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();


                LblStechingGood.Text = dsP.Tables[0].Rows[0]["Stiching_bags_Good"].ToString();

                LblStechingBad.Text = dsP.Tables[0].Rows[0]["Stiching_bags_Bad"].ToString();

                LblStencileGood.Text = dsP.Tables[0].Rows[0]["Stencile_bags_Good"].ToString();

                LblStencileBad.Text = dsP.Tables[0].Rows[0]["Stencile_bags_Bad"].ToString();


                lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());
                lblwcmno.Text = dsP.Tables[0].Rows[0]["Acceptance_No"].ToString(); ;
            }
            catch (Exception ex)
            {
                lbleror.Visible = true;
                lbleror.Text = ex.Message;
                lbleror.ForeColor = System.Drawing.Color.Red;

                if (con_paddy.State == ConnectionState.Open)
                {
                    con_paddy.Close();
                }

            }
            finally
            {
                if (con_paddy.State == ConnectionState.Open)
                {
                    con_paddy.Close();
                }

               
            }
        }

        # endregion

        # region Maize
        else if (Session["Commodity_Id"].ToString() == "4" || Session["Commodity_Id"].ToString() == "5" || Session["Commodity_Id"].ToString() == "6" || Session["Commodity_Id"].ToString() == "7" || Session["Commodity_Id"].ToString() == "8")
        {
            try
            {
                if (con_Maze.State == ConnectionState.Closed)
                {
                    con_Maze.Open();
                }

                string str = "SELECT IssueCenterReceipt_Online.*,tbl_MPWLC_Godown_Storage.GodownNO,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name , isnull(Acceptance_Note_Detail.Stiching_bags_Good,0)Stiching_bags_Good ,isnull(Acceptance_Note_Detail.Stiching_bags_Bad,0)Stiching_bags_Bad , isnull(Acceptance_Note_Detail.Stencile_bags_Good,0)Stencile_bags_Good ,ISNULL(Acceptance_Note_Detail.Stencile_bags_Bad,0)Stencile_bags_Bad  FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo and Acceptance_Note_Detail.IssueID = IssueCenterReceipt_Online.IssueID left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID  left join tbl_MPWLC_Godown_Storage on IssueCenterReceipt_Online.Recd_Godown = tbl_MPWLC_Godown_Storage.DepotGodownID where IssueCenterReceipt_Online.DistrictId='23" + lblSessionDist.Text + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + lblSessionDepot.Text + "' and Acceptance_Note_Detail.Acceptance_No= '" + ddlAccptNumber.SelectedItem.Text + "'";

                //string str = " SELECT IssueCenterReceipt_Online.*,CONVERT(varchar,IssueCenterReceipt_Online.DateOfIssue,106)as DateOfIssue1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,Acceptance_Note_Detail.Acceptance_No,Acceptance_Note_Detail.Acceptance_Date,Crop_Master.crop as Commodity_Name,TransportMaster.Transporter_Name,Districts.District_Name,(Society.Society_Name+','+Society.SocPlace)as Society_Name FROM IssueCenterReceipt_Online left join Crop_Master on Crop_Master.crpcode = IssueCenterReceipt_Online.CommodityId left join TransportMaster on TransportMaster.Transporter_ID=IssueCenterReceipt_Online.TransporterId and TransportMaster.SocietyCode = IssueCenterReceipt_Online.SocietyID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = IssueCenterReceipt_Online.TruckChalanNo left join Districts on Districts.District_Code = IssueCenterReceipt_Online.Sending_District left join Society on Society.Society_Id= IssueCenterReceipt_Online.SocietyID where IssueCenterReceipt_Online.DistrictId='" + distid + "' and IssueCenterReceipt_Online.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No= '" + ddlAccptNumber.SelectedItem.Text + "'";
               
                SqlDataAdapter daP = new SqlDataAdapter(str, con_Maze);
                DataSet dsP = new DataSet();
                daP.Fill(dsP);
                grd_viewDepot.DataSource = dsP.Tables[0];
                grd_viewDepot.DataBind();
                lbldepon.Text = dname;

                lblsenddist.Text = dsP.Tables[0].Rows[0]["District_Name"].ToString();
                lblpccenter.Text = dsP.Tables[0].Rows[0]["Society_Name"].ToString();
                cdate = dsP.Tables[0].Rows[0]["DateOfIssue"].ToString();
                string gdate = getdateM(cdate);

                lblcrop.Text = dsP.Tables[0].Rows[0]["CropYear"].ToString();
                lblcomdty.Text = dsP.Tables[0].Rows[0]["Commodity_Name"].ToString();


                LblStechingGood.Text = dsP.Tables[0].Rows[0]["Stiching_bags_Good"].ToString();

                LblStechingBad.Text = dsP.Tables[0].Rows[0]["Stiching_bags_Bad"].ToString();

                LblStencileGood.Text = dsP.Tables[0].Rows[0]["Stencile_bags_Good"].ToString();

                LblStencileBad.Text = dsP.Tables[0].Rows[0]["Stencile_bags_Bad"].ToString();


                lblmoisture.Text = getdateM(dsP.Tables[0].Rows[0]["Acceptance_Date"].ToString());
                lblwcmno.Text = dsP.Tables[0].Rows[0]["Acceptance_No"].ToString();
            }
            catch (Exception ex)
            {
                lbleror.Visible = true;
                lbleror.Text = ex.Message;


                if (con_Maze.State == ConnectionState.Open)
                {
                    con_Maze.Close();
                }

            }
            finally
            {

                if (con_Maze.State == ConnectionState.Open)
                {
                    con_Maze.Close();
                }

            }
        }

        # endregion

        # region Others
        else
        {

            mobj1 = new MoveChallan(ComObj);
            //string qrey4 = "select * from dbo.SCSC_Procurement where IssueCenter_ID='" + issueid + "' and GatePass_id='" + sid + "'";
            string qrey4 = "SELECT SCSC_Procurement.*,(SCSC_Procurement.No_of_Bags)as Bags,(SCSC_Procurement.Quantity)as Quantity,(SCSC_Procurement.TC_Number)as TruckChalanNo,(SCSC_Procurement.Truck_Number)as TruckNo ,CONVERT(varchar,SCSC_Procurement.Dispatch_Date,106)as DateOfIssue1,tbl_MetaData_STORAGE_COMMODITY .Commodity_Name as Commodity_Name,(Acceptance_Note_Detail.Acceptance_No)as Acceptance_No1,Acceptance_Note_Detail.Bags as Acc_Bag,Acceptance_Note_Detail.Acceptance_Date,Acceptance_Note_Detail.Accept_Qty,Acceptance_Note_Detail.Reject_Qty,tbl_MetaData_Purchase_Center.PurchaseCenterName,Transporter_Table.Transporter_Name as Transporter_Name,tbl_MetaData_DEPOT.DepotName as DepotName,districtsmp.district_name as district_name   FROM dbo.SCSC_Procurement left join dbo.tbl_MetaData_STORAGE_COMMODITY on SCSC_Procurement.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY .Commodity_Id left join dbo.Transporter_Table on SCSC_Procurement.Transporter_ID =Transporter_Table.Transporter_ID left join pds.districtsmp on SCSC_Procurement.Sending_District=districtsmp.district_code left join dbo.tbl_MetaData_DEPOT on SCSC_Procurement.Purchase_Center=tbl_MetaData_DEPOT.DepotID left join Acceptance_Note_Detail on Acceptance_Note_Detail.TC_Number = SCSC_Procurement.TC_Number left join tbl_MetaData_Purchase_Center on tbl_MetaData_Purchase_Center.PcId = SCSC_Procurement.Purchase_Center where SCSC_Procurement.Distt_ID='" + dist + "' and  SCSC_Procurement.IssueCenter_ID='" + issueid + "' and Acceptance_Note_Detail.Acceptance_No='" + sid + "'";

            DataSet ds4 = mobj1.selectAny(qrey4);
            DataRow dr4 = ds4.Tables[0].Rows[0];
            grd_viewDepot.DataSource = ds4.Tables[0];
            grd_viewDepot.DataBind();
            lbldepon.Text = dname;
            //lblchallanno.Text = dr4["TC_Number"].ToString();
            lblsenddist.Text = dr4["district_name"].ToString();
            lblpccenter.Text = dr4["PurchaseCenterName"].ToString();
            //lblchallandt.Text = dr4["challan_date"].ToString();
            cdate = dr4["Dispatch_Date"].ToString();
            string gdate = getdate(cdate);
            //lblchallandt.Text = gdate;
            lblcomdty.Text = dr4["Commodity_Name"].ToString();

            //lblcrop.Text = ds4.Tables[0].Rows[0]["CropYear"].ToString();

            lblwcmno.Text = dr4["Acceptance_No1"].ToString();
            lblmoisture.Text = getdateM(dr4["Acceptance_Date"].ToString());
        }

        # endregion
        //con

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        
    }

    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy-hh:mm tt");
    }
    public string getdateM(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MMM/yyyy");
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void grd_viewDepot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            float Acce = 0;

            float Qtytrans = 0;

           string Acce1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Accept_Qty"));

           string Qtytrans1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "QtyTransffer"));

           double diff1 = Convert.ToDouble(Qtytrans1) - Convert.ToDouble(Acce1);

           //Math.Round(inputValue, 2);

           double diff = Math.Round(diff1, 2);

            if (diff < 0)
            {
                diff = 0;
            }

            double NetAmt = diff;

            Label TxtQtyAmt = (Label)e.Row.FindControl("TxtNetQ");

            TxtQtyAmt.Text = NetAmt.ToString();


            Int32 RBgs = 0;

            Int32 Rbgs = 0;

            Rbgs = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bags"));

            RBgs = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Acc_Bag"));

            Int32 diffBgs = Rbgs - RBgs;


            if (diffBgs < 0)
            {
                diffBgs = 0;
            }

            Int32 NetRBgs = diffBgs;

            Label TxtRbags = (Label)e.Row.FindControl("TxtNetAmt");

            TxtRbags.Text = NetRBgs.ToString();




            totalSB += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bags"));

            totalSQ += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "QtyTransffer"));

            totalRB += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Acc_Bag"));
            totalRQ += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Accept_Qty"));

            totalRjB += NetRBgs;
            totalRjQ += NetAmt;        
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmount1 = (Label)e.Row.FindControl("lbl_SB");

            lblAmount1.Text = totalSB.ToString();

            Label lblqty = (Label)e.Row.FindControl("lbl_SQty");

            lblqty.Text = totalSQ.ToString();


            Label lbl_AB = (Label)e.Row.FindControl("lbl_AB");

            lbl_AB.Text = totalRB.ToString();


            Label lbl_AQ = (Label)e.Row.FindControl("lbl_AQ");

            lbl_AQ.Text = totalRQ.ToString();

            Label lbl_RjB = (Label)e.Row.FindControl("lbl_RjB");

            lbl_RjB.Text = totalRjB.ToString();


            Label lbl_RjQ = (Label)e.Row.FindControl("lbl_RjQ");

            lbl_RjQ.Text = totalRjQ.ToString();  

        }   

    }
}
