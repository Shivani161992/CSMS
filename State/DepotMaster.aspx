<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="DepotMaster.aspx.cs" Inherits="State_DepotProfile" Title="Depot Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <table style="width :500px ;background-image: url(../images/images[26].jpg);">
                    <tr>
                        <td colspan="4" class="HeadingBlue" style="background-color: dimgray; height: 15px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height:20px;border-collapse: collapse; border:solid 1px white;">
                            <strong><span style="font-size: 8pt; color: #990033">
                                <asp:Label ID="lblDepotMaster" runat="server" Text="Issue Center Master"></asp:Label></span></strong></td>
                    </tr>
                    <tr>
                        <td style="width: 196px; height: 31px; text-align: left;border-collapse: collapse; border:solid 1px white;" >
                            <span style="font-size: 9pt; font-family: Verdana">
                                </span></td>
                        <td style="width: 144px; height: 31px;border-collapse: collapse; border:solid 1px white;" >
                            &nbsp;</td>
                        <td style="width: 48px; height: 31px;border-collapse: collapse; border:solid 1px white;" >
                            </td>
                        <td style="border-collapse: collapse; border:solid 1px white; height: 31px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 196px; height: 24px; text-align: left;border-collapse: collapse; border:solid 1px white;" >
                            <asp:Label ID="lblDistrict" runat="server" Text="District Name" Width="140px" Font-Size="8pt" Font-Names="Verdana" Font-Bold="True"></asp:Label></td>
                        <td style="width: 144px; height: 24px;border-collapse: collapse; border:solid 1px white;" >
                                <asp:DropDownList ID="ddlDistrictName" runat="server"  Width="203px" Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml" OnSelectedIndexChanged="ddlDistrictName_SelectedIndexChanged" AutoPostBack="True">
                                
                                </asp:DropDownList>
                            </td>
                        <td style="width: 48px; height: 24px;border-collapse: collapse; border:solid 1px white;" >
                                <asp:Label ID="lblDepotType" runat="server" Text="Issue Center Name" Width="131px" Font-Size="8pt" Font-Names="Verdana" Font-Bold="True"></asp:Label></td>
                        <td style="width: 412px; height: 24px;border-collapse: collapse; border:solid 1px white;">
                                <asp:DropDownList ID="ddlDepoType" runat="server" Width="203px" Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml" OnSelectedIndexChanged="ddlDepoType_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList></td>
                            <asp:DropDownList ID="ddlTehsilName" runat="server"  Width="203px" Visible="false" Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml" AutoPostBack="True">
                                </asp:DropDownList></tr>
                    <tr id="type1" runat="server" visible="false">
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" >
                            &nbsp;</td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;
                            text-align: left" >
                            &nbsp;</td></tr>
                    <tr>
                        <td colspan="4" style="height: 7px; border-collapse: collapse; border:solid 1px white; text-align: center;" >
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Label ID="lblDepotDetails" runat="server" Font-Bold="True" Text="Issue Center Details" ForeColor="Black" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                            &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" align="left" >
                                <asp:Label ID="lblAddress" runat="server" Text=" Address" Width="167px" Font-Names="Verdana" Font-Size="8pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;
                            text-align: left" >
                            <%--<asp:DropDownList ID="ddl_depo" runat="server" Width="202px"  AutoPostBack="false" Visible="false"  Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml" >
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="txtLocationAddress" runat="server" TextMode="MultiLine" Width="198px" Font-Names="Verdana" Font-Size="9pt" 
 TabIndex="2"
