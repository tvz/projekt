<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" runat="server">
    <h1>
        Najnoviji projekti</h1>
    
    <div runat="server" id="projekti_novi">
        <!--prikaz tri projekta u ravnini-->
        <!--prvi projekt u ravnini-->
        <div runat="server" id="proj" class="proj">
            <h2>
                Ime prvog projekta!</h2>
            <h4>
                PROSTOR ZA SLIKU</h4>
            <h3>
                <b>AUTOR PROJEKTA:</b> Ime Ime</h3>
            <h3>
                <b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit,
                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
            <h3>
                <b>SAKUPLJENO:</b>
            </h3>
            <h3>
                <b>DO KRAJA:</b>
            </h3>
            <asp:Button ID="Button1" runat="server" Text="DONIRAJ!" CssClass="gumb" />
        </div>
        <!--drugi projekt u ravnini-->
        <div class="proj">
            <h2>
                Ime drugog projekta!</h2>
            <h4>
                PROSTOR ZA SLIKU</h4>
            <h3>
                <b>AUTOR PROJEKTA:</b> Ime Ime</h3>
            <h3>
                <b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit,
                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
            <h3>
                <b>SAKUPLJENO:</b>
            </h3>
            <h3>
                <b>DO KRAJA:</b>
            </h3>
            <asp:Button ID="Button2" runat="server" Text="DONIRAJ!" CssClass="gumb" />
        </div>
        <!--treci projekt u ravnini-->
        <div class="proj">
            <h2>
                Ime trećeg projekta!</h2>
            <h4>
                PROSTOR ZA SLIKU</h4>
            <h3>
                <b>AUTOR PROJEKTA:</b> Ime Ime</h3>
            <h3>
                <b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit,
                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
            <h3>
                <b>SAKUPLJENO:</b>
            </h3>
            <h3>
                <b>DO KRAJA:</b>
            </h3>
            <asp:Button ID="Button3" runat="server" Text="DONIRAJ!" CssClass="gumb" />
        </div>
    </div>
    <hr class="druga_lin" />
    <h1>
        Projekti pred istekom vremena za donaciju</h1>
    <div runat="server" id="projekti_stari">
        <!--prikaz tri projekta u ravnini-->
        <!--prvi projekt u ravnini-->
        <div class="proj">
            <h2>
                Ime prvog projekta!</h2>
            <h4>
                PROSTOR ZA SLIKU</h4>
            <h3>
                <b>AUTOR PROJEKTA:</b> Ime Ime</h3>
            <h3>
                <b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit,
                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
            <h3>
                <b>SAKUPLJENO:</b>
            </h3>
            <h3>
                <b>DO KRAJA:</b>
            </h3>
            <asp:Button ID="Button4" runat="server" Text="DONIRAJ!" CssClass="gumb" />
        </div>
        <!--drugi projekt u ravnini-->
        <div class="proj">
            <h2>
                Ime drugog projekta!</h2>
            <h4>
                PROSTOR ZA SLIKU</h4>
            <h3>
                <b>AUTOR PROJEKTA:</b> Ime Ime</h3>
            <h3>
                <b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit,
                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
            <h3>
                <b>SAKUPLJENO:</b>
            </h3>
            <h3>
                <b>DO KRAJA:</b>
            </h3>
            <asp:Button ID="Button5" runat="server" Text="DONIRAJ!" CssClass="gumb" />
        </div>
        <!--treci projekt u ravnini-->
        <div class="proj">
            <h2>
                Ime trećeg projekta!</h2>
            <h4>
                PROSTOR ZA SLIKU</h4>
            <h3>
                <b>AUTOR PROJEKTA:</b> Ime Ime</h3>
            <h3>
                <b>OPIS PROJEKTA:</b> Lorem ipsum dolor sit amet, consectetur adipisicing elit,
                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim</h3>
            <h3>
                <b>SAKUPLJENO:</b>
            </h3>
            <h3>
                <b>DO KRAJA:</b>
            </h3>
            <asp:Button ID="Button6" runat="server" Text="DONIRAJ!" CssClass="gumb" />
        </div>
    </div>
    </form>
</asp:Content>
