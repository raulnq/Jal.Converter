packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Converter\Jal.Converter.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Converter.Nuget

packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Converter.AutoMapper\Jal.Converter.AutoMapper.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Converter.Nuget

packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Converter.Installer\Jal.Converter.Installer.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Converter.Nuget

pause;