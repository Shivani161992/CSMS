<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaddyMilling_Status_MRFD.aspx.cs" Inherits="Reports_State_PaddyMilling_Status_MRFD" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>

    <style type="text/css">
        
        .ButtonClass
        {
            cursor: pointer;
        }

        </style>

    <style type="text/css">
        
        .ButtonClass
        {
            cursor: pointer;
        }

        .column
        {
            height: 30px;
            color: #1B2631;
            font-family: 'Lucida Fax';
            text-align: center;
            font-size: 25px;
            border-radius: 32px;
            background-color: #6B8E23;
        }

        .columncolumn
        {
            height: 30px;
            color: #6B8E23;
            font-family: 'Lucida Fax';
            text-align: center;
            font-size: 25px;
            border-radius: 32px;
            background-color: #1B2631;
        }

        .tddl
        {
            width: 400px;
            border-radius: 32px;
            height: 25px;
            text-align: center;
            font-family: 'Lucida Fax';
        }

        .bttn
        {
            width: 150px;
            height: 25px;
            border-radius: 25px;
            color: #fff!important;
            background-color: #000!important;
            border-radius: 32px;
        }

        .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover
        {
            color: #1B2631!important;
            background-color: #6B8E23!important;
            font-family: 'Lucida Fax';
            font-size: 25px;
        }

        .bttn:active
        {
            background-color: yellow;
        }
    </style>

</head>
<body>
    <%--  <form id="form1" runat="server">
        <div>

            <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3; padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color: lavender;"
                align="center">
                <tr style="text-align: center">
                    <td colspan="3" class="auto-style1" style="color: #CC6600; background-color:#FFCCFF">Paddy Milling Status(District Wise)</td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="Label1" runat="server" Style="font-weight: 700; font-size: large;" Text="CropYear"></asp:Label>
                        <asp:DropDownList ID="ddlCropYear" runat="server" Width="173px" style="margin-left:20px">

                        </asp:DropDownList>
                       
       <asp:RadioButton ID="rdbCivil" runat="server" GroupName="Dist" style="font-weight: 700;margin-left:20px; color: #CC00FF;" Text="Civil Supplies District" />
                        <asp:RadioButton ID="rdbMFD" runat="server" GroupName="Dist" style="font-weight: 700;margin-left:20px; color: #CC00FF;" Text="Markfed Dist"  />
                  
      
        </td>
                    <td style="text-align: center" class="auto-style3">
                        <asp:Button ID="btnReport" runat="server"  Text="View Report" OnClick="btnReport_Click" style="font-weight: 700;  font-size: small;" CssClass="ButtonClass" />
                    </td>
               
                    <td style="text-align: center">
                        <asp:LinkButton ID="lnkbtnBack" runat="server" Font-Bold="True" ForeColor="#CC3300" Style="text-align: center" OnClick="lnkbtnBack_Click">Back</asp:LinkButton></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnlreport" runat="server" Visible="false">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="520px" ProcessingMode="Remote"
                                Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False"
                                ZoomMode="PageWidth" BackColor="CornflowerBlue" BorderColor="#E0E0E0"
                                LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                                ShowBackButton="true" EnableTheming="true">
                                <ServerReport ReportServerUrl="" />
                            </rsweb:ReportViewer>
                        </asp:Panel>
                    </td>
                </tr>
            </table>

        </div>

    </form>--%>



    <form id="form1" runat="server">
        <div>

            <table style="width: 100%; background-color: white;" align="center">

                <tr style="text-align: center">
                    <td colspan="4" class="column">Paddy Milling Status(District Wise)</td>
                </tr>
                <tr style="background-color: #1B2631; color: #6B8E23; height: 30px;">
                    <td class="columncolumn">
                        <table style="width: 100%;">

                            <tr>
                                <td colspan="3" style="text-align: Center; height: 10px">
                                   


                                </td>

                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label1" runat="server" Style="font-weight: 700; font-size: large;" Text="CropYear"></asp:Label>

                                    <asp:DropDownList ID="ddlCropYear" runat="server" class="tddl" Width="173px" Style="margin-left: 20px">
                                    </asp:DropDownList>
                                     <asp:RadioButton ID="rdbCivil" runat="server" GroupName="Dist" style="font-size:small; text-align:center;" Text="Civil Supplies District" />
                                    &nbsp;<asp:RadioButton ID="rdbMFD" runat="server" GroupName="Dist" style="font-size:small" Text="Markfed Dist" /> &nbsp;
                                    <asp:RadioButton ID="rdbMer" runat="server" GroupName="Dist" style="font-size:small" Text="Merged " />
                                </td>

                                <td style="text-align: right;">
                                    <asp:Button ID="Button1" runat="server" Text="View Report" Style="font-weight: 700; font-size: small;" CssClass="bttn w3-grey" OnClick="btnReport_Click" />
                                </td>

                                <td style="text-align: center;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" ForeColor="#6B8E23" Style="text-align: center; font-size: medium;" OnClick="lnkbtnBack_Click">Back</asp:LinkButton>

                                </td>

                            </tr>

                        </table>
                    </td>

                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="pnlreport" runat="server" Visible="false">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="520px" ProcessingMode="Remote"
                                Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False"
                                ZoomMode="PageWidth" BackColor="CornflowerBlue" BorderColor="#E0E0E0"
                                LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                                ShowBackButton="true" EnableTheming="true">
                                <ServerReport ReportServerUrl="" />
                            </rsweb:ReportViewer>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>

    </form>


</body>
</html>
