<%@ Page Language="C#" AutoEventWireup="true" Inherits="ISPresentationLayer.login" Codebehind="login.aspx.cs" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><script src="/littlescript.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" href="/galerie.css"/>
    <title>Login - foto.grafik.at</title>
</head>
<body>
    <form id="form1" runat="server">

<div id="header">
	<table>
		<tr>
			<td id="zelle1" align="left">
               <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Images/Logo1.png"/>
			</td>
			<td id="zelle2" align="right" style="width: auto">

			</td>
			<td id="zelle3" align="right" style="width: auto">

                </td>
			<td id="zelle4" align="right" style="width: auto">
			    <img src="../Images/Logo2.png" />
            </td>
		</tr>
	</table>
</div>

        <!--CONTENT -->
        <div id="login_content" align="center" >

            <!-- Login FORM -->
            <div id="login_form"><br />
                <h2>Benutzer Login</h2> <br /> 
               <asp:TextBox class="txt_input1" ID="username" runat="server"></asp:TextBox><br />
               <asp:TextBox class="txt_input1" ID="passwort" type="password" runat="server"></asp:TextBox>
                <br /><br />
                
                <!-- <asp:CheckBox ID="CheckBox1" runat="server" Text="Angemeldet bleiben" />--><br /> 
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/buttons/Login.png" style="margin-top: 10px" OnClick="Unnamed3_Click"/>
               
                <!-- <p style="margin-top: 10px"><a href="" >Als Gast einloggen</a></p> -->
            </div>

        </div>
       

        
        <asp:Panel runat="Server" id="demoshow" Visible="false" style="position:absolute; border-style:solid; border-width:1px; border-color:#000000; top:100px; left:90px; padding: 20px; width:770px; color:#000000; background-color:#ffffff; font-size:10px;">Dies ist eine <span class="mark">Demo Version</span>. Änderungen über das Backend sind in dieser Demo Version nicht durchführbar.<br />Das betrifft ein neues Anlegen und Hochladen von Objekten wie z.B. Alben oder Bildern.<br />Auch das Modifizieren (sowie auch das Löschen) bestehender Objekte ist nicht möglich.<br /><br />Loggen Sie sich für die <span class="mark">Nutzungs des Frontends</span> mit dem <span class="mark">Benutzer "Testuser"</span> und dem <span class="mark">Passwort "abc"</span>ein.<br />Loggen Sie sich für die <span class="mark">Nutzungs des (<b>eingeschränkten</b>) Backends</span> mit dem <span class="mark"> Benutzer "admin"</span> und dem <span class="mark">Passwort "demoDEMO"</span> ein.</asp:Panel>

</div></div><!-- FOOTER -->


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