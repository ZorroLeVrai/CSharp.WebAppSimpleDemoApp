# Web API Exercice 1

Créez une Web API REST en ASP.NET Core pour gérer une liste de tâches.

Une tâche est caractérisé par
- Id
- Intitulé
- Statut
- Échéance

Etapes à suivre
1. Initialiser le projet
1. Créer le modèle Tache
1. Créer un service pour les tâches
1. Créer le contrôleur REST

Voici les routes à gérer au niveau du contrôleur

- **GET /api/taches** – Récupérer toutes les tâches
- **GET /api/taches/{id}** – Récupérer une tâche par ID
- **POST /api/taches** – Créer une tâche
- **PUT /api/taches/{id}** – Modifier une tâche
- **DELETE /api/taches/{id}** – Supprimer une tâche

## Bonus 1
**Logging avec Serilog**
- Installer et configurer Serilog
- Ajouter un log fichier journalier (logs/log-.txt)
- Logger les actions CRUD sur les tâches

## Bonus 2
**Middleware de journalisation**
- Créer un middleware personnalisé
- Logger la requête reçue (l’URL, méthode HTTP, …)
- Logger le statut de la réponse

## Bonus 3
**Configuration de l’application**
- Ajouter une section `TacheConfig:TacheMax`
- Injecter la configuration via `IOptions<T>`
- Refuser la création de tâche si le max est dépassé
