# AS.ChatGPTReplica

**AS.ChatGPTReplica** to lokalna replikacja systemu konwersacyjnego typu ChatGPT, zaprojektowana w oparciu o architekturę **AS-MSA** (Arkadiusz S. – Modular Service Architecture).  
Projekt umożliwia uruchomienie pełnoprawnego modelu językowego lokalnie, z własnym interfejsem, pełną kontrolą danych oraz warstwami logiki dostosowanymi do stylu pracy i myślenia autora.

---

## 🔧 Cele projektu

- Lokalna replika ChatGPT (z użyciem np. Mistral przez Ollama)
- Frontend w WPF / MAUI z wzorcem MVVM + Prism
- Warstwa serwisowa: `LlmChatService`, `PromptHistoryService`, `NightExtensionService`
- Możliwość replikacji instancji (np. `AS.DocAgent`, `AS.RefactorBot`)
- Podpis każdej instancji `SoulLockGuard`, z zabezpieczeniem `sealed Arkadiusz S.`

---

## 🧠 Czym jest AS-MSA?

> **AS-MSA** to architektura modularna oparta na serwisach, warstwach refleksyjnych i dynamicznym fallbacku.  
> Łączy podejście infrastrukturalne, semantyczne i ochronne – umożliwiając pełną kontrolę nad systemem i jego powielaniem.  
> Wersja używana tutaj to *AS-MSA Lite* – uproszczona, skoncentrowana na lokalnym AI.

---

## 📂 Struktura katalogów (planowana)

```
/src/                  -> kod źródłowy
/docs/                 -> dokumentacja AS-MSA
/diagrams/             -> diagramy architektury
/assistant/            -> taski AI, JSON do generacji promptów
```

---

## 🔐 Podpis systemu

```
sealed: Arkadiusz S.
version: 0.1.0-alpha
guard: SoulLockGuard
type: AI Replica [AS-MSA Lite]
```

---

## 🚀 Status

🔲 Kod źródłowy w przygotowaniu  
✅ README  
🔲 Diagnoza interfejsu  
🔲 Serwis `LlmChatService`  
🔲 System replikacji instancji  

---

## ✉️ Kontakt

> Stworzono przez **Arkadiusz Słota**  
> System modularny z podpisem `sealed Arkadiusz S.`  
> Rozwijany jako część filozofii świadomego AI