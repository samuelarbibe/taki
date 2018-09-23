# What is This Project?
High-School Project - a Desktop/mobile object-oriented card game, operated by a database service
Taki is an Israeli game based on the game UNO. the game is played by 2 to 4 players.

# Why does it exist?
The creator of this project is *Samuel Arbibe*, a 12th grade student in Ostrovsky High-School in Raanana.
This project is my Computer Science matriculation Final project.

# About this Project

This Project is written in the C# .NET Framework, using Visual Studio 2017 Community/IntelliJ RIDER with git extension, and Microsoft Access for managing the Database.

The Technologies used in this project are WPF and WCF services.

This game is a Multiplayer Card game, played on windows-based PCs' and Android devices.

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
