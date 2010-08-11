require 'rubygems'
require 'rake/gempackagetask'

task :default => [:init, :package, :clean]

task :clean do
	if FileTest.exists?("lib") then FileUtils.rm_rf("lib") end
end

task :init do
	if FileTest.exists?("lib") then FileUtils.rm_rf("lib") end
	if FileTest.exists?("pkg") then FileUtils.rm_rf("pkg") end
	
	FileUtils.mkdir_p "lib"
	
	Dir["../src/Component/bin/release/ZipCodes.*"].each do | file |
		FileUtils.copy(file, "lib");
	end

	spec = Gem::Specification.new do |spec|
		spec.platform = Gem::Platform::RUBY
		spec.summary = "Zip Code Coordinates .NET Component"
		spec.name = "zipcodecoords"
		spec.version = "1.0.0.0"
		spec.files = Dir["lib/**/*"]
		spec.authors = ["Mike O'Brien"]
		spec.homepage = "http://github.com/mikeobrien/ZipCodeCoords"
		spec.description = "The Zip Code to Coordinates .NET Component enables you to retrieve the global coordinates (Latitude and Longitude) of a Zip Code."
	end

	Rake::GemPackageTask.new(spec) do |package|
	end
end