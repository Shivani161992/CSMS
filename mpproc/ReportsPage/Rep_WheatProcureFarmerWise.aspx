<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rep_WheatProcureFarmerWise.aspx.cs" Inherits="ReportsPage_Rep_WheatProcureFarmerWise" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Wheat Report Farmer Wise</title>
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
                            <td style="width: 100px; height: 25px; background-color: #99cc66">
                            </td>
                            <td style="width: 100px; height: 25px; background-color: #99cc66">
                            </td>
                            <td style="width: 77px; height: 25px; background-color: #99cc66">
                                <asp:Label ID="lbl_tit_WhtReport" runat="server" Text="Wheate Procurement Report"
                                    Width="197px" Font-Bold="True" ForeColor="Gray"></asp:Label></td>
                            <td style="width: 100px; height: 25px; background-color: #99cc66">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
                                <asp:Label ID="lbl_dist" runat="server" Text="Dist Name:-" Width="98px"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Width="67px"></asp:Label></td>
                           
                            <td colspan="2" >
                                <asp:Label ID="lbl_PC" runat="server" Text="Purchase Center Name:-" Width="152px"></asp:Label>
                                <asp:Label ID="Label2" runat="server"></asp:Label></td>
                            
                        </tr>
                        <tr>
                              <td  colspan="2">
                                <asp:Label ID="Label3" runat="server" Text="From Date :-" Width="91px"></asp:Label>
                                <asp:TextBox ID="txtfromDate" runat="server" Width="109px"></asp:TextBox>
                                
                                   <script type  ="text/javascript">
                         new tcal (
                         {
                         'formname': 'form1',
                         'controlname':'txtfromDate' }
                         );
                         
	                     </script>
                                
                                
                                </td>
                           
                            <td  colspan="2">
                                <asp:Label ID="Label4" runat="server" Text="To Date :-" Width="91px"></asp:Label>
                                <asp:TextBox ID="txtTodate" runat="server" Width="94px"></asp:TextBox>
                                 <script type  ="text/javascript"> new tcal ({'formname': 'form1','controlname': 'txtTodate' });
                           </script>
                           </td>   
                           
                        </tr>
                        <tr>
                            <td style="width: 62px">
                                </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/mpproc/Procurement/agency_welcom.aspx">Back</asp:LinkButton></td>
                            <td style="width: 77px">
                                <asp:Button ID="btn_ViewReport" runat="server" Text="View Report" OnClick="btn_ViewReport_Click" /></td>
                            <td style="width: 100px">
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
