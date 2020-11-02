<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="IssueCenterto_FPS.aspx.cs" Inherits="District_LinkCooperativesociety" Title="IssueCenter to FPS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
function CheckIsnondecimal(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 47) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>


   <center>
    <table style="width: 600px; height: 400px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="2" style="font-family: Georgia; font-size: large">
                <center style="background-color: #99CCFF">
                
                <strong>Distance from IssueCenter&nbsp;to FPS</strong></center></td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center">
                &nbsp;</td>
            <td style="width: 230px; height: 1px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center">
                प्रदाय केंद्र</td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="194px" 
                    OnSelectedIndexChanged="ddlissuecenter_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 341px; text-align: center">
                &nbsp;</td>
            <td style="width: 230px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <span style="color: #990033">
                प्रदाय केंद्र उचित मूल्य दूकान से सम्बंधित है उनका चयन कर डाटा सुरक्षित करे |</span></td>
        </tr>
        <tr>
        <center>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None"
                    Width="590px" EnableModelValidation="True" 
                  style="font-size: small" >
                    
                     <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectAll" runat="server" Width = "30px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fps_code" HeaderText="एफ. पी. एस. कोड" SortExpression="fps_code" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="100px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:BoundField DataField="fps_Uname" HeaderText="एफ. पी. एस. नाम" SortExpression="fps_Uname" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" Width="450px" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="प्रदाय केंद्र से एफ.पी.एस. की  दूरी ">
               <ItemTemplate>
              <ItemStyle Width = "200px" />
                    <HeaderStyle/>
                   <asp:TextBox ID="txtdistance" runat="server"  Text='0' ReadOnly="false"  BorderColor="Black" Width="100px"> </asp:TextBox>
                     
                   In Km
                     
                 </ItemTemplate>
                 
                <%-- <FooterTemplate>
                                   
                    <asp:Label ID="lbl_CurrentDarj" runat="server"  ForeColor="White" Font-Names="Vani" Text = "Total"></asp:Label>
                    
                    </FooterTemplate>--%>
                      <FooterStyle CssClass="FooterStyle" />
                 <HeaderStyle Font-Size="10px" />
                </asp:TemplateField>
                
                
                
                
                </Columns>
                    
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
            </center>
        </tr>
        <tr>
            <td style="width: 341px">
                <asp:Button ID="btnClose" runat="server" Text="New" Width="126px" 
                    OnClick="btnClose_Click" /></td>
            <td style="width: 214px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="129px" OnClick="btnSave_Click" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ListBox ID="ddl_fps_name" runat="server" Visible="False" Width="192px">
                </asp:ListBox>
                <asp:HiddenField ID="hd_fps" runat="server" />
            </td>
        </tr>
    </table>
    </center>
</asp:Content>

