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
- pracę z plikami JSON,
- obsługę błędów i walidację danych.

---

## 🚀 Funkcjonalności

### 📋 Zarządzanie planami
- dodawanie planu treningowego
- edycja planu wraz z możliwością wyboru czy chcemy edytować ćwiczenia
- usuwanie planu
- wyświetlanie planów (z ćwiczeniami i bez)

### 🏃 Wykonywanie treningu
- przechodzenie przez ćwiczenia i serie
- liczenie czasu trwania treningu
- podsumowanie po zakończeniu

### 📜 Historia treningów
- zapis historii do pliku JSON
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

---

## 💡 Jak działa aplikacja

Użytkownik tworzy plan treningowy składający się z ćwiczeń i obwodów.  
Następnie może rozpocząć trening, przechodząc przez kolejne serie i przerwy.  

Po zakończeniu treningu wynik zapisywany jest do historii (plik JSON),  
a dane mogą być później analizowane w sekcji statystyk.

---

## 🛠️ Technologie

- C#
- .NET
- JSON (System.Text.Json)
- LINQ

---

## 🧱 Struktura projektu
```text
TreningApp/
├── Models/ # modele danych (Plan, Historia, Statystyki itd.)
├── Services/ # logika biznesowa (PlanService, HistoriaService, StatystykiService)
├── UI/ # ConsoleRenderer (obsługa wyświetlania)
├── Helpers/ # InputHelper (obsługa inputu i walidacji)
├── Data/ # pliki JSON (plany, historia)
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
3. Uruchom aplikację:
```bash
dotnet run
```

---

## ⚠️ Wymagania
- .NET SDK (zalecane .NET 8 lub nowszy)

---

## 📈 Etapy rozwoju
### ✅ Etap 1 (obecny)
- aplikacja konsolowa (CLI)
- zapis danych do JSON
- historia i statystyki
- walidacja inputu
- brak warningów nullable
### 🔜 Etap 2
- zamiana JSON na bazę danych
- wersja desktopowa

---

## 🧠 Czego się nauczyłem

- pracy z architekturą aplikacji (rozdzielenie logiki i UI)
- zarządzania stanem aplikacji (shared services)
- obsługi danych i serializacji JSON
- eliminowania warningów nullable w C#

---

## 💡 Autor 
Projekt stworzony przez: Miłosz Roman