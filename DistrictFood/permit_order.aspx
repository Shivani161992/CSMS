<%@ Page Language="C#" MasterPageFile="~/MasterPage/DF_MPSCSC.master"  AutoEventWireup="true" CodeFile="permit_order.aspx.cs" Inherits="permit_order" Title="permit_order" %>
 
 
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
	
    <div style="text-align: right">
    <asp:Panel ID="panelDO" runat ="server"  >
        <table>
        <tr><td align="center">
            <strong style="color: maroon; background-color: white">Permit Order</strong></td></tr>
            <tr>
                <td>
                <fieldset style="vertical-align: top; text-align: left" >
                <legend>
                Permit&nbsp; Order Details
                </legend>
                    <table style="width: 740px; background-color: skyblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="6" style="font-size: 10pt;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt;">
                                Allotment Month</td>
                            <td align="left" style="font-size: 10pt; width: 18px; color: red; position: static;
                                height: 8px">
                                <asp:DropDownList ID="ddl_allot_month" runat="server" Width="105px">
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
                                Allot.
                                Year</td>
                            <td align="left" style="font-size: 10pt; color: black;">
                                <asp:DropDownList ID="ddd_allot_year" runat="server">
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp;IssueCentre</td>
                            <td colspan="2" style="font-size: 10pt; height: 8px">
                                <asp:DropDownList ID="ddlissue" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged" >
                                    <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 85px; height: 8px">
                                Bank
                                Permit No.</td>
                            <td align="left" style="font-size: 10pt; position: static; height: 8px; width: 18px; color: red;">
                                <asp:TextBox ID="tx_permit_no" runat="server" Width="108px" MaxLength="15"></asp:TextBox>*</td>
                            <td align="left" style="font-size: 10pt; width: 64px; height: 8px">
                                Permit Date</td>
                            <td align="left" style="font-size: 10pt; width: 139px; color: black; height: 8px">
                              <asp:TextBox ID="tx_permit_date" runat="server" Width="108px"></asp:TextBox>
                                 <script type  ="text/javascript">
              new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_permit_date'
	    });
	     </script>
                            </td>
                            <td style="font-size: 10pt; height: 8px; width: 64px;">
                                &nbsp;Permit Validity</td>
                            <td align="left" style="font-size: 10pt; width: 113px; color: red; height: 8px">
                                &nbsp;<asp:TextBox ID="tx_permit_validity" runat="server" Width="76px" MaxLength="2"></asp:TextBox>*</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 85px; height: 8px">
                                Issue To</td>
                            <td align="left" style="font-size: 10pt; position: static; height: 8px; width: 18px;">
                                <asp:DropDownList ID="ddl_issueto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_issueto_SelectedIndexChanged">
                                <asp:ListItem Value="LF">Lead Society</asp:ListItem>
                                    <asp:ListItem Value="F">FPS</asp:ListItem>
                                <asp:ListItem Value="O">Others</asp:ListItem>
                            </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; width: 64px; height: 8px">
                                <asp:Label ID="lbl_lead" runat="server" Text="Name" Font-Size="Small" Width="40px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 139px; color: black; height: 8px">
                                <asp:DropDownList ID="ddl_lead" runat="server" Font-Names="Kruti Dev 010" Width="192px" Font-Size="Medium" AutoPostBack="True" OnSelectedIndexChanged="ddl_lead_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; height: 8px; width: 64px;" align="left">
                                Rate Type</td>
                            <td align="left" style="font-size: 10pt; width: 113px; color: red; height: 8px"><asp:DropDownList ID="ddl_rate_type" runat="server" >
                                <asp:ListItem Selected="True" Value="R">Rural</asp:ListItem>
                                <asp:ListItem Value="U">Urban</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 85px;">
                                Commodity</td>
                            <td align="left" colspan="2" style="font-size: 10pt; height: 8px">
                                <asp:DropDownList ID="ddl_commodity" runat="server" ValidationGroup="1" OnSelectedIndexChanged="ddl_commodity_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td align="left" colspan="3" style="font-size: 10pt; height: 8px">
                                Scheme &nbsp;<asp:DropDownList ID="ddl_scheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; height: 8px" colspan="3">
                                <asp:Label ID="Label4" runat="server" Text="Allocation for the Month"></asp:Label>
                                &nbsp;<asp:TextBox ID="tx_allot_qty" runat="server" MaxLength="8" Width="90px" BackColor="Linen" ReadOnly="True"></asp:TextBox>
                                <asp:Label ID="Label9" runat="server" Text="Qtls." Width="22px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; color: black; height: 8px" colspan="2">
                                &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="Label6" runat="server" Text="Already Issued Qty. "></asp:Label>
                                <asp:TextBox ID="tx_already_iqty" runat="server" BackColor="Linen" MaxLength="8"
                                    ReadOnly="True" Width="90px"></asp:TextBox>
                                <asp:Label ID="Label10" runat="server" Text="Qtls."></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 113px; color: red; height: 8px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; height: 8px" colspan="3">
                                <asp:Label ID="Label7" runat="server" Text="Current Balance Eligibility"></asp:Label>
                                <asp:TextBox ID="tx_balQty" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox>
                                <asp:Label ID="Label11" runat="server" Text="Qtls."></asp:Label></td>
                            <td align="left" style="font-size: 10pt; color: black; height: 8px" colspan="3">
                                <asp:Label ID="Label8" runat="server" Text="Balance Stock of Commodity at IssueCentre"></asp:Label>
                                <asp:TextBox ID="tx_bal_ic" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox>
                                <asp:Label ID="Label12" runat="server" Text="Qtls."></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; height: 5px; background-color: #cccccc; width: 85px;">
                            </td>
                            <td style="font-size: 10pt; height: 5px; background-color: #cccccc; width: 18px;">
                            </td>
                            <td align="center" colspan="2" style="font-size: 12pt; background-color: #cccccc; height: 5px;">
                                FPS&nbsp; Details</td>
                            <td style="font-size: 10pt; height: 5px; background-color: #cccccc; width: 64px;">
                            </td>
                            <td style="font-size: 10pt; height: 5px; background-color: #cccccc; width: 113px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 85px; height: 21px">
                                <asp:Label ID="Label5" runat="server" ForeColor="Black" Text="FPS Block"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 18px; height: 21px">
                                <asp:DropDownList ID="ddl_block" runat="server" Width="125px" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" AutoPostBack="True">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; height: 21px; width: 64px;">
                                FPS Name</td>
                            <td align="left" colspan="2" style="font-size: 10pt; height: 21px">
                                <asp:DropDownList ID="ddl_fps_name" runat="server" Font-Names="DVBW-TTYogeshEN" Width="252px" Font-Size="Medium" AutoPostBack="True" OnSelectedIndexChanged="ddl_fps_name_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 85px">
                                Allotment Qty</td>
                            <td align="left" style="font-size: 10pt; width: 18px">
                                <asp:TextBox ID="tx_allot_qty2" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox></td>
                            <td align="left" colspan="4" style="font-size: 10pt">
                                Already Issued Qty
                                <asp:TextBox ID="tx_already_iqty2" runat="server" BackColor="Linen" MaxLength="8"
                                    ReadOnly="True" Width="90px"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Balance Qty
                                <asp:TextBox ID="tx_balQty2" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox>
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 85px">
                                Quantity</td>
                            <td align="left" style="font-size: 10pt; width: 18px">
                                <asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="100px"></asp:TextBox><asp:Label
                                    ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="*" style="left: 0px"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; width: 64px;">
                                Qtls.</td>
                            <td align="left" colspan="2" style="font-size: 10pt">
                                Total Quantity &nbsp;<asp:TextBox ID="tx_tot_qty" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="100px"></asp:TextBox>&nbsp;</td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                <asp:Button ID="Button1" runat="server" Text="Add FPS" OnClick="Button1_Click" ValidationGroup="2" /></td>
                        </tr>
                    </table>
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="740px">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                            Font-Size="Small" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                                <asp:CommandField SelectText="Delete" ShowSelectButton="True">
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
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#006699" Font-Bold="False" Font-Size="Small" ForeColor="White" />
                        </asp:GridView>
                    </asp:Panel>
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
                            <td align="left" style="font-size: 10pt; width: 105px; color: black; height: 27px">
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
                            <td style="font-size: 10pt; height: 27px; color: black; width: 105px;" align="left"><asp:DropDownList ID="ddl_pmode" runat="server" Width="104px" AutoPostBack="True" OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged">
                                <asp:ListItem Value="D">DD</asp:ListItem>
                                <asp:ListItem Value="H">Cheque</asp:ListItem>
                                <asp:ListItem Value="R">Credit</asp:ListItem>
                                <asp:ListItem Value="A">Cash</asp:ListItem>
                            </asp:DropDownList></td>
                            <td style="font-size: 10pt; height: 27px; color: black; width: 213px;" colspan="2" align="left">
                                DD/Chq. No.&nbsp; &nbsp;<asp:TextBox ID="tx_dd_no" runat="server" Width="120px" MaxLength="50"></asp:TextBox><asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" Text="*" ForeColor="Red"></asp:Label></td>
                            <td style="font-size: 10pt; height: 27px; color: black; width: 79px;" align="left">
                                DD/Chq.
                                Date</td>
                            <td style="font-size: 10pt; height: 27px; color: black;" align="left">
                          
                                <asp:TextBox ID="tx_dd_date" runat="server" Width="109px"></asp:TextBox>
                                 <script type  ="text/javascript">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_dd_date'
	    });
	     </script>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tx_permit_validity"
                        ErrorMessage="Please enter permit order validity" Height="0px" ValidationGroup="1"
                        Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_qty"
                        ErrorMessage="Please enter quantity" Height="0px" ValidationGroup="2" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tx_permit_no" ErrorMessage="Please enter permit order no."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="tx_dd_no" ErrorMessage="Please enter DD No."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="tx_dd_amount"
                                    ErrorMessage="Please enter DD Amount" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_commodity"
                        ErrorMessage="Please enter commodity" Height="0px" ValidationGroup="2" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tx_rate_qt"
                        ErrorMessage="Please enter Rate" Height="0px" ValidationGroup="2"
                        Width="0px">*</asp:RequiredFieldValidator><asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="Small" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="2" />
        <asp:HyperLink id="hlinkpdo" runat="server" NavigateUrl="#" Visible="False">Issue Permit Order</asp:HyperLink></span></div>

</asp:Content>

