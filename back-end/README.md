# Summer

后端部分



EF Core Migration

```powershell
Summer.Infrastructure>

dotnet ef migrations add SummerDbContext_Initial -c SummerDbContext -o Data/Migrations -s ../Summer.WebApi

dotnet ef migrations add MasterDbContext_Initial -c MasterDbContext -o MasterData/Migrations -s ../Summer.WebApi

dotnet ef database update -c SummerDbContext -s ../Summer.WebApi

dotnet ef database update -c MasterDbContext -s ../Summer.WebApi
```

