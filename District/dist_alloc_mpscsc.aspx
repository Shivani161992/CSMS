<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="dist_alloc_mpscsc.aspx.cs" Inherits="District_dist_alloc_mpscsc" Title="District Allocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <script type="text/javascript">
   function CheckIsNumeric(tx)
    {
        var AsciiCode = event.keyCode;
        var txt=tx.value;
        var txt2 = String.fromCharCode(AsciiCode);
        var txt3=txt2*1;
        if ((AsciiCode < 46) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
        {
            alert('Please enter only numbers.');
            event.cancelBubble = true;
            event.returnValue = false;
        }
        var num=tx.value;
        var len=num.length;
        var indx=-1;
        indx=num.indexOf('.');
        if (indx != -1)
        {
            if ((AsciiCode == 46 ))
            {
                alert('Point must be apear only one time.');
                event.cancelBubble = true;
                event.returnValue = false;
            }
            var dgt=num.substr(indx,len);
            var count= dgt.length;
            //alert (count);
            if (count > 5)  
            {
                alert("Only 5 decimal digits allowed");
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }
    }
    </script>
   
    <table style="border-right: teal 1px solid; border-top: teal 1px solid; border-left: teal 1px solid; border-bottom: teal 1px solid; background-color: #b0cdff" border="1" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" colspan="4" style="font-weight: bold; font-size: 15pt; color: white;
                height: 39px; background-color: lightslategray;">
                District Allocation for the Scheme Other Than (APL,BPL,AAY)</td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <asp:Label ID="Label1" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                Allocation Month</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:DropDownList ID="ddl_allot_month" runat="server" Width="167px">
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                Allocation Year</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:DropDownList ID="ddd_allot_year" runat="server" Width="161px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                Commodity</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:DropDownList ID="ddl_commodity" runat="server"
                    ValidationGroup="1" Width="168px">
                </asp:DropDownList></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                Scheme</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:DropDownList ID="ddl_scheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged"
                    Width="161px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                &nbsp;</td>
            <td align="right" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                Allotment
                Quantity</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:TextBox ID="TextBox1" runat="server" Width="109px" MaxLength="12"></asp:TextBox>*</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                Qtls.</td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
            </td>
            <td align="right" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:Button ID="save" runat="server" Font-Bold="True" Font-Size="Small"
                    Text="Save" Width="107px" OnClick="save_Click" ValidationGroup="1" /></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="Small"
                    OnClick="Button1_Click" Text="New" Width="106px" /></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Size="Small" OnClick="Button2_Click"
                    Text="Close" Width="107px" /></td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
        ErrorMessage="Please enter Allotment Quantity" ValidationGroup="1"> </asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="1" />
</asp:Content>

