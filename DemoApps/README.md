# Demo Apps
There are three WPF demo applications to demonstrate the Timeline control's compatibility with:
- .NET Framework (4.6.2)
- .NET 6.0
- .NET 8.0

The demo applications are largely the same, but there are a few small differences to demonstrate different scenarios:

| Demo App | Timeline DataContext | Default culture (set in App.xaml.cs) | 
| --- | --- | --- |
| .NET Framework | Binding with basic MVVM | en |
| .NET 6.0 | Code-behind | ko |
| .NET 8.0 | Binding with CommunityToolkit.MVVM | en |

## Shared Resources
The three applications share some resource files from the [Resources](/Resources) folder, but with some drawbacks:
- The .NET Framework app duplicates the .ico file into its own Resources folder when the file is added as an Embedded Resource. There is no good way around this.
- The three applications duplicate the default Strings.resx file in their own Resources folder; even though there is nothing project-specific in the .resx file, it does not appear to be possible to have each application generate its own version of the Strings.Designer.cs file into a local folder based on a shared .resx file.
