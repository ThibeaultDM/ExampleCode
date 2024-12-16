Examples of how I write code, I still need to work on general error handeling. The Customer component is the oldest of my code.

If you wish to run it, configure a MS SQL server dataSource in both the CustomerCommunicationLayer and NewInvoiceCommunicationLayer appsettings.json file. 
Exicute the "update-database" command from the package manager console on the previously mentioned CommunicationLayers. 
Set CustomerCommunicationLayer, NewInvoiceCommunicationLayer, OrchestrationLayer as startup projects and add a Customer to the database.
Then you can add any of the UI's as a startup project. The winforms project is my most recent one.
