using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataAccess;
using System.Data.SqlClient;


/// <summary>
/// Summary description for tahsil
/// </summary>
public class tahsil
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;
    public String ID;
  
    public tahsil(Common cCommon)
	{
        db = new DbClass(cCommon.Connection);
	}

    public DataSet selectAll()
    {

        found = false;
        String qrySelect = "SELECT * from LR_TehsilMaster";
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }
    public DataSet selectAny(String selectstring)
    {

        found = false;
        String qrySelect = selectstring;
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }
}
