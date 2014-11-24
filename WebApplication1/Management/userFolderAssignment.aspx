﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userFolderAssignment.aspx.cs" Inherits="ISPresentationLayer.userFolderAssignment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="galerie.css"/>
<title>User Albumzuweisung</title>
</head>

<body>
<div id="header" align="center">
	<table>
		<tr>
			<td id="zelle1" align="left">
				<a href="alben.html"><img src="images/Logo1.png" width="117" height="36" alt="Logo" /></a>
			</td>
			<td id="zelle2" align="right" style="width: auto">
				<input id="suche" type="text" placeholder="Albumsuche" />
			</td>
			<td id="zelle3" align="right" style="width: auto">
				<img src="images/User.png" width="15" height="15" alt="User" />
                eingeloggt als:<br/>
          	    <strong>Max Mustermann</strong>
                </td>
			<td id="zelle4" align="right">
			    <a href="login.html"><img src="images/buttons/Ausloggen.png" width="108" height="41" alt="Ausloggen" /></a>
            </td>
		</tr>
	</table>
</div>

<div id="content" align="center">
  <div id="warenkorb" >
		<h1>User Detailansicht</h1><a href="userverwaltung.html">zurück</a><br/><br/><br/><hr /><br/>
        	<p>
                User:
				<h3>Max Mustermann</h3>
			</p>
        	<p>
                Albumsuche:<br/><br/>
				<input id="name" type="text" placeholder="Mustermann" /><br/><br/>
				<img src="images/buttons/Suchen.png" width="53" height="41" alt="Suchen" /><br/><br/><br/>
			</p>
        	<p>
    <form action="input_checkbox.htm">
              <p><strong>Wählen sie die Alben aus, die für den User sichtbar sein sollen:</strong><br/><br/></p>
              <p>
                <input type="checkbox" name="Album" value="unchecked"> Kroatien Familienfotos<br><br>
                <input type="checkbox" name="Album" value="checked"> Portrait - Manuela<br><br>
                <input type="checkbox" name="Album" value="checked"> Kartrennen ÖTSM Krems<br><br>
                <input type="checkbox" name="Album" value="unchecked"> Kroatien Familienfotos<br><br>
                <input type="checkbox" name="Album" value="checked"> Portrait - Manuela<br><br>
                <input type="checkbox" name="Album" value="checked"> Kartrennen ÖTSM Krems<br><br>
                <input type="checkbox" name="Album" value="unchecked"> Kroatien Familienfotos<br><br>
                <input type="checkbox" name="Album" value="checked"> Portrait - Manuela<br><br>
                <input type="checkbox" name="Album" value="checked"> Kartrennen ÖTSM Krems<br><br>
                <input type="checkbox" name="Album" value="unchecked"> Kroatien Familienfotos<br><br>
                <input type="checkbox" name="Album" value="checked"> Portrait - Manuela<br><br>
                <input type="checkbox" name="Album" value="checked"> Kartrennen ÖTSM Krems<br><br>
              </p>
			</form>
            <br/><br/><br/>
		  </p>
    <img src="images/buttons/Speichern.png" width="216" height="41" alt="Speichern" />


<!-- FOOTER -->

<div align="right">
	<p>
		<img src="images/Logo2.png" width="124" height="39" alt="Photobook-Logo" />
	</p>
</div></div>



<div id="footer" align="center">
	<table>
		<tr>
			<td id="zelle5" align="left">
				<p style="text-align:left; margin-top: 20px">Copyright 1995-2013, grafik.at - Atelier Hannes Gsell - Werbegrafik & Fotografie - UID: ATU60138023</p>
			</td>
	    	<td id="Td1" align="right">
			    <p style="text-align:right; margin-top: 20px">Sorggasse 5, 3100 St. Pölten, Austria - Tel: +43/664/75003647 - <a href="mailto:office@grafik.at">office@grafik.at</a></p>
            </td>
		</tr>
	</table>
</div>

    
        <!-- ERROR-MELDUNGEN -->
     <asp:Panel ID="error_div" runat="server" Visible="false">
            <asp:Label ID="error_meldung" runat="server" Text="Error-Meldung"></asp:Label><br />
            <asp:Button ID="ok_btn" runat="server" Text="OK" onClick="ok_btn_Click" />
     </asp:Panel>



</body>
</html>