<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master"  AutoEventWireup="true" CodeFile="delivery1.aspx.cs" Inherits="delivery1" Title="Delivery Order" %>
 
 
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
        <table style="width: 704px">
        <tr><td align="center">
            <strong style="color: maroon; background-color: white">Delivery Order</strong></td></tr>
            <tr>
                <td>
                <fieldset >
                <legend>
                Permit / Release Order Details
                </legend>
                    <table style="width: 688px">
                        <tr>
                            <td colspan="7" style="font-size: 10pt" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="6" style="font-size: 10pt" align="left">
                                Allotment Month&nbsp;<asp:DropDownList ID="ddl_allot_month" runat="server" Width="105px">
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
                            </asp:DropDownList>
                                &nbsp;
                                Allotment
                                Year 
                                <asp:DropDownList ID="ddd_allot_year" runat="server">
                                </asp:DropDownList></td>
                            <td style="font-size: 8pt">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 57px; height: 8px;" align="left">
                                Permit No.</td>
                            <td style="font-size: 10pt; position: static; height: 8px;" align="left">
                                <asp:TextBox ID="tx_permit_no" runat="server" Width="125px" MaxLength="15"></asp:TextBox></td>
                            <td style="font-size: 10pt; width: 62px; height: 8px;" align="left">
                                Permit Date</td>
                            <td style="font-size: 10pt; width: 197px; color: black; height: 8px;" align="left">
                                <asp:TextBox ID="tx_permit_date" runat="server"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_permit_date'
	    });
	     </script>
                            </td>
                            <td style="font-size: 10pt; height: 8px;">
                                </td>
                            <td style="font-size: 10pt; width: 113px; color: red; height: 8px;" align="left">
                                <asp:TextBox ID="tx_permit_validity" runat="server" Width="95px" MaxLength="2" Visible="False"></asp:TextBox></td>
                            <td style="font-size: 8pt; height: 8px; width: 12px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 57px; font-size: 10pt;" align="left">
                                DO No.</td>
                            <td style="width: 140px; font-size: 10pt; color: red; position: static;" align="left">
                                <asp:TextBox ID="tx_do_no" runat="server" Width="125px" MaxLength="15"></asp:TextBox>*</td>
                            <td style="font-size: 10pt; width: 62px;" align="left">
                                DO Date</td>
                            <td style="width: 197px; font-size: 10pt; color: black;" align="left">
                                <asp:TextBox ID="tx_to_date" runat="server"></asp:TextBox>
                                
                                </td>
                            <td style="font-size: 10pt;" align="left">
                                DO Validity</td>
                            <td style="width: 113px; font-size: 10pt; color: red;" align="left">
                                <asp:TextBox ID="tx_do_validity" runat="server" Width="95px" MaxLength="2"></asp:TextBox>*</td>
                            <td style="font-size: 8pt; width: 12px;">
                                Days</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 57px; height: 6px" align="left">
                                Issue To</td>
                            <td style="font-size: 10pt; width: 140px; position: static; height: 6px;" align="left">
                                <asp:DropDownList ID="ddl_issueto" runat="server" Width="128px" AutoPostBack="True" OnSelectedIndexChanged="ddl_issueto_SelectedIndexChanged">
                                <asp:ListItem Value="L">Lead Society</asp:ListItem>
                                    <asp:ListItem Value="F">FPS</asp:ListItem>
                                <asp:ListItem Value="O">Others</asp:ListItem>
                            </asp:DropDownList></td>
                            <td style="font-size: 10pt; width: 62px; height: 6px" align="left">
                                <asp:Label ID="lbl_lead" runat="server" Text="Name" Font-Size="Small" Width="40px"></asp:Label></td>
                            <td align="left" colspan="4" style="font-size: 10pt; color: red; height: 6px">
                                <asp:DropDownList ID="ddl_lead" runat="server" Font-Names="Kruti Dev 010" Width="192px" Font-Size="Medium" AutoPostBack="True" OnSelectedIndexChanged="ddl_lead_SelectedIndexChanged">
                                </asp:DropDownList><asp:Label ID="lbl_ld_v" runat="server" Font-Bold="True" Font-Size="Medium" Text="*"></asp:Label>&nbsp;
                                &nbsp;<asp:Label ID="Label5" runat="server" ForeColor="Black" Text="FPS Block"></asp:Label>&nbsp;<asp:DropDownList ID="ddl_block" runat="server" Width="136px" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; height: 5px; background-color: #cccccc;">
                            </td>
                            <td style="font-size: 10pt; height: 5px; background-color: #cccccc;">
                            </td>
                            <td align="center" colspan="2" style="font-size: 12pt; background-color: #cccccc; height: 5px;">
                                DO Details FPS wise</td>
                            <td style="font-size: 10pt; height: 5px; background-color: #cccccc;">
                            </td>
                            <td style="font-size: 10pt; height: 5px; background-color: #cccccc; width: 113px;">
                            </td>
                            <td style="font-size: 10pt; height: 5px; background-color: #cccccc;">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; background-color: #ffcccc;" align="left">
                                FPS Name</td>
                            <td colspan="6" style="font-size: 10pt; background-color: #ffcccc;" align="left">
                                <asp:DropDownList ID="ddl_fps_name" runat="server" Font-Names="DVBW-TTYogeshEN" Width="199px" Font-Size="Medium" AutoPostBack="True" OnSelectedIndexChanged="ddl_fps_name_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; &nbsp;Commodity
                                <asp:DropDownList ID="ddl_commodity" runat="server" Width="195px" ValidationGroup="1">
                                </asp:DropDownList><asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"
                                    Text="*" style="z-index: 100; left: 0px"></asp:Label>
                                &nbsp; &nbsp;Scheme
                                <asp:DropDownList ID="ddl_scheme" runat="server" Width="80px" AutoPostBack="True" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" style="font-size: 10pt; background-color: #ffcccc;">
                                Allotment Qty
                                <asp:TextBox ID="tx_allot_qty" runat="server" MaxLength="8" Width="90px" BackColor="Linen" ReadOnly="True"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp;
                                &nbsp;&nbsp;
                                &nbsp;Already Issued Qty <asp:TextBox ID="tx_already_iqty" runat="server" BackColor="Linen" MaxLength="8"
                                    ReadOnly="True" Width="90px"></asp:TextBox>
                                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Balance Qty
                                <asp:TextBox ID="tx_balQty" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox></td>
                            <td style="font-size: 10pt; background-color: #ffcccc; height: 26px;" align="left">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" style="font-size: 10pt; background-color: #ffcccc">
                                Bal.
                                at IssueCentre
                                <asp:TextBox ID="tx_bal_ic" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox>
                                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;Rate/Quintal
                                <asp:TextBox ID="tx_rate_qt" runat="server"
                                    MaxLength="8" Width="90px" ReadOnly="True" BackColor="Linen"></asp:TextBox>
                                Rs. &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; Quantity
                                <asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="100px"></asp:TextBox>
                                <asp:Label
                                    ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="*"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; background-color: #ffcccc">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td align="left" colspan="7" style="font-size: 10pt; background-color: #ffcccc; height: 27px;">
                                Total Quantity
                                <asp:TextBox ID="tx_tot_qty" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="100px"></asp:TextBox>Qtls. &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                &nbsp;&nbsp; Total Amount&nbsp;<asp:TextBox ID="tx_tot_amt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="100px"></asp:TextBox>Rs. &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp;&nbsp;
                                <asp:Button ID="Button1" runat="server" Text="Add FPS" OnClick="Button1_Click" ValidationGroup="2" /></td>
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
                <legend>
                Payment Details
                </legend>
                    <table style="width: 688px">
                        <tr>
                            <td style="font-size: 10pt; color: black; height: 27px;" align="left">
                                Payment Mode</td>
                            <td style="font-size: 10pt; height: 27px; color: black;" align="left"><asp:DropDownList ID="ddl_pmode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged">
                                <asp:ListItem Value="D">DD / Cheque</asp:ListItem>
                                <asp:ListItem Value="R">Free Scheme / Credit </asp:ListItem>
                                <asp:ListItem Value="A">Cash</asp:ListItem>
                            </asp:DropDownList></td>
                            <td style="font-size: 10pt; height: 27px; color: black;" colspan="2" align="left">
                                DD/Chq. No.
                                <asp:TextBox ID="tx_dd_no" runat="server" Width="120px" MaxLength="50"></asp:TextBox><asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" Text="*" ForeColor="Red"></asp:Label></td>
                            <td style="font-size: 10pt; height: 27px; color: black;" align="left">
                                DD/Chq.
                                Date</td>
                            <td style="font-size: 10pt; height: 27px; color: black;" align="left">
                               
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
                            <td align="left" style="font-size: 10pt; color: black; height: 27px;">
                                Bank Name</td>
                            <td align="left" colspan="3" style="font-size: 10pt; color: black; height: 27px;">
                                <asp:DropDownList ID="ddl_bank" runat="server" >
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px;">
                                Amount</td>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px;">
                                <asp:TextBox ID="tx_dd_amount" runat="server" Width="112px" MaxLength="12"></asp:TextBox><asp:Label
                                    ID="lbl_amt" runat="server" Text="*" style="z-index: 100" ForeColor="Red"></asp:Label>
                                <asp:Label
                                    ID="Label2" runat="server" ForeColor="Black" Text="Rs."></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; color: black; height: 27px;">
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tx_do_validity"
                        ErrorMessage="Please enter delivery order validity" Height="0px" ValidationGroup="1"
                        Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_qty"
                        ErrorMessage="Please enter quantity" Height="0px" ValidationGroup="2" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tx_do_no" ErrorMessage="Please enter delivery order no."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="tx_dd_no" ErrorMessage="Please enter DD No."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="tx_dd_amount"
                                    ErrorMessage="Please enter DD Amount" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_commodity"
                        ErrorMessage="Please enter commodity" Height="0px" ValidationGroup="2" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tx_rate_qt"
                        ErrorMessage="Please enter Rate" Height="0px" ValidationGroup="2"
                        Width="0px">*</asp:RequiredFieldValidator><asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="Small" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="2" />
        <asp:HyperLink id="hlinkpdo" runat="server" NavigateUrl="#" Visible="False">Issue Delivery Order</asp:HyperLink></span></div>

</asp:Content>

