
 
try {

    mkdir package -ErrorAction SilentlyContinue  
    Remove-Item package\* -Recurse -Force -ErrorAction SilentlyContinue
    $packageFolder = "./package/QM_RaidShowAlly"
    mkdir $packageFolder | Out-Null

    # ---- Build the projects.  The projects will automatically deploy to the Steam Workshop folder.

    "Bootstrap"
    dotnet clean ./src\QM_RaidShowAlly_Bootstrap.csproj
    dotnet build -c Release ./src\QM_RaidShowAlly_Bootstrap.csproj -o $packageFolder
    Copy-Item ../main-repo/media/thumbnail.png $packageFolder

    

    "Stable"
    dotnet clean ..\main-repo\src\QM_RaidShowAlly.csproj
    dotnet build -c Release ..\main-repo\src\QM_RaidShowAlly.csproj -o $packageFolder\Stable
    # dotnet build has a bug where it always copies any project references.
    del $packageFolder/Stable/QM_RaidShowAlly_Bootstrap.*

    "Beta"
    dotnet clean ..\beta\src\QM_RaidShowAlly.csproj
    dotnet build -c Release ..\beta\src\QM_RaidShowAlly.csproj -o $packageFolder\Beta
    # dotnet build has a bug where it always copies any project references.
    del $packageFolder/Beta/QM_RaidShowAlly_Bootstrap.*


    # ---- Create the package zip file.
    Copy-Item ../main-repo/media/thumbnail.png $packageFolder
    Copy-Item ../main-repo/README.md $packageFolder
    Copy-Item version-info.json $packageFolder
    Copy-Item modmanifest.json $packageFolder

    Compress-Archive -Path $packageFolder\* -DestinationPath ./QM_RaidShowAlly.zip -Force

    # Add the beta text if beta is not disabled.
    # Otherwise remove?

    "Build completed"
} catch {
    Write-Error "Build Failure: $_"
    exit 1
}



