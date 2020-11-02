using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GunnyBags_GunnyBags_Allocation : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    //  string strconuparjan = ConfigurationManager.ConnectionStrings["uparjan"].ConnectionString;      //uparjan
    public string gatepass = "";
    string Rates;
    public int getnum;
    SqlDataReader dr;

    double QtyTotal = 0;
    public string sid = "";

    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    double total = 0;
    double remainingRH = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_id"] != null)
        {
            string sid = Session["st_id"].ToString();
            if (!IsPostBack)
            {
                
                Session["qty"] = null;
                Session["fdjfhxncdfh"] = null;
                ViewState["Row"] = "0";
                FillIndent();
                GetDist();
            }
            
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

   public void FillIndent()
   {
     using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT distinct IndentNumber, IndentorName+' ('+IndentNumber+')' as Indent   FROM Gunny_Bags_Indent_Creation ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlIndent.DataSource = ds.Tables[0];
                        ddlIndent.DataTextField = "Indent";
                        ddlIndent.DataValueField = "IndentNumber";
                        ddlIndent.DataBind();
                        ddlIndent.Items.Insert(0, "--Select--");                        
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
   }

   public void GetDist()
   {
       using (con = new SqlConnection(strcon))
       {
           try
           {
               con.Open();

               string select = "";
               select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";
               da = new SqlDataAdapter(select, con);
               ds = new DataSet();
               da.Fill(ds);

               if (ds != null)
               {
                   if (ds.Tables[0].Rows.Count > 0)
                   {
                       ddlDistrict.DataSource = ds.Tables[0];
                       ddlDistrict.DataTextField = "district_name";
                       ddlDistrict.DataValueField = "district_code";
                       ddlDistrict.DataBind();
                       ddlDistrict.Items.Insert(0, "--Select--");                
                   }
               }
           }
           catch (Exception ex)
           {
               Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
           }

           finally
           {
               if (con.State != ConnectionState.Closed)
               {
                   con.Close();
               }
           }
       }
   }
   protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        string indent = ddlIndent.SelectedValue.ToString();
        string railHead = ddlRailHead.SelectedValue.ToString();
        string quantityRailHead = txtQuantity.Text;
        string districtID = ddlDistrict.SelectedValue.ToString();
        
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select god.Godown_Name  as Godown , god.Godown_ID  from tbl_MetaData_GODOWN god  where god.DistrictId ='" + districtID + "' and (god.Remarks <> 'N' or god.Remarks is null)  and god.Godown_ID not in ( select IGA.godownId from GunnyBags_IndentToGodownAllocation IGA  where IGA.districtID = '" + districtID + "'  and IGA.indentNumber ='" + indent + "' and IGA.railHead ='" + railHead + "' ) order by god.Godown_Name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    protected void bttAdd_Click(object sender, EventArgs e)
    {
        if (Session["qty"] != null)
        {
           // total = Convert.ToDouble(Session["qty"]);
           // remainingRH = Convert.ToDouble(Session["remainingRH"]);

            double va = Convert.ToDouble(txtQuantity.Text) - total;

            if (total == Convert.ToDouble(txtQuantity.Text))
            {
                bttAdd.Enabled = false;
                ViewState["Row"] = "15";
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Rail Head quantity distribution for godown allocation is completed. You Can Not Add Another Godown.'); </script> ");
            }
                //  else if (Convert.ToDouble(txtGodownQuantity.Text) > remainingRH)
            else if (Convert.ToDouble(txtGodownQuantity.Text) > va )
            {
                
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('रेमैनिंग रेल हेड क्वांटिटी से ज्यादा का एलोकेशन नही किया जा सकता|'); </script> ");
                return;
            }
            else
            {
                getData();
            }

        }
        else
        {
            getData();
        }

        
    }

    public void getData()
    {
     if (ddlIndent.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Indent'); </script> ");
            return;
        }
        else if (ddlRailHead.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Rail Head Destination'); </script> ");
            return;
        }
        else if (txtFund.Text =="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Fund'); </script> ");
            return;
        }

        else if (txtQuantity.Text =="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Indent Quantity'); </script> ");
            return;
        }

        else if (txtRailHeadDate.Text =="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Rail Head Date '); </script> ");
            return;
        }
        else if (ddlDistrict.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select District'); </script> ");
            return;
        }        
       
        else if (ddlGodown.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select godown'); </script> ");
            return;
        }
         else if (txtGodownQuantity.Text == "")
         {
             Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Quantity '); </script> ");
             return;
         }
         else
         {
             trGrid.Visible = true;
             DataTable dt = adddetails();
             if (dt == null)
             {
                 dt = new DataTable("aadqty");
                 dt.Columns.Add("District");                 
                 dt.Columns.Add("Godown");
                 dt.Columns.Add("Quantity");
                 dt.Columns.Add("QuantityinPercent");
                 dt.Columns.Add("hfGodownDistrictID");
                 dt.Columns.Add("hfGodownID");
             }

       
             string id = ddlGodown.SelectedValue.ToString();
             //priyanka
             DataTable dtTable = dt;
            

             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 if (dt.Rows[i]["hfGodownID"].ToString() == id)
                 {
                     Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Selected godown already assigned for a selected indent.'); </script> ");
                     return;
                 }
             }
            
                 DataRow dr = dt.NewRow();
                 dr["District"] = ddlDistrict.SelectedItem.Text;                
                 dr["Godown"] = ddlGodown.SelectedItem.ToString();
                 dr["Quantity"] = txtGodownQuantity.Text;
                 dr["QuantityinPercent"] = ((Convert.ToDecimal(txtGodownQuantity.Text) * 100) / Convert.ToDecimal(txtQuantity.Text)).ToString("0.00");
                 dr["hfGodownDistrictID"] = ddlDistrict.SelectedValue.ToString();
                 dr["hfGodownID"] = ddlGodown.SelectedValue.ToString();
                 dt.Rows.Add(dr);
           


            
             Session["fdjfhxncdfh"] = dt;
             fillgrid();
             ddlDistrict.SelectedIndex = 0;           
             ddlGodown.SelectedIndex = 0;
             txtGodownQuantity.Enabled = true;
             txtGodownQuantity.Text = "";
         }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string indent = ddlIndent.SelectedValue.ToString();
        string railHead = ddlRailHead.SelectedValue.ToString();
        string quantityRailHead = txtQuantity.Text;
        string districtID = ddlDistrict.SelectedValue.ToString();       
        string godownID = ddlGodown.SelectedValue.ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "select *  from GunnyBags_IndentToGodownAllocation where indentNumber ='" + indent + "' and railHead ='" + railHead + "'  and districtID='" + districtID + "'  and godownId ='" + godownID + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtGodownQuantity.Enabled = false;
                        bttAdd.Enabled = false;
                    }
                    else
                    {
                        txtGodownQuantity.Enabled = true;
                        bttAdd.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    protected void bttSubmit_Click(object sender, EventArgs e)
    {
        DataTable dtpage = adddetails();

        if (ddlIndent.SelectedIndex < 0)
        {

            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Indent'); </script> ");
            return;
        }
        else if (ddlRailHead.SelectedIndex < 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Rail Head Destination'); </script> ");
            return;
        }
        else if (txtFund.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Fund'); </script> ");
            return;
        }

        else if (txtQuantity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Indent Quantity'); </script> ");
            return;
        }

        else if (txtRailHeadDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter Rail Head Date '); </script> ");
            return;
        }
        
        else if(dtpage.Rows.Count < 1)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please do allocation for indent... '); </script> ");
            return;
        }
       
        else 
        {
        using (con = new SqlConnection(strcon))
            try
            {
                string indent = ddlIndent.SelectedValue.ToString();
                string railHead = ddlRailHead.SelectedValue.ToString();
                string fundRailHead = txtFund.Text;               
                string railHeadQuantity = txtQuantity.Text;
            
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                con.Open();
             //   string qrey = "select *  from GunnyBags_IndentToGodownAllocation  where  indentNumber ='" + indent + "' and railHead= '" + railHead + "' and districtID= '" + districtId + "' and issueCenterId= '" + issueCenterId + "' and branchId= '" + branchId + "' and godownId= '" + godownId + "'";
                string qrey = "select * from GunnyBags_IndentToGodownAllocation  where  indentNumber ='" + indent + "' and railHead= '" + railHead + "'";
                da = new SqlDataAdapter(qrey, con);
                ds = new DataSet();
                da.Fill(ds);
               
                           
                if (ds.Tables[0].Rows.Count > 0)
                {
                    qrey = "select sum(godownQuantity) godownQuantity from GunnyBags_IndentToGodownAllocation  where  indentNumber ='" + indent + "' and railHead= '" + railHead + "'";
                    da = new SqlDataAdapter(qrey, con);
                    ds = new DataSet();
                    da.Fill(ds);
                    double godownQuantityDB = ds.Tables[0].Rows[0]["godownQuantity"] == null ? 0 : Convert.ToDouble(ds.Tables[0].Rows[0]["godownQuantity"]);

                    if (godownQuantityDB == Convert.ToDouble(railHeadQuantity))
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Indent allocation is Fully done'); </script> ");
                        return;
                    }
                    else //indent allocation is partially done.
                    { 
                        DataTable dt = adddetails();
                        string strinsert = "";

                        if (dt != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                qrey = "select * from GunnyBags_IndentToGodownAllocation  where  indentNumber ='" + indent + "' and railHead= '" + railHead + "' and districtID ='" + dt.Rows[i]["hfGodownDistrictID"].ToString() + "' and godownId ='" + dt.Rows[i]["hfGodownID"].ToString() + "'  ";
                                da = new SqlDataAdapter(qrey, con);                                
                                da.Fill(ds);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    //update allocation
                                    ds.Clear();
                                    //strinsert = "update GunnyBags_IndentToGodownAllocation set godownQuantity = '" + godownQuantity + "' , updatedDate = '" + System.DateTime.Now + "',IP='" + ip + "'  where  indentNumber ='" + indent + "' and railHead= '" + railHead + "' and districtID ='" + dt.Rows[i]["District"].ToString() + "' and godownId ='" + dt.Rows[i]["GodownId"].ToString() + "'  ";
                                    //cmd = new SqlCommand(strinsert, con);
                                    //cmd.ExecuteNonQuery();
                                }
                                else
                                { 
                                   //insert new allocation
                                    string getDistrict = "";
                                    string getGodownId = "";
                                    getDistrict = dt.Rows[i]["hfGodownDistrictID"].ToString();
                                    getGodownId = dt.Rows[i]["hfGodownID"].ToString();

                                    //SqlDataReader rdr;
                                    //string DistRowValue = dt.Rows[i]["District"].ToString();
                                    //string GodownRowValue = dt.Rows[i]["Godown"].ToString();
                                    
                                    //SqlCommand getData = new SqlCommand("select (select district_code from pds.districtsmp where district_name ='" + DistRowValue + "') GodownDistrict,(select Godown_ID from tbl_MetaData_GODOWN where Godown_Name ='" + GodownRowValue + "') Godown_ID", con);
                                    //SqlDataReader dr = getData.ExecuteReader();
                                    

                                    //if (dr.HasRows)
                                    //{
                                    //    getDistrict = dr.GetValue(0).ToString();
                                    //    getGodownId = dr.GetValue(1).ToString();

                                    //}
                                    string sid = Session["st_id"].ToString();
                                    strinsert = "insert into GunnyBags_IndentToGodownAllocation(indentNumber, railHead, fundRailHead, quantityRailHead, districtID, godownId, godownQuantity, godownQuantityPercentage,createdDate, updatedDate, userID, IP) values ('" + indent + "','" + railHead + "','" + fundRailHead + "','" + railHeadQuantity + "','" + getDistrict + "','" + getGodownId + "','" + dt.Rows[i]["Quantity"].ToString() + "','" + dt.Rows[i]["QuantityinPercent"].ToString() + "','" + System.DateTime.Now + "','" + System.DateTime.Now + "','" + sid + "','" + ip + "')";
                                    cmd = new SqlCommand(strinsert, con);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Indent allocation is done'); </script> ");
                    }
                }
                else //Insert if database have no indent railhead allocation
                {
                    DataTable dt = adddetails();
                    string strinsert = "";

                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string getDistrict = "";
                            string getGodownId = "";
                            getDistrict = dt.Rows[i]["hfGodownDistrictID"].ToString();
                            getGodownId = dt.Rows[i]["hfGodownID"].ToString();

                            //SqlDataReader rdr; 
                            //string DistRowValue = dt.Rows[i]["District"].ToString();
                            //string GodownRowValue = dt.Rows[i]["Godown"].ToString();
                         
                            //SqlCommand getData = new SqlCommand("select (select district_code from pds.districtsmp where district_name ='" + DistRowValue + "') GodownDistrict,(select Godown_ID from tbl_MetaData_GODOWN where Godown_Name ='" + GodownRowValue + "') Godown_ID",con);
                            //SqlDataReader dr = getData.ExecuteReader();
                           

                            //if(dr.Read())
                            //{
                            //    getDistrict = dr["GodownDistrict"].ToString();
                            //    getGodownId = dr["Godown_ID"].ToString();
                           
                            //}



                            string sid = Session["st_id"].ToString();
                            strinsert = "insert into GunnyBags_IndentToGodownAllocation(indentNumber, railHead, fundRailHead, quantityRailHead, districtID, godownId, godownQuantity, godownQuantityPercentage,createdDate, updatedDate, userID, IP) values ('" + indent + "','" + railHead + "','" + fundRailHead + "','" + railHeadQuantity + "','" + getDistrict + "','" + getGodownId + "','" + dt.Rows[i]["Quantity"].ToString() + "','" + dt.Rows[i]["QuantityinPercent"].ToString() + "','" + System.DateTime.Now + "','" + System.DateTime.Now + "','" + sid + "','" + ip + "')";
                            //dr.Close();
                            cmd = new SqlCommand(strinsert, con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Indent allocation is done'); </script> ");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex + "'); </script> ");
            }
        }

    }
   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            QtyTotal = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            QtyTotal += (double.Parse(e.Row.Cells[3].Text));

            total = QtyTotal;
           
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Total";
            e.Row.Cells[2].Text = QtyTotal.ToString();

            e.Row.Cells[3].Text = "Remaining RailHad Quantity";
            e.Row.Cells[4].Text = Convert.ToString(Convert.ToDouble(txtQuantity.Text) - Convert.ToDouble(QtyTotal));
           
           
        }

       Session["qty"] = QtyTotal;
       total = QtyTotal;
       remainingRH = Convert.ToDouble(txtQuantity.Text) - Convert.ToDouble(QtyTotal);
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string indent = ddlIndent.SelectedValue.ToString();
        string railHead = ddlRailHead.SelectedValue.ToString();
        string fundRailHead = txtFund.Text;
        string railHeadQuantity = txtQuantity.Text;
        string districtId = ddlDistrict.SelectedValue.ToString();      
        string godownId = ddlGodown.SelectedValue.ToString();
        string godownQuantity = txtGodownQuantity.Text;
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        DataTable dt = adddetails();
        if (dt == null)
        {
            dt = new DataTable("aadqty");
            dt.Columns.Add("District");            
            dt.Columns.Add("Godown");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("QuantityinPercent");
            dt.Columns.Add("hfGodownDistrictID");
            dt.Columns.Add("hfGodownID");
        }
        else
        {
            SqlDataReader rdr;
            string DistRowValue = dt.Rows[e.RowIndex]["hfGodownDistrictID"].ToString();
            string GodownRowValue = dt.Rows[e.RowIndex]["hfGodownID"].ToString();
            //priyanka
            

            con = new SqlConnection(strcon);

            string getDistrict = "";
            string getGodownId = "";
            getDistrict = dt.Rows[e.RowIndex]["hfGodownDistrictID"].ToString();
            getGodownId = dt.Rows[e.RowIndex]["hfGodownID"].ToString();

           
              

                String qrey = "select * from GunnyBags_IndentToGodownAllocation  where  indentNumber ='" + indent + "' and railHead= '" + railHead + "' and districtID ='" + getDistrict + "' and godownId ='" + getGodownId + "'  ";
                da = new SqlDataAdapter(qrey, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string strinsert = "insert into GunnyBags_IndentToGodownAllocation_Log(allocationID,indentNumber, railHead, fundRailHead, quantityRailHead, districtID, godownId, godownQuantity, godownQuantityPercentage,createdDate, updatedDate, userID, IP) values ('" + ds.Tables[0].Rows[0]["allocationID"].ToString() + "','" + indent + "','" + railHead + "','" + fundRailHead + "','" + railHeadQuantity + "','" + ds.Tables[0].Rows[0]["districtID"].ToString() + "','" + ds.Tables[0].Rows[0]["GodownId"].ToString() + "','" + ds.Tables[0].Rows[0]["godownQuantity"].ToString() + "', '" + ds.Tables[0].Rows[0]["godownQuantityPercentage"].ToString() + "','" + ds.Tables[0].Rows[0]["createdDate"] + "','" + ds.Tables[0].Rows[0]["updatedDate"] + "',null,'" + ip + "')";
                    cmd = new SqlCommand(strinsert, con);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    String strdelete = "delete from GunnyBags_IndentToGodownAllocation where indentNumber ='" + indent + "' and railHead= '" + railHead + "' and districtID ='" + getDistrict + "' and godownId ='" + getGodownId + "'  ";
                    cmd = new SqlCommand(strdelete, con);
                    cmd.ExecuteNonQuery();
                }
                dt.Rows.RemoveAt(e.RowIndex);
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Record Deleted Succesfully...'); </script> ");
        }


        double sum = 0;
        foreach (DataRow dr in dt.Rows)
        {
            sum += Convert.ToDouble(dr["Quantity"]);
        }

        //double drs = Convert.ToDouble(dt.Compute("Sum(Quantity)", "[Quantity] IS NOT NULL"));
        Session["qty"] = sum;
        if (sum < Convert.ToDouble(txtQuantity.Text))
        {
            bttAdd.Enabled = true;
        }

        
        Session["fdjfhxncdfh"] = dt;
        dt.Clear();
        fillgrid();
    }

    public DataTable adddetails()
    {
        
        DataTable dt = (DataTable)Session["fdjfhxncdfh"];
        return dt;
    }

    public void fillgrid()
    {
        DataTable dt = adddetails();
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            trGrid.Visible = true;
            GridView1.DataSource = dt;
            //GridView1.Columns[3].HeaderText = ddlComdtyMode.SelectedItem.ToString();
            GridView1.DataBind();
        }
    }
    protected void ddlIndent_SelectedIndexChanged(object sender, EventArgs e)
    {       
     GetRailHead();

     txtFund.Text = "";
     txtQuantity.Text = "";
     txtRailHeadDate.Text = "";

     ddlDistrict.SelectedIndex = 0;
     ddlGodown.Items.Clear();
     txtGodownQuantity.Text = "";
     GridView1.DataSource = null;
     GridView1.DataBind();
     Session["fdjfhxncdfh"] = null;
    }

    public void GetRailHead()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string indent = ddlIndent.SelectedValue.ToString();
                string select = "";
                select = "select ic.indentnumber,RH.RailHead_Code,rh.RailHead_Name + ' ('+RH.RailHead_Code +')' RailHead,IC.RailHead_Destination from tbl_Rail_Head RH  inner join Gunny_Bags_Indent_Creation IC on rh.RailHead_Code = IC.RailHead_Destination where ic.indentnumber='"+indent+"' order by RailHead_Name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlRailHead.DataSource = ds.Tables[0];
                        ddlRailHead.DataTextField = "RailHead";
                        ddlRailHead.DataValueField = "RailHead_Code";
                        ddlRailHead.DataBind();
                        ddlRailHead.Items.Insert(0, "--Select--");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected void ddlRailHead_SelectedIndexChanged(object sender, EventArgs e)
    {       
     GetRailHeadDetails();

     FillRailHeadGrid();
     //Session["fdjfhxncdfh"] = null;
    }

    public void FillRailHeadGrid()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string indent = ddlIndent.SelectedValue.ToString();
                string railHead = ddlRailHead.SelectedValue.ToString();
                string select = "";
                select = "select * from GunnyBags_IndentToGodownAllocation GBIA  where GBIA.indentnumber='" + indent + "' and GBIA.RailHead='" + railHead + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable("aadqty");
                        dt.Columns.Add("District");
                        dt.Columns.Add("Godown");
                        dt.Columns.Add("Quantity");
                        dt.Columns.Add("QuantityinPercent");
                        dt.Columns.Add("hfGodownDistrictID");
                        dt.Columns.Add("hfGodownID");

                        for(int i=0; i<=ds.Tables[0].Rows.Count-1; i++)
                        {
                            DataRow dr = dt.NewRow();

                            select = "select (select district_name from pds.districtsmp where district_code ='" + ds.Tables[0].Rows[i]["districtID"].ToString() + "') GodownDistrict,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID ='" + ds.Tables[0].Rows[i]["godownID"].ToString() + "') GodownName";
                            DataSet dsGet = new DataSet();
                            da = new SqlDataAdapter(select, con);
                            da.Fill(dsGet);

                            dr["District"] = dsGet.Tables[0].Rows[0]["GodownDistrict"].ToString();
                            dr["Godown"] = dsGet.Tables[0].Rows[0]["GodownName"].ToString();
                            dr["Quantity"] = ds.Tables[0].Rows[i]["godownQuantity"].ToString();
                            dr["QuantityinPercent"] = ds.Tables[0].Rows[i]["godownQuantityPercentage"].ToString();
                            dr["hfGodownDistrictID"] = ds.Tables[0].Rows[i]["districtID"].ToString();
                            dr["hfGodownID"] = ds.Tables[0].Rows[i]["godownID"].ToString();
                            dt.Rows.Add(dr);

                        }
                        Session["fdjfhxncdfh"] = dt;
                        fillgrid();
                        ddlDistrict.SelectedIndex = 0;
                        ddlGodown.SelectedIndex = 0;
                        txtGodownQuantity.Enabled = true;
                        txtGodownQuantity.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    public void GetRailHeadDetails()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string indent = ddlIndent.SelectedValue.ToString();
                string railHead = ddlRailHead.SelectedValue.ToString();
                string select = "";
                select = "select ic.indentnumber,RH.RailHead_Code,rh.RailHead_Name + ' ('+RH.RailHead_Code +')' RailHead,IC.RailHead_Destination,IC.Fund_Required,IC.Quantity,IC.DeliveryDate from tbl_Rail_Head RH inner join Gunny_Bags_Indent_Creation IC on rh.RailHead_Code = IC.RailHead_Destination where ic.indentnumber='"+indent+"' and IC.RailHead_Destination='"+railHead+"'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtFund.Text = ds.Tables[0].Rows[0]["Fund_Required"].ToString();
                        txtQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                        txtRailHeadDate.Text =Convert.ToDateTime(ds.Tables[0].Rows[0]["DeliveryDate"]).ToShortDateString();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
}