<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="alloc_mpscsc.aspx.cs" Inherits="dist_alloc_mpscsc" Title="District Allocation" %>
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
   
    <table style="border-right: teal 1px solid; border-top: teal 1px solid; border-left: teal 1px solid; border-bottom: teal 1px solid; background-color: #b0cdff; width: 671px;" border="1" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" colspan="4" style="font-weight: bold; font-size: 15pt; color: white;
                height: 39px; background-color: lightslategray;">
                District Allocation for the Scheme Other Than (APL,BPL,AAY)</td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <strong><span style="color: #ff0066">Note:&nbsp; <span style="color: #3300ff">पूरे वर्ष
                    में </span></span><span style="color: #3300ff">पहला आवंटन अप्रैल से जून एवं अंतिम आवंटन
                        जनवरी से मार्च रहेगा , कृपया सावधानी पूर्वक वर्ष चुनें</span></strong></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                Select District Name</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <asp:DropDownList ID="ddldist" runat="server" Width="161px">
                </asp:DropDownList>
                </td>
            <td align="left" style="font-size: 10pt; width: 100px; position: static; background-color: #cfdcc8">
            </td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
               Quarterly Allocation </td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:DropDownList ID="ddl_allot_month" runat="server" Width="207px">
                    
                    <asp:ListItem Value="1">I Quarter(April-June)</asp:ListItem>
                    <asp:ListItem Value="2">II Quarter(July-September)</asp:ListItem>
                    <asp:ListItem Value="3">III Quarter(Oct-November)</asp:ListItem>
                    <asp:ListItem Value="4">IV Quarter(Jan-March)</asp:ListItem>
                    
                </asp:DropDownList></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 100px;">
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
                    ValidationGroup="1" Width="205px">
                </asp:DropDownList></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 100px;">
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
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 100px;">
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
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 100px;">
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

