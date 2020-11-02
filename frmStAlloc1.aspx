<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmStAlloc1.aspx.vb" Inherits="frmStAlloc1" Debug ="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>State Allocation</title>
    
    <script type="text/javascript" src="js/chksql.js"></script> 
    <script type="text/javascript">
    function CheckIsNumeric(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;                        
        if ((AsciiCode < 46 && AsciiCode != 8 && AsciiCode != 9) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="z-index: 102; left: 0px; position: absolute; top: 0px">
            <tr>
                <td style="width: 100px; height: 87px;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/main_01.gif" Height="80px" Width="718px" /></td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <table style="width: 636px">
            <tr>
                <td style="width: 73px; height: 25px;">
                    Year &nbsp;&nbsp;&nbsp;
                </td>
                <td style="width: 133px; height: 25px">
                    <asp:DropDownList ID="dd_year" runat="server">
                    </asp:DropDownList></td>
                <td style="width: 200px; height: 25px">
                    Month&nbsp;
                    <asp:DropDownList ID="dd_month" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="01">January</asp:ListItem>
                        <asp:ListItem Value="02">February</asp:ListItem>
                        <asp:ListItem Value="03">March</asp:ListItem>
                        <asp:ListItem Value="04">April</asp:ListItem>
                        <asp:ListItem Value="05">May</asp:ListItem>
                        <asp:ListItem Value="06">June</asp:ListItem>
                        <asp:ListItem Value="07">July</asp:ListItem>
                        <asp:ListItem Value="08">August</asp:ListItem>
                        <asp:ListItem Value="09">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="width: 78px; height: 25px" align="right">
                    <asp:LinkButton ID="LinkButton3" runat="server" Width="40px">Print</asp:LinkButton></td>
                <td align="right" style="width: 78px; height: 25px">
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/State/State_Welcome.aspx" Width="40px">Back</asp:LinkButton></td>
                <td align="right" style="width: 78px; height: 25px">
        <asp:LinkButton ID="LinkButton2" runat="server">Logout</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 73px; height: 25px">
                    Commodity</td>
                <td style="width: 133px; height: 25px">
                    <asp:DropDownList ID="ddl_commodity" runat="server" ValidationGroup="1" AutoPostBack="True">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem Value="rice_apl">Rice APL</asp:ListItem>
                        <asp:ListItem Value="rice_bpl">Rice BPL</asp:ListItem>
                        <asp:ListItem Value="rice_aay">Rice AAY</asp:ListItem>
                        <asp:ListItem Value="wheat_apl">Wheat APL</asp:ListItem>
                        <asp:ListItem Value="wheat_bpl">Wheat BPL</asp:ListItem>
                        <asp:ListItem Value="wheat_aay">Wheat AAY</asp:ListItem>
                        <asp:ListItem Value="sugar">Sugar</asp:ListItem>
                        <asp:ListItem Value="kerosene">Kerosene</asp:ListItem>
                        <asp:ListItem Value="salt">Salt</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="height: 25px" colspan="4">
                    <asp:Label ID="msg" runat="server" ForeColor="Blue" Font-Size="Large"></asp:Label></td>
            </tr>
        </table>
        <table style="border-right: gray thin inset; border-top: gray thin inset; border-left: gray thin inset;
            border-bottom: gray thin inset; width: 640px; background-color: #99ccff;">
            <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="400px">
                                    <asp:GridView ID="gvDistrict" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            Font-Names="Arial Black" Font-Size="Small" PageSize="1" CellPadding="0" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" CellSpacing="1" ForeColor="Black">
                            <Columns>
                                <asp:BoundField DataField="division" HeaderText="Division" SortExpression="division">
                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="False" />
                                    <ItemStyle Font-Names="Arial Narrow" Font-Size="Medium" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="district_name" HeaderText="District " SortExpression="district_name">
                                    <HeaderStyle Font-Bold="False" Font-Size="Medium" HorizontalAlign="Center" />
                                    <ItemStyle Font-Names="Arial Narrow" Font-Size="Medium" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="district_code" HeaderText="District Code" SortExpression="district_code" >
                                    <HeaderStyle Font-Bold="False" Font-Names="Arial Narrow" />
                                    <ItemStyle Font-Bold="False" Font-Names="Arial Narrow" Font-Size="Small" />
                                </asp:BoundField>
                                
                               
                                        <asp:TemplateField HeaderText="Quantity" SortExpression="qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox1"   MaxLength ="13" Width="90px" runat="server" Text='<%# Bind("qty") %>'></asp:TextBox>
                                                
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="False" />
                                </asp:TemplateField>
                                <asp:TemplateField >
                                            <ItemTemplate>                                              
                                                <asp:Label ID="Label1" runat="server" Text="Label">Qtls.</asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Names="Arial" Font-Size="Small"/>
                                                                        
                                        </asp:TemplateField>
                            </Columns>
                                        <RowStyle BackColor="White" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#00C0C0" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="Medium" ForeColor="White" />
                        </asp:GridView>
                    </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;<asp:Button ID="save" runat="server" Height="40px" Text="Save" Width="125px" Font-Bold="True" Font-Size="Medium" /></td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
        <table style="font-family: Times New Roman">
            <tr>
                <td style="width: 100px">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/nic_logo_blue.jpg" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
