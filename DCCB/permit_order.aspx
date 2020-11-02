<%@ Page Language="C#" MasterPageFile="~/MasterPage/DCCB_Master.master"  AutoEventWireup="true" CodeFile="permit_order.aspx.cs" Inherits="permit_order" Title="Permit Order" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




<script type="text/javascript">
function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
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
	
    <div style="text-align: right">
    <asp:Panel ID="panelDO" runat ="server"  >
        <table style="width: 704px">
        <tr><td align="center">
            <strong style="color: maroon; background-color: white">Permit Order Details</strong></td></tr>
            <tr>
                <td>
                <fieldset >
                <legend style="color: white; background-color: maroon">
                Permit / Release Order Details
                </legend>
                    <table style="width: 688px">
                        <tr>
                            <td colspan="7" style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; height: 8px; background-color: #cfdcc8;">
                                <asp:Label ID="Label15" runat="server" Text="Allotment Month "></asp:Label></td>
                            <td align="left" style="font-size: 10pt; height: 8px; background-color: #cfdcc8;">
                                <asp:DropDownList ID="ddl_allot_month" runat="server" Width="105px" OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged">
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
                            <td align="left" style="font-size: 10pt; height: 8px; background-color: #cfdcc8;">
                                <asp:Label ID="Label16" runat="server" Text="Allotment Year"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; height: 8px; background-color: #cfdcc8;">
                                <asp:DropDownList ID="ddd_allot_year" runat="server">
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;">
                                <asp:Label ID="Label17" runat="server" Text=" Issue Center "></asp:Label></td>
                            <td align="left" style="font-size: 10pt; height: 8px; background-color: #cfdcc8;">
                                <asp:DropDownList ID="ddlissue" runat="server" Width="138px" AutoPostBack="True" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged">
     <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList></td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                Permit No.</td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                <asp:TextBox ID="tx_permit_no" runat="server" Width="125px" MaxLength="15"></asp:TextBox></td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                Permit Date</td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                             <asp:TextBox ID="tx_permit_date" runat="server"></asp:TextBox>
                                <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_permit_date'
	    });
	     </script>
                                
                            </td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;">
                                Validity</td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                <asp:TextBox ID="tx_permit_validity" runat="server" Width="95px" MaxLength="2"></asp:TextBox></td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;">
                                Days</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                Issue To</td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                <asp:DropDownList ID="ddl_issueto" runat="server" Width="128px" AutoPostBack="True" OnSelectedIndexChanged="ddl_issueto_SelectedIndexChanged">
                                <asp:ListItem Value="L">Lead Society</asp:ListItem>
                                    <asp:ListItem Value="F">FPS</asp:ListItem>
                                <asp:ListItem Value="O">Others</asp:ListItem>
                            </asp:DropDownList></td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="lbl_lead" runat="server" Text="Name" Font-Size="Small" Width="40px"></asp:Label></td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                &nbsp;<asp:DropDownList ID="ddl_lead" runat="server" Font-Names="Kruti Dev 010" Width="172px" Font-Size="Medium" AutoPostBack="True" OnSelectedIndexChanged="ddl_lead_SelectedIndexChanged">
                                </asp:DropDownList><asp:Label ID="lbl_ld_v" runat="server" Font-Bold="True" Font-Size="Medium" Text="*"></asp:Label></td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label5" runat="server" ForeColor="Black" Text="FPS Block" Width="57px"></asp:Label></td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;" align="left">
                                <asp:DropDownList ID="ddl_block" runat="server" Width="136px" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" AutoPostBack="True">
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; height: 8px; background-color: #cfdcc8;">
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="background-color: lightgrey">
                            </td>
                            <td align="left" style="background-color: lightgrey">
                            </td>
                            <td align="left" style="background-color: lightgrey">
                            </td>
                            <td align="left" style="background-color: lightgrey">
                                DO Details FPS wise</td>
                            <td align="left" style="background-color: lightgrey">
                            </td>
                            <td align="left" style="background-color: lightgrey">
                            </td>
                            <td style="background-color: lightgrey">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label6" runat="server" Text="FPS Name" Width="55px"></asp:Label></td>
                            <td align="left" colspan="2" style="font-size: 10pt; width: 140px; color: black;
                                position: static; background-color: lightsteelblue">
                                <asp:DropDownList ID="ddl_fps_name" runat="server" Font-Names="DVBW-TTYogeshEN" Width="206px" Font-Size="Medium" AutoPostBack="True" OnSelectedIndexChanged="ddl_fps_name_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label18" runat="server" Text="Commodity" Width="66px"></asp:Label>
                                <asp:DropDownList ID="ddl_commodity" runat="server" Width="88px" ValidationGroup="1">
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label20" runat="server" Text="Scheme" Width="42px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:DropDownList ID="ddl_scheme" runat="server" Width="80px" AutoPostBack="True" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; width: 140px; color: black; position: static; background-color: lightsteelblue">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label7" runat="server" Text="Allotment Qty  " Width="77px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:TextBox ID="tx_allot_qty" runat="server" MaxLength="8" Width="90px" BackColor="Linen" ReadOnly="True"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label8" runat="server" Text="Already Issued Qty"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:TextBox ID="tx_already_iqty" runat="server" BackColor="Linen" MaxLength="8"
                                    ReadOnly="True" Width="74px"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label9" runat="server" Text=" Balance Qty"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:TextBox ID="tx_balQty" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox></td>
                            <td style="font-size: 10pt; width: 140px; color: black; position: static; background-color: lightsteelblue">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label10" runat="server" Text="Bal. at IssueCentre"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:TextBox ID="tx_bal_ic" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label11" runat="server" Text="Rate/Quintal   "></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:TextBox ID="tx_rate_qt" runat="server"
                                    MaxLength="8" Width="74px" ReadOnly="True" BackColor="Linen"></asp:TextBox>Rs.&nbsp;</td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label12" runat="server" Text="Quantity"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="85px"></asp:TextBox>
                                <asp:Label
                                    ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label></td>
                            <td style="font-size: 10pt; width: 140px; color: black; position: static; background-color: lightsteelblue">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label13" runat="server" Text="Total Quantity    "></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:TextBox ID="tx_tot_qty" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="83px"></asp:TextBox>Qtls.</td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Label ID="Label14" runat="server" Text="Total Amount"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:TextBox ID="tx_tot_amt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="74px"></asp:TextBox>Rs.</td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                                <asp:Button ID="Button1" runat="server" Text="Add FPS" OnClick="Button1_Click" ValidationGroup="2" /></td>
                            <td align="left" style="font-size: 10pt; width: 140px; color: black; position: static;
                                background-color: lightsteelblue">
                            </td>
                            <td style="font-size: 10pt; width: 140px; color: black; position: static; background-color: lightsteelblue">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 600px">
                        <tr>
                            <td style="font-size: 10pt" align="left">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="680px">
                                <asp:GridView ID="GridView1" runat="server"
                                    Width="680px" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" Font-Bold="False" Font-Size="Small">
                                    <Columns>
                                        <asp:CommandField SelectText="Delete" ShowSelectButton="True" >
                                            <ItemStyle Font-Bold="False" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="fps_name" HeaderText="FPS Name" SortExpression="fps_name">
                                            <ItemStyle Font-Bold="False" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="commodity_name" HeaderText="Commodity" SortExpression="commodity_name">
                                            <ItemStyle Font-Bold="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="scheme_name" HeaderText="Scheme" SortExpression="scheme_name">
                                            <ItemStyle Font-Bold="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="allot_qty" HeaderText="Against Allot." SortExpression="allot_qty" />
                                        <asp:BoundField DataField="qty" HeaderText="Quantity" SortExpression="qty">
                                            <ItemStyle Font-Bold="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="rate_qtls" HeaderText="Rate/Qtls." SortExpression="rate_qtls">
                                            <ItemStyle Font-Bold="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="amt" HeaderText="Amount" SortExpression="amt">
                                            <ItemStyle Font-Bold="False" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle ForeColor="#000066" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="False" Font-Size="Small" ForeColor="White" />
                                </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                
                </td>
            </tr>
            
            <tr>
            <td style="width: 736px">
                <fieldset >
                <legend style="color: white; background-color: maroon">
                Payment Details
                </legend>
                    <table style="width: 688px">
                        <tr>
                            <td style="font-size: 10pt; color: black; height: 27px; background-color: #cfdcc8;" align="left">
                                Payment Mode</td>
                            <td style="font-size: 10pt; height: 27px; color: black; background-color: #cfdcc8;" align="left"><asp:DropDownList ID="ddl_pmode" runat="server" Width="104px" AutoPostBack="True" OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged">
                                <asp:ListItem Value="D">DD</asp:ListItem>
                                <asp:ListItem Value="H">Cheque</asp:ListItem>
                                <asp:ListItem Value="R">Credit</asp:ListItem>
                                <asp:ListItem Value="A">Cash</asp:ListItem>
                            </asp:DropDownList></td>
                            <td style="font-size: 10pt; height: 27px; color: black; background-color: #cfdcc8;" colspan="2" align="left">
                                DD/Chq. No.
                                <asp:TextBox ID="tx_dd_no" runat="server" Width="120px" MaxLength="50"></asp:TextBox><asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" Text="*" ForeColor="Red"></asp:Label></td>
                            <td style="font-size: 10pt; height: 27px; color: black; background-color: #cfdcc8;" align="left">
                                DD/Chq.
                                Date</td>
                            <td style="font-size: 10pt; height: 27px; color: black; background-color: #cfdcc8;" align="left">
                                <asp:TextBox ID="tx_dd_date" runat="server"></asp:TextBox>
                                <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_dd_date'
	    });
	     </script>
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px; background-color: #cfdcc8;">
                                Bank Name</td>
                            <td align="left" colspan="3" style="font-size: 10pt; color: black; height: 27px; background-color: #cfdcc8;">
                                <asp:DropDownList ID="ddl_bank" runat="server" >
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px; background-color: #cfdcc8;">
                                Amount</td>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px; background-color: #cfdcc8;">
                                <asp:TextBox ID="tx_dd_amount" runat="server" Width="112px" MaxLength="12"></asp:TextBox><asp:Label
                                    ID="lbl_amt" runat="server" Text="*" style="z-index: 100" ForeColor="Red"></asp:Label>
                                <asp:Label
                                    ID="Label2" runat="server" ForeColor="Black" Text="Rs."></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px; background-color: #cfdcc8;">
                            </td>
                            <td align="right" colspan="3" style="font-size: 10pt; color: black; height: 27px; background-color: #cfdcc8;">
                                <asp:Button ID="save" runat="server" Text="Save" Font-Bold="False" Font-Size="Medium" Width="104px" OnClick="save_Click" ValidationGroup="1" /></td>
                            <td align="left" colspan="2" style="font-size: 10pt; color: black; height: 27px; background-color: #cfdcc8;">
                                <asp:Button ID="btnClose" runat="server" Font-Size="Medium" OnClick="btnClose_Click"
                                    Text="Close" Width="90px" /></td>
                        </tr>
                    </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <span>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="Small" ShowMessageBox="True"
                        ShowSummary="False" Style="left: 0px; top: 0px" ValidationGroup="1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_qty"
                        ErrorMessage="Please enter quantity" Height="0px" ValidationGroup="2" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="tx_dd_no" ErrorMessage="Please enter DD No."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="tx_dd_amount"
                                    ErrorMessage="Please enter DD Amount" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_commodity"
                        ErrorMessage="Please enter commodity" Height="0px" ValidationGroup="2" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tx_rate_qt"
                        ErrorMessage="Please enter Rate" Height="0px" ValidationGroup="2"
                        Width="0px">*</asp:RequiredFieldValidator><asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="Small" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="2" />
        <asp:HyperLink id="hlinkpdo" runat="server" NavigateUrl="#" Visible="False">Print Permit Order</asp:HyperLink></span></div>

</asp:Content>

