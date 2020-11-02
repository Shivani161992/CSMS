<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="LinkCooperativesociety.aspx.cs" Inherits="District_LinkCooperativesociety" Title="Link Society to FPS" %>
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
    <table style="width: 771px; height: 400px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="2"><center>
                
                <strong>Link Cooperative Society with FPS</strong></center></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <span style="color: #cc6633"><strong>बैंक के नाम नहीं दिखने की स्थति में कृपया बैंक
                    मास्टर में बैंक का नाम जोड़ें |</strong></span></td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center">
                प्रदाय केंद्र</td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="194px" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center;">
                एफ. पी. एस. संचालित करने वाली समिति का नाम
            </td>
            <td style="width: 230px; height: 1px; text-align: left;">
                <asp:TextBox ID="txtSocName" runat="server" Width="221px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center;">
                समिति का पता
            </td>
            <td style="width: 230px; height: 1px; text-align: left;">
                <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine"
                    Width="221px" Height="26px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 341px; text-align: center; height: 1px;">
                मोबाइल नंबर
                </td>
            <td style="width: 230px; text-align: left; height: 1px;">
                <asp:TextBox ID="txtmobile" runat="server" Width="186px" MaxLength="11" ></asp:TextBox>
        
                </td>
        </tr>
        <tr>
            <td style="width: 341px; height: 26px; text-align: center">
                तहसील चुने
            </td>
            <td style="width: 230px; height: 26px; text-align: left">
                <asp:DropDownList ID="ddltehsil" runat="server" Width="230px" >
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 341px; text-align: center;">
                समिति के सम्बंधित बैंक का नाम
            </td>
            <td style="width: 230px; text-align: left;">
                <asp:DropDownList ID="ddlBank" runat="server" Width="230px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 341px; text-align: center">
                बैंक के ब्रांच का नाम
            </td>
            <td style="width: 230px; text-align: left">
                <asp:TextBox ID="txtbranch" runat="server" Width="230px" 
                   ></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 341px; text-align: center;">
                बैंक खाता क्रमांक</td>
            <td style="width: 230px; text-align: left;">
                <asp:TextBox ID="txtaccount" runat="server" Width="230px" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 341px; text-align: center">
                एफ. पी. एस. ब्लॉक चुने</td>
            <td style="width: 230px; text-align: left">
                <asp:DropDownList ID="ddlBlock" runat="server" Width="194px" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <span style="color: #990033">
                समिति जिन उचित मूल्य दूकान से सम्बंधित है उनका चयन कर डाटा सुरक्षित करे |</span></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                    Width="590px" >
                    
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

