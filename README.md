# TeamManagement

[![.NET](https://github.com/fabsg0/TeamManagement/actions/workflows/dotnet.yml/badge.svg)](https://github.com/fabsg0/TeamManagement/actions/workflows/dotnet.yml)
[![CodeQL Advanced](https://github.com/fabsg0/TeamManagement/actions/workflows/codeql.yml/badge.svg)](https://github.com/fabsg0/TeamManagement/actions/workflows/codeql.yml)

A modern Blazor application to manage teams — ideal for sports clubs, departments, or any membership-based group.

## Features

- 🧑 Member management with full CRUD support
- 🏷️ Filter and search by department or name
- 📁 Export members to Excel
- 📊 Department-specific views
- ✨ Beautiful and responsive UI with Bootstrap 5

## Screenshots

<p align="center">
  <img src="https://github.com/user-attachments/assets/c5118d48-4582-443e-a8fc-8d570504adc6" alt="Screenshot 1" style="max-width: 100%; border-radius: 10px; box-shadow: 0 2px 12px rgba(0,0,0,0.15); margin-bottom: 1rem;">
  <img src="https://github.com/user-attachments/assets/c826f985-4e98-4509-8429-889ad7246217" alt="Screenshot 2" style="max-width: 100%; border-radius: 10px; box-shadow: 0 2px 12px rgba(0,0,0,0.15); margin-bottom: 1rem;">
  <img src="https://github.com/user-attachments/assets/2af52d6a-6212-4017-869b-3a5fe8386efa" alt="Screenshot 3" style="max-width: 100%; border-radius: 10px; box-shadow: 0 2px 12px rgba(0,0,0,0.15);">
</p>

## Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/fabsg0/TeamManagement.git
   ```

2. Open in Visual Studio or Rider and restore dependencies:

   ```bash
   dotnet restore
   ```

 3. Create the database and tables
  Go to <a href="https://github.com/fabsg0/TeamManagement/tree/main/Database" target="_blank">Database</a> and create those tables and the stored procedure in your database.
  

4. Run the application:

   ```bash
   dotnet run --project Api.TeamManagement
   dotnet run --project Web.TeamManagement.Blazor
   ```

5. Navigate to `https://localhost:5001`

## Technologies Used

- Blazor Server
- Entity Framework Core
- MS SQL Server
- Bootstrap 5
- ClosedXML for Excel export

## License

This project is licensed under the MIT License.

