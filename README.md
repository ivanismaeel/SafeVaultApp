# SafeVaultApp

## Overview
SafeVaultApp is a secure web application designed to manage user accounts and sensitive information. The application implements robust security measures, including input validation, SQL injection prevention, authentication, and authorization mechanisms.

## Features
- **User Registration and Login:** Secure user registration and login using ASP.NET Core Identity.
- **Role-Based Access Control:** Authorization policies to control user access based on roles.
- **SQL Injection Prevention:** Secure database queries using Entity Framework Core.
- **Input Validation:** Validate user inputs to prevent common security vulnerabilities.
- **Cross-Site Scripting (XSS) Prevention:** Implement output encoding to prevent XSS attacks.

## Technologies Used
- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **ASP.NET Core Identity**
- **JWT (JSON Web Token) Authentication**
- **Serilog for Logging**

## Prerequisites
- **.NET 8.0 SDK**: Download and install from [here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server**: Ensure you have SQL Server installed and running.

## Getting Started

### 1. Clone the Repository
```sh
git clone https://github.com/ivanismaeel/SafeVaultApp.git
cd SafeVaultApp
