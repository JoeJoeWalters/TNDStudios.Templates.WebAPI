Echo %1
dotnet tool install -g dotnet-reportgenerator-globaltool
rd coverage /S /Q
FOR /D /r %%G in ("*.Tests") DO (
	Echo We found %%~nxG
	cd %%~nxG
	rd TestResults /S /Q
	dotnet test --collect:"XPlat Code Coverage" --filter %1
	cd ..
)
reportgenerator "-reports:./**/coverage.cobertura.xml" "-targetdir:./coverage" "-reporttypes:Html"
start ./coverage/index.html