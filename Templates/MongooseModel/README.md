# Mongoose Model Template v0.1.2

## Summary

### Background
Provides data logic methods and API access routes to simplify creating data access for NodeJS applications.  Design is done through SQL Server Diagram tool.  Generates Mongoose schemas for MongoDB and direct routes to each data call from an ExpressJS API route.  Allows for dynamically replacing lengthy variable names with shortened versions within the MongoDB database.

### Status
In development.  Initial creation uploaded to repository.  Testing beginning.

## Requirements

### General
SQL Server and CodeSmith Generator v7.0 is needed for generation.  Earlier versions of Generator may also work.  NodeJS and the Node Package Manager ("npm") must be installed to setup the generated code base.

### Development
Template designed to run in CodeSmith Generator, a Windows-only product.  Once generated the codebase should run in any environment that supports NodeJS.

### Production
Generated code based runs in any environment supporting NodeJS.  ExpressJS is needed if the API routes are used.

## Deviations
None.

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

---

Last updated: 2/23/2015 6:57:40 PM   
