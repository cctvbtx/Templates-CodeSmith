> Scroll to the bottom for a complete list of templates and their description.

### Author & Usage
Templates are a normal part of my day.  I've been writing code for decades and loathe doing work twice.  Over time I plan to extract many portions of my mission-critical templates, or their entirety, and add them here.  Feel free to use, contribute, or comment at will.  I'm always around if you need a hand or have a suggestion.  

Thanx,  
Fred Lackey  
[fred.lackey@gmail.com](mailto:fred.lackey@gmail.com?subject=Templates-Codesmith%20Feedback)

### Lowest Common Developer
Most templates and snippets were written for legibility and comprehension by an entire development team.  Please follow this if you decide to contribute.  Junior developers need to be able to understand the syntax just as well as senior developers and architects.  This might mean separating out chunks of code into include files, adding large comment blocks or headers, and using less shorthand.  For example, none of the utility classes use RegEx and, instead, walk the reader through the process of analyzing and working with the data.  This notion helps increase both team understanding as well as the chance that the entire team might contribute or help tackle issues.

---

### Conventions

#### No Ad-Hoc Queries  
To help protect the database against costly and destructive queries, the templates generate queries based on indicies.  Generally, one repository method is generated for each index within the database.  Unique constraints cause singular methods to be generated (i.e. `findOne()`) whereas non-unique indices return collections (i.e. `find` or `findMany()`).

#### Unique Constraints Over No Composite Keys 
None of the templates support (or will support) composite keys.  If any error handling is added to protect against this it is minor and legacy.

#### Non-Destructive Deletes
The use of a "deleted flag" is encouraged and supported on each table.  If present on a table, three operations are generated for each:

* **Delete (Non-Destructive)**  
Records are *flagged* as "deleted".  Queries exclude these records by default.

* **Purge (`purgeOne()` / `purgeAll()`)**  
Permanently removes records previously flagged as "deleted".

* **Kill (`killOne()` / `killAll()`)** (always generated)  
Permanently removes records regardless of deleted status.


#### Auditing
While some templates offer more verbose audit trails, the use of audit columns is encouraged and supported on each table.  At a minimum, the following audit columns are supported:

* **Version User (e.g. "AuditVersionUser", "_u")**  
ID of the user who created or last updated the record.  Create and Update repository operations allow for the value to be supplied. 

* **Version Date (e.g. "AuditVersionDate", "_v")**  
Datetime stamp of the last change to the record.

* **Deleted Flag (e.g. "AuditDeletedDate", "IsDeleted", "_d")**  
Datetime stamp showing when the record was deleted.  Boolean values are also acceptable when overhead is a concern.


#### Versioning
Whenever possible the templates maintain versions of work.  Internal data uses version dates (see "Auditing" above).  Generated files are usually stored in a folder within the output folder you select.  While this can certainly bloat your development workstation if not maintained, more often than not this provides an additional measure of safety from lost work product.  This also provides a simple avenue to gauge the impact of your enhancements over time.

#### Public Identifiers
To help obfuscate the identifiers floating around on the interwebs, most of the templates optionally make use of a UUID / GUID identifier column.  Should your primary key be easy to guess or spoof, adding this column to a table will cause the templates to generate separate "identifier" methods (`findByUid()`, etc.) and allow the UUID to be used in RESTful queries.  This helps keep your keys human-readable and easier to deal with on the back end while decreasing the chance of someone spoofing or guessing the keys.

#### Grouping by Owner / Schema Name
Make use of the table owner!  Most templates make use of the non-default owner names to group, partition, or segment functionality.  For example, several of the full stack templates generate separate sets of projects for each non-default owner which only include the objects from that schema or owner.  At a minimum, namespaces and output folders are usually generated to group related files together.

