# S.E.V.E.N. HealthCare

A healthcare information and management web app built with **ASP.NET MVC 5** and **Entity Framework 6**. Visitors can browse the hospital's doctors, read the latest notices and send enquiries. Administrators manage doctors, notifications and enquiries from a dedicated admin zone.

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-512BD4)
![ASP.NET MVC](https://img.shields.io/badge/ASP.NET%20MVC-5.2.9-5C2D91)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-6.5.1-512BD4)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Express-CC2927)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3.8-7952B3)

**Live demo:** https://healthcare-priyanshu-g9c7cbfgeugvb4ht.centralindia-01.azurewebsites.net/

---

## Features

### Public site
- **Home** with a scrolling ticker of the three latest notifications, an image carousel, the mission statement and a services section (Lab Testing, Vaccination, General Check-Up)
- **Doctors** page listing every doctor with specialization and experience
- **Contact Us** enquiry form (name, email, mobile, message) saved to the database, with a confirmation page
- **About Us** and **Developer Team** pages
- **Admin login** at `/General/AdminLogIn`

### Admin zone
- **Dashboard** with shortcuts to every admin task
- **Notifications:** add, list and delete notices shown on the home page ticker
- **Doctors:** add, list and delete doctor profiles
- **Enquiries:** view and delete messages submitted through the contact form

## Tech stack

| Layer | Technology |
| --- | --- |
| Framework | ASP.NET MVC 5.2.9 on .NET Framework 4.7.2 |
| Views | Razor (`.cshtml`) with shared layouts for the public and admin areas |
| Data access | Entity Framework 6.5.1, Database First (`Model1.edmx`) |
| Database | Microsoft SQL Server (Express or LocalDB) |
| Front end | Bootstrap 5.3.8, Bootstrap Icons 1.13.1, jQuery 3.7.1 (loaded from CDNs) |
| Hosting | Azure App Service (Central India) |

## Getting started

### Prerequisites
- Windows with **Visual Studio 2019 or 2022** and the *ASP.NET and web development* workload
- **.NET Framework 4.7.2** developer pack
- **SQL Server Express** (or LocalDB) and, optionally, SQL Server Management Studio

### 1. Clone the repository
```bash
git clone https://github.com/priyanshuverma7667/HealthCare.git
cd HealthCare
```

### 2. Create the database
Run the script below in SSMS (or `sqlcmd`). It creates the `healthcaredb` database with the four tables the app expects.

```sql
CREATE DATABASE healthcaredb;
GO
USE healthcaredb;
GO

CREATE TABLE dbo.adminmaster (
    AdminId  VARCHAR(50) NOT NULL PRIMARY KEY,
    Password VARCHAR(50) NULL
);

CREATE TABLE dbo.doctormaster (
    DocterId       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    DoctorName     VARCHAR(200) NULL,
    Specialization VARCHAR(300) NULL,
    Experience     VARCHAR(50)  NULL,
    AddedOn        DATETIME     NULL
);

CREATE TABLE dbo.enquirymaster (
    EnquiryId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name      VARCHAR(200) NULL,
    Email     VARCHAR(200) NULL,
    Mobile    VARCHAR(50)  NULL,
    Message   VARCHAR(800) NULL,
    EnquiryDT DATETIME     NULL
);

CREATE TABLE dbo.notificationmaster (
    NotiId  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Message VARCHAR(800) NULL,
    AddedOn DATETIME     NULL
);
```

> The doctor key column is spelled `DocterId` in the model. Keep that spelling so the Entity Framework mapping matches.

### 3. Create an admin account
There is no sign-up page, so add an admin row yourself:

```sql
INSERT INTO dbo.adminmaster (AdminId, Password) VALUES ('your-admin-id', 'your-password');
```

### 4. Point the app at your database
Open `Web.config` and edit the `healthcaredbEntities` connection string. Usually only `data source` needs to change to your own SQL Server instance:

```xml
<add name="healthcaredbEntities"
     connectionString="metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.\SQLEXPRESS;initial catalog=healthcaredb;integrated security=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework&quot;"
     providerName="System.Data.EntityClient" />
```

### 5. Run
1. Open `HealthCare_ProtoType.sln` in Visual Studio.
2. Let NuGet restore packages (or right-click the solution and choose *Restore NuGet Packages*).
3. Press **F5**. The app runs on IIS Express at `https://localhost:44368/`.

## Routes

The default route is `{controller}/{action}/{id}`, and the home page is `General/Index`.

| URL | Description |
| --- | --- |
| `/` | Home |
| `/General/AboutUs` | About the hospital |
| `/General/Doctors` | Doctors directory |
| `/General/ContactUs` | Enquiry form |
| `/General/AdminLogIn` | Admin sign-in |
| `/General/DeverloperTeam` | Project team |
| `/Admin/Index` | Admin dashboard |
| `/Admin/Notification` | Add a notification |
| `/Admin/ManageNotification` | List and delete notifications |
| `/Admin/AddDoctors` | Add a doctor |
| `/Admin/ManageDoctors` | List and delete doctors |
| `/Admin/ManageEnquiry` | List and delete enquiries |

## Project structure

```
HealthCare/
├── App_Start/
│   └── RouteConfig.cs
├── Content/
│   ├── CSS/CommonDesign.css
│   └── Images/                  # logos, hospital, doctor and team photos
├── Controllers/
│   ├── GeneralController.cs     # public pages, enquiry form, admin login
│   └── AdminController.cs       # dashboard, doctors, notifications, enquiries
├── Models/
│   ├── Model1.edmx              # Entity Framework Database First model
│   ├── adminmaster.cs
│   ├── doctormaster.cs
│   ├── enquirymaster.cs
│   ├── notificationmaster.cs
│   └── EMailer.cs               # optional SMTP helper (see below)
├── Views/
│   ├── General/                 # Index, AboutUs, Doctors, ContactUs, ResponseEnquiry,
│   │                            # AdminLogIn, DeverloperTeam
│   ├── Admin/                   # Index, Notification, ManageNotification,
│   │                            # AddDoctors, ManageDoctors, ManageEnquiry
│   └── Shared/                  # General_Layout.cshtml, Admin_Layout.cshtml
├── Global.asax / Global.asax.cs
├── Web.config
├── packages.config
└── HealthCare_ProtoType.sln
```

## Optional: email replies to enquiries

`Models/EMailer.cs` is a small helper that sends mail through Gmail SMTP (`smtp.gmail.com`, port 587, SSL). The call in `GeneralController.SaveEnquiry` is currently commented out, so no emails are sent. To enable it, load the sender address and app password from configuration or environment variables rather than keeping them in source code, then uncomment the call.

## Deployment

The live site runs on Azure App Service. To publish your own copy, use *Build → Publish* in Visual Studio. Set the `healthcaredbEntities` connection string in the App Service configuration so it points to your production SQL Server or Azure SQL database.

## Roadmap

- Online appointment booking
- Patient reports and records
- Email confirmation for submitted enquiries
- Stronger admin authentication and authorization

## Team S.E.V.E.N.

| Name | Role |
| --- | --- |
| [Priyanshu Kumar Verma](https://www.linkedin.com/in/priyanshu-kumar-verma1905/) | Project Manager, Full Stack Developer |
| Abhijeet Rawat | Team Leader, Full Stack Developer |
| [Saurabh Kumar Singh](https://www.linkedin.com/in/saurabh-kumar-singh0505/) | Full Stack Developer |
| Rahul Pandey | Web Developer |
| Vandana Sharma | Web Developer |
| Kritika Pandey | Full Stack Developer |
| Shubham Priya | Web Developer |


https://healthcare-priyanshu-g9c7cbfgeugvb4ht.centralindia-01.azurewebsites.net/
