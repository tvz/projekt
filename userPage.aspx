<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="userPage.aspx.cs" Inherits="userPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


      
    


    
  
    
<form id="f1" runat="server">
    
    <br/>
    <br/>
    <asp:Label ID="LabelImeProjekta" runat="server" Text="Ime projekta:"></asp:Label>
    <br />
    <asp:Label ID="LabelOpisProjekta" runat="server" Text="Opis projekta"></asp:Label>
    <br />
    <asp:Label ID="LabelUnesiPromjenu" runat="server" Text="Unesite novi opis projekta"></asp:Label>
    <br />
    


    <asp:TextBox ID="TextBoxPromjena" width="200px" Height="300px" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br /> <br />
    
    <asp:Button ID="ButtonSubmit" runat="server"  onclick="ButtonSubmit_Click" style="height: 26px" Text="Confirm" />
    <asp:Button ID="ButtonNext"  runat="server" onclick="ButtonNext_Click" Text="Next" />
    
</form>
      
    

</asp:Content>




