<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PM_MillerMain.aspx.cs" Inherits="Millers_Login_PM_MillerMain" %>

<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body
        {
            background-color: #2c3e50;
        }

        .main
        {
            position: fixed;
            width: 100%;
            background-color: #2c3e50;
            letter-spacing: 2px;
        }

        .header
        {
            color: #fff;
            font-size: 35px;
            font-family: sans-serif;
            text-align: center;
        }

        .menu
        {
            height: 23px;
            box-shadow: 10px 10px 15px rgba(0,0,0,0.5);
        }

        .submenu
        {
            width: 60%;
        }

        .subcolomn
        {
            background: white;
            height: 40%;
            text-align: center;
            font-size: 50px;
            width: 100%;
            border-radius: 50%;
            height: 200px;
            font-family: sans-serif;
        }

            .subcolomn:hover
            {
                text-align: center;
                color: #FFF;
                background-color: #e74c3c;
                font-family: sans-serif;
                letter-spacing: 2px;
                box-shadow: 10px 10px 15px rgba(0,0,0,0.5);
                text-shadow: 2px 2px rgba(0,0,0,0.5);
                border-color: #9ecaed;
                box-shadow: 0 0 10px #9ecaed;
            }

        .middle
        {
            background-color: #2c3e50;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="main">
                <tr class="menu">
                    <td colspan="5" class="header">Madhya Pradesh State Civil Supplies Corporation Ltd.
                    </td>

                </tr>


            </table>
            <br />

            <br />

            <br />
            <br />
            <br />

            <br />
            <br />
            <br />
            <br />
            <br />
            <br />


            <center>
          <table class="submenu" >
              <tr >
                  <td style="width:40%;"  >
                    
                     
                           <asp:Button ID="BttLogin" runat="server" Text="Login" CssClass="subcolomn" OnClick="BttLogin_Click"></asp:Button>
                         
                    
                      </td>
                  <td style="width:20%;">

                  </td>
                     
                   <td style="width:40%;" >
                    
                           <asp:Button ID="bttRegister" runat="server" Text="Register" CssClass="subcolomn" OnClick="bttRegister_Click"></asp:Button>
                     
                  </td>
              </tr>
          </table>
              
      </center>
        </div>
    </form>
</body>
</html>
