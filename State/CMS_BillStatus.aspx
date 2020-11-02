<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMS_BillStatus.aspx.cs" Inherits="State_CMS_BillStatus" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
    <style>
        .Rptheader
        {
            width: 100%;
            border-radius: 32px;
            color: #E4EDDB;
            background: #307672;
        }

        .RptColumn
        {
            width: 100%;
            letter-spacing: 4px;
            text-align: center;
            color: #E4EDDB;
            font-size: 20px;
            font-family: 'Bellefair';
        }

        .Rptfilters
        {
            width: 100%;
            border-radius: 32px;
            color: #E4EDDB;
            background: #1A3C40;
        }
        .insptxt
        {
            width: 150px;
            height: 15px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
            outline:none;
        }
         .inspddl
        {
            width: 160px;
            height: 19px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            padding-left: 10px;
             outline:none;
        }
            .sign
        {
            color: #ffffff;
            font-size: 12px;
            text-decoration: none;
            letter-spacing: 2px;
        }
                .sign:hover
                {
                    
                  text-decoration:none;
                }
    </style>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("#<%= txtFdate.ClientID %> ").datepicker(
              {
                  dateFormat: 'dd-mm-yy'
              }
            );
        });
    </script>

     <script type="text/javascript">
         $(function () {
             $("#<%= txtTdate.ClientID %> ").datepicker(
              {
                  dateFormat: 'dd-mm-yy'
              }
            );
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table class="Rptheader">
               
                <tr>
                     <td style="width: 10%;  text-align: left; padding-left: 20px;">
                          <a href="ChanaMasurReport.aspx" class="sign">&#9754 Back
                    </a>
                    </td >
                    <td class="RptColumn" style="width: 80%;  text-align: center;">CMS Bill Status
                     
                       
                    </td>
                     <td style="width: 10%; font-size: 12px; text-align: left; padding-left: 20px;">
                          <a href="../IssueCenter/PaddyMillingHome.aspx" class="sign">
                    </a>
                    </td>
                </tr>
                 
            </table>
            <table>
                <tr>
                    <td></td>
                </tr>
            </table>
            <table class="Rptfilters">
                <tr>
                    <td class="RptColumn" style="width: 25%; font-size: 12px; text-align: left; padding-left: 20px;">Commodity :-
                         <asp:DropDownList ID="ddlcomm" runat="server" CssClass="inspddl"  AutoPostBack="true">
                    </asp:DropDownList>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Style="font-size: 12px; color:white"
                        ControlToValidate="ddlcomm" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="RptColumn" style="width: 25%; font-size: 12px; text-align: left; padding-left: 20px;">From Date :-
                       <asp:TextBox ID="txtFdate" CssClass="insptxt" ReadOnly="true"  runat="server" AutoComplete="off" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="font-size: 12px;  color:white"
                        ControlToValidate="txtFdate" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="RptColumn" style="width: 25%; font-size: 12px; text-align: left; padding-left: 20px;">To Date:-
                         <asp:TextBox ID="txtTdate" CssClass="insptxt" ReadOnly="true"  runat="server" AutoComplete="off" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="font-size: 12px;  color:white"
                        ControlToValidate="txtTdate" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="RptColumn" style="width: 25%; font-size: 12px; text-align: right; padding-right: 20px;">
                        <asp:LinkButton ID="lbviewreport" Text="View Report" runat="server" CssClass="sign" OnClick="LinkButton1_Click">View Report</asp:LinkButton>
                       
                    </td>
                </tr>

              
            </table>

            <table style="width:100%;">
                <tr>
                     <td colspan="4" style="width:100%;">
                         <asp:Panel ID="pnlreport" runat="server" Visible="false" style="width:100%">
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
