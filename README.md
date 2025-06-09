# Library API

REST API do zarządzania biblioteką: czytelnicy, książki oraz wypożyczenia.


## Opis projektu

API umożliwia zarządzanie:

- Książkami (dodawanie, edycja, usuwanie, pobieranie)  
- Czytelnikami (rejestracja, aktualizacja, usuwanie, pobieranie)  
- Wypożyczeniami książek (tworzenie, zwrot, usuwanie, pobieranie)

Zaimplementowano pełną walidację, obsługę błędów oraz wersjonowanie API (`v1`).

---

## Technologie

- .NET 8.0  
- Entity Framework Core (Code First)  
- SQL Server Express  
- Swagger (OpenAPI)  
- Microsoft.AspNetCore.Mvc.Versioning  
- Microsoft.AspNetCore.JsonPatch (PATCH)  
- Newtonsoft.Json (serializacja JSON)  

---

## Instalacja i uruchomienie

### Wymagania

- .NET 8 SDK  
- SQL Server Express (z Management Studio)  
- PowerShell (do skryptów uruchomieniowych)  

# Uruchom PowerShell jako Administrator

    cd $HOME\Documents

# Klonuj repozytorium
    git clone https://github.com/Senjhin/LibraryAPI
    cd LibraryApi

# Przywróć wszystkie paczki NuGet
    dotnet restore

# (Opcjonalnie, jeśli nie masz EF CLI)
    dotnet tool install --global dotnet-ef

# Zainstaluj zależności NuGet (jeśli nie zostały zaciągnięte automatycznie):
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.AspNetCore.JsonPatch
    dotnet add package Microsoft.AspNetCore.Mvc.Versioning
    dotnet add package Swashbuckle.AspNetCore
    dotnet add package Newtonsoft.Json

# Odpal automatyczny skrypt
    ./run.ps1
