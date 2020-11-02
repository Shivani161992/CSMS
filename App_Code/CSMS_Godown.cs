using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for CSMS_Godown
/// </summary>
[WebService(Namespace = "http://microsoft.co.in/", Name = "CSMS_Service_For_Godown_Entry", Description = "Inserting Godowns when Godown Inserted in WareHouse Application, Hosted Date :: ")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CSMS_Godown : System.Web.Services.WebService
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    private SqlCommand command, cmd;
    private SqlTransaction trans = null;
    public CSMS_Godown()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod(Description = "This Method Is Used For Inserting Godown Information")]
    public void Insert_Godown_Info(DataSet dsGodownInfo, string Pass, string User)
    {
        string Godown_ID = "";
        try
        {
            if (connection != null)
            {
                if (dsGodownInfo != null)
                {
                    string Password = "csms@godown";
                    string Userid = "csms";
                    if (Password == Pass && Userid == User)
                    {
                        connection.Open();
                        foreach (DataRow dr in dsGodownInfo.Tables[0].Rows)
                        {
                            Godown_ID = dr["Godown_ID"].ToString();
                            string DistrictId = (dr["DistrictId"].ToString().Substring(2, 2));
                            string DepotId = dr["DepotId"].ToString();
                            string GodownName = dr["Godown_Name"].ToString();

                            float Godown_Capacity = CheckFloat(dr["Godown_Capacity"].ToString());
                            string Remarks = dr["Remarks"].ToString();
                            string CreatedBy = dr["CreatedBy"].ToString();
                            string UpdatedBy = dr["UpdatedBy"].ToString();

                            string Hired_Type = dr["Hired_Type"].ToString();
                            string Storage_Type = dr["Storage_Type"].ToString();
                            float Godown_Scientific_Capacity = CheckFloat(dr["Godown_Scientific_Capacity"].ToString());

                            command = new SqlCommand("Select count(*) from tbl_MetaData_GODOWN where Godown_ID='" + Godown_ID + "' and  DistrictId='" + DistrictId + "' and DepotId = '" + DepotId + "' ", connection);
                            string res = command.ExecuteScalar().ToString();
                            if (Convert.ToInt32(res) <= 0)
                            {
                                SqlCommand cmd = new SqlCommand("SP_Insert_Godown", connection);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
                                cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                                cmd.Parameters.AddWithValue("@DepotId", DepotId);
                                cmd.Parameters.AddWithValue("@Godown_Name", GodownName);

                                cmd.Parameters.AddWithValue("@Godown_Capacity", Godown_Capacity);
                                cmd.Parameters.AddWithValue("@Remarks", Remarks);
                                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                                cmd.Parameters.AddWithValue("@Hired_Type", Hired_Type);

                                cmd.Parameters.AddWithValue("@Storage_Type", Storage_Type);
                                cmd.Parameters.AddWithValue("@Godown_Scientific_Capacity", Godown_Scientific_Capacity);

                                int Status = cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                #region Update Godown Master in csms database

                                SqlCommand cmd = new SqlCommand("SP_Update_Godown", connection);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
                                cmd.Parameters.AddWithValue("@DepotId", DepotId);

                                cmd.Parameters.AddWithValue("@Godown_Name", GodownName);
                                cmd.Parameters.AddWithValue("@Godown_Capacity", Godown_Capacity);
                                cmd.Parameters.AddWithValue("@Godown_Scientific_Capacity", Godown_Scientific_Capacity);
                                cmd.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);

                                cmd.Parameters.AddWithValue("@Hired_Type", Hired_Type);
                                cmd.Parameters.AddWithValue("@Storage_Type", Storage_Type);

                                int Update = cmd.ExecuteNonQuery();

                                #endregion
                            }
                        }
                    }
                }
            }
        }

        catch (Exception)
        {
            connection.Close();
        }
        finally
        {
            connection.Close();
        }
    }

    [WebMethod(Description = "This Method Is Used For Deleting Godown Information")]
    public bool Delete_Godown(string depotId, string Godownid, string Pass, string User)
    {
        bool result = false;
        try
        {
            string Password = "csms@godown";
            string Userid = "csms";
            if (Password == Pass && Userid == User)
            {
                connection.Open();
                trans = connection.BeginTransaction();
                cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = trans;
                string query = "delete from dbo.tbl_MetaData_GODOWN where Godown_ID='" + Godownid + "' and depotId = '" + depotId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                int x = cmd.ExecuteNonQuery();
                trans.Commit();
                if (x == 1)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

        }
        catch (Exception)
        {
            trans.Rollback();
            connection.Close();
        }
        finally
        {
            connection.Close();
        }
        return result;
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

    float CheckFloat(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
}

