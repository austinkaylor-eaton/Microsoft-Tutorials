# Build your first Orleans app with ASP.NET Core
> Back to [Solution README](../README.md)

## About
This tutorial is a follow-along of the [Quickstart: Build your first Orleans app with ASP.NET Core](https://learn.microsoft.com/en-us/dotnet/orleans/quickstarts/build-your-first-orleans-app?tabs=visual-studio) tutorial on Microsoft Learn.
In this tutorial, you use Orleans and ASP.NET Core 8.0 Minimal APIs to build a URL shortener app. 

Users submit a full URL to the app's /shorten endpoint and get a shortened version to share with others, who are redirected to the original site. The app uses Orleans grains and silos to manage state in a distributed manner to allow for scalability and resiliency.

## Objectives
- Learn how to add Orleans to an ASP.NET Core app
- Learn how to work with grains and silos
- Learn how to configure state management
- Learn how to integrate Orleans with API endpoints

## Projects
###  [OrleansURLShortener](./OrleansURLShortener)
This ASP.NET Core Web API project is the main project for the URL shortener app. It contains the API endpoints for the app and the Orleans configuration.
## Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio Code](https://code.visualstudio.com/), [Visual Studio](https://visualstudio.microsoft.com/), [JetBrains Rider](https://www.jetbrains.com/rider/), or another IDE of your choice

## Running Locally
1. Start the project in the IDE or by running `dotnet run` in the terminal.
2. Navigate to either of the following URLs in your browser:
   - https://localhost:7165
   - http://localhost:5164
3. In the browser address bar, test the shorten endpoint by entering a URL path such as {localhost}/shorten?url=https://learn.microsoft.com. The page should reload and provide a shortened URL. 
4. Copy the shortened URL to your clipboard
5. Paste the shortened URL into the browser address bar and press Enter. The browser should redirect you to the URL that you intended to shorten. For example, if you followed the instructions exactly, you'd be redirected to the Microsoft Learn homepage.