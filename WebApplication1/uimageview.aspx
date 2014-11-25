<%@ Page Language="C#" AutoEventWireup="true" Inherits="ISPresentationLayer.uimageview" Codebehind="uimageview.aspx.cs" %>

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
<%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> --%>
            <!-- WARENKORB als verlinktes div -->
            <br /><div id="warenkorb">
            zum Warenkorb:<a href="warenkorb.aspx">
            <asp:Image ID="warenkorb_icon" runat="server" ImageUrl="/images/Warenkorb.png" />
                     <%-- %><asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">--%>
                        <%--  <ContentTemplate>--%>  
                    <asp:Label ID="warenkorb_lbl" runat="server" Text="zum Warenkorb"></asp:Label>
                         <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%> </a>
            </div>



            <!--Albumtitel-->
            <h1>Album - <asp:Label ID="albumtitel_lbl" runat="server" Text=""></asp:Label></h1>
            <span>Anzahl:</span> <asp:Label ID="anzahlFotos_lbl" runat="server" Text=""></asp:Label><br /><br />
            <!--Buttons -->
            <asp:ImageButton ID="download_btn" runat="server" ImageUrl="~/Images/buttons/DownloadAlbum.png" style="float:left" OnClick="download_btn_Click"/>

            <span>Auflösung:</span><br />
             <asp:DropDownList ID="resolutions" runat="server" DataTextField="ResolutionDerived" DataValueField="ID">
                    </asp:DropDownList>
            

            <br /> <br /> <br />



          <script>
              //AJAX
              var currImgID;
              var xmlHttp;
              function loadImage(imgID, imgWidth, imgHeight) {
           
                  xmlHttp = new XMLHttpRequest();
                  //location.href = 'AJAXHelper.aspx?reqImageID=' + imgID + '&reqHeight=' + imgHeight + '&reqWidth=' + imgWidth;
                  xmlHttp.open('GET', 'AJAXHelper.aspx?reqImageID=' + imgID + '&reqHeight=' + imgHeight + '&reqWidth=' + imgWidth, true);
                  xmlHttp.send();
                  xmlHttp.onreadystatechange = function ()
                  {
                      resposeAnswr = "";
                      if (xmlHttp.readyState == 4) {
                          resposeAnswr = xmlHttp.responseText;
                          if (resposeAnswr != "") {
                              currImgID = imgID;
                              document.getElementById("theImage").src = resposeAnswr;
                              document.getElementById("imageView").style.visibility = "visible";
                          }
                      }


                  };
          

              }

              function newOrder(pt) {

                  xmlHttp = new XMLHttpRequest();
                  xmlHttp.open('GET', 'AJAXHelper.aspx?reqImageID=' + currImgID + '&productType=' + pt, true);
                  xmlHttp.send();
                  xmlHttp.onreadystatechange = function ()
                  {
                      resposeAnswr = "";
                      if (xmlHttp.readyState == 4) {
                          resposeAnswr = xmlHttp.responseText;
                          document.getElementById("warenkorb_lbl").innerHTML = resposeAnswr;
                      }


                  };
                  return false;
              }
           </script>
           
            
           <%--  <script type="text/javascript">
            function loadImage(imgID, imgHeight, imgWidth)
            {
                var obj = document.getElementById("<%= labelID.ClientID %>");
                obj.value = imgID;
                obj = document.getElementById("<%= labelHeight.ClientID %>");
                obj.value = imgHeight;
                obj = document.getElementById("<%= labelWidth.ClientID %>");
                obj.value = imgWidth;
                __doPostBack("<%= buttonXX.ClientID %>", "");
                document.getElementById("imageView").style.visibility = "visible";
            }
            </script>--%>

            <!--FOTOS -->

                   <asp:ListView ID="images_listview" GroupItemCount="4" runat="server" ItemPlaceholderID="ItemPlaceHolder" GroupPlaceholderID="GroupPlaceHolder">
                     <LayoutTemplate><table id="images"><asp:PlaceHolder ID="GroupPlaceHolder" runat="server"></asp:PlaceHolder></table></LayoutTemplate>


                     <GroupTemplate><tr><asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder></tr></GroupTemplate>


                     <ItemTemplate>
                         <td>
                          <a href="#">
                          <img style="border:none" onclick='loadImage("<%#Eval("ID")%>",640,360)' id='images_imgbtn1' src='<%# ((ISBusinessLayer.ISImage)Container.DataItem).getImageInResolution(new ISBusinessLayer.ISResolution(224,126))%>' />
                            </a>
                              <%-- <asp:ImageButton PostBackUrl="javascript:void(0);" OnClientClick='loadImage(<%#Eval("ID")%>,767,450);return false;' ID='images_imgbtn' runat="server" ImageID='<%#Eval("ID")%>' src='<%# ((ISBusinessLayer.ISImage)Container.DataItem).getImageInResolution(new ISBusinessLayer.ISResolution(383,225))%>' style="width: 300px;" />--%>
                            </td>
                     </ItemTemplate>
                 </asp:ListView>

            <!--SingleImage -->
          
          

            <%--
              <asp:Button ID="buttonXX" runat="server" Text="Button" style="display:none;" OnClick="image_update"/>
            <asp:Label ID="labelID" runat="server" Text="Label" style="display:none;"/>
            <asp:Label ID="labelHeight" runat="server" Text="Label" style="display:none;"/>
            <asp:Label ID="labelWidth" runat="server" Text="Label" style="display:none;"/ >--%>


           <div id="imageView" style="visibility:hidden; background-color:black; background:rgba(0, 0, 0, .85); position:absolute; text-align: center; padding:100px; width: auto; height:auto; left :3px; top: 3px; ">
          <a href="#" onclick="document.getElementById('imageView').style.visibility='hidden';" style="text-decoration:none; color:#ffffff; font-size:18px;">X</a>
               <br /><br /><br /> 
               <!-- muss innerhalb dieses DIVS bleiben!!!:-->
               <img src="/images/empty.png" alt="ImagePlaceholder" id="theImage" />
               
            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
            <%--  <ContentTemplate> --%> 
                 <%--  <asp:Label ID="imageLabel" runat="server" Text=""></asp:Label> --%>
                   
              
            
               <%-- WITH GROUPPLACEHOLDER: <asp:ListView ID="productType_view" runat="server" ItemPlaceholderID="ItemPlaceHolder" GroupItemCount="3" GroupPlaceholderID="GroupPlaceHolder"> --%>
                <asp:ListView ID="productType_view" runat="server" ItemPlaceholderID="ItemPlaceHolder"> 
                    <LayoutTemplate>
                        <div id="productType_lv">
                            <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>    
                        
                        </div>
                    </LayoutTemplate>
           
                   
                    
                    <ItemTemplate><br />
                        <%# Eval("Name")%>, Stückpreis: <%#Eval ("Price") %>€
                        <%-- <asp:Button ID="addToCart_btn" runat="server" Text="zum Warenkorb hinzufügen" onClick="addToCart_Click" TypeID='<%#Eval("ID") %>' /><br /><br />--%>
                            <input type="button" value="zum Warenkorb hinzufügen" onclick='newOrder("<%#Eval("ID") %>");' /><br />
                    </ItemTemplate>
                    
                </asp:ListView><br />
                 <%--    </ContentTemplate>--%>
            <%--  <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="buttonXX" EventName="Click" />
               </Triggers>--%>

                <%-- </asp:UpdatePanel> --%>
                
            </div>
             </div>  
            <br /><br /><br /><br />
            </div></div>
        <!-- FOOTER -->
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