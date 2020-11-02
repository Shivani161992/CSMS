<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FarmerReports.aspx.cs" Inherits="mpproc_ReportsPage_FarmerReports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
                            <td style="width: 62px">
                                </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/mpproc/Procurement/agency_welcom.aspx" OnClick="LinkButton1_Click">Back</asp:LinkButton></td>
                            <td style="width: 77px">
                                </td>
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

