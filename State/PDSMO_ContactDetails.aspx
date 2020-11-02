<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PDSMO_ContactDetails.aspx.cs" Inherits="State_PDSMO_ContactDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .auto-styleNSC {
            width: 500px;
        }

        .ButtonClass {
            cursor: pointer;
            text-align: center;
        }
    </style>

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

        <script type="text/javascript" language="javascript">
            function validate() {
                if (Page_ClientValidate())
                    return confirm('Are You Sure To Save Data?');
            }
    </script>

       <script type="text/javascript">
  
           function onlyNumbersbags(evt) {
               var AsciiCode = event.keyCode;
               var txt = evt.value;
               var txt2 = String.fromCharCode(AsciiCode);
               var txt3 = txt2 * 1;
               if ((AsciiCode <= 46) || (AsciiCode > 57)) {
                   alert('Please enter only numbers.');
                   event.cancelBubble = true;
                   event.returnValue = false;
               }
           }

    </script>

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Alphabets.js"></script>

    <table align="center" style="width: 600px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Employee Contact Details of PDS Movement Department</strong></td>
            <input id="hdfData" type="hidden" runat="server"/>
            <input id="hdfMobileNo" type="hidden" runat="server" />
        </tr>
        <tr>
            <td rowspan="4" style="background-color: #CCCCCC">&nbsp;</td>
            <td>Mobile Number</td>
            <td>
                <asp:TextBox ID="txtDMMobile0" runat="server" Enabled="False" Width="25px">+91</asp:TextBox>

                <asp:TextBox ID="txtDMMobile" runat="server" Width="100px" onkeypress="return onlyNumbersbags(this);" MaxLength="10" autocomplete="off"></asp:TextBox>

                <asp:Button ID="btnSearch" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Search" Width="103px" CssClass="ButtonClass" Style="margin-left: 10px; background-color: #FF9900;" CausesValidation="False" OnClick="btnSearch_Click" />

                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDMMobile" Display="Dynamic"
                    ErrorMessage="RegularExpressionValidator" ValidationExpression="[0-9]{10}">NAN</asp:RegularExpressionValidator>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDMMobile" Display="Dynamic"
                    ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>

                </td>
            <td rowspan="4" style="background-color: #CCCCCC">&nbsp;</td>
        </tr>
        <tr>
            <td>Code of Employee</td>
            <td>
                <asp:DropDownList ID="ddlEmpCode" runat="server" Width="137px" AutoPostBack="True" Enabled="False" Height="18px" OnSelectedIndexChanged="ddlEmpCode_SelectedIndexChanged">
                </asp:DropDownList>

                </td>
        </tr>
        <tr>
            <td>Name of Employee</td>
            <td>

                <asp:TextBox ID="txtDMName" runat="server" Width="250px" MaxLength="28" autocomplete="off" Class="alphaOnly" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDMName" Display="Dynamic"
                    ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>

                </td>
        </tr>
        <tr>
            <td>Email of Employee</td>
            <td>

                <asp:TextBox ID="txtDMMail" runat="server" Width="250px" MaxLength="30" autocomplete="off" Enabled="False" ></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDMMail" Display="Dynamic"
                    ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDMMail" Display="Dynamic"
                    ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>

                </td>
        </tr>

        <tr style="text-align: center">
            <td colspan="4">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />
                <asp:Button ID="btnSave" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Save" Width="115px" CssClass="ButtonClass" OnClientClick="return validate();"  OnClick="btnSave_Click" Style="margin-left: 20px" Enabled="False" />
                <asp:Button ID="btnClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnClose_Click" Style="margin-left: 20px" />
            </td>
        </tr>

        <%--Allow Only Number & One Decimal using class="NumberDecimal" --%>
        <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/NumberDecimal.js"></script>

        <%-- Not Allow Hindi Character's--%>
        <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Alphabets.js"></script>
    </table>

</asp:Content>

