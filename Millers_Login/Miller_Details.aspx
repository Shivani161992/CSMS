<%@ Page Title="" Language="C#" MasterPageFile="~/Millers_Login/MillerMaster.master" AutoEventWireup="true" CodeFile="Miller_Details.aspx.cs" Inherits="Millers_Login_Miller_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
         .tableSub
      {
          width:99.2%;
          /*background-color:rgba(0,0,0,.8);*/
           background-color:#2c3e50;
           /*border-radius:8px;*/
         
          /*box-shadow: 10px 10px 5px rgba(0,0,0,0.5);*/
      }
          .Coloumnsub
      {
          text-align:center;
           height:490px;
           font-size:20px;

      }
  </style>
      <div>
     
      <center>
       <table class="table">
           <tr>
               <td class="Coloumn">
                 
                 <%--  <center>
                       <table class="tableSub">
                           <tr>
                               <td class="Coloumnsub">

                               </td>
                           </tr>

                       </table>
                   </center>--%>
                 
                  
               </td>
           </tr>
       </table>
          </center>
   </div>
</asp:Content>

