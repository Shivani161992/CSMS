using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for MarketingSeas
/// </summary>
public class comodity
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;
   
  
    public comodity(Common cCommon)
    {
        db = new DbClass(cCommon.Connection);
    }

    public DataSet selectAll()
    {

        found = false;
        String qrySelect = "SELECT * FROM CommodityMaster";
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }

    public DataSet selectWhere(String where)
    {

        found = false;
        String qrySelect = "SELECT * FROM CommodityMaster  where " + where;
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
