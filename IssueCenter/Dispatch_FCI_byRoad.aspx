<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Dispatch_FCI_byRoad.aspx.cs" Inherits="IssueCenter_Dispatch_FCI_byRoad" Title="Dispatch by Road" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id ="ronewmargin">
<center>

<table cellpadding ="0" cellspacing ="0" class ="tablelayout" style="width: 587px">

<tr>
 <td style="width: 872px">
 <table border ="0" cellpadding ="0" cellspacing ="0" style="width: 713px">
     <tr>
         <td align="center"  colspan="4" style="border-right: lightblue 1px solid;
             border-top: lightblue 1px solid; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid; background-color: #cccccc; height: 16px;">
    <asp:Label ID="lbltransfer" runat="server" Text="Dispatch  by Road" Width="360px" Font-Bold="True" Font-Size="12px"></asp:Label></td>
     </tr>
     <tr>
         <td  style="border-right: lightblue 1px solid; border-top: lightblue 1px solid;
             border-left: lightblue 1px solid; border-bottom: lightblue 1px solid; background-color: lightslategray;" colspan="4" align="center">
    <asp:Label ID="lbltransferdepot" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
        Text="To be entered By Sending Issue Center" Width="300px"></asp:Label></td>
     </tr>
 <tr>
 <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
     <asp:Label ID="lblpendqty" runat="server" Width="82px" Visible="False"></asp:Label></td>
 <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
     <asp:TextBox ID="txtrono" runat="server" Width ="148px" Visible="False" OnTextChanged="txtrono_TextChanged"></asp:TextBox>
 </td>
     <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         <asp:Label ID="lbltid" runat="server" Visible="False"></asp:Label></td>
  <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 224px;"> <asp:TextBox ID="txtroqty" runat="server"  Width ="130px" Visible="False"></asp:TextBox></td>
 
 </tr>
     <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             <asp:Label ID="lblDistrictName" runat="server" Text="Sending District"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Navy"
                 Width="153px"></asp:Label></td>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="lblNameDepot" runat="server" Text="Sending Depot"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 224px;">
             <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Navy"
                 Width="128px"></asp:Label></td>
     </tr>
     <tr>
         <td align="left" class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
             <asp:Label ID="Label2" runat="server" Text="Dispatch Type"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
             <asp:DropDownList ID="ddldispatchType" runat="server" Width="153px" OnSelectedIndexChanged="ddldispatchType_SelectedIndexChanged" AutoPostBack="True" ondatabound="ddldispatchType_DataBound">
                
                
                 
             </asp:DropDownList></td>
       <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
             <asp:Label ID="lbl_dispdate" runat="server" Text="Dispatch Date"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc; width: 224px;">   <asp:TextBox ID="DaintyDate1" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
                 new tcal({
                     'formname': '0',
                     'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
                 });
	     </script>
         </td>
         </td>
     </tr>
     <tr>
         <td align="left" class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
             <asp:Label ID="lblyear" runat="server" Text=" Sending Year" Visible="false"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
             &nbsp;<asp:DropDownList ID="ddlallot_year" runat="server" OnSelectedIndexChanged="ddlallot_year_SelectedIndexChanged"
                 Width="153px" Visible="false">
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
             <asp:Label ID="lblmonth" runat="server" Text="Sending Month" Width="109px" Visible="false"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc; width: 224px;">
             <asp:DropDownList ID="ddlalotmm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlalotmm_SelectedIndexChanged"
                 Width="153px" Visible="false">
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
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             <asp:Label ID="lbltono" runat="server" Text="Transport Order No." Width="138px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddltono" runat="server" OnSelectedIndexChanged="ddltono_SelectedIndexChanged"
                 Width="155px" AutoPostBack="True">
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Label ID="lblmono" runat="server" Text="T.O.No." Visible="False" Width="97px"></asp:Label>
             &nbsp;
         </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 224px;">
             <asp:TextBox ID="txttono" runat="server" Visible="False" Width="146px"></asp:TextBox>&nbsp;
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttono"
                 ErrorMessage="T.O. Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
     <tr>
         <td class="tdmarginddl" colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
             &nbsp;<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None"
                 OnRowDataBound="GridView2_RowDataBound" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" ForeColor="#333333" Width="598px"
                 >
                 <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                 <Columns>
                     <asp:TemplateField>
                         <AlternatingItemTemplate>
                             <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="false" />
                         </AlternatingItemTemplate>
                         <ItemTemplate>
                             <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="false" />
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="TO_Number" HeaderText="TO No." SortExpression="TO_Number">
                         <HeaderStyle CssClass="gridlarohead" />
                         <ItemStyle CssClass="griditemlaro" Font-Size="8pt" />
                     </asp:BoundField>
                     <asp:BoundField DataField="district_name" HeaderText="To District" SortExpression="district_name">
                         <HeaderStyle CssClass="gridlarohead" />
                         <ItemStyle CssClass="griditemlaro" />
                     </asp:BoundField>
                     <asp:BoundField DataField="DepotName" HeaderText="IssueCenter" SortExpression="DepotName">
                         <HeaderStyle CssClass="gridlarohead" />
                         <ItemStyle Font-Size="10pt" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Quantity" HeaderText="Qty." SortExpression="Quantity">
                         <HeaderStyle CssClass="gridlarohead" />
                         <ItemStyle CssClass="griditemlaro" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Qty_send" HeaderText="Lifted Qty." SortExpression="Qty_send">
                         <HeaderStyle CssClass="gridlarohead" />
                         <ItemStyle CssClass="griditemlaro" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Pending" HeaderText="Pending Qty." SortExpression="Pending">
                         <HeaderStyle CssClass="gridlarohead" />
                         <ItemStyle CssClass="griditemlaro" />
                     </asp:BoundField>
                     <asp:BoundField DataField="toDistrict" HeaderText="DCode" SortExpression="toDistrict">
                         <HeaderStyle Font-Size="1px" />
                         <ItemStyle Font-Size="1px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="toIssueCenter" HeaderText="ICode" SortExpression="toIssueCenter">
                         <HeaderStyle Font-Size="1px" />
                         <ItemStyle Font-Size="1px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Trunsuction_Id" HeaderText="ID" SortExpression="Trunsuction_Id">
                         <HeaderStyle Font-Size="1px" />
                         <ItemStyle CssClass="griditemlaro" Font-Size="1px" />
                     </asp:BoundField>
                 </Columns>
                 <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                 <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                 <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                 <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                 <EditRowStyle BackColor="#999999" />
             </asp:GridView>
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; height: 24px; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
         </td>
         <td align="left" style="font-size: 10pt; position: static; height: 24px; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
         </td>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; height: 24px; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:Button ID="btngetdtl" runat="server" Text="Get Details" Width="135px" OnClick="btngetdtl_Click" Visible="False" /></td>
         <td align="left" style="font-size: 10pt; position: static; height: 24px; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 224px;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
             <asp:Label ID="lblrecddist" runat="server" Text="Receiving FCI District" Width="142px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="153px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:Label ID="lblrecdepo" runat="server" Text="Receiving FCI Depot" Width="135px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
             <asp:DropDownList ID="ddlissuecenter" runat="server" Width="153px" AutoPostBack="True" OnSelectedIndexChanged="ddlissuecenter_SelectedIndexChanged">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" 
             style="border: 1px solid silver; font-size: 10pt; position: static; background-color: #cfdcdc; height: 24px; width: 186px;" 
             align="left">
             <asp:Label ID = "lblstate" runat = "server" Visible = "false" Text = "Select Receiving State"></asp:Label>
             </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="True" 
                 Height="21px" Width="150px" 
                 onselectedindexchanged="ddlstate_SelectedIndexChanged" Visible="False">
             </asp:DropDownList>
         </td>
         <td class="tdmarginddl" style="border: 1px solid silver; font-size: 10pt; position: static; background-color: #cfdcdc; height: 24px; width: 185px;">
            <asp:Label ID = "lblrec_stdist" runat = "server" Visible = "false" Text = "Select Receiving District"></asp:Label>
         </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
             <asp:DropDownList ID="ddl_stDistrict" runat="server" Height="24px" 
                 Width="168px" Visible="False">
             </asp:DropDownList>
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
             <asp:Label ID="lbldispsource" runat="server" Text="Stock Issued From" Width="137px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:DropDownList ID="ddlsarrival" runat="server" 
                 Width="153px">
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
     <asp:DropDownList ID="ddlscheme" runat="server"  Width ="153px" >
     
     </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
             <br />
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <br />
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width ="153px"  AutoPostBack ="True" 
                 onselectedindexchanged="ddlcomdty_SelectedIndexChanged" >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
                                <asp:Label ID="lbl_Branch" runat="server" Text="Branch"></asp:Label>
                            </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
                                <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" 
                                    
                 onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="135px">
                                </asp:DropDownList>
                            </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
             &nbsp;</td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             &nbsp;</td>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:Label ID="lblGodownNo" runat="server" Text="Dispatch Godown"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
     <asp:DropDownList ID="ddlgodown" runat="server" Width="153px" OnSelectedIndexChanged="ddldispdipo_SelectedIndexChanged" AutoPostBack="True">
     <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList></td>
     </tr>
     <tr>
         <td align="left" class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 24px; background-color: #cfdcdc">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 24px; background-color: #cfdcdc" colspan="3">
             <strong><span style="color: crimson">स्वयं के जिले के अलावा परिवहन करता का नाम जरुरी
                 नहीं है|</span></strong></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
             <asp:Label ID="lblbqty" runat="server" Text="Balance Quantity "></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:TextBox ID="txtbqty" runat="server" Width="145px"></asp:TextBox></td>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:Label ID="Label21" runat="server" Text="Current Bags" Width="115px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
             <asp:TextBox ID="txtcurbags" runat="server" Width="146px"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
             &nbsp;</td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
      

         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:Label ID="lblTrans" runat="server" Text="Transporter Name " Width="119px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
     <asp:DropDownList ID="ddltransporter" runat="server" Width="183px">
     </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
             <asp:Label ID="lblchallanNumber" runat="server" Text="Truck Challan No."></asp:Label></td>
         <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:DropDownList ID="ddlchallan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged"
                 Visible="False" Width="153px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList>
             &nbsp;&nbsp;
             <br />
     <asp:TextBox ID="txttrukcno" runat="server"  Width ="146px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttrukcno"
         ErrorMessage="Challan Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No."></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
         <asp:TextBox ID="txttruckno" runat="server" Width="146px"></asp:TextBox></td>
     </tr>
  <tr>
  <td class ="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
      <asp:Label ID="lblBagNumber" runat="server" Text="NO.Of Bags"></asp:Label></td>
 <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
     <asp:TextBox ID="txtbagno" runat="server" Width="146px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbagno"
         ErrorMessage="No. of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td class ="tdmarginddl" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
         <asp:Label ID="lblQuantity" runat="server" Text="Dispatch Qty."></asp:Label></td>
  <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;"> <asp:TextBox ID="txtquant" runat="server" Width="146px"></asp:TextBox>Qtls.<asp:RequiredFieldValidator
      ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtquant" ErrorMessage="Quantity Required"
      ValidationGroup="1">*</asp:RequiredFieldValidator></td>
 
  
  </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
         </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:Label ID="lbldist" runat="server" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:Label ID="lbldepo" runat="server" Visible="False"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
         </td>
     </tr>
     <tr>
         <td  style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
             <asp:Label ID="lbldistime" runat="server" Text="Time Of Dispatch" Width="125px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             
                                               
    <asp:DropDownList ID="ddlhour" runat="server">
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                        </asp:DropDownList>
                        :

  
 
 
 <asp:DropDownList ID="ddlminute" runat="server">
                             <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                              <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="26">26</asp:ListItem>
                            <asp:ListItem Value="27">27</asp:ListItem>
                            <asp:ListItem Value="28">28</asp:ListItem>
                            <asp:ListItem Value="29">29</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="31">31</asp:ListItem>
                            <asp:ListItem Value="32">32</asp:ListItem>
                            <asp:ListItem Value="33">33</asp:ListItem>
                            <asp:ListItem Value="34">34</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="36">36</asp:ListItem>
                            <asp:ListItem Value="37">37</asp:ListItem>
                            <asp:ListItem Value="38">38</asp:ListItem>
                            <asp:ListItem Value="39">39</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="41">41</asp:ListItem>
                            <asp:ListItem Value="42">42</asp:ListItem>
                            <asp:ListItem Value="43">43</asp:ListItem>
                            <asp:ListItem Value="44">44</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="46">46</asp:ListItem>
                            <asp:ListItem Value="47">47</asp:ListItem>
                            <asp:ListItem Value="48">48</asp:ListItem>
                            <asp:ListItem Value="79">49</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="51">51</asp:ListItem>
                            <asp:ListItem Value="52">52</asp:ListItem>
                            <asp:ListItem Value="53">53</asp:ListItem>
                            <asp:ListItem Value="54">54</asp:ListItem>
                            <asp:ListItem Value="55">55</asp:ListItem>
                            <asp:ListItem Value="56">56</asp:ListItem>
                            <asp:ListItem Value="57">57</asp:ListItem>
                            <asp:ListItem Value="58">58</asp:ListItem>
                            <asp:ListItem Value="59">59</asp:ListItem>
                          
                        </asp:DropDownList>

