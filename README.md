# PHP REST API, ASP.NET FRAMEWORK WCF, ASP.NET FRAMEWORK WEB API CRUD

## NOTEZ BIEN
* Avant de démarrer l'application veuillez mettre à jour le service connecté au niveau du webGl
* La recherche est uniquement effectuer au niveau du wcf
* Apres test l'appelle des fonctions de consommation des API a été conmenté

# AUTEUR
* Mouhmadou TALL

## PHP CRUD API
* `GET - http://localhost/php-rest-api/api/read.php` Récuperer la liste des personne
* `GET - http://localhost/php-rest-api/api/single_read.php/?IdPersonne=3`récupere un obet personne selon id
* `POST - http://localhost/php-rest-api/api/create.php` Inserer une personne
* `POST - http://localhost/php-rest-api/api/update.php` Mettre à jour un enregistrement de personne
* `DELETE - http://localhost/php-rest-api/api/delete.php` Supprimer un enregistrement de personne

## ASP.NET FRAMEWORK WEB API CRUD
* `GET - https://localhost:44331/api/Personnes/GetAllPersonnes` Récuperer la liste des personne
* `GET - https://localhost:44331/api/Personnes/GetPersonneById/{id}` récupere un obet personne selon id
* `POST - https://localhost:44331/api/Personnes/AddPersonne` Inserer une personne
* `PUT - https://localhost:44331/api/Personnes/UpdatePersonne/{id}` Mettre à jour un enregistrement de personne
* `DELETE - https://localhost:44331/api/Personnes/DeletePersonne/{id}` Supprimer un enregistrement de personne
