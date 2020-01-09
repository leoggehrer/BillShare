# BillShare (Domainname: BillShare)
Beschreibung vom Projekt.  
Das ist eine neue Zeile.  
Eine Auflistung kann wie folgt erstellt werden:  
+ **Schritt1:**
+ **Schritt2:**
+ *Schritt3:*

Ein Progrmmabschnitt kann auch eingef�gt werden. Dazu verwende folgende Syntax:  
```csharp
public class Person
{
  public string Fistname { get; set; }
}
```  
## Projektstruktur erstellen
+ **Schritt 1**  
Projektname �berlegen und mit diesem Namen eine 'Solution' erstellen  
+ **Schritt 2**  
Eine Klassenbiliothek 'CommonBase' erstellen. In dieser Bibliothek werden alle Algorithmen, welche unabh�ngig vom Domain-Bereich sind, gesammelt.  
+ **Schritt 3**  
Eine Klassenbibliothek f�r die Schnittstellen anlegen. Der Projektname wird wie folgt definiert: [Domainname].Contracts.  
+ **Schritt 4**  
Eine Klassenbibliothek f�r die Gesch�ftslogik. In diesem Projekt werden alle Gesch�ftsprozesse gesammelt. Projektname wird wie folgt definert: [Domainname].Logic  
+ **Schritt 5**  
Erstellen einer Konsolenanwendung zum Testen der Struktur. Projektname wird wie folgt definiert: [Domainname].ConApp  
**Hinweis:** Im weiteren Ausbau werden noch weitere Projekte hinzugef�gt (z.B.: Rest-Service).  
**Schritt 5**  
Abh�nigkeiten definieren.

## Schnittstellen definieren

![Schnittstellen](Contracts.png)