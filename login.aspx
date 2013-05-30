<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
    <br />  
    <form id="form1" runat="server">
    //kod iz facebookove dokumentacije
    <script>
        window.fbAsyncInit = function () {
            FB.init({
            appId: '489540494450683', // App ID Žicalica
        // Channel File, potreban zbog nekih problema s IE-om
        // vama vjerojatno nece delat
        //channelUrl: 'http://localhost:51949/channel.html', 
        //ne znam jel ce vama trenutno delat zbog redirect url-a iz Žicalica fb aplikacije
        //nakon logina, napravite refresh, pa bi vam onaj gornji social plugin like/send...
        //...trebal pokazat fb slike ispod ako je login uspješan                                              
            status: true, // check login status
            cookie: true, // enable cookies to allow the server to access the session
            xfbml: true  // parse XFBML
        });


    // Here we subscribe to the auth.authResponseChange JavaScript event. This event is fired
    // for any authentication related change, such as login, logout or session refresh. This means that
    // whenever someone who was previously logged out tries to log in again, the correct case below 
    // will be handled. 
    FB.Event.subscribe('auth.authResponseChange', function (response) {
        // Here we specify what we do with the response anytime this event occurs. 
        if (response.status === 'connected') {
            // The response object is returned with a status field that lets the app know the current
            // login status of the person. In this case, we're handling the situation where they 
            // have logged in to the app.
        } else if (response.status === 'not_authorized') {
            // In this case, the person is logged into Facebook, but not into the app, so we call
            // FB.login() to prompt them to do so. 
            // In real-life usage, you wouldn't want to immediately prompt someone to login 
            // like this, for two reasons:
            // (1) JavaScript created popup windows are blocked by most browsers unless they 
            // result from direct interaction from people using the app (such as a mouse click)
            // (2) it is a bad experience to be continually prompted to login upon page load.
            FB.login();
        } else {
            // In this case, the person is not logged into Facebook, so we call the login() 
            // function to prompt them to do so. Note that at this stage there is no indication
            // of whether they are logged into the app. If they aren't then they'll see the Login
            // dialog right after they log in to Facebook. 
            // The same caveats as above apply to the FB.login() call here.
            //FB.login();
            //window.location = "http://localhost:51295/TVZ_projekt/index.aspx";
        }
    });
};



