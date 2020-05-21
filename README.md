# ZipContent.Azure
![build & test](https://github.com/hkutluay/ZipContent.Azure/workflows/build%20&%20test/badge.svg)

Lists zip file content on Azure Blob service without downloading whole document. Supports both zip and zip64 files.


# Usage

First install ZipContent.Azure via NuGet console:
```
PM> Install-Package ZipContent.Azure
```

Sample usage:
```csharp
string containerName = "test";
string fileName = "foo.zip";

var connectionString = "DefaultEndpointsProtocol=https;AccountName=accountname;AccountKey=/zzzz;EndpointSuffix=core.windows.net";
BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

// Get container client object
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

// Get a reference to a blob
BlobClient blobClient = containerClient.GetBlobClient(fileName);

IPartialFileReader partialReader = new AzurePartialFileReader(blobClient);

IZipContentLister lister = new ZipContentLister();

var contentList = lister.GetContents(partialReader);

foreach (var content in contentList)
   Console.WriteLine(item.FullName);
 ```
