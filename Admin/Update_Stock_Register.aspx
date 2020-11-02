<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="Update_Stock_Register.aspx.cs" Inherits="Admin_Update_Stock_Register" Title="Stock Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> Stock Register</div>
<div id="transcheme">
<div id ="ronewmargin">

 <table cellpadding ="0" cellspacing ="0" border ="0" >
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Source</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; font-weight: bold;
             border-left: 1px solid; width: 172px; color: #003333; border-bottom: 1px solid;
             font-style: italic">
             <asp:DropDownList ID="ddlsarrival" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             &nbsp; &nbsp;&nbsp;
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:Label ID="Label6" runat="server" Text="District Name"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddldistrict" runat="server"  Width="152px" AutoPostBack="True" OnSelectedIndexChanged="ddldistric_SelectedIndexChanged" Font-Bold="False" Font-Italic="True" ForeColor="Navy"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             <asp:Label ID="Label7" runat="server" Text="Issue Center "></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddlissue" runat="server"  Width="132px" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged"  >
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:Label ID="Label3" runat="server" Text="Month"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddl_allot_month" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged"
                 Width="153px">
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
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             <asp:Label ID="Label5" runat="server" Text="Year" Width="64px"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddd_allot_year" runat="server" OnSelectedIndexChanged="ddd_allot_year_SelectedIndexChanged"
                 Width="133px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
             <asp:Label ID="Label8" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="width: 172px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="152px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged"  >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginro" style="width: 141px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
             &nbsp;<asp:Label ID="Label2" runat="server" Text="Scheme"></asp:Label></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid">
             <asp:DropDownList ID="ddlscheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged"
                 Width="135px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
 <tr>
  <td class ="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;"  >
      </td>
  <td  align="left" style="width: 172px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
      </td>
     <td class ="tdmarginro" style="width: 141px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" >
         &nbsp; &nbsp;&nbsp;Godown</td>
     <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid" >
         &nbsp;&nbsp;</td>
 </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             <asp:Button ID="Button2" runat="server" Text="Opening" Width="122px" OnClick="Button2_Click" /></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtgetopening" runat="server"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             <asp:Button ID="Button3" runat="server" Text="Receipt" Width="123px" OnClick="Button3_Click" /></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtgreceipt" runat="server"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             <asp:Button ID="Button4" runat="server" Text="Dispatch" Width="121px" OnClick="Button4_Click" /></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtgettruck" runat="server"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             <asp:Button ID="Button5" runat="server" Text="Issue Agains DO" Width="124px" OnClick="Button5_Click" /></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtgettc" runat="server"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             <asp:Button ID="Button6" runat="server" Text="Scheme Transfer" Width="125px" OnClick="Button6_Click" /></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtgetscheme" runat="server" Width="107px"></asp:TextBox>Send</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtrecdScheme" runat="server" Width="76px"></asp:TextBox>Receive</td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             <asp:Button ID="Button8" runat="server" Text="Get Cap" Width="123px" /></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtccap" runat="server"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             <asp:Button ID="Button9" runat="server" Text="Get Cap" Width="123px" /></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             <asp:Button ID="Button10" runat="server" Text="Get Cap" Width="123px" /></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
             <asp:Button ID="Button7" runat="server" Text="Insert Stock" Width="169px" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid; background-color: #006699">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid; background-color: #006699">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid; background-color: #006699">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid; background-color: #006699">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; font-weight: bold;
             border-left: 1px solid; width: 172px; color: #003333; border-bottom: 1px solid;
             font-style: italic">
      <asp:Button ID="Button1" runat="server" Text="Get Details" Width="171px" OnClick="Button1_Click" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Opening Balance</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; font-weight: bold;
             border-left: 1px solid; width: 172px; color: #003333; border-bottom: 1px solid;
             font-style: italic">
             <asp:TextBox ID="txtopenbal" runat="server"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Receipt From FCI</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
             <asp:TextBox ID="txtrecdfci" runat="server"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             Distribution</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtdistribution" runat="server"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Receipt From Procurement</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
             <asp:TextBox ID="txtrecdproc" runat="server"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             Transfer to Other Godown</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txtsaltoother" runat="server"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Receipt From Other Godown</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
             <asp:TextBox ID="txtrecdother" runat="server"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
             Transfer To Other
             Scheme</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             <asp:TextBox ID="txttranssch" runat="server"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Receipt From Other Scheme</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
             <asp:TextBox ID="txtrecdosch" runat="server"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid; height: 30px">
             Received from other Source</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid; height: 30px">
             <asp:TextBox ID="txtrecdOS" runat="server"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid; height: 30px">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid; height: 30px">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Received from CMR</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
             <asp:TextBox ID="txtrecdCMR" runat="server"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             Received from Levy Rice</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
             <asp:TextBox ID="txtrecdLR" runat="server"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
         </td>
     </tr>
  <tr>
  <td class ="tdmarginro" style="width: 154px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" >
      Received from Rail Head</td>
 <td style="width: 172px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
     <asp:TextBox ID="txtrecdRH" runat="server"></asp:TextBox></td>
     <td class ="tdmarginro" style="width: 141px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
         </td>
  <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid"> &nbsp;</td>

  </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp;&nbsp;
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 172px; border-bottom: 1px solid">
             <asp:Button ID="btnsubmit" runat="server" Text="Update" OnClick="btnsubmit_Click" ValidationGroup="1" Width="143px" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 141px; border-bottom: 1px solid">
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="147px" />&nbsp;</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 154px; border-bottom: 1px solid">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td align="left" colspan="4" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid;">
             <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label>&nbsp;
         </td>
         <td style="height: 16px">
         </td>
         <td style="height: 16px">
         </td>
         <td style="height: 16px">
         </td>
     </tr>
       <tr>
       <td align="left" colspan="4" rowspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; width: 154px; border-bottom: 1px solid"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" />
     </td>
       <td> </td>
       <td> </td>
       </tr>
     <tr>
         <td>
         </td>
         <td>
         </td>
     </tr>
 </table>

</div>
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


