﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualityInspection_HOLevelInspWise.aspx.cs" Inherits="Reports_State_QualityInspection_HOLevelInspWise" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>

    <style type="text/css">
        
        .ButtonClass {
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
            border-radius: 8px;
            height: 25px;
            text-align: center;
             font-family: 'Lucida Fax';
        }
        .txt
        {
            width: 200px;
            border-radius: 8px;
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
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("#<%= txtFrmdate.ClientID %> ").datepicker(
              {
                  dateFormat: 'dd-mm-yy'
              }
            );
        });
    </script>
     <script type="text/javascript">
         $(function () {
             $("#<%= txtTodate.ClientID %> ").datepicker(
              {
                  dateFormat: 'dd-mm-yy'
              }
            );
        });
    </script>

<body>
    <form id="form1" runat="server">
        <div>

            <table style="width: 100%; background-color: white;" align="center">

                <tr style="text-align: center">
                    <td colspan="4" class="column">Quality Inspection HO Level Inspector Wise (QC-04)</td>
                </tr>
                <tr style="background-color: #1B2631; color: #6B8E23; height: 30px;">
                    <td class="columncolumn">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align:left;">
                                    <asp:Label ID="Label1" runat="server" Style="font-weight: 400; font-size: large;" Text="CropYear"></asp:Label>

                                    <asp:DropDownList ID="ddlCropYear" runat="server" class="tddl" Width="173px" Style="margin-left: 20px">
                                    </asp:DropDownList>
                                </td>
                                 <td style="text-align:right;">
                                </td>
                                 <td style="text-align:center;">

                                </td>
                                </tr>

                               <tr>
                                <td style="text-align:left;">
                                    <asp:Label ID="Label2" runat="server" Style="font-weight: 400; font-size: large;" Text="From Date"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtFrmdate" CssClass="txt" runat="server" ReadOnly="true"></asp:TextBox>
                                     &nbsp;
                                     <asp:Label ID="Label3" runat="server" Style="font-weight: 400; font-size: large;" Text="To Date"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtTodate" CssClass="txt" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                                 <td style="text-align:right;">
                                    <asp:Button ID="Button1" runat="server" Text="View Report" Style="font-weight: 700; font-size: small;" CssClass="bttn w3-grey"  OnClick="btnReport_Click"/>
                                </td>
                                 <td style="text-align:center;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" ForeColor="#6B8E23" Style="text-align: center; font-size:medium;" OnClick="lnkbtnBack_Click">Back</asp:LinkButton>

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