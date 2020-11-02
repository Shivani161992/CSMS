<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmStAlloc.aspx.vb" Inherits="_frmStAlloc" Debug ="true"  %>

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
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/main_01.gif" Height="80px" Width="750px" /><br />
        <table width="750">
            <tr>
                <td align="center" style="width: 100px">
        <table style="width: 640px" border="1" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px; height: 25px;">
                    Year &nbsp;&nbsp;
                    <asp:DropDownList ID="dd_year" runat="server">
                    </asp:DropDownList></td>
                <td style="width: 200px; height: 25px">
                    Month&nbsp;
                    <asp:DropDownList ID="dd_month" runat="server">
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
                <td colspan="2" style="height: 25px">
                    <asp:Label ID="msg" runat="server" ForeColor="Blue" Font-Size="Large" Width="344px"></asp:Label></td>
                <td style="width: 78px; height: 25px">
                    </td>
                <td style="width: 78px; height: 25px">
                </td>
                <td style="width: 78px; height: 25px">
                </td>
            </tr>
        </table>
        <table style="border-right: gray thin inset; border-top: gray thin inset; border-left: gray thin inset;
            border-bottom: gray thin inset; width: 640px;">
            <tr>
                <td style="width: 106px">
                    <table style="width: 632px; background-color: #99ccff;">
                        <tr>
                            <td style="width: 216px;" rowspan="4">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" Text="District"></asp:Label><strong>
                                </strong>
                                <asp:Panel ID="Panel1" runat="server" Height="328px" ScrollBars="Auto" Width="208px">
                                    &nbsp;<asp:GridView ID="gvDistrict" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            Font-Names="DVBW-TTYogeshEN" Font-Size="Small" PageSize="1" CellPadding="5" Height="296px" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" ShowHeader="False">
                            <Columns>
                                <asp:ButtonField CommandName="Select" Text="Select" >
                                    <ItemStyle Font-Size="Medium" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="district_name" HeaderText="District Name">
                                    <HeaderStyle Font-Bold="False" Font-Size="Medium" />
                                    <ItemStyle Font-Names="Arial Narrow" Font-Size="Medium" />
                                </asp:BoundField>
                                <asp:BoundField DataField="district_code" HeaderText="District Code" />
                            </Columns>
                                        <RowStyle BackColor="White" ForeColor="#003399" />
                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <HeaderStyle BackColor="#0000C0" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                            Font-Size="Medium" ForeColor="White" />
                        </asp:GridView>
                    </asp:Panel>
                            </td>
                            <td style="font-size: 12pt; width: 250px; height: 21px">
                    Allocation for&nbsp;<strong> </strong>
                                    <asp:Label ID="yrmth" runat="server"></asp:Label></td>
                        </tr>
                        <tr style="font-size: 12pt">
                            <td style="width: 250px; height: 21px;" >
                                <strong>District</strong> &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lbl_dist" runat="server" Font-Names="Arial" Font-Size="Medium" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-family: Times New Roman; font-size: 12pt;">
                            <td style="vertical-align: top; width: 250px">
                    <table style="width: 320px; background-color: #ccccff">
                        <tr>
                            <td style="width: 86px; height: 26px;">
                                Rice APL</td>
                            <td style="width: 100px; height: 26px;">
                                <asp:TextBox ID="tx_rice_apl" runat="server" Width="112px" MaxLength="15"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 131px; height: 26px">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td style="width: 86px">
                                Rice BPL</td>
                            <td style="width: 100px">
                                <asp:TextBox ID="tx_rice_bpl" runat="server" Width="112px" MaxLength="15"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 131px; height: 26px">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td style="width: 86px">
                                Rice AAY</td>
                            <td style="width: 100px">
                                <asp:TextBox ID="tx_rice_aay" runat="server" Width="112px" MaxLength="15"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 131px; height: 26px">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td style="width: 86px; height: 28px;">
                                Wheat APL</td>
                            <td style="width: 100px; height: 28px;">
                                <asp:TextBox ID="tx_wht_apl" runat="server" Width="112px" MaxLength="15"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 131px; height: 28px">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td style="width: 86px; height: 28px;">
                                Wheat BPL</td>
                            <td style="width: 100px; height: 28px;">
                                <asp:TextBox ID="tx_wht_bpl" runat="server" Width="112px" MaxLength="15"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 131px; height: 28px">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td style="width: 86px">
                                Wheat AAY</td>
                            <td style="width: 100px">
                                <asp:TextBox ID="tx_wht_aay" runat="server" Width="112px" MaxLength="15"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 131px; height: 26px">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td style="width: 86px; height: 20px">
                                Sugar</td>
                            <td style="width: 100px; height: 20px">
                                <asp:TextBox ID="tx_sugar" runat="server" Width="112px" MaxLength="15"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 131px; height: 26px">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td style="width: 86px; height: 26px;">
                                Kerosene</td>
                            <td style="width: 100px; height: 26px;">
                                <asp:TextBox ID="tx_kerosene" runat="server" Width="112px" MaxLength="15"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 131px; height: 26px">
                                KL.</td>
                        </tr>
                        <tr>
                            <td style="width: 86px; height: 25px">
                                Salt</td>
                            <td style="width: 100px; height: 25px">
                                <asp:TextBox ID="tx_salt" runat="server" Width="112px" MaxLength="15"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; width: 131px; height: 26px">
                                Qtls.</td>
                        </tr>
                    </table>
                            </td>
                        </tr>
                        <tr style="font-family: Times New Roman">
                            <td style="vertical-align: bottom; width: 250px; height: 42px">
                    <asp:Button ID="save" runat="server" Height="32px" Text="Save" Width="72px" /><asp:Button
                        ID="update" runat="server" Height="32px" Text="Save" Visible="False" Width="72px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
    
    </div>
        <table style="font-family: Times New Roman">
            <tr>
                <td style="width: 100px">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Nic-Logo_blue1.bmp" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
