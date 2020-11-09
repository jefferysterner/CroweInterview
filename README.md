## CroweInterview

<p>
<ins>Notes</ins><br>
Running Crowe.Web and Crowe.Console display "Hello World" as required by making a REST call to Crowe.Api.<br>
Crowe.Data ultimately could be re-implemented with Repository pattern to further decouple the dbContext and database.<br>
</p>


<ins>Projects</ins><br>
**Crowe.Api:** exposes HelloWorld api, which returns default "Hello World", or takes in a message via query string, saves it to the database, and returns it for display.<br>
**Crowe.Api.Test:** MSTest unit tests to validate return of "Hello World" and saving of any querystring value passed (HttpGet used to save values instead of HttpPost for simplicity).<br>
**Crowe.Business:** contains the domain model.<br>
**Crowe.Console:** writes "Hello World" to the console output using api call.<br>
**Crowe.Data:** EF data context for Messages, uses (localdb)\mssqllocaldb without migrations, connection string loaded from config file (data seeded on Configure).<br>
**Crowe.Data.Test:** mostly trivial MSTest unit tests of CRUD functionality, tests combined into one TestMethod for brevity.<br>
**Crowe.Web:** AspNetCore web app displaying "Hello World" via the api.<br>