/* Here we run a very simple test of the Graph API after login is successful. 
This testAPI() function is only called in those cases. 
function testAPI() {
console.log('Welcome!  Fetching your information.... ');
FB.api('/me', function (response) {
console.log('Good to see you, ' + response.name + '.');
                
});
}
Learn more about options for the login button plugin:
https://developers.facebook.com/docs/reference/plugins/login/ */
</script>

    <!-- za inicijalizaciju fb komponenti -->
    <div id="fb-root"></div>
    
       <!--login -->
    <div class="log-reg">
        
        <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/index.aspx" 
            onauthenticate="Login1_Authenticate" BorderColor="White" 
            FailureText="Žao nam je, Vaša prijava nije bila uspješna. Molimo pokušajte ponovo." 
            Height="297px" LoginButtonText="PRIJAVA" Width="485px" Font-Size="15px" 
            PasswordLabelText="Lozinka:" 
            PasswordRequiredErrorMessage="Trebate unijeti lozinku." 
            RememberMeText="Zapamti me na ovom računalu." TitleText="Prijava" 
            UserNameLabelText="Korisničko ime:" 
            UserNameRequiredErrorMessage="Trebate unijeti korisničko ime." 
            BorderStyle="None">
            <CheckBoxStyle Font-Names="Corbel" Font-Size="15px" ForeColor="#000000" 
                CssClass="zapamtiMe" />
            <TextBoxStyle BorderColor="#D02552" CssClass="textbox" />
            <LoginButtonStyle Font-Names="Corbel" CssClass="gumb2" Font-Size="15px" />
            <ValidatorTextStyle ForeColor="#D02552" />
            <LabelStyle Font-Names="Corbel" CssClass="labele" />
            <FailureTextStyle Font-Names="Corbel" Font-Size="15px" ForeColor="#D02552" 
                CssClass="poruka" />
            <TitleTextStyle CssClass="prijava" />
        </asp:Login>
        <a href="" onclick="popup('povratImeLozinka.aspx')">Zaboravili ste korisničko ime ili lozinku?</a>
        <br />
        <fb:login-button autologoutlink="true" data-perms="email" width="200" size="xlarge" max-rows="1">
        Prijava putem facebooka
        </fb:login-button>
    </div>
    
    <!--registracija -->
    <div class="log-reg">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
        oncreateduser="CreateUserWizard1_CreatedUser" ContinueButtonText="" 
            CancelButtonText="Odustani" 
            CompleteSuccessText="Vaš korisnički račun je uspješno napravljen" 
            CreateUserButtonText="KREIRAJ" Height="455px" Width="423px"       
            DuplicateEmailErrorMessage="Već postoji korisnički račun koji koristi unesenu e-mail adresu." 
            DuplicateUserNameErrorMessage="Molimo unesite drugo korisničko ime." 
            InvalidEmailErrorMessage="Molimo unesite ispravnu e-mail adresu." 
            InvalidAnswerErrorMessage="Molimo unesite drugo sigurnosno pitanje." 
            InvalidPasswordErrorMessage="Minimalna duljina lozinke je: {0}.Ne-alfanumeričkih znakova potrebno: {1}." 
            InvalidQuestionErrorMessage="Unesite drugo sigurnosno pitanje" 
            
            UnknownErrorMessage="Korisnički račun nije kreiran. Molimo pokušajte ponovo." 
            CssClass="poruka" FinishCompleteButtonText="Završi" 
            FinishPreviousButtonText="Natrag" StartNextButtonText="Dalje">
        <FinishCompleteButtonStyle CssClass="gumb" />
        <CompleteSuccessTextStyle CssClass="zapamtiMe" Font-Size="15px" 
            ForeColor="#D02552" />
        <ContinueButtonStyle CssClass="gumb2" />
        <HeaderStyle CssClass="poruka" />
        <CreateUserButtonStyle CssClass="gumb2" />
        <TitleTextStyle CssClass="labele" />
        <FinishPreviousButtonStyle CssClass="gumb2" />
        <StartNextButtonStyle CssClass="gumb2" />
        <CancelButtonStyle CssClass="gumb2" />
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table class="tablica">
                        <tr>
                            <td align="center" colspan="2" class="reg" height="5px">
                                Registriraj se:<br />
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                    CssClass="labele">Korisničko ime:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="Korisničko ime je obavezno." 
                                    ToolTip="User Name is required." ValidationGroup="CreateUserWizard1" 
                                    ForeColor="#D02552" BorderStyle="None">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                                    CssClass="labele">Lozinka:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" 
                                    CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                    ControlToValidate="Password" ErrorMessage="Lozinka je obavezna." 
                                    ToolTip="Password is required." ValidationGroup="CreateUserWizard1" 
                                    ForeColor="#D02552" BorderStyle="None">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                    AssociatedControlID="ConfirmPassword" CssClass="labele">Potvrdi lozinku:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" 
                                    CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                                    ControlToValidate="ConfirmPassword" 
                                    ErrorMessage="Potvrda lozinke je obavezna." 
                                    ToolTip="Confirm Password is required." 
                                    ValidationGroup="CreateUserWizard1" ForeColor="#D02552" BorderStyle="None">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" 
                                    CssClass="labele">E-mail:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Email" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                    ControlToValidate="Email" ErrorMessage="E-mail je obavezan." 
                                    ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1" 
                                    ForeColor="#D02552" BorderStyle="None">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question" 
                                    CssClass="labele">Sigurnosno pitanje:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Question" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" 
                                    ControlToValidate="Question" ErrorMessage="Sigurnosno pitanje je obavezno." 
                                    ToolTip="Security question is required." 
                                    ValidationGroup="CreateUserWizard1" ForeColor="#D02552" BorderStyle="None">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" 
                                    CssClass="labele">Sigurnosni odgovor:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" 
                                    ControlToValidate="Answer" ErrorMessage="Sigurnosni odgovor je obavezan." 
                                    ToolTip="Security answer is required." ValidationGroup="CreateUserWizard1" 
                                    ForeColor="#D02552" BorderStyle="None" Width="16px">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" class="tablica" height="5px">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                    ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                    Display="Dynamic" 
                                    ErrorMessage="Lozinka i potvrda lozinke se moraju poklapati" 
                                    ValidationGroup="CreateUserWizard1" CssClass="poruka" ForeColor="" 
                                    BorderStyle="None"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:#D02552;" class="tablica" height="5px">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
    </form>
    </div>
</asp:Content>

