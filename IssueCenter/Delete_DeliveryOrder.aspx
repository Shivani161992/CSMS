<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Delete_DeliveryOrder.aspx.cs" Inherits="IssueCenter_Delete_DeliveryOrder" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
  <div>
<asp:Panel ID="panel_do" runat ="server" >
        <table>
        <tr><td align="center" style="background-color: #cccccc">
            <asp:Label ID="Label27" runat="server" Font-Bold="True" Text="Delete Delivery Order (Issue to Lead Society)" Width="323px"></asp:Label></td></tr>
            <tr>
                <td style="height: 21px" >
                    <span style="font-size: 9pt; color: #ff0033">इस डिलीवरी आर्डर से सम्बंधित सारा डेटा
                        डिलीट हो जायेगा, डिलीट होने के बाद डेटा की रिकवरी नहीं हो सकेगी, कृपया सावधानी पूर्वक
                        डिलीट करें |</span></td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left; height: 198px;">
                <fieldset style="height: 162px" >
                <legend>Order Details
                </legend>
                    <table style="width: 736px; background-color: lightblue; height: 168px;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="7" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 21px;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="#0000C0" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;">
                                <asp:Label ID="lbldono" runat="server" Text="Delivery Order No" Width="99px" ForeColor="Red"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;">
                                <asp:DropDownList ID="ddl_do_no" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged" Width="120px">
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;" align="left">
                                <asp:Label ID="lbldodate" runat="server" Text="DO Date" Width="70px"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;" align="left">
                                <asp:TextBox ID="tx_do_date" runat="server" Width="90px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;" align="left">
                                <asp:Label ID="lbldovalidity" runat="server" Text="DO Validity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;">
                                <asp:TextBox ID="tx_do_validity" runat="server" Width="86px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;" align="left">
                                <asp:Label ID="lbltoissue" runat="server" Text="Issued To"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;">
                                <asp:TextBox ID="tx_issueto" runat="server" Width="120px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;" align="left">
                                <asp:Label ID="lblQuantity" runat="server" Text="DO Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;" align="left">
                                <asp:TextBox ID="tx_do_qty" runat="server" Width="92px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;" align="left">
                                <asp:Label ID="lbl_balqty" runat="server" Text="Bal Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;" align="left">
                                <asp:TextBox ID="tx_balance_qty" runat="server" Width="88px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 30px;">
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
                                </asp:DropDownList>
                                </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 32px;">
                                <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 32px;">
                                <asp:DropDownList ID="ddl_commodity" runat="server" Enabled="False"
                                     ValidationGroup="1"
                                    Width="191px">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 32px;">
                                <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 32px;">
                                <asp:DropDownList ID="ddl_scheme" runat="server" Enabled="False"
                                     Width="90px" >
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 32px;">
                                <asp:Label ID="lblallotyear" runat="server" Text="Allot.Year"></asp:Label></td>
                            <td align="left" colspan="6" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 32px;">
                                <asp:DropDownList ID="ddd_allot_year" runat="server" Enabled="False">
                                </asp:DropDownList></td>
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
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; width: 100px;">
                                <asp:TextBox ID="tx_issue_balqty" runat="server" ReadOnly="True" Width="90px" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;" align="left">
                                <asp:Label ID="Label25" runat="server" Text="Qtls."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 130px;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;<asp:Button ID="btndelete" runat="server"  Font-Bold="False" Font-Size="Medium"
                                    OnClick="btndelete_Click" Text="Delete" ValidationGroup="1" Width="88px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 100px;">
                                <asp:Button ID="btnclose" runat="server" Font-Size="Medium" OnClick="btnclose_Click"
                                    Text="Close" Width="95px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click"
                                    Text="New" Width="70px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                </td>
                        </tr>
                    </table>
                </fieldset> 
          
                &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                    Font-Size="Small" ShowMessageBox="True" ShowSummary="False" Style="z-index: 101"
                    ValidationGroup="1" />
                </td>
            </tr>
        </table>
        </asp:Panel> 
    </div>  
</asp:Content>

