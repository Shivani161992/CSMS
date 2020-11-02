<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rep_DistwiseAgProc.aspx.cs" Inherits="mpproc_ReportsPage_StateLevel_Rep_DistwiseAgProc" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Procurement Report</title>
 <script type="text/javascript" src="../../js/calendar_eu.js"></script> 

 <link href="../../CSS/calendar.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="width: 100%">
                    <table style="background-color: lightgrey; width: 100%">
                        <tr>
                            <td style="height: 25px; background-color: #cccc66">
                            </td>
                            <td colspan="2" style="height: 25px; background-color: #cccc66">
                                <asp:Label ID="lbl_tit_WhtReport" runat="server" Text="District Wise Procurement by Agency "
                                    Width="262px" Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                            <td style="height: 25px; background-color: #cccc66">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
                                <asp:Label ID="lbl_Agency" runat="server" Text="Agency:-" Width="98px"></asp:Label>
                                <asp:Label ID="lbl_AgRes" runat="server" Width="67px" ForeColor="Navy"></asp:Label></td>
                           
                            <td colspan="2" >
                                <asp:Label ID="lbl_MS" runat="server" Text="MarketingSeason:-" Width="152px"></asp:Label>
                                <asp:Label ID="lbl_MsRes" runat="server" ForeColor="Navy"></asp:Label>
                                
                                </td>
                            
                        </tr>
                        <tr>
                              <td  colspan="2">
                                <asp:Label ID="lbl_CropYear" runat="server" Text="CropYear:-" Width="94px"></asp:Label>
                                <asp:Label ID="lbl_CYRes" runat="server" ForeColor="Navy"></asp:Label>
                                
                                </td>
                           
                            <td  colspan="2">
                            
                              <asp:Label ID="lbl_Commodity" runat="server" Text="Commodity:-" Width="152px"></asp:Label>&nbsp;<asp:DropDownList
                                  ID="DDL_Commodity" runat="server">
                              </asp:DropDownList>
                              
                              
                           </td>   
                           
                        </tr>
                        <tr>
                            <td style="width: 62px">
                                </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/mpproc/State/StateWelcome.aspx"  >Back</asp:LinkButton></td>
                            <td style="width: 77px">
                                <asp:Button ID="btn_ViewReport" runat="server" Text="View Report" OnClick="btn_ViewReport_Click"  /></td>
                            <td style="width: 96px">
                            </td>
                        </tr>
                    </table>
                   
            </tr>
            <tr>
                <td >
                   
                    <table>
                    <tr>
                    <td>
                    <rsweb:ReportViewer id="ReportViewer1" runat="server" Width="147%" ShowCredentialPrompts="False" Font-Size="8pt" Font-Names="Verdana" ProcessingMode="Remote" Height="440px">
          
        </rsweb:ReportViewer>
                    </td>
                    
                    </tr>
                    
                    </table>
                    
                    </td>
            </tr>
        </table>
    </form>
</body>
</html>
