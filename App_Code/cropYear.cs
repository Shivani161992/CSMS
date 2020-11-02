using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for cropYear
/// </summary>
public class cropYear
{
    private Common cCommon;
    private DbClass db;
    public String query;
    public Boolean found;
    public String ID;
    public String MarketingId;
    public String MarketingSeason;
    public cropYear(Common cCommon)
	{
        db = new DbClass(cCommon.Connection);
	}
    public DataSet selectAll()
    {

        found = false;
        String qrySelect = "SELECT * FROM CropYearMaster";
        db.DbParameters.Clear();
        db.Query = qrySelect;
        DataSet ds = db.SelectinDataSet();
        return ds;
    }
}
