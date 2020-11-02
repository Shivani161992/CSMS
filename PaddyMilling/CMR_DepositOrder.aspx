<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="CMR_DepositOrder.aspx.cs" Inherits="PaddyMilling_CMR_DepositOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आपने सही लॉट तथा गोदाम चुना है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे तथा सही लॉट की संख्या और गोदाम चुने|');
        }
    </script>

    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>

    <%--For Calendar Controls--%>
    <link href="calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="calendar/calendar.js"></script>

    <style type="text/css">
        .ButtonClass
        {
            cursor: pointer;
        }

        .auto-style1
        {
            color: #0000FF;
        }

        .auto-style2
        {
            font-size: small;
        }
    </style>

    <table runat="server" align="center" style="width: 740px; border-style: solid; text-align: left; font-size: medium" border="1" cellspacing="0" cellpadding="2">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>CMR Deposit Order</strong>
                <input id="hdfAgrmtDist" type="hidden" runat="server" />
                <input id="hdfDistance" type="hidden" runat="server" />
                <input id="hdfAllQty" type="hidden" runat="server" />
                <input id="hdfMillerDist" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="14">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="14"></td>
        </tr>
        <tr>
            <td>District</td>
            <td>
                <asp:TextBox ID="txtDistManager" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDistManager" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>Crop Year</td>
            <td>

                <asp:DropDownList ID="ddlCropyear" runat="server" Height="27px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropyear_SelectedIndexChanged">
                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCropyear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>मिल का नाम</td>
            <td>
                <asp:DropDownList ID="ddlMillName" runat="server" Height="27px" Width="230px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMillName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>अनुबंध नंबर</td>
            <td>
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Height="27px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAgtmtNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td colspan="3">DO के अनुसार मिलर को प्रदायित धान का लॉट नंबर</td>
            <td>

                <asp:DropDownList ID="ddlLotNO" runat="server" Width="141px" OnSelectedIndexChanged="ddlLotNO_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Miller District</td>
            <td>

                <asp:TextBox ID="txtMillDist" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
            <td>Delivery Order Number</td>
            <td>
                <asp:TextBox ID="txtRONum" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>


            </td>
        </tr>


        <tr>
            <td colspan="4" style="background-color: #999999; color: #999999;"></td>
        </tr>
        <tr style="font-size: small; background-color: #CCFF99;">
            <td colspan="4">लॉट
                क्र.<asp:Label ID="lbllot" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
                तथा DO क्र.<asp:Label ID="lblDONO" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
                &nbsp;के अनुसार मिलर द्वारा उठाई गयी
                <asp:Label ID="lblMillingType" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
               <asp:Label ID="lbltype2" runat="server" Text="" Style="font-weight: 700; color: #0000FF" Visible="false"></asp:Label> मात्रा
                <asp:Label ID="lblDOQty" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
                <span class="auto-style1"><strong>Qtls</strong></span> तथा शेष  <asp:Label ID="lbltype" runat="server" Text="" Style="font-weight: 700; color: #0000FF" Visible="false"></asp:Label>  मात्रा
                <asp:Label ID="lblDORem" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
                <span class="auto-style1"><strong>Qtls</strong></span></td>
        </tr>
        <tr>
            <td colspan="4" style="background-color: #999999; color: #999999;"></td>
        </tr>
        <tr style="font-size: small">
            <td colspan="4" style="background-color: #CCFF99;">मिलर को किस प्रदाय केंद्र के, किस ब्रांच तथा किस गोदाम में लॉट क्र.
                <asp:Label ID="lbllot2" runat="server" Text="" Style="font-weight: 700; color: #0000FF"></asp:Label>
                के विरूद्ध CMR जमा करना है, कृपया चुनाव करे:-</td>
        </tr>


        <tr>
            <td colspan="4" style="background-color: #999999; color: #999999;"></td>
        </tr>


        <tr>
            <td>Deposit District</td>
            <td>

                <asp:DropDownList ID="Ddldist" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="Ddldist_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>प्रदाय केंद्र</td>
            <td>

                <asp:DropDownList ID="ddlIC" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged" Enabled="False">
                </asp:DropDownList>
            </td>
            <td>ब्रांच</td>
            <td>

                <asp:DropDownList ID="ddlBranch" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>


        <tr>
            <td>गोदाम</td>
            <td>

                <asp:DropDownList ID="ddlGodam" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodam_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="auto-style2">CMR जमा करने का दिनांक</td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True"></asp:TextBox>
                &nbsp;
                    
                    <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate', 'restrict=true,instance=single,limit=<%= DateLimit %>'  )" />--%>
                <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' , 'expiry=true,elapse=-150,restrict=true,limit=7,close=true')" />--%>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="font-size: small" runat="server" visible="true" id="trIsAgMovOrder">
            <td colspan="4" style="background-color: #CCFF99;">क्या आप CMR Movement Order के विरुद्ध जमा करना चाहते हैं?
            <asp:DropDownList ID="ddlIsAgainstMovementOrder" runat="server" Width="141px" AutoPostBack="True" Enabled="true" OnSelectedIndexChanged="ddlIsAgainstMovementOrder_SelectedIndexChanged">
                <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>

                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>

                <asp:ListItem Text="No" Value="No"></asp:ListItem>

            </asp:DropDownList>
            </td>

        </tr>
        <tr runat="server" id="trMovementOrder" visible="false">
            <td colspan="3">कृपया Movement Order चुनें</td>
            <td>

                <asp:DropDownList ID="ddlMovementOrder" runat="server" Width="175px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td colspan="6" style="background-color: #CCFF99; color: #0000FF;"></td>
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

