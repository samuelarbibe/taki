# Taki
> An Israeli card game based on the game UNO

## Table of contents
* [General info](#general-info)
* [Screenshots](#screenshots)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)
* [Inspiration](#inspiration)
* [Contact](#contact)

## General info
This project is my 12th grade CS Final project.
The goal of this project was to learn multiplayer dynamics, database implementation and improce C# coding skills.

## Screenshots
![Example screenshot](./screenshots/TMS_recording.gif)

## Technologies
This Project is written in the C# .NET Framework, using Visual Studio 2017 Community/IntelliJ RIDER with git extension, and Microsoft Access for managing the Database.

The Technologies used in this project are WPF and WCF services.

## Structre

This game is a Multiplayer Card game, played on windows-based PCs' and Android/IOS devices.

The Project consist of two smaller projects: Client and Server.


*CLIENT*:

The Client solution is a WPF app, with a refrence to the service that is given by the Server solution.
The Client solution handels front-end functions, like passing through information to the service(Login, Register, etc.),
and all the graphics related to the game itself.

*SERVER*:

The Server solution consists of 5 mini-projects:
 - Model (contains all the data structres necessary such as User, Player, Card, Game etc.)
 - ViewModel (contains all the query functions for all the data types, making this an Object-Oriented Database project)
 - Service (a WCF service containing all the functions needed by the client, but mostly redirects the request to the Business Layer)
 - BL (stands for Business Layer, conatins all the functions from the player loging and checking, to the player matching algorithm and game mechanics algorithm. Essentialy, this is the brain behind the Server, and the heart of the game)
 - Host (a WPF application to host the WCF service)

## Status
Project is: _finished_.

## Inspiration
Taki Project was inspired by the well-known card game UNO.
I really like the isareli edition to this game, Taki, so I decided to make it instead.

## Documentation

Full Documentation (in Hebrew): link_to_docu_

### Contact
samuel.arbibe@gmail.com
