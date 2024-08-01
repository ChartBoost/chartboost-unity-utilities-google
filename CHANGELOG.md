# Changelog
All notable changes to this project will be documented in this file using the standards as defined at [Keep a Changelog](https://keepachangelog.com/en/1.0.0/). This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0).

### Version 1.0.0 *(2024-08-01)*

First version of Chartboost Google Utilities package for Unity.

Added:

- `EditorMenu` at `Chartboost/Google/Configure`.
- `GoogleSettings` scriptable object to save persistent Google application id settings.
- `IPostprocessBuildWithReport` script to patch `Info.plist` file.
- `IPreprocessBuildWithReport` script to patch `AndroidManifest.xml` file.