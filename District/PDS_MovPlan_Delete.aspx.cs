using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;


public partial class District_PDS_MovPlan_Delete : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCropYear.Items.Insert(0, "--Select--");
            ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1));
            ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year);
            ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1));
            GetDistName();
           
        }
        GetDistName();
       
    }

    public void GetDistName()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";

                //select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";

                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code='" + DistCode + "'";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txt_toDist.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
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


    public void GetMONumber()
    {
        string DistCode = Session["dist_id"].ToString();
        ddlMoveOrdNo.Items.Clear();

        if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Crop Year Number'); </script> ");
            return;
        }
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "select MoveOrdernum,SMO from StateMovementOrder where ToDist= '" + DistCode + "' and (RecAgainstHO='Y' and ((DATEADD(DAY,210,CreatedDate))>=Getdate())) and IsAccepted='Y' and IsIssued='N' and ModeofDispatch='12' ";
                                
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMoveOrdNo.DataSource = ds.Tables[0];
                    ddlMoveOrdNo.DataTextField = "MoveOrdernum";
                    ddlMoveOrdNo.DataValueField = "MoveOrdernum";
                    ddlMoveOrdNo.DataBind();
                    ddlMoveOrdNo.Items.Insert(0, "--Select--");

                   
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('MO Number Is Not Available'); </script> ");
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


    protected void ddlMoveOrdNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMoveOrdNo.SelectedIndex > 0)
        {
            GetData();
        }

    }

    public void GetData()
    {
         string DistCode = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";

                //select = "SELECT district_name,district_code FROM pds.districtsmp Order By district_name";

                select = "select D.district_name as FrmDist, D1.district_name as ToDist, Quantity, convert (varchar(10), MODate, 103) as MO_Date from RecAgainst_StateMovementOrder as MPMO inner join Pds.districtsmp as D on D.district_code=MPMO.FrmDist inner join pds.districtsmp as D1 on D1.district_code=MPMO.ToDist where ModeofDispatch='12' and MoveOrdernum='" + ddlMoveOrdNo.SelectedValue.ToString() + "' and ToDist='" + DistCode + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txt_FrmDist.Text = ds.Tables[0].Rows[0]["FrmDist"].ToString();
                        txt_todistF.Text = ds.Tables[0].Rows[0]["ToDist"].ToString();
                         txt_Quan.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                         txt_DOMP.Text = ds.Tables[0].Rows[0]["MO_Date"].ToString();
                         btnDelete.Enabled = true;
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlMoveOrdNo.SelectedIndex<=0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Movement order Number'); </script> ");
            return;
        }
        else if (ddlCropYear.SelectedIndex <= 0)
        {
            btnDelete.Enabled = false;
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CropYear|'); </script> ");
            return;
        }

        else
        {
           
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                             string DistCode = Session["dist_id"].ToString();
                            ClientIP objClientIP = new ClientIP();
                            string GetIp = (objClientIP.GETIP());

                           
                            con.Open();

                            string instr = "";

                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                            instr += "insert into RecAgainst_StateMovementOrder_Logs select MoveOrdernum ,SMO ,RMO, FrmDist, ToDist, Commodity ,Quantity, CropYear, ReachDate, MODate, ModeofDispatch, DispatchAgainstMO, ReceivedAgainstMO, CreatedDate, IP, Issue_Center, Branch, Godown, RequiredQuantity, RemQty, SubmitedQty, IssuedQty, SubSMO, ModeofDist, GETDATE(),'" + GetIp + "' from RecAgainst_StateMovementOrder where ModeofDispatch='12' and MoveOrdernum='" + ddlMoveOrdNo.SelectedValue.ToString() + "' and ToDist='" + DistCode + "'";
                            instr += "update StateMovementOrder set RecAgainstHO='N' where ModeofDispatch='12' and MoveOrdernum='" + ddlMoveOrdNo.SelectedValue.ToString() + "' and ToDist='" + DistCode + "'";

                            instr += "Delete From RecAgainst_StateMovementOrder where ModeofDispatch='12' and MoveOrdernum='" + ddlMoveOrdNo.SelectedValue.ToString() + "' and ToDist='" + DistCode + "'";

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnDelete.Enabled = false;
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry, Deletion Not Allow'); </script> ");
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
    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/MovementOrderHome.aspx");

    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex > 0)
        {
            GetMONumber();
        }
    }
}