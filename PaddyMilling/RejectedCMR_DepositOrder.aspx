<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="RejectedCMR_DepositOrder.aspx.cs" Inherits="PaddyMilling_RejectedCMR_DepositOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आपने सही जानकारी भरी हैं? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
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

        .auto-style2 {
            font-size: small;
        }
    </style>

    <table align="center" style="width: 800px; border-style: solid; text-align: left; font-size: medium" border="1" cellspacing="0" cellpadding="2">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Rejected CMR Deposit Order</strong>
                <input id="hdfAgrmtDist" type="hidden" runat="server" />
                <input id="hdfDistance" type="hidden" runat="server" />
                <input id="hdfAllQty" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="12">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="12"></td>
        </tr>
        <tr>
            <td>Crop Year</td>
            <td>
                <asp:DropDownList ID="ddlCropYear" runat="server" Width="149px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>District</td>
            <td>

                <asp:TextBox ID="txtDist" runat="server" Width="145px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDist" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Miller&#39;s Name</td>
            <td>
                <asp:DropDownList ID="ddlMillName" runat="server" Height="27px" Width="230px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMillName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>Rejection No.</td>
            <td>
                <asp:DropDownList ID="ddlRejectionNo" runat="server" Height="27px" Width="149px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>Inspection Date</td>
            <td>
                <asp:TextBox ID="txtInspDate" runat="server" Width="145px" AutoComplete="off" MaxLength="14"  Enabled="False" ReadOnly="True"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtInspDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Stack No.</td>
            <td>
                <asp:TextBox ID="txtStackNo" runat="server" Width="145px" AutoComplete="off" MaxLength="14" Enabled="false" ReadOnly="True"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtStackNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Rejected Qty <strong>(Qtls)</strong></td>
            <td>
                <asp:TextBox ID="txtRejQty" runat="server" Width="145px" AutoComplete="off" MaxLength="14"  Enabled="false" ReadOnly="True"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtRejQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Issued Qty <strong>(Qtls)</strong></td>
            <td>
                <asp:TextBox ID="txtIssuedQty" runat="server" Width="145px" AutoComplete="off" MaxLength="14" Enabled="false" ReadOnly="True"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtIssuedQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="background-color: #999999; color: #999999;"></td>
        </tr>
        <tr style="font-size: small">
            <td colspan="4" style="background-color: #CCFF99;">मिलर को किस प्रदाय केंद्र के, किस ब्रांच तथा किस गोदाम में CMR जमा करना है, कृपया चुनाव करे:-</td>
        </tr>


        <tr>
            <td colspan="4" style="background-color: #999999; color: #999999;"></td>
        </tr>


        <tr>
            <td>Issue Centre</td>
            <td>

                <asp:DropDownList ID="ddlIC" runat="server" Width="149px" AutoPostBack="True" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
            <td>Branch</td>
            <td>

                <asp:DropDownList ID="ddlBranch" runat="server" Width="149px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>


        <tr>
            <td>Godown</td>
            <td>

                <asp:DropDownList ID="ddlGodam" runat="server" Width="149px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodam_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
            <td>CMR Deposit Date</td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" Width="145px" Style="margin-left: 0px" ReadOnly="True"></asp:TextBox>
                &nbsp;
                    
                    <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate', 'restrict=true,instance=single,limit=<%= DateLimit %>'  )" />--%>
                <img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' , 'expiry=true,elapse=-250,restrict=true,limit=7,close=true')" />

                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>


        <tr>
            <td>Type of CMR</td>
            <td>

                <asp:DropDownList ID="ddlCommodity" runat="server" Width="149px">
                </asp:DropDownList></td>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td colspan="4" style="background-color: #CCFF99; color: #0000FF;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="False" OnClick="btnRecptSubmit_Click1" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnPrint_Click" CausesValidation="False" Enabled="False" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

