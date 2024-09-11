# E-Commerce Platform

## Overview

This is a simple e-commerce platform built using .NET, designed to manage products, categories, users, and orders. The platform includes user authentication and role-based access control (RBAC), allowing different levels of access for administrators and regular users.

## Features

1. User Authentication: Register, login, and manage user sessions.
2. Role-Based Access Control (RBAC): Different roles (e.g., Admin, Customer) with varying levels of access.
3. Category Management: Administrators can create, update, and delete product categories.
4. Product Management: Products are categorized, and both admins and users can view products. Admins can manage product listings (CRUD operations).
5. Order Management: Users can place orders, and admins can manage and process them.

## API Endpoints: RESTful API endpoints for managing categories, products, users, and orders.

### Technologies

- .NET: The project is built with .NET, leveraging the framework's power for building a robust e-commerce platform.
- Entity Framework Core: Used for database interactions.
- ASP.NET Identity: Manages user authentication and role-based access control.
- SQL Server: Database engine for persisting data.
- Swagger: API documentation and testing.

### Installation

1. Prerequisites

- .NET SDK
- SQL Server or any compatible database engine
- Postman (for API testing) or Swagger.

2. Steps to Run

- Clone the repository:

  ```
  git clone https://github.com/Andreaa-Dev/server-with-auth
  ```

- Navigate to the project folder:

  ```
  cd server-with-auth
  ```

- Update the connection string in appsettings.json to match your SQL Server instance.

  ```
  "ConnectionStrings": {
  "DefaultConnection": "Server=yourserver;Database=ecommerce;User Id=yourusername;Password=yourpassword;"
  }
  ```

- Run database migrations to set up the database schema:
  ```
  dotnet ef database update
  ```
- Build and run the project:

  ```
  dotnet run
  ```

- Access the project:
  ```
  Web: https://localhost:5001
  API: https://localhost:5001/swagger
  ```

### API Endpoints

1. User Authentication
   - POST /api/auth/register: Register a new user.
   - POST /api/auth/login: Authenticate a user and get a token.
2. Category Management (Admin Only)
   - GET /api/categories: List all categories.
   - POST /api/categories: Create a new category.
   - PUT /api/categories/{id}: Update a category.
   - DELETE /api/categories/{id}: Delete a category.
3. Product Management
   - GET /api/products: List all products.
   - GET /api/products/{id}: View a product.
   - POST /api/products: Create a new product (Admin).
   - PUT /api/products/{id}: Update a product (Admin).
   - DELETE /api/products/{id}: Delete a product (Admin).
4. Order Management
   - GET /api/orders: List all orders (Admin).
   - POST /api/orders: Create a new order (Customer).
   - PUT /api/orders/{id}: Update order status (Admin)
5. Roles and Permissions
   - Admin: Has full access to all endpoints, including management of categories, products, and orders.
   - Customer: Can view products and place orders.

### Future Improvements

1. Implement search functionality for products.

2. Introduce shopping cart and payment gateway integration.

3. Testing
