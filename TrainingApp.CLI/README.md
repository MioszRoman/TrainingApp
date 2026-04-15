# 🏋️‍♂️ TrainingApp

Aplikacja konsolowa w C# do zarządzania planami treningowymi, wykonywania treningów oraz analizy historii i statystyk.

## 📌 Opis projektu

TrainingApp to aplikacja CLI napisana w .NET, która pozwala użytkownikowi:

- tworzyć plany treningowe,
- edytować i usuwać plany,
- wykonywać treningi krok po kroku,
- zapisywać historię treningów,
- analizować statystyki treningowe.

Projekt został stworzony w celach edukacyjnych, z naciskiem na:

- czystą strukturę kodu,
- rozdzielenie logiki i UI,
- pracę z bazą danych (EF Core),
- logikę biznesową i integralność danych.

---

## 💡 Jak działa aplikacja

Użytkownik tworzy plan treningowy składający się z ćwiczeń i obwodów.  
Następnie może rozpocząć trening, przechodząc przez kolejne serie i przerwy.  

Po zakończeniu treningu wynik zapisywany jest do bazy danych przy użyciu Entity Framework Core,  
a dane mogą być później analizowane w sekcji statystyk.

---

## 🚀 Funkcjonalności

### 📋 Zarządzanie planami
- dodawanie planów treningowych
- edycja planów wraz z możliwością wyboru czy chcemy edytować ćwiczenia
- usuwanie planów
- wyświetlanie planów (z ćwiczeniami i bez)

### 🏃 Wykonywanie treningu
- przechodzenie przez ćwiczenia i serie
- liczenie czasu trwania treningu
- podsumowanie po zakończeniu

### 📜 Historia treningów
- zapis historii do bazy danych
- wyświetlanie wszystkich treningów
- filtrowanie po dacie
- wyszukiwanie po ID planu
- usuwanie wpisów historii

### 📊 Statystyki
- liczba treningów
- łączny czas treningów
- średni czas treningu
- najdłuższy i najkrótszy trening
- najczęściej wykonywany plan
- najdłuższy trening w ostatnim tygodniu
- liczba wykonań poszczególnych planów

### Logika biznesowa
Aplikacja zawiera podstawowe zasady biznesowe:
- Nie można usunąć planu, jeśli istnieją wpisy historii dla tego planu
- relacja **Plan (1) → (N) Cwiczenia**
- relacja **Plan (1) → (N) HistoriaTreningu**
- spójność danych między planami i historią

---

## 🛠️ Technologie

- C#
- .NET
- Entity Framework Core 
- SQLite
- Linq

---

## Baza danych
Aplikacja korzysta z bazy SQLite zarządzanej przez Entity Framework Core.

### Tabele:
- `Plany`
- `Cwiczenia`
- `HistoriaTreningow`

### Relacje:
- **Plan (1) → (N) Cwiczenia**
- **Plan (1) → (N) HistoriaTreningu**

---

## 🧱 Struktura projektu
```text
TreningApp/
├── Models/ # modele danych (Plan, Historia, Statystyki itd.)
├── Services/ # logika biznesowa (PlanService, HistoriaService, StatystykiService)
├── UI/ # ConsoleRenderer (obsługa wyświetlania)
├── Helpers/ # InputHelper (obsługa inputu i walidacji)
├── Data/ # Konfiguracja bazy danych (DbContext)
├── Migrations/ # pliki migracyjne EF Core
├── Program.cs # punkt wejścia aplikacji
```

---

## ▶️ Jak uruchomić

1. Sklonuj repozytorium:
```bash
git clone https://github.com/MioszRoman/TrainingApp.git
```
2. Przejdź do folderu projektu:
```bash
cd TrainingApp
```
3. Zainstaluj zależności:
```bash
dotnet restore
```
4. Utwórz bazę danych:
```bash
dotnet ef database update
```
5. Uruchom aplikację:
```bash
dotnet run
```

---

## ⚠️ Wymagania
- .NET SDK (zalecane .NET 8 lub nowszy)

---

## 📈 Etapy rozwoju
### ✅ Etap 1 (Zakończony)
- aplikacja konsolowa (CLI)
- zapis danych do JSON
- historia i statystyki
- walidacja inputu
- brak warningów nullable
Technologie:
- C#
- .NET
- JSON (System.Text.Json)
- LINQ
### ✅ Etap 2 (Zakończony)
- migracja z JSON -> baza danych
- Entity Framework Core
- SQLite
- relacje między encjami
- logika biznesowa (blokada usuwania planu z historią)
### 🔜 Etap 3
- API lub aplikacja desktopowa
- dalszy rozwój architektury
- możliwa migracja do PostgreSQL

---

## 🧠 Czego się nauczyłem

- pracy z architekturą aplikacji (rozdzielenie logiki i UI)
- pracy z Entity Framework Core
- projektowania relacji w bazie danych
- refaktoryzacji kodu i poprawy struktury projektu
- zarządzania migracjami i bazą danych

---

## 💡 Autor 
Projekt stworzony przez: Miłosz Roman

---