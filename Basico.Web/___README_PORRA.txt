<html>
<head>
    <title>Billete</title>
    <meta name="google-site-verification" content="1N8eQvyav1jl0OYQmQEHwLN1eqjfFQmYiifZt9FS7e4" />
</head>
<body>
<%
Response.Redirect "http://www.billete.somee.com/basico"
%>
</body>
</html>


-- Somee
  <connectionStrings>
    <!--<add name="Basico" connectionString="Data Source=Home-01\SQLEXPRESS;Initial Catalog=basicodb;Integrated Security=SSPI; MultipleActiveResultSets=true" providerName="System.Data.SqlClient" />
    <add name="Basico" connectionString="Data Source=Home-01\SQLEXPRESS;Initial Catalog=Basico;Integrated Security=SSPI; MultipleActiveResultSets=true" providerName="System.Data.SqlClient" /> -->
     <add name="Basico" connectionString="workstation id=basicodb.mssql.somee.com;packet size=4096; Data Source=basicodb.mssql.somee.com;Initial Catalog=basicodb;Persist Security Info=False;user id=betabilhete_SQLLogin_1;Password=2udw327to9;MultipleActiveResultSets=true" providerName="System.Data.SqlClient" />
  </connectionStrings>


-- Home
  <connectionStrings>
    <add name="Basico" connectionString="Data Source=Home-01\SQLEXPRESS;Initial Catalog=basicodb;Integrated Security=SSPI; MultipleActiveResultSets=true" providerName="System.Data.SqlClient" />
    <!--<add name="Basico" connectionString="Data Source=Home-01\SQLEXPRESS;Initial Catalog=Basico;Integrated Security=SSPI; MultipleActiveResultSets=true" providerName="System.Data.SqlClient" />
     <add name="Basico" connectionString="workstation id=basicodb.mssql.somee.com;packet size=4096; Data Source=basicodb.mssql.somee.com;Initial Catalog=basicodb;Persist Security Info=False;user id=betabilhete_SQLLogin_1;Password=2udw327to9;MultipleActiveResultSets=true" providerName="System.Data.SqlClient" /> -->
  </connectionStrings>


-- email
  <system.web>
    <compilation debug="true" targetFramework="4.6.1" />
    <httpRuntime targetFramework="4.6.1" />
    <securityPolicy>
      <trustLevel  name="Full" policyFile="internal" />     
    </securityPolicy>
  </system.web>



<a href="."><img src="http://www.billete.somee.com/basico/_imagery/_noimage_person.png" alt="www.billete.somee.com/basico/" title="www.billete.somee.com/basico/" width="140"/></a>

<a href="."><img src="http://www.billete.somee.com/basico/_imagery/_noimage_person.png" alt="www.billete.somee.com/basico/" title="www.billete.somee.com/basico/" width="140" height="auto" /></a>



<a href="."><img src="http://marcelo.linkpc.net:8188/billet/_imagery/_billet_logo_096.png" alt="www.billete.somee.com/basico/" title="www.billete.somee.com/basico/" width="140" height="auto" /></a>


>{billetParam_00x} replace to   " hidden >"
>{billetParam_00x} replace to   ">content"