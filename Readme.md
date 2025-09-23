# Exempel RESTful API

Projektet följer en klassisk 3-lagersarkitektur för att separera ansvar, underlätta testning och göra det lättunderhållet.
Ska ses som ett exempel för att förstå hur man bygger lager i C#.

## Arkitektur

```
┌─────────────────────────┐
│   PRESENTATIONSLAGER    │  ← API Kontroller, HTTP hantering
│     (SchoolAPI.API)     │
├─────────────────────────┤
│   AFFÄRSLOGIKLAGER      │  ← Tjänster, affärslogik, validering
│  (SchoolAPI.Business)   │
├─────────────────────────┤
│   DATAÅTKOMSTLAGER      │  ← DbContext, Entity Framework
│    (SchoolAPI.Data)     │
└─────────────────────────┘
│                         │
│   MODELLER/ENTITETER    │  ← DTOs, Entiteter (delad)
│   (SchoolAPI.Models)    │
└─────────────────────────┘
```

## Principer

Varje lager har ett specifikt ansvar och kommunicerar endast med närliggande lager.
Alla beroenden injiceras via konstruktorer, vilket gör koden testbar och löst kopplad.
Direktanvändning av DbContext (inte Generisk Repository mönster)

## Teknologier

- Ramverk: .NET 8
- ORM: Entity Framework Core 8
- Databas: SQL Server
- API: ASP.NET Core Web API
- Dokumentation: OpenAPI
- Mappning: Manuell mappning för enklare förståelse av vad som görs
- Repository: Entity Framework Core 8

## Vad som behövs

- .NET 8 SDK
- Visual Studio 2022 eller VS Code
- SQL Server LocalDB (kommer med Visual Studio)

## För att köra projektet

1. Klona till Visual Studio från GitHub

2. dotnet restore

3. Kontrollera att ConnectionString finns i appsettings.json

   Exempel:
   ```
   "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SchoolDB;Trusted_Connection=true;MultipleActiveResultSets=true"
   ```

4. Skapa databas

   Visual Studio:
   I Package Manager Console: update-database
   (SchoolAPI.Data bör vara valt projekt)

   Visual Studio Code:
   Från solution root:
   dotnet ef database update --project SchoolAPI.Data --startup-project SchoolAPI.API

5. Starta projektet