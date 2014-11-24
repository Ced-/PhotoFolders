<%@ Page Language="C#" AutoEventWireup="true" Inherits="ISPresentationLayer.ufolderview" Codebehind="ufolderview.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><script src="/littlescript.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Albenansicht - foto.grafik.at</title>
<link rel="stylesheet" href="/galerie.css"/>
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

        <!--CONTENT -->
         <div id="contentinternal1">
            <h1><br />Willkommen!</h1>
             <b>Sie befinden sich hier in der Albumauswahl!</b> <br /><br />Mit dem Button "Ansehen" können Sie sich ihre Albumfotos anschauen, <br />
             herunterladen, entwickeln lassen und auf ein Produkt ihrer Wahl drucken lassen.<br /><br /><br />

            <!-- WARENKORB als verlinktes div -->
            <a href="warenkorb.aspx">
                <div id="warenkorb">
                    <asp:Image ID="warenkorb_icon" runat="server" ImageUrl="/images/Warenkorb.png" />
                    <asp:Label ID="warenkorb_lbl" runat="server" Text="zum Warenkorb"></asp:Label>
               </div>
                </a><br /><br />

            <!--ALBENAUFLISTUNG -->
            <%--Mit Groups: <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="ItemPlaceHolder" GroupItemCount="3" GroupPlaceholderID="GroupPlaceHolder">--%>
            <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="ItemPlaceHolder">
                <LayoutTemplate>
                    <div id="albenauflistung">
                        <%--<asp:PlaceHolder ID="GroupPlaceHolder" runat="server"></asp:PlaceHolder>--%>
                        <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                    </div>
                </LayoutTemplate>
                <%--
                <GroupTemplate>
                    <div>
                        <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                    </div>
                </GroupTemplate>
                    --%>
                <ItemTemplate>
                <hr /><br />
                <img id="titelbild" src="<%# ((ISBusinessLayer.ISImage)Eval("Images[0]")).getImageInResolution(new ISBusinessLayer.ISResolution(208,117))%>"><br /><br />
                <img src="/images/public<%# Eval("FolderType")%>.png" />
                <b> <%# Eval("Foldername")%></b><br />
                <h5><span>Fotos:</span> <%# Eval("DerivedImageCount") %></h5>
                <asp:ImageButton ID="ansehen_btn" AlbumID='<%#Eval("ID") %>' runat="server" ImageUrl="~/Images/buttons/Ansehen.png" OnClick="ansehen_btn_Click" />
                
                <!-- <asp:ImageButton ID="download_btn" runat="server" ImageUrl="~/Images/buttons/Download.png" /><br / >

                <asp:RadioButtonList ID="RadioButtonList_downloads" runat="server">
                     <asp:ListItem>Auslösung: 1920x1080</asp:ListItem>

                </asp:RadioButtonList>   -->
                     
                <p style="clear:left"></p>
                </ItemTemplate>

            </asp:ListView>

            </div>
            <!--<asp:Label ID="albenauflistung" runat="server" Text="albenauflistung"></asp:Label> -->
            



            <!--Aufbau des automatisch generierten Inhaltes
                Das Titelbild 
                    <asp:Image ID="titelbild" runat="server" />

                Anzeige wer das Album sieht; aktueller Benutzer oder öffentlich 
                    <asp:Label ID="freigabe" runat="server" Text="Label"></asp:Label><br />

                Name vom Album
                    <asp:Label ID="albumtitel" runat="server" Text="Label"></asp:Label><br />

                Infos zum Album
                    Albumname: <asp:Label ID="albumtitel2" runat="server" Text="Albumname"></asp:Label><br />
                    Datum: <asp:Label ID="datum" runat="server" Text="Datum"></asp:Label><br />
                    Anzahl der Bilder: <asp:Label ID="anzahl" runat="server" Text="Anzahl"></asp:Label><br />
                    Dateigröße: <asp:Label ID="size" runat="server" Text="Größe"></asp:Label><br />

                Buttons
                    <asp:Button ID="ansehen_btn" runat="server" Text="Ansehen" />
                    <asp:Button ID="download" runat="server" Text="Download" />
                    <asp:RadioButton ID="RadioButton1" runat="server" Text="Auflösung1" /><br />
                    <asp:RadioButton ID="RadioButton2" runat="server" Text="Auflösung2" />
             -->
               
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