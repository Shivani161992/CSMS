<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="CMRInspection_Rejection.aspx.cs" Inherits="PaddyMilling_CMRInspection_Rejection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>

    <%--For Calendar Controls--%>
    <link href="calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="calendar/calendar.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आप CMR को Reject करना चाहते हैं? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style2 {
            text-decoration: underline;
        }

        .auto-style3 {
            color: #FF3300;
        }
    </style>

    <table align="center" style="width: 740px; border-style: solid; border-width: 1px; text-align: left; font-size: medium" border="1" cellspacing="0" cellpadding="5">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><strong><span class="auto-style2">Inspection of CMR Rejection Entry</span></strong>
                <input id="hdfAllotedDOQty" type="hidden" runat="server" />
                <input id="hdfLotNO" type="hidden" runat="server" />
                <input id="hdfAdjustCMRDO" type="hidden" runat="server" />
            </td>

        </tr>

        <tr>
            <td rowspan="9">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="lblmsg" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="9"></td>
        </tr>

        <tr>
            <td>Crop Year</td>
            <td>
                <asp:DropDownList ID="ddlCropYear" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>District</td>
            <td>
                <asp:TextBox ID="txtDist" runat="server" Width="137px" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDist" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Miller&#39;s State</td>
            <td>

                <asp:RadioButton ID="rbMPState" runat="server" Font-Bold="True" GroupName="State" Text="M.P State" AutoPostBack="True" ForeColor="Blue" OnCheckedChanged="rbMPState_CheckedChanged" />
            </td>
            <td>
                <asp:RadioButton ID="rbOtherState" runat="server" Font-Bold="True" GroupName="State" Text="Other States" AutoPostBack="True" ForeColor="Blue" OnCheckedChanged="rbOtherState_CheckedChanged" />
            </td>
            <td>

                <asp:DropDownList ID="ddlOtherStates" runat="server" Width="212px" AutoPostBack="True" OnSelectedIndexChanged="ddlOtherStates_SelectedIndexChanged" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Miller&#39;s District</td>
            <td>
                <asp:DropDownList ID="ddlMacersAddDist" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlMacersAddDist_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>Mill Name</td>
            <td>

                <asp:DropDownList ID="ddlMacersName" runat="server" Width="212px" AutoPostBack="true" OnSelectedIndexChanged="ddlMacersName_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Issue Centre</td>
            <td>

                <asp:DropDownList ID="ddlIC" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>Branch</td>
            <td>

                <asp:DropDownList ID="ddlBranch" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Godown</td>
            <td>

                <asp:DropDownList ID="ddlGodam" runat="server" Width="141px" OnSelectedIndexChanged="ddlGodam_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td>Inspection Date</td>
            <td>

                <asp:TextBox ID="txtFromDate" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True"></asp:TextBox>
                &nbsp;
                <img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' , 'expiry=true,elapse=-250,restrict=true,close=true')" />

                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Stack No.</td>
            <td>
                <asp:TextBox ID="txtStackNo" runat="server" Width="137px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Enabled="false"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtStackNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Rej. CMR Qty <span class="auto-style3"><strong>(Qtls)</strong></span></td>
            <td>
                <asp:TextBox ID="txtQty" runat="server" Width="137px" Style="text-align: right" MaxLength="10" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;" Enabled="false"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Inspected By</td>
            <td colspan="3">
                <asp:TextBox ID="txtInspectedName" runat="server" Width="470px" AutoComplete="off" MaxLength="40" onfocus="this.select();" onmouseup="return false;" Enabled="false" Class="alphaOnly"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtInspectedName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                </td>
        </tr>


        <tr>
            <td colspan="4" style="background-color: #CCFF99; color: #0000FF;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Reject" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="false" OnClick="btnRecptSubmit_Click1" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnPrint_Click" CausesValidation="False" Enabled="False" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>

        <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
        <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>
        <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Alphabets.js"></script>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

