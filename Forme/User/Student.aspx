<%@ Page Language="C#" Title="Odjel Računarstvo - Erasmus" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Student.aspx.cs" Inherits="Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <title>Studij</title>    

    <script type="text/javascript" src="../../Scripts/OIB.js"></script>
    <style type="text/css">
        .style5
        {
            height: 29px;
        }
        .style6
        {
            height: 58px;
        }
        .style8
        {
            height: 106px;
        }
        .style9
        {
            height: 58px;
            width: 801px;
        }
    .style10
    {
        height: 29px;
        width: 492px;
    }
    .style11
    {
        width: 492px;
    }
    .style12
    {
        height: 58px;
        width: 492px;
    }
    
    
    
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h2>Studij</h2>

<p>Erasmus stipendija djelomično pokriva troškove smještaja, puta i osiguranja. Iznos troškova smještaja, puta i osiguranja ovisi o stranoj državi, a visina  stipendije o matičnom visokom učilištu. Za podrobnije obavijesti obratite se Vašem Erasmus koordinatoru.</p>

<p>Trajanje mobilnosti ovisi o ciljnoj skupini i aktivnosti. Za studente koji odlaze na  Erasmus studijski boravak ili stručnu praksu, dužina boravka iznosi najmanje 3, a najviše 12 mjeseci. Za nastavno i nenastavno osoblje koje odlazi na Erasmus stručno usavršavanje, dužina boravka iznosi najmanje 5 dana, a najviše 6 tjedana. Nastavnici koji odlaze u inozemnu ustanovu održavati nastavu, mogu ostati najmanje 1 dan, a najviše 6 tjedana s tim da moraju održati najmanje 5 sati nastave.</p>

<p>Student prije odlaska u inozemstvo zaključuje sa svojom ustanovom tzv. Learning Agreement. Student je dužan prije odlaska kontaktirati ECTS koordinatora u svojoj matičnoj kao i inozemnoj ustanovi i s njima zajednički sastaviti popis kolegija koje se obvezuje pohađati odnosno položiti. Ukoliko je student ispunio sve obveze iz dokumenta Learning Agreement, za njega po završetku boravka u inozemstvu ne bi trebalo biti neugodnih iznenađenja.</p>

                <!--AJAX - Accordion (meniji)-->
<div id="Accordion">
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
    SuppressHeaderPostbacks="true" Width="650px">
    <Panes>

        <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Detalji </Header>
            <Content>Lorem ipsum dolor sit amet, et dolores pertinax mea, falli tamquam epicurei nam ei, id inani eirmod animal qui. Pri eu duis laudem. Docendi albucius eu mei. Eam postea aliquip ea, partem utroque admodum an has, viris nullam altera usu no. Vim an suscipit forensibus adversarium, no nonumy consul ocurreret vix. Nullam efficiendi ei eam, ne alii sonet vocibus qui. Illum ridens latine duo ex, graeci mentitum ne qui.

Quo dicam adolescens an. Est agam assentior ex, ei nec sonet offendit. Et quidam nominavi nominati his, quo ne scripta reprehendunt. In est stet indoctum tincidunt, pri ad volutpat recteque pertinacia. Ut utamur fabulas posidonium duo, id nam augue rationibus scriptorem, periculis posidonium eum cu. Sea ne purto ridens, id vim lorem noster.

