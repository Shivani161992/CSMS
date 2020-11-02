<%@ Page Title="" Language="C#" MasterPageFile="~/MBCFPCL/MPCFPCL_Master.master" AutoEventWireup="true" CodeFile="MBCFPCL_ChangePassword.aspx.cs" Inherits="MBCFPCL_MBCFPCL_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <style>
           .Box
           {
               position: absolute;
               top: 50%;
               left: 50%;
               transform: translate(-50%, -50%);
               width: 500px;
               padding: 40px;
               background: rgba(204, 207, 208, 0.22);
               border-radius: 10px;
           }

           .surtable
           {
               width: 500px;
               border-top: 6px groove #FFF;
               border-bottom: 6px groove #FFF;
               border-left: 6px groove #FFF;
               border-right: 6px groove #FFF;
               border-radius: 8px;
           }

           .surveLinks
           {
               text-decoration: none;
               font-family: 'Bellefair';
               letter-spacing: 2px;
               font-size: 16px;
           }

           .InspColumn
           {
               width: 33%;
               color: #10321f;
               letter-spacing: 2px;
               font-family: Almendra;
               font-size: 13px;
               font-weight: bold;
           }

           .insptxt
           {
               width: 300px;
               height: 20px;
               letter-spacing: 2px;
               font-family: sans-serif;
               font-size: 12px;
               border-color: #4f84b1;
               border: 1px solid #4f84b1;
               border-radius: 8px;
               color: black;
               padding-left: 10px;
           }

               .insptxt:focus
               {
                   border: none;
                   outline-color: #4f84b1;
               }

           .insfixtext
           {
               width: 310px;
               height: 20px;
               letter-spacing: 2px;
               font-family: sans-serif;
               font-size: 12px;
               border-color: #03a9f4;
               border: 1px solid #03a9f4;
               border-radius: 8px;
               color: black;
               padding-left: 10px;
           }

           .bttsubother
           {
               background: transparent;
               border: none;
               outline: none;
               color: #fff;
               background: #00AAA0;
               padding: 10px 20px;
               cursor: pointer;
               border-radius: 5px;
               letter-spacing: 4px;
               font-family: Almendra;
               height: 30px;
               width: 150px;
           }

               .bttsubother:enabled, button[enabled]
               {
                   background: #e74c3c;
               }

           .sign
           {
               color: #062946;
               font-size: 15px;
               text-decoration: none;
               letter-spacing: 2px;
           }
       </style>
     <table style="width: 100%; font-size: 12px;">
            <tr>
                <td colspan="3">

                </td>
            </tr>
     <center>
        <table style="width: 100%; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 10%">
                    <a href="MPCFPCL_WelcomeHome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 80%">
                    <%--<h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px;">Surveyor Quality Inspection</h2>
                    <input type="hidden" runat="server" id="hdfDist" />--%>
                   
                   
                </td>

                <td style="text-align: right; width: 10%">
                    <a href="MBCFPCL_ChangePassword.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>
        </table>
    </center>
     <div class="Box">
         
         <h2 style="color:#4f84b1; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align:center;">Change Password</h2>
        <table class="surtable" >
             <tr>
                <td style="height:10px;" class="InspColumn" >

                </td>
            </tr>
              <tr>
                <td style="height:10px; text-align: center; color:#e74c3c;"" class="InspColumn" >
                    You have <b><span id="myCounter">10</span></b> characters remaining
                </td>
            </tr>
            <tr>
                
                  <td class="InspColumn" style="padding-left: 90px; text-align: left">Old password
                    <br /> 
                    <asp:TextBox ID="txtoldpassword" CssClass="insptxt" runat="server" MaxLength="10" TextMode="Password" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtoldpassword" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                
                  <td class="InspColumn" style="padding-left: 90px; text-align: left">New Password
                    <br />
                    <asp:TextBox ID="txtnewpassword" MaxLength="10"  CssClass="insptxt" runat="server" TextMode="Password" ></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnewpassword"
                ErrorMessage="New Password Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                     
<asp:RegularExpressionValidator ID="valMin" runat="server" ControlToValidate="txtnewpassword" ErrorMessage="Minimum length is 5!" ValidationExpression=".{5}.*" Display="Dynamic" />

                 
                </td>
            </tr>
             <tr>
                
                  <td class="InspColumn" style="padding-left: 90px; text-align: left">Confirm Password
                    <br />
                    <asp:TextBox ID="txtconpassword" MaxLength="10" CssClass="insptxt" runat="server" TextMode="Password" ></asp:TextBox>
                     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtnewpassword"
        ControlToValidate="txtconpassword" ErrorMessage="Password not Match" ValidationGroup="1">*</asp:CompareValidator></td>
                </td>
            </tr>
            <tr>
                <td style="height:10px;" class="InspColumn" >

                </td>
            </tr>
            <tr>
                <td style="height: 10px;">
                    <center>
                        <asp:Button ID="bttsub" runat="server" Text="Submit" CssClass="bttsubother" Visible="true" Enabled="true" OnClick="bttsub_Click" />
                    </center>
                </td>
            </tr>
            <tr>
                <td style="height:10px;" class="InspColumn" >

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

