# GhaUnityBuildReporter

GhaUnityBuildReporter is a Unity package that automatically reflects [build report](https://docs.unity3d.com/ScriptReference/Build.Reporting.BuildReport.html) in GitHub Job Summary when building with Unity on GitHub Actions.

<img width="400" alt="top" src="https://github.com/VeyronSakai/GhaUnityBuildReporter/assets/43900255/ce50ca9a-928d-458a-8350-ff0630bdea0a">

This package is inspired by [Unity-Technologies/BuildReportInspector](https://github.com/Unity-Technologies/BuildReportInspector).

## Features

GhaUnityBuildReporter specifically reflects the following information in the Job Summary.

- Basic Info
- Build Steps
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

### Output Files

List of file paths output during the build and their sizes.

<img width="600" alt="output-files" src="https://github.com/VeyronSakai/GhaUnityBuildReporter/assets/43900255/20bd078a-8608-4efd-a665-19635ffe7b10">

### Included Modules

Information about the native engine modules included in the build and why they were included.

<img width="300" alt="include-modules" src="https://github.com/VeyronSakai/GhaUnityBuildReporter/assets/43900255/af5fffda-d336-4653-a449-c8035383c179">

## Setup

基本的にはこのパッケージを Unity Project に追加するだけでセットアップが完了します。

注意:
Docker コンテナ内で Unity によるビルドを行うような場合はサポートしておりません。

## Usage

## License

This software is released under the MIT License. Please see the LICENSE file for details.
