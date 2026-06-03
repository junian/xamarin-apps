<div align="center">

<img src="https://raw.githubusercontent.com/junian/commons-media/refs/heads/master/svg/xamarin-logo.svg" height="196px" />

# .NET Xamarin Apps

Xamarin Code samples

</div>

## What's Inside?

- Xamarin.Forms Video Background Project
  - [Source Code](./src/background-video/)
  - [Full Guide](https://www.junian.dev/tech/xamarin-forms-video-background/) 
- Xamarin.Forms Custom Font Project
  - [Source Code](./src/custom-font/)
  - [Full Guide](https://www.junian.dev/tech/xamarin-forms-android-custom-font/)
- Xamarin.Forms Audio Player Project
  - [Source Code](./src/audio-player/)
  - [Full Guide](https://www.junian.dev/tech/xamarin-forms-play-audio/)
- Xamarin.Forms Todo App
  - [Source Code](./src/todo-app/)

## How to Build with Mac

Requirements:

- [Xcode][xcode]
- Android SDK
  
  ```bash
  brew install android-commandlinetools
  ```

- Mono MDK and Xamarin frameworks
  
  ```bash
  brew tap junian/dotnet
  brew install mono-mdk@6.12 xamarin-mac@9.3 xamarin-ios@16.4 xamarin-android@13.2
  ```

- IDE: You can use [JetBrains Rider][rider].
- Alternative IDE: Visual Studio 2022 for Mac

  ```bash
  brew install junian/dotnet/visual-studio@2022
  ```

## Xamarin iOS Troubleshooting

If you're experiencing build issue for Xamarin iOS with message like this:

> error HE0004: Could not load the framework 'IDEDistribution' (path: /Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/IDEDistribution): 
dlopen(/Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/IDEDistribution, 0x0001): Library not loaded: @rpath/AppThinning.framework/Versions/A/AppThinning
  Referenced from: <6546882B-CA27-3A00-AF75-BFD972711FF8> /Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/IDEDistribution
  Reason: tried: '/usr/lib/swift/AppThinning.framework/Versions/A/AppThinning' (no such file, not in dyld cache), '/System/Volumes/Preboot/Cryptexes/OS/usr/lib/swift/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Library/Frameworks/Xamarin.iOS.framework/Versions/16.4.0.23/lib/mlaunch/mlaunch.app/Contents/Frameworks/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/Frameworks/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/../../../../AppThinning.framework/Versions/A/AppThinning' (no such file), '/usr/lib/swift/AppThinning.framework/Versions/A/AppThinning' (no such file, not in dyld cache), '/System/Volumes/Preboot/Cryptexes/OS/usr/lib/swift/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Library/Frameworks/Xamarin.iOS.framework/Versions/16.4.0.23/lib/mlaunch/mlaunch.app/Contents/Frameworks/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/Frameworks/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/../../../../AppThinning.framework/Versions/A/AppThinning' (no such file), '/Library/Frameworks/Xamarin.iOS.framework/Versions/16.4.0.23/lib/mlaunch/mlaunch.app/Contents/MonoBundle/AppThinning.framework/Versions/A/AppThinning' (no such file)
        
Follow the [Link Xcode AppThinning to Xamarin iOS guide][xamarin-app-thinning].

## LICENSE

[MIT](./LICENSE)

[xamarin-app-thinning]: <https://www.junian.dev/dev/xamarin-forms-ios-framework-idedistribution-issue/>
[xcode]: <https://developer.apple.com/xcode/>
[rider]: <https://www.jetbrains.com/rider/>
