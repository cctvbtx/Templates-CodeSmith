# MEAN Web Service Template v0.1.0

## Summary

### Background
Quick prototyping of web service from SQL Server using MEAN Stack.

### Status
In development.  Initial creation uploaded to repository.  Testing beginning.

## Requirements

### General
CodeSmith Generator v7.0 is needed for generation.  Earlier versions may also work.  NodeJS and the Node Package Manager ("npm") must be installed to setup the generated code base.

### Development
Template designed to run in CodeSmith Generator, a Windows-only product.  Once generated the codebase should run in any environment that supports NodeJS.

### Production
Generated code based runs in any environment supporting NodeJS.

## Deviations

### Authentication
Generated code base uses jwt-simple to decode the Authorization header and assumes the user ID is valid for all operations.  A custom JWT secret may be set in the generated `constants.js` file.

## Future Goals
None.  Initial release needs to be completed and tested.

---

Last updated: 1/16/2015 3:40:21 PM 
