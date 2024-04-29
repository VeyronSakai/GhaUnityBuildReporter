# GhaUnityBuildReporter

GhaUnityBuildReporter is a Unity package that automatically reflects [build report](https://docs.unity3d.com/ScriptReference/Build.Reporting.BuildReport.html) in GitHub Job Summary when building with Unity on GitHub Actions.

<img width="400" alt="top" src="https://github.com/VeyronSakai/GhaUnityBuildReporter/assets/43900255/ce50ca9a-928d-458a-8350-ff0630bdea0a">

[Here](https://github.com/VeyronSakai/GhaUnityBuildReporter/actions/runs/8864812982) is an example of a specific build report.

This package is inspired by [Unity-Technologies/BuildReportInspector](https://github.com/Unity-Technologies/BuildReportInspector).

## Features

GhaUnityBuildReporter specifically reflects the following information in the Job Summary.

- Basic Info
- Build Steps
- Source Assets
- Output Files
- Included Modules

> [!NOTE]
> Items with no information to display are hidden. For example, if the Scripting Backend is Mono instead of IL2CPP, Included Modules will be hidden.

### Basic Info

The basic information about the build.

| Item | Description |
| --- | --- |
| Platform | The platform that the build was created for. |
| Total Time | The total time taken by the build process. |
| Total Size | The total size of the build output. |
| Build Result | The outcome of the build. |
| Total Errors | The total number of errors and exceptions recorded during the build process. |
| Total Warnings | The total number of warnings recorded during the build process. |

<img width="200" alt="basic-info" src="https://github.com/VeyronSakai/GhaUnityBuildReporter/assets/43900255/c2822a2b-77dd-4ba1-9512-065613e67907">

### Build Steps

All build steps and the duration of each step and the messages output within the step.

<img width="400" alt="build-steps" src="https://github.com/VeyronSakai/GhaUnityBuildReporter/assets/43900255/1feddaa6-a9c0-4f0d-9a1f-46558d5ec944">

Each emoji corresponds to the following [LogType](https://docs.unity3d.com/ScriptReference/LogType.html).

| Emoji | LogType |
| --- | --- |
| :x: | Error |
| :no_entry_sign: | Assert |
| :warning: | Warning |
| :information_source: | Log |
| :boom: | Exception |
| :question: | Unknown |

### Source Assets

Information on all assets used in the build.

<img width="650" alt="output-files" src="https://github.com/VeyronSakai/GhaUnityBuildReporter/assets/43900255/dd07dd3e-cbdd-46c4-831d-83465fe05e17">

### Output Files

File paths output during the build and their sizes.

<img width="600" alt="output-files" src="https://github.com/VeyronSakai/GhaUnityBuildReporter/assets/43900255/20bd078a-8608-4efd-a665-19635ffe7b10">

### Included Modules

The names of the native engine module included in the build and the reasons the module was included in the build.

<img width="300" alt="include-modules" src="https://github.com/VeyronSakai/GhaUnityBuildReporter/assets/43900255/af5fffda-d336-4653-a449-c8035383c179">

## Setup

Because all the processing is done in Unity post-processing, the setup is basically completed by simply adding GhaUnityBuildReporter to the Unity project by following the steps below;

1. Open the Package Manager in the UnityEditor.
2. Select the `+` button in the upper left corner.
3. Select Add package from git URL.
4. Enter https://github.com/VeyronSakai/GhaUnityBuildReporter.git?path=Assets/GhaUnityBuildReporter and Select Add button.

It can also be installed by downloading .unitypackage from [Releases](https://github.com/VeyronSakai/GhaUnityBuildReporter/releases/latest).

> [!WARNING]
> The Workflow file of GitHub Actions basically does not need to be changed, but if you use Docker to build Unity, you must copy the files at `$GITHUB_STEP_SUMMARY` in the Docker container to the path at `$GITHUB_STEP_SUMMARY` on the host machine after building with Unity.

## How to suppress GhaUnityBuildReporter

Perhaps there is a Workflow or Job for which you would like to suppress GhaUnityBuildReporter.

In such cases, setting the environment variable `GHA_UNITY_BUILD_REPORTER_OPTOUT` to `1` or `true` will suppress GhaUnityBuildReporter in the scope where that environment variable is valid.

## Contribution

Bugs and new feature suggestions are welcome in issues and Pull Requests.

## License

This software is released under the MIT License. Please see the [LICENSE](https://github.com/VeyronSakai/GhaUnityBuildReporter/blob/main/LICENSE) file for details.