></asp:TextBox></td>
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" align="left" >
                                <asp:Label ID="lblFax" runat="server" Text="Fax No." Font-Names="Verdana" Font-Size="8pt" Font-Bold="True" Width="77px"></asp:Label></td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;" dir="ltr">
                                <asp:TextBox ID="txtLocationFaxNo" runat="server" Width="198px" Font-Names="Verdana" Font-Size="9pt" Onkeyup = "Spc_validator1(this)" MaxLength="12" TabIndex="4"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" align="left" >
                                <asp:Label ID="lnlPhNo" runat="server" Text="Phone No." Font-Names="Verdana" Font-Size="8pt" Font-Bold="True" Width="93px"></asp:Label></td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;
                            text-align: left">
                                <asp:TextBox ID="txtLocationPhoneNo" runat="server" Width="198px" Font-Names="Verdana" Font-Size="9pt" MaxLength="12" Onkeyup = "Spc_validator1(this)" TabIndex="3"></asp:TextBox>&nbsp;
                        </td>
                        <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white;">
                                <asp:Label ID="lblEmail" runat="server" Text="E-Mail Address" Width="170px" Font-Names="Verdana" Font-Size="8pt" Font-Bold="True" Visible="False"></asp:Label></td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;">
                                <asp:TextBox ID="txtLocationEMailAddress" runat="server" Width="198px" Font-Names="Verdana" Font-Size="9pt" TabIndex="6" CausesValidation="True" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        
                        <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white; height: 23px;">
                            </td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white; height: 23px;">
                            </td>
                            <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white; height: 23px;">
                                </td>
                                <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white; height: 23px;">
                                </td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 113px; border-collapse: collapse; border:solid 1px white;" >
                                </td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;">
                                </td>
                        <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white;" >
                                </td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;">
                                </td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 107px; border-collapse: collapse; border:solid 1px white; text-align: left; ">
                            </td>
                        <td colspan="3" style="border-top-width: thin; border-collapse: collapse; border:solid 1px white; text-align: left; ">
                            </td>
                    </tr>
        <tr>
            <td  style="border-collapse: collapse; border:solid 1px white; text-align: center; height: 20px;" colspan="4">
                <asp:RegularExpressionValidator ID="LocationEmail" Display="Static" ControlToValidate="txtLocationEMailAddress"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server"
                    Font-Size="10pt" ErrorMessage="  **Enter Valid Location E-Mail Address" Font-Names="Verdana" SetFocusOnError="True" ValidationGroup="validemail"></asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td style="border-collapse: collapse; border:solid 1px white; text-align: center; height: 17px;"
                 colspan="4">
                
                    <%--<asp:Button ID="btnSave" runat="server" Text="Save" Font-Bold="True" Width="77px"
                    OnClick="btnSave_Click" BackColor="Silver" Font-Size="X-Small" BorderStyle="Solid"
                    BorderWidth="1px" CssClass="buttonBig"/>--%>
                    <asp:Button ID="btnupdate" runat="server" Text="Save" Width="77px"  BackColor="Silver" Font-Size="X-Small" BorderStyle="Solid"
                    BorderWidth="1px" CssClass="buttonBig" TabIndex="15" CausesValidation="False" ValidationGroup="validate" OnClick="btnupdate_Click" Height="23px" />
                &nbsp;
              <%--  <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />&nbsp;--%>
                </td>
        </tr>
        <tr>
            <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                height: 37px; text-align: center">
                <br />
                &nbsp; &nbsp; &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                text-align: center; height: 28px;">
             
                
                
              <%--  <asp:TextBox ID="txtEncrypted" runat="server" ></asp:TextBox>--%>
             <%-- <asp:TextBox ID="txtPwd" runat="server" Enabled="False" ></asp:TextBox>
                <asp:TextBox ID="txtUID" runat="server" Enabled="False" ></asp:TextBox>
                <asp:TextBox ID="txtName" runat="server" Enabled="False"></asp:TextBox>--%>
                <%--   <asp:TextBox ID="txtMEncrypted" runat="server"  ></asp:TextBox>--%>
            &nbsp; &nbsp; &nbsp;
                
        </tr>
        <tr>
            <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                text-align: center">
                
          
                
            </td>
        </tr>
        <tr>
            <td colspan="4" style="border-collapse: collapse; border:solid 1px white; text-align: center" >
                &nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red" Visible="true" Font-Size="X-Small"
                    Font-Bold="True"></asp:Label></td>
        </tr>
        <tr style=" border-collapse: collapse; border:solid 1px white" class="HeadingBlue">
                <td colspan="4" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; border-collapse: collapse; height: 15px; text-align: center; background-color: dimgray;">
                </td>
            </tr>
    </table>
</asp:Content>

