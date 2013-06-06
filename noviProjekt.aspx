<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="noviProjekt.aspx.cs" Inherits="noviProjekt" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
    
    
        <div id="pozadina">
        <h2>UNESITE PODATKE O SVOM PROJEKTU:</h2>
        <div class="poravnanje">
        <asp:Label ID="Label_ime" runat="server" Text="Ime projekta:" CssClass="labele" 
                BorderStyle="None"></asp:Label>
        </div>
        <div class="poravnanje">
        <asp:TextBox ID="TextBox_name" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox_name"
            ErrorMessage="Morate unijeti ime projekta!" EnableTheming="True" 
                ForeColor="#D02552" BorderStyle="None" CssClass="poruka"></asp:RequiredFieldValidator>
        </div>
        
        
        <div class="poravnanje">
        <asp:Label ID="Label_vrijednost" runat="server" Text="Vrijednost projekta:" 
                CssClass="labele" BorderStyle="None"></asp:Label>
        </div>
               
        <div class="poravnanje">
        <asp:TextBox ID="TextBox_goal" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_goal"
            ErrorMessage="Morate unijeti vrijednost projekta!" BorderStyle="None" 
                ForeColor="#D02552" CssClass="poruka"></asp:RequiredFieldValidator>
            <br />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Vrijednost projekta mora biti iskazana znamenkama!"
            ValidationExpression="[0-9]+" ControlToValidate="TextBox_goal" 
                BorderStyle="None" ForeColor="#D02552" CssClass="poruka"></asp:RegularExpressionValidator>
         </div>
       
        
        <div class="poravnanje">
        <asp:Label ID="Label4" runat="server" Text="Datum isteka projekta:" 
                CssClass="labele" BorderStyle="None"></asp:Label>
         </div>       
        
        <div class="poravnanje">
        <asp:TextBox ID="TextBox_expiration_date" runat="server" CssClass="textbox" 
                Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_expiration_date"
            ErrorMessage="Morate unijeti datum isteka projekta!" BorderStyle="None" 
                ForeColor="#D02552" CssClass="poruka"></asp:RequiredFieldValidator>
        </div>
        
        
        
        <div class="poravnanje">
        <asp:Label ID="Label5" runat="server" Text="URL slike:" CssClass="labele" 
                BorderStyle="None"></asp:Label>
        </div>
        
        <div class="poravnanje">
        <asp:TextBox ID="TextBox_image_path" runat="server" CssClass="textbox" 
                Width="250px"></asp:TextBox>
        </div>
        
        
        
        <div class="poravnanje">
        <asp:Label ID="Label6" runat="server" Text="URL videa:" CssClass="labele" 
                BorderStyle="None"></asp:Label>
        </div>
        
        <div class="poravnanje">
        <asp:TextBox ID="TextBox_video_path" runat="server" CssClass="textbox" 
                Width="250px"></asp:TextBox>
        </div>
        
        
        <div class="poravnanje">
        <asp:Label ID="Label_opis" runat="server" Text="Opis projekta:" CssClass="labele" 
                BorderStyle="None"></asp:Label>
        </div>
        
        <div class="poravnanje">
        <asp:TextBox ID="TextBox_description" runat="server" CssClass="textboxOpis" 
                TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox_description"
            ErrorMessage="Morate unijeti opis projekta!" BorderStyle="None" 
                ForeColor="#D02552" CssClass="poruka"></asp:RequiredFieldValidator>
        </div>
       
        
        
        <asp:Button ID="Button1" runat="server" Text="SPREMI" OnClick="Button1_Click" 
                CssClass="gumb2" />
        <br />
        
        
        &nbsp;
        </div>
        
    </form>
    
</asp:Content>
