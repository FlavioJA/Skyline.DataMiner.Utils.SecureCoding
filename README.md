# Skyline.DataMiner.Utils.SecureCoding
A NuGet library offering a set of robust and **secure coding methods for .NET development**. With a focus on mitigating common security vulnerabilities, this package equips developers with a curated collection of methods to ensure the integrity and resilience of their applications against potential threats. 

## Overview
Secure Coding is a NuGet library designed to streamline secure development by minimizing the need for boilerplate code. This library offers a set of methods and functionalities aimed at enhancing the security of your applications, reducing common vulnerabilities, and promoting secure coding practices.

## Features
- **Boilerplate Code Reduction:** Secure Coding simplifies the integration of security measures into your application by providing a set of methods that handle common security concerns. This allows developers to focus on the core functionality of their code without sacrificing security.
- **Security Best Practices:** The library follows industry-accepted security best practices and incorporates them into its methods. This ensures that developers benefit from proven security measures without having to delve into the intricacies of implementation.
- **Customizable Security Components:** While offering predefined secure methods, Secure Coding also allows for customization based on specific project requirements. Developers can tailor security components to fit the unique needs of their applications.

## Installation

# Skyline.DataMiner.Utils.SecureCoding.Analyzers
A complementary NuGet package designed to complement your security arsenal by harnessing the powerful capabilities of the **Roslyn compiler platform**. It provides a suite of code analysis tools meticulously crafted to elevate the security posture of your .NET applications.

# Analyzers's rules
|Id|Category|Description|Severity|Is enabled|Code fix|
|--|--------|-----------|:------:|:--------:|:------:|
|[SLC-SC0001]()|Usage|File operation usage detected without secure path construction|<span title='Warning'>⚠️</span>|✔️|❌|
|[SLC-SC0002]()|Usage|Avoid using 'System.IO.Path.Combine'|<span title='Warning'>⚠️</span>|✔️|✔️|
|[SLC-SC0003]()|Usage|Avoid using the JavaScriptSerializer for (de)serialization.|<span title='Warning'>⚠️</span>|✔️|❌|
|[SLC-SC0004]()|Usage|Avoid deserializing json strings by using Newtonsoft directly.|<span title='Warning'>⚠️</span>|✔️|✔️|

# .editorconfig - default values

```editorconfig
# SLC-SC0001: File operation usage detected without secure path construction
dotnet_diagnostic.SLC-SC0001.severity = warning

# SLC-SC0002: Avoid using 'System.IO.Path.Combine'
dotnet_diagnostic.SLC-SC0002.severity = warning

# SLC-SC0003: Avoid using the JavaScriptSerializer for (de)serialization.
dotnet_diagnostic.SLC-SC0003.severity = warning

# SLC-SC0004: Avoid deserializing json strings by using Newtonsoft directly.
dotnet_diagnostic.SLC-SC0004.severity = warning
```

# .editorconfig - all rules disabled

```editorconfig
# SLC-SC0001: File operation usage detected without secure path construction
dotnet_diagnostic.SLC-SC0001.severity = none

# SLC-SC0002: Avoid using 'System.IO.Path.Combine'
dotnet_diagnostic.SLC-SC0002.severity = none

# SLC-SC0003: Avoid using the JavaScriptSerializer for (de)serialization.
dotnet_diagnostic.SLC-SC0003.severity = none

# SLC-SC0004: Avoid deserializing json strings by using Newtonsoft directly.
dotnet_diagnostic.SLC-SC0004.severity = none
```
