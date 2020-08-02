# installing and using dotnet migrate tool

Installation
`dotnet tool install --global Project2015To2017.Migrate2019.Tool`


Usage
`dotnet migrate-2019 migrate -t net461 -t netcoreapp3.1`

make sure all referneced assemblies that are non netcore you will wrap inside
```XML
  <ItemGroup Condition="TargetFramework == net461">
    <Reference Include="System.Web" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Net.Http" />
  </ItemGroup>
```


