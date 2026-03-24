## Co aplikace dělá
- Každou hodinu stáhne XML z konfigurovatelné URL meteostanice
- Převede XML na JSON a uloží do SQL databáze spolu s datem a časem stažení
- Pokud je stanice offline, uloží prázdný záznam s chybovou zprávou
- Zobrazuje historii záznamů v přehledné tabulce s automatickou aktualizací

## Technologie
- .NET 8, ASP.NET Core MVC
- Entity Framework Core + SQL Server
- BackgroundService pro pravidelné stahování

## Spuštění
1. Naklonujte repozitář
2. Uprav connection string v Program.cs
3. Spusť migraci databáze:
4. Spusť aplikaci:

## Konfigurace
V appsettings.json

## Čas implementace

Implementace zabrala přibližně 6 hodin.
