## Overview

The `CatalogService` solution is a microservice designed to manage and serve course catalog data. It leverages ASP.NET Core for building the web API, Entity Framework Core for data access, and Kafka for message consumption. The solution is structured to be scalable, maintainable, and easy to deploy.

## Projects

### CatalogService

This is the main project of the solution. It contains the following key components:

- **Program.cs**: The entry point of the application. It sets up the web host, configures services, and defines the middleware pipeline.
- **Consumers**: Contains the Kafka consumer service that listens to Kafka topics and processes messages.
- **Repositories**: Contains the repository interfaces and implementations for data access.
- **Models**: Defines the data models used in the application.
- **Controllers**: Contains the API controllers that handle HTTP requests and responses.

## Key Features

- **Kafka Integration**: The service is configured to consume messages from Kafka topics using the `KafkaConsumerService`.
- **In-Memory Database**: For simplicity and testing purposes, the service uses an in-memory database to store course data.
- **Dependency Injection**: The service uses dependency injection to manage service lifetimes and dependencies.
- **Swagger**: Integrated Swagger for API documentation and testing.
- **HTTPS Redirection**: Ensures all HTTP requests are redirected to HTTPS for security.

## Configuration

The application is configured using the `appsettings.json` file. Key configuration sections include:

- **KafkaSettings**: Configuration for Kafka, including broker addresses and topic names.
- **ConnectionStrings**: Database connection strings (if using a persistent database).



