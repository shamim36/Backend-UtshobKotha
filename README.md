# UtshobKotha - Backend

## Project Overview
**UtshobKotha - Backend** is an ASP.NET Web API project built using **ASP.NET 9** to provide backend services for UtshobKotha. The API serves different endpoints and is integrated with a database. Swagger is enabled to provide API documentation for easy testing and exploration.

## Prerequisites

To run this project, you need to install the following software:

1. **[Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)** or **[Visual Studio Code](https://code.visualstudio.com/Download)** (Recommended: Visual Studio for full experience)
   - Visual Studio is the IDE for developing ASP.NET Core applications. 
   - **Visual Studio Code** can also be used with proper extensions like C#.

2. **[Git](https://git-scm.com/downloads)** (for cloning the repository from GitHub)

3. **[ .NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet)** (Required to run ASP.NET 9 Web API projects)
   - Download and install .NET SDK 9.0 to your machine.

4. **SQL Server** (if your project requires a database, you can use [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)).

---

## Step 1: Clone the Repository from GitHub

1. Open **Git Bash** or any terminal with Git installed.
2. Run the following command to clone the project repository to your local machine:
   ```bash
   git clone https://github.com/shamim36/Backend-UtshobKotha.git

## Step 2: Open the Project in Visual Studio

1. Open **Visual Studio 2022** or **Visual Studio Code**.
   
2. In **Visual Studio**, follow these steps:
   - Click **File** > **Open** > **Project/Solution** and browse to the folder where you cloned the project.
   - Open the `.sln` (solution) file in the project folder.

3. In **Visual Studio Code**, follow these steps:
   - Click **File** > **Open Folder** and select the folder where you cloned the project.

---

## Step 3: Install Dependencies

1. Open **Package Manager Console** in Visual Studio:
   - Go to **Tools** > **NuGet Package Manager** > **Package Manager Console**.

2. Run the following command in the **Package Manager Console** to restore the project dependencies:
   ```bash
   dotnet restore

## Step 4: Add Database Migrations

If your project includes database models that need to be migrated to the database, follow these steps:

1. **Open the Package Manager Console**:
   - In **Visual Studio**, go to **Tools** > **NuGet Package Manager** > **Package Manager Console**.
   - Alternatively, you can use **Visual Studio Code** with the terminal for the following commands.

2. **Add a New Migration**:
   - In the **Package Manager Console** or terminal, run the following command to add a new migration:
   ```bash
   Add-Migration InitialCreate
## Step 5: Build the Project

Once all the dependencies are installed, you need to build the project to compile the code and check for any errors. You can do this in **Visual Studio** or using the **.NET CLI**.

1. **In Visual Studio**:
   - Open the project in **Visual Studio**.
   - Click on **Build** in the top menu and select **Build Solution** (or press `Ctrl + Shift + B`).
   - Visual Studio will compile the entire solution, checking for any errors or warnings in your code.

2. **In the Terminal (using .NET CLI)**:
   - Open a terminal or command prompt and navigate to the project directory.
   - Run the following command to build the project:
   ```bash
   dotnet build

## Step 6: Run the Application Locally

Once the project is built and dependencies are restored, you can run the application locally by following these steps:

1. **In Visual Studio**:
   - Click **Run** or press `F5` to start the project. This will launch the application locally in your browser.
   
2. **In the Terminal (using .NET CLI)**:
   - Navigate to the project directory using the terminal (if you're not already there).
   - Run the following command to start the application:
   ```bash
   dotnet run


![image](https://github.com/user-attachments/assets/9d534bcd-3122-41be-8e24-2c9a27ecbd39)
