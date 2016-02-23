#DOCUMENTATION
##Description
This is software for quality management system. The structure is similar to the requirements of ISO 9001.
The Application represents the company hierarchy. There are several levels of the company hierarchy.
First, there are Divisions. Each division has different departments. and each department has areas. For each area there is a responsible employee. One employee can be responsible for many areas.

There are procedures which consist of different groups of documents. A document has title, description and a file. The employees use the documents to document different kind of events.

The employee can create records within the area for which is responsible. The record uses the documents to create it's own file, same as the file of the document. And here is the main functionality: For each event that the employees document, they use the same template as the template from the documents files.

The program consist mainly of three parts: public, private and admin.

In the public part the users can view the whole company hierarchy.

In the private part the users can manage their areas. Also there is a real time messenger available only for the logged in users.

In the admin part the admin can do crud operations over the whole company hierarchy. Also the admin can add roles to the users. For each level of the company hierarchy there is a role which gives administration options to the user. There is one extra role which gives a user the option to view and manage all user areas.

##Architecture
The program is structured within different projects. 

**Qms.Web**

This is the startup project. Here are all the controllers and views. Also the areas are placed here.

**Qms.Data** 

Here is configured the DbContext, the repository pattern and the unit-of-works pattern.

**Qms.Models**

Here are all the database models.

**Qms.Services**

Here is all the logic that the controllers use. Within the constructors of the services is injected the unit of works database class and in the controlellers are injected the different services they use. Ninject has been used for the injection. And Automapper for mapping the db models with the view models.

**Qms.Helpers**

Here is placed some common logic like random generator and file saving logic.

**Qms.Common**

Here are defined all the global constants like error and success messages.

**Qms.Web.Infrastructure**

Here is placed the configuration of the automapper.

**Qms.Web.ViewModels**

Here are all the view models used.

 

##Routes
###Public routes
	"/home"
![home page](/images/public/home.png)

	"/divisions"
![home page](/images/public/divisions.png)

	"/departments"
![home page](/images/public/departments.png)

	"/areas"
![home page](/images/public/areas.png)

	"/procedures"
![home page](/images/public/procedures.png)

	"/users"
![home page](/images/public/users.png)

	"/users/details"
![home page](/images/public/users-details.png)

###Admin routes
	"admin/divisions"
![home page](/images/admin/divisions.png)

	"admin/divisions/details/{id}"
![home page](/images/admin/divisions-details.png)

	"admin/divisions/create"
![home page](/images/admin/divisions-create.png)

	"admin/departments"
![home page](/images/admin/departments.png)

	"admin/departments/create"
![home page](/images/admin/departments-create.png)

	"admin/departments/details"
![home page](/images/admin/departments-details.png)

	"admin/areas"
![home page](/images/admin/areas.png)

	"admin/areas/details"
![home page](/images/admin/areas-details.png)

	"admin/areas/edit/{id}"
![home page](/images/admin/areas-edit.png)

	"admin/procedures"
![home page](/images/admin/procedures.png)

	"admin/procedures/create"
![home page](/images/admin/procedures-create.png)

	"admin/procedures/details/{id}"
![home page](/images/admin/procedures-details.png)

	"admin/procedures/documents"
![home page](/images/admin/documents.png)

	"admin/procedures/documents/create"
![home page](/images/admin/documents-create.png)

	"admin/procedures/documents/details/{id}"
![home page](/images/admin/documents-details.png)

	"admin/procedures/users"
![home page](/images/admin/users.png)

	"admin/procedures/users/register"
![home page](/images/admin/users-register.png)

	"admin/procedures/users/edit/{id}"
![home page](/images/admin/users-edit.png)

###Private routes

	"private/areas/"
![home page](/images/private/areas.png)

	"private/areas/manage/{id}"
![home page](/images/private/areas-manage.png)

	"private/areas/manage/{id}/createrecord"
![home page](/images/private/record-create.png)

	"private/records/edit/{id}"
![home page](/images/private/records-edit.png)

	"private/users/update"
![home page](/images/private/users-update.png)

	"private/messages"
![home page](/images/private/messages.png)

##Database
###Diagram

![home page](/images/database/diagram.png)