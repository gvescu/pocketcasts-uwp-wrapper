# UWP Wrapper for Pocketcasts Web App

This is a just-for-fun project, as my first dabble into UWP/C# world.

Maybe I just should have used the Hosted Web App template, but:
* I want to learn C# and UWP app development
* I needed to use the media keys and I can't inject code into Hosted Web Apps to do that (there must be a way, so if it is somewhere I may nuke this project to implement that)

Project inspired by [Morten Just](http://mortenjust.com/)'s [Pocket Casts for Mac](https://github.com/mortenjust/PocketCastsOSX) wrapper.

![Screenshot](https://raw.githubusercontent.com/gvescu/pocketcasts-uwp-wrapper/master/images/screenshot.png)

---

## How do I get it?
[Check the releases page](https://github.com/gvescu/pocketcasts-uwp-wrapper/releases). It's an .appxbundle that you can install by double click if you have sideloading enabled (which, apparently, everyone has enabled since build 10586).

## Current status
- Load Pocketcasts web app on launch using WebView.
- Remember login status (thanks to WebView).
- Support for Play/Pause, Forward (30s) and Rewind (10s) buttons.


## TODO list
- [ ] Learn ~~to program damn asyncs~~ C# and UWP intricacies.
- [x] Make the size of the WebView element the same of the viewport.
- [ ] Update the media controls display with podcast info.
- [ ] Polish (A LOT)
- [ ] Test on more machines (only Win10 x64 so far)
