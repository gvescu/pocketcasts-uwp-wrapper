# UWP Wrapper for Pocketcasts Web App

This is a just-for-fun project, as my first dabble into UWP/C# world.

Maybe I just should have used the Hosted Web App template, but:
* I want to learn C# and UWP app development
* I needed to use the media keys and I can't inject code into Hosted Web Apps to do that (there must be a way, so if it is somewhere I may nuke this project to implement that)

Project inspired by [Morten Just](http://mortenjust.com/)'s [Pocket Casts for Mac](https://github.com/mortenjust/PocketCastsOSX) wrapper.

---

## Current status
- Load Pocketcasts web app on launch using WebView.
- Remember login status (thanks to WebView).
- Support for Play/Pause, Forward (30s) and Rewind (10s) buttons.


## TODO list
- [ ] Learn ~~to program damn asyncs~~ C# and UWP intricacies.
- [x] Make the size of the WebView element the same of the viewport.
- [ ] Update the media controls display with podcast info.
- [ ] Polish (A LOT)
