<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Paddy_Adjust_DO.aspx.cs" Inherits="PaddyMilling_Paddy_Adjust_DO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आप Adjustment Delivery Order बनाना चाहते हो? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style2 {
            text-decoration: underline;
        }

    </style>

    <table align="center" style="width: 740px; border-style: solid; border-width: 1px; text-align: left; font-size: small" border="1" cellspacing="0" cellpadding="5">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><strong><span class="auto-style2">Adjustment Delivery Order of Paddy</span></strong>
                <input id="hdfAllotedDOQty" type="hidden" runat="server" />
                <input id="hdfLotNO" type="hidden" runat="server" />
                <input id="hdfAdjustCMRDO" type="hidden" runat="server" />
            </td>

        </tr>

        <tr>
            <td rowspan="7">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="lblmsg" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="7"></td>
        </tr>

        <tr>
            <td>Agrmt District</td>
            <td>
                <asp:TextBox ID="txtDistManager" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDistManager" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>Crop Year</td>
            <td>

                <%--<asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
         --%>  
                   <asp:DropDownList ID="ddlCropyear" runat="server" Height="27px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropyear_SelectedIndexChanged">
                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCropyear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>


            </td>
        </tr>

        <tr>
            <td>Mill Name</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlMillName" runat="server" Width="495px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Agreement No.</td>
            <td>
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>DO Number</td>
            <td>
                <asp:DropDownList ID="ddlDONo" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlDONo_SelectedIndexChanged" Style="height: 22px">
                </asp:DropDownList>
            </td>
        </tr>

        
        <tr>
            <td>Lot No.</td>
            <td>
                <asp:TextBox ID="txtLotNO" runat="server" Width="137px" ReadOnly="True" Style="text-align: right" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLotNO" ErrorMessage="**"></asp:RequiredFieldValidator>
            </td>
            <td>DO Qty</td>
            <td>
                <asp:TextBox ID="txtDOQty" runat="server" Width="137px" ReadOnly="True" Style="text-align: right" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDOQty" ErrorMessage="**"></asp:RequiredFieldValidator>
            </td>
        </tr>

        
        <tr>
            <td>Alloted DO Qty</td>
            <td>
                <asp:TextBox ID="txtAllotedDOQty" runat="server" Width="137px" ReadOnly="True" Style="text-align: right" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAllotedDOQty" ErrorMessage="**"></asp:RequiredFieldValidator>
            </td>
            <td>Rem. DO Qty</td>
            <td>
                <asp:TextBox ID="txtRemDOQty" runat="server" Width="137px" ReadOnly="True" Style="text-align: right" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRemDOQty" ErrorMessage="**"></asp:RequiredFieldValidator>
            </td>
        </tr>

        
        <tr>
            <td colspan="4" style="background-color: #CCFF99; color: #0000FF;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="false" OnClick="btnRecptSubmit_Click1" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

