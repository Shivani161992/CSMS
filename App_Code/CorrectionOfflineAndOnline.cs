using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
/// <summary>
/// Summary description for CorrectionOfflineAndOnline
/// </summary>
[WebService(Namespace = "http://tempuri.org/", Name = "CorrectionOfflineAndOnline", Description = "CorrectionOfflineAndOnline Date:15/05/2014")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CorrectionOfflineAndOnline : System.Web.Services.WebService {
    private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con_Proc_online_WPMS2014"].ToString());
    private SqlCommand command;
    public SecuredWebServiceHeader spAHeader;
    public CorrectionOfflineAndOnline () {
         //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public int Pro_Correc_Update_CommodityReceivedFromFarmer(string District_Id, string Society_Id, string Farmer_Id, string ReceivedID, string QtyReceived, string Bags, string TotaAmountPayableToFarmer, string TaulPatrakNo, string AmtAgainstSCCredit, string AmtAgainstBankCredit, string AmtAgIrg_Loan, string NetAmountPayableToFarmer, string Date_Of_Receipt, string Date_Of_Updation, string UserId)
    {

        int res = 0, res1 = 0;
        SqlTransaction trans = null;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                trans = con.BeginTransaction();
                command = new SqlCommand();
                command.Transaction = trans;

                command.CommandType = CommandType.Text;

                command.Connection = con;
                command.CommandText = "";
                command.CommandText = "insert into CommodityReceivedFromFarmer_Log ([ComTransID],[ReceivedID],[District_Id],[Farmer_Id],[Proc_AgID],[Society_Id],[CropYear] ,[CommodityId],[QtyReceived],[Bags],[MarketingSeasonId] ,[TotaAmountPayableToFarmer] ,[TaulPatrakNo],[FarmerLoanFromSc] ,[FarmerLoanFromBank],[AmtAgainstSCCredit],[AmtAgainstBankCredit],[Irrigation_Loan],[AmtAgIrg_Loan],[NetAmountPayableToFarmer],[Date_Of_Receipt],[Date_Of_Creation] ,[Date_Of_Updation],[Status],[UserId])  SELECT [ComTransID],[ReceivedID],[District_Id],[Farmer_Id],[Proc_AgID] ,[Society_Id],[CropYear],[CommodityId] ,[QtyReceived] ,[Bags] ,[MarketingSeasonId] ,[TotaAmountPayableToFarmer] ,[TaulPatrakNo],[FarmerLoanFromSc],[FarmerLoanFromBank],[AmtAgainstSCCredit] ,[AmtAgainstBankCredit] ,[Irrigation_Loan] ,[AmtAgIrg_Loan],[NetAmountPayableToFarmer] ,[Date_Of_Receipt],[Date_Of_Creation],getDate(),[Status],'" + UserId + "' FROM [CommodityReceivedFromFarmer]   where  District_Id='" + District_Id + "' and Society_Id='" + Society_Id + "' and Farmer_Id='" + Farmer_Id + "' and ReceivedID='" + ReceivedID + "'";
                command.CommandTimeout = 0;
                res1 = command.ExecuteNonQuery();


                command.CommandText = "";
                command.CommandText = "UPDATE [CommodityReceivedFromFarmer] SET [QtyReceived] = '" + QtyReceived + "',[Bags] = '" + Bags + "',[TotaAmountPayableToFarmer] = '" + TotaAmountPayableToFarmer + "',[TaulPatrakNo] = '" + TaulPatrakNo + "',[AmtAgainstSCCredit] = '" + AmtAgainstSCCredit + "',[AmtAgainstBankCredit] = '" + AmtAgainstBankCredit + "',[AmtAgIrg_Loan] = '" + AmtAgIrg_Loan + "',[NetAmountPayableToFarmer] = '" + NetAmountPayableToFarmer + "',[Date_Of_Receipt] = '" + getDate_YMd(Date_Of_Receipt) + "',[Date_Of_Updation] = getDate(),[UserId] = '" + UserId + "' where  District_Id='" + District_Id + "' and Society_Id='" + Society_Id + "' and ReceivedID='" + ReceivedID + "'";
                command.CommandTimeout = 0;
                res = command.ExecuteNonQuery();
                trans.Commit();
                command.Dispose();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                trans.Rollback();
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return res;



    }
    [WebMethod]
    public int Pro_Correc_Delete_CommodityReceivedFromFarmer(string District_Id, string Society_Id, string ReceivedID, string localip)
    {
        int res = 0, res1 = 0;
        SqlTransaction trans = null;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                trans = con.BeginTransaction();
                command = new SqlCommand();
                command.Transaction = trans;


                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "";
                command.CommandText = "insert into CommodityReceivedFromFarmer_dtLog ([ComTransID],[ReceivedID],[District_Id],[Farmer_Id],[Proc_AgID],[Society_Id],[CropYear] ,[CommodityId],[QtyReceived],[Bags],[MarketingSeasonId] ,[TotaAmountPayableToFarmer] ,[TaulPatrakNo],[FarmerLoanFromSc] ,[FarmerLoanFromBank],[AmtAgainstSCCredit],[AmtAgainstBankCredit],[Irrigation_Loan],[AmtAgIrg_Loan],[NetAmountPayableToFarmer],[Date_Of_Receipt],[Date_Of_Creation] ,[Date_Of_Updation],[Date_Of_Deletion],[Status],[UserId])  SELECT [ComTransID],[ReceivedID],[District_Id],[Farmer_Id],[Proc_AgID] ,[Society_Id],[CropYear],[CommodityId] ,[QtyReceived] ,[Bags] ,[MarketingSeasonId] ,[TotaAmountPayableToFarmer] ,[TaulPatrakNo],[FarmerLoanFromSc],[FarmerLoanFromBank],[AmtAgainstSCCredit] ,[AmtAgainstBankCredit] ,[Irrigation_Loan] ,[AmtAgIrg_Loan],[NetAmountPayableToFarmer] ,[Date_Of_Receipt],[Date_Of_Creation],[Date_Of_Updation],getDate(),[Status],'" + localip + "' FROM [CommodityReceivedFromFarmer]  where  District_Id='" + District_Id + "' and Society_Id='" + Society_Id + "' and ReceivedID='" + ReceivedID + "'";
                command.CommandTimeout = 0;
                res1 = command.ExecuteNonQuery();

                command.CommandText = "";
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from CommodityReceivedFromFarmer  where  District_Id='" + District_Id + "' and Society_Id='" + Society_Id + "' and ReceivedID='" + ReceivedID + "'";
                res = command.ExecuteNonQuery();
                trans.Commit();
                command.Dispose();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                trans.Rollback();
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return res;

    }
    [WebMethod]
    public int Pro_Correc_Update_IssueToSangrahanaKendra(string District_Id, string Society_Id, string IssueID, string Bags, string QtyTransffer, string DateOfIssue, string TaulPtrakNo, string TransporterId, string TruckChalanNo, string TruckNo, string DriverName,string UpdatedDate, string UpdatedBy, string UserId)
    {
        int res = 0, res1 = 0;
        SqlTransaction trans = null;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
              
                trans = con.BeginTransaction();
                command = new SqlCommand();
                command.Transaction = trans;
               
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "";
                command.CommandText = "insert into [IssueToSangrahanaKendra_log]([TransID],[IssueID],[DistrictId],[Proc_AgID],[SocietyID] ,[PCID],[CropYear],[MarketingSeasonId],[DateOfIssue] ,[IssueTo] ,[SendingDistId] ,[GodownTypeId],[GodownCenterId] ,[GodownName],[GodownNumber] ,[Place],[RailRackOf_ID] ,[RailRack_SendingPlace] ,[RailRack_RecievingPlace] ,[Miller_ID] ,[MillerRepresentative] ,[CommodityId] ,[Bags] ,[QtyTransffer] ,[TaulPtrakNo] ,[TransporterId] ,[TruckChalanNo] ,[TruckNo] ,[DriverName] ,[CreatedDate] ,[CreatedBy] ,[UpdatedDate] ,[UpdatedBy] ,[UserId]  ,[IP] )  SELECT [TransID] ,[IssueID] ,[DistrictId]  ,[Proc_AgID],[SocietyID] ,[PCID],[CropYear] ,[MarketingSeasonId] ,[DateOfIssue] ,[IssueTo] ,[SendingDistId] ,[GodownTypeId],[GodownCenterId] ,[GodownName]  ,[GodownNumber]      ,[Place] ,[RailRackOf_ID] ,[RailRack_SendingPlace]  ,[RailRack_RecievingPlace] ,[Miller_ID] ,[MillerRepresentative] ,[CommodityId]  ,[Bags] ,[QtyTransffer] ,[TaulPtrakNo] ,[TransporterId] ,[TruckChalanNo] ,[TruckNo] ,[DriverName] ,[CreatedDate] ,[CreatedBy] ,[UpdatedDate] ,[UpdatedBy] ,[UserId] ,[IP]  FROM [IssueToSangrahanaKendra]   where  DistrictId='" + District_Id + "' and SocietyId='" + Society_Id + "' and IssueID='" + IssueID + "'";
                command.CommandTimeout = 0;
                res1 = command.ExecuteNonQuery();


                command.CommandText = "";
                command.CommandText = "update IssueToSangrahanaKendra set Bags='" + Bags + "',QtyTransffer='" + QtyTransffer + "' ,DateOfIssue ='" + getDate_YMd(DateOfIssue) + "' ,TaulPtrakNo='" + TaulPtrakNo + "',  TransporterId='" + TransporterId + "',TruckChalanNo='" + TruckChalanNo + "',TruckNo='" + TruckNo + "',DriverName='" + DriverName + "' ,UpdatedDate =getDate(), UpdatedBy='" + UpdatedBy + "' , UserId='" + UserId + "' where  DistrictId='" + District_Id + "' and SocietyId='" + Society_Id + "' and IssueID='" + IssueID + "'";
                command.CommandTimeout = 0;
                res = command.ExecuteNonQuery();
                trans.Commit();
                command.Dispose();



                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                trans.Rollback();
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return res;

    }
    [WebMethod]
    public int Pro_Correc_Delete_IssueToSangrahanaKendra(string District_Id, string Society_Id, string IssueID,string localip)
    {
        int res = 0, res1 = 0;
        SqlTransaction trans = null;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                trans = con.BeginTransaction();
                command = new SqlCommand();
                command.Transaction = trans;
               

                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "";
                command.CommandText = "insert into [Del_IssueToSangrahanaKendra_log]([TransID],[IssueID],[DistrictId],[Proc_AgID],[SocietyID] ,[PCID],[CropYear],[MarketingSeasonId],[DateOfIssue] ,[IssueTo] ,[SendingDistId] ,[GodownTypeId],[GodownCenterId] ,[GodownName],[GodownNumber] ,[Place],[RailRackOf_ID] ,[RailRack_SendingPlace] ,[RailRack_RecievingPlace] ,[Miller_ID] ,[MillerRepresentative] ,[CommodityId] ,[Bags] ,[QtyTransffer] ,[TaulPtrakNo] ,[TransporterId] ,[TruckChalanNo] ,[TruckNo] ,[DriverName] ,[CreatedDate] ,[CreatedBy] ,[deletedDate] ,[UpdatedBy] ,[UserId]  ,[IP] )  SELECT [TransID] ,[IssueID] ,[DistrictId]  ,[Proc_AgID],[SocietyID] ,[PCID],[CropYear] ,[MarketingSeasonId] ,[DateOfIssue] ,[IssueTo] ,[SendingDistId] ,[GodownTypeId],[GodownCenterId] ,[GodownName]  ,[GodownNumber]      ,[Place] ,[RailRackOf_ID] ,[RailRack_SendingPlace]  ,[RailRack_RecievingPlace] ,[Miller_ID] ,[MillerRepresentative] ,[CommodityId]  ,[Bags] ,[QtyTransffer] ,[TaulPtrakNo] ,[TransporterId] ,[TruckChalanNo] ,[TruckNo] ,[DriverName] ,[CreatedDate] ,[CreatedBy] ,getDate() ,'" + localip + "' ,[UserId] ,[IP]  FROM [IssueToSangrahanaKendra]   where  DistrictId='" + District_Id + "' and SocietyId='" + Society_Id + "' and IssueID='" + IssueID + "'";
                command.CommandTimeout = 0;
                res1 = command.ExecuteNonQuery();

                command.CommandText = "";
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from IssueToSangrahanaKendra where  DistrictId='" + District_Id + "' and SocietyId='" + Society_Id + "' and IssueID='" + IssueID + "'";
                res = command.ExecuteNonQuery();
                trans.Commit();
                command.Dispose();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                trans.Rollback();
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return res;

    }
    [WebMethod]
    public int Pro_Correc_Update_GunnyBagsReceipt(string District_ID, string SocietyCode, string GReceiptNo, string NoOfBags, string DateOfRecv, string GunnyType, string TruckChallanNo, string TruckNo, string TruckChallanDate, string UserId)
    {
        int res = 0, res1 = 0;
        SqlTransaction trans = null;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                trans = con.BeginTransaction();
                command = new SqlCommand();
                command.Transaction = trans;

                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "";
                command.CommandText = "insert into GunnyBagsReceipt_log  select * from  GunnyBagsReceipt  where  District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "' and GReceiptNo='" + GReceiptNo + "'";
                command.CommandTimeout = 0;
                res1 = command.ExecuteNonQuery();

                command.CommandText = "";
                command.CommandText = "update GunnyBagsReceipt set NoOfBags='" + NoOfBags + "',DateOfRecv='" + getDate_YMd(DateOfRecv) + "',GunnyType=N'" + GunnyType + "',TruckChallanNo='" + TruckChallanNo + "' ,TruckNo='" + TruckNo + "' , TruckChallanDate ='" + getDate_YMd(TruckChallanDate) + "' ,datetimestamp =getDate(),ip='" + UserId + "' where District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "' and GReceiptNo='" + GReceiptNo + "'";
                command.CommandTimeout = 0;
                res = command.ExecuteNonQuery();
                trans.Commit();
                command.Dispose();



                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                trans.Rollback();
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return res;

    }
    [WebMethod]
    public int Pro_Correc_Delete_GunnyBagsReceipt(string District_ID, string SocietyCode, string GReceiptNo, string localip)
    {
        int res = 0, res1 = 0;
        SqlTransaction trans = null;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                trans = con.BeginTransaction();
                command = new SqlCommand();
                command.Transaction = trans;


                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "";
                command.CommandText = "insert into [GunnyBagsReceipt_log] ([GReceiptNo],[District_ID],[SocietyCode],[PC_Id],[DateOfRecv],[GunnyType],[NoOfBags] ,[TruckChallanNo],[TruckNo] ,[ReceivedFrom] ,[userid],[datetimestamp] ,[opration],[Locked],[TruckChallanDate] ,[ip]) SELECT [GReceiptNo] ,[District_ID],[SocietyCode],[PC_Id],[DateOfRecv],[GunnyType],[NoOfBags],[TruckChallanNo],[TruckNo] ,[ReceivedFrom],[userid],getDate(),'D' ,[Locked],[TruckChallanDate] ,'" + localip + "'  FROM [GunnyBagsReceipt]  where  District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "' and GReceiptNo='" + GReceiptNo + "'";
                command.CommandTimeout = 0;              
                res1 = command.ExecuteNonQuery();

               
                command.CommandType = CommandType.Text;
                command.CommandText = "";
                command.CommandText = "delete from GunnyBagsReceipt where  District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "' and GReceiptNo='" + GReceiptNo + "'";
                res = command.ExecuteNonQuery();
                trans.Commit();
                command.Dispose();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                trans.Rollback();
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return res;

    }
    [WebMethod]
    public int Pro_Correc_Update_GunnyBagsIssueTable(string District_ID, string SocietyCode, string IssueNo, string NoOfBags, string DateOfIssue, string GunnyType, string TruckChallanNo, string TruckNo, string TruckChallanDate, string UserId)
    {
        int res = 0, res1 = 0;
        SqlTransaction trans = null;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                trans = con.BeginTransaction();
                command = new SqlCommand();
                command.Transaction = trans;

                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "";
                command.CommandText = "insert into GunnyBagsIssueTable_log  select * from  GunnyBagsIssueTable  where District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "' and IssueNo='" + IssueNo + "'";
                command.CommandTimeout = 0;
                res1 = command.ExecuteNonQuery();

                command.CommandText = "";
                command.CommandText = "update GunnyBagsIssueTable set NoOfBags='" + NoOfBags + "',DateOfIssue='" + getDate_YMd(DateOfIssue) + "',GunnyType=N'" + GunnyType + "',TruckChallanNo='" + TruckChallanNo + "' ,TruckNo='" + TruckNo + "' , TruckChallanDate ='" + getDate_YMd(TruckChallanDate) + "' ,datetimestamp =getDate(),ip='" + UserId + "' where District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "' and IssueNo='" + IssueNo + "'";
                command.CommandTimeout = 0;
                res = command.ExecuteNonQuery();
                trans.Commit();
                command.Dispose();



                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                trans.Rollback();
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return res;

    }
    [WebMethod]
    public int Pro_Correc_Delete_GunnyBagsIssueTable(string District_ID, string SocietyCode, string IssueNo, string localip)
    {
        int res = 0, res1 = 0;
        SqlTransaction trans = null;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                trans = con.BeginTransaction();
                command = new SqlCommand();
                command.Transaction = trans;

                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "";
                command.CommandText = "insert into [GunnyBagsIssueTable_log] ([IssueNo],[District_ID],[SocietyCode],[PC_Id],[DateOfIssue],[GunnyType],[NoOfBags] ,[TruckChallanNo],[TruckNo] ,[IssuedFrom] ,[userid],[datetimestamp] ,[opration],[Locked],[TruckChallanDate] ,[ip]) SELECT [IssueNo] ,[District_ID],[SocietyCode],[PC_Id],[DateOfIssue],[GunnyType],[NoOfBags],[TruckChallanNo],[TruckNo] ,[IssuedFrom],[userid],getDate(),'D' ,[Locked],[TruckChallanDate] ,'" + localip + "'  FROM [GunnyBagsIssueTable]  where  District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "' and IssueNo='" + IssueNo + "'";
                command.CommandTimeout = 0;
                res1 = command.ExecuteNonQuery();

                command.CommandType = CommandType.Text;
                command.CommandText = "";
                command.CommandText = "delete from GunnyBagsIssueTable where  District_ID='" + District_ID + "' and SocietyCode='" + SocietyCode + "' and IssueNo='" + IssueNo + "'";
                res = command.ExecuteNonQuery();
                trans.Commit();
                command.Dispose();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                trans.Rollback();
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        return res;

    }
    public class SecuredWebServiceHeader : System.Web.Services.Protocols.SoapHeader
    {
        public string Sername;
        public string Username;
        public string Password;

    }
    [WebMethod]
    public bool Check_User(string SerName, string Username, string Password)
    {
        if (Check_UserAuthentication(SerName, Username, Password))
            return true;
        else
            return false;
    }
    [WebMethod]
    public bool Check_ReceivedID(string District_Id, string Society_Id, string ReceivedID)
    {
        bool chkRid = false;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                command = new SqlCommand();
                DataSet ds = new DataSet();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "select count(ReceivedID) from CommodityReceivedFromFarmer where District_Id ='" + District_Id.ToString() + "' and Society_Id='" + Society_Id.ToString() + "' and ReceivedID='" + ReceivedID + "'";
                command.CommandTimeout = 0;
                string res = command.ExecuteScalar().ToString();
                command.Dispose();
                if (Convert.ToInt32(res) > 0)
                {
                    chkRid = true;

                }
                else
                {
                    chkRid = false;
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return chkRid;


    }
    [WebMethod]
    public bool Check_GReceiptNo(string District_ID, string SocietyCode, string GReceiptNo)
    {
        bool chkGid = false;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                command = new SqlCommand();
                DataSet ds = new DataSet();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "select count(GReceiptNo) from GunnyBagsReceipt where District_ID ='" + District_ID.ToString() + "' and SocietyCode='" + SocietyCode.ToString() + "' and GReceiptNo='" + GReceiptNo + "'";
                command.CommandTimeout = 0;
                string res = command.ExecuteScalar().ToString();
                command.Dispose();
                if (Convert.ToInt32(res) > 0)
                {
                    chkGid = true;

                }
                else
                {
                    chkGid = false;
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return chkGid;


    }
    [WebMethod]
    public bool Check_IssueNo(string District_ID, string SocietyCode, string IssueNo)
    {
        bool chkGid = false;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                command = new SqlCommand();
                DataSet ds = new DataSet();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "select count(IssueNo) from GunnyBagsIssueTable where District_ID ='" + District_ID.ToString() + "' and SocietyCode='" + SocietyCode.ToString() + "' and IssueNo='" + IssueNo + "'";
                command.CommandTimeout = 0;
                string res = command.ExecuteScalar().ToString();
                command.Dispose();
                if (Convert.ToInt32(res) > 0)
                {
                    chkGid = true;

                }
                else
                {
                    chkGid = false;
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return chkGid;


    }
    [WebMethod]
    public bool Check_IssueID(string District_Id, string Society_Id, string IssueID)
    {
        bool chksid = false;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                command = new SqlCommand();
                DataSet ds = new DataSet();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "select count(IssueID) from IssueToSangrahanaKendra where DistrictId ='" + District_Id.ToString() + "' and SocietyId='" + Society_Id.ToString() + "' and IssueID='" + IssueID + "'";
                command.CommandTimeout = 0;
                string res = command.ExecuteScalar().ToString();
                command.Dispose();
                if (Convert.ToInt32(res)> 0)
                {
                    chksid = true;

                }
                else
                {
                    chksid = false;
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return chksid;


    }
    protected string getDate_YMd(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("yyyy-MM-dd"));

    }
    protected string getDate_YMd_HH(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("yyyy-MM-dd HH:mm:ss.sss"));

    } 
    public bool Check_UserAuthentication(string SerName, string Username, string Password )
    {
        bool chkUser = false;
        try
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                string User = Username.ToString();
                string Upass = Password.ToString();
                command = new SqlCommand();
                DataSet ds = new DataSet();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = "select * from  WebServicesInfo where Servicename ='" + SerName.ToString() + "' and Username='" + Username.ToString() + "' and status='active'";
                command.CommandTimeout = 0;
                SqlDataAdapter ad = new SqlDataAdapter(command);
                ad.Fill(ds);
                ad.Fill(ds, "Cuser");
                command.Dispose();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        string username = dr["Username"].ToString();
                        string pwd = dr["Password"].ToString();

                        if (User == Username && Upass == pwd)
                        {

                            chkUser = true;

                        }

                        else
                        {

                            chkUser = false;


                        }


                    }


                }
                else
                {

                    return chkUser;


                }

            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return chkUser;


    }
   
}


