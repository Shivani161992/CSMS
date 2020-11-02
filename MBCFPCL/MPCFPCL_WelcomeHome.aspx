<%@ Page Title="" Language="C#" MasterPageFile="~/MBCFPCL/MPCFPCL_Master.master" AutoEventWireup="true" CodeFile="MPCFPCL_WelcomeHome.aspx.cs" Inherits="MBCFPCL_MPCFPCL_WelcomeHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        .Box
        {
           
            width: 100%;
            padding: 40px;
            background: rgba(204, 207, 208, 0.22);
            border-radius: 10px;
        }

        .surtable
        {
            width: 500px;
            border-top: 3px inset darkgrey;
            border-bottom: 3px inset darkgrey;
            border-left: 3px inset darkgrey;
            border-right: 3px inset darkgrey;
            border-radius: 32px;
            
        }

        .surveLinks
        {
            text-decoration: none;
            font-family: 'Bellefair';
            letter-spacing: 2px;
            font-size: 14px;
        }

            .surveLinks:hover
            {
                text-decoration: dotted;
                color: #ff6b5b;
            }
    </style>
     <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <div class="container">
       <table style=" text-align:center; width:100%;">
           <tr>
               <td>
                    <h2 style="color: #e74c3c; font-size: 35px; font-family: 'Bellefair'; letter-spacing: 4px;">
            <asp:Image ID="Image1" ImageUrl="~/Images/MBCFPCL1.png" CssClass="image" runat="server" /></h2>
               </td>
           </tr>
       </table>
         
           
             
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#home">Operations</a></li>
            <li><a data-toggle="tab" href="#menu1">Reprints</a></li>
            <li><a data-toggle="tab" href="#menu2">Updates</a></li>
            <li><a data-toggle="tab" href="#menu3">Delete</a></li>
            
        </ul>

        <div class="tab-content">
            <div id="home" class="tab-pane fade in active">
               <%-- <h3>Master Reports</h3>--%>
                  <br />
             
                 <div class="Box">
                      <center>
        <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align: center;">Contents</h2>
        <table class="surtable">
            <tr>
                <td style="text-align:left; padding-left:20px;">
                   
                       <br />
                    <li style="text-align:left; color:#404B69; ">
                             <asp:LinkButton ID="LinkButton1" runat="server" CssClass="surveLinks"
                    PostBackUrl="~/MBCFPCL/MPCFPCL_CSC_ChanaMasoor.aspx">Receipt & Acceptance</asp:LinkButton>
                        </li>

                     <li style="text-align:left; color:#404B69; ">
                             <asp:LinkButton ID="LinkButton7" runat="server" CssClass="surveLinks"
                    PostBackUrl="~/MBCFPCL/MPCFPCL_DepositorForm.aspx">Depositor Form</asp:LinkButton>
                        </li>
                      <li style="text-align:left; color:#404B69; ">
                             <asp:LinkButton ID="LinkButton2" runat="server" CssClass="surveLinks"
                    PostBackUrl="~/MBCFPCL/MPCFPCL_FinalDepositer_CSM2018.aspx">Final Depositor Form</asp:LinkButton>
                        </li>
                        <li style="text-align:left; color:#404B69; ">
                             <asp:LinkButton ID="LinkButton6" runat="server" CssClass="surveLinks"
                    PostBackUrl="~/MBCFPCL/MBCFPCL_ChangePassword.aspx">Change Password</asp:LinkButton>
                        </li>

                     <li style="text-align:left; color:#404B69; ">
                             <asp:LinkButton ID="LinkButton3" runat="server" CssClass="surveLinks"
                    PostBackUrl="~/User_Manual/CSM_demo.docx">User Manual</asp:LinkButton>
                        </li>
                         
                       
                    <br />
                </td>
            </tr>
        </table>
                          </center>
    </div>


            </div>
            <div id="menu1" class="tab-pane fade">
                  <br />
                  <div class="Box">
                      <center>
        <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align: center;">Contents</h2>
        <table class="surtable">
            <tr>
                <td style="text-align:left; padding-left:20px;">
                    <br />
                   <%-- <ul style="color: #404B69;">--%>
                       
                      
                         
                       
                   <%-- </ul>--%>
                    <br />
                </td>
            </tr>
        </table>
                          </center>
    </div>
               
               
                

            </div>
            <div id="menu2" class="tab-pane fade">
              
                  
             
                <br />
                  <div class="Box">
                      <center>
        <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align: center;">Contents</h2>
        <table class="surtable">
            <tr>
                <td style="text-align:left; padding-left:20px;">
                    <br />
                  <%--  <ul style="color: #404B69;">--%>
                       
                      
                         
                       
                  <%--  </ul>--%>
                    <br />
                </td>
            </tr>
        </table>
                          </center>
    </div>
            </div>
                
           
            <div id="menu3" class="tab-pane fade">
               <%-- <h3>Bill Reports</h3>
              --%>
                <br />
                  <div class="Box">
                      <center>
        <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align: center;">Contents</h2>
        <table class="surtable">
            <tr>
                <td style="text-align:left; padding-left:20px;">
                    <br />
                  <%--  <ul style="color: #404B69;">--%>
                       
                        
                         
                 <%--      
                    </ul>--%>
                    <br />
                </td>
            </tr>
        </table>
                          </center>
    </div>
            </div>
       
    </div>


</asp:Content>

