<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspection_Registration.aspx.cs" Inherits="PaddyMilling_Inspection__Rice_wheat_Inspection_Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" lang="javascript" src="../Scripts/jquery-2.1.1.js"></script>
    <link href="../calendar/calendar_blue.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../calendar/calendar.js"></script>


    <style>
        body
        {
            background-color: #5D6D7E;
        }

        .column
        {
            height: 60px;
            color: #6B8E23;
            font-family: 'Lucida Fax';
            text-align: center;
            font-size: 35px;
        }

        .tcolumn
        {
            width: 400px;
            text-align: center;
            border-radius: 32px;
            height: 35px;
            font-size: 15px;
            font-family: 'Lucida Fax';
        }

            .tcolumn:focus
            {
                box-shadow: 0 0 25px rgb(107,142,35);
                padding: 3px 0px 3px 3px;
                margin: 5px 1px 3px 0px;
                border: 1px solid rgba(81, 203, 238, 1);
            }

        .tddl
        {
            width: 400px;
            border-radius: 32px;
            height: 35px;
            text-align: center;
        }

        .bttn
        {
            width: 400px;
            height: 50px;
            border-radius: 25px;
            color: #fff!important;
            background-color: #000!important;
            border-radius: 32px;
        }

        .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover
        {
            color: #1B2631!important;
            background-color: #6B8E23!important;
            font-family: 'Lucida Fax';
            font-size: 25px;
        }

        .bttn:active
        {
            background-color: yellow;
        }

        .txt
        {
        }

            .txt:focus
            {
            }

        .table
        {
            background-color: #1B2631;
            width: 500px;
            border-radius: 32px;
            margin-right: 35px;
            text-align: center;
            
        }
    </style>
</head>
<body>
    <div style="width: 100%; height: 45px; background-color: #1B2631; border-radius: 32px;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 20%; height: 45px; text-align: center; color: #6B8E23; font-family: 'Lucida Fax'; font-size: large;">
                    <asp:HyperLink ID="HyperLink3" runat="server" ForeColor="#6B8E23" NavigateUrl="~/PaddyMilling/Inspection(Rice_wheat)/Inspection_Registration.aspx">Register</asp:HyperLink>
                    
                </td>
                <td style="width: 20%; height: 45px; text-align: center; color: #6B8E23; font-family: 'Lucida Fax'; font-size: large;">
                    
                </td>
                <td style="width: 20%; height: 45px; text-align: center; color: #6B8E23; font-family: 'Lucida Fax'; font-size: large;">
                    
                </td>
                <td style="width: 20%; height: 45px; text-align: center; color: #6B8E23; font-family: 'Lucida Fax'; font-size: large;">
                    <asp:HyperLink ID="HyperLink5" runat="server" ForeColor="#6B8E23" NavigateUrl="~/PaddyMilling/Inspection(Rice_wheat)/Inspector_Login.aspx">Approve Inspector</asp:HyperLink>
                 
                </td>
                <td style="width: 20%; height: 45px; text-align: center; color: #6B8E23; font-family: 'Lucida Fax'; font-size: large;">
                    <asp:HyperLink ID="HyperLink2" runat="server" ForeColor="#6B8E23" NavigateUrl="~/PaddyMilling/Inspection(Rice_wheat)/Inspector_Login.aspx">Login</asp:HyperLink>
                
                </td>
            </tr>
        </table>

    </div>
    <form id="form1" runat="server" style="width: 100%;">
        <div style="margin-top: 25px; float: right; width: 100%">


            <table class="table">
                <tr>
                    <td colspan="4" class="column">Inspector Register
               
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center; height: 50px; font-size: large;">
                        <asp:TextBox ID="txtname" runat="server" placeholder="Name" class="tcolumn"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 50px; font-size: large;">
                       <asp:TextBox ID="txtDesig" runat="server" placeholder="Designation" class="tcolumn"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 50px; font-size: large;">
                        <asp:DropDownList ID="ddlDist" runat="server" class="tddl" AutoPostBack="True"></asp:DropDownList>
                    </td>
                </tr>
               
                <tr>
                    <td style="text-align: center; height: 50px; font-size: large;">
                       <asp:TextBox ID="txtDate_of_Joining" runat="server" placeholder="Date Of Joining" class="tcolumn"></asp:TextBox></td>
                        <td>
                         <img src="../calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'txtDate_of_Joining' ,'expiry=false')" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDate_of_Joining" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                    
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 50px; font-size: large;">
                       <asp:TextBox ID="txtEmailID" runat="server" placeholder="Email ID" class="tcolumn"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 50px; font-size: large;">
                       <asp:TextBox ID="txtPasswd" runat="server" placeholder="Password" class="tcolumn"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 50px; font-size: large;">
                       <asp:TextBox ID="txtConfirmPass" runat="server" placeholder="Confirm Passwords" class="tcolumn" CausesValidation="True"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 60px; font-size: large;">

                        <asp:Button ID="bttRegister" runat="server" Text="Register" CssClass="bttn w3-grey" OnClick="bttRegister_Click" /></td>
                </tr>
                <tr>
                    <td colspan="4" class="column"></td>
                </tr>
            </table>
        </div>
        <table border="0" cellspacing="0" cellpadding="0" width="780" align="center" style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid; height: 66px;">
    <tr>
        <td style="height: 10px; font-weight: bold; font-size: 12px; color: White;" align="center">
             Site Designed and Hosted By:</td>
        <td style="height: 10px; font-weight: bold; font-size: 12px; color: White;" align="center">
           Contents Provided By:</td>
    </tr>
<tr>
	<td style="height: 82px; text-align:center"><p>
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Arial Narrow" Font-Size="12px"
            ForeColor="White" Height="47px" Width="320px" Font-Bold="True">National Informatics Centre, Madhya Pradesh
Ministry of Communications and Information Technology
Department of Information Technology</asp:HyperLink>&nbsp;</p></td>
	      <td style="height: 82px; text-align:center;"> 
            <p class="bot"><b> <a href="">
                <asp:HyperLink ID="HyperLink4" runat="server" Font-Names="Arial Narrow" Font-Size="12px"
                    ForeColor="White" Height="35px" Width="338px">Madhya Pradesh State Civil Supplies Corporation Ltd. Block-1, 3rd Floor, Paryavas Bhavan, Jail Road, Bhopal- 462011 India  Phones : 91-755-2768590, Fax :91-755-2677847 Email : mpscsc@bsnl.in </asp:HyperLink></a></b></p>
	</td>
</tr>

    </form>
</body>
</html>
