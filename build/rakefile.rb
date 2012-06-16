require "albacore"
require_relative "filesystem"
require_relative "gallio-task"

reportsPath = "reports"
version = ENV["BUILD_NUMBER"]

task :build => [:createPackage]
task :pushPackages => [:pushPackage]

assemblyinfo :assemblyInfo do |asm|
    asm.version = version
    asm.company_name = "Ultraviolet Catastrophe"
    asm.product_name = "Zip Code Coords"
    asm.title = "Zip Code Coords"
    asm.description = "Zip code coordinate lookup."
    asm.copyright = "Copyright (c) 2012 Ultraviolet Catastrophe"
    asm.output_file = "src/ZipCodeCoords/Properties/AssemblyInfo.cs"
end

msbuild :buildLibrary => :assemblyInfo do |msb|
    msb.properties :configuration => :Release
    msb.targets :Clean, :Build
    msb.solution = "src/ZipCodeCoords/ZipCodeCoords.csproj"
end

msbuild :buildTests => [:buildLibrary] do |msb|
    msb.properties :configuration => :Release
    msb.targets :Clean, :Build
    msb.solution = "src/Tests/Tests.csproj"
end

task :unitTestInit do
	FileSystem.EnsurePath(reportsPath)
end

gallio :unitTests => [:buildTests, :unitTestInit] do |runner|
	runner.echo_command_line = true
	runner.add_test_assembly("src/Tests/bin/Release/Tests.dll")
	runner.verbosity = 'Normal'
	runner.report_directory = reportsPath
	runner.report_name_format = 'tests'
	runner.add_report_type('Html')
end

nugetApiKey = ENV["NUGET_API_KEY"]
deployPath = "deploy"

packagePath = File.join(deployPath, "package")
nuspec = "zipcodecoords.nuspec"
packageLibPath = File.join(packagePath, "lib")
binPath = "src/ZipCodeCoords/bin/Release"

task :prepPackage => :unitTests do
	FileSystem.DeleteDirectory(deployPath)
	FileSystem.EnsurePath(packageLibPath)
	FileSystem.CopyFiles(File.join(binPath, "ZipCodeCoords.dll"), packageLibPath)
	FileSystem.CopyFiles(File.join(binPath, "ZipCodeCoords.pdb"), packageLibPath)
end

nuspec :createSpec => :prepPackage do |nuspec|
   nuspec.id = "zipcodecoords"
   nuspec.version = version
   nuspec.authors = "Mike O'Brien"
   nuspec.owners = "Mike O'Brien"
   nuspec.title = "Zip Code Coords"
   nuspec.description = "Enables the lookup of coordinates by zip code and vice versa."
   nuspec.summary = "Enables the lookup of coordinates by zip code and vice versa."
   nuspec.language = "en-US"
   nuspec.licenseUrl = "https://github.com/mikeobrien/ZipCodeCoords/blob/master/LICENSE"
   nuspec.projectUrl = "https://github.com/mikeobrien/ZipCodeCoords"
   nuspec.iconUrl = "https://github.com/mikeobrien/ZipCodeCoords/raw/master/misc/logo.png"
   nuspec.working_directory = packagePath
   nuspec.output_file = nuspec
   nuspec.tags = "zipcode coordinates"
end

nugetpack :createPackage => :createSpec do |nugetpack|
   nugetpack.nuspec = File.join(packagePath, nuspec)
   nugetpack.base_folder = packagePath
   nugetpack.output = deployPath
end

nugetpush :pushPackage => :createPackage do |nuget|
    nuget.apikey = nugetApiKey
    nuget.package = File.join(deployPath, "zipcodecoords.#{version}.nupkg")
end