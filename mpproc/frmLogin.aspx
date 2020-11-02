<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLogin.aspx.cs" Inherits="frmLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MP Procurement Login</title>
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src ="js/chksql.js"></script>
       <script type="text/javascript" src="js/md5.js"></script>
       <script type="text/javascript">
    window.history.forward(0); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
  <table width="932" border="0"   cellpadding="0" cellspacing="0"  style="border-color:#F8F6F7; background-color:#F8F6F7";   >
 <tr>
    <td   align="center" valign="middle"  style="background-color:#F8F6F7; height:65px">
   
        <img src="Images/MP-header.jpg" height="65"  width="930" alt="mpHeader"/>
       
   </td>
   </tr>
    <tr>
          <td>
          <table border="0" cellpadding="0" cellspacing="0" width="100%"  >
    <tr>
    <td   align="center">
    <table border="1" cellpadding ="1"  cellspacing ="2"   style="margin-top:10px; margin-bottom:10px"> 
      <tr >
      <td colspan="2"   align="center" style="background-color:#cccc66">
      Login
      </td>
      </tr>
      <tr>
      <td colspan="2" style="color: #006633; height: 22px;">
    
       Select Login Type
      
      </td>
      </tr>
      <tr>
       <td align="left" style="font-size:14px; color: #990000; width: 70px; height: 50px;">
       <asp:RadioButton ID="RDButonAgency" Text="Agency" GroupName="LogType"  runat="server" AutoPostBack="True"  />    
      </td>  
      <td rowspan="4" style="height:150px">
      <table border="0" width="500px">
      <tr>
      <td style="height: 134px">
    
     <%-- --------------------%>
      <table border="0" style="font-size:12px; margin-left:40px" width="60%" >
     <%-- <tr>
      <td  align="left" >
              
          <asp:Label ID="labelName" runat="server" Text="Name"></asp:Label>
          
      </td>
    
        <td  align="left">
      
        <asp:DropDownList ID="DDLName" runat="server" AutoPostBack="True">
            <asp:ListItem>Name</asp:ListItem>
     
        </asp:DropDownList>
       
      </td>
  
      </tr>--%>
  
      <tr>
      <td  align="left" >
             <asp:Label ID="lbl_Agency" runat="server" Text="Agency"></asp:Label>
      </td>
       <td  align="left">
      
        <asp:DropDownList ID="DDL_Agency" runat="server" AutoPostBack="True" ToolTip=" Click For Select Agency Name" >
     
        </asp:DropDownList>
    
      
      </td>
      
     
      </tr>
      <tr>
     <td  align="left" >
         
      <asp:Label ID="lbl_Dist" runat="server" Text="District"></asp:Label>
      
      </td>
       
      <td  align="left">
        <asp:DropDownList ID="DDL_Dist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged">
         </asp:DropDownList>
      
      </td>
   </tr>
    <tr>
      <td  align="left" >
         
      <asp:Label ID="lbl_Depot" runat="server" Text="Depot"></asp:Label>
      
      </td>
       
      <td  align="left">
        <asp:DropDownList ID="DDL_Depot" runat="server" AutoPostBack="True">
         </asp:DropDownList>
      
      </td>
    </tr>
      <tr> 
       <td  align="left" >
      <asp:Label ID="lbl_CropYear" runat="server" Text="Crop Year"></asp:Label>
       
      
      </td>
      
     
       <td  align="left">
        <asp:DropDownList ID="DDL_CropYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_CropYear_SelectedIndexChanged" Enabled="False">
        </asp:DropDownList>
      
      </td>
      </tr>
      <tr> 
     <td  align="left" >
      
      
      <asp:Label ID="lbl_MarkSeas" runat="server" Text="Marketing Season"></asp:Label>
   
      </td>
       
       <td  align="left">
        <asp:DropDownList ID="DDL_MarkSeas" runat="server" OnSelectedIndexChanged="DDL_MarkSeas_SelectedIndexChanged" AutoPostBack="True" Enabled="False">
        </asp:DropDownList>
      
      </td>
   
      </tr>
          <tr>
              <td align="left">
                  <asp:Label ID="lbl_PC" runat="server" Text="Purchase Center:"></asp:Label></td>
              <td align="left">
                  <asp:DropDownList ID="DDL_PC" runat="server">
                  </asp:DropDownList></td>
          </tr>
      
      <tr>
       <td  align="left" >
          <asp:Label ID="lbl_Admin" runat="server" Text="Name"></asp:Label>
      
      
       </td>
        <td  align="left">
        <asp:DropDownList ID="DDL_Admin" runat="server">
        </asp:DropDownList>
      
      </td>
      </tr>
      </table>
   
       <%-- --------------------%>
          <asp:CheckBox ID="chkB_Hindi" runat="server" Text="Hindi Version" />||
          <a href="User_Manual/User_Manual.pdf"><span style="color: #000066">User Manual</span></a>
          </td>
      
      </tr>
      </table>
          </td>   
      </tr>
      
      <tr>
       <td align="left" style="font-size:14px; color: #990000; width: 70px;" rowspan="2">
      <%-- <asp:RadioButton ID="RDButonStorage" Text="Storage" GroupName="LogType" runat="server" Visible="False" /> --%> 
          <asp:RadioButton ID="RDButonState" Text="State" GroupName="LogType" runat="server" AutoPostBack="True"  />  
      </td>  
      
          
   
         
      </tr>
      
      <tr>
      </tr>
        <tr>
            <td align="left" style="font-size: 14px; width: 70px; color: #990000; height: 33px;">
                <asp:RadioButton ID="RDButonAdmin" Text="Admin" GroupName="LogType" runat="server" AutoPostBack="True" /></td>
        </tr>
   
      <tr>
      
      <td  colspan="2" align="center">
      <table border="1" style="background-color: #cccc66;">
      <tr>
      <td >
       Password:   
      </td>
      <td>
          <asp:TextBox ID="txt_password"  runat="server" TextMode="Password" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_password"
              ErrorMessage="Password Can not be blank" Font-Bold="True" ValidationGroup="1">*</asp:RequiredFieldValidator>
             
         
      </td>   
      </tr>
      </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ValidationGroup="1"  ShowSummary="false"/>
      </td>  
      
      </tr>
     
      <tr>
      <td align="center" colspan="2">
          <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="btn_login_Click" ValidationGroup="1" ToolTip="Click for Login" />
      </td>
      
      </tr>
       </table>
    
    </td>
    </tr>
    
    </table>
          </td>
     </tr>
      <tr>
            <td align="center" valign="middle" colspan="2" style="height: 73px">
               <div style="padding:5px;background-color:#cccccc;"></div>
                <div class="divFooter">
                    &nbsp;Developed & Maintented By 
                   <br />
                     <div >
                       <img src="Images/Nic-Logo_blue.gif"  width="200" height="50" alt="niclogo"/>
                       
                    </div>                         
                 </div>
                 </td>
        
        </tr>
        <tr>
        <td>
        
       <table border="0"  cellpadding ="0" cellspacing="0" style="margin-top :30px">
       <tr>
       <td align="center" style="font-size :small; background-color: #cccc66;"  >
         <hr />
       © 2011 National Informatics Centre.All Rights Reserved : 
       No part of this application software code and it's documentation may be reproduced in any form or stored in a database or retrieval system or transmitted or distributed in any form by any means, electronic, mechanical photocopying, recording or otherwise without prior permission of the Food and Consumer Affairs Division, National Informatics Centre except as permitted by the Copyright Act. 
         <hr />
       </td>
        
       </tr>
       
       </table>
        
        </td>
        
        
        </tr>
     </table>
     </center>
    </div>
    </form>
</body>
</html>
