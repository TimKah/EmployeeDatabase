$Env:AnalyticsPassword="guest"
$Env:AnalyticsDatabase="analyticsproject"
$Env:AnalyticsPort="7777"
$Env:AnalyticsSchemaLocation=$("$(Get-Location)\DatabaseSchema\dbSchema.sql")

Write-Host ""
Write-Host "This script will set up a postgres database hosted in a docker container"
read-host "Press ENTER to continue"

docker pull postgres
if (-not $?) { exit 1 }

$portAssign = $Env:AnalyticsPort + ":5432"
$container=$(docker run -e "POSTGRES_PASSWORD=$Env:AnalyticsPassword" -p "$portAssign" -d postgres)
if (-not $?) { exit 1 }

Try {
    Write-Host "Database starting. Setting db schema..."

    pushd DatabaseSchema
    dotnet run
    if (-not $?) { exit 1 }

    Write-Host ""
    Write-Host "The database is ready to use" -ForegroundColor Green
    Write-Host "Conection string: 'Server=localhost; User ID=postgres; Password=$Env:AnalyticsPassword; Port=$Env:AnalyticsPort; Database=$Env:AnalyticsDatabase;'"
    Write-Host "Schema applied to database:"
    cat ./dbSchema.sql
    Write-Host ""
    Write-Host "Press Ctrl^C to stop the database server and exit" -ForegroundColor Green
    docker attach "$container"
} Finally {
    popd
    docker stop "$container"
}