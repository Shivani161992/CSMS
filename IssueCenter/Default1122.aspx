<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default1122.aspx.cs" Inherits="IssueCenter_Default1122" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>   <form id="form1" runat="server">      
 <div>          
  <asp:GridView ID="GridView1" runat="Server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand">      
          
           <Columns>                 
             <asp:BoundField DataField="Title" HeaderText="Title" />
                               
              <asp:TemplateField HeaderText="Year">  
              <ItemTemplate>        
              <asp:TextBox ID="text1" runat="server"></asp:TextBox> 
               <asp:Calendar ID="Cal1" runat="Server" Visible="false" OnSelectionChanged="Cal1_SelectionChanged"></asp:Calendar>
                                       </ItemTemplate>                  
              </asp:TemplateField>  
              
              <asp:TemplateField>
  <ItemTemplate>
    <asp:Button ID="AddButton" runat="server" 
      CommandName="AddToCart" 
      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
      Text="Add to Cart" />
  </ItemTemplate> 
</asp:TemplateField>
          
                           
               
           </Columns>           </asp:GridView>       </div>   </form></body>
</html>
