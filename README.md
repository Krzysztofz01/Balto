![logo](https://user-images.githubusercontent.com/46250989/119037552-684b1380-b9b2-11eb-9667-2331a3694693.png)
# What is the Balto?
Balto is a project planning and management system. It has a number of functionalities that allow for easy organization of work. **Balto is for everyone.** This system will work for developers, freelancers, small businesses, artists and various types of teams working in the scrum / agile methodology.

## Functionalities
**Objectives** - a system of personal tasks, assigned to one user. They can be one-time or cyclical. There is an option to remind about deadlines via email.
**Notes** - a system for saving text documents and sharing it with other users. The documents are of the richtext type and can be exported to .pdf and .odf files (in progress).
**Projects** - a comprehensive system for managing tasks related to larger projects. The system informs us about deadlines and allows for easy division of duties during work.

## Integration
Balto allows you to migrate your board from **Trello** to Balto. This can be done by downloading and uploading the appropriate JSON file or by linking accounts and using the REST API (in the future). Planning is also integrated with platforms such as **Evernote** and **Google Documents** to integrate the notes system.

## Deployment and security
Balto is not a centralized web platform, one for all. A person or company can create their own instance of this platform in a number of ways. You can use one computer with Docker, you can create a small home server using, for example: Raspberry Pi or use a full-fledged local server or host your own VPS instance. There are many options, but the important thing is that there is no risk of data being stolen by third parties. We have full control over who can use our platform.

## Development
The Balto system is development-oriented and easy to maintain due to the chosen architecture. It is possible to easily add functionality at the customer's request.

## Technology stack
The balto database is Microsoft SQL Server. The backend was written using ASP.NET Core, the ORM Entity Framework Core and third party technologies such as Hangfire, Automapper and Serilog were used. The client application was created using the Angular framework and the Bootstrap library. Everything was containerized using Docker.
