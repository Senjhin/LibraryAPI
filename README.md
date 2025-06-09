# Library API

REST API do zarządzania biblioteką: czytelnicy, książki oraz wypożyczenia.


## Opis projektu

API umożliwia zarządzanie:

- Książkami (dodawanie, edycja, usuwanie, pobieranie)  
- Czytelnikami (rejestracja, aktualizacja, usuwanie, pobieranie)  
- Wypożyczeniami książek (tworzenie, zwrot, usuwanie, pobieranie)

Zaimplementowano pełną walidację, obsługę błędów oraz wersjonowanie API (`v1`).

---

## Instalacja i uruchomienie

### Wymagania

- .NET 8 SDK  
- SQL Server Express (z Management Studio)  
- PowerShell (do skryptów uruchomieniowych)  

### Instalacja

#### Uruchom PowerShell jako Administrator

    cd $HOME\Documents

#### Klonuj repozytorium
    git clone https://github.com/Senjhin/LibraryAPI
    cd LibraryApi

#### Zainstaluj zależności NuGet (jeśli nie zostały zaciągnięte automatycznie):
    dotnet add package Microsoft.AspNetCore.JsonPatch --version 9.0.5
    dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 8.0.5
    dotnet add package Microsoft.AspNetCore.Mvc.Versioning --version 5.1.0
    dotnet add package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer --version 5.1.0
    dotnet add package Microsoft.AspNetCore.OData --version 9.3.2
    dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.16
    dotnet add package Microsoft.EntityFrameworkCore --version 9.0.5
    dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.5
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.5
    dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.5
    dotnet add package Swashbuckle.AspNetCore --version 8.1.4
    dotnet add package Swashbuckle.AspNetCore.Filters --version 8.0.3
    dotnet add package System.ComponentModel.Annotations --version 5.0.0

#### Skrypt do tworzenia Bazy Danych oraz Migracji
    ./run.ps1

#### Swagger dostepny pod adresem
    http://localhost:5063/swagger/index.html