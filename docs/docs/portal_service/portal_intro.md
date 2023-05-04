---
title: Introduction
description: Portal API Introduction
sidebar_position: 1
---

# Introduction

API Support: [nguyenxuannhan407@gmail.com](mailto:nguyenxuannhan407@gmail.com) | URL: [https://github.com/foxminchan/SaoVietPortal](https://github.com/foxminchan/SaoVietPortal) |
License: [MIT](https://opensource.org/license/mit/)

[Terms of Service](https://blogdaytinhoc.com/chinh-sach-chung-va-dieu-khoan-trung-tam-tin-hoc-sao-viet-222)

<p align="justify">
Sao Viet Portal is an open source platform designed to manage and organize student information for the Sao Viet. With this portal, students, teachers, and staff can easily access and update student records, such as attendance, grades, and personal information.
</p>

[#SaovietPortal](https://github.com/foxminchan/SaoVietPortal) [#TinhocSaoViet](https://blogdaytinhoc.com/)

## OpenAPI Specification

<p align="justify">
Sao Viet Portal uses the OpenAPI Specification (OAS) for describing its API. The OAS is a specification for machine-readable interface files for describing, producing, consuming, and visualizing REST web services. The specification is written in YAML and JSON and can be read on GitHub.
</p>

:::info
We also use **ReDoc** to generate API documentation from the OpenAPI Specification. ReDoc is a **OpenAPI/Swagger-generated** API Reference Documentation. It is available as a hosted version or as a self-hosted version.
:::

## Cross-Origin Resource Sharing (CORS)

<p align="justify">
Sao Viet Portal supports Cross-Origin Resource Sharing (CORS) in compliance with the <a href="https://www.w3.org/TR/cors/">W3C</a> specification. CORS is a mechanism that allows restricted resources on a web page to be requested from another domain outside the domain from which the first resource was served.
</p>

## Authentication

<p align="justify">
With backend for frontend (BFF) architecture, Sao Viet Portal uses the <a href="https://jwt.io/">JSON Web Token (JWT)</a> standard for authentication. The JWT standard defines a compact and self-contained way for securely transmitting information between parties as a JSON object. This information can be verified and trusted because it is digitally signed.
</p>

<p align="justify">
For bff for website, we use cookie to store token and OpenID Connect (OIDC) for authentication. OIDC is an authentication layer on top of OAuth 2.0, an authorization framework. The standard is controlled by the OpenID Foundation.
</p>

## Authorization

<p align="justify">
For authorization, Sao Viet Portal uses policy based authorization. Policy-based authorization is a flexible and extensible approach to authorization that is based on claims that are contained in tokens. The policy-based authorization model in ASP.NET Core is based on evaluating authorization requirements against the claims found in the current user's ClaimsPrincipal object.
</p>

Here is the list of roles and claims for policy based authorization:

<table>
    <thead>
        <th>Name</th>
        <th>Role</th>
        <th>Claim</th>
        <th>Description</th>
    </thead>
    <tbody>
        <tr>
            <td>Developer</td>
            <td></td>
            <td>[Technical, Developer]</td>
            <td>User can access all APIs in development environment</td>
        </tr>
        <tr>
            <td>Admin</td>
            <td>Staff</td>
            <td>Branch Manager, Teacher, Accountant, System Admin</td>
            <td>Advanced user can access all APIs in production environment</td>
        </tr>
        <tr>
            <td>Teacher</td>
            <td>Staff</td>
            <td>Teacher</td>
            <td>Teacher can access limited APIs for teaching</td>
        </tr>
        <tr>
            <td>Branch Manager</td>
            <td>Staff</td>
            <td>Branch Manager, Teacher, Accountant</td>
            <td>Advanced user can access limited APIs for managing branch</td>
        </tr>
        <tr>
            <td>Accountant</td>
            <td>Staff</td>
            <td>Teacher, Accountant</td>
            <td>Advanced user can access limited APIs for managing finance</td>
        </tr>
        <tr>
            <td>Student</td>
            <td>Student</td>
            <td></td>
            <td>Student can access limited APIs for viewing student information</td>
        </tr>
    </tbody>
</table>

## API Versioning

<p align="justify">
Sao Viet Portal uses the <a href="https://semver.org/">Semantic Versioning (SemVer)</a> standard for versioning its API. The SemVer standard defines a versioning scheme that uses a three-part version number: MAJOR.MINOR.PATCH. The basic idea behind semantic versioning is to assign specific meanings to each of the three version numbers:
</p>

- MAJOR version when you make incompatible API changes,
- MINOR version when you add functionality in a backwards compatible manner, and
- PATCH version when you make backwards compatible bug fixes.
- Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.