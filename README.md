# Async Message Processor 

## Overview
This project is an async message processing system built using .NET Worker Services and Azure Service Bus. This app is designed to take messages from a queue, route them, and execute business logic. 
In this project I am focusing on architecture, and a clean seperation of responsibilities.

## Goals
- Apply Clean Architecture in a service like this
- Keep message transport and business logic seperate
- Make the project scalable
- Make it ready to integrate into Azure later

## Architecture
![picture of the architecture of this project](./assets/architecture.png)
