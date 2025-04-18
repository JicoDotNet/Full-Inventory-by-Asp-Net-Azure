# Inventory, Billing & GST System

## Table of Contents of Change Log
- [Version 2.0.0](#version-200)
- [Version 1.0.0](#version-100)  

## Version 2.0.0
### New Features
- Frameworks Version upgradation. Class Library projects upgrade to `.netstandard` to handle by `.net8.0` & `.net48` both apps.

### Changed/Breaking/Removed Features
- Remove `System.Data.SqlClient` packages from `DataAccess.Sql`.
- Add `Microsoft.Data.SqlClient` packages at `DataAccess.Sql`.
- **`DataAccess.AzureStorage`** packages upgrade to **3.0.0**

### Fixed Bug
- `IGenericDescription` interface is added in `JicoDotNet.Inventory.Core` project to remove ambiguity of the `Description` property.

### Maintenance
- `DataAccess.Sql` project upgrade to multitarget frameworks to `netstandard2.0` & `netstandard2.1`
- **`JicoDotNet.Inventory.Core`** upgrade to `.netstandard2.0`  
- **`JicoDotNet.Inventory.Helper`** upgrade to `.netstandard2.0` 
- **`JicoDotNet.Inventory.Logging`** upgrade to `.netstandard2.0`
- **`JicoDotNet.Inventory.BusinessLayer`** upgrade to `.netstandard2.0`



## Version 1.0.0
### New Features
- Initial release for open source code with full features.
### Changed/Breaking/Removed Features
- N/A
### Fixed Bug
- N/A
### Maintenance
- N/A