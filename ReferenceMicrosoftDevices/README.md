# Reference Microsoft.Azure.Devices

The following samples go through different scenarios when attempting to use Microsoft.Azure.Devices with Azure Functions.

Partial compatibility with v1. Full compatibality with v2.

## ReferenceMicrosoftDevices: V1 (.NET Framework 4.7.1)

To install in the project, first install `Newtonsoft.Json` version 10.0.3.  Some classes will work (`ServiceClient`) and others may throw assembly conflict errors (`RegistryManager`).

|Function|Description|Supported|
|--|--|--|
|BlobTrigger_CloudBlockBlob_Devices|Uses the `ServiceClient` class and passes in a rich `CloudBlockBlob` input to trigger|Yes|
|BlobTrigger_Stream_RegistryManager|Uses `IQuery` with `RegistryManager` - trigger is a `Stream` native type|No|
|HttpTrigger_Devices|`HttpRequestMessage` trigger and the `ServiceClient` class|Yes|
|HttpTrigger_JObject_Devices|`JObject` trigger and the `ServiecClient` class|Yes|
|HttpTrigger_RegistryManager|`HttpRequestMessage` trigger and the `RegistryManager` class|No|
|TimerTrigger_RegistryManager|Timer trigger with `RegistryManager` class|No|

## ReferenceMicrosoftDevices_v2: V2 (.NET Core 2.0)

Already using a newer version of `Newtonsoft.Json` so should just install

|Function|Description|Supported|
|--|--|--|
|HttpTrigger_Devices|`HttpRequestMessage` trigger and the `ServiceClient` class|Yes|
|TimerTrigger_RegistryManager|Timer trigger with `RegistryManager` class|Yes|
