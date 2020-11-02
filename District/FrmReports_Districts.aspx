<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="FrmReports_Districts.aspx.cs" Inherits="District_FrmReports_Districts" Title="District Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both"
            CssClass="tblnew">
            <table style="border: 1pt solid #FF9933; color: #0000FF; font-family: Calibri;"
                border="1" cellpadding="0" cellspacing="0" width="800px">
                <tr>
                    <td align="center"
                        style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="3">Reports</td>
                </tr>



                <tr>
                    <td align="left">1</td>
                    <td align="left">
                        <asp:LinkButton ID="lnkaccepnote" runat="server" ForeColor="Navy"
                            PostBackUrl="~/District/Frm_Procurement_Reports.aspx">उपार्जन रिपोर्टस (Updated on 13/10/2015)</asp:LinkButton>

                    </td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td align="left">2</td>
                    <td align="left">

                        <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Navy"
                            PostBackUrl="~/District/Frm_DPY_Reports.aspx">द्वार प्रदाय योजना  (Updated on 22/9/2015)</asp:LinkButton></td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td align="left">3</td>
                    <td align="left">
                        <asp:LinkButton ID="lnkdelaccept" runat="server" ForeColor="Navy" PostBackUrl="~/District/Frm_RackMovement_Reports.aspx">रेक मूवमेट रिपोर्टस (Updated on 22/9/2015)</asp:LinkButton></td>
                    <td>&nbsp;</td>
                </tr>


                <tr>
                    <td align="left">4</td>
                    <td align="left">
                        <asp:LinkButton ID="lnlrackproc" runat="server" ForeColor="Navy" PostBackUrl="~/District/RoadMovementRepots.aspx">रोड मूवमेंट रिपोर्टस (Updated on 22/9/2015)</asp:LinkButton></td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td align="left">5</td>
                    <td align="left">
                        <asp:LinkButton ID="lnktrucrec_drill" runat="server" ForeColor="Navy" PostBackUrl="~/District/FrmPaddyMillingReports.aspx">धान मिलिंग  रिपोर्टस (Updated on 22/9/2015)</asp:LinkButton></td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td align="left">6</td>
                    <td align="left">
                        <asp:LinkButton ID="lnktrucrec_drill1" runat="server" ForeColor="Navy"
                            PostBackUrl="~/District/FrmStockReports.aspx">स्टॉक रिपोर्टस (Updated on 22/9/2015)</asp:LinkButton></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left">7</td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Navy"
                            PostBackUrl="~/District/FrmFCIClaimsReports.aspx">FCI परिदान रिपोर्टस (Updated on 22/9/2015)</asp:LinkButton></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left">8</td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Navy"
                            PostBackUrl="~/District/FrmMoniteringRpts.aspx">मोनिटरिंग रिपोर्टस (Updated on 22/9/2015)</asp:LinkButton></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left">9</td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Navy"
                            PostBackUrl="~/District/FrmDOReports.aspx">डी.ओ रिपोर्टस (Updated on 22/9/2015)</asp:LinkButton></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left">10</td>
                    <td align="left">
                        <asp:HyperLink ID="DH110" runat="server"
                            NavigateUrl="~/District/frm_reUparjan_Accounting.aspx">उपार्जन लेखा (Updated on 22/9/2015)</asp:HyperLink>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left">11</td>
                    <td align="left">
                        <asp:HyperLink ID="HyperLink1" runat="server"
                            NavigateUrl="~/District/PDSMORoadStatusHome_Rpt.aspx">PDS Movement [By Road] (Updated on 04/12/2015)</asp:HyperLink>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left">12</td>
                    <td align="left">
                        <asp:HyperLink ID="HyperLink2" runat="server"
                            NavigateUrl="~/District/PDSMORailRackStatusHome_Rpt.aspx">PDS Movement [By Rail Rack] (Updated on 04/12/2015)</asp:HyperLink>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left">
                        13</td>
                    <td align="left">
                        <asp:HyperLink ID="hyptran" runat="server" 
                            NavigateUrl="~/ReportForms_District/Transporter_Uparjan2016.aspx">Transportation Details Wheat 2016</asp:HyperLink>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left">
                        14</td>
                    <td align="left">
                        <asp:HyperLink ID="LinkButton5" runat="server" 
                            NavigateUrl="~/District/POS_Reports.aspx">पी.ओ.एस रिपोर्ट्स (Updated on 27/05/2016)</asp:HyperLink>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left">
                        15</td>
                    <td align="left">
                        <asp:HyperLink ID="hypSugar" runat="server" 
                            NavigateUrl="~/District/Frm_Sugar_Salt_Reportt.aspx">Sugar Salt Report</asp:HyperLink>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>

        </asp:Panel>
    </div>
</asp:Content>


