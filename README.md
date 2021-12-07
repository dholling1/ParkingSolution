# Parking Solution
## Assumptions/Requirements
Following the problem statement and followup questions and answers, this is a simple solution to allow an unauthenticated user to navigate to the application page, select a parking lot, and increment & decrement the number of parking customers.  Since this is a coding exercise completed over a weekend, the following are not considered:
* User authentication or management
* Long term scalable data storage
* HTTPS (enabling developer certificates and managing them can lead to frustration given all of the current browser security lockdowns)
* Parking lot location management (CRUD)
* No extensive logging or telemetry support
* This is example code and does not represent full production code or even necessarily "best practices" as some of the implementation is contrived to show validation/error handling, etc.

## General
Initial projects created using .Net Core Web and .Net Core Web API templates.  Any tooling is added as NuGet packages and a <mark>dotnet restore</mark> should be run for the solution upon cloning the repo.  
Nuget packages used:
* XUnit
* Swagger
* EF Core
* Various others as required by the above

## Running
By default this is not "deployment ready" - it is designed to be run from Visual Studio 2019.  The API project is set up to run on port 5001, while the web app runs on port 5000.  The launch settings for viewing the Swagger UI interface is <mark>http://localhost:5001/swagger/index.html</mark>.  The methods are all based from <mark>http://localhost:5001/api/Parking</mark>.  

The code was developed and tested using the built-in kestrel server and the dev IIS (although VS just loves to reset it back to IIS for the web app).  In VS2019 you can set up the solution to run both projects at the same time.

## Domain Design
I chose a super slim domain model based on a POCO with a repository rather than a DDD Aggregate/Value model with commands and queries simply due to that being almost worthless on a problem domain of this complexity.  It should be noted that in this design I envision the repository having some business rules, but really due to issues such as locking or concurreny that really depends on the data storage classes and/or requires a MUCH bigger investment in time to factor out things like abstract locking and really... in the real world you would probably only have 1 implementation anyway.  Thus, the repository could probably be done away with and just use the data stores, but that also makes me feels dirty.  So conflicted here.  Sigh.

Look closely at Startup.cs in the Parking.Web project to see how the various data providers can be swapped out.

The EFCore in Parking.DataSource has a brush against concurrency in a DB context.  This is much more complex than can be adequately addressed in this weekend project, ut a super simple and horribly flawed implementation was provided.

## Testing
In the API controller tests I utilized the SimpleDictionary concrete class to save time from having to mock 3 variations of each call when the Simple Dictionary class already has all the same logic and is initialized with the same values each time.

## Further Discussion
Everything from actual user management and enterprise instrumentation to a real world persistent data store.  One thing I'd like to point out is that the data model needed for this test is so simple I did NOT manufacture scenarios requiring joins or anything complex in either the data model or the storage - it simply isn't needed for something so trivial.  Obviously if you had a scenario where you had a location with multiple lots you'd then have a 1-M for location => lot.  The SQL data storage option was shown since the scenario document specifically called out using an ORM.  Data storage in a real world scenarion would involve a lor of trade-offs for speed/reliability/cost etc.  A fully transacted clustered SQL Server implementation may be super reliable, but there's a lot of cost and overhead.  For even larger systems if real time information is not needed after every update you could implement a queuing system or pub/sub architecture and go for eventual consistency. 
