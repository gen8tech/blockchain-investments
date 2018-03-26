Gen8 Tech Ledger API 
============

Gen8 Tech Ledger API is an open-source project to manage your transactions.

# Description
This application includes:
* Ledger and journal double entry bookkeeping where entries are recorded by debiting one or more accounts and crediting another one or more accounts with the same total amount

# Implementation

## Architecture
* CQRS/ES: [According to Martin Fowler](https://martinfowler.com/bliki/CQRS.html), CQRS stands for Command Query Responsibility Segregation. It's a pattern that I first heard described by [Greg Young](http://codebetter.com/gregyoung/).
![Command and Query Separation](https://martinfowler.com/bliki/images/cqrs/cqrs.png)

### Domain Driven Design (DDD) Bounded Contexts
* **Aggregate Root**: *Account, Book*
* **Entities**: *Lot, Period, Price, Security*
* **Value Object**: *JournalEntry, Transaction*

![Entity-Relationship Diagram](https://github.com/rafaelturon/blockchain-investments-docs/raw/master/images/er-ledger-diagram.png)

### Technical Details
**Server Side** - ASPNET Core backend with a restful API using Kestrel Web API Controllers:
* [ASP.NET Core](https://github.com/aspnet/Home) and C# for cross-platform server-side code
* CQRS/ES architecture using [CQRSLite](https://github.com/gautema/CQRSlite)
* Persistence layer using [MongoDB](https://github.com/mongodb/mongo)

## Build & development
Running `dotnet build` will build and `dotnet run` will present a preview.

## Testing
Running `dotnet test` will run the unit tests with xunit.

## Deployment
Running `dotnet publish` will run the deployment steps.
> Setting up `ASPNETCORE_ENVIRONMENT`, `MONGOLAB_URI` and `JWT_SECURITY_KEY` environment variables will be necessary in your deployment. [ASP.NET Core Environments](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments)
