<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master"  AutoEventWireup="true" CodeFile="Edit_delivery.aspx.cs" Inherits="Edit_delivery" Title="Delivery Order" %>
 
 
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
    
    <div >
    <asp:Panel ID="panel_do" runat ="server" >
        <table>
        <tr><td align="center" style="background-color: #cccccc;">
            <asp:Label ID="lbldo" runat="server" Font-Bold="True" ForeColor="Transparent" Text="Edit Delivery Order"
                Width="198px"></asp:Label></td></tr>
            <tr>
                <td style="vertical-align: top; text-align: left;">
                <fieldset >
                <legend>
                 Permit / Release Order Details
                </legend>
                    <table style="width: 744px; background-color: lightblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="7" style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                DO No.</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:DropDownList ID="ddl_do_no" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                DO Date</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:TextBox ID="tx_do_date" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_date'
	    });
	     </script>
                                </td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                DO Validity</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left" colspan="2">
                            
                            <asp:TextBox ID="tx_do_validity" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_validity'
	    });
	     </script></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                Issue To</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:DropDownList ID="ddl_issueto" runat="server" AutoPostBack="True" Enabled="False">
                                    <asp:ListItem Value="LF">Lead Society</asp:ListItem>
                                    <asp:ListItem Value="F">Only FPS</asp:ListItem>
                                    <asp:ListItem Value="O">Others</asp:ListItem>
                                    <asp:ListItem Value="L">Lead Society</asp:ListItem>
                                    <asp:ListItem>FCI</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="lbl_lead" runat="server" Text="Name" Font-Size="Small" Width="40px"></asp:Label></td>
                            <td colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:DropDownList ID="ddl_lead" runat="server" Font-Names="Kruti Dev 010"
                                    Font-Size="Medium" AutoPostBack="True" OnSelectedIndexChanged="ddl_lead_SelectedIndexChanged">
                                </asp:DropDownList><asp:TextBox ID="tx_lead" runat="server" Visible="False" MaxLength="130"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                Allot. Year</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddd_allot_year" runat="server" Enabled="False">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                Permit No.</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_permit_no" runat="server" Width="125px" MaxLength="15"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                Permit Date</td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_permit_date" runat="server" Width="119px"></asp:TextBox>
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
                                Allot.
                                Month</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_allot_month" runat="server" Enabled="False">
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
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                Commodity</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_commodity" runat="server"
                                    ValidationGroup="1" AutoPostBack="True" OnSelectedIndexChanged="ddl_commodity_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                Scheme</td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_scheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                            </td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                Balance Stock of Commodity</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="txtcomdty_bal" runat="server" BackColor="Linen" Font-Bold="False"
                                    ForeColor="Black" ReadOnly="True" Width="91px"></asp:TextBox>Qtls.</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                Rate Type</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_rate_type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_rate_type_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="R">Rural</asp:ListItem>
                                    <asp:ListItem Value="U">Urban</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_alloc" runat="server" Text="Allocation for the Month"></asp:Label>&nbsp;
                                <asp:TextBox ID="tx_allot_qty" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox><asp:Label ID="lbl_qtalloc" runat="server" Text="Qtls."></asp:Label></td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_issue" runat="server" Text="DO already Issued Qty."></asp:Label>&nbsp;
                                <asp:TextBox ID="tx_already_iqty" runat="server" BackColor="Linen" MaxLength="8"
                                    ReadOnly="True" Width="90px"></asp:TextBox><asp:Label ID="lbl_qtissue" runat="server"
                                        Text="Qtls."></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_curbal" runat="server" Text="Current Balance Eligibility"></asp:Label>
                                <asp:TextBox ID="tx_balQty" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox><asp:Label ID="lbl_qtcur" runat="server" Text="Qtls."></asp:Label></td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_stock" runat="server" Text="Balance Stock of Commodity/Scheme"></asp:Label>
                                <asp:TextBox ID="tx_bal_ic" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox><asp:Label ID="lbl_qtstk" runat="server" Text="Qtls."></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 104px; height: 5px; background-color: #cccccc">
                            </td>
                            <td style="font-size: 10pt; width: 18px; height: 5px; background-color: #cccccc">
                            </td>
                            <td align="center" colspan="2" style="font-size: 12pt; height: 5px; background-color: #cccccc">
                                <asp:Label ID="lbl_dodtl" runat="server" Text="DO Details FPS - Wise" Visible="False"></asp:Label></td>
                            <td style="font-size: 10pt; width: 64px; height: 5px; background-color: #cccccc">
                            </td>
                            <td style="font-size: 10pt; width: 113px; height: 5px; background-color: #cccccc">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lbl_blk" runat="server" ForeColor="Black" Text="FPS Block" Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:DropDownList ID="ddl_block" runat="server" AutoPostBack="True" Font-Names="DVBW-TTYogeshEN"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged" Visible="False">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lbl_fps" runat="server" ForeColor="Black" Text="FPS Name" Visible="False"></asp:Label></td>
                            <td align="left" colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:DropDownList ID="ddl_fps_name" runat="server" AutoPostBack="True" Font-Names="DVBW-TTYogeshEN"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddl_fps_name_SelectedIndexChanged" Visible="False">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lbl_allotqty" runat="server" ForeColor="Black" Text="Allotment Qty." Visible="False" Width="91px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:TextBox ID="tx_allot_qty2" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px" Visible="False"></asp:TextBox></td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lbl_issueqty" runat="server" ForeColor="Black" Style="z-index: 100"
                                    Text="Already Issued Qty" Visible="False"></asp:Label>
                                <asp:TextBox ID="tx_already_iqty2" runat="server" BackColor="Linen" MaxLength="8"
                                    ReadOnly="True" Width="98px" Visible="False"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lbl_balqty" runat="server" ForeColor="Black" Style="z-index: 100;
                                    left: -85px; top: 0px" Text="Balance Qty" Visible="False"></asp:Label>
                                <asp:TextBox ID="tx_balQty2" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px" Visible="False"></asp:TextBox>
                                <asp:Label ID="lbl_qtls" runat="server" ForeColor="Black" Text="Qtls." Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lbl_qty" runat="server" ForeColor="Black" Text="Quantity" Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="100px" Visible="False"></asp:TextBox><asp:Label
                                    ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"
                                    Style="left: 0px" Text="*" Visible="False"></asp:Label></td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lbl_qtls1" runat="server" ForeColor="Black" Text="Qtls." Visible="False"></asp:Label>
                                </td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Button ID="Button1" runat="server" Enabled="False" OnClick="Button1_Click" Text="Add FPS"
                                    ValidationGroup="2" Visible="False" /></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                Rate/Qtls.
                                <asp:TextBox ID="tx_rate_qt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" colspan="4" align="left">
                                Total Quantity 
                                <asp:TextBox ID="tx_tot_qty" runat="server" BackColor="Linen" MaxLength="8"
                                    Width="96px" OnTextChanged="tx_tot_qty_TextChanged"></asp:TextBox>Qtls. &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Amount
                                <asp:TextBox ID="tx_tot_amt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="102px"></asp:TextBox>
                                Rs.</td>
                        </tr>
                    </table>
                    <table style="width: 720px">
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="740px">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                                        Font-Size="Small" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <RowStyle ForeColor="#000066" />
                                        <Columns>
                                            <asp:CommandField SelectText="Delete" ShowSelectButton="True">
                                                <ItemStyle HorizontalAlign="Left" Width="30px" />
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
                <fieldset >
                <legend>
                Payment Details
                </legend>
                    <table style="width: 746px; background-color: lightsteelblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                Payment Mode</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:DropDownList ID="ddl_pmode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged">
                                    <asp:ListItem Value="D">DD / Cheque</asp:ListItem>
                                    <asp:ListItem Value="R">Free Scheme / Credit </asp:ListItem>
                                    <asp:ListItem Value="A">Cash</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" colspan="2" align="left">
                                DD/Chq. No.
                                <asp:TextBox ID="tx_dd_no" runat="server" Width="136px" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"
                                    Text="*"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                DD/Chq.
                                Date</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_dd_date" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_dd_date'
	    });
	     </script>
                                </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                Bank Name</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left" colspan="3">
                                <strong>
                                    <asp:DropDownList ID="ddl_bank" runat="server">
                                    </asp:DropDownList></strong></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">Amount</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:TextBox ID="tx_dd_amount" runat="server" MaxLength="12" Width="112px"></asp:TextBox>
                                <asp:Label ID="lbl_amt" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                <asp:Label ID="Label2" runat="server" ForeColor="Black" Style="z-index: 101; left: 0px"
                                    Text="Rs."></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Button ID="save"  runat="server" Text="Save" Font-Bold="True" Font-Size="Medium" Width="88px" OnClick="save_Click" ValidationGroup="1" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                            <asp:Button ID="Close" runat="server" Text="Close" Font-Bold="True" Font-Size="Medium" Width="68px" OnClick="Close_Click"/></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click"
                                    Text="New" Width="75px" />
                                <asp:HyperLink ID="hlinkpdo" runat="server" NavigateUrl="#" Width="133px" Visible="False">Issue Delivery Order</asp:HyperLink></td>
                        </tr>
                    </table>                   
                </fieldset> &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_tot_qty"
                    ErrorMessage="Please enter Quantity" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>
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

