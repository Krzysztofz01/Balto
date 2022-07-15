# What is Balto?
Most people, regardless of whether they work for large corporations, for small businesses, whether they are freelancers or just spend time with their hobbys, they like to plan and have full control over their projects. There are a lot of popular tools, platforms and services used to create notes, planning tasks for teams or even whole companies. Every day, an independent application is also created for a similar purpose. The idea behind the Balto project is to provide a universal backend for small and large applications designed to arranging tasks, planning activities, project management etc. Balto also allows you to migrate or/and integrate with other popular platforms.

## Extensibility and development
The Balto project is development-oriented. The plugin system allows you to create extensions so it allows you to integrate with any system or platform, it allows you also to easily add any functionality needed.

## Base functionalities
Due to the lightweight design of the system only the most universal data structures have been explicitly implemented, such as:
- **Goal** - Tasks that the user assigns himself. They have one-time or cyclical character. This model is most often found in "Todo" type application.
- **Notes** - Notes supporting the Markdown syntax. Single user or several people can work on them together. This model is often found in applications to create notes such as *Evernote* or *Inkdrop*.
- **Project** - The most extensive structure. It allows you to create tasks with a division into categories/groups. It has a ticket system. Expanded management of time and assignment of users to tasks. Model often found in project management applications such as *Jira* or *Trello*.
- **Tags** - Headers that can be assigned to notes or tasks are used to facilitate the assignment and organization of resources. They are shared between goals, notes and project tasks.
- **Teams** - A way to group users. The division can be made on the basis of the scope of activities or tools used, e.g. backend devs, devops, admins etc.

## Deployment and security
An important feature of the project is that it is open-source. We can be sure about security and data privacy. In the appearance of various projects, privacy is very important. The project is self-hosted, so no infrastructure is imposed. We can use one computer with Docker, a small home server using, for example: Raspberry Pi or use a full-fledged local server or host your own VPS instance. Created integration and plugins can be created without sharing the source code, so we are not imposed to share for example our company infrastructure. The important thing is that there is no risk of data being stolen by third parties. We have full control over who can use our platform.

## Technology stack
Balto was created using ASP.NET and the ORM system Entity Framework. The default database provider is MySQL, but it is possible to create support for PostgressSQL or MSSQL. Technologies like Quartz, Serilog and Swagger have also been used. The application is containerized using Docker.