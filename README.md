# BankBlazor
A banking system built with ASP.Net Core Web API and Blazor WebAssembly connected to belonging database. The project allows customers to manage accounts, profile, view transaction history, and make transfers while employees can browse account, customer and transaction data.

##
ASP.NET Core Web API
Entity Framework Core
Blazor WebAssembly
SQL Server
Newtonsoft.Json
RESTful API Integration

## Setup
1: Clone respitory: https://github.com/hannahult/BankApp
2: Open in Visual Studio 
3: **Use this connectionstring for DB connection:** "Server=localhost;Database=BankBlazor;Trusted_Connection=True;TrustServerCertificate=true;Command Timeout=180"
4: **Database First Scaffolding:** Server=localhost;Database=BankBlazor;Trusted_Connection=True;TrustServerCertificate=true;Command Timeout=180" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data
5: Control so both projects start(API and Client)
6: Ensure the ports in `launchSettings.json` do not conflict 
7: Set both projects as startup project and start

## Features
-Role-based navigation(customer and employee)
-Transfer money between accounts
-For customer: 
-View profile, accounts and transaction history
-For Employee
	-View all transaction, account and customer data
-Third-party API integration for bank holidays

## API Examples
-”GET /api/customer/paged”
-”POST /api/transaction”
-”GET /api/transaction/customer/{customerId}/paged”
-”GET api/account/customer/{customerId}”

## Scalability
To ensure preformance and scalability, server side pagination has been implemented on all data-heavy pages such as:
-All Customers
-All Transactions (per customer and account as well)
-All Accounts

## Requirements
-An interactive web application with relevant functionalities such as viewing data for customers, accounts and transactions
-JSON communication between Client side and API side
-A RESTful API with relevant endpoints
-Navigation to pages
-Implementation of third-party API
-Well structured codebase with DTOs, services, interfaces ect.


