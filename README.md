# Core for Unity Games

## Scripts

### CommonCore
- ISaveDataContainer (PlayerPrefsSaveDataContainer)
- SaveLoad module
- LocalLeaderboard - leaderboard for best scores on the device
- UpdateService - service to replace and optimize MonoBehaviour.Update/FixedUpdate/LateUpdate

### Core
- Audio - async UniTask playing of sound split into SFX/Music channels, needs to implement ISoundNameCollection and implement ISoundNamesCollection
- Collisions - ReactiveX (Rx) adapter for Unity collisions, needs to be implemented for 3D
- Compliments - pop-up encouraging feedback words
- Configs/Levels module for building levels
  - ILevelViewData - base interface for levels config, with a unique ID for analytics and progress tracking

## Pools
- Simple pools implementation

## ScreenNavigation
- Simple UI navigation from screen to screen

## States
- State machine to split the game cycle into readable states and handle other special cases

## Tools
- InEditorBootstrapper - script for running the game from any scene from the very beginning
- UserInput - OnScreenJoystick and MouseTouchInput (needs to replace the legacy input system)

## Other
- Booting
  - AppSession - starts the app session and loads the initial state
  - BootstrapBase - the base app bootstrapper
  - IBootstrapable - service for booting app services

  ****
- CameraProvider - Saves the main render camera
- SceneLoader - async UniTask scene loading
- SceneName - in-game scene names (needs to refactor to match AudioService)

## Models
- Analytics
