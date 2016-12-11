.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:.\packages\NUnit.Runners.2.6.4\tools\nunit-console.exe -targetargs:"/nologo /noshadow .\Jal.Converter.Tests\bin\Debug\Jal.Converter.Tests.dll" -filter:"+[Jal.*]* -[*.Tests]*" -register:user

.\packages\coveralls.io.1.3.4\tools\coveralls.net.exe --opencover .\results.xml