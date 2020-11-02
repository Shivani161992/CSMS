using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataAccess;
using System.Data.SqlClient;

namespace Data  {

  public class DistributionCenters {

     private Common cCommon; 
     private DbClass db;
     public String query;
     public Boolean found;
     public String District_ID; 
     public String DC_ID; 
     public String DC_name; 
     public String User_ID; 
     public String DateTimeStamp; 
     public String Operation; 

 public DistributionCenters(Common cCommon) {

    db = new DbClass(cCommon.Connection);
 }

 public void select()
 {

     found=false;
     String qrySelect = "SELECT * FROM dbo.tbl_MetaData_DEPOT where DistrictId =@DC_ID";
     db.DbParameters.Clear();
     db.DbParameters.Add("@DC_ID",DC_ID);
    db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    if (ds != null)
    {

    if (ds.Tables[0].Rows.Count > 0) 
    {

     found=true;
     DataRow dr = ds.Tables[0].Rows[0];
     District_ID = dr["District_ID"].ToString() ; 
     DC_ID = dr["DC_ID"].ToString() ; 
     DC_name = dr["DC_name"].ToString() ; 
     User_ID = dr["User_ID"].ToString() ; 
     DateTimeStamp = dr["DateTimeStamp"].ToString() ; 
     Operation = dr["Operation"].ToString() ; 
    }
   }
 }

 public DataSet select(String where) {

     found=false;
     String qrySelect = "SELECT * FROM dbo.tbl_MetaData_DEPOT where " + where;
     db.DbParameters.Clear();
     db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    return ds;
}
      public DataSet selectPC(String where)
      {

          found = false;
          String qrySelect = "SELECT * FROM dbo.tbl_MetaData_Purchase_Center where " + where;
          db.DbParameters.Clear();
          db.Query = qrySelect;
          DataSet ds = db.SelectinDataSet();
          return ds;
      }
 public DataSet selectAll() {

     found=false;
     String qrySelect = "SELECT * FROM dbo.tbl_MetaData_DEPOT";
     db.DbParameters.Clear();
     db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    return ds;
}

 public DataSet selectAll(String where) {

     found=false;
     String qrySelect = "SELECT * FROM dbo.tbl_MetaData_DEPOT " + where;
     db.DbParameters.Clear();
     db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    return ds;
}

 public DataSet selectAny(String selectstring) {

     found=false;
     String qrySelect= selectstring ;
     db.DbParameters.Clear();
     db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    return ds;
}

 public int insert() {

     String qryInsert = "insert into  dbo.tbl_MetaData_DEPOT (DepotID,StateId,DistrictId,DepotName) values(@District_ID,@DC_ID,@DC_name,@User_ID,@DateTimeStamp,@Operation)";
   db.DbParameters.Clear();
    db.DbParameters.Add("@District_ID",District_ID);
    db.DbParameters.Add("@DC_ID",DC_ID);
    db.DbParameters.Add("@DC_name",DC_name);
    db.DbParameters.Add("@User_ID",User_ID);
    db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
    db.DbParameters.Add("@Operation",Operation);
   db.Query = qryInsert;
   return db.ExecuteNonQuery();
 }

 public int update() {

     String qryUpdate = "update dbo.tbl_MetaData_DEPOT set District_ID=@District_ID,DC_ID=@DC_ID,DC_name=@DC_name,User_ID=@User_ID,DateTimeStamp=@DateTimeStamp,Operation=@Operation where DC_ID =@DC_ID";
   db.DbParameters.Clear();
    db.DbParameters.Add("@District_ID",District_ID);
    db.DbParameters.Add("@DC_ID",DC_ID);
    db.DbParameters.Add("@DC_name",DC_name);
    db.DbParameters.Add("@User_ID",User_ID);
    db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
    db.DbParameters.Add("@Operation",Operation);
   db.Query = qryUpdate;
   return db.ExecuteNonQuery();
 }

 public int updateOnly(String str) {

   String stringTemp;
   String stringCal;
   String stringDuplicate;
   int count = 0;
   int pos;
   int start = 0;
   int i = 0;
   str = str+String.Concat(",");
   stringDuplicate = str;
   int length = str.Length;

while (length != 0){
    pos = str.IndexOf(",");
     count = count + 1;
     string stringCal1 = str.Substring(pos + 1);
     stringCal = stringCal1;
     length = length - (pos + 1);
     str = stringCal;
}
str = stringDuplicate;
length = str.Length;
string[] compareString = new string[count];
while (length != 0){
	pos = str.IndexOf(",");
	stringTemp = str.Substring(start, pos);
	compareString[i] = stringTemp;
	stringCal = str.Substring(pos + 1);
	length = length - (pos + 1);
	str = stringCal;
	i++;
}

for (int j = 0; j < compareString.Length; j++){
 if (compareString[j].Equals("District_ID"))  
 query = query + compareString[j] + " = " + "@District_ID,"; 
 if (compareString[j].Equals("DC_ID"))  
 query = query + compareString[j] + " = " + "@DC_ID,"; 
 if (compareString[j].Equals("DC_name"))  
 query = query + compareString[j] + " = " + "@DC_name,"; 
 if (compareString[j].Equals("User_ID"))  
 query = query + compareString[j] + " = " + "@User_ID,"; 
 if (compareString[j].Equals("DateTimeStamp"))  
 query = query + compareString[j] + " = " + "@DateTimeStamp,"; 
 if (compareString[j].Equals("Operation"))  
 query = query + compareString[j] + " = " + "@Operation,"; 
  }

query= query.Substring(0, query.Length - 1);
String sqlStr = "update dbo.tbl_MetaData_DEPOT set " + query + " where DC_ID =@DC_ID";
   db.DbParameters.Clear();
    db.DbParameters.Add("@District_ID",District_ID);
    db.DbParameters.Add("@DC_ID",DC_ID);
    db.DbParameters.Add("@DC_name",DC_name);
    db.DbParameters.Add("@User_ID",User_ID);
    db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
    db.DbParameters.Add("@Operation",Operation);
   db.Query = sqlStr;
   return db.ExecuteNonQuery();
 }

 public int delete() {

     String qryDelete = " delete from dbo.tbl_MetaData_DEPOT where DC_ID =@DC_ID";
    db.DbParameters.Clear();
    db.DbParameters.Add("@DC_ID",DC_ID);
    db.Query = qryDelete;
    return db.ExecuteNonQuery();
   }
  }
 }
