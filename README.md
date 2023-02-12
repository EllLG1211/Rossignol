![Tag](https://img.shields.io/badge/Rossignol-v0.5-blueviolet)
![Language](https://img.shields.io/github/languages/top/HandyS11/Rossignol)

[![Build Status](https://codefirst.iut.uca.fr/api/badges/valentin.clergue/Rossignol/status.svg)](https://codefirst.iut.uca.fr/valentin.clergue/Rossignol)
[![Quality Gate Status](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=alert_status&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)
[![Coverage](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=coverage&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)
[![Maintainability Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=sqale_rating&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)
[![Reliability Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=reliability_rating&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)
[![Security Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=security_rating&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)

[![Technical Debt](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=sqale_index&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)
[![Bugs](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=bugs&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)
[![Vulnerabilities](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=vulnerabilities&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)
[![Code Smells](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=code_smells&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)
[![Duplicated Lines (%)](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=duplicated_lines_density&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)
[![Lines of Code](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Rossignol&metric=ncloc&token=9e1f4645b86eb1abd678f85c330ad6047baebd85)](https://codefirst.iut.uca.fr/sonar/dashboard?id=Rossignol)


# Rossignol

Rossignol is a mobile app for password management.
Find our wiki [there](https://codefirst.iut.uca.fr/git/valentin.clergue/Rossignol/wiki).

## Context

**Rossignol** is a mobile application available on booth **IOS** and **Android**. It's main goal is to provide to they users the ability to **securly store** and **generate** their **passwords**.

This is a quick look of the **HomePage** & **MainPage** page of the application *(while been connected with an online account)*.

| LoginPage | MainPage |
| -- | -- |
| <img src=https://cdn.discordapp.com/attachments/715975451558019132/1030990408819421354/HomePage.png> | <img src=https://cdn.discordapp.com/attachments/715975451558019132/1030990921610825831/MainPage.png> |

## Features

* Localy store & generate passwords
* Edit and store informations about a password
* Use the application throw multiples devices*
* Share password to application users*
* Be notify when someone shares you a password

**with an online account*

## Getting Started

* 1) Install [Visual Studio 22](https://visualstudio.microsoft.com/fr/)
* 2) With the **Visual Studio Installer** add theses components:
  * .NET Desktop developpement *(.NET6 runtime)*
  * ASP.NET *(Core)*
* 3) Clean & Build the project solution

## EntityFramework

with the newest versions of our IDataManager this procedure is no longer required to use this project, as the code veries that the database already exists or creates it whenever needed. This is applicable for :memory: and sqlite databases. The sqlite database file is created at the root of the temporary windows system folder.

  * To create some **migration**
    * Delete `./Rossignol/Sources/EF_Model/Migrations`
    * With the terminal and with the `./Rossignol/Sources/EF_Model` path, run the `dotnet ef migrations add <whatever>` command.
      * If an error occur, run this command: `dotnet tool install --global dotnet-ef`
  * To create a **data base**
    * With the same path run the `dotnet ef database update --startup-project ..\Tests\TestEntities\TestEntities.csproj` command, which will apply the migration to the project.

*Note that the `EF_Model` project needs the `EntityFrameworkCore` + `EntityFrameworkCore.Sqlite` + `EntityFrameworkCore.Tools` nugget package to work.*
*Also note that the `TestEntities` project needs the `EntityFrameworkCore.Design` nugget package to work.*

## APIs

You can find the documentation & the [Postman](https://www.postman.com) test collection under `./Documentation/webservices`.

### API Gateway

* Local: https://localhost:7037
* Online: http://codefirst.iut.uca.fr/containers/valentinclergue-rossignolgateway:80

### REST API

* Local: https://localhost:7114
* Online: http://codefirst.iut.uca.fr/containers/valentinclergue-rossignolrestapi:80

### WebSocket
  
* Local: https://localhost:7266
* Online: https://codefirst.iut.uca.fr/containers/valentinclergue-rossignolwebsocket:80

## Credits

* Co-author: [**Elliott Le Guéhennec**](https://github.com/EllLG1211)
* Co-author: [**Mathis Ribémont**](https://github.com/TEDDAC)
* Co-author: [**Valetin Clergue**](https://github.com/HandyS11)
* Co-author: [**Yorick Geoffre**](https://github.com/Kanken6174)