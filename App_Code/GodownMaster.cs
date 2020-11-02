using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataAccess;
using System.Data.SqlClient;

namespace Data  {

  public class GodownMaster {

     private Common cCommon; 
     private DbClass db;
     public String query;
     public Boolean found;
     public String DC_ID; 
     public String Godown_ID; 
     public String Godown_Name; 
     public String Capacity; 
     public String DateTimeStamp; 
     public String User_ID; 
     public String Operation; 
     public String Transfered; 

 public GodownMaster(Common cCommon) {

    db = new DbClass(cCommon.Connection);
 }

 public void select() {

     found=false;
     String qrySelect = "SELECT * FROM dbo.tbl_MetaData_DEPOT where districtid = @DC_ID And Godown_ID =@Godown_ID";
     db.DbParameters.Clear();
     db.DbParameters.Add("@DC_ID",DC_ID);
    db.DbParameters.Add("@Godown_ID",Godown_ID);
    db.Query = qrySelect;
    DataSet ds = db.SelectinDataSet();
    if (ds != null) {

    if (ds.Tables[0].Rows.Count > 0) {

     found=true;
     DataRow dr = ds.Tables[0].Rows[0];
     DC_ID = dr["DC_ID"].ToString() ; 
     Godown_ID = dr["Godown_ID"].ToString() ; 
     Godown_Name = dr["Godown_Name"].ToString() ; 
     Capacity = dr["Capacity"].ToString() ; 
     DateTimeStamp = dr["DateTimeStamp"].ToString() ; 
     User_ID = dr["User_ID"].ToString() ; 
     Operation = dr["Operation"].ToString() ; 
     Transfered = dr["Transfered"].ToString() ; 
    }
   }
   ds.Dispose();
 }

 public DataSet select(String where) {

     found=false;
     String qrySelect = "SELECT * FROM dbo.tbl_MetaData_DEPOT where " + where;
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

     String qryInsert = "insert into  tbl_MetaData_DEPOT (DC_ID,Godown_ID,Godown_Name,Capacity,DateTimeStamp,User_ID,Operation,Transfered) values(@DC_ID,@Godown_ID,@Godown_Name,@Capacity,@DateTimeStamp,@User_ID,@Operation,@Transfered)";
   db.DbParameters.Clear();
    db.DbParameters.Add("@DC_ID",DC_ID);
    db.DbParameters.Add("@Godown_ID",Godown_ID);
    db.DbParameters.Add("@Godown_Name",Godown_Name);
    db.DbParameters.Add("@Capacity",Capacity);
    db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
    db.DbParameters.Add("@User_ID",User_ID);
    db.DbParameters.Add("@Operation",Operation);
    db.DbParameters.Add("@Transfered",Transfered);
   db.Query = qryInsert;
   return db.ExecuteNonQuery();
 }

 public int update() {

     String qryUpdate = "update tbl_MetaData_DEPOT set DC_ID=@DC_ID,Godown_ID=@Godown_ID,Godown_Name=@Godown_Name,Capacity=@Capacity,DateTimeStamp=@DateTimeStamp,User_ID=@User_ID,Operation=@Operation,Transfered=@Transfered where DC_ID =@DC_ID And Godown_ID =@Godown_ID";
   db.DbParameters.Clear();
    db.DbParameters.Add("@DC_ID",DC_ID);
    db.DbParameters.Add("@Godown_ID",Godown_ID);
    db.DbParameters.Add("@Godown_Name",Godown_Name);
    db.DbParameters.Add("@Capacity",Capacity);
    db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
    db.DbParameters.Add("@User_ID",User_ID);
    db.DbParameters.Add("@Operation",Operation);
    db.DbParameters.Add("@Transfered",Transfered);
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
 if (compareString[j].Equals("DC_ID"))  
 query = query + compareString[j] + " = " + "@DC_ID,"; 
 if (compareString[j].Equals("Godown_ID"))  
 query = query + compareString[j] + " = " + "@Godown_ID,"; 
 if (compareString[j].Equals("Godown_Name"))  
 query = query + compareString[j] + " = " + "@Godown_Name,"; 
 if (compareString[j].Equals("Capacity"))  
 query = query + compareString[j] + " = " + "@Capacity,"; 
 if (compareString[j].Equals("DateTimeStamp"))  
 query = query + compareString[j] + " = " + "@DateTimeStamp,"; 
 if (compareString[j].Equals("User_ID"))  
 query = query + compareString[j] + " = " + "@User_ID,"; 
 if (compareString[j].Equals("Operation"))  
 query = query + compareString[j] + " = " + "@Operation,"; 
 if (compareString[j].Equals("Transfered"))  
 query = query + compareString[j] + " = " + "@Transfered,"; 
  }

query= query.Substring(0, query.Length - 1);
String sqlStr = "update tbl_MetaData_DEPOT set " + query + " where DC_ID =@DC_ID And Godown_ID =@Godown_ID";
   db.DbParameters.Clear();
    db.DbParameters.Add("@DC_ID",DC_ID);
    db.DbParameters.Add("@Godown_ID",Godown_ID);
    db.DbParameters.Add("@Godown_Name",Godown_Name);
    db.DbParameters.Add("@Capacity",Capacity);
    db.DbParameters.Add("@DateTimeStamp",DateTimeStamp);
    db.DbParameters.Add("@User_ID",User_ID);
    db.DbParameters.Add("@Operation",Operation);
    db.DbParameters.Add("@Transfered",Transfered);
   db.Query = sqlStr;
   return db.ExecuteNonQuery();
 }

 public int delete() {

     String qryDelete = " delete from dbo.tbl_MetaData_DEPOT where DC_ID =@DC_ID And Godown_ID =@Godown_ID";
    db.DbParameters.Clear();
    db.DbParameters.Add("@DC_ID",DC_ID);
    db.DbParameters.Add("@Godown_ID",Godown_ID);
    db.Query = qryDelete;
    return db.ExecuteNonQuery();
   }
  }
 }
