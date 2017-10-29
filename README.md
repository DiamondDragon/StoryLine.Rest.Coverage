Welcome to **StoryLine.Rest.Coverage** tool. This tool allows to measure coverage of **StoryLine.Rest** tests. Tool accepts API definition (in form of swagger document) and [StoryLine.Rest](https://github.com/DiamondDragon/StoryLine.Rest) tests execution log. Test execution log configration is describe [here](https://github.com/DiamondDragon/StoryLine.Rest/wiki/Configuration).

Use the following snipped to add **StoryLine.Rest.Coverage** tool to project:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <ItemGroup>
    <DotNetCliToolReference Include="StoryLine.Rest.Coverage" Version="0.1.0" />
  </ItemGroup>  

</Project>
```

The following command line can be used build REST API coverage report:

```
dotnet restcoverage -s "d:\Swagger.json"  -l "d:\responses-2017-09-30.txt" -o "report.json" -f "service" -a "Service1"
```

**NOTE:** Swagger location and test execution logs are can be either local file or url to document in document. 

Here are evailable parameters:
```
        -s, --swagger... Swagger file to process
        -l, --log... Log file to handle
        -o, --output... Location to save results
        -f, --filter[optional]... Specifies type of filter to use
        -a, --argument[optional]... Filter argument to use
```