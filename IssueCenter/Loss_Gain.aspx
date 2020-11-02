<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Loss_Gain.aspx.cs" Inherits="IssueCenter_Loss_Gain" Title="Loss-Gain"  EnableEventValidation="true" %>
 
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
    <script type="text/javascript">
    function CheckIndate(e)
    {         
        var AsciiCode = e.keyCode;                               
        if ((AsciiCode >0))
        {            
            alert('Please Click on Calendar Control to Enter Date...');
            return false;
        }       
    }
    </script>

    &nbsp;<table style="border-right: teal 1px solid; border-top: teal 1px solid; border-left: teal 1px solid; border-bottom: teal 1px solid; background-color: #ffcccc" border="1" cellpadding="3" cellspacing="0">
        <tr>
            <td align="center" colspan="4" style="background-color: lightslategray">
    <asp:Label ID="lbldepositstock" runat="server" Text="Deposition Of Stock - Loss & Gain" Font-Bold="True" Font-Size="Large" ForeColor="White"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:Label ID="lblSorcePfArrival" runat="server" Text="Source of Arrival"></asp:Label></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:DropDownList ID="ddlsarrival" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                <asp:Label ID="lbl_stktype" runat="server" Text="Stock Type"></asp:Label></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:DropDownList ID="ddl_lossgain" runat="server" Width="71px">
                    <asp:ListItem Value="G">Gain</asp:ListItem>
                    <asp:ListItem Value="L">Loss</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                <asp:Label ID="lblcomdtytxt" runat="server" Text="Commodity"></asp:Label></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:DropDownList ID="ddlcomdty" runat="server">
                    <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                </asp:DropDownList></td>
            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                <asp:Label ID="lblschemetxt" runat="server" Text="Scheme"></asp:Label></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:DropDownList ID="ddlscheme" runat="server">
                    <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:Label ID="lblGodownNo" runat="server" Text="Godown No."></asp:Label></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                <asp:Label ID="lblMaxCap" runat="server" Text="Max. Capacity"></asp:Label></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:TextBox ID="txtmaxcap" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="#0000C0"
                    ReadOnly="True" Width="108px" BackColor="#FFFFC0"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:Label ID="lblCurStackCap" runat="server" Text="Current Capacity"></asp:Label></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:TextBox ID="txtcurntcap" runat="server" Font-Bold="True" Font-Italic="False"
                    ForeColor="#0000C0" ReadOnly="True" Width="108px" BackColor="#FFFFC0"></asp:TextBox></td>
            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                <asp:Label ID="lblAvailable" runat="server" Text="Available Capacity"></asp:Label></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:TextBox ID="txtavalcap" runat="server" Font-Bold="True" Font-Italic="False"
                    ForeColor="#0000C0" ReadOnly="True" Width="108px" BackColor="#FFFFC0"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                <asp:Label ID="lblTotalQuantityReceived" runat="server" ForeColor="Black" Text="Quantity"></asp:Label></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:TextBox ID="txtrqty" runat="server" MaxLength="13"
                    Width="79px"></asp:TextBox>*&nbsp;
                <asp:Label ID="Label1" runat="server" ForeColor="Black"
                        Text="Qtls." Font-Size="Small"></asp:Label></td>
            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
                <asp:Label ID="lbltotalReceivedBags" runat="server" ForeColor="Black" Text="Bags"></asp:Label></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:TextBox ID="txtrecbags" runat="server" MaxLength="5" Width="80px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                <asp:Label ID="lbl_date" runat="server" ForeColor="Black" Text="Date"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
            <asp:TextBox ID="tx_lgdate" runat="server" Width="100px"></asp:TextBox>*
                <script type  ="text/javascript">
	             new tcal ({
				            'formname': 'aspnetForm',
				            'controlname': 'ctl00_ContentPlaceHolder1_tx_lgdate'
	                      });
	          </script>

                </td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
            </td>
        </tr>
        <tr>
            <td style="font-size: 10pt; position: static; background-color: #cfdcc8">
                &nbsp;</td>
            <td align="right" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Save"
                    ValidationGroup="1" Width="108px" Font-Bold="True" Font-Size="Medium" Height="30px" /></td>
            <td align="right" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click1"
                        Text="Close" Width="68px" Height="30px" /></td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <asp:Button ID="btn_new" runat="server" OnClick="btn_new_Click"
                        Text="New" Width="68px" Height="30px" /></td>
        </tr>
        <tr>
            <td colspan="4" align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="600px">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ForeColor="#333333" GridLines="None">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:CommandField SelectText="Edit" ShowSelectButton="True">
                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Width="40px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="Source_Name" HeaderText="Source of Arrival" ReadOnly="True"
                                SortExpression="Source_Name">
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown" SortExpression="Godown_Name">
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" ReadOnly="True"
                                SortExpression="Commodity_Name">
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" ReadOnly="True" SortExpression="Scheme_Name">
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="stock_type" HeaderText="Loss(L) / Gain(G)" ReadOnly="True"
                                SortExpression="stock_type">
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity (Qtls.)" ReadOnly="True"
                                SortExpression="Quantity">
                                <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Bags" HeaderText="Bags" ReadOnly="True" SortExpression="Bags">
                                <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreatedDate" HeaderText="Date" SortExpression="CreatedDate">
                                <ItemStyle Font-Size="Small" HorizontalAlign="Right" Width="40px" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrqty"
        ErrorMessage="Please enter Quantity" ValidationGroup="1" ForeColor="White">*</asp:RequiredFieldValidator>
   
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tx_lgdate"
        ErrorMessage="Please select date from calendar" ValidationGroup="1" ForeColor="White">*</asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="1" />
</asp:Content>

