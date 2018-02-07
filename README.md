# Excel-Development
<h3>Install EPPlus: <b>Install-Package EPPlus -Version 4.1.1</b><h3>
<h3>How to use the console by command:</h3>
<b>-h or - help:</b> display the way to use this console by command parameters</br>
<b>-test:</b> read testData.xlsx file</br>
<b>-other:</b> read OtherData.xlsx</br>
<b>-order:</b> generate Order.xlsx</br>

# Issues
1. encounter error <b>"No Entity Framework provider found for the ADO.NET provider with invariant name 'System.Data.SqlClient'. Make sure the provider is registered in the 'entityFramework' section of the application config file. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information."</b>
<p><b>Solution:</b>This error is caused by that if we forget to include "EntityFramework.SqlServer.dll". so need to run <b>"Install-Package EntityFramework"</b> on specity project</p>