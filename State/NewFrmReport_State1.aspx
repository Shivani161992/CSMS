﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="NewFrmReport_State1.aspx.cs" Inherits="State_NewFrmReport_State" Title="State Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div >
    <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
        <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;" 
            border="1" cellpadding="0" cellspacing="0" width="620px">
        <tr>
            <td align="center" 
                style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;" 
                colspan="3">
                Reports</td>
        </tr>
        
       
        
       <tr>
            <td align="left">
                1</td>
            <td align="left">
           <asp:LinkButton ID="lnkaccepnote" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/State/FrmUparjanReports.aspx">उपार्जन रिपोर्टस </asp:LinkButton> 
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
       <tr>
            <td align="left">
                2</td>
            <td align="left">
        
       <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Navy"
                    PostBackUrl="~/State/FrmDPYrpt_State.aspx">डी.पी.वाई रिपोर्टस</asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
        
       <tr>
            <td align="left">
                3</td>
            <td align="left">
                <asp:LinkButton ID="lnkdelaccept" runat="server" ForeColor="Navy" PostBackUrl="~/State/FrmPDSReports.aspx">पी.डी.एस रिपोर्टस</asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
        
     
       <tr>
            <td align="left">
                4</td>
            <td align="left">
                <asp:LinkButton ID="lnlrackproc" runat="server" ForeColor="Navy" PostBackUrl="~/State/FrmSugarNSaltReports.aspx">शुगर एंड साल्ट स्प्लाई रिपोर्टस </asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
        
       <tr>
            <td align="left">
                5</td>
            <td align="left">
                <asp:LinkButton ID="lnktrucrec_drill" runat="server" ForeColor="Navy" PostBackUrl="~/State/FrmMonitoringReports_State.aspx">डाटा एंटरी मोनिटरिंग रिपोर्टस</asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
        
       <tr>
            <td align="left">
                6</td>
            <td align="left">
                <asp:LinkButton ID="lnktrucrec_drill1" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/State/FrmPaddyMillingRpt.aspx">धान मिलिंग  रिपोर्टस</asp:LinkButton></td>
            <td>
                &nbsp;</td>
        </tr>
</table>

                </asp:Panel>
    </div>
</asp:Content>


