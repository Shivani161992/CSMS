<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Delete_PDSMO_ReceiptEntry.aspx.cs" Inherits="District_Delete_PDSMO_ReceiptEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>

    <div id="dvScroll" style="overflow-y: scroll; height: 500px;">
        <table align="center" style="width: 760px; border-style: solid; border-width: 1px; text-align: left;" border="1" cellspacing="0" cellpadding="2">
            <tr>
                <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Delete Receipt Entry For PDS Movement Order</strong>
                    <input id="hdfDispMode" type="hidden" runat="server" />
                      <input id="hdfSMO" type="hidden" runat="server" />
                     <input id="hdfRMO" type="hidden" runat="server" />
                     <input id="hdfWHRValue" type="hidden" runat="server" />
                </td>
            </tr>

            <tr style="margin-top: 50px;">
                <td rowspan="6"></td>
                <td style="text-align: left; background-color: #FFCC99; font-weight: 700;">
                    <asp:RadioButton ID="rdbRoad" runat="server" Text="By Road" GroupName="RR" AutoPostBack="True" OnCheckedChanged="rdbRoad_CheckedChanged" />
                    <br />
                    <asp:RadioButton ID="rdbRack" runat="server" Text="By Rack" GroupName="RR" AutoPostBack="True" OnCheckedChanged="rdbRack_CheckedChanged" />
                </td>
                <td style="text-align: center; background-color: #FFCC99; font-weight: 700;">IC&nbsp;
                    <asp:DropDownList ID="ddlIssueCentre" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlIssueCentre_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
                <td style="text-align: center; background-color: #FFCC99; font-weight: 700;">From Date<asp:TextBox ID="txtFromDate" runat="server" Width="100px" ReadOnly="True" Style="margin-left: 10px"></asp:TextBox>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' ,'expiry=false')" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                    <br />
                    To Date<asp:TextBox ID="txtToDate" runat="server" Width="100px" ReadOnly="True" Style="margin-left: 26px"></asp:TextBox>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' ,'expiry=false' )" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
                <td style="text-align: center; background-color: #FFCC99; font-weight: 700;">
                    <asp:Button ID="btnSearch" runat="server" BackColor="#FF6937" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="25px" Text="Search" Width="115px" CssClass="ButtonClass" OnClick="btnSearch_Click" CausesValidation="False" />
                </td>
                <td rowspan="6"></td>
            </tr>

            <tr>
                <td>Receipt Number</td>
                <td>
                    <asp:DropDownList ID="ddlDC" runat="server" Width="187px" AutoPostBack="True" OnSelectedIndexChanged="ddlDC_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
                <td>MO Number</td>
                <td>
                    <asp:TextBox ID="txtMONo" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>Commodity</td>
                <td>
                    <asp:TextBox ID="txtCommodity" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCommodity" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
                <td>Transport Order Number</td>
                <td>
                    <asp:TextBox ID="txtTranspNO" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTranspNO" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
            </tr>


            <tr>
                <td>From Dist</td>
                <td>
                    <asp:TextBox ID="txtFrmDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
                <td>From Issue Centre</td>
                <td>
                    <asp:TextBox ID="txtToDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Recd Qty <strong>(Qtls)</strong></td>
                <td>
                    <asp:TextBox ID="txtIssuedQty" runat="server" Width="146px" MaxLength="10" AutoComplete="off" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtIssuedQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                </td>
                <td>Recd Bags</td>
                <td>
                    <asp:TextBox ID="txtBags" runat="server" Width="146px" MaxLength="10" AutoComplete="off" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td>Truck Number</td>
                <td>
                    <asp:TextBox ID="txtTCNo" runat="server" Width="146px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Enabled="False"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTCNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                </td>
                <td>Receive Godown</td>
                <td>
                    <asp:TextBox ID="txtRecdGodown" runat="server" Width="146px" MaxLength="10" AutoComplete="off" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: center; font-size: large;">
                    <asp:Button ID="btnRecptNew" runat="server" Style="background-color: #FF6937;" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                    <asp:Button ID="btnDelete" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Delete" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px; background-color: #FF6937;" OnClientClick="return confirm('क्या आप इस डिलेवरी चालान को डिलीट करना चाहते है?');" OnClick="btnDelete_Click" Enabled="False" />

                    <asp:Button ID="btnRecptClose" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px; background-color: #FF6937;" OnClick="btnRecptClose_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

