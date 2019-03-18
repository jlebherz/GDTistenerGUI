Readme File GDTListenerGUI
Das Programm wurde entwickelt von der IQ-5 GmbH John Lebherz
Copyright John Lebherz
Verwendung auf eigene Gefahr, Ohne Haftung für eventuell entstandene Schäden!

Es wird mindestens .NET Framework 4.5 benötigt!

Verwendung:

GDTListenerGUI.exe [--autostart]

der optionale Parameter --autostart bewirkt, dass die Starttaste nciht gedrückt werden muss z.B. für den Autostart bei Windows geeignet.
!!!WICHTIG!!! Das Programm darf NICHT als Hintergrundprozess ausgeführt werden, da es sonst die GDT Applikation nicht in der GUI starten kann

Settings.ini
[GDTListenerService]
GDTFile="C:\GDT\GDT.txt" 					==> Der GDT Pfad der abgeprüft werden soll, ob die jeweilige GDT Datei vorhanden ist (nur verwendet wenn MoveMode=false)
ApplicationFile="C:\application.exe" 		==> Der Pfad zur Anwendung, die gestartet werden soll, wenn die GDT Datei verfügbar ist
CycleTime=10000 							==> Zeit in Millisekunden, die gewartet werden soll bis die Anwendung wieder nach der GDT Datei sucht
WaitTime=50000								==> Zeit in Millisekunden, die gewartet werden soll nachdem die Anwendung gestartet wurde,  bis wieder nach der GDT Datei gesucht werden soll

MoveMode=false								==> Dieser Modus kann verwendet werden um die GDT Datei z.B. von einem Terminalserver abzuholen und ins lokale GDT Verzeichnis zu verschieben
RemoteGDTFileIN="\\server\GDT\gdt.in"		==> Pfad zur fernen GDT Datei (auch UNC Pfad möglich)
LocalGDTFileIN="C:\GDT\gdt.in"				==> Pfad in den die GDT Datei verschoben werden soll
RemoteGDTFileOut="\\server\GDT\gdt.out"		==> Pfad an den die GDT Antwortdatei verschoben werden soll
LocalGDTFileOut="C:\GDT\gdt.out"			==> Pfad von dem aus GDT Antwortdatei abgeholt werden soll

die settings.ini muss sich im selben Pfad wie die GDTListenerGUI.exe befinden
im selbigen Verzeichnis wird auch eine Logdatei namens log.txt angelegt in welcher nach jedem Durchlauf der aktuelle Zeitstempel geschrieben wird

Es können mehrere Instanzen parallel gestartet werden, allerdings müssen sich die GDTListenerGUI.exe und die settings.ini in je einem eigenen Verzeichnis befinden

Die Anwendung muss lese und Schreibrechte auf die jeweiligen Pfade haben