<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperatorRegistration.aspx.cs" Inherits="District_OperatorRegistration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Operator Registration</title>
    <script type="text/javascript" language="javascript">
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

//alert (count);
if (len > 10 )  
{
 alert("Only 10  digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}


}

    </script>
    
    
     <script type="text/javascript" language="javascript">
function CheckIsChar(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 65) || (AsciiCode > 122))
{
alert('Please enter only character');
event.cancelBubble = true;
event.returnValue = false;
}



}

    </script>
    
    
    
    
    
     <script type="text/javascript" language="javascript">
function CheckIsNumericMobile(tx)
{
var num=tx.value;
var len=num.length;

//alert (count);
if (len<10)  
{
 alert("Mobile Number should be 10 Digits");
 event.cancelBubble = true;
 event.returnValue = false;
}


}
function Checkpasswaordlength(tx)
{
var num=tx.value;
var len=num.length;

//alert (count);
if (len>10)  
{
 alert("Password length cannot be greater than 10");
 event.cancelBubble = true;
 event.returnValue = false;
}


}

    </script>
 
 
 
 
</head>
<body>
    <form id="form1" runat="server">
   <table border="0" cellspacing="0" cellpadding="0" width="100%" height="100%">
  <tr>
	<td width="50%" background="images/bg.gif"><img src="../images/px1.gif" width="1" height="1" alt="" border="0"></td>
	<td valign="bottom" background="../images/bg_left.gif" style="width: 18px"><img src="../images/bg_left.gif" alt="" width="17" height="16" border="0"></td>
	<td>
	<table width="780" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><img src="../Images/CH2.jpg" height="126" style="width: 776px"></td>
        </tr>
      </table>
     
      <center>
      <table border="0" cellspacing="0" cellpadding="0" width="780" >
          <tr>
              <td align="center" colspan="3" style="background-color: #99cc99">
                  <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Agency FB"
                      Font-Size="20px" Text="Operator Registration for Empaneled Agency " Width="329px"></asp:Label></td>
          </tr>
          <tr>
              <td align="center" colspan="3" valign="top">
                  <asp:GridView ID="gdview_OperatorDist" runat="server" AutoGenerateColumns="False" BackColor="White"
                      BorderColor="#CCCCCC" BorderStyle="Solid" CellPadding="1" Font-Names="Calibri"
                      Font-Size="10pt" Width="224px">
                      <RowStyle ForeColor="#000066" />
                      <Columns>
                          <asp:BoundField DataField="OperatorID" HeaderText="ID">
                              <HeaderStyle BorderStyle="Double" />
                              <ItemStyle BorderStyle="Solid" />
                          </asp:BoundField>
                          <asp:BoundField DataField="OperatorName" HeaderText="Operator Name">
                              <HeaderStyle BorderStyle="Solid" />
                              <ItemStyle BorderStyle="Solid" />
                          </asp:BoundField>
                      </Columns>
                      <FooterStyle BackColor="White" ForeColor="#000066" />
                      <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                      <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                      <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                  </asp:GridView>
              </td>
          </tr>
      <tr>
      <td style="height: 232px; ;" align="center" >
        
      
          <asp:Panel ID="PanelOP_Login" runat="server" Visible="False">
          <table border ="0" cellpadding ="2" cellspacing ="2"  style="font-size:12px; margin-left:30px"> 
            <tr>
 
            
            <td colspan="2" style="background-color: #cccc66; font-size:12px; height: 19px; font-weight: bold;">
            
             Operator Login
           
            </td>
            
            </tr>
           <tr>
             <td align="left" style="width: 90px; height: 28px">
             
              Operator ID  :
                 
            </td>
            
            <td style="height: 28px" align="left">
            
            
                <asp:DropDownList ID="DDLOperetorId" runat="server" Width="142px">
                </asp:DropDownList>
              
            
            
            </td>
         
            </tr>
            
        
            <tr>
             <td align="left" style="width: 90px; height: 28px">
             
              Password :
                 
            </td>
            
            <td style="height: 28px">
            
            
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"  ></asp:TextBox>
            
            
            </td>
         
            </tr>
            
          
            
            <tr>
            <td colspan="2" align="center">
             <hr  style="background-color: #cccc66;  height:5px; " />
            
                <asp:Button ID="btnLogin" runat="server" Text="Login" ForeColor="Blue" BorderColor="#004000" />
                <asp:Button ID="Button1" runat="server" Text="Close" ForeColor="Blue" BorderColor="#004000" OnClick="Button1_Click"  /></td>
            
            </tr>
             <tr>
            <td colspan="2" align="center">
             <hr  style="background-color: #cccc66;  height:5px; " />
            
               <asp:LinkButton ID="linkNewOprator" runat="server" OnClick="linkNewOprator_Click">New Operator Registeration </asp:LinkButton>
            
            
            </td>
            
            </tr>
          
            </table>
          
          
          </asp:Panel>
            
      
       </td>
      <td align="center">
          <asp:Panel ID="PanelMessage" runat="server" >
          
              <asp:Label ID="lblmassage" runat="server" Text="No Operator Exists Please Register Here :" Visible="False"></asp:Label>
              <br />
              <asp:LinkButton ID="LinkbtnnewOp" runat="server"  Visible="False">New Operator Registeration </asp:LinkButton>
              <br />
              <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/MainLogin.aspx" Visible="False"><--Home</asp:HyperLink></asp:Panel>
          &nbsp;<asp:Panel ID="PanelNoOper" runat="server" Visible="False">
              <asp:Label ID="Label7" runat="server" Text="No Operator Exists Please Ask Branch Manager To Add Operator"
                  Width="233px" Visible="False"></asp:Label></asp:Panel>
      
      
      
      
      </td>
      
      <td style="height: 232px ;"  align="center">
      
      
        <asp:Panel ID="PanelOP_Reg" runat="server">
                
          <table border ="0" cellpadding ="2" cellspacing ="2"  style="font-size:12px; margin-right:30px; width: 293px; border-right: olive 2px solid; border-top: olive 2px solid; border-left: olive 2px solid; border-bottom: olive 2px solid;"> 
              <tr>
                  <td align="right" colspan="2">
                      <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/District/Dist_Welcome.aspx">Go Back To Home</asp:LinkButton></td>
              </tr>
              <tr>
                  <td align="left" colspan="2">
                      <asp:Label ID="lblmsg" runat="server" ForeColor="Green" Font-Bold="True"></asp:Label></td>
              </tr>
        
            <tr>
 
            
            <td colspan="2" style="background-color: #cccc66; font-size:12px; height: 19px; font-weight: bold;">
            
             Operator Registration
           
            </td>
            
            </tr>
            <tr>
            <td align="left" style="height: 28px; width: 90px;">
            
            Operator Name :
                 
            </td>
            
            <td style="height: 28px" align="left">
            
            
             <asp:TextBox ID="txtOp_Name" runat="server"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="13px" ForeColor="Red"
                    Text="*"></asp:Label>
            
            
            </td>
         
            </tr>
            <tr>
             <td align="left" style="width: 90px; height: 28px">
            Mobile :
                 
            </td>
            
            <td style="height: 28px" align="left">
            
            
                <asp:TextBox ID="txtOp_Mobile" runat="server"  ></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="13px" ForeColor="Red"
                    Text="*"></asp:Label></td>
         
            </tr>
            
            <tr>
             <td align="left" style="width: 90px">
            Email :
                 
            </td>
            
            <td align="left">
            
            
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="13px" ForeColor="Red"
                    Text="*"></asp:Label></td>
         
            </tr>
            
            <tr>
            <td align="left" style="width: 90px; height: 28px">
           Address :
                 
            </td>
            
            <td style="height: 28px" align="left">
            
            
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            
            
            </td>
            
         
            </tr>
            
            <tr>
            <td align="left" style="width: 90px">
           Branch Manager :
                 
            </td>
            
            <td align="left">
            
            
                <asp:TextBox ID="txtBranchMgr" runat="server"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="13px" ForeColor="Red"
                    Text="*"></asp:Label></td>
         
            </tr>
            
             <tr>
            <td align="left" style="height: 28px; width: 90px;">
             Mobile No:
                 
            </td>
            
            <td style="height: 28px" align="left">
            
            
                <asp:TextBox ID="txtBr_MobileNO" runat="server"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="13px" ForeColor="Red"
                    Text="*"></asp:Label></td>
         
            </tr>
            
             <tr>
            <td align="left" style="width: 90px">
             Office No:
                 
            </td>
            
            <td align="left">
            
            
                <asp:TextBox ID="txtOfficNo" runat="server"></asp:TextBox>
            
            
            </td>
         
            </tr>
            
              <tr>
            <td align="left" style="width: 90px">
             Password:
                 
            </td>
            
            <td align="left">
            
            
                <asp:TextBox ID="txtpwd" runat="server" TextMode="Password"></asp:TextBox>
            
            
            </td>
         
            </tr>
            <tr>
            <td align="left" style="width: 90px">
            Conform Password:
                 
            </td>
            
            <td align="left">
            
            
                <asp:TextBox ID="txtConPwd" runat="server" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtpwd"
                    ControlToValidate="txtConPwd" ErrorMessage="*"></asp:CompareValidator></td>
         
            </tr>
            
            <tr>
            <td colspan="2" align="center">
             <hr  style="background-color: #cccc66;  height:5px; " />
            
                <asp:Button ID="btnSubmit" runat="server" Text="Save" ForeColor="Blue" BorderColor="#004000" OnClick="btnSubmit_Click"  />
               <asp:Button ID="btnCancal" runat="server" Text="Cancel" ForeColor="Blue" BorderColor="#004000"  OnClick="btnCancal_Click" />
            
            </td>
            
            </tr>
          
            </table>
                
                
                </asp:Panel>
          
            
         
       </td> 
            </tr> 
            </table>
            </center>
