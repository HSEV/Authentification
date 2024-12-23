# Authentication System

This is a simple authentication system for account creation and login, implemented in C# and JavaScript, using a local SQLite database.

## Prerequisites

Make sure you have **.NET SDK** installed on your system. You can download it from [dotnet.microsoft.com](https://dotnet.microsoft.com/download).

## Installation

1. Clone the repository or download the source code.
2. Open a terminal and navigate to the project directory.

## Running the Project

To start the project, run the following command in the terminal:

```sh
dotnet run
```

After the project is running, click on the following URL to open the application in your browser:
----------------------

<img width="366" alt="Capture d’écran 2024-12-23 à 15 16 55" src="https://github.com/user-attachments/assets/7993b5a9-b7db-4345-a5dd-737d601381aa" />


## Libraries Used
*Microsoft.Data.Sqlite:* For interacting with the SQLite database.
*Microsoft.AspNetCore.Mvc:* For building the web application.
*Microsoft.AspNetCore.Identity:* For handling user authentication and authorization.

## How It Works
*Registration :*
Users can create an account by providing a username, email, and password on the registration page (register.html). The information is sent to the server, where it is stored in the SQLite database.

## Login
Users can log in by providing their email and password on the login page (index.html). The server verifies the credentials and grants access if they are correct.
