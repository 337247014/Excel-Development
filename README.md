# Excel-Development
Install EPPlus: <b>Install-Package EPPlus -Version 4.1.1</b>
# Issues
1. encounter error <b>"No Entity Framework provider found for the ADO.NET provider with invariant name 'System.Data.SqlClient'. Make sure the provider is registered in the 'entityFramework' section of the application config file. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information."</b>
<p><b>Solution:</b>This error is caused by that if we forget to include "EntityFramework.SqlServer.dll". so need to run <b>"Install-Package EntityFramework"</b> on specity project</p>