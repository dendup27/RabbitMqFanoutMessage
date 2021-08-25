## ASP.NET Core (.net 5) with PostgreSQL, MassTransit and RabbitMQ Demo
Application demo designed to show how ASP.NET Core microservices can communicate with each other using service bus (MassTransit) and message broker (RabbitMq).
The app uses PostgreSQL and Entity Framework Core to create a simple database necessary for this demo.

## RabbitMQ Exchange Types
RabbitMQ has four different types of exchanges: Direct, Topic, Fanout, Headers.
Each exchange type routes the message differently using different parameters and bindings setups. Clients can either create own exchanges or use the predefined default exchanges. Exchange can have bound queues or exchanges. For this application demo, <b>*Fanout*</b> exchanges types is used.

## What is the *Fanout* Exchange Types?
Fanout is a messaging design where the publisher (producer) publishes message to multiple different subscribers (consumer) independently and simultaneously.
The intention is that the same published message will be consumed by different consumers and be processed in different ways.
In a message broker like RabbitMQ, the publisher publishes a message which is put into different queues through a special type of exchange called “Fanout Exchange”.
Consumers listening to these queues get the same message to be consumed.
The below diagram shows a diagrammatic representation of *Fanout* Exchange types.
![FanoutPattern](https://user-images.githubusercontent.com/26617310/130724572-133ec1db-28b5-4d27-9d7e-06a2a853a8cf.png)

## When to Use *Fanout* Exchange Types?
The use case of fanout pattern is applicable in places where a publisher needs to asynchronously communicate to multiple consumers on a single workload.

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:
* Visual Studio 2019 or later
* .Net Core 5.0 or later

### Installing
Follow these steps to get your development environment set up:
1. Clone the repository and at the root directory, restore required packages by running:
dotnet restore
1. Build the solution by running:
dotnet build
1. Within the root directory of each microservice, launch the back end by running:
dotnet run

### For Visual Studio 2019 or later
Open solution with your IDE after cloning. To run multiple application, follow the below steps:
1. *Right-click* on the solution folder *RabbitMqFanoutMessage* and select the *Properties* option.
2. Click *Startup Project* under the *Common Properties".
3. Select *Multiple startup projects*.
4. Select *start* from the *Action* dropdown for each project.
5. Then press the *Apply* and *Ok*.
6. Ready to run the application.


## Technologies
* .NET Core 5.0
* ASP.NET Core 5.0
* Microsoft.EntityFrameworkCore 5.0
* MassTransit 7.2
* MassTransit.AspNetCore 7.2
* MassTransit.RabbitMQ 7.2
* AutoMapper 8.1

## Architecture
* Microservice Architecture
* Full architecture with responsibility separation of concerns
* SOLID and Clean Code
* Unit of Work
* Repository and Generic Repository

## Disclaimer
This repository is not intended to be a definitive solution. Beware to use in production way.

## Author
* Tashhi Denddup - [dendup27](https://github.com/dendup27)
> Check also gihtub page of repository [here](https://github.com/dendup27)

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/dendup27/RabbitMqFanoutMessage/blob/master/LICENSE) file for details.
