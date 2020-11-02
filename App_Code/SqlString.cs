using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SqlString
/// </summary>
public class SqlString
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;

    public SqlString(Common cCommon)
	{
        db = new DbClass(cCommon.Connection);
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
