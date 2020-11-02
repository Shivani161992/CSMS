<%@ Page Title="" Language="C#" MasterPageFile="~/Millers_Login/MillerMaster.master" AutoEventWireup="true" CodeFile="Miller_Home.aspx.cs" Inherits="Millers_Login_Miller_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
      .table
      {
          width:95%;
          background-color:rgba(248, 242, 242, 0.76);
         
          border-radius:8px;
          box-shadow: 10px 10px 5px rgba(0,0,0,0.5);
      }
      .Coloumn
      {
          text-align:center;
           height:500px;
           font-size:20px;

      }
        .lbl
        {
            /*color:#16a085;*/
              font-family: sans-serif;
              letter-spacing:2px;
                font-size:30px;
        }
  </style>
    
     <div>
     
      <center>
       <table class="table">
           <tr>
               <td class="Coloumn">
                 
                  <a style="color:#e74c3c;">Welcome</a>                   <br />
                   <asp:Label runat="server" Text="Label" id="lblMillerName" class="lbl"></asp:Label>
                  
               </td>
           </tr>
       </table>
          </center>
   </div>
</asp:Content>

