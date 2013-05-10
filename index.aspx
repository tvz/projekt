<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href='http://fonts.googleapis.com/css?family=Chango&subset=latin,latin-ext' rel='stylesheet' type='text/css'>
    <style type="text/css">
        #form1
        {
            height: 1952px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="logo">
    Logo
    </div>
    <div id="nav_login">
        <asp:HyperLink ID="RegLink" Font-Underline="False" runat="server" 
            NavigateUrl="~/register.aspx" CssClass="LogLink"><span>Registracija</span></asp:HyperLink>
        <asp:HyperLink ID="LogInLink" Font-Underline="False" runat="server" CssClass="LogLink" 
            NavigateUrl="~/login.aspx">Prijava</asp:HyperLink>
    </div>
    
    
    
    
    <br />
    <br />
    <br />
    <br />
    <br />
    
<!--testiranje promjene sa comit-->  
    
    
    <div id="navigacija">
        <asp:HyperLink ID="IndexLink" Font-Underline="False" runat="server" 
            NavigateUrl="~/index.aspx" CssClass="NavLink">Početna</asp:HyperLink>
        <asp:HyperLink ID="NoviProjLink" Font-Underline="False" runat="server" 
            CssClass="NavLink" NavigateUrl="~/noviProjekt.aspx">Započni projekt</asp:HyperLink>
        <asp:HyperLink ID="PregledProjLink" Font-Underline="False" runat="server" 
            CssClass="NavLink" NavigateUrl="~/pregledProjekata.aspx">Pregled projekata</asp:HyperLink>
        <asp:HyperLink ID="OnamaLink" Font-Underline="False" runat="server" 
            CssClass="NavLink" NavigateUrl="~/Onama.aspx">O nama</asp:HyperLink>
    
    </div>
    
    <hr class="prva_lin" />
    
    <h1>Najnoviji projekti</h1>
    
    <div runat="server" id="projekti_novi">
    <%--prikaz tri projekta u ravnini--%>
    
    <%--prvi projekt u ravnini--%>
    <div id="proj">
    <h2>Ime prvog projekta!</h2>
    <h4>PROSTOR ZA SLIKU</h4>
    <h3><b>AUTOR PROJEKTA:</b> Ime Ime</h3>
    <h3><b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
    <h3><b>SAKUPLJENO:</b> </h3>
    <h3><b>DO KRAJA:</b> </h3>
    
    <asp:Button ID="Button1" runat="server" Text="DONIRAJ!" CssClass="gumb" />
    </div>
    
    <%--drugi projekt u ravnini--%>
    <div id="proj">
    <h2>Ime drugog projekta!</h2>
    <h4>PROSTOR ZA SLIKU</h4>
    <h3><b>AUTOR PROJEKTA:</b> Ime Ime</h3>
    <h3><b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
    <h3><b>SAKUPLJENO:</b> </h3>
    <h3><b>DO KRAJA:</b> </h3>
    
    <asp:Button ID="Button2" runat="server" Text="DONIRAJ!" CssClass="gumb" />
    </div>
    
   
    <%--treci projekt u ravnini--%>
    <div id="proj">
    <h2>Ime trećeg projekta!</h2>
    <h4>PROSTOR ZA SLIKU</h4>
    <h3><b>AUTOR PROJEKTA:</b> Ime Ime</h3>
    <h3><b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
    <h3><b>SAKUPLJENO:</b> </h3>
    <h3><b>DO KRAJA:</b> </h3>
    
    <asp:Button ID="Button3" runat="server" Text="DONIRAJ!" CssClass="gumb" />
    </div>
    </div>
   <hr class="druga_lin" />
    
    <h1>Projekti pred istekom vremena za donaciju</h1>
    
    <div runat="server" id="projekti_stari">
    <%--prikaz tri projekta u ravnini--%>
    
    <%--prvi projekt u ravnini--%>
    <div id="proj">
    <h2>Ime prvog projekta!</h2>
    <h4>PROSTOR ZA SLIKU</h4>
    <h3><b>AUTOR PROJEKTA:</b> Ime Ime</h3>
    <h3><b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
    <h3><b>SAKUPLJENO:</b> </h3>
    <h3><b>DO KRAJA:</b> </h3>
    
    <asp:Button ID="Button4" runat="server" Text="DONIRAJ!" CssClass="gumb" />
    </div>
    
    <%--drugi projekt u ravnini--%>
    <div id="proj">
    <h2>Ime drugog projekta!</h2>
    <h4>PROSTOR ZA SLIKU</h4>
    <h3><b>AUTOR PROJEKTA:</b> Ime Ime</h3>
    <h3><b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
    <h3><b>SAKUPLJENO:</b> </h3>
    <h3><b>DO KRAJA:</b> </h3>
    
    <asp:Button ID="Button5" runat="server" Text="DONIRAJ!" CssClass="gumb" />
    </div>
    
   
    <%--treci projekt u ravnini--%>
    <div id="proj">
    <h2>Ime trećeg projekta!</h2>
    <h4>PROSTOR ZA SLIKU</h4>
    <h3><b>AUTOR PROJEKTA:</b> Ime Ime</h3>
    <h3><b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
    <h3><b>SAKUPLJENO:</b> </h3>
    <h3><b>DO KRAJA:</b> </h3>
    
    <asp:Button ID="Button6" runat="server" Text="DONIRAJ!" CssClass="gumb" />
    </div>
    </div>
    
    
    </form>
</body>
</html>
