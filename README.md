# Hackweek 2023

Very simple game that shows Unity interstitial (non rewarded) ads and collects analytics.
<img src="Screenshot 2023-02-10 at 11.43.44.png"/>

Using Unity 2021.3.11f1
- Install support for ios
- Create 3D URP project

In Unity Editor go to Package Manager and install
- Advertisement 4.3.0 (legacy ads)
- Analytics 4.2.0
- Cinemachine 2.8.9

In Unity Editor Project settings
- Link/create Unity project in Services 
- You should now have gameIds for both ios and android

In Unity Editor
- Locate `AdsThingy` and edit its script variables to have correct gameIds
- Toggle testmode to have test/real ads when running game in real phone

In Unity Dashboard
- ads https://dashboard.unity3d.com/gaming/monetization
  - using the default placements and configs
- analytics https://dashboard.unity3d.com/gaming/analytics
  - added new custom events in event manager 
    - "adStarted"
    - "tapOnScreen"

To test on iPhone
- Install and open Xcode 

- In Unity Editor go to build settings
  - select GameScene to build
  - switch platform to iOS
  - build
  - when complete, build folder opens and click `.xcodeproj` to open Xcode

- In Xcode
  - connect iPhone
  - click the root level `Unity-iPhone` and select Signing & Capabilities
  - enable automatic signing, choose your iphone apple account as the team
  - have globally unique package id
  - also setup developer mode, add your apple account etc, so that you're able to get the signing cert to work
  - press play to get the build running and app should appear in your phone

<img src="Screenshot 2023-02-10 at 11.42.00.png"/>