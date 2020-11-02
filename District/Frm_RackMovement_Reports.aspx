<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Frm_RackMovement_Reports.aspx.cs" Inherits="District_Frm_RackMovement_Reports" Title="RackMovement_Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
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
            <td align="left" colspan="2" 
                style="font-weight: 700; text-align: center; font-style: italic">
               Rack Movement Reports</td>
            <td align="left">
            <asp:LinkButton ID="bakbtn" runat="server" ForeColor="Navy" PostBackUrl="~/District/FrmReports_Districts.aspx">Back</asp:LinkButton>
                </td>
        </tr>
        <tr>
            <td align="left">
                1</td>
            <td align="left">
                <asp:LinkButton ID="btn_do" runat="server" PostBackUrl="~/ReportForms_District/Rack_statement_dispatch.aspx" ForeColor="Navy">Sending Rack Statement</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td align="left">
                2</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton9" runat="server" PostBackUrl="~/ReportForms_District/Rack_receipt_statement.aspx" ForeColor="Navy">Receiving Rack Statement</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        
       
        
        <tr>
            <td align="left">
                3</td>
            <td align="left">
               <asp:LinkButton ID="lnkhnl" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/District_DispatchbyRack.aspx">Dispatch by Rack</asp:LinkButton>
                    </td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
            <td align="left">
                4</td>
            <td align="left">
      <asp:LinkButton ID="lnkrack_commowise" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/Dist_Rackdetails_Commoditywise.aspx">By Rack Dispatch detail Commoditywise</asp:LinkButton>
                    </td>
            <td>
                &nbsp;</td>
        </tr>
</table>

                </asp:Panel>
    </div>
</asp:Content>




