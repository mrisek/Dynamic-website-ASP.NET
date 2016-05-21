<%@ Page Title="Odjel Računarstvo - Erasmus" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pregled.aspx.cs" Inherits="Pregled" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h2>Studenti</h2>
<hr />

    <h3>Prijave studenata za ERASMUS stručni studij</h3>
    <asp:TextBox ID="tbTrazi" runat="server"></asp:TextBox>

    <!-- AJAX - BaloonPopupExtender -->
    <ajaxToolkit:BalloonPopupExtender ID="BalloonPopupExtender1" TargetControlID="tbTrazi" UseShadow="true" DisplayOnFocus="true"
    Position="BottomRight" BalloonPopupControlID="Panel2" BalloonStyle="Cloud" runat="server">
    </ajaxToolkit:BalloonPopupExtender>
    <asp:Panel ID="Panel2" runat="server">
    Filtrirajte podatke na osnovu upisanog tekstualnog pojma, prema godini upisa ili prema spolu...
    </asp:Panel>

    <asp:Button ID="btnTrazi" runat="server" Text="Traži" 
        onclick="btnTrazi_Click" />
    <asp:Label ID="Label1" runat="server" style="padding-left: 30px;" Text="Kombinacija uvjeta filtriranja:"></asp:Label>
    <asp:Button ID="Button6" runat="server" onclick="Button6_Click" 
        Text="Uneseni pojam i spol" />
    <asp:Button ID="Button7" runat="server" onclick="Button7_Click" 
        Text="Uneseni pojam i godina" />
    <br />
    <br />
    <asp:RadioButton ID="RadioButton1" Text="Prikaži samo studente" runat="server" 
        GroupName="Filter" BorderStyle="None" />
    <asp:RadioButton ID="RadioButton2" Text="Prikaži samo studentice" 
        runat="server" GroupName="Filter" BorderStyle="None" />
    <asp:Button ID="Button3" runat="server" Text="Filtriraj prema spolu" 
        onclick="Button3_Click" />
    <br />
    <asp:RadioButton ID="RadioButton3" runat="server" BorderStyle="None" 
        GroupName="Filter2" Text="Samo prva godina" />
    <asp:RadioButton ID="RadioButton4" runat="server" BorderStyle="None" 
        GroupName="Filter2" Text="Samo druga godina" />
    <asp:RadioButton ID="RadioButton5" runat="server" BorderStyle="None" 
        GroupName="Filter2" Text="Samo treća godina" />
    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
        Text="Filtriraj prema godini" />
    <br/><br/>
    <asp:GridView ID="gvStudenti" runat="server" 
        AutoGenerateColumns="False" DataKeyNames="id">
        <Columns>
            <asp:BoundField DataField="ime_i_prezime" HeaderText="Ime i prezime" />
            <asp:BoundField DataField="email" HeaderText="e-Mail" />
            <asp:BoundField DataField="oib" HeaderText="OIB" />
            <asp:BoundField DataField="spol" HeaderText="Spol" />
            <asp:BoundField DataField="adresa" HeaderText="Adresa" />
            <asp:BoundField DataField="godina" HeaderText="Godina" />
            <asp:BoundField DataField="hobiji" HeaderText="Hobiji" />
            <asp:BoundField DataField="boje" HeaderText="Boje" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbObrisati" runat="server" onclick="lbObrisati_Click">Obrisati</asp:LinkButton>
                    <asp:ConfirmButtonExtender 
                        ID="cbtObrisati" runat="server" 
                        DisplayModalPopupID="mpeObrisati" 
                        TargetControlID="lbObrisati">
                    </asp:ConfirmButtonExtender>
                    <asp:ModalPopupExtender 
                        ID="mpeObrisati" runat="server" 
                        BackgroundCssClass="Background"                         
                        TargetControlID="lbObrisati" 
                        PopupControlID="pnlObrisati"
                        CancelControlID="btOdustati" 
                        OkControlID="btObrisati">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlObrisati" runat="server" CssClass="Pnl">
                        Želite li obrisati odabranu prijavu?
                        <hr/>
                        <asp:Button ID="btObrisati" runat="server" Text="Obrisati" />
                        &nbsp;
                        <asp:Button ID="btOdustati" runat="server" Text="Odustati" />                        
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#6495ED" Font-Bold="True" ForeColor="#CCCCFF" />
        <RowStyle BackColor="White" ForeColor="#003399" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
</asp:GridView>
<br />

    <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
        Text="Osvježi" />

<asp:Label ID="lbBrojZapisa" runat="server" Text="Broj rezultata:"></asp:Label>

<br />
<br />
<br />

<hr />
<h3>Prijave studenata za ERASMUS stručnu praksu</h3>
<asp:GridView ID="gvTablica" runat="server" AutoGenerateColumns="false">

    <Columns>
        <asp:BoundField DataField="imePraksa" HeaderText="Ime i prezime" />
        <asp:BoundField DataField="emailPraksa" HeaderText="E-mail" />
        <asp:BoundField DataField="oibPraksa" HeaderText="OIB" />
        <asp:BoundField DataField="spolPraksa" HeaderText="Spol" />
        <asp:BoundField DataField="adresaPraksa" HeaderText="Adresa" />
        <asp:BoundField DataField="godinaPraksa" HeaderText="Godina" />
        <asp:BoundField DataField="drzavaPraksa" HeaderText="Država" />
        <asp:BoundField DataField="posaoPraksa" HeaderText="Posao" />
    </Columns>

        <HeaderStyle BackColor="#6495ED" Font-Bold="True" ForeColor="#CCCCFF" />
        <RowStyle BackColor="White" ForeColor="#003399" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />

</asp:GridView>


<br />
    <asp:Button ID="Button8" runat="server" onclick="Button8_Click" 
        Text="Samo prva godina" />
    <asp:Button ID="Button9" runat="server" onclick="Button9_Click" 
        Text="Samo druga godina" />
    <asp:Button ID="Button10" runat="server" onclick="Button10_Click" 
        Text="Samo treća godina" />
    <asp:Button ID="Button11" runat="server" style="margin-left: 30px;" onclick="Button11_Click" 
        Text="Osvježi" />
<hr />
<h3>Download PDF datoteke</h3>
    <asp:Button ID="Button1" runat="server" Text="Prijavljeni na stručnu praksu" 
        onclick="Button1_Click" />
<br />
<asp:Button ID="Button2" runat="server" Text="Prijavljeni na stručni studij" 
        onclick="Button2_Click" />



</asp:Content>

