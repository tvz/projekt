<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="pregledProjekata.aspx.cs" Inherits="pregledProjekata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
    <div id="pozadina">
    <h2>UNESITE PODATKE O PROJEKTIMA KOJE ŽELITE PREGLEDATI:</h2>
    <div class="poravnanje">
    <asp:Label ID="LabelProjectName" runat="server" Text="Ime projekta" 
            CssClass="labele" BorderStyle="None"></asp:Label><br />
    <asp:TextBox ID="TextBoxProjectName" runat="server" CssClass="textbox"></asp:TextBox><br />
    <a id="prikazi_opcije" href="">Prikaži dodatne opcije</a>
    <br />
    </div>
    
    <div class="poravnanjeHide" runat="server" id="vrijednost">
    <asp:Label ID="LabelGoal" runat="server" Text="Vrijednost projekta" 
            CssClass="labele" BorderStyle="None"></asp:Label> <br />
    <asp:TextBox ID="TextBoxGoalStart" runat="server" CssClass="textbox"></asp:TextBox>
    - <asp:TextBox ID="TextBoxGoalEnd" runat="server" CssClass="textbox"></asp:TextBox><br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Moguće je unijeti samo brojeve."
            ValidationExpression="[0-9]+" ControlToValidate="TextBoxGoalStart" 
            CssClass="poruka" Font-Names="Corbel" Font-Size="15px" ForeColor="#D02552" 
            Height="15px" BorderStyle="None" Display="Dynamic"></asp:RegularExpressionValidator><br />
    </div>
            
    <div class="poravnanjeHide" runat="server" id="pocetak">
    <asp:Label ID="LabelCreatedAt" runat="server" Text="Datum početka projekta" 
            CssClass="labele" BorderStyle="None"></asp:Label> <br />
    <asp:TextBox ID="TextBoxCreatedAtStart" runat="server" CssClass="textbox"></asp:TextBox>
    - <asp:TextBox ID="TextBoxCreatedAtEnd" runat="server" CssClass="textbox"></asp:TextBox><br /> <br />
    </div>
    
    <div class="poravnanjeHide" runat="server" id="kraj">
    <asp:Label ID="LabelExpirationDate" runat="server" Text="Datum isteka projekta" 
            CssClass="labele" BorderStyle="None"></asp:Label><br />
    <asp:TextBox ID="TextBoxExpirationDateStart" runat="server" CssClass="textbox"></asp:TextBox>
    - <asp:TextBox ID="TextBoxExpirationDateEnd" runat="server" CssClass="textbox"></asp:TextBox><br /><br />
    </div>
    
      <div class="poravnanjeHide" runat="server" id="sortiranje">
        <asp:Label ID="Label2" runat="server" Text="Sortiraj"  CssClass="labele" 
              BorderStyle="None"></asp:Label><br />
        <asp:DropDownList ID="DropDownListSort" runat="server" AutoPostBack="True" 
            Enabled="False" 
            onselectedindexchanged="DropDownListSort_SelectedIndexChanged" 
            CssClass="textbox" Font-Names="Corbel" Font-Size="15px">
            <asp:ListItem Value="name">Naziv</asp:ListItem>
            <asp:ListItem Value="goal">Vrijednost</asp:ListItem>
            <asp:ListItem Value="createdAt">Datum početka projekta</asp:ListItem>
            <asp:ListItem Value="expirationDate">Datum isteka projekta</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:RadioButton ID="RadioButtonDESC" runat="server" GroupName="sortDirection" 
            Text="Silazno" AutoPostBack="True" Enabled="False" 
            oncheckedchanged="DropDownListSort_SelectedIndexChanged" 
            CssClass="labele" BorderStyle="None" />
        <asp:RadioButton ID="RadioButtonASC" runat="server" GroupName="sortDirection" 
            Text="Uzlazno" AutoPostBack="True" Enabled="False" 
            oncheckedchanged="DropDownListSort_SelectedIndexChanged" 
            CssClass="labele" BorderStyle="None" />
       </div>
              <asp:Button ID="ButtonSearch" runat="server" onclick="ButtonSearch_Click" 
        Text="Pretraži" CssClass="gumb2" />
        <asp:Button ID="ButtonReset" runat="server" 
        Text="Obriši" CssClass="gumb2" onclick="ButtonReset_Click" /> <br />
        <asp:Label ID="LabelSearchResult" runat="server" Text=""
         CssClass="labele" BorderStyle="None"></asp:Label><br /> <br />
   </div>
  <br />
       <div runat="server" id="projekti_search">
    </div>
    
     <div runat="server" id="Div1">
    </div>
   
    </form>
</asp:Content>

