function detailedMessage()
{   
    errortext = "";
    changetext = true;
    errorcode = document.getElementById("error_meldung").innerHTML;
    switch (errorcode) {
        case "0":
            errortext="Select Statement lieferte keine Ergebnisse";
            break;
        case "1":
            errortext="Es konnte keine Verbindung zur Datenbank aufgebaut werden";
            break;
        case "2":
            errortext="Beim Updaten eines Datensatzes ist ein Fehler aufgetreten!";
            break;
        case "3":
            errortext="Beim Erzeugen eines neuen Datensatzes ist ein Fehler aufgetreten!";
            break;
        case "4":
            errortext="Die zwei Elemente konnten nicht miteinander verbunden werden.";
            break;
        case "5":
            errortext="Datensatz konnte nicht gelöscht werden";
            break;
        case "7":
            errortext="Es wurde eine ID im ungültigen Format (GUID Format beachten) angegeben";
            break;
        case "8":
            errortext="Operation auf Objekt durchgeführt, dass sich nicht im manageAccess Mode befindet, obwohl diese Operation dies erfordert";
            break;
        case "9":
            errortext="Das neue Image konnte nicht erzeugt und an den Ordner angehängt werden";
            break;
        case "10":
            errortext="Das Objekt mit der angeforderten ID ist nicht vorhanden";
            break;
        case "11":
            errortext="Das Objekt konnte nicht gelöscht werden, da nicht alle Referenzen gelöscht werden konnten";
            break;
        case "12":
            errortext="Das Objekt selbst konnte nicht gelöscht werden (meist ausgelöst durch Fehlercode 5)";
            break;
        case "13":
            errortext="Das angegebene Image konnte nicht aus diesem Ordner gelöscht werden.";
            break;
        case "14":
            errortext="Das Bild konnte nicht gespeichert werden";
            break;
        case "15":
            errortext="SCHWERWIEGEND!! Das Bild wurde zunächst in der Datenbank gespeichert, konnte nicht ins FileSystem gespeichert werden, aber danach auch nicht mehr aus der Datenbank entfernt werden.";
            break;
        case "16":
            errortext="Die Berechtigung auf ein Element ist inzwischen nicht mehr verfügbar (NOT USED AT THE MOMENT - implement parent model!)";
            break;
        case "17":
            errortext="Neue Order konnte nicht erzeugt and angehängt werden";
            break;
        case "18":
            errortext="Order konnte nicht gelöscht werden";
            break;
        case "19":
            errortext="Order mit dieser ID konnte nicht gefunden werden";
            break;
        case "20":
            errortext="ManagementAccess Objekt konnte nicht erzeugt werden, da übergebender User nicht die benötigten Rechte besitzt (Permissionlevel 1)";
            break;
        case "21":
            errortext="Folder zu einem Image konnte nicht ermittelt werden.";
            break;
        case "22":
            errortext="LOGIN nicht erfolgreich (Passwort/Username Kombination falsch)";
            break;
        case "23":
            errortext="User hinzufügen: User bereits vorhanden";
            break;
        case "24":
            errortext="Es wurden nicht alle notwendigen Daten eingegeben (Datensatz konnte nicht in die Datenbank gespeichert werden)";
            break;
        case "25":
            errortext="Die hochgeladene Datei ist kein Bild.";
            break;
        case "26":
            errortext="Beim Updaten wurde ein Leerwert angegeben. Kein Update stattgefunden.";
            break;
        case "27":
            errortext="Beim Inserten wurde ein Leerwert angegeben. Kein Insert stattgefunden.";
            break;
        case "28":
            errortext="Es wurde ein negativer Wert eingegeben, wo kein negativer Wert eingegeben werden darf.";
            break;
        case "29":
            errortext="Es wurde ein ungültiger Wert für das PermissionLevel angegeben";
            break;
        case "30":
            errortext="Es wurde ein ungültiger Wert für den FolderType angegeben";
            break;
        case "31":
            errortext="Es wurde versucht ein Mail an eine ungültig formatierte Mailadresse zu versenden";
            break;
        case "32":
            errortext="Das Mail konnte nicht versandt werden";
            break;
        case "33":
            errortext="Die Orders konnten nicht zu einem Mail aufbereitet werden (gibt es möglicherweise keine?)";
            break;
        case "34":
            errortext="Fehler beim Lesen des angeforderten Images";
            break;
        case "38":
            errortext = "Fehler beim Erstellen des ZIP-Files.";
            break;
        case "39":
            errortext = "Fehler beim Erstellen des ZIP-Files.";
            break;
        case "41":
            errortext="Preiseingabe: Der eingegebende Wert ist keine gültige Gleitkommazahl";
            break;
        case "42":
            errortext="Bei Angabe der Höhe/Weite einer Resolution, (beim Permissionlevel) und beim Anlegen eines Folders, sowie bei der Anzahl einer Bestellung: Der angegebene Wert ist keine Ganzzahl.";
            break;
        case "5000":
            errortext = "Bei dieser DEMO-Version sind keine Änderungen möglich.<br>Das schließt auch das Neuanlegen von Objekten wie Alben oder Benutzern ein, sowie das Hochladen von Bildern oder das Löschen von Objekten.";
            break;
        default:
            changetext = false;
            break;
           

    }
    if (changetext)
    {
        document.getElementById("error_meldung").innerHTML = errortext + "(Code: " + errorcode + ")";
    }

}