# DvdLibrary

### **Description**:
A full-stack application which creates a virtual dvd library
<br> Built with: TSQL, C#, ASP.NET, HTML, CSS, Bootstrap, Javascript, jQuery

Check out a live demo [here](https://dvd-library-app.herokuapp.com/)!

### **Updated Feb 24th 2022**
- Refactored scripts from 4 sql files into 1 postgresql file
- The postgresql file gets read and resets the postgresql database every hour
- All original files are still included 

### **Information**:
The application hosts three parts:
1. Scripts: A collection of TSQL scripts to create the database
2. API: The C# / ASP.NET API to connect with the created database
3. Client: An HTML / Javascript user-interface for front-end integration

#### **Scripts**:
Created and ran with SQL Server Management Studio (SSMS)
<br> Scripts are rerunnable and must be ran in the following order:
1. DvdLibrary.Create.sql 
2. DvdLibrary.SampleData.sql
3. DvdLibrary.Sprocs.sql
4. DvdLibrary.Security.sql

Create: &emsp;&emsp;&emsp;&ensp;Creates the database and database table
<br> SampleData: &emsp;Populates the database table with mock data
<br> Sprocs: &emsp;&emsp;&emsp;&nbsp;Creates stored procedures for accessing the database
<br> Security: &emsp;&emsp;&ensp;&nbsp;Creates login, password, and user then grants user access to stored procedures

#### **API**:
Created and ran with Visual Studio 2019
<br> Additional information:
- API is configured to use CORS
- Uses ADO .NET to store and retrieve data from SQL database
- By changing the value of key "Mode" in Web.config different implementations can be used
<br> One being for the mentioned ADO implementation and one for a Mock data implementation

#### **Client**:
Created with Visual Studio 2019
<br> Additional information:
- User-Interface for the created API and Database
- Displays dvd database and information
- Selecting a search category and entering a search term allows users to search the database
- User's are able to view, add, edit, or delete dvds directly impacting the database
