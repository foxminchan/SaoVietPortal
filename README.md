<h1 align="center">
	<a name="readme-top"></a>
	<p><img width=25% src="./resources/Assets/img/logo.jpg"></p>
Sao Viet Portal
</h1>

<p align="center">An open source portal built to manage student's information at Sao Viet.</p>

<p align="center">
	<img src="https://img.shields.io/badge/.NET%20Core-7.0-%238338ec?style=for-the-badge&logo=appveyor">
	<img src="https://img.shields.io/badge/Python-3.11-blue?style=for-the-badge&logo=appveyor">
	<img src="https://img.shields.io/badge/swagger-valid-green?style=for-the-badge&logo=appveyor">
	<a href="https://opensource.org/licenses/MIT"><img src="https://img.shields.io/badge/license-MIT-blue.svg?style=for-the-badge&logo=appveyor"></a>
	<a href="https://creativecommons.org/licenses/by/4.0/"><img src="https://img.shields.io/badge/license-CC--BY--4.0-blue.svg?style=for-the-badge&logo=appveyor"></a>
</p>

# Table of Contents

- [Table of Contents](#table-of-contents)
- [Overview](#overview)
  - [Introduction](#introduction)
  - [Features](#features)
  - [Timeline](#timeline)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
  - [Sao Viet Portal](#sao-viet-portal)
  - [Web Application](#web-application)
  - [Desktop Application](#desktop-application)
  - [Chatbot](#chatbot)
- [Documentation](#documentation)
- [Contributing](#contributing)
- [Sponsor](#sponsor)
- [Contact](#contact)
- [License](#license)
- [References](#references)

# Overview

## Introduction

<p style="text-align: justify">
The SaoViet Portal is a web and app-based platform designed for managers and administrators to efficiently manage student-related data and activities. The platform is intuitive, user-friendly, and accessible from anywhere, at any time. It features advanced security measures, customization options, and is designed to reduce paperwork, save time, and minimize errors.

<b>⚠️Note:</b> This project is currently in development. The project is a university graduation project, and is not intended to be used in production or for commercial purposes. The project is not affiliated with Sao Viet. HUTECH University is not responsible for any damage caused by the use of this project.

</p>

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Features

- [x] Manage student's information, including personal information, academic information, and financial information.
- [x] Manage teacher information, including their personal details, qualifications, and employment history
- [x] Manage the different branches of the center, including their location, contact details, and available courses
- [x] Keep track of the center's financial activities, including income and expenses
- [x] Generate statistical data and reports based on the information stored in the system
- [x] Provides users with interactive chatbot interface that can assist with common queries and tasks
- [ ] Follow the center's teachers and students `(requires IoT devices)`
- [ ] Manage the assets of the center, including equipment, furniture, and other resources `(requires Grantt chart)`
- [ ] Analyze the center's financial data and generate revenue predictions based on trends and patterns
- [ ] Provide more advanced chatbot capabilities, using the latest GPT-4 technology
- [ ] Store student data on a secure blockchain platform, ensuring the privacy and security of sensitive information

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Timeline

This project is currently in development. The following is the timeline of the project:

- [x] 2023-03-13: Project started.
- [x] 2023-03-14: Getting business requirements.
- [x] 2023-03-22: Designing the architecture and database.
- [ ] 2023-03-26: Developing the portal api.
- [ ] (TBD): Writing the api documentation.
- [ ] (TBD): Developing the portal web app.
- [ ] (TBD): Developing the portal application.
- [ ] (TBD): Researching and implementing chatbot.
- [ ] (TBD): Developing the chatbot.
- [ ] (TBD): Release the the portal.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Technologies

<p style="text-align: justify">
Sao Viet Portal utilizes various technologies to provide a robust and efficient platform for its users. The following are the technologies used in the application:
</P>

- .[NET Core 7.0](https://dotnet.microsoft.com/download/dotnet/7.0) - A free, cross-platform, open source developer platform for building many different types of applications.
- [Bicep](https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/overview) - A Domain Specific Language for deploying Azure resources declaratively.
- [HCL](https://www.terraform.io/docs/language/index.html) - A declarative language that is designed to describe infrastructure in a concise way.
- [FastAPI](https://fastapi.tiangolo.com/) - A modern, fast, web framework for building APIs with Python 3.6+ based on standard Python type hints.
- [Apache Lucene](https://lucene.apache.org/) - A high-performance, full-featured text search engine library written entirely in Java.

And many more...

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Architecture

<p style="text-align: justify">
The architecture of the application is designed to be highly scalable and flexible, which means that it can handle a large number of users and can easily adapt to changing needs and requirements. The following diagram shows the high-level components of the architecture:
</p>

<img src="./resources/Assets/img/overview-architecture.png" style="align: center;">

It showcases the following components:

- **Client**: The client includes the web application, desktop application, and mobile application.
- **BFF**: The BFF is a backend for frontend, which is responsible for handling the client's requests and forwarding them to the API.
- **API Services**: The API services are responsible for handling the requests from the BFF and the database.
- **OpenTelemetry Collector**: The OpenTelemetry Collector is responsible for collecting and processing telemetry data from the API services and the database.
- **External Services**: The external services are responsible for handling the requests from the API services and the database.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Getting Started

## Sao Viet Portal

💻 Install the following tools:

- [Install .NET Core 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Install Docker](https://www.docker.com/products/docker-desktop)
- [Install MS SQL Server 2022](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Redis](https://redis.io/download) (If you want run without docker)

🐳 In addition, you can set up the enviroment in container using docker:

> First, you need to pull the image named .net core 7.0 sdk from docker hub
>
> ```bash
> docker pull mcr.microsoft.com/dotnet/sdk:7.0
> ```
>
> Set up the SQL Server 2022
>
> ```bash
> docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123" -p 1433:1433 --name sql1 -d mcr.microsoft.com/mssql/server:2022-latest
> ```
>
> Set up the Redis
>
> ```bash
> docker run -p 6379:6379 --name redis -d redis
> ```
>
> Run the command to start the application
>
> ```bash
> docker run -it --rm -v ${PWD}:/app -w /app mcr.microsoft.com/dotnet/sdk:7.0 dotnet run --project api/portal/src/Portal.Api/Portal.Api.csproj
> ```
>
> **Note**: You can use the command `docker ps` to check the status of the container

For the database:

1. Install the **dotnet ef** tool: `dotnet tool install --global dotnet-ef`
2. Navigate to the `api/portal/src/Portal.Infrastructure` folder.
   - Clean up old migrations: `dotnet ef migrations remove --project Portal.Infrastructure.csproj --startup-project Portal.Api.csproj --force`
   - Create a new migration: `dotnet ef migrations add Initial --project Portal.Infrastructure.csproj --startup-project Portal.Api.csproj`
   - Update the database: `dotnet ef database update --project Portal.Infrastructure.csproj --startup-project Portal.Api.csproj`
3. Learn more about [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/).

**⚠️Note**: You can use backup files to restore the database. The backup files are located in the `api/portal/database` folder.

For JWT generation:

To initialize the keys for JWT generation, run `dotnet user-jwts` in to the `api/portal/src/Portal.Api` folder.

```bash
dotnet user-jwts create --claim "Technical=Developer"
```

Set up external services:

1. Navigate to the `api/portal/deploys/docker` folder.
2. Run `docker-compose up -d` to start the external services.

Install Tye for global tool using the following command:

```bash
dotnet tool install -g Microsoft.Tye
```

Run `tye run` in the repository root and navigate to the tye dashboard (usually http://localhost:8000) to see both applications running.

## Web Application

`Update later`

## Desktop Application

`Update later`

## Chatbot

`Update later`

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Documentation

- See the changelog at [here](./CHANGELOG.md).
- See documentation on how to use the API at [here](./docs/README.md).
- See the wiki at [here](https://github.com/foxminchan/SaoVietPortal/wiki).
- See code of conduct at [here](./CODE_OF_CONDUCT.md).
- See contributing guidelines at [here](./.github/CONTRIBUTING.md).
- See privacy policy at [here](./SECURITY.md).
- See the article about the project at [here](#).
- See the support at [here](./.github/SUPPORT.md).

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Contributing

<p style="text-align: justify">
We welcome contributions from the community. If you would like to contribute to this project, please read our <a href="./.github/CONTRIBUTING.md">contributing guidelines</a> for more information.
</p>

<table align="center">
  <tbody>
	<tr>
		<td align="center"><a href="https://github.com/foxminchan"><img src="https://avatars.githubusercontent.com/u/56079798?v=4?s=100" width="100px;" alt="Nguyen Xuan Nhan"/><br /><sub><b>Nguyen Xuan Nhan</b></sub></a><br /><a href="https://github.com/foxminchan/SaoVietAPI/commits?author=foxminchan" title="Code">💻</a></td>
		<td align="center"><a href="https://github.com/lycaphe8x"><img src="https://avatars.githubusercontent.com/u/3860060?v=4?s=100" width="100px;" alt="Nguyen Xuan Nhan"/><br /><sub><b>Nguyen Dinh Anh</b></sub></a><br /><a href="#" title="Guide">🧑‍🏫</a></td>
	</tr>
  </tbody>
</table>

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Sponsor

I'm looking for sponsors to help me maintain and develop this project.

If you are interested in sponsoring this project, please contact me at [here](mailto:nguyenxuannhan407@gmail.com) or use button `Sponsor` at the top of this page.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Contact

If you have any questions, please contact me at [here](mailto:nguyenxuannhan407@gmail.com)

For contact to my instructor, please contact him at [here](mailto:nd.and@hutech.edu.vn)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# License

<p style="text-align: justify">
The Sao Viet Portal project is governed by two distinct licenses. The first license, the MIT License, applies to the codebase of the project. The MIT License permits the use, modification, and distribution of the codebase under certain conditions. The full terms of the MIT License can be found in the <a href="./LICENSE">MIT</a> file.

The second license, the Attribution 4.0 International License, applies to the documentation of the project. This license permits the use, reproduction, and distribution of the documentation provided that proper attribution is given to the original authors. The full terms of the Attribution 4.0 International License can be found in the <a href="./LICENSE-docs">LICENSE-docs</a> file.

Sao Viet Portal is not affiliated with Microsoft, Azure, or any other Microsoft products. All product are developed and copyrignt by <a href="https://github.com/foxminchan">Nguyen Xuan Nhan</a> and <a href="https://github.com/lycaphe8x">Nguyen Dinh Anh</a>.

<a href="mailto:nd.and@hutech.edu.vn">Nguyen Dinh Anh</a>, a lecturer at `Hutech University` is co-owner of this project. The Faculty of Information Technology at `Hutech University` will be will act as the project evaluation and grading committee.

For more information about the project's copyright, please refer to the <a href="./COPYRIGHT.txt">COPYRIGHT</a> file. All rights reserved.

</p>

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# References

- [ASP.NET Core in Action, Third Edition (MEAP v06)](https://www.manning.com/books/asp-net-core-in-action-third-edition)
- [Web Development with Blazor - Second Edition](https://www.packtpub.com/product/web-development-with-blazor-second-edition/9781803241494)
- [Introducing .NET MAUI: Build and Deploy Cross-platform Applications Using C# and .NET Multi-platform App UI](https://www.oreilly.com/library/view/introducing-net-maui/9781484292341/)
- [High-Performance Web Apps with FastAPI: The Asynchronous Web Framework Based on Modern Python](https://www.oreilly.com/library/view/high-performance-web-apps/9781484291788/)
- [Infrastructure as Code with Azure Bicep: Streamline Azure resource deployment by bypassing ARM complexities](https://www.oreilly.com/library/view/infrastructure-as-code/9781801813747/B17805_FM_Epub.xhtml)
- [Beginning HCL Programming: Using Hashicorp Language for Automation and Configuration](https://www.oreilly.com/library/view/beginning-hcl-programming/9781484266342/)

Some useful links:

- [Azure DevOps](https://azure.microsoft.com/en-us/services/devops/)
- [Terraform Language Documentation](https://www.terraform.io/docs/language/index.html)
- [DevOps for ASP.NET Core Developers](https://docs.microsoft.com/en-us/dotnet/architecture/devops-for-aspnet-developers/)
- [ASP.NET Core 6](https://www.pluralsight.com/paths/aspnet-core-6)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
