# Task Management API

This is a Task Management API built using ASP.NET Core, following a layered architecture that separates business logic (BLL) from data access logic (DAL), using Entity Framework Core for database interactions.

## How Does It Work?

1. **Request Authentication**:  
   When a request is sent to the controller (specifically for task-related operations), the system first verifies if the request is authenticated. Only authenticated users can access the tasks related to them.

2. **Business Logic Layer (BLL)**:  
   Once the request is authenticated, the controller forwards the data to the appropriate service in the BLL assembly. The BLL contains the business logic for handling the task operations, such as mapping incoming data to entities.

3. **Common Assembly**:  
   The Common assembly is responsible for handling **Data Transfer Objects (DTOs)** and **mapping profiles**.  
   - **DTOs**: Used to encapsulate the data being transferred between the client and the API. This layer ensures that only necessary data is sent in requests and responses.
   - **Mapping Profiles**: AutoMapper is used to map between the DTOs and the entity models, allowing the BLL to work with clean, structured data.
  
4. **Data Access Layer (DAL)**:  
   The mapped entities are then passed to the repository in the DAL assembly. This layer interacts with the database using Entity Framework Core (an Object-Relational Mapper). The DAL assembly is responsible for executing queries and persisting data in the SQL Server database.

## Technologies Used

- **ASP.NET Core**: Backend framework used for building APIs.
- **Entity Framework Core**: ORM used for interacting with the SQL Server database.
- **SQL Server**: Relational database used for data storage.
- **AutoMapper**: For mapping between DTOs and entities.
- **JWT Authentication**: Authentication system used for secure access to the API.

## Layers of the Application

- **Controller Layer**: Handles HTTP requests, validates authentication, and forwards them to the BLL.
- **BLL (Business Logic Layer)**: Contains services that process the business logic, map incoming data, and interact with the DAL.
- **DAL (Data Access Layer)**: Handles data queries and updates using Entity Framework Core.
- **Common Layer**: Contains DTOs and AutoMapper profiles for transforming data between layers.

## API Endpoints

- **/api/tasks/GetAll**: Retrieves all tasks for the authenticated user with optional filtering and pagination.
- **/api/tasks/Create**: Creates a new task for the authenticated user.
- **/api/tasks/Update**: Updates an existing task.
- **/api/tasks/Delete**: Deletes a task for the authenticated user.

## Prerequisites

- .NET 8.0 SDK
- SQL Server

## Getting Started
  
  Clone the repository:
   ```bash
   git clone https://github.com/your-repo/task-management-api.git
   ```
## How to Run It

1. **Open Package Manager Console (PMC)**:
    Or open Terminal in the project folder.
2. **Set up the SQL Server database**:
    Update your `appsettings.json` file with your database connection string:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=<YourServerName>;Database=TaskAPI;Integrated Security=true;TrustServerCertificate=true;"
    }
    ```

3. **Apply migrations**:
    Navigate to the DAL directory and run migrations to set up the database schema:
    ```bash
    cd .\DAL
    ```
    ```bash
    update-database
    ```

5. **Start the application**:
    Open the project in Visual Studio and click the **green HTTP button** at the top to run the application.

6. **Test the API**:
    Once the application is running, a new tab will open with **Swagger**. Swagger is a UI for exploring and testing the API.

    ### Register and Authenticate:
    - If you are not registered yet, use the `/register` endpoint to create a new account.
    - After registering, you will receive an **AccessToken** in the response body.
    - Copy the AccessToken and scroll up to the top-right of the Swagger page.
    - You will see a green **Authorize** button. Click on it.
    - In the dialog that appears, type `Bearer {AccessToken}` (replace `{AccessToken}` with the actual token you copied).
    - Click **Authorize** to authenticate your requests.

    Now you can test other endpoints, and all requests will be authenticated using your token.
