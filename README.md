# DOSBox Staging VCT | Visual Config Tool

DOSBox Staging VCT: A Visual Configuration Tool for DOSBox Staging

Developed in .NET (C#), currently only available for Windows, it allows you to create and modify configuration files for [DOSBox Staging](https://www.dosbox-staging.org/) visually.

The DOSBox Staging VCT project is designed to simplify the creation and modification of configuration files (.conf) for DOSBox Staging in a user-friendly, visual manner. Here are the key details:

- Prerequisite:
   - Before using it, make sure you have [DOSBox Staging](https://www.dosbox-staging.org/) installed.
   - [.NET Core 8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
 
- Features:
  - Although it is in an initial state, it is fully functional.
  - Emphasizes ease of use as a primary requirement.
  - Detects portable and non-portable installations, allowing you to convert non-portable installations to portable ones with a single click.
  - Launch any configured DOS game or application within DOSBox.
  - The launcher supports custom parameters.

- Internal Dependencies:
  It internally utilizes the following libraries:
   - [ini-parser-netstandard](https://github.com/rickyah/ini-parser)
   - [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)

[DOSBox Staging](https://www.dosbox-staging.org/) is a modern continuation of DOSBox with advanced features and current development practices.
