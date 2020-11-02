<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GunnyBagsDetails.aspx.cs" Inherits="IssueCenter_GunnyBagsDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Gunny Bags Details</title>
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href ="../MyCss/menu.css" type ="text/css" />
    
    <script language="javascript"  type="text/javascript">
    
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();		
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
 prtContent.innerHTML=strOldOne;
}
</script>
    
</head>
<body>
    <form id="form1" runat="server">
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0">
        <tr>
            <td colspan="4" style="background-color: #333333">
            </td>
        </tr>
    <tr>
    <td colspan ="4" style="background-color: #e3dba4"> 
        <asp:Label ID="title" runat="server" Text="Gunny Bags Master" ForeColor ="Maroon" Font-Size ="20px"></asp:Label>
     </td>
     </tr> 
       
      
                <tr>
                <td style="background-color: #e3dba4"> Gunny Type</td>
                <td style="background-color: #e3dba4" align="left"> 
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="114px">
                    </asp:DropDownList></td>
                    <td style="background-color: #e3dba4">
                        Gunny Category</td>
                    <td style="background-color: #e3dba4" align="left">
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="91px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                <td style="background-color: #e3dba4"> No. Of Bags</td>
                <td style="background-color: #e3dba4" align="left"> 
                    <asp:TextBox ID="TextBox1" runat="server" Width="102px"></asp:TextBox></td>
                    <td style="background-color: #e3dba4">
                        <asp:Button ID="btnadd" runat="server" Text="Add" Width="87px" /></td>
                    <td style="background-color: #e3dba4">
                    </td>
                </tr>
        <tr>
        <td colspan="4" style="background-color: #e3dba4">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  Width="425px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound ="GridView1_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None" >
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="G_Name" HeaderText="Godown Name" SortExpression="G_Name" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="8pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Stack" HeaderText="Stack Name" SortExpression="Stack" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Bags" HeaderText="Bags" SortExpression="Bags" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Weight" HeaderText="Weight" SortExpression="Weight" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
            </Columns>
                             <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                             <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                             <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                             <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                             <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                             <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
         </td>
                </tr>
                <tr>
                <td style="background-color: #e3dba4"> </td>
                    <td colspan="2" style="background-color: #e3dba4">
                        <asp:Button ID="btnaddrect" runat="server" Text="Add To Receipt" Width="167px" OnClick="btnaddrect_Click1" /></td>
                    <td style="background-color: #e3dba4">
                    </td>
                </tr>
  
    
    </table>
    </center>
    <center >
        &nbsp;</center >
    </div>
    </form>
</body>
</html>
