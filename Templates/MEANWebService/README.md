# MEAN Stack Web Service Template v0.3.3

## Summary

### Background
Creates MEAN Stack CRUD API web service, and proxy libraries for connecting to that web service, from a SQL Server database (based on the standard express-generator).  This allows a model-first approach to creating MEAN Stack web services in that the modeling can be done through tools such as SQL Server Diagram Tool.  All features of the global templates are supported (non-destructive deletes, audit trails, etc.).

### Details
One web service project and four resource projects are generated:

#### %PROJECT_NAME%
This is the main API application built on top of ExpressJS.  All npm scripts are run from this folder.

- **Note: The following folders are intended to be included in a separate project (for example, via SVN Externals) to connect back to the main API application.**

#### %PROJECT_NAME%.Business
Stub files meant to contain multi-step or over-reaching business logic.  Business operations call upon the service libraries.  The goal of these files is to provide higher-level operations which generally span multiple smaller service operations.

#### %PROJECT_NAME%.Module
Resource files used by other projects as stubs for AngularJS module and services.  Unlike the other subtemplates, where the table name is used to construct the object name, the module is generated one level higher using the table owner / schema name.

- **Note:  Only non-default owner names are generated!**

#### %PROJECT_NAME%.Proxies
Resource files used by other projects for connecting to this ExpressJS application.  Generally, only the Service libraries will ever call the proxy libraries, however each service file does have a proxy instance variable for convenience.

#### %PROJECT_NAME%.Services
Stub files meant to contain business logic.  These files already contain references to the Proxy files and are meant to be deployed along side the Proxies as a resource for a sister project.

### Features

#### Space Saving Names
Two map files are provided with the template: `DtoFieldNames.map` and `SchemaFieldNames.map`.  Using these maps allow you to model your database with plain English table names and field names, and to have these verbose names within business objects, but then generate smaller names for use in the database and data transfer objects (DTOs).  The overall goal is to conserve space in the MongoDB database and over the wire with API calls.  For example, the audit field "AuditVersionMemberId" is use while modeling the database but is shortened to "_m" with the Mongoose schema.

### Status
Basic CRUD functionality tested and complete.

## Requirements

### General
SQL Server and CodeSmith Generator v7.0 is needed for generation.  Earlier versions of Generator may also work.  NodeJS, the Node Package Manager ("npm"), and NodeMon must be installed to setup the generated code base.

### Development
Template designed to run in CodeSmith Generator, a Windows-only product.  Once generated the codebase should run in any environment that supports NodeJS.

### Production
Generated code based runs in any environment supporting NodeJS.

#### ExpressJS Setup
**1.** Install NodeJS following specific instructions for your operating system;  
**2.** Although optional, it is recommended that you upgrade NPM once NodeJS is installed:

`npm install -g npm`

**3.** Install NodeMon globally using the following command:

`npm install -g nodemon`

**4.** Using CodeSmith Generator v7.0, generate the template.  This will generate five folders:

`%PROJECT NAME%`  
`%PROJECT NAME%.Business`  (resource project)  
`%PROJECT NAME%.Module`  (resource project)  
`%PROJECT NAME%.Proxies`  (resource project)  
`%PROJECT NAME%.Services`  (resource project)

**5.** Change directory into the `%PROJECT NAME%` folder and install all required binaries using the following command: 

`npm install` or `sudo npm install`

**6.** Initialize the web server using the following command.

`npm start`

Once running two sets of URLs will be available:

`http://%FQDN%:%PORT%`  (shows the index placeholder page)  
`http://%FQDN%:%PORT%/api/%OBJECT-NAME%`  (common RESTful paths used at this point)  

## Deviations
- Only UID and char (String) data types are used for keys.  Fixed length char fields, from SQL Server, are converted to String keys and must be supplied during any create operation.  All other types (ie. uniqueidentifier, bigint, int, etc.) are converted to String fields and automatically populated with a random UID during create.

## Future Plans  
- Need to improve with feedback from experienced Mongoose / MongoDB developers.  
- Add methods for paging and other advanced features.  
- Enhance Update methods to check for collisions  
- Replace composite PK with unique constraint (currently skipping additional PK columns)  

## Bugs & Limitations
None at this time.

## Updates
### v0.1.1
- Added data index template (`index.data.js.cst`)
- Added master template

### v0.1.2
- Added route template (`route.js.cst`)
- Added route index template (`index.route.js.cst`)
- Enhanced validation within model (`model.js.cst`)

### v0.1.3
- Basic CRUD functionality tested and complete.

### v0.2.0
- Complete ExpressJS web service built around routes and models.  Proxy files added for connecting to web service from other ExpressJS applications.

### v0.2.1
- Added index for proxies to allow dot-notation navigation from root to individual proxy methods.

### v0.3.0
- Added seeding methods and configuration files.  Added `count` operations for model, routes, and proxy.

### v0.3.1
- Added stub files for service operations.  These files call upon the proxy files added in v0.3.0.  As with the proxy files, these files are meant to be included as "externals" within a separate project.

### v0.3.2
- Added stub files for AngularJS module.  These files are meant to be included as "externals" within a separate project.

### v0.3.3
- Added stub files for business operations.  These files are meant to be included as "externals" within a separate project.  **Needs to be rethought.**

---

Last updated: 7/1/2015 4:55:43 PM     

