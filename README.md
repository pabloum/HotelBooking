# Readme

This is an API built for a challenge. You may read the challenge in the next section. You may also find instructions to run the api locally


## How to Run the App

### What you need
Locally you need to install Visual Studio , and .Net Core 7.0, SQL Server. 

### Data 
You can use In Memory Data or an Actual Database, by setting the key "UseDataBase" in the appsettings.json to either True or False.

#### InMemory Data

This was added only for developing and testing purposes. If you use this approach, the data will not persist after you stop running the application, and will be resestablish to the same default values everytime the app is executed. Use this approach if you don't want to (or cannot) create a local database.

#### Database
If you want to use the database you can Publish the database project in your local machine. SQL Server was the database engine chosen for this project. Note that a PostDeployment script will insert default Data.

Take into account that tha .sqlproj is independent from the dotnet solution. 

### What to expect
Once you build it and run it, you may be able to see a Swagger UI page. 


## CHALLENGE

### Post-Covid scenario:

People are now free to travel everywhere but because of the pandemic, a lot of hotels went bankrupt. Some former famous travel places are left with only one hotel.

You’ve been given the responsibility to develop a booking API for the very last hotel in Cancun.

The requirements are:

- API will be maintained by the hotel’s IT department.
- As it’s the very last hotel, the quality of service must be 99.99 to 100% => no downtime
- For the purpose of the test, we assume the hotel has only one room available
- To give a chance to everyone to book the room, the stay can’t be longer than 3 daysand can’t be reserved more than 30 days in advance.
- All reservations start at least the next day of booking,
- To simplify the use case, a “DAY’ in the hotel room starts from 00:00 to 23:59:59.
- Every end-user can check the room availability, place a reservation, cancel it or modify it.
- To simplify the API is insecure.
