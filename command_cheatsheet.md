# installing and using dotnet migrate tool

Installation
`dotnet tool install --global Project2015To2017.Migrate2019.Tool`


Usage
`dotnet migrate-2019 migrate -t net461 -t netcoreapp3.1`

make sure all referneced assemblies that are non netcore you will wrap inside
```XML
  <ItemGroup Condition="'$(TargetFramework)' == 'net461'">
    <PackageReference Include="log4net" Version="2.0.4" />
    <Reference Include="System.Web" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Net.Http" />
  </ItemGroup>
  
  <ItemGroup Condition="'$(TargetFramework)' == 'netcoreapp3.1'">
    <ProjectReference Include="..\StaticHttpContextForNetCore\StaticHttpContextForNetCore.csproj" />
  </ItemGroup>
```


