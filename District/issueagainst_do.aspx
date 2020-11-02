<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master"  ValidateRequest="false"   AutoEventWireup="true" CodeFile="issueagainst_do.aspx.cs" Inherits="issueagainst_do" Title="Issue Against DO" %>
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
    

<div>
<asp:Panel ID="panel_do" runat ="server" >
        <table>
        <tr><td align="center" style="height: 21px">
            <asp:Label ID="Label27" runat="server" Text="Issue Against Delivery Order" Font-Bold="False" Font-Italic="True" ForeColor="Navy" Width="201px"></asp:Label></td></tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
                <fieldset style="width: 646px" >
                <legend>Delivery Order Details
                </legend>
                    <table style="width: 619px; border-right: navy 3px double; padding-right: 1px; border-top: navy 3px double; padding-left: 1px; padding-bottom: 1px; border-left: navy 3px double; padding-top: 1px; border-bottom: navy 3px double; border-collapse: collapse;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="6" style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="#0000C0" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label2" runat="server" Text="Delivery Order No"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_do_no" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged" Width="130px">
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="Label3" runat="server" Text="DO Date" Width="74px"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:TextBox ID="tx_do_date" runat="server" Width="90px" ReadOnly="True" BackColor="Linen"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="lbldovalidity" runat="server" Text="DO Validity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_do_validity" runat="server" Width="86px" ReadOnly="True" BackColor="Linen"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="Label5" runat="server" Text="Issued To"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_issueto" runat="server" Width="122px" ReadOnly="True" BackColor="Linen" ></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="lblQuantity" runat="server" Text="DO Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:TextBox ID="tx_do_qty" runat="server" Width="92px" ReadOnly="True" BackColor="Linen"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="lblbalqty" runat="server" Text="Bal Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:TextBox ID="tx_balance_qty" runat="server" Width="88px" ReadOnly="True" BackColor="Linen"></asp:TextBox>
                                <asp:Label ID="Label26" runat="server" Text="Qtls"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label8" runat="server" Text="Name"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_lead" runat="server" Enabled="False" Font-Names="Kruti Dev 010"
                                    Font-Size="Medium" Visible="False" Width="113px">
                                </asp:DropDownList><br />
                                <asp:TextBox ID="tx_lead" runat="server" ReadOnly="True" Width="133px"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label9" runat="server" Text="Allot. Month"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_allot_month" runat="server" OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged" Enabled="False" Width="96px">
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
                                <asp:Label ID="Label12" runat="server" Text="Allot.Year"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddd_allot_year" runat="server" Enabled="False" Width="96px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblCommodityName" runat="server" Text="Commodity"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_commodity" runat="server" Enabled="False"
                                     ValidationGroup="1"
                                    Width="134px">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblSchemeName" runat="server" Text="Scheme"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_scheme" runat="server" Enabled="False"
                                     Width="101px">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label7" runat="server" Text="R.O. Number" Width="70px"></asp:Label></td>
                            <td align="left" colspan="5" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddlrono" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlrono_SelectedIndexChanged"
                                    Width="110px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label4" runat="server" Text="R.O. Qty." Width="65px"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="txtroqty" runat="server" BackColor="Linen" ReadOnly="True" Width="98px"></asp:TextBox></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label6" runat="server" Text="T.O. Issued Qty." Width="95px"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_balqty" runat="server" BackColor="Linen" ReadOnly="True" Width="98px"></asp:TextBox></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label10" runat="server" Text="T.O. Balance Qty." Width="101px"></asp:Label></td>
                            <td align="left" colspan="5" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="txttobalance" runat="server" BackColor="Linen" ReadOnly="True" Width="98px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label11" runat="server" Text="Lifted Qty." Width="73px"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="txtliftqty" runat="server" BackColor="Linen" ReadOnly="True" Width="98px"></asp:TextBox></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label13" runat="server" Text="R.O. Balance Qty." Width="102px"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="txtrobalanceqty" runat="server" BackColor="Linen" ReadOnly="True" Width="98px"></asp:TextBox></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                            </td>
                            <td align="left" colspan="5" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbldispsource" runat="server" Text="Stock Issued From" Visible="False"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddlsarrival" runat="server" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged" Visible="False">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblGodownNo" runat="server" Text="Godown" Visible="False" Width="85px"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged" Width="108px" Visible="False">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                            </td>
                            <td align="left" colspan="5" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="10" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblmsg" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Navy"
                                    Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="Label14" runat="server" Text="Current Balance of Commodity at IssueCentre" Visible="False"></asp:Label></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_cur_bal" runat="server" BackColor="Linen" ReadOnly="True" Width="80px" Visible="False"></asp:TextBox>
                                <asp:Label ID="Label15" runat="server" Text="Label" Visible="False"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblNoofBags" runat="server" Text="No of  Bags" Visible="False"></asp:Label></td>
                            <td align="left" colspan="5" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_cur_bags" runat="server" BackColor="Linen" ReadOnly="True" Width="80px" Visible="False"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table style="width: 648px">
                        <tr>
                            <td style="width: 100px">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="660px" Visible="False">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                                        Font-Size="Small" Width="663px">
                                        <RowStyle ForeColor="#000066" />
                                        <Columns>
                                            <asp:TemplateField>
                        <ItemStyle Width="5%"></ItemStyle>
                        <HeaderTemplate>
                          <asp:Label ID="lbl1" runat ="server" Text ="Select"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" runat="server" id="chkBoxId"
                              name="chkBoxId" onclick="CheckChanged(this);" checked="checked"  />
                        </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:BoundField DataField="fps_code" HeaderText="FPS Code" SortExpression="fps_code" />
                                            <asp:BoundField DataField="fps_name" HeaderText="FPS Name" SortExpression="fps_name">
                                                <ItemStyle Font-Bold="False" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                                                <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" SortExpression="Scheme_Name">
                                                <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="qty" HeaderText="Quantity" SortExpression="qty">
                                                <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="rateqtls" HeaderText="Rate/Qtls." SortExpression="rateqtls">
                                                <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="amt" HeaderText="Amount" SortExpression="amt">
                                                <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="commodity" HeaderText="Commodity Id" SortExpression="commodity" />
                                            <asp:BoundField DataField="scheme_id" HeaderText="Scheme Id" SortExpression="scheme_id" />
                                            <asp:BoundField DataField="block_code" HeaderText="Block Code" ReadOnly="True" SortExpression="block_code" />
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
            <td style="vertical-align: top; text-align: left">
                <fieldset >
                <legend>Issued Details
                </legend>
                    <table style="width: 657px; border-right: navy 3px double; padding-right: 1px; border-top: navy 3px double; padding-left: 1px; padding-bottom: 1px; border-left: navy 3px double; padding-top: 1px; border-bottom: navy 3px double; border-collapse: collapse;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label17" runat="server" Text="Issued Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:TextBox ID="tx_issued_qty" runat="server" ReadOnly="True" Width="90px" BackColor="Linen"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label23" runat="server" Text="Qtls."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label18" runat="server" Text="Balance Qty."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:TextBox ID="tx_issue_balqty" runat="server" ReadOnly="True" Width="90px" BackColor="Linen"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label25" runat="server" Text="Qtls."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label21" runat="server" Text="Quantity To Issue"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:TextBox ID="tx_qty_to_issue" runat="server" Width="90px" MaxLength="13"></asp:TextBox>*&nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label24" runat="server" Text="Qtls."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label19" runat="server" Text="No of Bags"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                <asp:TextBox ID="tx_bags" runat="server" Width="90px" MaxLength="4"></asp:TextBox>*</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label20" runat="server" Text="Issued Date"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:TextBox ID="tx_issued_date" runat="server" Width="123px"></asp:TextBox>
                                <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_issued_date'
	    });
	     </script>
                                
                                </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                                <asp:Label ID="Label22" runat="server" Text="Truck No."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" colspan="5">
                                <asp:TextBox ID="tx_gatepass" runat="server" Width="370px" MaxLength="70"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Button ID="save" runat="server" Font-Bold="False" Font-Size="Medium"
                                    OnClick="save_Click" Text="Save" ValidationGroup="1" Width="88px" Font-Italic="False" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Button ID="btnClose" runat="server" Font-Size="Medium" OnClick="btnClose_Click"
                                    Text="Close" Font-Bold="False" Font-Italic="False" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click"
                                    Text="New" Width="75px" Font-Italic="False" /></td>
                        </tr>
                    </table>
                </fieldset> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tx_qty_to_issue"
                    ErrorMessage="Please enter quantity to issue" Height="0px" ValidationGroup="1"
                    Width="1px">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_bags"
                    ErrorMessage="Please enter no of bags" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>
                &nbsp; &nbsp; &nbsp;&nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server"
                    Font-Size="Small" ShowMessageBox="True" ShowSummary="False" Style="z-index: 101"
                    ValidationGroup="1" />
                </td>
            </tr>
        </table>
        </asp:Panel> 
    </div>

</asp:Content>

