# Mongoose Model Template v0.1.3

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

#### ExpressJS Setup
**1.** Using NPM, install three packages: `dzutils`, `moment`, and `mongoose`;  
**2.** Add two lines to the Express Generator `app.js` file:  
<pre>
var apiRouter = require('./routes/api');  
app.use('/api', apiRouter);
</pre>
**3.** Change the event handlers to:  
<pre>
// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
    app.use(function(err, req, res, next) {
        res.status(err.status || 500);
        res.send({
            msg: err.message,
            err: err
        });
    });
}

// production error handler
// no stacktraces leaked to user
app.use(function(err, req, res, next) {
    res.status(err.status || 500);
    res.send({
        msg: err.message,
        err: {}
    });
});
</pre>
**4.** Add the Mongoose configuration to the Express Generator `www` file:  
**This block assumes you have a configuration variable, called 'config,' with the necessary database settings.  Adjust appropriately for your environment:**
<pre>
/**
 * Mongoose
 */
var mongoose = require('mongoose');
var db = mongoose.connection;
db.on('error', console.error.bind(console, config.name + ' db error'));
db.once('open', function callback() {
    console.log(config.name + ' db opened');
});
mongoose.connect(config.db);
</pre>
**5.** Finally, run `npm install` to bring in the dependencies from Express Generator.  

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

---

Last updated: 2/25/2015 12:53:41 PM 
