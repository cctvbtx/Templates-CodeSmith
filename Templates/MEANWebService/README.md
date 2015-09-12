# MEAN Stack Web Service Template v0.4.4

## Summary

### Background
Creates MEAN Stack CRUD API web service, and proxy libraries for connecting to that web service, from a SQL Server database (based on the standard express-generator).  This allows a model-first approach to creating MEAN Stack web services in that the modeling can be done through tools such as SQL Server Diagram Tool.  All features of the global templates are supported (non-destructive deletes, audit trails, etc.).

### Details
One web service project and **seven** sets of resource files are generated:

#### %PROJECT_NAME%
This is the main API application built on top of ExpressJS.  All npm scripts are run from this folder.

- **Important: This is the only hosted project folder.  All other folders are intended to be included in a separate project (using a technology such as SVN Externals) to connect back to this main API application.**

- **Note: To increase clarity, the resource folders are listed here in order of priority and NOT alphabetically.**

#### %PROJECT_NAME%.Proxies
Main set of resource files used in a secondary application.  These proxies are responsible for validating all data calls, before going across the wire, as well as all actual communication with the main project.  Generally, only the Service libraries will ever call the proxy libraries, however each service file does have a proxy instance variable for convenience.

#### %PROJECT_NAME%.Services
Service calls are bundled together by the object type.  For example, a Widget service operation may encapsulate the work-flow of multiple data calls.  In this example, the Widget service file is used to call upon the Widget proxy file.  For this reason, one service file (stub) is generated for each object type.  Additionally, one Common service file is created at each level for those calls which are over-reaching within a namespace, and across object types, or those which are common to the entire project.

#### %PROJECT_NAME%.Business
Similar to how the Service files are used to encapsulate calls to each Proxy, the Business Layer contains a set of files which may be used to span multiple namespaces and contain multi-step or over-reaching business logic.  The goal of these files is to provide higher-level operations which generally span multiple smaller service operations.

#### %PROJECT_NAME%.Routes.Private
Within a typical API application, business and service operations are exposed via routes.  Route stubs provide a reusable mechanism for exposing this logic.  The Private routes generally contain those that are used in situations where security is less of a concern (ie, back office, corporate, DMZ, etc.).

#### %PROJECT_NAME%.Routes.Public
Similar to the Routes.Private folder above, but intended for use in a public-facing application.

#### %PROJECT_NAME%.Module.Private
Solutions needing a web-based user interface make use of AngularJS modules to call upon the Routes (which, in turn, call the service and business layers, etc.).  Each namespace has an associated AngularJS service.  And, each AngularJS service is wrapped within an AngularJS Module for the project.  These files complete the client-server communication sequence.  As with the Routes folder, the Private module generally contains operations used in secure or back-office scenarios where security is less of a concern.

#### %PROJECT_NAME%.Module.Public
Similar to the Module.Private folder above, but intended for use in a public-facing application.

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

**4.** Using CodeSmith Generator v7.0, generate the template.  This will generate eight folders:

`%PROJECT NAME%`  
`%PROJECT NAME%.Business`  
`%PROJECT NAME%.Module.Private`  
`%PROJECT NAME%.Module.Public`  
`%PROJECT NAME%.Proxies`  
`%PROJECT NAME%.Routes.Private`  
`%PROJECT NAME%.Routes.Public`  
`%PROJECT NAME%.Services`  

**5.** Change directory into the `%PROJECT NAME%` folder and install all required binaries using the following command: 

`npm install` or `sudo npm install`

**6.** Initialize the web server using the following command.

`npm start`

Once running two sets of URLs will be available:

`http://%FQDN%:%PORT%`  (shows the index placeholder page)  
`http://%FQDN%:%PORT%/api/%OBJECT-NAME%`  (common RESTful paths used at this point)  

#### Dependencies & Master Project Setup
Most of the folders generated are meant to be used within a second project, known as the "Master Project."  Within a system like Subversion (aka "SVN") this is generally done through Externals.  There are four dependent NPM projects which must be added to the master project.  To install these, execute the following command:

`npm install --save dzutils moment async request-json` 

**or**

`sudo npm install --save dzutils moment async request-json`


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

### v0.4.0 - v0.4.1
- Flushed out Business & Service classes to provide for over-reaching operations.

### v0.4.2
- Added Routes files for client deployment.

### v0.4.3
- Use of Public & Private folder structures introduced.

### v0.4.4
- Enhanced proxy & model functionality to increase DTO verbosity on creates as needed.

---

Last updated: 9/12/2015 4:06:17 PM    
      