#### Complex Objects / Stored Procedures / Views / Functions
Most of the templates are created in hopes of moving between multiple database platforms or, at a minimum, being database agnostic.  For this reason the code base is generally leaned upon more heavily than the database.  It is uncommon for templates to make use of a feature (stored procedures, custom functions, etc.) that may not exist (or may be painful to implement) on multiple database platforms.  In most cases, Views generate read-only versions of composite objects or their operations (get, find, etc.) while transactions account for their creation when applicable. 

---

### Column Rules, Names, and Functionality
Please keep in mind these rules are general for all templates.  Although they are desirable, not all rules are implemented for every template.  They are included here to provide an overall set of goals and line of thinking to govern the development of templates.  Any template not employing these rules will clearly state the exclusion and reasoning for doing so.

#### Primary Keys
Composite primary keys are not supported nor will they ever be.  Tables should have a single primary key column.  Tables that do not have a single primary key column will have certain operations excluded during generation.  The name of the PK column is irrelevant to the templates.

- In some situations, the output name of the PK is automatically overridden.  For example, templates working with the MongoDB database will output `_id` as a column name regardless of the source database.

#### Identifier Columns
Many people prefer to use automatically incremented numeric values for primary keys.  Unfortunately, this creates a security hole for public methods in that it becomes easy for rouge individuals to guess alternative key values.  The presence of an identifier column generally forces templates to ignore the primary key, and instead use the identifier column, for public facing operations.  It is assumed the identity column will be a UUID, GUID, or similar value which is difficult to predict.

- Identifier columns must be named one of the following: `Identifier`, `PublicIdentifier`, `PublicUid`, `uid`
- Be sure to properly index your identifier columns!  Include the Delete Flag column if it exists in your table.

#### Indices & Queries
Generally speaking, the only query operations generated are based off of the existence of an index.  Data repositories and web services will generate a single "find" or "get" operation for each index a table.  The goal is to help teams make predicable / safe calls to the database and to force the creation of indicies.

#### Auditing
The use of audit columns is optional but encouraged.  Three columns are usually supported:

##### Delete Flag (boolean or date)
Indicates if (and when) the record has been deleted virtually.  If set to either a date or true (depending on the data type) the record will be excluded from the result set of query operations.  Should the column exist on, data repository methods for that table will subsequently contain an `includeDeleted` option to allow the records to be manually included.

- Adding a delete flag column to your tables causes "purge" operations to be generated for data repositories.  This combination provides a two-step approach to permanently destroying data.
- Cascading deletes, if desired, are the responsibility of the database.  Generally, developers will need walk through the dependency chain using the generated operations.
- Delete flag columns must be named one of the following: `AuditDeletedDate`, `_k`, `DateDeleted`, `IsDeleted`

##### Version User
The ID of the user whom created, deleted, or last updated the record.  If present, data operations will require this value be supplied for these types of operations.

- Version user columns must be named one of the following: `AuditMemberUid`, `_u`

##### Version Date
The date and time when the record was created, deleted, or last updated.  The data repository is responsible for injecting this date.

- Version user columns must be named one of the following: `AuditVersionDate`, `_d`, `AuditDateCreated`

---

### Catalog (Currently Published: 3)

#### JSON Schema
Generates either a JSON or JavaScript representation of the database.  Useful to decouple CodeSmith from the generation process; for example, to create a secondary generator in a NodeJS environment.  Template does contain some basic properties which allow the template to either be generated by itself or as a resource template within another process.

#### MEAN Web Service
Simple data web service.  Created as an academic exercise while learning MEAN Stack development.  Primary goal when creating was to leverage the SQL Diagram tool to visually / quickly prototype Mongoose and Express resources.  Hopefully this will let fellow Microsoft and .NET developers kick the tires on the MEAN Stack more easily.

#### Mongoose Model
Provides data logic methods for simplifying data access.  Allows for dynamically replacing lengthy variable names with shortened versions within the MongoDB database.  Expected use is within an ExpressJS application from `express-generator`.

---

Last updated: 2/23/2015 6:38:58 PM 