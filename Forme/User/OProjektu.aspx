<%@ Page Title="Odjel Računarstvo - Erasmus" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OProjektu.aspx.cs" Inherits="OProjektu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h2>Informacije</h2>

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
            <Header> Osnovne informacije </Header>
            <Content>
            
                <h2>Osnovni uvjeti</h2>

<p>Svaki student koji odlazi u inozemstvo u okviru programa Erasmus može dobiti mjesečnu financijsku potporu - stipendiju koja će djeomično pokriti troškove života, puta, smještaja i osiguranja.
Dužina studijskog boravka odnosno stručne prakse može iznositi najmanje 3, a najviše 12 mjeseci.
Status državljanina Republike Hrvatske ili neke druge države sudionice Programa (države članice EU, EFTA države + Turska), status izbjeglice, osobe bez državljanstva odnosno registrirano stalno boravište u Republici Hrvatskoj.
</p><p>U trenutku odlaska na mobilnost studenti moraju biti upisani u najmanje 2. godinu preddiplomskog studija.
Mobilnost je moguće ostvariti na visokim učilištima unutar 27 država članica EU.
Visoka učilišta u inozemstvu Erasmus studentima ne smiju naplatiti školarinu niti ostale naknade koje ne plaćaju njihovi matični studenti.</p>

            
            </Content>
        </ajaxToolkit:AccordionPane>        

        <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Dodatne informacije </Header>
            <Content>
<p>Prije odlaska studenti potpisuju sa svojom matičnom ustanovom Ugovor o dodjeli financijske potpore kojim se uređuju međusobna prava i obveze. Ugovor se sastoji od dva privitka:
Learning Agreement / Training Agreement u kojem su određene pojedinosti oko nastavnog plana i programa odnosno programa stručne prakse.</p>
<p>Erasmus studentska povelja (Erasmus Student Charter) u kojoj su navedena prava i obveze studenata vezano uz studijski boravak u inozemstvu.
Studenti su nakon povratka iz inozemne ustanove dužni svojoj matičnoj ustanovi dostaviti sljedeće dokumente: završno izvješće (prema obrascu matične ustanove), prijepis ocjena i potvrdu inozemne ustanove iz koje se vidi točna dužina boravka.</p>
            
            </Content>
        </ajaxToolkit:AccordionPane>

        <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Kontakt </Header>
            <Content>
            
            <h2>Kontakt osobe</h2>

    <asp:GridView ID="gvImenik" runat="server">
    </asp:GridView>
    <br />
    <asp:Button ID="Button1" runat="server" Text="PDF Download" 
        onclick="Button1_Click" />
            
            </Content>
        </ajaxToolkit:AccordionPane>       

    </Panes>            
    <HeaderTemplate>...</HeaderTemplate>
    <ContentTemplate>...</ContentTemplate>
</ajaxToolkit:Accordion>



</asp:Content>

