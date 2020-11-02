<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="DeliveryOrder_FCI.aspx.cs" Inherits="IssueCenter_DeliveryOrder_FCI" Title="Delivery Order" Debug="true"  %>
 
<%@ Register Assembly="CustomControlFreak" Namespace="CustomControlFreak" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
    function CheckIsNumeric(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;                        
        if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
        {
            alert('Please enter only numbers.');
            return false;
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
                return false;
            }
            var dgt=num.substr(indx,len);
            var count= dgt.length;
            //alert (count);
            if (count > 5 && AsciiCode != 8)  
            {
                alert("Only 5 decimal digits allowed.");
                return false;
            }
        }
    }
    </script>
	
    <div style="text-align: right">
    <asp:Panel ID="panelDO" runat ="server"  >
        <table>
        <tr><td align="center">
            <strong style="color: maroon; background-color: white">Delivery Order FCI</strong></td></tr>
            <tr>
                <td>
                <fieldset style="vertical-align: top; text-align: left" >
                <legend>
                Permit / Release Order Details
                </legend>
                    <table style="width: 744px; background-color: skyblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="6" style="font-size: 10pt;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 104px; height: 8px">
                                Issue To</td>
                            <td align="left" style="font-size: 10pt; position: static; height: 8px; width: 18px;">
                                <asp:DropDownList ID="ddl_issueto" runat="server">
                                <asp:ListItem Value="FCI">FCI</asp:ListItem>
                            </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; width: 64px; height: 8px">
                                <asp:Label ID="lbl_lead" runat="server" Text="Name" Font-Size="Small" Width="40px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; color: red;">
                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="178px" MaxLength="30"></asp:TextBox>*</td>
                            <td style="font-size: 10pt; height: 8px; width: 64px;" align="left">
                                Allot.
                                Year</td>
                            <td align="left" style="font-size: 10pt; width: 113px; color: red; height: 8px">
                                <asp:DropDownList ID="ddd_allot_year" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 104px;">
                                Allot. Month</td>
                            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 8px">
                                <asp:DropDownList ID="ddl_allot_month" runat="server">
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
                            <td align="left" style="font-size: 10pt; width: 64px; height: 8px">
                                Commodity</td>
                            <td align="left" style="font-size: 10pt; width: 139px; color: black;">
                                <asp:DropDownList ID="ddl_commodity" runat="server" ValidationGroup="1" OnSelectedIndexChanged="ddl_commodity_SelectedIndexChanged" >
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; width: 64px; height: 8px">
                                Scheme</td>
                            <td align="left" style="font-size: 10pt; width: 113px; color: red; height: 8px">
                                <asp:DropDownList ID="ddl_scheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged" Width="193px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 104px">
                            </td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; height: 8px">
                                Balance Stock of Commodity</td>
                            <td align="left" style="font-size: 10pt; width: 139px; color: black; height: 8px">
                                <asp:TextBox ID="txtcomdty_bal" runat="server" BackColor="Linen" Font-Bold="False"
                                    ForeColor="Black" ReadOnly="True" Width="91px"></asp:TextBox>Qtls.</td>
                            <td align="left" style="font-size: 10pt; width: 64px; height: 8px">
                                Rate Type</td>
                            <td align="left" style="font-size: 10pt; width: 113px; color: red; height: 8px">
                                <asp:DropDownList ID="ddl_rate_type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_rate_type_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="R">Rural</asp:ListItem>
                                    <asp:ListItem Value="U">Urban</asp:ListItem>
                                    <asp:ListItem Value="C">Consumers</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 12pt; width: 104px; height: 5px; background-color: #cccccc;">
                            </td>
                            <td align="left" style="font-size: 12pt; height: 5px; width: 18px; background-color: #cccccc;">
                            </td>
                            <td align="center" style="font-size: 12pt; height: 5px; background-color: #cccccc;" colspan="2">
                                Delivery Order Details</td>
                            <td style="font-size: 12pt; height: 5px; width: 64px; background-color: #cccccc;">
                            </td>
                            <td align="left" style="font-size: 12pt; height: 5px; background-color: #cccccc;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt">
                                DO No.</td>
                            <td align="left" style="font-size: 10pt; width: 116px; color: red; position: static">
                                <asp:TextBox ID="tx_do_no" runat="server" MaxLength="15" Width="102px"></asp:TextBox>*</td>
                            <td align="left" style="font-size: 10pt">
                                DO Date</td>
                            <td align="left" style="font-size: 10pt; color: black">
                                <cc1:DaintyDate ID="tx_do_date" runat="server" FormatType="DDMMYYYY" />
                            </td>
                            <td align="left" style="font-size: 10pt">
                                DO Validity</td>
                            <td align="left" style="font-size: 10pt; color: red">
                                <cc1:DaintyDate ID="tx_do_validity" runat="server" FormatType="DDMMYYYY" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 104px">
                                DO
                                Quantity</td>
                            <td align="left" style="font-size: 10pt; width: 18px">
                                <asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="100px" AutoPostBack="True" OnTextChanged="tx_qty_TextChanged"></asp:TextBox><asp:Label
                                    ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="*" style="left: 0px"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; width: 64px;">
                                Qtls.</td>
                            <td align="left" colspan="2" style="font-size: 10pt">
                                &nbsp;</td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                </td>
                        </tr>
                    </table>
                </fieldset>
                
                </td>
            </tr>
            
            <tr>
            <td>
                <fieldset style="vertical-align: top; text-align: left" >
                <legend>
                Payment Details
                </legend>
                    <table style="width: 740px; background-color: lightsteelblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 87px; color: black; height: 27px">
                                Rate/Quintal</td>
                            <td align="left" style="font-size: 10pt; width: 149px; color: black; height: 27px">
                                <asp:TextBox ID="tx_rate_qt" runat="server"
                                    MaxLength="8" Width="90px" ReadOnly="True" BackColor="Linen"></asp:TextBox></td>
                            <td align="left" colspan="2" style="font-size: 10pt; width: 213px; color: black;
                                height: 27px">
                                Total Amount&nbsp;<asp:TextBox ID="tx_tot_amt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="100px"></asp:TextBox>
                                Rs.</td>
                            <td align="left" style="font-size: 10pt; width: 79px; color: black; height: 27px">
                                &nbsp;</td>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; color: black; height: 27px; width: 87px;" align="left">
                                Payment Mode</td>
                            <td style="font-size: 10pt; height: 27px; color: black; width: 149px;" align="left"><asp:DropDownList ID="ddl_pmode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged">
                                <asp:ListItem Value="D">DD / Cheque</asp:ListItem>
                                <asp:ListItem Value="R">Free Scheme / Credit </asp:ListItem>
                                <asp:ListItem Value="A">Cash</asp:ListItem>
                            </asp:DropDownList></td>
                            <td style="font-size: 10pt; height: 27px; color: black; width: 213px;" colspan="2" align="left">
                                DD/Chq. No.&nbsp; &nbsp;<asp:TextBox ID="tx_dd_no" runat="server" Width="120px" MaxLength="50"></asp:TextBox><asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" Text="*" ForeColor="Red"></asp:Label></td>
                            <td style="font-size: 10pt; height: 27px; color: black; width: 79px;" align="left">
                                DD/Chq.
                                Date</td>
                            <td style="font-size: 10pt; height: 27px; color: black;" align="left">
                                <cc1:DaintyDate ID="tx_dd_date" runat="server" FormatType="DDMMYYYY" />
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px; width: 87px;">
                                Bank Name</td>
                            <td align="left" colspan="3" style="font-size: 10pt; color: black; height: 27px;">
                                <asp:DropDownList ID="ddl_bank" runat="server" >
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px; width: 79px;">
                                Amount</td>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px;">
                                <asp:TextBox ID="tx_dd_amount" runat="server" Width="112px" MaxLength="12"></asp:TextBox><asp:Label
                                    ID="lbl_amt" runat="server" Text="*" style="z-index: 100" ForeColor="Red"></asp:Label>
                                <asp:Label
                                    ID="Label2" runat="server" ForeColor="Black" Text="Rs."></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px; width: 87px;">
                            </td>
                            <td align="right" colspan="3" style="font-size: 10pt; color: black; height: 27px;">
                                <asp:Button ID="save" runat="server" Text="Save" Font-Bold="False" Font-Size="Medium" Width="104px" OnClick="save_Click" ValidationGroup="1" /></td>
                            <td align="left" colspan="2" style="font-size: 10pt; color: black; height: 27px;">
                                <asp:Button ID="btnClose" runat="server" Font-Size="Medium" OnClick="btnClose_Click"
                                    Text="Close" Width="90px" />
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click"
                                    Text="New" Width="75px" /></td>
                        </tr>
                    </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox1"
            ErrorMessage="Please enter name of IssueTo" Height="0px" ValidationGroup="1"
            Width="0px">*</asp:RequiredFieldValidator>
        <span>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="Small" ShowMessageBox="True"
                        ShowSummary="False" Style="left: 0px; top: 0px" ValidationGroup="1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_qty"
                        ErrorMessage="Please enter quantity" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tx_do_no" ErrorMessage="Please enter delivery order no."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="tx_dd_no" ErrorMessage="Please enter DD No."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="tx_dd_amount"
                                    ErrorMessage="Please enter DD Amount" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_commodity"
                        ErrorMessage="Please enter commodity" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tx_rate_qt"
                        ErrorMessage="Please enter Rate" Height="0px" ValidationGroup="1"
                        Width="0px">*</asp:RequiredFieldValidator>
        <asp:HyperLink id="hlinkpdo" runat="server" NavigateUrl="#" Visible="False">Issue Delivery Order</asp:HyperLink></span></div>

</asp:Content>