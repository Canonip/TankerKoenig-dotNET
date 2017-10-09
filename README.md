# Tankerkoenig-dotNet
[![NuGet](https://img.shields.io/nuget/v/TankerKoenig.svg)](https://www.nuget.org/packages/TankerKoenig)

This is a Wrapper for the Tankerkoenig API for the .NET Framework.

Tankerkoenig is a website, with all Gas Stations in Germany and their prices provided by MTS-K (Markttransparenzstelle für Kraftstoffe)

Feel free to edit the Code and create a Pull Request!
## Usage
Get yourself an API Key from Tankerkoenig [here](https://creativecommons.tankerkoenig.de/api-key)


Install [TankerKoenig package](https://www.nuget.org/packages/TankerKoenig/)
```
PM> Install-Package TankerKoenig
```

Create instance of TankerKoenigManager and search for Gas Stations in your Area.

```csharp
string apikey = "00000000-0000-0000-0000-000000000000";
var manager = new TankerKoenigManager(apikey);

//Coordinates: 52.521, 13.438
//Search radius: 1.5km
var foundStations = await manager.SearchForGasStations(52.521, 13.438, 1.5);
```