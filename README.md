<div align="center">

<img src="https://raw.githubusercontent.com/junian/commons-media/refs/heads/master/svg/xamarin-logo.svg" height="196px" />

# .NET Xamarin Apps

Xamarin Code samples

</div>

## Content

- [Xamarin Forms custom font demo project](./src/custom-font/)
- [To Do App](./src/todo-app/)

## Troubleshooting

If you're experiencing build issue for Xamarin iOS with message like this:

> error HE0004: Could not load the framework 'IDEDistribution' (path: /Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/IDEDistribution): 
dlopen(/Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/IDEDistribution, 0x0001): Library not loaded: @rpath/AppThinning.framework/Versions/A/AppThinning
  Referenced from: <6546882B-CA27-3A00-AF75-BFD972711FF8> /Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/IDEDistribution
  Reason: tried: '/usr/lib/swift/AppThinning.framework/Versions/A/AppThinning' (no such file, not in dyld cache), '/System/Volumes/Preboot/Cryptexes/OS/usr/lib/swift/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Library/Frameworks/Xamarin.iOS.framework/Versions/16.4.0.23/lib/mlaunch/mlaunch.app/Contents/Frameworks/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/Frameworks/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/../../../../AppThinning.framework/Versions/A/AppThinning' (no such file), '/usr/lib/swift/AppThinning.framework/Versions/A/AppThinning' (no such file, not in dyld cache), '/System/Volumes/Preboot/Cryptexes/OS/usr/lib/swift/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Library/Frameworks/Xamarin.iOS.framework/Versions/16.4.0.23/lib/mlaunch/mlaunch.app/Contents/Frameworks/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/Frameworks/AppThinning.framework/Versions/A/AppThinning' (no such file), '/Applications/Xcode.app/Contents/SharedFrameworks/IDEDistribution.framework/Versions/A/../../../../AppThinning.framework/Versions/A/AppThinning' (no such file), '/Library/Frameworks/Xamarin.iOS.framework/Versions/16.4.0.23/lib/mlaunch/mlaunch.app/Contents/MonoBundle/AppThinning.framework/Versions/A/AppThinning' (no such file)
        
Follow the [Xcode AppThinning guide][xamarin-app-thinning].

## LICENSE

[MIT](./LICENSE)

[xamarin-app-thinning]: <https://www.junian.net/dev/xamarin-forms-ios-framework-idedistribution-issue/>
