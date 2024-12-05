# Create a minimal Orleans application
> Back to [Solution README](../README.md)

## About
This tutorial is a follow-along of the [Create a minimal Orleans application](https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/tutorial-1) tutorial on Microsoft Learn.

## Objectives
- Create a minimal Orleans application
- Understand the basic concepts of Orleans
- Learn how to create a simple Orleans application
- Learn how to run an Orleans application
- Learn how to test an Orleans application

## Projects
### [OrleansClient](./OrleansClient)
This project is the client that will interact with the Orleans cluster.
### [OrleansGrainInterfaces](./OrleansGrainInterfaces)
This project contains the interfaces for the grains.
### [OrleansGrains](./OrleansGrains)
This project contains the grains that will be hosted in the Orleans cluster.
### [OrleansSilo](./OrleansSilo)
This project is the host for the Orleans cluster.

## Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio Code](https://code.visualstudio.com/), [Visual Studio](https://visualstudio.microsoft.com/), [JetBrains Rider](https://www.jetbrains.com/rider/), or another IDE of your choice

## Running Locally
1. Run the [OrleansSilo](./OrleansSilo) project first.
2. Once the OrleansSilo project is running, run the [OrleansClient](./OrleansClient) project.
3. You should see the output from the OrleansSilo project in the terminal window. The output should be `SayHello message received: greeting = "Hi friend!"`
4. Confirm you alwos see output from the OrleansClient project in the terminal window. The output should be `Client said: "Hi friend!", so HelloGrain says: Hello!`'