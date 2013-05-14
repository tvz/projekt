<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="iznosDonacije.aspx.cs" Inherits="iznosDonacije" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form id="form1" runat="server">
<div id="pozadina">
<h1>IME PROJEKTA</h1> <!-- Tu bi trebalo vuc iz baze ime projekta za koji se vrsi donacija -->

<h2>Unesite sumu koju želite donirati:</h2>
    <h2>
        <asp:TextBox ID="TextBox1" runat="server" 
            CssClass="textbox_suma" Font-Size="XX-Large" 
            Height="40px" Width="250px" Font-Bold="True"></asp:TextBox>
        kn
   </h2>
        
   <h5 style="color:#d02552">Odaberite svoju nagradu:</h5> 
   <h5>
       <asp:RadioButton ID="RadioButton1" runat="server" />
       Bez nagrade - Želim samo donirati novac</h5>
   <h5>
       <asp:RadioButton ID="RadioButton2" runat="server" />
       Neki iznos - Tu piše kaj će dobit za taj iznos</h5>
       
    <h5>
       <asp:RadioButton ID="RadioButton3" runat="server" />
       Neki iznos - Tu piše kaj će dobit za taj iznos</h5>
       
    <h5>
       <asp:RadioButton ID="RadioButton4" runat="server" />
       Neki iznos - Tu piše kaj će dobit za taj iznos</h5>
       
    <h5>
       <asp:RadioButton ID="RadioButton5" runat="server" />
       Neki iznos - Tu piše kaj će dobit za taj iznos</h5>
       
    <h5>
       <asp:RadioButton ID="RadioButton6" runat="server" />
       Neki iznos - Tu piše kaj će dobit za taj iznos</h5>  
        
    <asp:Button ID="Button1" runat="server" CssClass="gumb2" Text="DALJE" />
     <br />
     <br />   
</div>
    </form>

</asp:Content>

