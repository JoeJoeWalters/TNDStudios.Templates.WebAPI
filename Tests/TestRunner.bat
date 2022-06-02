FOR /D /r %%G in ("*.Tests") DO (
	Echo We found %%~nxG
	cd %%~nxG && dotnet test --collect:"XPlat Code Coverage" --filter "InMemory=yes"
	cd ..
)
pause
