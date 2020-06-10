# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0] - **Unreleased**
### Added
- Sounds utility functions
- Sounds for various actions
- Options screen with option to enable sounds
- Save, load, and delete presets using json files
- Restore defaults button
- Load preset from alpha.txt or alphax.txt
- Delete preset
### Fixed
- Slow window resizing
- Sounds playing when preset is loaded
- Description text not updating when preset is loaded
- Presets can be saved and loaded with no name
- Added Costura.Fody package so DLLs for json library are included in exe
### Changed
- Redesigned General tab
- General refactoring
- Some UI updates

## [1.0.0] - 2020-05-28
### Added
- Tabs for General, Other, Units, Facilities
- Save alpha.txt and alphax.txt - overwrite game file or save as...
- Set game resolution
- Open game folder in windows explorer
- Automatically find game installation directory
- Search for or manually set game installation folder
- Sound utility functions
- Basic hurry cost calculator
