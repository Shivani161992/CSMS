using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataAccess;
using System.Data.SqlClient;
/// <summary>
/// Summary description for RationCardType
/// </summary>
public class RationCardType
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;

    public RationCardType(Common cCommon)
	{
        db = new DbClass(cCommon.Connection);
	}
    public DataSet selectAll()
    {

        found = false;
        String qrySelect = "SELECT * from FES_Master";
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }
}
