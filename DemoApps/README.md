# Demo Apps
There are two WPF demo applications to demonstrate the Timeline control's compatibility with:
- .NET Framework (4.6.2)
- .NET 8.0

The demo applications use the [WPFLocalizeExtension](https://www.nuget.org/packages/WpfLocalizeExtension) package to demonstrate localisation of the control.

The demo applications are largely the same, but there are a few small differences to demonstrate different scenarios:

| Demo App | Timeline DataContext | Default culture (set in App.xaml.cs) | 
| --- | --- | --- |
| .NET Framework | Binding with basic MVVM | ko |
| .NET 8.0 | Binding with CommunityToolkit.MVVM | en |

## Shared Resources
The demo applications share some resource files from the [Resources](/DemoApps/Resources) folder, but with one drawback:
- The demo applications duplicate the default Strings.resx file in their own Resources folder; even though there is nothing project-specific in the .resx file, it does not appear to be possible to have each application generate its own version of the Strings.Designer.cs file into a local folder based on a shared .resx file.
