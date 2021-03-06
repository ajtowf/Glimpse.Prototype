# Change Log
All notable changes to this project will be documented in this file, which has been inspired by the example and guidance at [keepachangelog.com](http://keepachangelog.com/) 

This project adheres to [Semantic Versioning](http://semver.org/).

## [Current Focus][focus] - Unreleased
### Added
- [Support "auto-magically" injecting `<script>` tags, like Glimpse 1.x does.](https://github.com/Glimpse/Glimpse.Prototype/pull/14)

### Changed
- Dropped support for web sockets for now. Favoring HTTP POST's and Server Sent Events.
- Integrated in a default, in-memory message store.
- Added `[PromoteTo("...")]` and `[PromoteToXyz]` attributes and support to promote message properties into the indices bag.
- [`IResource`](https://github.com/Glimpse/Glimpse.Prototype/pull/23#issuecomment-133591273) and [`IInspector`](https://github.com/Glimpse/Glimpse.Prototype/pull/31#issue-103361314) designs are complete and implemented.
- Removed hosting layer abstration from original v2 prototype. 
- Update to ASP.NET 5 1.0.0-beta7-12290, along with [ASP.NET Beta 5 Visual Studio 2015 Tooling](http://blogs.msdn.com/b/webdev/archive/2015/07/27/announcing-availability-of-asp-net-5-beta-6.aspx).
- Added [readme.md](https://github.com/Glimpse/Glimpse.Prototype/blob/dev/src/readme.md) that covers how to run sample projects.

[focus]: https://github.com/Glimpse/Glimpse.Prototype/milestones/2015.09.04%20-%203
