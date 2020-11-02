<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login_nafed_form.aspx.cs" Inherits="login_naged_form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- Bootstrap core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="css/small-business.css" rel="stylesheet">

    <!--  <meta name="viewport" content="width=device-width, initial-scale=1">-->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <style>
  .modal-header, h4, .close {
      background-color: #ff00dc;
      color:white !important;
      text-align: center;
      font-size: 30px;
  }
  .modal-footer {
      background-color: #f9f9f9;
  }
  </style>
    <style>
  .modal-header, h4, .close {
      background-color: #b6ff00;
      color:white !important;
      text-align: center;
      font-size: 30px;
  }
  .modal-footer {
      background-color: #f9f9f9;
  }
  </style>
    
</head>
<body>
    <form id="form1" runat="server">
   
      <div class="container" style="margin-top:10px;">
           <a href="http://mpsc.mp.nic.in/csms/Mainlogin.aspx" class="pull-left"><img src="Images/mpscsc_logo.png"></a> 
          <a href="#" class="pull-right"><img src="images/NIC-logo.png"></a> 
          <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        
      </div>

    <div class="container">
      <div class="row my-4">
        <div class="col-lg-8">
  <div id="myCarousel" class="carousel slide" data-ride="carousel">
    <!-- Indicators -->
    <ol class="carousel-indicators">
      <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
      <li data-target="#myCarousel" data-slide-to="1"></li>
      <li data-target="#myCarousel" data-slide-to="2"></li>
    </ol>

    <!-- Wrapper for slides -->
    <div class="carousel-inner">
      <div class="item active">
        <img src="Images/chana.jpg" alt="Chana" style="width:100%;height:100%;">
      </div>

      <div class="item">
        <img src="Images/masoor.jpg" alt="Masoor" style="width:100%;height:100%;">
      </div>
    
      <div class="item">
        <img src="Images/Sarso_farm.JPG" alt="Sarso" style="width:100%;height:100%;">
      </div>
    </div>

    <!-- Left and right controls -->
    <a class="left carousel-control" href="#myCarousel" data-slide="prev">
      <span class="glyphicon glyphicon-chevron-left"></span>
      <span class="sr-only">Previous</span>
    </a>
    <a class="right carousel-control" href="#myCarousel" data-slide="next">
      <span class="glyphicon glyphicon-chevron-right"></span>
      <span class="sr-only">Next</span>
    </a>
  </div>
        </div>
        <div class="col-lg-4">
          <h2><strong>NAFED</strong></h2>
          <p> National Agricultural Cooperative Marketing Federation of India Ltd.(NAFED) was established on the auspicious day of Gandhi Jayanti on 2nd October 1958. Nafed is registered under the Multi State Co-operative Societies Act. Nafed was setup with the object to promote Co-operative marketing of Agricultural Produce to benefit the farmers. Agricultural farmers are the main members of Nafed, who have the authority to say in the form of members of the General Body in the working of Nafed.</p>
           <div class="col-md-4" style="margin-left:85px"> 
                   <button type="button" class="btn btn-info btn-lg" id="myBtn" runat="server">Login</button>
            </div>
        </div>
      </div>
    
      <div class="card text-white bg-secondary my-4 text-center">
        <div class="card-body">
          <p class="text-white m-0">National Agricultural Cooperative Marketing Federation of India Ltd.(NAFED)</p>
        </div>
      </div>

      <div class="row">
        <div class="col-md-4 mb-4">
          <div class="card h-100">
            <div class="card-body">
              <h2 class="card-title">Chana</h2>
              <p class="card-text">The chickpea or chick pea (Cicer arietinum) is a legume of the family Fabaceae, subfamily Faboideae. Its different types are variously known as gram or Bengal gram, garbanzo or garbanzo bean, or Egyptian pea Its seeds are high in protein. It is one of the earliest cultivated legumes: 7,500-year-old remains have been found in the Middle East.In 2016, India produced 64% of the world total of chickpeas.</p>
            </div>
        
          </div>
        </div>
        <!-- /.col-md-4 -->
        <div class="col-md-4 mb-4">
          <div class="card h-100">
            <div class="card-body">
              <h2 class="card-title">Masur</h2>
              <p class="card-text">The lentil (Lens culinaris or Lens esculenta) is an edible pulse. It is a bushy annual plant of the legume family, known for its lens-shaped seeds. It is about 40 cm (16 in) tall, and the seeds grow in pods, usually with two seeds in each.</p>
            </div>
           
          </div>
        </div>
        <!-- /.col-md-4 -->
        <div class="col-md-4 mb-4">
          <div class="card h-100">
            <div class="card-body">
              <h2 class="card-title">Mustard (Sarso)</h2>
              <p class="card-text">Mustard plants are any of several plant species in the genera Brassica and Sinapis in the family Brassicaceae. Mustard seed is used as a spice. Grinding and mixing the seeds with water, vinegar, or other liquids creates the yellow condiment known as prepared mustard. The seeds can also be pressed to make mustard oil, and the edible leaves can be eaten as mustard greens.</p>
            </div>
          </div>

      </div>
      <!-- /.row -->

    </div>

  <div class="modal fade" id="myModal" role="dialog" >
    <div class="modal-dialog" >
    
      <!-- Modal content-->
      <div class="modal-content" style="margin-top:5px">
        <div class="modal-header" style="padding:35px 50px;">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4><span class="glyphicon glyphicon-lock"></span> Login</h4>
        </div>
        <div class="modal-body" style="padding:40px 50px;">
         <%-- <form role="form">--%>
            <div class="form-group">
    <div class="container">
    <div class="row">
        <div>
            <h3 class="text-center login-title" style="font:bold">NAFED</h3>
            <div>
                <img src="images/nafed.jpg" class="rounded mx-auto d-block" alt="NAFED">
                <div class="form-signin">
                    <div class="form-group">
                    <label for="usrname"><span class="glyphicon glyphicon-user"></span> USERNAME</label>
                      <asp:TextBox ID="username" class="form-control" runat="server"  placeholder="Enter UserName..."  Width="375px"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="username" ErrorMessage="Username is required" ValidationGroup="A" required="required"></asp:RequiredFieldValidator>
                     </div>
                    <div class="form-group">
                        <label for="psw"><span class="glyphicon glyphicon-eye-open"></span> PASSWORD</label>
                      <asp:TextBox ID="psw" class="form-control" runat="server" TextMode="Password"  placeholder="Enter password"  Width="375px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="psw" ErrorMessage="Password is required" ValidationGroup="A" required="required"></asp:RequiredFieldValidator>
          
                    </div>
                    
                    <div class="form-group">
                     <label for="psw"><%--<span class="glyphicon glyphicon-eye-open"></span>--%> Captcha</label>
                    <asp:Image ID="Image2" runat="server" Height="55px" ImageUrl="~/Captcha.aspx" Width="186px" />  
                          </div>
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtVerificationCode" class="form-control" Width="375px"></asp:TextBox> 
                   <asp:Label runat="server" ID="lblCaptchaMessage"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtVerificationCode" ErrorMessage="Captcha is required" ValidationGroup="A" required="required"></asp:RequiredFieldValidator>
                  </div>
                    
                    <div class="form-group">
                          <asp:Button ID="btn_submit"  Text="Login" Width="375px" class="btn btn-lg btn-primary btn-block" 
                            Height="50px"  Font-Size="18pt"
                   runat="server" ValidationGroup="A" OnClick="btn_submit_Click"> </asp:Button > 
                        </div>
                    <asp:Button ID="btn_cancel" class="btn btn-danger btn-default pull-right" data-dismiss="modal" Text="Cancel" runat="server"> </asp:Button >   
                </div>
            </div>
        </div>
    </div>
</div>
            </div>
        </div>
      </div>
      
    </div>
  </div> 
    
    <footer class="py-3 bg-dark">
      <div class="container">
        <p class="m-0 text-center text-white"></p>
      </div>
      <!-- /.container -->
    </footer>
        </div>

    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
      <script>
          $(document).ready(function () {
              $("#myBtn").click(function () {
                  $("#myModal").modal();
              });
          });
</script>
    </form>
</body>
</html>
