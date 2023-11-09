# Task Manager Project Documentation

## Overview

The Task Manager project enables logged-in users to efficiently manage tasks for the current day.

## Getting Started

To run the project, follow these steps:

1. **Create a PostgreSQL Database**:
   - Run the following command to set up a PostgreSQL database using Docker:
     ```
     docker run --name postgres-container -p 1433:5432 -e POSTGRES_PASSWORD=Qwerty123@ -d postgres
     ```

2. **Start the API**:
   - Execute the following command from the root of the repository to launch the API:
     ```
     dotnet run --project src/TaskManager.Api/TaskManager.Api
     ```

3. **Run the User Interface (UI)**:
   - Start the UI project using the following command from the root of the repository:
     ```
     dotnet run --project src/TaskManager.UI/TaskManager.UI
     ```

4. Open your web browser and visit the URL: [https://localhost:7197](https://localhost:7197)

Now you're ready to manage your tasks effectively!

## Architecture

The Task Manager project is designed with scalability and maintainability in mind, using the following architectural elements:

- **Clean Architecture**: The project is organized based on the Clean Architecture design principles, promoting separation of concerns and maintainability.

- **Microservices Ready**: The solution is divided into two projects, laying the foundation for future microservices adoption.

- **Database**: PostgreSQL is used as the database system, integrated with Entity Framework Core for efficient data access.

- **API and CQS**: The API follows the Command Query Separation (CQS) pattern, implemented through Mediator to achieve a more organized and extensible codebase.

## API

The API project serves as the backbone of the Task Manager system. It provides endpoints for managing tasks and other related functionality. The API leverages various libraries and tools to ensure smooth operation.

## User Interface (UI)

The UI project do not have good and clean validation and it needs more work for the design because I do not have enough time for it.

## Testing

The project is equipped with a robust testing framework, including integration tests.

## Clean Code and Design Patterns

I emphasize clean code practices throughout the project's development. Design patterns, such as Clean Architecture and CQS, are employed to maintain code quality and readability.

## Security and Authentication

User authentication is a critical aspect of the Task Manager project, and it is implemented securely using JSON Web Tokens (JWT). Here's an overview of how user authentication works:

- **JWT Tokens**: The authentication process relies on JWTs, which are cryptographically signed tokens that include user-specific information and an expiration time. These tokens are issued upon successful login and are used to authenticate subsequent API requests.

- **Login**: Users provide their credentials (typically email and password) to the system. The system validates these credentials, and if they are correct, a JWT token is generated and returned to the user.

- **Token Storage**: The JWT token is stored securely on the client side, typically in a secure HTTP cookie or local storage, to ensure it remains private.

- **Token Expiry**: JWT tokens have a limited lifespan. Once a token expires, users must re-authenticate. This helps mitigate the risk of token theft or misuse.


## Error Handling

Effective error handling is implemented, with clear error codes, comprehensive logging, and user-friendly error messages to ensure a smooth user experience.

## Best Practices

The project adheres to best practices, including RESTful API design, versioning standards, and error handling conventions.


## Conclusion

The Task Manager project simplifies task management for users, offering a powerful and efficient solution for organizing daily tasks.
