<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master"  ValidateRequest="false"   AutoEventWireup="true" CodeFile="edit_do_adm.aspx.cs" Inherits="edit_do_adm" Title="Delivery Order" %>
 
 
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
        <tr><td align="center" style="width: 807px">
            <strong>Edit Delivery Order</strong></td></tr>
            <tr>
                <td style="vertical-align: top; text-align: left; width: 807px;">
                <fieldset >
                <legend>Order Details
                </legend>
                    <table style="width: 744px; background-color: lightblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="7" style="font-size: 10pt;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="#0000C0" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="7" style="font-size: 10pt">
                                <asp:GridView ID="GridView3" runat="server">
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; font-size: 10pt; height: 21px;">
                                Delivery Order No</td>
                            <td style="width: 189px; font-size: 10pt; height: 21px;">
                                <asp:TextBox ID="tx_dono" runat="server"></asp:TextBox></td>
                            <td style="width: 72px; font-size: 10pt; height: 21px;" align="left">
                                <asp:TextBox ID="tx_issue" runat="server"></asp:TextBox></td>
                            <td style="font-size: 10pt;" align="left">
                                </td>
                            <td style="font-size: 10pt;" align="left">
                                </td>
                            <td style="font-size: 10pt;">
                                <asp:Button ID="Button1" runat="server" Text="Get Details" OnClick="Button1_Click" /></td>
                            <td style="font-size: 10pt; width: 20px; height: 21px">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 744px">
                        <tr>
                            <td style="width: 100px; height: 71px;">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="800px">
                                    <asp:GridView ID="GridView1" runat="server" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                                        Font-Size="Small">
                                        <RowStyle ForeColor="#000066" />
                                        <Columns>
                                            <asp:TemplateField><HeaderTemplate>
                                                <asp:Label ID="lbl1" runat="server" Text="Select"></asp:Label>
                                            </HeaderTemplate>
                                                <ItemTemplate>
                                                    <input type="checkbox" runat="server" id="chkBoxId"
                              name="chkBoxId" onclick="CheckChanged(this);" checked="checked"  />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                        </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="False" Font-Size="Small" ForeColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table><asp:GridView ID="GridView2" runat="server" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                                        Font-Size="Small">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lbl1" runat="server" Text="Select"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type="checkbox" runat="server" id="chkBoxId1"
                              name="chkBoxId1" onclick="CheckChanged(this);" checked="checked"  />
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="False" Font-Size="Small" ForeColor="White" />
                    </asp:GridView>
                </fieldset>
                
                </td>
            </tr>
            
            <tr>
            <td style="vertical-align: top; text-align: left; width: 807px;">
                <fieldset >
                <legend>Issued Details
                </legend>
                    <table style="background-color: lightsteelblue; width: 744px;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100px; height: 29px;">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 26px; height: 29px;">
                                &nbsp;</td>
                            <td style="width: 93px; height: 29px;">
                                &nbsp;</td>
                            <td style="width: 94px; height: 29px;">
                                <asp:Button ID="save" runat="server"  Font-Bold="False" Font-Size="Medium"
                                    OnClick="save_Click" Text="Save" ValidationGroup="1" Width="88px" /></td>
                            <td style="width: 66px; height: 29px;">
                                <asp:Button ID="btnClose" runat="server" Font-Size="Medium"
                                    Text="Close" OnClick="btnClose_Click" /></td>
                            <td style="width: 129px; height: 29px;">
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium"
                                    Text="New" Width="75px" OnClick="btn_new_Click" /></td>
                        </tr>
                    </table>
                </fieldset> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
        </table>
        </asp:Panel> 
    </div>

</asp:Content>

