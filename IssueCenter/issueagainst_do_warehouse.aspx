<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master"   AutoEventWireup="true" CodeFile="issueagainst_do_warehouse.aspx.cs" Inherits="issueagainst_do_warehouse" Title="Issue Against DO" %>
 
 
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
        <table style="text-align: left; background-color: #e3dba4;">
        <tr><td align="center">
            <strong>Issue Against Delivery Order (Warehouse)</strong></td></tr>
            <tr>
                <td>
                <fieldset >
                <legend>Order Details
                </legend>
                    <table style="width: 704px">
                        <tr>
                            <td colspan="7" style="font-size: 10pt;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="#0000C0" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" style="font-size: 10pt">
                                <span id="lblDepositorType">Depositor Type &nbsp; &nbsp; &nbsp;&nbsp;<asp:TextBox
                                    ID="TextBox1" runat="server" BackColor="Linen" ReadOnly="True" Width="90px">Institution</asp:TextBox>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Depositor Name
                                    <asp:TextBox ID="TextBox2" runat="server" BackColor="Linen" ReadOnly="True" Width="90px">MPSCSC</asp:TextBox></span></td>
                            <td style="font-size: 8pt; width: 7px; height: 13px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" style="font-size: 10pt">
                                Allotment Month &nbsp; &nbsp;<asp:DropDownList ID="ddl_allot_month" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged"
                                    Width="105px">
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
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; Allotment Year
                                <asp:DropDownList ID="ddd_allot_year" runat="server" OnSelectedIndexChanged="ddd_allot_year_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td style="font-size: 8pt; width: 7px; height: 13px">
                            </td>
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
                                </td>
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
                            <td colspan="4" style="font-size: 10pt; height: 2px" align="left">
                                <asp:TextBox ID="tx_issueto_name" runat="server" BackColor="Linen" ReadOnly="True" Width="358px" Font-Bold="True" Font-Names="Kruti Dev 010" Font-Size="Medium"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Commodity</td>
                            <td style="vertical-align: middle;" align="left">
                                <asp:ListBox ID="lst_comm" runat="server" Height="30px"></asp:ListBox></td>
                            <td style="font-size: 8pt; width: 20px; height: 21px">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 704px">
                        <tr>
                            <td style="width: 100px">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="696px" Visible="False">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                                        Font-Size="Small">
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
                                            <asp:BoundField DataField="quantity" HeaderText="Quantity" SortExpression="quantity">
                                                <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="rate_per_qtls" HeaderText="Rate/Qtls." SortExpression="rate_per_qtls">
                                                <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="amt" HeaderText="Amount" SortExpression="amt">
                                                <ItemStyle Font-Bold="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="commodity" HeaderText="Commodity Id" SortExpression="commodity" />
                                            <asp:BoundField DataField="scheme_id" HeaderText="Scheme Id" SortExpression="scheme_id" />
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
            <td style="height: 289px">
                <fieldset >
                <legend>Issued / WHR Details
                </legend>
                    <table>
                        <tr>
                            <td style="font-size: 10pt;" align="left">
                                Issued Quantity</td>
                            <td style="font-size: 10pt; color: red">
                                <asp:TextBox ID="tx_issued_qty" runat="server" ReadOnly="True" Width="90px" BackColor="Linen"></asp:TextBox></td>
                            <td style="font-size: 8pt;" align="left">
                                Qtls.</td>
                            <td style="font-size: 10pt;" align="right" colspan="2">
                                Balance Qty.</td>
                            <td style="font-size: 10pt;" align="left" colspan="2">
                                <asp:TextBox ID="tx_issue_balqty" runat="server" ReadOnly="True" Width="90px" BackColor="Linen"></asp:TextBox>
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 13pt; background-color: #907464; height: 16px;">
                            </td>
                            <td style="font-size: 13pt; background-color: #907464; height: 16px;">
                            </td>
                            <td align="left" style="font-size: 13pt; background-color: #907464; height: 16px;">
                            </td>
                            <td align="center" colspan="2" style="font-size: 13pt; background-color: #907464; height: 16px;">
                                WHR DETAILS</td>
                            <td align="left" style="font-size: 13pt;  background-color: #907464; height: 16px;">
                            </td>
                            <td style="font-size: 13pt; background-color: #907464; height: 16px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; height: 21px; ">
                                Godown No.</td>
                            <td align="left" style="font-size: 10pt;">
                                <asp:DropDownList ID="ddl_godown" runat="server" Width="84px">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt;">
                            </td>
                            <td align="left" colspan="2" style="font-size: 10pt; height: 21px">
                                <asp:Button ID="showwhr" runat="server" Text="Show WHR" OnClick="showwhr_Click" Enabled="False" /></td>
                            <td align="left" style="font-size: 10pt; ">
                            </td>
                            <td style="font-size: 10pt; height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3" style="font-size: 12pt; vertical-align: top; color: maroon">
                                <asp:Label ID="lbl_stock" runat="server" Text="Available Stock" Visible="False"></asp:Label></td>
                            <td align="center" colspan="4" style="font-size: 12pt; vertical-align: top; color: maroon">
                                <asp:Label ID="lbl_issuestock" runat="server" Text="Stock for Issue" Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3">
                                <asp:Panel ID="Panel2" runat="server" Width="275px" ScrollBars="Auto" Visible="False">
                                    <span>
                                       <asp:GridView ID="grid_whr" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#8080FF" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Font-Bold="False"
                                        Font-Size="Small">
                                        <RowStyle ForeColor="#000066" />
                                        <Columns>
                                            <asp:TemplateField>
                      
                        <HeaderTemplate>
                          <asp:Label ID="lbl1" runat ="server" Text ="Select"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" runat="server" id="chkBoxId"
                              name="chkBoxId" />
                        </ItemTemplate>
                        </asp:TemplateField>
                                            <asp:BoundField DataField="WHRId" HeaderText="WHR No." SortExpression="WHRId" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Stack_Name" HeaderText="Stack Name" SortExpression="Stack_Name">
                                                <ItemStyle Font-Bold="False" Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Lot No." DataField="Lot_No" SortExpression="Lot_No">
                                                <ItemStyle Font-Bold="False" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Bags" HeaderText="Bags" SortExpression="Bags">
                                                <ItemStyle Font-Bold="False" />
                                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Weight" HeaderText="Weight" SortExpression="Weight">
                                                <ItemStyle Font-Bold="False" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="False" Font-Size="Small" ForeColor="White" />
                                    </asp:GridView>
                                    </span>
                                </asp:Panel>
                                <asp:Button ID="whrdata" runat="server" OnClick="whrdata_Click" Text="Fetch Details" Visible="False" Width="83px" /></td>
                            <td align="left" colspan="4" style="vertical-align: top"><asp:Panel ID="Panel3" runat="server" Width="400px" ScrollBars="Auto" Visible="False">
                                <span><asp:GridView ID="grid_stock_issue" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#8080FF" BorderStyle="Solid" BorderWidth="1px" CellPadding="0" Font-Size="Medium" OnRowDataBound="grid_stock_issue_RowDataBound">
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
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WHR No." SortExpression="whr">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("whr") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("whr") %>'></asp:Label>
                                            </ItemTemplate>
                                           
                                            <ItemStyle Width="80px" Font-Size="Small" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stack Name" SortExpression="stack">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("stack") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("stack") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" Font-Size="Small" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            
                                          
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bags" SortExpression="bags">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox1"   MaxLength ="4" Width="45px" runat="server" Text='<%# Bind("bags") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />                          
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Weight" SortExpression="weight">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox2" MaxLength ="13" Width="90px" runat="server" Text='<%# Bind("weight") %>'></asp:TextBox>
                                                
                                            </ItemTemplate>
                                            <ItemStyle Width="90px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />                                
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="abags" HeaderText="Available Bags" SortExpression="abags" />
                                        <asp:BoundField DataField="aweight" HeaderText="Available Weight" SortExpression="aweight" />
                                        <asp:TemplateField HeaderText="Stack Killed" >
                                            <ItemTemplate>                                              
                                             <asp:DropDownList ID="ddl_stack" runat="server">
                                             <asp:ListItem Value="N">No</asp:ListItem>
                                             <asp:ListItem Value="Y">Yes</asp:ListItem>
                                              </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Width="20px"/>
                                            <HeaderStyle HorizontalAlign="Center" />                                     
                                            
                                        
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                                </asp:GridView>
                                </span>
                            </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt;" align="left">
                                Quantity To Issue</td>
                            <td align="left" colspan="2" style="font-size: 8pt">
                                <asp:TextBox ID="tx_qty_to_issue" runat="server" Width="88px" MaxLength="13" BackColor="Linen" ReadOnly="True"></asp:TextBox>Qtls.</td>
                            <td style="font-size: 10pt; " align="left">
                                No of
                                Bags</td>
                            <td style="font-size: 10pt; color: red;" align="left">
                                <asp:TextBox ID="tx_bags" runat="server" Width="64px" MaxLength="4" BackColor="Linen" ReadOnly="True"></asp:TextBox>*</td>
                            <td style="font-size: 10pt " align="left">
                                Issued Date</td>
                            <td align="left">
                               
                                
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
                            <td align="left" style="font-size: 13pt; background-color: #907464">
                            </td>
                            <td align="left" colspan="2" style="font-size: 13pt; background-color: #907464">
                            </td>
                            <td align="center" colspan="2" style="font-size: 13pt; background-color: #907464">
                                TRUCK DETAILS</td>
                            <td align="left" style="font-size: 13pt;  background-color: #907464">
                            </td>
                            <td align="left" style="font-size: 13pt;  background-color: #907464">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt;">
                                Name of Transporter</td>
                            <td align="left" style="font-size: 10pt;" colspan="2">
                                <asp:DropDownList ID="ddl_transport" runat="server" Width="164px">
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt;  height: 26px" align="left">
                                Vehicle Type</td>
                            <td style="font-size: 10pt; height: 26px" align="left"><asp:DropDownList ID="ddl_vtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_vtype_SelectedIndexChanged">
                            </asp:DropDownList></td>
                            <td style="font-size: 10pt; " align="left">
                                <asp:Label ID="lbl_vno" runat="server" Text="Vehicle No." Visible="False"></asp:Label></td>
                            <td style="font-size: 10pt;  height: 26px" align="left">
                                <asp:TextBox ID="tx_gatepass" runat="server" MaxLength="15" Width="126px" Visible="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt;">
                                <asp:Label ID="lbl_licence" runat="server" Text="Licence No." Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt;" colspan="2">
                                <asp:TextBox ID="TextBox3" runat="server" MaxLength="15" Width="120px" Visible="False"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; height: 26px">
                                <asp:Label ID="lbl_valid" runat="server" Text="Valid Upto" Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; height: 26px">
                            </td>
                            <td align="left" style="font-size: 10pt; ">
                                <asp:Label ID="lbl_driver" runat="server" Text="Driver Name" Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt;  height: 26px">
                                <asp:TextBox ID="tx_driver" runat="server" MaxLength="30" Width="168px" Visible="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td >
                                </td>
                            <td>
                                <asp:Button ID="save" runat="server" Font-Bold="True" Font-Size="Medium" Height="32px"
                                    OnClick="save_Click" Text="Save" ValidationGroup="1" Width="88px" /></td>
                            <td style=" font-size: 10pt;">
                            </td>
                            <td >
                                <asp:Button ID="btnClose" runat="server" Font-Size="Medium" OnClick="btnClose_Click"
                                    Text="Close" Width="90px" /></td>
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

