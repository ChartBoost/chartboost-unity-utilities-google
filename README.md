# Chartboost Canary Utilities

Simple utilities package for the Chartboost Unity development environment.

# Installation
This package is meant to be a dependency for other Chartboost Packages; however, if you wish to use it by itself, it can be installed through UPM & NuGet as follows:

## Using the public [npm registry](https://www.npmjs.com/search?q=com.chartboost.unity.utilities.google)
```json
"dependencies": {
    "com.chartboost.unity.utilities.google": "1.0.0",
    ...
},
"scopedRegistries": [
{
    "name": "NpmJS",
    "url": "https://registry.npmjs.org",
    "scopes": [
    "com.chartboost"
    ]
}
]
```

## Using the public [NuGet package](https://www.nuget.org/packages/Chartboost.CSharp.Utilities.Google.Unity)

To add the Chartboost Core Unity SDK to your project using the NuGet package, you will first need to add the [NugetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) package into your Unity Project.

This can be done by adding the following to your Unity Project's ***manifest.json***

```json
  "dependencies": {
    "com.github-glitchenzo.nugetforunity": "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity",
    ...
  },
```

Once <code>NugetForUnity</code> is installed, search for `Chartboost.CSharp.Utilities.Google.Unity` in the search bar of Nuget Explorer window(Nuget -> Manage Nuget Packages).
You should be able to see the `Chartboost.CSharp.Utilities.Google.Unity` package. Choose the appropriate version and install.

# Usage

This package creates an `Editor` only scriptable object to contain your Google application Id. The id will be utilized to patch your `AndroidManifest.xml` & `Info.plist`, for the Android and iOS platforms respectively.

To configure, utilize the `EditorMenu` located at: `Chartboost/Google/Configure`, then input your application ids as needed.