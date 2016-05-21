<%@ Page Title="Odjel Računarstvo - Erasmus" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<h2>Administracija</h2>



            <!--AJAX - Accordion (meniji)-->

<ajaxToolkit:Accordion
    ID="MyAccordion"
    runat="Server"
    SelectedIndex="0"
    HeaderCssClass="accordionHeader"
    HeaderSelectedCssClass="accordionHeaderSelected"
    ContentCssClass="accordionContent"
    AutoSize="None"
    FadeTransitions="true"
    TransitionDuration="250"
    FramesPerSecond="40"
    RequireOpenedPane="false"
    SuppressHeaderPostbacks="true">
    <Panes>

        <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Administracija korisnika </Header>
            <Content>
            
                <h2>Pregled svih korisnika</h2>

    <asp:GridView ID="gvKorisnici" runat="server">

        <HeaderStyle BackColor="#6495ED" Font-Bold="True" ForeColor="#CCCCFF" />
        <RowStyle BackColor="White" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />

    </asp:GridView>
            
            </Content>
        </ajaxToolkit:AccordionPane>        

        <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Ispis podataka </Header>
            <Content>
            
                <h2>Ispis svih korisnika iz baze podataka</h2>
                <asp:Button ID="Button1" runat="server" Text="PDF Download" 
                onclick="Button1_Click" />
            
            </Content>
        </ajaxToolkit:AccordionPane>     

    </Panes>            
    <HeaderTemplate>...</HeaderTemplate>
    <ContentTemplate>...</ContentTemplate>
</ajaxToolkit:Accordion>




</asp:Content>

