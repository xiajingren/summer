# Summer

后端部分



EF Core Migration

```powershell
Summer.Infrastructure>

dotnet ef migrations add SummerDbContext_Initial -o Data/Migrations -s ../Summer.WebApi

dotnet ef database update -s ../Summer.WebApi
```

