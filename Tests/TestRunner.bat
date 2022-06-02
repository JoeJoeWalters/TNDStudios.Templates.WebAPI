dotnet tool install -g dotnet-reportgenerator-globaltool
pause
FOR /D /r %%G in ("*.Tests") DO (
	Echo We found %%~nxG
	cd %%~nxG && dotnet test --collect:"XPlat Code Coverage" --filter "InMemory=yes"
	cd ..
)
pause
reportgenerator "-reports:./**/coverage.cobertura.xml" "-targetdir:./coverage" "-reporttypes:Html"
pause
