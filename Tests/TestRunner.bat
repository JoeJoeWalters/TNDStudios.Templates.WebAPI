cd Component.Tests && dotnet test --collect:"XPlat Code Coverage"
cd ..
cd Repository.Tests && dotnet test --collect:"XPlat Code Coverage"
cd ..
pause
