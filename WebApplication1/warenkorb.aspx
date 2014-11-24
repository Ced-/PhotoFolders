<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="warenkorb.aspx.cs" Inherits="ISPresentationLayer.warenkorb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="/galerie.css"/>
<title>Warenkorb</title>
</head>

<body>
    <form id="form1" runat="server">

<div id="header">
	<table>
		<tr>
			<td id="zelle1" align="left">
				 <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Images/Logo1.png" OnClick="Image1_Click"/>
			</td>
			<td id="zelle2" align="right" style="width: auto">
				
			</td>
			<td id="zelle3" align="right" style="width: auto">
				<img src="/images/User.png" width="15" height="15" alt="User" />
                eingeloggt als:<br/>
                <strong><asp:Label ID="username_lbl" runat="server" Text="Label"></asp:Label></strong>
                </td>
			<td id="zelle4" align="right">
			   <asp:ImageButton ID="ausloggen_btn" runat="server" ImageUrl="~/Images/buttons/Ausloggen.png" OnClick="ausloggen_btn_Click" />
            </td>
		</tr>
	</table>
</div>


        <div id="contentinternal1">
		<h1>Warenkorb</h1>
            <br />
		<hr />
        

    <!--Orders -->
        <asp:ListView ID="orders_listview" runat="server" ItemPlaceholderID="ItemPlaceHolder" GroupPlaceholderID="GroupPlaceHolder">
        <LayoutTemplate>
            <table id="orders">
                <asp:PlaceHolder ID="GroupPlaceHolder" runat="server"></asp:PlaceHolder>

            </table>
        </LayoutTemplate>

        <GroupTemplate>
            <tr>
                <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
            </tr>
        </GroupTemplate>

        <ItemTemplate>
            <td id="zelle7"align:"left" style="width: auto" >
				
                    <strong><%#Eval("ProductType.Name") %></strong>    <br />  
                    <img src='<%# ((ISBusinessLayer.ISImage)Eval("Image")).getImageInResolution(new ISBusinessLayer.ISResolution(208,117)) %>' /> </strong>    <br />
                    Einzelpreis:
                    <%#Eval("ProductType.Price") %>€ <br /><br />
                    
					
					</td>

					<td id="zelle10" align="left"style="width: auto" >
                    	<p>
	                        <strong>Menge:</strong> <br/><br/>
                           <asp:TextBox class="txt_input" ID="rtb" runat="server" Text='<%#Eval("counter") %>'></asp:TextBox>
		                    <h6>Zum Löschen "0" eingeben und "aktualisieren" drücken</h6><br/>
                        </p>
                    </td>
					<td id="Td1" align="left"style="width: auto" >
                        <asp:ImageButton ID="rbn" runat="server" OrderID='<%#Eval("ID") %>' onClick="refresh_btn_Click" ImageUrl="~/images/buttons/aktualisieren.png" />
                    </td>                    
                    <td id="zelle12" align="right"style="width: auto">
                    	<strong>Gesamtpreis:</strong><br/><br/>
                        <asp:Label ID="preis_lbl" runat="server" Text='<%#Eval("DerivedAmountPrice")%>'></asp:Label> €
                   </td> 
                                   </ItemTemplate>   	  
    </asp:ListView>
            <hr />

      
        <br/><br/>
        <div align="right"">
	        Artikelpreis: 
            <asp:Label ID="summe_lbl" runat="server" Text="Summe"></asp:Label> €<br/>
            Versand: 
            <asp:Label ID="versand" runat="server" Text="Summe">,00 €</asp:Label><br/>
            <br/>
	        <b>Gesamtpreis: 
            <asp:Label ID="gesamtsumme_lbl" runat="server" Text="Gesamtsumme"></asp:Label> €<br/>
    	    <h6>inkl. Mwst</h6></b>
        </div>
        <br/><br/>
            <hr /><br/>
        <h4>Lieferdaten:</h4>
       		<table style="width:100%">
				<tr>
					<td id="zelle13" align="left">
                            <h6>Vor- und Nachname:</h6>
                            <asp:Label ID="firstname_txtbox" runat="server" Text=""></asp:Label>
			        </td>
				  <td id="zelle14" align="left">
                            <h6>PLZ, Ort, Hausnummer:</h6>
                   <asp:Label ID="address_txtbox" runat="server" Text=""></asp:Label>
				  </td>
					
				</tr>
			</table>
            <br/>

            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true">
                <asp:ListItem Text="<strong>normaler Versand</strong> 5,00€ zzgl. Verpackung (bis 2kg)<br /><br />" Value="5" Selected="True"></asp:ListItem>
                <asp:ListItem Text="<strong>Expressversand</strong> 9,00€ zzgl. Verpackung (bis 2kg)<br /><br /><br />" Value="9"></asp:ListItem>
            </asp:RadioButtonList>

           
            <asp:ImageButton ID="order_btn" runat="server" ImageUrl="~/images/buttons/BestellungAbschicken.png" onClick="order_btn_Click"/>

</div>
<br />
</div></div><!-- FOOTER -->
<div align="right" id="footer1">
    	<p>
    	  <img src="/images/Logo2.png" width="124" height="39" alt="Photobook-Logo" />
  	  </p>
	
</div>


<div id="footer" align="center">
	<table>
		<tr>
			<td align="left">
				<p style="text-align:left; margin-top: 20px">Copyright 1995-2013, grafik.at - Atelier Hannes Gsell - Werbegrafik & Fotografie - UID: ATU60138023</p>
			</td>
	    	<td align="right">
			    <p style="text-align:right; margin-top: 20px">Sorggasse 5, 3100 St. Pölten, Austria - Tel: +43/664/75003647 - <a href="mailto:office@grafik.at">office@grafik.at</a></p>
			</td>

           
	  </tr>
	</table>
</div>
    
        <!-- ERROR-MELDUNGEN -->
     <asp:Panel ID="error_div" runat="server" Visible="false">Leider ist ein Fehler aufgetreten.<br/>Sie können auf den unten stehenden Fehlercode klicken um detailiertere Informationen zu erhalten.<br/><br/>
            <div onclick="detailedMessage()"><a href=# style="text-decoration:none"><asp:Label ID="error_meldung" runat="server" Text="Error-Meldung"></asp:Label></a></div><br />
            <asp:Button ID="ok_btn" runat="server" Text="OK" onClick="ok_btn_Click" />
     </asp:Panel>

    </form>
</body>
</html>