<div class="px" align="center"><img src="../images/bot01.jpg" width="780" height="9" alt="" border="0"></div>
<table border="0" cellspacing="0" cellpadding="0" width="780" align="center" style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid">
    <tr>
        <td style="height: 19px; font-weight: bold; font-size: 12px; color: green;" align="center">
             Site Designed and Hosted By:</td>
        <td style="height: 19px; font-weight: bold; font-size: 12px; color: green;" align="center">
           Contents Provided By:</td>
    </tr>
<tr>
	<td style="height: 63px"><p>
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Arial Narrow" Font-Size="12px"
            ForeColor="Navy" Height="50px" Width="310px" Font-Bold="True">National Informatics Centre, Madhya Pradesh
Ministry of Communications and Information Technology
Department of Information Technology</asp:HyperLink>&nbsp;</p></td>
	      <td style="height: 63px"> 
            <p class="bot"><b> <a href="">
                <asp:HyperLink ID="HyperLink4" runat="server" Font-Names="Arial Narrow" Font-Size="12px"
                    ForeColor="Navy" Height="40px" Width="338px">Madhya Pradesh State Civil Supplies Corporation Ltd. Block-1, 3rd Floor, Paryavas Bhavan, Jail Road, Bhopal- 462011 India  Phones : 91-755-2551539, Fax :91-755-2551289 Email : mpscsc@sancharnet.in </asp:HyperLink></a></b></p>
	</td>
</tr>
</table>
	<td valign="bottom" background="../images/bg_right.gif"><img src="../images/bg_right.gif" alt="" width="17" height="16" border="0"></td>
	<td width="50%" background="../images/bg.gif"><img src="../images/px1.gif" width="1" height="1" alt="" border="0"></td>
    </tr> 
	</table>
    </form>
</body>

    
</html>

