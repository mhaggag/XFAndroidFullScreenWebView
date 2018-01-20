# Xamarin Forms Full-screen WebView on Android
This is a small sample illustrating how to enable full-screen support in a Xamarin Forms WebView for Android. It does not cover other Xamarin platforms such as iOS. It was written in response to a thread on the Xamarin forums where I described the solution in [this comment](https://forums.xamarin.com/discussion/comment/310812/#Comment_310812).

It is licensed under the MIT license, so do what you will with it.

## Summary
The solution contains two projects: A Xamarin Android application and a .NET standard library containing the Xamarin Forms UI. The library contains one page, `MainPage`, which defines a `WebView` and points it to YouTube.

The library defines a custom `FullScreenEnabledWebView` which extends `WebView` and adds two commands for entering/exiting full-screen mode. A default implementation is provided for the commands; it pushes/pops a modal page containing the full-screen content. You may wish to add your command to perform additional actions, such as truly going full-screen (by hiding the status bar) and/or requesting landscape orientation.

The heart of the implementation lies in the renderer, `FullScreenEnabledWebViewRenderer`, and its supporting classes:
* `FullScreenEnabledWebChromeClient`: An extension of `FormsWebChromeClient` (which in turn is an extension of `WebChromeClient`) that implements the necessary callbacks for full-screen operation.
* `EnterFullScreenRequestedEventArgs`: Event arguments for the event raised when the web view content requests entering full-screen in response to a user action. Android passes the full-screen content in an Android view, and this event exposes that view to the renderer.

The renderer listens to `Enter/ExitFullScreenRequested` events on the web chrome client, transforms the native Android view to a Xamarin Forms view, and forwards the call to `FullScreenEnabledWebView` through its public enter/exit commands.

## Limitations
The YouTube videos I tried did not play for me on the Google Android emulator. I could hear the sound but the display was black. It works on my Android device, so *make sure you to test on a real device*. Non-video content works for me on the emulator, so I'm assuming it's a codec-specific or GPU/rendering-specific issue with videos.

## Improvements
Feel free to suggest improvements or implement them yourself and create a pull request. I cannot promise that I'll implement anything on a timely basis, but I'll try.

## Other platforms
I haven't tried this on other platforms except iOS. To the best of my knowledge, iOS web views do not support full-screen operation. What I did in my project was to emulate full-screen operation entirely manually on iOS:
* Show enter/exit full-screen buttons overlayed on content using an `AbsoluteLayout`, `GridLayout`, or similar. This is necessary because the iOS web view does not support the full-screen API, so websites do not show the full-screen buttons.
* Respond to the enter/exit buttons and invoke the enter/exit full-screen commands on `FullScreenEnabledWebView`, doing any extra platform-specific work before/after them. You can use composite commands for this if you have them.

Naturally, if you figure out a better way to do this on iOS, I'm all ears :)
