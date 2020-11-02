<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master"  ValidateRequest="false"   AutoEventWireup="true" CodeFile="issueagainst_do.aspx.cs" Inherits="issueagainst_do" Title="Issue Against DO" %>
 
 
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
        <tr><td align="center" style="background-color: #cccccc">
            <asp:Label ID="Label27" runat="server" Font-Bold="True" Text="Issue Against Delivery Order"></asp:Label></td></tr>
            <tr>
                <td style="vertical-align: top; text-align: left;">
                <fieldset>
                <legend>Order Details
                </legend>
                    <table style="width: 736px; background-color: lightblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="7" style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="#0000C0" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbldono" runat="server" Text="Delivery Order No" Width="99px" ForeColor="Red"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
                                <asp:DropDownList ID="ddl_do_no" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged" Width="182px">
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; " align="left">
                                <asp:Label ID="lbldodate" runat="server" Text="DO Date" Width="70px"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; " align="left">
                                <asp:TextBox ID="tx_do_date" runat="server" Width="90px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; " align="left">
                                <asp:Label ID="lbldovalidity" runat="server" Text="DO Validity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;">
                                <asp:TextBox ID="tx_do_validity" runat="server" Width="86px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; " align="left">
                                <asp:Label ID="lbltoissue" runat="server" Text="Issued To"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
                                <asp:TextBox ID="tx_issueto" runat="server" Width="120px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; " align="left">
                                <asp:Label ID="lblQuantity" runat="server" Text="DO Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; " align="left">
                                <asp:TextBox ID="tx_do_qty" runat="server" Width="92px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; " align="left">
                                <asp:Label ID="lbl_balqty" runat="server" Text="Bal Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;" align="left">
                                <asp:TextBox ID="tx_balance_qty" runat="server" Width="88px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="lbl_lead" runat="server" Text="Name" Width="86px"></asp:Label></td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_lead" runat="server" Enabled="False" Font-Names="Arial Unicode MS"
                                    Font-Size="Small" Width="216px">
                                </asp:DropDownList><br />
                                <asp:TextBox ID="tx_lead" runat="server" ReadOnly="True" Visible="False" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblallotmonth" runat="server" Text="Allot. Month"></asp:Label></td>
                            <td colspan="6" style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
                                <asp:DropDownList ID="ddl_allot_month" runat="server" OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged" Enabled="False">
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
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
                                <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
                                <asp:DropDownList ID="ddl_commodity" runat="server" Enabled="False"
                                     ValidationGroup="1"
                                    Width="191px">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
                                <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
                                <asp:DropDownList ID="ddl_scheme" runat="server" Enabled="False"
                                     Width="90px" >
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblallotyear" runat="server" Text="Allot.Year"></asp:Label></td>
                            <td align="left" colspan="6" style="font-size: 10pt; position: static; background-color: #cfdcdc; ">
                                <asp:DropDownList ID="ddd_allot_year" runat="server" Enabled="False" >
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; ; background-color: #cfdcdc">
                                <asp:Label ID="lbltransporter" runat="server" Text="Transporter" Width="97px"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                                <asp:DropDownList ID="ddltransporter" runat="server" Width="188px">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; 
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static;
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; 
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" colspan="6" style="font-size: 10pt; position: static; 
                                background-color: #cfdcdc">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 700px">
                        <tr>
                            <td >
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="700px" Height = "1px">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                                        Font-Size="Small" Width="700px" Height = "1px" >
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
                                            <asp:BoundField DataField="fps_Uname" HeaderText="FPS Name" SortExpression="fps_Uname">
                                                <ItemStyle Font-Bold="False" Font-Names="Arial Unicode MS" Font-Size="Small" />
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
                                            <asp:BoundField DataField="commodity" HeaderText="Commodity Id" SortExpression="commodity" >
                                                <HeaderStyle Font-Size="0px" />
                                                <ItemStyle Font-Size="0px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="scheme_id" HeaderText="Scheme Id" SortExpression="scheme_id" >
                                                <HeaderStyle Font-Size="0px" />
                                                <ItemStyle Font-Size="0px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="block_code" HeaderText="Block Code" ReadOnly="True" SortExpression="block_code" >
                                                <HeaderStyle Font-Size="0px" />
                                                <ItemStyle Font-Size="0px" />
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
            <td style="vertical-align: top; text-align: left; height: 266px;">
                <fieldset >
                <legend>Issued Details
                </legend>
                    <table style="background-color: lightsteelblue; width: 736px;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;" align="left">
                                <asp:Label ID="lbl_issueqty" runat="server" Text="Issued Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 130px; height: 30px;">
                                <asp:TextBox ID="tx_issued_qty" runat="server" ReadOnly="True" Width="90px" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;" align="left">
                                <asp:Label ID="Label23" runat="server" Text="Qtls."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;" align="left">
                                <asp:Label ID="lblbalqty" runat="server" Text="Balance Qty."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:TextBox ID="tx_issue_balqty" runat="server" ReadOnly="True" Width="90px" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; width: 81px;" align="left">
                                <asp:Label ID="Label25" runat="server" Text="Qtls."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 31px;">
                                <asp:Label ID="lbldispsource" runat="server" Text="Stock Issued From"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 130px; height: 31px;">
                                <asp:DropDownList ID="ddlsarrival" runat="server" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 31px;">
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 31px;">
                                <asp:Label ID="lblGodownNo" runat="server" Text="Godown"></asp:Label></td>
                            <td colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 31px;">
                                <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:Label ID="lblbalcomdty" runat="server" Text="Current Balance of Commodity at IssueCentre"></asp:Label></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:TextBox ID="tx_cur_bal" runat="server" BackColor="#E0E0E0" ReadOnly="True" Width="80px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>Qtls.</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:Label ID="lblNoofBags" runat="server" Text="No of  Bags"></asp:Label></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:TextBox ID="tx_cur_bags" runat="server" BackColor="#E0E0E0" ReadOnly="True" Width="80px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;" align="left">
                                <asp:Label ID="lblDispatchQty" runat="server" Text="Quantity To Issue"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; color: red; width: 130px; height: 30px;">
                                <asp:TextBox ID="tx_qty_to_issue" runat="server" Width="88px" MaxLength="13" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>*&nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;" align="left">
                                Qtls.</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;" align="left">
                                <asp:Label ID="lblBagNumber" runat="server" Text="No of Bags"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; color: red; height: 30px;" align="left">
                                <asp:TextBox ID="tx_bags" runat="server" Width="64px" MaxLength="4" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>*</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; width: 81px;" align="left">
                                <asp:Label ID="lblissuedate" runat="server" Text="Issued Date"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                           <asp:TextBox ID="tx_issued_date" runat="server" Width="119px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_issued_date'
	    });
	     </script>
                                </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;" align="left">
                                <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;" colspan="5">
                                <asp:TextBox ID="tx_gatepass" runat="server" Width="326px" MaxLength="70" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; height: 30px; background-color: #cfdcc8">
                                <asp:Label ID="lblgatepass" runat="server" Font-Size="12px" Height="21px" Text="GatePass Number"
                                    Width="109px"></asp:Label></td>
                            <td colspan="5" style="font-size: 10pt; position: static; height: 30px; background-color: #cfdcc8">
                                <asp:TextBox ID="txtgatepass" runat="server" MaxLength="30" Width="137px">0</asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; height: 30px; background-color: #cfdcc8">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 130px;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;<asp:Button ID="btnsave" runat="server"  Font-Bold="False" Font-Size="Medium"
                                    OnClick="btnsave_Click" Text="Save" ValidationGroup="1" Width="88px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Button ID="btnclose" runat="server" Font-Size="Medium" OnClick="btnclose_Click"
                                    Text="Close" Width="95px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 81px;">
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click"
                                    Text="New" Width="70px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                </td>
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
                    ValidationGroup="1" Width="245px" />
                </td>
            </tr>
        </table>
        </asp:Panel> 
    </div>

</asp:Content>

