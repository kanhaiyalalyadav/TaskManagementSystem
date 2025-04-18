Project Setup Instructions

Follow the steps below to successfully set up and run the project:

1. Update the Connection String:

Open the appsettings.json file in the root directory.

Replace the placeholder connection string with the actual connection string of your SQL Server instance.



2. Extract Project Files:

Extract the contents of the ZIP file to a suitable location on your system.



3. Set Up the Database:

Open SQL Server Management Studio (SSMS).

Locate and open the Oritso.sql file from the extracted folder.

Execute the SQL script to create the required database and tables.



4. Run the Project:

Open the solution in Visual Studio 2022.

Ensure that .NET Core 8.0 SDK is installed on your system.

Set the correct project as the Startup Project.

Press F5 or click Start Debugging to run the application.
