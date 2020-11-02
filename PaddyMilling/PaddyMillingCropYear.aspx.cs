using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class PaddyMilling_PaddyMillingCropYear : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            if (!IsPostBack)
            {
                ddlCropYear.Items.Insert(0, "--Select--");
                ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
                ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
                ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));

                ViewState["User_Name"] = Session["st_Name"].ToString();

                GETLotNo();
                GETCMRPercentNo();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    public void GETLotNo()
    {
        for (int i = 5; i <= 50; i++)
        {
            ddlLot.Items.Add(i.ToString());
        }
        ddlLot.Items.Insert(0, "--Select--");
    }

    public void GETCMRPercentNo()
    {
        for (int i = 10; i <= 100; i++)
        {
            ddlReturnCMR.Items.Add(i.ToString());
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlLot.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया लॉट की सिक्यूरिटी राशि चुनें|'); </script> ");
            return;
        }
        else if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Paddy Milling CropYear'); </script> ");
            return;
        }
        else
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();
                    string strselect = string.Format("Select CropYear from PaddyMilling_CropYear where CropYear = '{0}'", ddlCropYear.SelectedItem.ToString());
                    cmd = new SqlCommand(strselect, con);
                    string check = (string)cmd.ExecuteScalar();

                    if (check == null)
                    {
                        string GetIp = Request.ServerVariables["REMOTE_ADDR"].ToString();

                        string browser = Request.Browser.Browser.ToString();
                        string version = Request.Browser.Version.ToString();
                        string useragent = browser + version;

                        string instr = string.Format("Insert into PaddyMilling_CropYear(CropYear,Arva,Ushna_First3,Ushna_After3,IP_Address,Current_DateTime,User_Agent,Deposit_Money,Dist_Manager,Panility,ArvaChawal,UshnaChawal,Common_Dhan_Rs,GradeA_Dhan_Rs,TotaGA,TotaS,ChoteToteGA,ChoteToteS,VijatiyeGA,VijatiyeS,DamageDaaneGA,DamageDaaneS,BadrangDaaneGA,BadrangDaaneS,ChaakiDaaneGA,ChaakiDaaneS,LaalDaaneGA,LaalDaaneS,OtherGA,OtherS,ChokarDaaneGA,ChokarDaaneS,NamiGA,NamiS,Paddy_SecurityLot) values ('{0}',{1},{2},{3},'{4}',{5},'{6}',{7},'{8}',{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},'" + ddlLot.SelectedItem.ToString() + "')", ddlCropYear.SelectedItem.ToString(), txtArva.Text, txtUshnaFirst3.Text, txtUshnaAfter3.Text, GetIp.ToString(), "GETDATE()", useragent, txtDepositMoney.Text, ViewState["User_Name"].ToString(), txtPanility.Text, txtArvaChawal.Text, txtUshnaChawal.Text, txtCommonDhanRs.Text, txtGradeADhanRs.Text, TxtTotaGA.Text, TxtTotaS.Text, TxtChoteToteGA.Text, TxtChoteToteS.Text, txtVijatiyeGA.Text, txtVijatiyeS.Text, txtDamageDaaneGA.Text, txtDamageDaaneS.Text, txtBadrangDaaneGA.Text, txtBadrangDaaneS.Text, txtChaakiDaaneGA.Text, txtChaakiDaaneS.Text, txtLaalDaaneGA.Text, txtLaalDaaneS.Text, txtOtherGA.Text, txtOtherS.Text, txtChokarDaaneGA.Text, txtChokarDaaneS.Text, txtNamiGA.Text, txtNamiS.Text);

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully'); </script> ");
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
                        }
                    }
                    else
                    {
                        string GetIp = Request.ServerVariables["REMOTE_ADDR"].ToString();

                        string browser = Request.Browser.Browser.ToString();
                        string version = Request.Browser.Version.ToString();
                        string useragent = browser + version;
                        string instr = "";

                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";
                        instr += "Insert Into PaddyMilling_CropYear_Log(CropYear,Arva,Ushna_First3,Ushna_After3,IP_Address,Current_DateTime,User_Agent,Deposit_Money,Dist_Manager,Panility,ArvaChawal,UshnaChawal,Common_Dhan_Rs,GradeA_Dhan_Rs,TotaGA,TotaS,ChoteToteGA,ChoteToteS,VijatiyeGA,VijatiyeS,DamageDaaneGA,DamageDaaneS,BadrangDaaneGA,BadrangDaaneS,ChaakiDaaneGA,ChaakiDaaneS,LaalDaaneGA,LaalDaaneS,OtherGA,OtherS,ChokarDaaneGA,ChokarDaaneS,NamiGA,NamiS,Paddy_SecurityLot,UpdatedDate,UpdatedIP,ReturnAgrmtCMR_Percent) Select CropYear,Arva,Ushna_First3,Ushna_After3,IP_Address,Current_DateTime,User_Agent,Deposit_Money,Dist_Manager,Panility,ArvaChawal,UshnaChawal,Common_Dhan_Rs,GradeA_Dhan_Rs,TotaGA,TotaS,ChoteToteGA,ChoteToteS,VijatiyeGA,VijatiyeS,DamageDaaneGA,DamageDaaneS,BadrangDaaneGA,BadrangDaaneS,ChaakiDaaneGA,ChaakiDaaneS,LaalDaaneGA,LaalDaaneS,OtherGA,OtherS,ChokarDaaneGA,ChokarDaaneS,NamiGA,NamiS,Paddy_SecurityLot,GETDATE(),'" + GetIp + "',ReturnAgrmtCMR_Percent From PaddyMilling_CropYear  Where CropYear='" + ddlCropYear.SelectedItem.ToString() + "' ;";
                        instr += "Update PaddyMilling_CropYear set Arva='" + txtArva.Text + "',Ushna_First3='" + txtUshnaFirst3.Text + "',Ushna_After3='" + txtUshnaAfter3.Text + "',Deposit_Money='" + txtDepositMoney.Text + "',Dist_Manager='" + ViewState["User_Name"].ToString() + "',Panility='" + txtPanility.Text + "',ArvaChawal='" + txtArvaChawal.Text + "',UshnaChawal='" + txtUshnaChawal.Text + "',Common_Dhan_Rs='" + txtCommonDhanRs.Text + "',GradeA_Dhan_Rs='" + txtGradeADhanRs.Text + "',TotaGA='" + TxtTotaGA.Text + "',TotaS='" + TxtTotaS.Text + "',ChoteToteGA='" + TxtChoteToteGA.Text + "',ChoteToteS='" + TxtChoteToteS.Text + "',VijatiyeGA='" + txtVijatiyeGA.Text + "',VijatiyeS='" + txtVijatiyeS.Text + "',DamageDaaneGA='" + txtDamageDaaneGA.Text + "',DamageDaaneS='" + txtDamageDaaneS.Text + "',BadrangDaaneGA='" + txtBadrangDaaneGA.Text + "',BadrangDaaneS='" + txtBadrangDaaneS.Text + "',ChaakiDaaneGA='" + txtChaakiDaaneGA.Text + "',ChaakiDaaneS='" + txtChaakiDaaneS.Text + "',LaalDaaneGA='" + txtLaalDaaneGA.Text + "',LaalDaaneS='" + txtLaalDaaneS.Text + "',OtherGA='" + txtOtherGA.Text + "',OtherS='" + txtOtherS.Text + "',ChokarDaaneGA='" + txtChokarDaaneGA.Text + "',ChokarDaaneS='" + txtChokarDaaneS.Text + "',NamiGA='" + txtNamiGA.Text + "',NamiS='" + txtNamiS.Text + "',Paddy_SecurityLot='" + ddlLot.SelectedItem.ToString() + "',UpdatedDate=GETDATE(),UpdatedIP='" + GetIp + "',ReturnAgrmtCMR_Percent='" + ddlReturnCMR.SelectedItem.ToString() + "' where CropYear='" + ddlCropYear.SelectedItem.ToString() + "' ;";
                        instr += " COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                        cmd = new SqlCommand(instr, con);
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Updated Successfully'); </script> ");
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('!!!!Failed'); </script> ");
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
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/State/Paddy_Welcome.aspx");
    }

    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtArva.Text = txtUshnaFirst3.Text = txtUshnaAfter3.Text = txtDepositMoney.Text = txtPanility.Text = txtArvaChawal.Text = txtUshnaChawal.Text = txtCommonDhanRs.Text = txtGradeADhanRs.Text = TxtTotaGA.Text = TxtTotaS.Text = TxtChoteToteGA.Text = TxtChoteToteS.Text = txtVijatiyeGA.Text = txtVijatiyeS.Text = txtDamageDaaneGA.Text = txtDamageDaaneS.Text = txtBadrangDaaneGA.Text = txtBadrangDaaneS.Text = txtChaakiDaaneGA.Text = txtChaakiDaaneS.Text = txtLaalDaaneGA.Text = txtLaalDaaneS.Text = txtOtherGA.Text = txtOtherS.Text = txtChokarDaaneGA.Text = txtChokarDaaneS.Text = txtNamiGA.Text = txtNamiS.Text = "";
        ddlLot.SelectedIndex = ddlReturnCMR.SelectedIndex = 0;

        if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Paddy Milling CropYear'); </script> ");
            return;
        }
        else
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();
                    string select = string.Format("Select * from PaddyMilling_CropYear where CropYear = '{0}'", ddlCropYear.SelectedItem.ToString());
                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds, "PaddyMilling_CropYear");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtArva.Text = ds.Tables[0].Rows[0][1].ToString();
                        txtUshnaFirst3.Text = ds.Tables[0].Rows[0][2].ToString();
                        txtUshnaAfter3.Text = ds.Tables[0].Rows[0][3].ToString();
                        txtDepositMoney.Text = ds.Tables[0].Rows[0][7].ToString();
                        txtPanility.Text = ds.Tables[0].Rows[0][9].ToString();
                        txtArvaChawal.Text = ds.Tables[0].Rows[0][10].ToString();
                        txtUshnaChawal.Text = ds.Tables[0].Rows[0][11].ToString();
                        txtCommonDhanRs.Text = ds.Tables[0].Rows[0][12].ToString();
                        txtGradeADhanRs.Text = ds.Tables[0].Rows[0][13].ToString();

                        TxtTotaGA.Text = ds.Tables[0].Rows[0][14].ToString();
                        TxtTotaS.Text = ds.Tables[0].Rows[0][15].ToString();
                        TxtChoteToteGA.Text = ds.Tables[0].Rows[0][16].ToString();
                        TxtChoteToteS.Text = ds.Tables[0].Rows[0][17].ToString();
                        txtVijatiyeGA.Text = ds.Tables[0].Rows[0][18].ToString();
                        txtVijatiyeS.Text = ds.Tables[0].Rows[0][19].ToString();
                        txtDamageDaaneGA.Text = ds.Tables[0].Rows[0][20].ToString();
                        txtDamageDaaneS.Text = ds.Tables[0].Rows[0][21].ToString();
                        txtBadrangDaaneGA.Text = ds.Tables[0].Rows[0][22].ToString();
                        txtBadrangDaaneS.Text = ds.Tables[0].Rows[0][23].ToString();
                        txtChaakiDaaneGA.Text = ds.Tables[0].Rows[0][24].ToString();
                        txtChaakiDaaneS.Text = ds.Tables[0].Rows[0][25].ToString();
                        txtLaalDaaneGA.Text = ds.Tables[0].Rows[0][26].ToString();
                        txtLaalDaaneS.Text = ds.Tables[0].Rows[0][27].ToString();
                        txtOtherGA.Text = ds.Tables[0].Rows[0][28].ToString();
                        txtOtherS.Text = ds.Tables[0].Rows[0][29].ToString();
                        txtChokarDaaneGA.Text = ds.Tables[0].Rows[0][30].ToString();
                        txtChokarDaaneS.Text = ds.Tables[0].Rows[0][31].ToString();
                        txtNamiGA.Text = ds.Tables[0].Rows[0][32].ToString();
                        txtNamiS.Text = ds.Tables[0].Rows[0][33].ToString();

                        string SecurityLot = ds.Tables[0].Rows[0]["Paddy_SecurityLot"].ToString();
                        string ReturnCMRPercent = ds.Tables[0].Rows[0]["ReturnAgrmtCMR_Percent"].ToString();

                        ddlLot.SelectedIndex = ddlReturnCMR.SelectedIndex = 0;
                        if (SecurityLot != "")
                        {
                            ddlLot.SelectedValue = SecurityLot;
                        }

                        if (ReturnCMRPercent != "")
                        {
                            ddlReturnCMR.SelectedValue = ReturnCMRPercent;
                        }


                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Already Available For These CropYear...'); </script> ");
                    }
                    else
                    {
                        txtArva.Text = txtUshnaFirst3.Text = txtUshnaAfter3.Text = txtDepositMoney.Text = txtPanility.Text = txtArvaChawal.Text = txtUshnaChawal.Text = txtCommonDhanRs.Text = txtGradeADhanRs.Text = TxtTotaGA.Text = TxtTotaS.Text = TxtChoteToteGA.Text = TxtChoteToteS.Text = txtVijatiyeGA.Text = txtVijatiyeS.Text = txtDamageDaaneGA.Text = txtDamageDaaneS.Text = txtBadrangDaaneGA.Text = txtBadrangDaaneS.Text = txtChaakiDaaneGA.Text = txtChaakiDaaneS.Text = txtLaalDaaneGA.Text = txtLaalDaaneS.Text = txtOtherGA.Text = txtOtherS.Text = txtChokarDaaneGA.Text = txtChokarDaaneS.Text = txtNamiGA.Text = txtNamiS.Text = "";
                        ddlLot.SelectedIndex = 0;
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
}
