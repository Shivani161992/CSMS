<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginLog.aspx.cs" Inherits="mpproc_Admin_LoginLog" MasterPageFile="~/mpproc/MasterPage/AdminMasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%-- <link rel="stylesheet" type="text/css" href="../../CSS/calendar.css" />--%>
<script type="text/javascript" src="../../js/calendar_eu.js"></script> 

 
 
<table border="0" cellpadding ="2" cellspacing ="2" width="700"  style="font-size:12px">

<tr>
<td colspan="4"style="text-align: center; background-color:#cccc66; height:30px; ">

    <asp:Label ID="lbl_MasDetailTitle" runat="server" Text="User Login Log" Font-Bold="True" Font-Size="Small"></asp:Label>
 
</td>
</tr>
<tr>
<td colspan="4" style="height:30px">
<div id="DivMsg" runat="server"  style="color: #990033;"></div>
</td>

</tr>
  
<tr>
<td style="background-color:#d3d3d3; width: 143px; height: 26px;" align="left">
    <asp:Label ID="lbl_From" runat="server" Text="Date From:-"></asp:Label>


</td>
<td align="left" style="width: 270px; height: 26px">
    <asp:TextBox ID="txt_from" runat="server"></asp:TextBox>
    
                                <script type="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txt_from'
	    });


                                </script>
    
    
    </td>
<td style="background-color: #d3d3d3; width: 65px; height: 26px;" align="left">
    <asp:Label ID="lbl_CropYear" runat="server" Text="To:"></asp:Label>

</td>
<td align="left" style="height: 26px; width: 225px;">
    <asp:TextBox ID="txt_Todate" runat="server"></asp:TextBox>
    
    
                                <script type="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txt_Todate'
	    });


                                </script>
    
    
    </td>

</tr>
   
<tr>

</tr>
    <tr>
        <td colspan="4" style="height: 5px; background-color: #ffffff">
            <asp:Button ID="btn_Get" runat="server" OnClick="btn_Get_Click" Text="Get" /></td>
    </tr>
<tr>
    <td colspan="4" style="background-color: #cccc66;  height: 5px;">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None"
            BorderWidth="1px" CellPadding="3" CellSpacing="2" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
          
            <FooterStyle BackColor="#F7DFB5" ForeColor="Navy" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            
            <Columns>
                      
                   <asp:TemplateField HeaderText="S.N." ControlStyle-ForeColor="SkyBlue">
                            <ItemTemplate>
                               <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                       <HeaderStyle HorizontalAlign="Left" Width="20px" />
                    </asp:TemplateField>
                 
                    
                 <asp:TemplateField HeaderText="Login User">
                            <ItemTemplate  >
                               <%#Eval("User_Name")%>
                            </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Center" Width="300px" />
                     <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Login Date">
                         <ItemTemplate >
                         
                        <asp:Label ID="lbl_LogDate" runat="server"  Text ='<%#Eval("Login_Date")%>'></asp:Label>
                           
                          <%--<%#String.Format("{00:dd/MMM/yyyy}", Eval("Login_Date"))%>    --%>            
                 
                          
                            </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="70px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                  
                 <asp:TemplateField HeaderText="Login Time" >
                           <ItemTemplate>
                                            
                        <asp:Label ID="lbl_LogTime" runat="server"  Text ='<%#Eval("Login_Date")%>'></asp:Label>
                     <%--     <%#String.Format("{00:hh:mm:ss tt}", Eval("Login_Date"))%>                --%>
                            </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Left" Width="70px" />
                    </asp:TemplateField>
                    
                   <asp:TemplateField HeaderText="IP Address">
                            <ItemTemplate>
                            
                             
                                <%#Eval("IP_Address")%>
                            </ItemTemplate>
                       <HeaderStyle HorizontalAlign="Left" Width="70px" />
                    </asp:TemplateField>
                
                     <asp:TemplateField HeaderText="User Agent">
                            <ItemTemplate>
                            
                                <%#Eval("UserAgent")%>
                            </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" Width="70px" />
                    </asp:TemplateField>
                        
                
                    </Columns>
        </asp:GridView>
            
          
           
            </td>
    </tr>







</table>
  
</asp:Content>
