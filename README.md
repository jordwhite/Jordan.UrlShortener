## Jordan's URL Shortener
This is software will shorten URLs for you.

### Instructions for use
To test this program you will need Visual Studio 2022. You want to run the project `Jordan.UrlShortener.UserInterface.Api` and then detach it from Visual Studio's debug environmennt via 'Detach All'. Keep the API project running. You then want to set `Jordan.UrlShortener.UserInterface.Blazor` as your start up project and then execute this project. This is the front end for the application and it communicates with `Jordan.UrlShortener.UserInterface.Api`. 

### Swagger 
The Swagger UI for the API can be found at https://localhost:7080/swagger/index.html when the API is active. It is fairly simple.

### Database
The LocalDB database should create automatically for you unless there is a naming conflict e.g. it shares the same name with another database. If that happens you will have to delete the old database so that the application can create a new one.

### Architecture
The application is primarily built on the onion architecture, going from Domain -> Application -> Infrastructure -> User Interface & Testing. A project higher in the chain does not reference the other in order to reduce coupling and to separate the concerns of different modules.

### Model
The model is simple and consistents primarily of one class: the ShortenedUrl which calls all the information the system needs to find a full path URL from any given short URL.

### Patterns
Various patterns are used throughout the application, in particular:
    - The Mediator to send commands and recieve responses to command handlers from the API controllers.
    - Dependency injection to help prevent tight coupling and increase testability
    
### Diagrams & Notes
There is a diagram available in the `Diagrams` that will give you a very high level overview of the system. It shows how everything has been grouped in the onion architecture. I've also included a text file of my own notes in the `Notes` folder that I would make while developing the software.

Thank you for your time.