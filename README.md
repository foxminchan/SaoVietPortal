<h1 align="center">
	<a name="readme-top"></a>
	<p><img width=25% src="./resources/Assets/Logo.jpg"></p>
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
- [API development](#api-development)
- [Getting Started](#getting-started)
	- [💻 Infrastructure](#-infrastructure)
	- [🛠️ Backend](#️-backend)
	- [🖥️ Frontend](#️-frontend)
	- [📦 Optional tools](#-optional-tools)
- [OpenAPI](#openapi)
- [CI/CD Pipeline](#cicd-pipeline)
- [Monitoring, Logging, Tracing and Health Check](#monitoring-logging-tracing-and-health-check)
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
- [Pytorch](https://pytorch.org/) - An open source machine learning framework that accelerates the path from research prototyping to production deployment.

And many more...

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Architecture

<p style="text-align: justify">
The architecture of the application is designed to be highly scalable and flexible, which means that it can handle a large number of users and can easily adapt to changing needs and requirements. The following diagram shows the high-level components of the architecture:
</p>

<p align="center">
  <img src="./resources/Assets/SaoVietArchitecture.png">
</p>

<table align="center">
	<thead>
		<th>Name</th>
		<th>UseCase</th>
		<th>Technology</th>
	</thead>
	<tbody>
		<tr>
			<td><b>User</b></td>
			<td>N/A</td>
    	<td>N/A</td>
		</tr>
		<tr>
			<td><b>Client</b></td>
			<td>A client is a user who uses the application with a web browser or mobile app and desktop app to access the application.</td>
			<td>Blazor Server, .NET MAUI</td>
		</tr>
		<tr>
			<td><b>Back end for Front end</b></td>
			<td>A back end for front end is a server that is used by the client to access the application. It is responsible for handling requests from the client and returning responses to the client.</td>
			<td>ASP.NET Core, Duende IdentityServer 4</td>
		</tr>
		<tr>
				<td><b>Server</b></td>
				<td>A server is a computer that is used to host the application. It is responsible for handling requests from the client and returning responses to the client. It is also responsible for storing data in a database.</td>
				<td>ASP.NET Core, FastAPI, Kafka, etc.</td>
		</tr>
		<tr>
				<td><b>Database</b></td>
				<td>A database is a collection of data that is stored in a computer. It is used to store data in a structured way so that it can be easily accessed and manipulated.</td>
				<td>SQL Server 2022, Apache Lucene, Redis</td>
		</tr>
		<tr>
				<td><b>Opentelemetry</b></td>
				<td>Opentelemetry is a set of open source tools that are used to monitor and trace applications. It is used to collect metrics and traces from the application and send them to a monitoring system.</td>
				<td>Zipkin, Jaeger, Prometheus, Grafana, ELK Stack, Seq</td>
		</tr>
		<tr>
				<td><b>Email System</b></td>
				<td>An email system is a system that is used to send emails. It is used to send emails to users.</td>
				<td>Microsoft exchange server</td>
		</tr>
	</tbody>
</table>

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# API development

<p align="center">
  <img src="./resources/Assets/CleanArchitecture.png">
</p>

Learn more about the Clean Architecture [here](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture).

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Getting Started

## 💻 Infrastructure

- **[`Windows 11`](https://www.microsoft.com/en-us/windows/windows-11)** - The operating system for developing and building this application.
- **[`WSL 2 - Ubuntu 22.04.2 LTS`](https://docs.microsoft.com/en-us/windows/wsl/install)** - The subsystem for Linux to run Linux on Windows.
- **[`Docker Desktop (Kubernetes enabled)`](https://www.docker.com/products/docker-desktop)** - The containerization platform to run the application.
- **[`Kubernetes`](https://kubernetes.io/)** / **[`AKS`](https://azure.microsoft.com/en-us/services/kubernetes-service/)** / **[`Nomad`](https://www.nomadproject.io/)** - The container orchestration system to deploy the application.
- **[`helm`](https://helm.sh/)** - The package manager for Kubernetes to install the application.
- **[`tye`](https://github.com/dotnet/tye)** - The tool to run the application locally.

## 🛠️ Backend

- **[.NET Core 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)** - A free, cross-platform, open source developer platform for building many different types of applications.
- **[`Duende IdentityServer 6`](https://duendesoftware.com/products/identityserver)** - A free, open source OpenID Connect and OAuth 2.0 framework for ASP.NET Core.
- **[`EF Core`](https://docs.microsoft.com/en-us/ef/core/)** - A modern object-database mapper for .NET.
- **[`OpenTelemetry`](https://opentelemetry.io/)** - A set of open source tools for monitoring and tracing applications.
- **[`Serilog`](https://serilog.net/)** - A diagnostic logging library for .NET applications.
- **[`FluentValidation`](https://fluentvalidation.net/)** - A popular .NET library for building strongly-typed validation rules.
- **[`AutoMapper`](https://automapper.org/)** - A convention-based object-object mapper.
- **[`FastAPI`](https://fastapi.tiangolo.com/)** - A modern, fast (high-performance), web framework for building APIs with Python 3.6+ based on standard Python type hints.
- **[`Pytorch`](https://pytorch.org/)** - An open source machine learning framework that accelerates the path from research prototyping to production deployment.
- **[`Kafka`](https://kafka.apache.org/)** - A distributed streaming platform.

## 🖥️ Frontend

- **[`Blazor`](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)** - A free, open source framework for building client web apps with .NET.
- **[`.NET MAUI`](https://dotnet.microsoft.com/en-us/apps/maui)** - Build native, cross-platform desktop and mobile apps all in one framework.
- **[`Docusuarus`](https://docusaurus.io/)** - Build optimized websites quickly, focus on your content.

## 📦 Optional tools

- **[`Visual Studio 2022`](https://visualstudio.microsoft.com/vs/)** - The IDE for developing and building this application.
- **[`SQL Server 2022`](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)** - The relational database management system to store data.
- **[`Laragon`](https://laragon.org/)** - Modern & Powerful - Easy Operation.
- **[`Resharper`](https://www.jetbrains.com/resharper/)** - The Visual Studio extension for .NET developers.
- **[`PyCharm`](https://www.jetbrains.com/pycharm/)** - The Python IDE for Professional Developers.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# OpenAPI

> TODO

For details on how to use the API, see the [Docusaurus](https://foxminchan.github.io/SaoVietPortal/) documentation.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# CI/CD Pipeline

> TODO

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Monitoring, Logging, Tracing and Health Check

> TODO

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Documentation

- See the changelog at [here](./CHANGELOG.md).
- See documentation on how to use the API at [here](https://foxminchan.github.io/SaoVietPortal/)
- See the wiki at [here](https://github.com/foxminchan/SaoVietPortal/wiki).
- See code of conduct at [here](./CODE_OF_CONDUCT.md).
- See contributing guidelines at [here](./.github/CONTRIBUTING.md).
- See privacy policy at [here](./SECURITY.md).
- See all articles about the project at [here](./resources/articles/).
- See the support at [here](./.github/SUPPORT.md).

<p align="right">(<a href="#readme-top">back to top</a>)</p>

# Contributing

<p style="text-align: justify">
We welcome contributions from the community. If you would like to contribute to this project, please read our <a href="./.github/CONTRIBUTING.md">contributing guidelines</a> for more information.
</p>

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key))

<table align="center">
  <tbody>
    <tr>
      <td align="center">
        <a href="https://github.com/foxminchan">
          <img src="https://avatars.githubusercontent.com/u/56079798?v=4?s=100" width="150px;" alt="Nguyen Xuan Nhan" />
          <br />
          <sub>
            <b>Nguyen Xuan Nhan</b>
          </sub>
        </a>
        <br />
				<span title="Ideas, Planning, & Feedback">🤔</span>
        <a href="https://github.com/foxminchan/SaoVietAPI/commits?author=foxminchan" title="Code">💻</a>
				<span title="Documentation">📖</span>
				<span title="Tests">⚠️</span>
				<span title="Maintenance">🚧</span>
				<span title="Bug reports">🐛</span>
      </td>
      <td align="center">
        <a href="https://github.com/lycaphe8x">
          <img src="https://avatars.githubusercontent.com/u/3860060?v=4?s=100" width="150px;" alt="Nguyen Xuan Nhan" />
          <br />
          <sub>
            <b>Nguyen Dinh Anh</b>
          </sub>
        </a>
        <br />
        <span title="Guide">🧑‍🏫</span>
				<span title="Business">💼</span>
				<span title="User testing">📓</span>
      </td>
    </tr>
  </tbody>
</table>

Wanna be here? [Contribute](./.github/CONTRIBUTING.md).

- Fork this repository.
- Create your new branch with your feature: `git checkout -b my-feature`.
- Commit your changes: `git commit -am 'feat: My new feature'`.
- Push to the branch: `git push origin my-feature`.
- Submit a pull request.

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
