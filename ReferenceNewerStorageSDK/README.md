# Reference Newer Storage SDK

The following samples go through different scenarios when attempting to use a newer version of the Azure Storage SDK than the host version.

|Function|Description|Supported|
|--|--|--|
|BindToQueueMessage|Triggering or binding to a complex type like CloudQueueMessage|No|
|BindToStreamUseSDK|Triggering or binding to a Stream and using the newer SDK directly in code|No|
|BindToStringUseSDK|Triggering or binding to a `string` or `byte []` and using the newer SDK directly in code|Yes|

## Notes

Newer versions of Azure Storage SDK have a dependency on newer versions of `Newtonsoft.Json`.  As shown in the `ReferenceNuGetRequiringBindingRedirect` sample in this repo this requires you explicitely pull in the newer version into the project as well.  