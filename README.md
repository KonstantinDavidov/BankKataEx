# Bank Kata

## Description
The project is implementation of the Bank kata: https://katalyst.codurance.com/bank

With some custom requirements:
* a log of transactions regarding the account should be stored
* provide a web service that allows for:
  * making transactions between accounts
  * withdrawals
  * deposits
  * querying the balance of accounts
  * opening a new account
  * getting the bank statement
* use outside-in TDD, document your steps using commits in a GitHub repository
* There are Student, Business, and Giro Accounts
  * Student: no negative balance allowed
  * Business: max 100,000.00 negative balance
  * Giro: max 4,000.00 negative balance
  * When creating a student account, a student id number must be provided
  * When creating a business account, a business id number must be provided


## Description of projects
**BankKata.ConsoleApp** - console application for basic testing the original Bank Kata (https://katalyst.codurance.com/bank).

**BankKata** - web api service that contains endpoints related to Bank Accounts.

**BankKata.Infrastructure** - infrastructure layer of application, contains services to interact with business logic, request objects etc.

**BankKata.Business** - core project that contains business models, entities, core services.

**BankKata.Tests** - contains unit tests and acceptance tests.

**BankAccount.Shared** - project that contains stuff that could be reusable accross multiple projects, like constants, helper methods etc.

## Installation
The current project uses .NET Core 3.1 version.

## Usage
To use a web service, set **BankKata** as startup project. Then you can just run the application.
By default, the application starts on https://localhost:5001 + http://localhost:5000

If you use Postman (https://www.postman.com/), you can find Portaman collection for testing the service [here](./BankKata.postman_collection.json)

