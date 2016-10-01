

@ECHO OFF

nuget "pack" "..\Murtain\Murtain.csproj" -Prop Configuration=Release
nuget "pack" "..\Murtain.AutoMapper\Murtain.AutoMapper.csproj" -Prop Configuration=Release
nuget "pack" "..\Murtain.EntityFramework\Murtain.EntityFramework.csproj" -Prop Configuration=Release
nuget "pack" "..\Murtain.Redis4net\Murtain.Redis4net.csproj" -Prop Configuration=Release
nuget "pack" "..\Murtain.RedisCache\Murtain.RedisCache.csproj" -Prop Configuration=Release
nuget "pack" "..\Murtain.Web.ApiDocument\Murtain.Web.ApiDocument.csproj" -Prop Configuration=Release
nuget "pack" "..\Murtain.Web.SignalR\Murtain.Web.SignalR.csproj" -Prop Configuration=Release


pause