Insolens constituto at eam. Eleifend assentior forensibus duo an, mel minim mazim labore te. Eam tale fabulas id, corrumpit mnesarchum inciderint has ea. Et mundi ludus accusata sed, alii reprimique vim no, vis et fabulas debitis intellegam. Mei eu meis erant delenit, ornatus delectus interpretaris vix te, dicit diceret theophrastus vis id. Accusamus moderatius his ad.</Content>
        </ajaxToolkit:AccordionPane>        



        <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Forma </Header>
            <Content>
            
                <h2>Prijava na ERASMUS stručni studij</h2>
    <p>Na stručni studij mogu se prijaviti samo studenti od 1. do 3. godine Međimurskog velučilišta u Čakovcu. Studentu će se u inozemstvu pridjeliti njegov Erasmus Buddy pa je poželjno navesti omiljeni hobi. Studentima će se također podijeliti uniforme u svrhu promocije pa se mogu navesti i omiljene boje.</p>
    <hr/>
    <div>  
        <table>
            <tr>
                <td align="right" class="style10">
                    ID
                </td>
                <td class="style5">
                    <asp:TextBox ID="tbId" runat="server" Columns="10" Enabled="False"></asp:TextBox>                        
                </td>
            </tr>
            <tr>
                <td align="right" class="style11">
                    Ime i prezime
                </td>
                <td>
                    <asp:TextBox ID="tbImeIPrezime" runat="server" Columns="30" 
                        ToolTip="Upišite ime i prezime" MaxLength="50" AutoPostBack="True"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="vlImeIPrezime" runat="server" 
                        ControlToValidate="tbImeIPrezime" 
                        ErrorMessage="Ime i prezime su obavezni" ForeColor="Red" Display="Dynamic" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" class="style11">
                    E-mail
                </td>
                <td>
                    <asp:TextBox ID="tbEmail" runat="server" Columns="30" 
                        ToolTip="Upišite e-mail" MaxLength="100" AutoPostBack="True"></asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="vlEmail" 
                        runat="server" ControlToValidate="tbEmail" ErrorMessage="Pogrešan e-mail" 
                        ForeColor="Red" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" class="style10">
                    OIB
                </td>
                <td class="style5">
                    <asp:TextBox ID="tbOIB" runat="server" Columns="11" MaxLength="11" 
                        AutoPostBack="True"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="OIB je obavezan podatak" ControlToValidate="tbOIB" 
                        ForeColor="Red" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="vlOIB" runat="server" ControlToValidate="tbOIB" 
                        ErrorMessage="Pogrešan OIB" ForeColor="Red" 
                        ClientValidationFunction="OIBValidator" Display="Dynamic" 
                        onservervalidate="vlOIB_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td align="right" class="style10">
                    Spol
                </td>
                <td class="style5">                    
                    <asp:RadioButtonList ID="rbSpol" runat="server" RepeatDirection="Horizontal" 
                        AutoPostBack="True">
                        <asp:ListItem Value="Z">Ženski</asp:ListItem>
                        <asp:ListItem Value="M">Muški</asp:ListItem>
                    </asp:RadioButtonList>                    
                </td>
            </tr>  
            <tr>
                <td align="right" class="style12">
                    Adresa stanovanja
                </td>
                <td class="style9">
                    <asp:TextBox ID="tbAdresa" runat="server" Columns="25" MaxLength="11" Rows="3" 
                        TextMode="MultiLine" AutoPostBack="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style10">
                    Godina studija
                </td>
                <td class="style5">
                    <asp:DropDownList ID="ddGodina" runat="server" AutoPostBack="True">
                        <asp:ListItem Selected="True" Value="1">Prva</asp:ListItem>
                        <asp:ListItem Value="2">Druga</asp:ListItem>
                        <asp:ListItem Value="3">Treća</asp:ListItem>
                        <asp:ListItem Value="4">Četvrta</asp:ListItem>
                        <asp:ListItem Value="5">Peta</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;<asp:RangeValidator ID="vlGodina" runat="server" ControlToValidate="ddGodina" 
                        ErrorMessage="Samo prva, druga i treća godina mogu sudjelovati" ForeColor="Red" 
                        MaximumValue="3" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td align="right" class="style11">
                    Omiljeni hobi</td>
                <td class="style8">
                    <asp:ListBox ID="lbHobiji" runat="server" Rows="6" 
                        AutoPostBack="True">
                        <asp:ListItem>Plivanje</asp:ListItem>
                        <asp:ListItem>Trčanje</asp:ListItem>
                        <asp:ListItem>Biciklizam</asp:ListItem>
                        <asp:ListItem>Planinarenje</asp:ListItem>
                        <asp:ListItem>Čitanje</asp:ListItem>
                        <asp:ListItem>Muzika</asp:ListItem>
                        <asp:ListItem>Film</asp:ListItem>
                    </asp:ListBox>
                </td>
            </tr>    
            <tr>
                <td align="right" class="style10">
                    Preferirane boje
                </td>
                <td class="style5">
                    <asp:CheckBoxList ID="cbBoje" runat="server" 
                        RepeatDirection="Horizontal" AutoPostBack="True">
                        <asp:ListItem>Plava</asp:ListItem>
                        <asp:ListItem>Crvena</asp:ListItem>
                        <asp:ListItem>Žuta</asp:ListItem>
                        <asp:ListItem>Zelena</asp:ListItem>
                        <asp:ListItem>Crna</asp:ListItem>
                        <asp:ListItem>Bijela</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>          
        </table>
        <asp:RadioButtonList ID="rbSuglasnost" runat="server" 
            RepeatDirection="Horizontal" style="margin-top: 0px">
            <asp:ListItem Value="1">Suglasan sam</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">Nisam suglasan / da se moji podaci koriste za prijavu</asp:ListItem>
        </asp:RadioButtonList>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToValidate="rbSuglasnost" Display="Dynamic" 
            ErrorMessage="Niste prihvatili uvjete" ForeColor="Red" ValueToCompare="1" 
            EnableClientScript="True"></asp:CompareValidator>
        <br/>
        <asp:Button ID="Button1" runat="server" Text="Obrada" 
            onclick="Button1_Click" />
        <br />
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            HeaderText="Prije spremanja potrebno je popraviti sljedeće:" 
            style="margin-top: 0px" />
        <hr />
        <asp:TextBox ID="tbNapomena" runat="server" BorderStyle="None" Columns="50" 
            Rows="10" TextMode="MultiLine"></asp:TextBox>
    </div>
            
            </Content>
        </ajaxToolkit:AccordionPane>







        <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Kontakt </Header>
            <Content>
            
<div>
<h2>Kontakt osobe</h2>

    <asp:GridView ID="gvImenik" runat="server">
    </asp:GridView>
    <br />
        <asp:HyperLink   
            ID="HyperLink1"   
            runat="server"  
            Text="PDF Download"  
            NavigateUrl="pdf.aspx"  
            >  
        </asp:HyperLink>
</div>
            
            </Content>
        </ajaxToolkit:AccordionPane>       

    </Panes>            
    <HeaderTemplate>...</HeaderTemplate>
    <ContentTemplate>...</ContentTemplate>
</ajaxToolkit:Accordion>
</div>


</asp:Content>