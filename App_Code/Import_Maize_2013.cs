using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Serialization;


/// <summary>
/// Summary description for Import_Maize_2013
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/", Name = "RunnerServiceMaizeProcurement2013", Description = "Import Data (insert data in society mdb /Date: 29082013)")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Import_Maize_2013 : System.Web.Services.WebService 
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_MPMS2014"].ToString());
    private SqlCommand command, cmd;
    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;

    public Import_Maize_2013 () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Security

    public securityimportMaize2013 securityimportMaize2013;
    [SoapHeader("securityimportMaize2013")]

    [WebMethod(Description = "This Method Is Used For Security Check in")]
    public bool chkSecurityImport(securityimportMaize2013 S)
    {
        bool rtev = false;
        try
        {
            if (S != null)
            {
                OpenConnection();
                SqlCommand chkcmd = new SqlCommand();
                DataSet cds = new DataSet();
                chkcmd.Connection = connection;
                chkcmd.CommandType = CommandType.StoredProcedure;
                chkcmd.CommandText = "View_ServiceInformation";
                chkcmd.Parameters.Clear();
                chkcmd.Parameters.AddWithValue("@UserName", S.UserName);
                chkcmd.Parameters.AddWithValue("@SPasswordInClient", S.Password);

                SqlDataAdapter da = new SqlDataAdapter(chkcmd);
                da.Fill(cds);
                if (cds != null)
                {
                    if (cds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in cds.Tables[0].Rows)
                        {
                            S.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                            if (S.IsActive == true)
                            {
                                rtev = true;
                                return rtev;
                            }
                        }
                    }
                }

                CloseConnection();
            }
            else
            {
                return rtev;
            }
        }
        catch
        {
            /////////
        }
        return rtev;
    }

    #endregion

    #region Export Data from Online To Offline.......

    [WebMethod(Description = "This Method Is Used For Update UserPassword Table in offline Database")]
    public DataSet OpUserPassword()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from UserPassword where ischanged='Y'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Rollback();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Insert New Village in Offline Database")]
    public DataSet OpVillages(string DistrictId,string Villagecodes)
    {
        dataset = new DataSet();
        try
        {
            if (Villagecodes == "")
            {
                Villagecodes = "''";
            }
            OpenConnection();
            commandt = connection.CreateCommand();
            trans = connection.BeginTransaction();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from VillageMaster where District_Id='" + DistrictId + "' and  VILLAGE_STATUS='N' and VILLAGE_CODE not in (" + Villagecodes + ") ";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Insert New Bank in Offline Database")]
    public DataSet OpBankMaster()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from Bank where Status='N'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Insert Soceity Loan in Offline Database")]
    public DataSet OpSocietyLoanMaster(string D, string socloanmids)
    {
        dataset = new DataSet();
        try
        {
            if (socloanmids == "")
            {
                socloanmids = "''";
            }

            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from SocietyLoanMaster where SocietyLoanMaster.Society_Id not in (" + socloanmids + ")  and SocietyLoanMaster.DistrictId='" + D + "' ";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();

        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;

    }

    [WebMethod(Description = "This Method Is Used For Soceity Loan output for offline database ")]
    public DataSet OpSocietyLoan(string districtid, string societyid, string farmerids)
    {
        dataset = new DataSet();
        try
        {
            if (farmerids == "")
            {
                farmerids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from SocietyLoanOfFarmer where Farmer_Id not in (" + farmerids + ") and  District_Id='" + districtid + "' and PC_ID='" + societyid + "'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();

        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For DCCB Loan output for offline database ")]
    public DataSet OpDCCBLoan(string districtid, string dccbloanids)
    {
        dataset = new DataSet();
        try
        {
            if (dccbloanids == "")
            {
                dccbloanids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from DCCBLoanOfFarmer where DCCBLoanOfFarmer.Farmer_Id not in (" + dccbloanids + ") and  DCCBLoanOfFarmer.District_Id='" + districtid + "'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Irrigation Loan output for offline database ")]
    public DataSet OpIrrigationLoan(string districtid, string irriloafarmerids)
    {
        dataset = new DataSet();
        try
        {
            if (irriloafarmerids == "")
            {
                irriloafarmerids = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from IrrLoanOfFarmer where IrrLoanOfFarmer.Farmer_Id not in (" + irriloafarmerids + ") and  IrrLoanOfFarmer.District_Id='" + districtid + "'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Transport Master output for offline database ")]
    public DataSet OpTransportMaster(string districtid, string societyid)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from TransportMaster where District_ID='" + districtid + "' and SocietyCode='" + societyid + "'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Tehsil Yield output for offline database ")]
    public DataSet OpTehsilYeild(string District_Code)
    {
        dataset = new DataSet();
        try
        {

            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from TehsilYield where District_Code='" + District_Code + "'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Godown Storage output for offline database ")]
    public DataSet OpGodownStorage()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from tbl_MPWLC_Godown_Storage where flag = 'Y'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();

        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For MPWLC DEPOT output for offline database ")]
    public DataSet Optbl_MPWLC_DEPOT()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from tbl_MPWLC_DEPOT ";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    #endregion

    #region Insertion in Correction Log

    [WebMethod(Description = "This Method Is Used For Gunny bags Correction Log output for online database ")]
    public bool InGuunyBagsCorrection(DataSet dsGunnyBags)
    {
        bool result = false;
        try
        {
            if (dsGunnyBags != null)
            {
                if (dsGunnyBags.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsGunnyBags.Tables[0].Rows)
                    {
                        try
                        {
                            string IssueNo_or_GReceiptNo = dr["IssueNo_or_GReceiptNo"].ToString();
                            string District_ID = dr["District_ID"].ToString();
                            string Society_Id = dr["Society_Id"].ToString();
                            string NoOfBags = dr["NoOfBags"].ToString();
                            string IsUpdated = dr["IsUpdated"].ToString();
                            string IsDeleted = dr["IsDeleted"].ToString();
                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from GunnyBagsCorrectionLog where District_ID='" + District_ID + "'  and Society_Id= '" + Society_Id + "' and IssueNo_or_GReceiptNo='" + IssueNo_or_GReceiptNo + "' and NoOfBags='" + NoOfBags + "' and IsUpdated='" + IsUpdated + "' and IsDeleted='" + IsDeleted + "'";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string UserId = dr["UserId"].ToString();
                                string IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "in_GunnyBagsCorrectionLog";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IssueNo_or_GReceiptNo", IssueNo_or_GReceiptNo);
                                cmd.Parameters.AddWithValue("@District_ID", District_ID);
                                cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                                cmd.Parameters.AddWithValue("@NoOfBags", NoOfBags);
                                cmd.Parameters.AddWithValue("@IsUpdated", IsUpdated);
                                cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                                cmd.Parameters.AddWithValue("@UserId", UserId);
                                cmd.Parameters.AddWithValue("@IP", IP);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                            //////////////
                        }
                    }
                    trans.Commit();
                    result = true;
                }
            }
        }
        catch (Exception)
        {
            trans.Rollback();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return result;
    }

    [WebMethod(Description = "This Method Is Used For Praptee Correction Log output for online database ")]
    public bool InPrapteeCorrectionLog(DataSet dsPrapteeCorrection)
    {
        bool result = false;
        try
        {
            if (dsPrapteeCorrection != null)
            {
                if (dsPrapteeCorrection.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();

                    foreach (DataRow dr in dsPrapteeCorrection.Tables[0].Rows)
                    {
                        try
                        {
                            string ReceivedID = dr["ReceivedID"].ToString();
                            string District_Id = dr["District_Id"].ToString();
                            string Society_Id = dr["Society_Id"].ToString();
                            string Old_Bags = dr["Old_Bags"].ToString();
                            string New_Bags = dr["New_Bags"].ToString();
                            string Old_QtyReceived = dr["Old_QtyReceived"].ToString();
                            string New_QtyReceived = dr["New_QtyReceived"].ToString();
                            string IsDelete = dr["IsDelete"].ToString();
                            string IsUpdated = dr["IsUpdated"].ToString();

                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from PrapteeCorrectionLog where District_Id='" + District_Id + "'  and Society_Id= '" + Society_Id + "' and ReceivedID='" + ReceivedID + "' and Old_Bags='" + Old_Bags + "' and New_Bags='" + New_Bags + "' and Old_QtyReceived='" + Old_QtyReceived + "' and New_QtyReceived='" + New_QtyReceived + "'  and  IsUpdated='" + IsUpdated + "' and IsDelete='" + IsDelete + "'";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string Old_TotalAmount = dr["Old_TotalAmount"].ToString();
                                string New_TotalAmount = dr["New_TotalAmount"].ToString();
                                string UpdationDate = getRDate_MDY(dr["UpdationDate"].ToString());
                                string UserID = dr["UserID"].ToString();
                                string IP = dr["IP"].ToString();

                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "in_PrapteeCorrectionLog";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@ReceivedID", ReceivedID);
                                cmd.Parameters.AddWithValue("@District_Id", District_Id);
                                cmd.Parameters.AddWithValue("@Society_Id", Society_Id);
                                cmd.Parameters.AddWithValue("@Old_Bags", Old_Bags);
                                cmd.Parameters.AddWithValue("@New_Bags", New_Bags);
                                cmd.Parameters.AddWithValue("@Old_QtyReceived", Old_QtyReceived);
                                cmd.Parameters.AddWithValue("@New_QtyReceived", New_QtyReceived);
                                cmd.Parameters.AddWithValue("@IsDelete", IsDelete);
                                cmd.Parameters.AddWithValue("@IsUpdated", IsUpdated);
                                cmd.Parameters.AddWithValue("@Old_TotalAmount", Old_TotalAmount);
                                cmd.Parameters.AddWithValue("@New_TotalAmount", New_TotalAmount);
                                cmd.Parameters.AddWithValue("@UpdationDate", UpdationDate);
                                cmd.Parameters.AddWithValue("@UserID", UserID);
                                cmd.Parameters.AddWithValue("@IP", IP);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                            ////////////////
                        }
                    }
                    trans.Commit();
                    result = true;
                }
            }
        }
        catch (Exception)
        {
            trans.Rollback();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return result;
    }

    [WebMethod(Description = "This Method Is Used For Jaree correction log output for online database ")]
    public bool InJareeCorrectionLog(DataSet dsJareeCorrectionLog)
    {
        bool result = false;
        try
        {
            if (dsJareeCorrectionLog != null)
            {
                if (dsJareeCorrectionLog.Tables[0].Rows.Count > 0)
                {
                    OpenConnection();
                    trans = connection.BeginTransaction();
                    foreach (DataRow dr in dsJareeCorrectionLog.Tables[0].Rows)
                    {
                        try
                        {
                            string IssueID = dr["IssueID"].ToString();
                            string DistrictId = dr["DistrictId"].ToString();
                            string SocietyID = dr["SocietyID"].ToString();
                            string DateOfIssue = dr["DateOfIssue"].ToString();
                            string Old_Bags = dr["Old_Bags"].ToString();
                            string New_Bags = dr["New_Bags"].ToString();
                            string Old_QtyTransffer = dr["Old_QtyTransffer"].ToString();
                            string New_QtyTransffer = dr["New_QtyTransffer"].ToString();
                            string UpdatedDate = getRDate_MDY(dr["UpdatedDate"].ToString());
                            string IsDeleted = dr["IsDeleted"].ToString();
                            string IsUpdated = dr["IsUpdated"].ToString();

                            commandt = connection.CreateCommand();
                            commandt.Transaction = trans;
                            commandt.CommandType = CommandType.Text;
                            commandt.CommandText = "select count(*) from JareeCorrectionLog where DistrictId='" + DistrictId + "'  and SocietyID= '" + SocietyID + "' and IssueID='" + IssueID + "' and Old_Bags='" + Old_Bags + "' and New_Bags='" + New_Bags + "' and Old_QtyTransffer='" + Old_QtyTransffer + "' and New_QtyTransffer='" + New_QtyTransffer + "'  and  IsDeleted='" + IsDeleted + "' and IsUpdated='" + IsUpdated + "'";
                            Int64 res = Convert.ToInt64(commandt.ExecuteScalar());
                            commandt.Dispose();
                            if (res <= 0)
                            {
                                string UserId = dr["UserId"].ToString();
                                string Ip = dr["Ip"].ToString();

                                cmd = connection.CreateCommand();
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "in_JareeCorrectionLog";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IssueID", IssueID);
                                cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                                cmd.Parameters.AddWithValue("@SocietyID", SocietyID);
                                cmd.Parameters.AddWithValue("@DateOfIssue", DateOfIssue);
                                cmd.Parameters.AddWithValue("@Old_Bags", Old_Bags);
                                cmd.Parameters.AddWithValue("@New_Bags", New_Bags);
                                cmd.Parameters.AddWithValue("@Old_QtyTransffer", Old_QtyTransffer);
                                cmd.Parameters.AddWithValue("@New_QtyTransffer", New_QtyTransffer);
                                cmd.Parameters.AddWithValue("@UpdatedDate", UpdatedDate);
                                cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                                cmd.Parameters.AddWithValue("@IsUpdated", IsUpdated);
                                cmd.Parameters.AddWithValue("@UserId", UserId);
                                cmd.Parameters.AddWithValue("@Ip", Ip);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                            /////////////////////
                        }
                    }
                    trans.Commit();
                    result = true;
                }
            }
        }
        catch (Exception)
        {
            trans.Rollback();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return result;
    }

    #endregion

    #region Output Correction Log Information

    [WebMethod(Description = "This Method Is Used For Gunny bags Correction output for offline database ")]
    public DataSet OpGunnyBagsCorrection(string D, string S)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            //Here I use user id as unique id whic is a unique id in offline table when coreection module is created this is because of upade and delete both case handling
            commandt.CommandText = "select distinct GunnyBagsCorrectionLog.UserId from GunnyBagsCorrectionLog  where GunnyBagsCorrectionLog.District_ID='" + D + "' and GunnyBagsCorrectionLog.Society_Id='" + S + "'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Praptee Correction Log output for offline database ")]
    public DataSet OpPrapteeCorrection(string D, string S)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            //Here I use user id as unique id whic is a unique id in offline table when coreection module is created this is because of upade and delete both case handling
            commandt.CommandText = "select distinct PrapteeCorrectionLog.UserId from PrapteeCorrectionLog  where PrapteeCorrectionLog.District_ID='" + D + "' and PrapteeCorrectionLog.Society_Id='" + S + "' and PrapteeCorrectionLog.UserId=' '";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Jaree correction log output for offline database ")]
    public DataSet OpJareeCorrection(string D, string S)
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            //Here I use user id as unique id whic is a unique id in offline table when coreection module is created this is because of upade and delete both case handling
            commandt.CommandText = "select distinct JareeCorrectionLog.UserId from JareeCorrectionLog  where JareeCorrectionLog.DistrictId='" + D + "' and JareeCorrectionLog.SocietyID='" + S + "' and JareeCorrectionLog.UserId=' '";

            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod]
    public DataSet OpCommodityRate()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from CommodityRate where Status1='Y'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }

        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod]
    public DataSet OpDateControl()
    {
        dataset = new DataSet();
        try
        {
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from DateControl where status='Change'";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }

        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Jaree correction log output for offline database ")]
    public DataSet OP_Soc_Change(string D, string S, string F_IDS)
    {
        dataset = new DataSet();
        try
        {
            if (F_IDS == "")
            {
                F_IDS = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select distinct SocietyChangeLog.Farmer_ID from SocietyChangeLog  where SocietyChangeLog.District_ID='" + D + "' and SocietyChangeLog.NEW_PC_ID='" + S + "' and SocietyChangeLog.Farmer_ID NOT IN (" + F_IDS + ")";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Jaree correction log output for offline database ")]
    public DataSet OP_Soc_Change_Farmer_Basic_Info(string D, string S, string F_IDS)
    {
        dataset = new DataSet();
        try
        {
            if (F_IDS == "")
            {
                F_IDS = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from FarmerRegistration_New  where FarmerRegistration_New.District_Id='" + D + "' and FarmerRegistration_New.Procured_SocietyID='" + S + "' and FarmerRegistration_New.NewFarmer_Id IN (" + F_IDS + ")";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    [WebMethod(Description = "This Method Is Used For Jaree correction log output for offline database ")]
    public DataSet OP_Soc_Change_Farmer_Land_Info(string F_IDS)
    {
        dataset = new DataSet();
        try
        {
            if (F_IDS == "")
            {
                F_IDS = "''";
            }
            OpenConnection();
            trans = connection.BeginTransaction();
            commandt = connection.CreateCommand();
            commandt.Transaction = trans;
            commandt.Parameters.Clear();
            commandt.CommandType = CommandType.Text;
            commandt.CommandText = "select * from Farmer_LandRecordDescription_New  where Farmer_LandRecordDescription_New.NewFarmer_Id IN (" + F_IDS + ")";
            dataAdapter = new SqlDataAdapter(commandt);
            dataAdapter.Fill(dataset);
            commandt.Dispose();
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Commit();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return dataset;
    }

    #endregion

    #region Commonly Used Functions

    private string getRDate_MDY(string inDate)
    {
        string dat = "";
        if (inDate != "")
        {
            DateTime imd = Convert.ToDateTime(inDate);
            dat = imd.ToShortDateString();
            imd = Convert.ToDateTime(dat);
            dat = imd.ToString("MM/dd/yyyy");
        }
        return dat;
    }

    private string getDate_MDY(string inDate)
    {
        string dat = "";
        if (inDate != "")
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dat = (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
        }
        return dat;
    }

    #endregion

    #region Check Connection

    public void OpenConnection()
    {
        if (connection.State == ConnectionState.Open)
        {
            connection.Close();
            connection.Open();
        }
        else
        {
            connection.Open();
        }

    }

    public void CloseConnection()
    {
        if (connection.State == ConnectionState.Open)
        {
            connection.Close();
        }
    }

    #endregion
}
public class securityimportMaize2013 : System.Web.Services.Protocols.SoapHeader
{
    public bool IsActive;
    public String Password;
    public String UserName;
    public String RunnerId;
    public String RunnerPassword;
}


