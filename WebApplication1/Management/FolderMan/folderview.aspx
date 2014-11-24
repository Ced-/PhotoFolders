<%@ Page Language="C#" AutoEventWireup="true" Inherits="ISPresentationLayer.folderview" Codebehind="folderview.aspx.cs" %>

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
                    <!-- Steuerungskonsole -->
            <div id="wrapper"><div id="wrapperinternal"><div id="consoleinternal" >
                <h3>Steuerungskonsole</h3><br />
                <asp:ImageButton ID="addFolder_btn" runat="server" ImageUrl="~/Images/buttons/NeuesAlbumHinzufeugen.png" OnClick="addFolder_btn_Click" /><br /><br />
                <asp:ImageButton ID="manageUsers_btn" runat="server" ImageUrl="~/Images/buttons/Benutzerverwaltung.png" OnClick="manageUsers_btn_Click" /><br /><br />
                <asp:ImageButton ID="productType_btn" runat="server" ImageUrl="~/Images/buttons/ProduktkategorienVerwalten.png" OnClick="productType_btn_Click"  /><br /><br />
                 <asp:ImageButton ID="ImageButton1" runat="server" alt="Auflösungen verwalten" ImageUrl="~/Images/buttons/AufloesungenVewalten.png" OnClick="resolution_btn_Click"  /><br /><br />
            </div>
        <!--CONTENT -->
         <div id="contentinternal">
            <h1>Willkommen!</h1>
             Sie befinden sich in der Albumansicht des Administrators.<br /><br />


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
                <img id="titelbild" src='<%# ((ISBusinessLayer.ISImage)Eval("Images[0]")).getImageInResolution(new ISBusinessLayer.ISResolution(208,117))%>'><br />
                <img src='/images/public<%# Eval("FolderType")%>.png'>
                <span>Albumname:</span>
               <asp:TextBox class="txt_input" ID='ltb' runat="server" Text='<%#Eval("Foldername")%>'></asp:TextBox> <asp:Button ID="lbn" runat="server" Text="Speichern" FolderID='<%#Eval("ID")%>' onClick="saveFoldername_btn_Click"/><br /><br />
                <asp:ImageButton ID="edit_btn" FolderID='<%#Eval("ID")%>' runat="server" ImageUrl="~/Images/buttons/Bearbeiten.png" onClick="edit_btn_Click"/>
                <asp:ImageButton ID="deleteFolder_btn" FolderID='<%#Eval("ID")%>' runat="server" ImageUrl="~/Images/buttons/Löschen.png" onClick="deleteFolder_btn_Click"/><br / >
                <p style="clear:left"><hr /><br /></p>
                     
                
                </ItemTemplate>

            </asp:ListView>

            
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