<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="PurchaseCentereMaster.aspx.cs" Inherits="Admin_PurchaseCentereMaster" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: lightslategray 3px double; border-top: lightslategray 3px double; border-left: lightslategray 3px double; border-bottom: lightslategray 3px double; width: 569px;" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()">
     <tr>
         <td colspan="4" style="border-right: 1px solid; border-top: 1px solid; font-size: 10pt;
             border-left: 1px solid; border-bottom: 1px solid; background-color: #cccccc">
             <strong>
             Purchase Centere Master</strong></td>
     </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="left" >
             <asp:Label ID="lblopendate" runat="server" Text="Marketing Sesion" Font-Size="10pt" Width="99px"></asp:Label></td>
         <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
             <asp:DropDownList ID="ddlseason" runat="server" Width="145px" OnSelectedIndexChanged="ddlseason_SelectedIndexChanged">
                 <asp:ListItem Value="r">Rabi</asp:ListItem>
                 <asp:ListItem Value="k">Kharif</asp:ListItem>
             </asp:DropDownList></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:Label ID="Label3" runat="server" Text="Crop Year" Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
        

             &nbsp;<asp:DropDownList ID="ddlcropyear" runat="server"  Width="161px" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged" >
                                               <asp:ListItem Value="02">2011-2012</asp:ListItem>
                                                <asp:ListItem Value="03">2010-2011</asp:ListItem>
                                                <asp:ListItem Value="04">2009-2010</asp:ListItem>
                                                <asp:ListItem Value="05">2008-2009</asp:ListItem>
                                                <asp:ListItem Value="06">2007-2008</asp:ListItem>
                                                <asp:ListItem Value="07">2006-2007</asp:ListItem>
                                                <asp:ListItem Value="08">2005-2006</asp:ListItem>
                                                <asp:ListItem Value="09">2004-2005</asp:ListItem>
                                                <asp:ListItem Value="10">2003-2004</asp:ListItem>
                                                <asp:ListItem Value="11">2002-2003</asp:ListItem>
                                                </asp:DropDownList></td>
     </tr>
    <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
         <asp:Label ID="Label2" runat="server" Text="District"></asp:Label></td>
        <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:DropDownList ID="ddldistoff" runat="server" TabIndex="5" Width="148px" AutoPostBack="True" OnSelectedIndexChanged="ddldistoff_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="161px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged"  >
       <asp:ListItem Value="6" Selected ="True">Wheat</asp:ListItem>
      </asp:DropDownList></td>
    </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 18px;" colspan="4">
             &nbsp;</td>
     </tr>
                                
                       
                                   
                             
   
       <tr>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" > </td>
           <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
           </td>
       <td colspan ="1" align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
           &nbsp;
       </td>
           <td align="left" colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
           </td>
       </tr>
    <tr>
        <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:Label ID="Label4" runat="server" Font-Size="10pt" Text="Purchase Center Name" Width="124px"></asp:Label></td>
        <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:TextBox ID="txtCentreName" runat="server"></asp:TextBox></td>
        <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:Label ID="Label5" runat="server" Font-Size="10pt" Text="Nodal officer" Width="99px"></asp:Label></td>
        <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:TextBox ID="txtNodalOff" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="font-size: 10pt; position: static; background-color: #cfdcdc" align="left">
            <asp:Label ID="Label6" runat="server" Font-Size="10pt" Text="Centre Address" Width="97px"></asp:Label></td>
        <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
        <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:Label ID="Label7" runat="server" Font-Size="10pt" Text="Block/Tehsil" Width="99px"></asp:Label></td>
        <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:TextBox ID="txtBlock" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="font-size: 10pt; position: static; background-color: #cfdcdc" align="left">
            <asp:Label ID="Label8" runat="server" Font-Size="10pt" Text="Phone No." Width="79px"></asp:Label></td>
        <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td>
        <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:Label ID="Label9" runat="server" Font-Size="10pt" Text="Remarks" Width="99px"></asp:Label></td>
        <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
        </td>
        <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
        <asp:Button ID="btnsubmit" runat="server" Text="Save " OnClick="btnsubmit_Click" ValidationGroup="1" Width="82px" /></td>
        <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="74px" /></td>
        <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:TextBox ID="txtpcid" runat="server"></asp:TextBox></td>
    </tr>
     <tr>
         <td align="left" colspan="3" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            
         </td>
         <td align="left" colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td align="left" colspan="3" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label></td>
         <td align="left" colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
       <tr>
       <td align="left" colspan="3" rowspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" />
       </td>
           <td align="left" colspan="1" rowspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
           </td>
       </tr>
 </table>
 <div>
     &nbsp;</div>
  <div>
    &nbsp;</asp:Content>