:
 
    <asp:DropDownList ID="ddlampm" runat="server" Width="43px">
    <asp:ListItem Value="01">AM</asp:ListItem>
    <asp:ListItem Value="02">PM</asp:ListItem>
    </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
                                <asp:Label ID="Label32" runat="server" 
                 style="color: #000000" Text="Crop Year"></asp:Label>
         </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 224px;">
                                <asp:DropDownList ID="ddlcropyear" runat="server" 
                 Width="130px">
                                    <asp:ListItem>2015-2016</asp:ListItem>
                                    <asp:ListItem Value="2014-2015">2014-2015</asp:ListItem>
                                    <asp:ListItem Value="2013-2014">2013-2014</asp:ListItem>
                                    <asp:ListItem Value="2012-2013">2012-2013</asp:ListItem>
                                    <asp:ListItem Value="2011-2012">2011-2012</asp:ListItem>
                                    <asp:ListItem Value="2010-2011">2010-2011</asp:ListItem>
                                    <asp:ListItem Value="2009-2010">2009-2010</asp:ListItem>
                                </asp:DropDownList>
         </td>
     </tr>
  <tr>
  <td class ="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left" >
      <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label></td>
      <td align="left" colspan="3" rowspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
     <asp:TextBox ID="txtremark" runat="server"  Width ="347px" TextMode ="MultiLine" ></asp:TextBox>
      </td>
  </tr>
  <tr>
  <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left"> 
      <asp:Label ID="lblstatus" runat="server" Visible="False">N</asp:Label></td>
  
  </tr>
     <tr>
         <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
         </td>
         <td align="left" colspan="3" rowspan="1" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="217px" />
         </td>
     </tr>
  <tr>
   
 <td style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left"></td>
      <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcdc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;">
      <asp:Button ID="btnsave" runat="server" Text="Submit" OnClick="btnsave_Click" ValidationGroup="1" Width="89px"/>
          <asp:Button ID="btnaddnew" runat="server" Text="New" OnClick="btnaddnew_Click" Width="85px"/>&nbsp;
          <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="83px" />
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False" Width="109px">Print Challan</asp:HyperLink></td>
     
      

  
  </tr>
     <tr>
         <td colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcdc;" align="left">
             <asp:Label ID="Label1" runat="server" Text="Label" Width="404px" Visible="False"></asp:Label></td>
     </tr>
 </table>
 
 </td>
</tr>
</table>

</center>
</div>
<script type="text/javascript">
function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}

var num=tx.value;
var len=num.length;
var indx=-1;
indx=num.indexOf('.');
if (indx != -1)
{
var coda = event.keyCode
 if(coda == 46)
 {
    alert('Decimal cannot come twice');
    event.cancelBubble = true;
    event.returnValue = false;
 }
var dgt=num.substr(indx,len);
var count= dgt.length;
//alert (count);
if (count > 5)  
{
 alert("Only 5 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}



    </script>
</asp:Content>

