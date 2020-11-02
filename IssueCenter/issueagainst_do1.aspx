<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master"   AutoEventWireup="true" CodeFile="issueagainst_do1.aspx.cs" Inherits="issueagainst_do1" Title="Issue Against DO" %>
 
 
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
    

<div>
<asp:Panel ID="panel_do" runat ="server" >
        <table>
        <tr><td align="center">
            <strong>Issue Against Delivery Order</strong></td></tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
                <fieldset >
                <legend>Order Details
                </legend>
                    <table style="width: 704px; background-color: lightblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="7" style="font-size: 10pt;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="#0000C0" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100px; font-size: 10pt; height: 21px;">
                                Delivery Order No</td>
                            <td style="width: 130px; font-size: 10pt; height: 21px;">
                                <asp:DropDownList ID="ddl_do_no" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged" Width="128px">
                                </asp:DropDownList></td>
                            <td style="width: 68px; font-size: 10pt; height: 21px;" align="left">
                                Do Date</td>
                            <td style="width: 100px; font-size: 10pt; height: 21px;" align="left">
                                <asp:TextBox ID="tx_do_date" runat="server" Width="90px" ReadOnly="True" BackColor="Linen"></asp:TextBox></td>
                            <td style="width: 93px; font-size: 10pt; height: 21px;" align="left">
                                Do Validity Date</td>
                            <td style="width: 100px; font-size: 10pt; height: 21px;">
                                <asp:TextBox ID="tx_do_validity" runat="server" Width="90px" ReadOnly="True" BackColor="Linen"></asp:TextBox></td>
                            <td style="font-size: 10pt; width: 20px; height: 21px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 100px; font-size: 10pt; height: 21px;" align="left">
                                Issued To</td>
                            <td style="width: 130px; font-size: 10pt; height: 21px;">
                                <asp:TextBox ID="tx_issueto" runat="server" Width="120px" ReadOnly="True" BackColor="Linen" ></asp:TextBox></td>
                            <td style="width: 68px; font-size: 10pt; height: 21px;" align="left">
                                Do Quantity</td>
                            <td style="width: 100px; font-size: 10pt; height: 21px;" align="left">
                                <asp:TextBox ID="tx_do_qty" runat="server" Width="92px" ReadOnly="True" BackColor="Linen"></asp:TextBox></td>
                            <td style="width: 93px; font-size: 10pt; height: 21px;" align="left">
                                Balance
                                Quantity</td>
                            <td style="font-size: 10pt;" align="left">
                                <asp:TextBox ID="tx_balance_qty" runat="server" Width="88px" ReadOnly="True" BackColor="Linen"></asp:TextBox></td>
                            <td style="font-size: 8pt; width: 20px; height: 21px">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 100px; height: 21px" align="left">
                                Name</td>
                            <td align="left" colspan="3" style="font-size: 10pt; height: 21px">
                                <asp:DropDownList ID="ddl_lead" runat="server" Enabled="False" Font-Names="Kruti Dev 010"
                                    Font-Size="Medium">
                                </asp:DropDownList><asp:TextBox ID="tx_lead" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                            <td align="left" colspan="1" style="font-size: 10pt; height: 21px">
                                Allotment Month</td>
                            <td colspan="6" style="font-size: 10pt; height: 21px" align="left">
                                <asp:DropDownList ID="ddl_allot_month" runat="server" OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged"
                                    Width="105px" Enabled="False">
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
                            <td align="left" colspan="1" style="font-size: 10pt">
                                Commodity</td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                <asp:DropDownList ID="ddl_commodity" runat="server" Enabled="False"
                                     ValidationGroup="1"
                                    Width="191px">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                Scheme</td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                <asp:DropDownList ID="ddl_scheme" runat="server" Enabled="False"
                                     Width="109px">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                Allotment Year</td>
                            <td align="left" colspan="6" style="font-size: 10pt">
                                <asp:DropDownList ID="ddd_allot_year" runat="server" Enabled="False">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                <asp:Label ID="lblSorcePfArrival" runat="server" Text="Stock Issued From"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                <asp:DropDownList ID="ddlsarrival" runat="server" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged"
                                    Width="159px">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                Godown
                            </td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                            </td>
                            <td align="left" colspan="6" style="font-size: 10pt">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="font-size: 10pt">
                                Current Balance of Commodity at IssueCentre</td>
                            <td align="left" colspan="2" style="font-size: 10pt">
                                <asp:TextBox ID="tx_cur_bal" runat="server" BackColor="Linen" ReadOnly="True" Width="80px"></asp:TextBox>Qtls.</td>
                            <td align="left" colspan="1" style="font-size: 10pt">
                                No of&nbsp; Bags</td>
                            <td align="left" colspan="6" style="font-size: 10pt">
                                <asp:TextBox ID="tx_cur_bags" runat="server" BackColor="Linen" ReadOnly="True" Width="80px"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table style="width: 704px">
                        <tr>
                            <td style="width: 100px">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="696px">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                                        Font-Size="Small" OnRowDataBound="GridView1_RowDataBound" Width="695px" Caption="Quantity in Qtls.">
                                        <RowStyle ForeColor="#000066" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                            <AlternatingItemTemplate>
                                                <asp:CheckBox  ID="chkSelectAll"  runat="server" AutoPostBack ="true" />
                                            </AlternatingItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server"  AutoPostBack ="true" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="10px" />
                                        </asp:TemplateField>
                                        
                                            <asp:BoundField DataField="fps_code" HeaderText="FPS Code" SortExpression="fps_code" >
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fps_name" HeaderText="FPS Name" SortExpression="fps_name">
                                                <ItemStyle Font-Bold="False" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" Width="400px" Wrap="False" />
                                                <ControlStyle Width="400px" />
                                                <HeaderStyle Width="400px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                                                <ItemStyle Font-Bold="False" Width="200px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" SortExpression="Scheme_Name">
                                                <ItemStyle Font-Bold="False" Width="150px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="quantity" HeaderText="DO Quantity" SortExpression="quantity">
                                                <ItemStyle Font-Bold="False" HorizontalAlign="Right" Width="90px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="liftqty" HeaderText="Lifted Quantity" SortExpression="liftqty">
                                                <ItemStyle Font-Bold="False" HorizontalAlign="Right" Width="90px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="balqty" HeaderText="Balance QTY" SortExpression="balqty">
                                                <ItemStyle Font-Bold="False" HorizontalAlign="Right" Width="90px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Quantity To Issue">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox1" MaxLength ="13" Width="65px" runat="server" Text=""></asp:TextBox>
                                                
                                            </ItemTemplate>
                                            <ItemStyle Width="90px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />                                
                                        </asp:TemplateField>
                                            <asp:BoundField DataField="rate_per_qtls" HeaderText="Rate/Qtls." SortExpression="rate_per_qtls">
                                                <ItemStyle Font-Bold="False" HorizontalAlign="Right" Width="50px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="amt" HeaderText="Amount" SortExpression="amt">
                                                <ItemStyle Font-Bold="False" HorizontalAlign="Right" Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="commodity" HeaderText="c" SortExpression="commodity" />
                                            <asp:BoundField DataField="scheme_id" HeaderText="s" SortExpression="scheme_id" />
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
                    <table style="background-color: lightsteelblue; width: 701px;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="font-size: 10pt; width: 100px" align="left">
                                Issued Quantity</td>
                            <td style="font-size: 10pt; color: red">
                                <asp:TextBox ID="tx_issued_qty" runat="server" ReadOnly="True" Width="90px" BackColor="Linen"></asp:TextBox></td>
                            <td style="font-size: 8pt; width: 26px" align="left">
                                Qtls.</td>
                            <td style="font-size: 10pt; width: 93px" align="left">
                                Balance Qty.</td>
                            <td style="font-size: 10pt; width: 94px; color: black">
                                <asp:TextBox ID="tx_issue_balqty" runat="server" ReadOnly="True" Width="90px" BackColor="Linen"></asp:TextBox></td>
                            <td style="font-size: 8pt; width: 66px" align="left">
                                Qtls.</td>
                            <td style="font-size: 10pt; width: 129px; color: red">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 100px" align="left">
                                Quantity To Issue</td>
                            <td style="font-size: 10pt; color: red;">
                                <asp:TextBox ID="tx_qty_to_issue" runat="server" Width="88px" MaxLength="13" ReadOnly="True"></asp:TextBox>*&nbsp;</td>
                            <td style="font-size: 8pt; width: 26px" align="left">
                                Qtls.</td>
                            <td style="font-size: 10pt; width: 93px" align="left">
                                No of
                                Bags</td>
                            <td style="font-size: 10pt; width: 94px; color: red;" align="left">
                                <asp:TextBox ID="tx_bags" runat="server" Width="64px" MaxLength="4"></asp:TextBox>*</td>
                            <td style="font-size: 10pt; width: 66px" align="left">
                                Issued Date</td>
                            <td style="font-size: 10pt; width: 129px; color: red;">
                                
                                 <asp:TextBox ID="tx_issued_date" runat="server"></asp:TextBox>
                      <script type  ="text/javascript">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_issued_date'
	    });
	     </script>
                                </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 100px; height: 26px;" align="left">
                                Truck No.</td>
                            <td style="font-size: 10pt;" colspan="5">
                                <asp:TextBox ID="tx_gatepass" runat="server" Width="380px" MaxLength="70"></asp:TextBox></td>
                            <td style="font-size: 10pt; width: 129px; height: 26px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 26px">
                                &nbsp;</td>
                            <td style="width: 93px">
                                &nbsp;</td>
                            <td style="width: 94px">
                                <asp:Button ID="save" runat="server" Font-Bold="False" Font-Size="Medium"
                                    OnClick="save_Click" Text="Save" ValidationGroup="1" Width="88px" /></td>
                            <td style="width: 66px">
                                <asp:Button ID="btnClose" runat="server" Font-Size="Medium" OnClick="btnClose_Click"
                                    Text="Close" /></td>
                            <td style="width: 129px">
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click"
                                    Text="New" Width="75px" /></td>
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

