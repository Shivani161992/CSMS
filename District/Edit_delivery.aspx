<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master"  AutoEventWireup="true" CodeFile="Edit_delivery.aspx.cs" Inherits="Edit_delivery" Title="Edit Delivery Order" %>
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


}
    </script>
	
    <div>
    <asp:Panel ID="panel_do" runat ="server" >
        <table>
        <tr><td align="center" style="width: 736px; font-weight: bold; color: black; background-color: #cccccc;">
            Edit Delivery Order</td></tr>
            <tr>
                <td style="vertical-align: top; text-align: left;">
                <fieldset style="width: 570px" >
                <legend>
                 Permit / Release Order Details
                </legend>
                    <table style="width: 614px; background-color: lightblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="4" style="font-size: 10pt; position: static; background-color: gainsboro;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 41px;" align="left">
                                <asp:Label ID="lbldono" runat="server" Font-Size="12px" Text="DO No."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 41px;" align="left">
                                <asp:DropDownList ID="ddl_do_no" runat="server" AutoPostBack="True" 
                                    Width="128px" OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 41px;" align="left">
                                <asp:Label ID="lbldodate" runat="server" Font-Size="12px" Text="DO Date"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 41px;" align="left">
                                &nbsp;<asp:TextBox ID="tx_do_date" runat="server" Width="121px"></asp:TextBox>
                                <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_date'
	    });
	     </script>
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblQuantity" runat="server" Font-Size="12px" ForeColor="Black" Text="DO Quantity"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_tot_qty" runat="server" BackColor="Linen" MaxLength="8"
                                    Width="120px" OnTextChanged="tx_tot_qty_TextChanged"></asp:TextBox>Qtls.</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbldovalidity" runat="server" Font-Size="12px" Text="Validity Date"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                &nbsp;<asp:TextBox ID="tx_do_validity" runat="server" Width="122px"></asp:TextBox>
                                 <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_validity'
	    });
	     </script>
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblRateQuintal" runat="server" Font-Size="12px" Text="Rate/Quintal"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_rate_qt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="120px"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbltotamt" runat="server" Font-Size="12px" Text="Total Amount"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_tot_amt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="148px"></asp:TextBox>Rs.</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="lbltoissue" runat="server" Font-Size="12px" Text="Issue To"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:TextBox ID="tx_issueto" runat="server" MaxLength="12" Width="120px" ReadOnly="True"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="lbl_lead" runat="server" Font-Size="12px" Text="Name" Width="40px"></asp:Label></td>
                            <td colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:DropDownList ID="ddl_lead" runat="server" Font-Names="Kruti Dev 010"
                                    Font-Size="Medium" Enabled="False" Visible="False">
                                </asp:DropDownList><asp:TextBox ID="tx_lead" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label4" runat="server" Text="Permit No." Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_permit_no" runat="server" Width="120px" MaxLength="15" Visible="False"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label5" runat="server" Text="Permit Date" Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                &nbsp;<asp:TextBox ID="tx_permit_date" runat="server" Width="121px" Visible="False"></asp:TextBox>
                                 <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_permit_date'
	    });
	     </script>
                                
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblallotyear" runat="server" Font-Size="12px" Text="Allot. Year"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddd_allot_year" runat="server" Enabled="False" Width="130px">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblallotmonth" runat="server" Font-Size="12px" Text="Allot. Month"
                                    Width="67px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_allot_month" runat="server" Enabled="False" Width="158px">
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
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblCommodity" runat="server" Font-Size="12px" Text="Commodity"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_commodity" runat="server" Enabled="False"
                                    ValidationGroup="1" Width="130px">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblScheme" runat="server" Font-Size="12px" Text="Scheme"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_scheme" runat="server" Enabled="False"
                                    Width="160px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label15" runat="server" Text="Label" Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="txtrono" runat="server" ReadOnly="True" Width="122px" Visible="False"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_issueqty1" runat="server" Text="Issued Quantity"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_issue_balqty" runat="server" BackColor="Linen" ReadOnly="True"
                                    Width="90px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; background-color: #cfdcdc; position: static;">
                            </td>
                            <td style="font-size: 10pt; background-color: #cfdcdc; position: static;">
                            </td>
                            <td align="center" colspan="2" style="font-size: 10pt; background-color: #cfdcdc; position: static;">
                                <asp:Label ID="lbl_dodtl" runat="server" Text="DO Details FPS - Wise" Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_blk" runat="server" ForeColor="Black" Text="FPS Block" Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_block" runat="server" AutoPostBack="True" Font-Names="DVBW-TTYogeshEN"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged" Width="125px" Visible="False">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_fps" runat="server" ForeColor="Black" Text="FPS Name" Visible="False"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_fps_name" runat="server" AutoPostBack="True" Font-Names="DVBW-TTYogeshEN"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddl_fps_name_SelectedIndexChanged" Visible="False">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_allotqty" runat="server" ForeColor="Black" Text="Allotment Qty." Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_allot_qty2" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="116px" Visible="False"></asp:TextBox></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_issueqty" runat="server" ForeColor="Black" Style="z-index: 100"
                                    Text="Already Issued Qty" Visible="False"></asp:Label>
                                <asp:TextBox ID="tx_already_iqty2" runat="server" BackColor="Linen" MaxLength="8"
                                    ReadOnly="True" Width="90px" Visible="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_qty" runat="server" ForeColor="Black" Text="Quantity" Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="100px" Visible="False"></asp:TextBox><asp:Label
                                    ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"
                                    Style="left: 0px" Text="*" Visible="False"></asp:Label></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_qtls1" runat="server" ForeColor="Black" Text="Qtls." Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_balqty" runat="server" ForeColor="Black" Style="z-index: 100;
                                    left: -85px; top: 0px" Text="Balance Qty" Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_balQty2" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="118px" Visible="False"></asp:TextBox></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_qtls" runat="server" ForeColor="Black" Text="Qtls." Visible="False"></asp:Label>
                                <asp:Button ID="Button1" runat="server" Enabled="False" OnClick="Button1_Click" Text="Add FPS"
                                    ValidationGroup="2" Visible="False" /></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                &nbsp;<asp:Label ID="lbldoqty" runat="server" Visible="False"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" colspan="1" align="left">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table style="width: 610px">
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="580px">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                                        Font-Size="Small" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <RowStyle ForeColor="#000066" />
                                        <Columns>
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
            <td style="vertical-align: top; text-align: left;">
                <fieldset style="width: 593px" >
                <legend>
                Payment Details
                </legend>
                    <table style="width: 610px; background-color: lightsteelblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                <asp:Label ID="lblPaymentMode" runat="server" Font-Size="12px" Text="Payment Mode"
                                    Width="92px"></asp:Label></td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                <asp:DropDownList ID="ddl_pmode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged" Width="161px">
                                    <asp:ListItem Value="D">DD / Cheque</asp:ListItem>
                                    <asp:ListItem Value="R">Free Scheme / Credit </asp:ListItem>
                                    <asp:ListItem Value="A">Cash</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                <asp:Label ID="lblddchekno" runat="server" Font-Size="12px" Text="DD/Chq. No. "></asp:Label></td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                <asp:TextBox ID="tx_dd_no" runat="server" Width="112px" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"
                                    Text="*"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                <asp:Label ID="lblddchekdate" runat="server" Font-Size="12px" Text="DD/Chq. Date"></asp:Label></td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                &nbsp;<asp:TextBox ID="tx_dd_date" runat="server" Width="123px"></asp:TextBox>
                                
                                 <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_dd_date'
	    });
	     </script>
                                
                                </td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                <asp:Label ID="lblamount" runat="server" Font-Size="12px" Text="Amount"></asp:Label></td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                <asp:TextBox ID="tx_dd_amount" runat="server" MaxLength="12" Width="114px"></asp:TextBox><asp:Label ID="lbl_amt" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                <asp:Label ID="Label2" runat="server" ForeColor="Black" Style="z-index: 101; left: 0px"
                                    Text="Rs." Font-Size="11px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                <asp:Label ID="lblBankName" runat="server" Font-Size="12px" Text="Bank Name"></asp:Label></td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                    <asp:DropDownList ID="ddl_bank" runat="server" Width="160px">
                                    </asp:DropDownList></td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                            </td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                            </td>
                            <td align="right" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                               <asp:Button ID="save" runat="server" Text="Save" Font-Bold="True" Font-Size="Small" Width="96px" OnClick="save_Click" ValidationGroup="1" Font-Italic="True" /></td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                            <asp:Button ID="Close" runat="server" Text="Close" Font-Bold="True" Font-Size="Small" Width="112px" OnClick="Close_Click" Font-Italic="True"/></td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                <asp:Button ID="btn_new" runat="server" Font-Bold="True" Font-Size="Small" OnClick="btn_new_Click"
                                    Text="New" Width="92px" Font-Italic="True" /></td>
                        </tr>
                        <tr>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                &nbsp;</td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                &nbsp;</td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                &nbsp;</td>
                            <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                                <asp:HyperLink ID="hlinkpdo" runat="server" NavigateUrl="#" Width="133px" Visible="False">Issue Delivery Order</asp:HyperLink></td>
                        </tr>
                    </table>                   
                </fieldset> &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_permit_no"
                    ErrorMessage="Please enter Permit No." Height="0px" ValidationGroup="2" Width="0px">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tx_dd_no"
                    ErrorMessage="Please enter DD No." Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tx_dd_amount"
                    ErrorMessage="Please enter DD Amount" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tx_qty"
                    ErrorMessage="Please enter quantity" Height="0px" ValidationGroup="2" Width="0px">*</asp:RequiredFieldValidator>
                &nbsp; &nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="Small" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="1" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="Small" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="2" />
                </td>
            </tr>
        </table>
        </asp:Panel>
    </div>

</asp:Content>

