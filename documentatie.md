# HohoCarApp

## Wat houdt mijn project in?
Ik heb een mobiel systeem gebouwd om **auto’s te posten**. Gebruikers (of ik als beheerder) kunnen auto’s **toevoegen**. Elke auto heeft o.a. **merk, model, prijs, bouwjaar en foto**. De lijst is **filterbaar op merk**, er is een **detailpagina**, en alles werkt ook **offline** via een lokale database.

## Hoe het werkt (kort)
- Op de tab **Cars** haal ik de lijst **asynchroon** op (je ziet kort een **spinner**).
- Met de **Picker** filter je op **merk**.
- Tik je op **Details**, dan toon ik **foto, prijs en jaar**.
- Bij **toevoegen/bewerken/verwijderen (CRUD)** ververst de lijst automatisch.
- Zonder internet gebruik ik **lokaal ge­seede data** uit **SQLite**.

## Architectuur in mensentaal
Ik gebruik **MVVM**:
- **Views (XAML)** tonen de data met **bindings** (geen logica).
- **ViewModels** houden de schermstaat bij (`IsBusy`, `Commands`) en praten met **Services**.
- **Services** roepen de **REST-API** aan (`HttpClient`) en/of **SQLite** voor offline.
- Dankzij **Dependency Injection** blijft alles los gekoppeld en testbaar.

## API en starten
Ik start mijn **API** en test de endpoints in **Swagger**.

## SQLite en seeding (offline)
Bij de **eerste opstart** maak ik de tabellen aan en **seed** ik enkele **voorbeeldauto’s** (en desgewenst een **testgebruiker** via de API-zijde), zodat de app **zonder internet** bruikbaar blijft.

## Problemen die ik tegenkwam en hoe ik ze oploste
Ik liep o.a. tegen **verschillende packageversies**, **syntaxfouten**, **onjuiste routes/poorten**, en HTTP-fouten **404/400** aan.  
Ik loste dit op door de **Output-console** (incl. **XAML Binding Failures**) te controleren, **Logcat** te gebruiken op Android, en mijn **routes/poorten** en **pakketversies** recht te zetten.

## Bronnen en verantwoording
Ik heb **ChatGPT uitsluitend gebruikt** voor **XML/XAML-uitleg**, **API-uitleg** en **.NET MAUI-uitleg** (navigatie, DI en structuur). **Alle overige code en implementaties zijn mijn eigen werk.**

Daarnaast heb ik **YouTube-video’s** gevolgd over **MVVM in .NET MAUI** en **API-connecties** met **HttpClient/System.Text.Json**. De concrete links voeg ik zelf toe. Ter inspiratie heb ik o.a. deze bekeken:
- https://www.youtube.com/watch?v=0wyu6EZJGns  
- https://www.youtube.com/watch?v=kyMY7efumJU

Ik heb ook **Stack Overflow** geraadpleegd voor gerichte issues:
- *Binding a ViewModel to data from an API*:  
https://stackoverflow.com/questions/79741507/binding-a-viewmodel-to-data-from-an-api
- *Missing ASP.NET Core/.NET runtimes (runtime/sdk-mismatch)*:  
https://stackoverflow.com/questions/76823304/missing-microsoft-aspnetcore-app-runtime-win-arm-microsoft-netcore-app-runti

## Technologie
Dit project is gebouwd met **.NET MAUI (Android/iOS)**, volgt **MVVM**, communiceert **volledig asynchroon** met een **REST-API**, gebruikt **bindingstechnieken** in **XAML** en ondersteunt **offline** via **SQLite + seeding**.
