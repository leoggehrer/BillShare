# BillShare (Domainname: BillShare)
Beschreibung vom Projekt.  
Das ist eine neue Zeile.  
Eine Auflistung kann wie folgt erstellt werden:  
+ **Schritt1:**
+ **Schritt2:**
+ *Schritt3:*

Ein Progrmmabschnitt kann auch eingefügt werden. Dazu verwende folgende Syntax:  
```csharp
public class Person
{
  public string Fistname { get; set; }
}
```  
## Projektstruktur erstellen
+ **Schritt 1**  
Projektname überlegen und mit diesem Namen eine 'Solution' erstellen  
+ **Schritt 2**  
Eine Klassenbiliothek 'CommonBase' erstellen. In dieser Bibliothek werden alle Algorithmen, welche unabhängig vom Domain-Bereich sind, gesammelt.  
+ **Schritt 3**  
Eine Klassenbibliothek für die Schnittstellen anlegen. Der Projektname wird wie folgt definiert: [Domainname].Contracts.  
+ **Schritt 4**  
Eine Klassenbibliothek für die Geschäftslogik. In diesem Projekt werden alle Geschäftsprozesse gesammelt. Projektname wird wie folgt definert: [Domainname].Logic  
+ **Schritt 5**  
Erstellen einer Konsolenanwendung zum Testen der Struktur. Projektname wird wie folgt definiert: [Domainname].ConApp  
**Hinweis:** Im weiteren Ausbau werden noch weitere Projekte hinzugefügt (z.B.: Rest-Service).  
**Schritt 5**  
Abhänigkeiten definieren.

## Schnittstellen definieren

![Schnittstellen](Contracts.png)