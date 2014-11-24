<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userAdd.aspx.cs" Inherits="ISPresentationLayer.userAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head><script src="/littlescript.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="/galerie.css"/>
<title>User hinzufügen</title>
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
                <strong><asp:Label ID="user_lbl" runat="server" Text="Label"></asp:Label></strong>
                </td>
			<td id="zelle4" align="right">
			    <asp:ImageButton ID="ausloggen_btn" runat="server" ImageUrl="~/Images/buttons/Ausloggen.png" OnClick="ausloggen_btn_Click" />
            </td>
		</tr>
	</table>
</div>

            <div id="wrapper"><div id="wrapperinternal"><div id="consoleinternal">
                <h3>Steuerungskonsole</h3><br />
                <asp:ImageButton ID="addFolder_btn" runat="server" ImageUrl="~/Images/buttons/NeuesAlbumHinzufeugen.png" OnClick="addFolder_btn_Click" /><br /><br />
                <asp:ImageButton ID="manageUsers_btn" runat="server" ImageUrl="~/Images/buttons/Benutzerverwaltung.png" OnClick="manageUsers_btn_Click" /><br /><br />
                <asp:ImageButton ID="productType_btn" runat="server" ImageUrl="~/Images/buttons/ProduktkategorienVerwalten.png" OnClick="productType_btn_Click"  /><br /><br />
                 <asp:ImageButton ID="ImageButton2" runat="server" alt="Auflösungen verwalten" ImageUrl="~/Images/buttons/AufloesungenVewalten.png" OnClick="resolution_btn_Click"  /><br /><br />
            </div>
 <div id="contentinternal">

		<h1>User hinzufügen</h1><a href="userManagement.aspx">zurück</a><br/><br/><br/><br/>
             <p>
               Benutzername:<br/><br/>
				<asp:TextBox runat="server" ID="Username" type="text" placeholder="Pflichtfeld" /><br/><br/><br/>
			</p>
        	<p>
                Vorname:<br/><br/>
				<asp:TextBox runat="server" ID="Firstname" type="text" placeholder="Pflichtfeld" /><br/><br/><br/>
			</p>
        	<p>
                Nachname:<br/><br/>
				<asp:TextBox runat="server" ID="Lastname" type="text" placeholder="Pflichtfeld" /><br/><br/><br/>
			</p>
        	<p>
                E-Mail Adresse:<br/><br/>
				<asp:TextBox runat="server" ID="EMail" type="text" placeholder="Pflichtfeld" /><br/><br/><br/>
			</p>
   	<p>
                Kennwort:<br/><br/>
				<asp:TextBox runat="server" ID="Password" type="password" placeholder="Pflichtfeld" /><br/><br/><br/>
		  </p>
      	<p>
                Straße:<br/><br/>
				<asp:TextBox runat="server" ID="Address1" type="text" placeholder="Pflichtfeld" /><br/><br/><br/>
		  </p>
      <p>
                Ort:<br/><br/>
				<asp:TextBox runat="server" ID="Address2" type="text" placeholder="Pflichtfeld" /><br/><br/><br/>
		  </p>
       <p>
                PLZ:<br/><br/>
				<asp:TextBox runat="server" ID="Address3" type="text" placeholder="Pflichtfeld" /><br/><br/><br/>
		  </p>
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/buttons/Speichern.png" OnClick="ImageButton1_Click" />
</div>

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