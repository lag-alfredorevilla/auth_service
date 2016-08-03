# Authentication Service

Standard service that handles authentication against oAuth services online. 

**This project is in ALPHA state, do not employ in production!**


## Running

**Important**: set the necessary environment variables:

For Powershell users:
```
$env:ASPNETCORE_ENVIRONMENT = "Production"
$env:ASPNETCORE_CONTENTROOT = "..\..\..\Resources\"
```
For Bash users:
```
export ASPNETCORE_ENVIRONMENT="Production"
export ASPNETCORE_CONTENTROOT="..\..\..\Resources\"
```
To get a more verbose log output use another environment like: `Development`
