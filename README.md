Steps to Reproduce
1.	Clone the repository: https://github.com/IsharaPraveeth/FarmManagementWeb.git
2.	Restore NuGet packages.
3.	Run the following command in the Package Manager Console (PMC) to create the database and seed the data:
Update-Database -Project FarmManagement.Infrastructure -StartupProject FarmManagement.API
Alternatively, you can run the db-init.sql script in your SQL Server to initialize the database.
(Note: You need to set the ConnectionStrings value in the appsettings.json of the API before step 3.)
4.	Set Multiple Startup Projects: FarmManagement.API & FarmManagement.Web.
