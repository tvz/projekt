<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="zahvala.aspx.cs" Inherits="zahvala" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
<div id="pozadina">
<h1>HVALA VAM NA DONACIJI!</h1> 

<h2>Vi ste BROJAČ NEKI,NEŠ donator na ovome projektu.</h2>
    <h2>
        
   </h2>
        
    <asp:Button ID="Button1" runat="server" CssClass="gumb" 
        PostBackUrl="~/index.aspx" Text="POVRATAK" /> 
        
    
     <br />
     <br />   
    <br />
     <br />
</div>
    </form>
</asp:Content>

