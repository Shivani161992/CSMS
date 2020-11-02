<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="ReceiptCMR_Delete.aspx.cs" Inherits="PaddyMilling_ReceiptCMR_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आप इसे Delete करना चाहते है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>

    <%--For Calendar Controls--%>
    <link href="calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="calendar/calendar.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>

    <table align="center" style="width: 610px; border-style: solid; border-width: 1px; text-align: left; font-size: small" border="1" cellspacing="0" cellpadding="5">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Delete Receipt Entry CMR</strong>
                <input id="hdfAgrmtDist" type="hidden" runat="server" />
                <input id="hdfCMRDoNo" type="hidden" runat="server" />
                <input id="hdfStatus" type="hidden" runat="server" />
                 <input id="hdfLotNo" type="hidden" runat="server" />
                <input id="hdfDO_NO" type="hidden" runat="server" />
                <input id="hdfWHRValue" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="8">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="8"></td>
        </tr>
        <tr>
            <td>District</td>
            <td>
                <asp:DropDownList ID="ddlFrmDist" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlFrmDist_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Crop Year</td>
            <td>

                <asp:DropDownList ID="ddlCropyear" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropyear_SelectedIndexChanged">
                </asp:DropDownList>


                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCropyear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Mill Name</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlMillName" runat="server" Width="460px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>Agreement No.</td>
            <td>
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Width="148px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
            <td>CMR Acceptance /<br />
                Rejection No</td>
            <td>
                <asp:DropDownList ID="ddlAcptNo" runat="server" Width="165px" AutoPostBack="True" OnSelectedIndexChanged="ddlAcptNo_SelectedIndexChanged" >
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>Lot Number</td>
            <td>

                <asp:TextBox ID="txtLotNo" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtLotNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                </td>
            <td>Status</td>
            <td>

                <asp:TextBox ID="txtStatus" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtStatus" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                </td>
        </tr>



        <tr>
            <td>Recd. Qty(Qtls)</td>
            <td>

                <asp:TextBox ID="txtQty" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                </td>
            <td>Recd. Bags</td>
            <td>

                <asp:TextBox ID="txtBags" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                </td>
        </tr>



        <tr>
            <td>Truck Number</td>
            <td>

                <asp:TextBox ID="txtTruckNo" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtTruckNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                </td>
            <td>Recd. Date</td>
            <td>

                <asp:TextBox ID="txtRecdDate" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtRecdDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                </td>
        </tr>



        <tr>
            <td colspan="4" style="background-color: #CCFF99; color: #0000FF;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Delete" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="False" OnClick="btnRecptSubmit_Click1" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

