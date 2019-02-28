# Utopia: DevOps with all the bells and whistles

Welcome to Utopia! Project Utopia showcases all kinds of cool features of both GitHub and Azure
DevOps in an easily accessible way to help you learn DevOps.

## Show me!

The public Azure DevOps project is located here: https://dev.azure.com/Utopia-Demo/Utopia.

You can view the deployed application here <a href="https://utopia-production-web.azurewebsites.net">https://utopia-production-web.azurewebsites.net</a>.

As a public user of Azure DevOps, you can see the pipelines and some other features. If you want to see other details and get access to the Azure Portal to see the result of the pipelines, please drop me an email at wouter@wouterdekort.com and I'll add you as a reader.

## What does it do?

The project is a simple ToDo application that I based on <a href="https://github.com/ardalis/CleanArchitecture">the
CleanArchitecture ASP.NET Core template from Steve Smith</a>. For now,
the app shows a list of ToDo items and allows you to add and remove items. This
is all very basic and that’s the idea. The focus is on the DevOps components,
not on a very complex application.

I will try to update the application each time a new version
of .NET Core is released. I will add new functionality in the future to the app
if I need it to demo an aspect of DevOps. For example, for Database DevOps, I
need a more complex database to showcase always on updates. For Feature Flags, I'll need authentication. And maybe there are even some microservices in the future.

## What DevOps features do you already have?

The basics are in place for now:

- Git repository containing the application
- A continuous integration build based on YAML that builds the application, runs unit tests and runs SonarQube and WhiteSource Bolt
- A release pipeline that uses ARM templates to deploy the application to a test and production environment

This is a minimal set of functionalities that I want to expand upon in the coming months. I’ve created a second team (<a href="https://dev.azure.com/Utopia-Demo/Utopia/_backlogs/backlog/Iron%20Man/Stories">Iron Man</a>) that contains user stories for all the things I would like to add.

## Can I help?

Yes please! The project is open source and I happily accept
pull requests. As you can see, the backlog is long and I’m pretty sure that the list will keep
growing since Microsoft is continually releasing new functionality.

My goal is to make sure that Utopia becomes one of the most
complete showcases of what DevOps can do for your organization.

If you want to contribute, please open a GitHub issue or send a pull request!
