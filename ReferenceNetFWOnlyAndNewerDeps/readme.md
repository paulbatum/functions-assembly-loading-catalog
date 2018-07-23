This scenario is not currently supported in either V1 (version=1.0.11913.0) or V2 (version=2.0.11933.0). It uses both [Microsoft.EntityFrameworkCore v2.1.1](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/) and [Microsoft.AnalysisServices.AdomdClient](https://www.nuget.org/packages/Microsoft.AnalysisServices.AdomdClient.retail.amd64/) NuGet packages.

The AdomdClient package can be used successfully in a V1 function app. However, this is not the case for EF Core and any attempt to use it fails with a **FileNotFoundException** (*Could not load file or assembly 'System.ComponentModel.Annotations, Version=4.2.0.0, ...'*).

When the same code is moved to a V2 function app, the EF Core code starts to work, but the AdomdClient fails with a **TypeLoadException** (*Could not load type 'System.Security.Principal.WindowsImpersonationContext' from assembly 'mscorlib, Version=4.0.0.0, ...'*). Note that when you add the AdomdClient package to the V2 project, you do get a warning alerting that this package targets .NET Framework, not .NET Standard and, presumably, WindowsImpersonationContext is not implemented in .NET Core, thus the failure.
```
Package 'Microsoft.AnalysisServices.AdomdClient.retail.amd64 15.3.1' was restored using '.NETFramework,Version=v4.6.1' instead of the project target framework '.NETStandard,Version=v2.0'. This package may not be fully compatible with your project.
```

The following exception is raised in the V1 app.
```
System.IO.FileNotFoundException: Could not load file or assembly 'System.ComponentModel.Annotations, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' or one of its dependencies. The system cannot find the file specified.
File name: 'System.ComponentModel.Annotations, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
   at Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal.CoreConventionSetBuilder.CreateConventionSet()
   at Microsoft.EntityFrameworkCore.Infrastructure.ModelSource.CreateConventionSet(IConventionSetBuilder conventionSetBuilder)
   at Microsoft.EntityFrameworkCore.Infrastructure.ModelSource.CreateModel(DbContext context, IConventionSetBuilder conventionSetBuilder, IModelValidator validator)
   at Microsoft.EntityFrameworkCore.Infrastructure.ModelSource.<>c__DisplayClass5_0.<GetModel>b__1()
   at System.Lazy`1.CreateValue()
   at System.Lazy`1.LazyInitValue()
   at System.Lazy`1.get_Value()
   at Microsoft.EntityFrameworkCore.Infrastructure.ModelSource.GetModel(DbContext context, IConventionSetBuilder conventionSetBuilder, IModelValidator validator)
   at Microsoft.EntityFrameworkCore.Internal.DbContextServices.CreateModel()
   at Microsoft.EntityFrameworkCore.Internal.DbContextServices.get_Model()
   at Microsoft.EntityFrameworkCore.Infrastructure.EntityFrameworkServicesBuilder.<>c.<TryAddCoreServices>b__7_1(IServiceProvider p)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitFactory(FactoryCallSite factoryCallSite, ServiceProviderEngineScope scope)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2.VisitCallSite(IServiceCallSite callSite, TArgument argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitScoped(ScopedCallSite scopedCallSite, ServiceProviderEngineScope scope)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2.VisitCallSite(IServiceCallSite callSite, TArgument argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitConstructor(ConstructorCallSite constructorCallSite, ServiceProviderEngineScope scope)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2.VisitCallSite(IServiceCallSite callSite, TArgument argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitScoped(ScopedCallSite scopedCallSite, ServiceProviderEngineScope scope)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2.VisitCallSite(IServiceCallSite callSite, TArgument argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.DynamicServiceProviderEngine.<>c__DisplayClass1_0.<RealizeService>b__0(ServiceProviderEngineScope scope)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngine.GetService(Type serviceType, ServiceProviderEngineScope serviceProviderEngineScope)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope.GetService(Type serviceType)
   at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService(IServiceProvider provider, Type serviceType)
   at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService[T](IServiceProvider provider)
   at Microsoft.EntityFrameworkCore.DbContext.get_DbContextDependencies()
   at Microsoft.EntityFrameworkCore.DbContext.get_InternalServiceProvider()
   at Microsoft.EntityFrameworkCore.DbContext.Microsoft.EntityFrameworkCore.Infrastructure.IInfrastructure<System.IServiceProvider>.get_Instance()
   at Microsoft.EntityFrameworkCore.Internal.InternalAccessorExtensions.GetService[TService](IInfrastructure`1 accessor)
   at Microsoft.EntityFrameworkCore.Infrastructure.AccessorExtensions.GetService[TService](IInfrastructure`1 accessor)
   at Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade.get_ProviderName()
   at Microsoft.EntityFrameworkCore.InMemoryDatabaseFacadeExtensions.IsInMemory(DatabaseFacade database)
   at StatsEngine.Metadata.StatsEngineDbContextFactory.Create(DbContextOptions`1 overrideOptions)
   at StatsEngine.Metadata.StatsEngineData.<GetAccumulatorsAsync>d__5.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at AzureFunctionsV1.Function1.<Run>d__2.MoveNext()

=== Pre-bind state information ===
LOG: DisplayName = System.ComponentModel.Annotations, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
 (Fully-specified)
LOG: Appbase = file:///C:/Users/Fernando/AppData/Local/AzureFunctionsTools/Releases/1.3.0/cli/
LOG: Initial PrivatePath = NULL
Calling assembly : Microsoft.EntityFrameworkCore, Version=2.1.1.0, Culture=neutral, PublicKeyToken=adb9793829ddae60.
===
LOG: This bind starts in LoadFrom load context.
WRN: Native image will not be probed in LoadFrom context. Native image will only be probed in default load context, like with Assembly.Load().
LOG: Using application configuration file: C:\Users\Fernando\AppData\Local\AzureFunctionsTools\Releases\1.3.0\cli\func.exe.Config
LOG: Using host configuration file: 
LOG: Using machine configuration file from C:\Windows\Microsoft.NET\Framework\v4.0.30319\config\machine.config.
LOG: Post-policy reference: System.ComponentModel.Annotations, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
LOG: Attempting download of new URL file:///C:/Users/Fernando/AppData/Local/AzureFunctionsTools/Releases/1.3.0/cli/System.ComponentModel.Annotations.DLL.
LOG: Attempting download of new URL file:///C:/Users/Fernando/AppData/Local/AzureFunctionsTools/Releases/1.3.0/cli/System.ComponentModel.Annotations/System.ComponentModel.Annotations.DLL.
LOG: Attempting download of new URL file:///C:/Users/Fernando/AppData/Local/AzureFunctionsTools/Releases/1.3.0/cli/System.ComponentModel.Annotations.EXE.
LOG: Attempting download of new URL file:///C:/Users/Fernando/AppData/Local/AzureFunctionsTools/Releases/1.3.0/cli/System.ComponentModel.Annotations/System.ComponentModel.Annotations.EXE.
LOG: Attempting download of new URL file:///C:/projects/AzureFunctionsV1/bin/Debug/net472/bin/System.ComponentModel.Annotations.DLL.
WRN: Comparing the assembly name resulted in the mismatch: Build Number
LOG: Attempting download of new URL file:///C:/projects/AzureFunctionsV1/bin/Debug/net472/bin/System.ComponentModel.Annotations/System.ComponentModel.Annotations.DLL.
LOG: Attempting download of new URL file:///C:/projects/MetadataEFCore/AzureFunctionsV1/bin/Debug/net472/bin/System.ComponentModel.Annotations.EXE.
LOG: Attempting download of new URL file:///C:/projects/AzureFunctionsV1/bin/Debug/net472/bin/System.ComponentModel.Annotations/System.ComponentModel.Annotations.EXE.
```

The following exception is raised in the V2 app.
```
System.TypeLoadException: Could not load type 'System.Security.Principal.WindowsImpersonationContext' from assembly 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.
   at Microsoft.AnalysisServices.AdomdClient.IdentityResolver.Dispose()
   at Microsoft.AnalysisServices.AdomdClient.XmlaClient.Connect(ConnectionInfo connectionInfo, Boolean beginSession)
   at Microsoft.AnalysisServices.AdomdClient.AdomdConnection.XmlaClientProvider.Connect(Boolean toIXMLA)
   at Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectToXMLA(Boolean createSession, Boolean isHTTP)
   at Microsoft.AnalysisServices.AdomdClient.AdomdConnection.Open()
   at FunctionAppV2.Function1.Run(HttpRequest req, TraceWriter log) in C:\projects\FunctionAppV2\Function1.cs:line 31
```
