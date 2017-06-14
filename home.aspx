<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: xx-large;
        }
        .newStyle1 {
            font-size: x-large;
            font-weight: bold;
            color: #555555;
        }
        body {
            background-color: red;
        }
       
    </style>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body style="background : #F2F2F2";>
     <nav class="navbar navbar-inverse navbar-fixed-top">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#">DIP</a>
        </div>
        <div id="navbar" class="collapse navbar-collapse">
          <ul class="nav navbar-nav">
            <li class="active"><a href="#">Home</a></li>
            <li><a href="#about">About</a></li>
            <li><a href="#logout">Logout</a></li>
          </ul>
        </div><!--/.nav-collapse -->
      </div>
    </nav>
    <br /><br /><br /> 
    <div class="container">

     

   
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <h1>
    
        <strong><span class="auto-style1">HOME</span></strong></h1>
        <br />
    
        <asp:Label ID="Label2" runat="server" Text="WELCOME" CssClass="newStyle1"></asp:Label>
        <br />
    
    </div>

         <p>
             Product Name
         </p>
        <p>
            <asp:DropDownList ID="DropDownList2" class="dropdown show" runat="server"  AutoPostBack="true">
        </asp:DropDownList>
        </p>

        <p>
            Insurance Type</p>
        <p>
            <asp:DropDownList ID="DropDownList1" runat="server" Height="30px" style="margin-top: 4px" Width="102px">
                <asp:ListItem>Vehicle</asp:ListItem>
                <asp:ListItem>Bulglary</asp:ListItem>
                <asp:ListItem>Fire</asp:ListItem>
                <asp:ListItem>Cargo</asp:ListItem>
                <asp:ListItem>House</asp:ListItem>
                <asp:ListItem>Life Assurance</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            Policy Number </p>
        <p>
            <asp:TextBox ID="TextBox1" class="input-group mb-2 mr-sm-2 mb-sm-0" runat="server"></asp:TextBox>
        </p>
        <asp:RadioButton ID="RadioButton1" runat="server" class="form-check-input" Text="New Business" GroupName="business" />
        <asp:RadioButton ID="RadioButton2" runat="server" class="form-check-input" Text="MTA" GroupName="business" />
        <br /><br />
        <p>

            <asp:Label ID="Label1" runat="server" Text="Upload Excel ?" />
            <asp:RadioButtonList ID="rbHDR" runat="server">
    <asp:ListItem Text = "Yes" Value = "Yes" Selected = "True" >
    </asp:ListItem>
    <asp:ListItem Text = "No" Value = "No"></asp:ListItem>
</asp:RadioButtonList>
<asp:GridView ID="GridView2" runat="server"
OnPageIndexChanging = "PageIndexChanging" AllowPaging = "true">
</asp:GridView>
     <br /><br>        

            <asp:FileUpload ID="FileUpload1" class="form-control-file" runat="server" />
            <br />
            <asp:Button ID="btnUpload" runat="server" Text="Upload"
            OnClick="btnUpload_Click" />
        </p>
        <br /><br />
        <p>
            &nbsp;<asp:Button ID="Button1"  runat="server" Text="Submit" class ="btn btn-primary" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" class ="btn btn-danger" Text="Logout" />
        </p>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="policyId" DataSourceID="SqlDataSource2">
            <Columns>
                <asp:BoundField DataField="policyId" HeaderText="policyId" InsertVisible="False" ReadOnly="True" SortExpression="policyId" />
                <asp:BoundField DataField="policyName" HeaderText="policyName" SortExpression="policyName" />
                <asp:BoundField DataField="policyNo" HeaderText="policyNo" SortExpression="policyNo" />
                <asp:BoundField DataField="business" HeaderText="business" SortExpression="business" />
            </Columns>
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="Data Source=.;Initial Catalog=dip;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [tableDip]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        <br />
        <p><a href="Default.aspx">Logout</a></p>
        </form>
         </div ><!-- /.container -->
    
    <script src="Scripts/jquery-3.1.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

</body>
</html>
