<%@ Page Title="Odjel Računarstvo - Erasmus" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style7
        {
            width: 212px;
        }
        .style8
        {
            width: 138px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
<br />
<br />
<br />
<br />
<br />


<!-- Panel sa Textboxevima za unos usernamea i passworda -->
<asp:Panel runat="server" ID="panel1" CssClass="PnlLogin">
    <h2>Upišite svoje korisničko ime i zaporku</h2>    
    <table style="width: 304px; margin-left: 120px">
        <tr>
            <td class="style8">Korisničko ime</td>
            <td class="style7"><asp:TextBox ID="tbUsername" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="style8">Zaporka</td>
            <td class="style7"><asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="style8"></td>
            <td class="style7"><asp:Button ID="btLogin" runat="server" Text="Prihvati" 
                    onclick="btLogin_Click" /></td>
        </tr>
    </table></asp:Panel><br/>




        <!-- AJAX - filtriranje textbox-a 
    (korisničko ime - mogu se utipkati samo mala slova,
    brojevi i posebni znakovi osim @ su onemogućeni) -->

    <ajaxToolkit:FilteredTextBoxExtender ID="ouuhohgu" runat="server"
    TargetControlID="tbUsername"         
    FilterType="Custom, LowercaseLetters"
    ValidChars="@." />


        <!-- AJAX - Dodavanje efekta sjene panelu -->
    <ajaxToolkit:DropShadowExtender ID="dse" runat="server"
    TargetControlID="panel1" 
    Opacity=".8" 
    Rounded="true"
    TrackPosition="true" />


    <!-- AJAX - button (OK/Cancel) -->
        <ajaxToolkit:ConfirmButtonExtender ID="cbe"  runat="server"
    TargetControlID="btLogin"
    ConfirmText="UPOZORENJE! Pristup ovoj stranici dozvoljen je samo studentima i profesorima MEV-a Čakovec." />


    <!-- status lozinke -->

    <br /><br /><br />
    <asp:Label ID="lbStatus" runat="server" Text="" EnableViewState="false"></asp:Label>
</asp:Content>

