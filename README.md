# WebApplication3

This is for .NET backend for NZMSA Phase 2

This Project may have some potential unfixed bugs.

The Middleware via DI ensures a loose coupling and a more flexible code style. Instead of initiating an instance, we pass the instance in, and this means we can change the property of the instance anytime and pass it in again.

The appsettings.json enables us to have different configuration ie.Connection string in different environment. So we can have a production database and a development database (if necessary). Please note that I used SQLite database for development and SQL database for production, therefore it might need further editing before running different enviroment - reason I does this is because I'm seeking to deploy the API to Azure, but hadn't finish by the time of submission.

edit: Finished initial deployment: https://speechtotextapplication320220810130715.azurewebsites.net/swagger/index.html

The testing middlewares basically help us to mock a test more easily. Note the in the testing project I hadn't change the test function after I made some edits to the main project.
