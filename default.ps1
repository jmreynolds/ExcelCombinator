# required parameters :
# 	$databaseName

Framework "4.6"

properties {
	$projectName = "ExcelCombinator"
    $unitTestAssembly = "UnitTests.dll"
    $integrationTestAssembly = "IntegrationTests.dll"
	$projectConfig = "Release"
	$base_dir = resolve-path .\
	$source_dir = "$base_dir\src"
    $nunitPath = "$source_dir\packages\NUnit.Console.3.0.1\tools"

	$build_dir = "$base_dir\build"
	$test_dir = "$build_dir\test"
	$testCopyIgnorePath = "_ReSharper"
	# $package_dir = "$build_dir\package"
	# $package_file = "$build_dir\latestVersion\" + $projectName +"_Package.zip"

    $setup_file = "$base_dir\SetupFiles\ExcelCombinatorSetup.msi"
    $installer_cmd_path = "C:\Program Files (x86)\Caphyon\Advanced Installer 13.5\bin\x86\AdvancedInstaller.com"
    $installer_project_file = "$base_dir\Installer\Installer.aip"
    $zero29 = "$base_dir\Zero29\Zero29.exe"
}

task default -depends Init, CommonAssemblyInfo, Compile, Test
task ci -depends Init, CommonAssemblyInfo, Compile, Test, Package

task Init {
    delete_file $package_file
    create_directory $test_dir
	create_directory $build_dir
}
task CommonAssemblyInfo {
    $version = get_semantic_version_number
    Update-AssemblyInfoFiles($version)
}
task Compile -depends Init, CommonAssemblyInfo {
	exec{
		Get-ChildItem -Path $source_dir -inc bin,obj -rec | Remove-Item -rec -force
	} "Cleaning the solution failed - make sure VS is closed, and try unlocking the bin and obj folders."
   exec {  & msbuild /t:clean /v:q /nologo /p:Configuration=$projectConfig $source_dir\$projectName.sln }
    delete_file $error_dir
    exec { & msbuild /t:build /v:q /nologo /p:Configuration=$projectConfig $source_dir\$projectName.sln
		} "Compilation Failed"
}
task Test -depends Compile {
 	copy_all_assemblies_for_test $test_dir
	exec {
		& $nunitPath\nunit3-console.exe $test_dir\$unitTestAssembly $test_dir\$integrationTestAssembly --where "cat != Interop"
	}
}
task Package -depends Compile {
    Write-Output "Generating Installation Files to $setup_file"
    delete_file $setup_file
    exec{
        $version = get_semantic_version_number
        & "$installer_cmd_path" /edit $installer_project_file /SetVersion -fromfile $source_dir\UI.WinForms\bin\Release\UI.WinForms.exe
        & "$installer_cmd_path" /edit $installer_project_file /SetPackageName $setup_file
        & "$installer_cmd_path" /rebuild $installer_project_file
        # $ /rebuild [project_file_path]
    }


}

function global:get_semantic_version_number(){
    $output = & "GitVersion.exe"
    $joined = $output -join "`n"
    $versionInfo = $joined | ConvertFrom-Json
    $result = $versionInfo.AssemblySemVer
    return $result
}

function global:Update-AssemblyInfoFiles ([string] $version) {
    exec{
        & "$zero29" -a $version
    }
}

function global:zip_directory($directory,$file) {
    write-host "Zipping folder: " $test_assembly
    delete_file $file
    cd $directory
    & "$base_dir\7zip\7za.exe" a -mx=9 -r $file
    cd $base_dir
}

function global:copy_files($source,$destination,$exclude=@()){
    create_directory $destination
    Get-ChildItem $source -Recurse -Exclude $exclude | Copy-Item -Destination {Join-Path $destination $_.FullName.Substring($source.length)}
}

function global:Copy_and_flatten ($source,$filter,$dest) {
  ls $source -filter $filter  -r | Where-Object{!$_.FullName.Contains("$testCopyIgnorePath") -and !$_.FullName.Contains("packages") }| cp -dest $dest -force
}

function global:copy_all_assemblies_for_test($destination){
  create_directory $destination
  Copy_and_flatten $source_dir *.exe $destination
  Copy_and_flatten $source_dir *.dll $destination
  Copy_and_flatten $source_dir *.config $destination
  Copy_and_flatten $source_dir *.xml $destination
  Copy_and_flatten $source_dir *.pdb $destination
  Copy_and_flatten $source_dir *.sql $destination
  Copy_and_flatten $source_dir *.xlsx $destination
}

function global:delete_file($file) {
    if($file) { remove-item $file -force -ErrorAction SilentlyContinue | out-null }
}

function global:delete_directory($directory_name)
{
  rd $directory_name -recurse -force  -ErrorAction SilentlyContinue | out-null
}

function global:delete_files_in_dir($dir)
{
	get-childitem $dir -recurse | foreach ($_) {remove-item $_.fullname}
}

function global:create_directory($directory_name)
{
  mkdir $directory_name  -ErrorAction SilentlyContinue  | out-null
}
