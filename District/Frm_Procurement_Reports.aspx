<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Frm_Procurement_Reports.aspx.cs" Inherits="District_Frm_Procurement_Reports" Title="Uparjan Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div >
    <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
        <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;" 
            border="1" cellpadding="0" cellspacing="0" width="800px">
        <tr>
            <td align="center" 
                style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;" 
                colspan="3">
                Reports</td>
        </tr>
        <tr>
            <td align="center" 
                style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;" 
                colspan="3">
                
            </td>
        </tr>
          <tr>
            <td align="left">&nbsp;</td>
            <td align="left" style="text-align: center">
                <b>Procurement(Uparjan)Reports</b></td>
           <td align="left">
            <asp:LinkButton ID="bakbtn" runat="server" ForeColor="Navy" PostBackUrl="~/District/FrmReports_Districts.aspx">Back</asp:LinkButton>
                </td>
        </tr>
        
       <tr>
            <td align="left">
                1</td>
            <td align="left">
           <asp:LinkButton ID="lnkaccepnote" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/AcceptNote_Dist.aspx">Acceptance Note Detail</asp:LinkButton> 
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
       <tr>
            <td align="left">
                2</td>
            <td align="left">
        
        <%--<tr>
            <td align="left">
                24.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton18" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/frm_rpt_CntrwisePrcTrns.aspx">Wheat Procured and Transported Rabi 2013-14</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                25.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton19" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/frm_rpt_Procurment_Commodity_CntrWis.aspx">Paddy and Coarse grain Procured and Transported-2013-14</asp:LinkButton></td>
            <td>
            </td>
        </tr>--%><asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Navy"
                    PostBackUrl="~/ReportForms_District/rpt_storagewiseDetail.aspx">Storage wise Deposit Report</asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
        
       <tr>
            <td align="left">
                3</td>
            <td align="left">
                <asp:LinkButton ID="lnkdelaccept" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/RptDist_DeleteAcceptance_Receipt.aspx">Deleted Acceptance and Receipt Report</asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
        
      <%-- <tr>
            <td align="left">
                4</td>
            <td align="left">
                <asp:LinkButton ID="lnktrucrec_drill0" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/Rpt_PaddyMillingStatus.aspx">Paddy Milling status Report</asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
        --%>
       <tr>
            <td align="left">
                4</td>
            <td align="left">
                <asp:LinkButton ID="lnlrackproc" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/Rpt_Rack_frmProcurement.aspx">Receive at Rack send from Procurement</asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
        
       <tr>
            <td align="left">
                5</td>
            <td align="left">
                <asp:LinkButton ID="lnktrucrec_drill" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/TruckRecevingDetails_DistLevel.aspx">Truck Received details from Procurement</asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
        
       <tr>
            <td align="left">
                6</td>
            <td align="left">
                <asp:LinkButton ID="lnktrucrec_drill1" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/DetailProc.aspx">Procurement Detail between two dates</asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
            <tr>
                <td align="left">
                    7</td>
                <td align="left">
                
                <asp:LinkButton ID="lnlproc" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/Proc_frmOtherDist.aspx">Deposited from Other District</asp:LinkButton>
                
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left">8</td>
                <td align="left">
                    <asp:LinkButton ID="Stckdtl" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/Rpt_CropYearWise_Stock_Detals.aspx">Crop Year wise Commodity Stock Details(New) </asp:LinkButton>
                </td>
                <td>&nbsp;</td>
            </tr>
           <tr>
                <td align="left">
                    9&nbsp;</td>
                <td align="left">
                    <asp:LinkButton ID="lnkreject" runat="server" ForeColor="Navy" 
                        PostBackUrl="~/ReportForms_District/RejectDetails.aspx">Rejection trucks Details</asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left">&nbsp;</td>
                <td align="left">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left">&nbsp;</td>
                <td align="center" style="color: #FF0000; text-decoration: underline; font-size: large;">Exception Reports</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left">1)</td>
                <td align="left">
                    <asp:LinkButton ID="lnkaccepnote0" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/PendingAcptKharif2016_Dist.aspx">Pending Acceptance Note Details</asp:LinkButton>
                </td>
                <td>&nbsp;</td>
            </tr>
         </table>

                </asp:Panel>
    </div>
</asp:Content>


