using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;

/// <summary>
/// Summary description for clsConnection
/// </summary>
public class clsConnectionPPMS_demo
{

    //  SqlConnection constr = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStringFarmerDB"].ToString());
    SqlConnection constr = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_PPMSDemo_Test"].ToString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataSet ds = null;
    SqlTransaction trans = null;


    public SqlConnection Con
    {
        get
        {
            return constr;
        }
    }

    public clsConnectionPPMS_demo()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    #region returnDropDownData

    public void FillDropDownLsit(string DataTextField, string DataValueField, DropDownList ddl,
    string ProcName, string ProcPDist, string ProcPTeh, string ProcPVilla, string ProcPFarmerId,
    string ValueDis, string ValueTeh, string ValueVilla, string ValuePFarmerId, int ParmCount)
    {
        try
        {
            OpenConnection();
            cmd = new SqlCommand();
            cmd.Connection = Con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcName;
            cmd.Parameters.Clear();

            if (ParmCount == 4)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue(ProcPDist, ValueDis);
                cmd.Parameters.AddWithValue(ProcPTeh, ValueTeh);
                cmd.Parameters.AddWithValue(ProcPVilla, ValueVilla);
                cmd.Parameters.AddWithValue(ProcPFarmerId, ValuePFarmerId);
            }
            else if (ParmCount == 3)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue(ProcPDist, ValueDis);
                cmd.Parameters.AddWithValue(ProcPTeh, ValueTeh);
                cmd.Parameters.AddWithValue(ProcPVilla, ValueVilla);
            }
            else if (ParmCount == 2)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue(ProcPDist, ValueDis);
                cmd.Parameters.AddWithValue(ProcPTeh, ValueTeh);
            }
            else if (ParmCount == 1)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue(ProcPDist, ValueDis);
            }
            else if (ParmCount == 0)
            {
                cmd.Parameters.Clear();
            }
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            ddl.DataSource = ds;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
            //  ddl.Items.Insert(0, "--चयन करें--");
            ddl.Items.Insert(0, new ListItem("--चयन करें--", "0"));
        }
        catch (Exception)
        {
            CloseConnection();
        }
        finally
        {
            cmd.Dispose();
            da.Dispose();
            CloseConnection();
        }


    }

    #endregion


    #region ExecuteNonQuery

    public Int32 ExecuteNonQuery(SqlCommand cmds, string ProcName)
    {
        Int32 ret = 0;
        try
        {
            OpenConnection();
            cmd = cmds;
            cmd.Connection = Con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcName;
            ret = cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            cmd.Dispose();
            CloseConnection();
        }
        finally
        {
            CloseConnection();
        }
        return ret;
    }

    #endregion


    #region returnDataSet

    public DataSet SelectDataSet(SqlCommand cmds, String ProcName)
    {
        ds = new DataSet();
        try
        {
            OpenConnection();
            cmd = cmds;
            cmd.Connection = Con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcName;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception)
        {
            CloseConnection();
        }
        finally
        {
            cmd.Dispose();
            da.Dispose();
            CloseConnection();
        }
        return ds;

    }

    #endregion


    #region Connection


    public void OpenConnection()
    {
        if (Con != null)
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
                Con.Open();
            }
            else
            {
                Con.Open();
            }
        }
    }

    public void CloseConnection()
    {
        if (Con != null)
        {

            if (Con.State != ConnectionState.Closed)
            {
                Con.Close();
            }
            else
            {

            }
        }
    }


    #endregion



}
