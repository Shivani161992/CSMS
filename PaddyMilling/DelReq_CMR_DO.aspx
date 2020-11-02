<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DelReq_CMR_DO.aspx.cs" Inherits="PaddyMilling_DelReq_CMR_DO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आप CMR Deposit Order को Delete करने के लिए Request भेजना चाहते हो? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style2 {
            text-decoration: underline;
        }

        .hiddencol {
            display: none;
        }
    </style>

    <table align="center" style="width: 680px; border-style: solid; border-width: 1px; text-align: left; font-size: small" border="1" cellspacing="0" cellpadding="5">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><strong><span class="auto-style2">Delete Request For CMR Deposit Order </span></strong>
                <input id="hdfAcpt" type="hidden" runat="server" />
                <input id="hdfReject" type="hidden" runat="server" />
                <input id="hdfDelReq" type="hidden" runat="server" />
                <input id="hdfLotNo" type="hidden" runat="server" />
                <input id="hdfMillID" type="hidden" runat="server" />
                <input id="hdfAgrmtID" type="hidden" runat="server" />
                <input id="hdfIC" type="hidden" runat="server" />
                <input id="hdfGodown" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="7">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; font-size: medium;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="7"></td>
        </tr>

        <tr>
            <td>District</td>
            <td>

                <asp:TextBox ID="txtDistManager" runat="server" Width="156px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
            <td>Crop Year</td>
            <td>

                <asp:TextBox ID="txtYear" runat="server" Width="156px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Mill Name</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlMillName" runat="server" Width="498px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>Agreement No.</td>
            <td>
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>CMR DO Number</td>
            <td>
                <asp:DropDownList ID="ddlDONo" runat="server" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlDONo_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>CMR DO Date</td>
            <td>

                <asp:TextBox ID="txtDODate" runat="server" Width="156px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDODate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>CMR DO Last Date</td>
            <td>
                <asp:TextBox ID="txtDOLastDate" runat="server" Width="156px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDOLastDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Issue Centre</td>
            <td>

                <asp:TextBox ID="txtIC" runat="server" Width="156px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
            <td>Godown</td>
            <td>

                <asp:TextBox ID="txtGodown" runat="server" Width="156px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #CCFF99"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnDelete" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Delete Request" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" OnClick="btnDelete_Click" Enabled="False" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnPrint_Click" CausesValidation="False" Enabled="False" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

