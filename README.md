
Workflow with JS scripts:
All vendor scripts are plugged via vendor.js file wich further will be bundled in one file.
Each specific page has its own scrips that loads with page. Using SystemJs all requred dependecies are loaded for the page.

System.js config:
	- download system.js library
	- if you use ES6 syntax: compile all script with babel gulp task 'gulp babel'
		- also you can use next config to run ES6 without files compilening with babel (but firstly download plugin-babel scripts)
			SystemJS.config({
				map: {
					'plugin-babel': '/system.js-0.20.0/plugin-babel/plugin-babel.js',
					'systemjs-babel-build': '/system.js-0.20.0/plugin-babel/systemjs-babel-browser.js'
				},
				transpiler: 'plugin-babel'
			});
	- config system.js
	- import script on page. E.g.:
		SystemJS.import('/js/pages/home/index.babel.js');

Polyfill (Babel) to emulate a full ES2015+ environment:
	- npm install --save babel-polyfill
	- inlude it as root dependency (require("babel-polyfill"))


Database
	- Store all timestamps, dates in UTC
	- Use Ulid (Universally Unique Lexicographically Sortable Identifier) as id
		ExperienceKeeper.NUlid assembly is 100% copy of original NUlid for .NET
		See - https://github.com/RobThree/NUlid


Development
	EFCore2 and PostgreSQL
		For PG
			- Create user with create DB rights (if DB will be created automatically)
		For .
			- Install Microsoft.EntityFrameworkCore
			- Install Npgsql.EntityFrameworkCore.PostgreSQL

			(For EF CLI support)
			- Microsoft.EntityFrameworkCore.Tools
			- Microsoft.EntityFrameworkCore.Tools.DotNet
				If fails when installing through NuGet add "<DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.1" />" to .csproj file (with correct version)

			- Add configurations (ConnectionString) to .json files
			- Add configurations to Startup.cs
		For .Data
			- Install Microsoft.EntityFrameworkCore
			- Install Npgsql.EntityFrameworkCore.PostgreSQL
			- Create DbContext
		- Create Initial migration
		- Update database

		Add migration (execute in base project directory)
			dotnet ef migrations add -h
			dotnet ef migrations add <MigrationName> -c ApplicationDbContext -o Migrations/ApplicationDb -s . -p ../ExperienceKeeper.Data
		
		Remove migration
			dotnet ef migrations remove -h
			dotnet ef migrations remove -c ApplicationDbContext -s . -p ../ExperienceKeeper.Data

		Update database
			dotnet ef database update -h
			dotnet ef database update -c ApplicationDbContext -s . -p ../ExperienceKeeper.Data

	ASP.NET Identity
		- Create new MVC project with "Individual user accounts" option for authentication 
		- or add it to existing project using new project as example
			Data
				- Microsoft.AspNetCore.Identity
				- Microsoft.AspNetCore.Identity.EntityFrameworkCor
				- Change DbContext
				- Create migration and update DB
				- Add AccountController, ManageControllers, Views, ViewModels, Services, Extensions





dotnet ef migrations add Add_Category_UserCategory_Record_RecordUserCategory -c ApplicationDbContext -o Migrations/ApplicationDb -s . -p ../ExperienceKeeper.Data