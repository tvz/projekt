<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="iznosDonacije.aspx.cs" Inherits="iznosDonacije" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form id="form1" runat="server">
<div id="pozadina">
<h1 runat="server" id="h1_ime_projekta">IME PROJEKTA</h1> <!-- Tu bi trebalo vuc iz baze ime projekta za koji se vrsi donacija -->

<h2>Unesite sumu koju želite donirati:</h2>
    <h2>
        <asp:TextBox ID="TextBox1" runat="server" 
            CssClass="textbox_suma" Font-Size="XX-Large" 
            Height="40px" Width="250px" Font-Bold="True"></asp:TextBox>
        kn
   </h2>
        
   <h5 style="color:#d02552">Odaberite svoju nagradu:</h5> 
   <h5>
       <input type="radio" name="iznos" value="1" />
       1 EUR - Bez nagrade - Želim samo donirati novac</h5>
   <h5>
       <input type="radio" name="iznos" value="5" />
       5 EUR - Tu piše kaj će dobit za taj iznos</h5>
       
    <h5>
       <input type="radio" name="iznos" value="10" />
       10 EUR - Tu piše kaj će dobit za taj iznos</h5>
       
    <h5>
       <input type="radio" name="iznos" value="50" />
       50 EUR - Tu piše kaj će dobit za taj iznos</h5>
       
    <h5>
       <input type="radio" name="iznos" value="100"  />
       100 EUR - Tu piše kaj će dobit za taj iznos</h5>
       
    <h5>
       <input type="radio" name="iznos" value="150" />
       150 EUR - Tu piše kaj će dobit za taj iznos</h5>  
        
    <asp:Button ID="Button1" runat="server" CssClass="gumb2" Text="DALJE" 
        onclick="Button1_Click1" />
     <br />
     <br />   
</div>
    </form>

</asp:Content>

