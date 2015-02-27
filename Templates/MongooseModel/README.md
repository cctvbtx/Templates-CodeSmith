# Mongoose Model Template v0.1.4

## Summary

### Background
Creates MEAN Stack CRUD API web service from a SQL Server database (based on the standard express-generator).  This allows a model-first approach to creating MEAN Stack web services in that the modeleing can be done through tools such as SQL Server Diagram Tool.  All features of the global templates are supported (non-destructive deletes, audit trails, etc.).

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

**4.** Using CodeSmith Generator v7.0, generate the template.  This will generate two folders:

`%PROJECT NAME%`  
`%PROJECT NAME%.Proxies`

**5.** Change directory into the `%PROJECT NAME%` folder and install all required binaries using the following command: 

`npm install`

**6.** Initialize the web server using the following command.

`npm start`

Once running two sets of URLs will be available:

`http://%FQDN%:%PORT%`  (shows the index placeholder page)  
`http://%FQDN%:%PORT%/api/%OBJECT-NAME%`  (common RESTful paths used at this point)  

## Deviations
- Only UID and char (String) data types are used for keys.  Fixed length char fields, from SQL Server, are converted to String keys and must be supplied during any create operation.  All other types (ie. uniqueidentifier, bigint, int, etc.) are converted to String fields and automatically populated with a random UID during create.

## Future Goals
Need to improve with feedback from experienced Mongoose / MongoDB developers.  Should also add methods for paging and other advanced features.
- Enhance Update methods to check for collisions

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

### v0.1.4
- Complete ExpressJS web service built around routes and models.

---

Last updated: 2/25/2015 12:53:41 PM 
