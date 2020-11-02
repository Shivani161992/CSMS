<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true"
    CodeFile="Godown_Master_New.aspx.cs" Inherits="IssueCenter_Godown_Master_New"
    Title="Godown Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
  
    <div>
        <div>
            <table class="yelloCell" style="width: 500px; margin-left: 10px; background-image: url(../images/images[26].jpg);">
                <tr>
                    <td colspan="2" style="background-color: dimgray; height: 15px; width: 503px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; width: 503px;">
                        <strong><span style="font-size: 12pt; color: #990033">
                           
                            <asp:Label ID="lblGodownMaster" runat="server" Text="Godown Master"></asp:Label></span></strong></td>
                </tr>
                 <tr>
                
                    <td colspan="2" style="text-align: center; width: 503px;">
                       <asp:Label ID="lbl_Branch" runat="server" Text="Branch Name" ></asp:Label> 
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddl_Branch_Name" runat="server" Width ="171px"  AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_Name_SelectedIndexChanged" > </asp:DropDownList>
                          </td>
                </tr>
                 <tr>
                    <td colspan="2" style="text-align: center; width: 503px;">
                        <strong><span style="font-size: 12pt; color: #990033">
                           
                            <asp:Label ID="lblMsg" runat="server" ></asp:Label></span></strong></td>
                </tr>
               
            </table>
        </div>
      
    </div>
</asp:Content>
