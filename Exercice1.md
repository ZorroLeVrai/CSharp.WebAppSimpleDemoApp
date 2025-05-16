# Web API Exercice 1

Cr�ez une Web API REST en ASP.NET Core pour g�rer une liste de t�ches.

Une t�che est caract�ris� par
- Id
- Intitul�
- Statut
- �ch�ance

Etapes � suivre
1. Initialiser le projet
1. Cr�er le mod�le Tache
1. Cr�er un service pour les t�ches
1. Cr�er le contr�leur REST

Voici les routes � g�rer au niveau du contr�leur

- **GET /api/taches** � R�cup�rer toutes les t�ches
- **GET /api/taches/{id}** � R�cup�rer une t�che par ID
- **POST /api/taches** � Cr�er une t�che
- **PUT /api/taches/{id}** � Modifier une t�che
- **DELETE /api/taches/{id}** � Supprimer une t�che

## Bonus 1
**Logging avec Serilog**
- Installer et configurer Serilog
- Ajouter un log fichier journalier (logs/log-.txt)
- Logger les actions CRUD sur les t�ches

## Bonus 2
**Middleware de journalisation**
- Cr�er un middleware personnalis�
- Logger la requ�te re�ue (l�URL, m�thode HTTP, �)
- Logger le statut de la r�ponse

## Bonus 3
**Configuration de l�application**
- Ajouter une section `TacheConfig:TacheMax`
- Injecter la configuration via `IOptions<T>`
- Refuser la cr�ation de t�che si le max est d�pass�
