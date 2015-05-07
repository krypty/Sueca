# Sueca

Sueca est un jeu développé avec des technologies .NET. Le projet est constitué de 3 axes.

Un serveur WCF a été développé afin de fournir aux clients des méthodes permettant de jouer au jeu. Par exemple, jouer un carte, lister les salles disponibles,...

Un client WPF permet de jouer sur un ordinateur et un client MVC.NET permet d'y jouer depuis un navigateur web.

L'objectif de ce projet de montrer et de proposer un jeu multiplateforme en utilisant des web services.

Les règles du jeu ont été adaptées selon celles-ci: http://perso.numericable.fr/poissy.portugal/Reglement%20sueca.pdf

## Pré-requis pour MVC.net

1. Installer Typescript 1.4
https://visualstudiogallery.msdn.microsoft.com/2d42d8dc-e085-45eb-a30b-3f7d50d55304
2. Supprimer le dossier
C:\Program Files (x86)\Microsoft SDKs\TypeScript\1.0\

## Lancer le serveur
1. Ouvrir visual studio en mode administrateur et ouvrir le projet du serveur
2. Clic droit sur le projet ConsoleHost -> Définir comme projet de démarrage
3. Enjoy
