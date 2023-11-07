# Ji2Core
Core for unity games
**Scripts**
  **CommonCore**
    -ISaveDataContainer(PlayerPrefsSaveDataContainer) - SaveLoad module
    -LocalLeaderboard - leaderboard for best scores on device
    -UpdateService - service to replace and optimeze Monobehaviour.Update/Fixedupdate/LateUpdate
  **Core**
    -Audio - async Unitask playing of sound splited in SFX/Music channels, need to implement ISoundNameCollection and implement ISoundNamesCollection
    -Collisions - rx adapter for unity collisions **need to  implement for 3d**
    -Compliments - pop-up encourage feedback words
    **Configs/Levels** module for building levels 
      ILevelViewData - base interface for levels config, has unique Id for analytics and progress tracking
    **Pools** Sinple pools implementation
    **ScreenNavigation** Simple UI navigation from screen to screen
    **States** State machine, basicly to split game cycle into readable states, and other special cases
    **Tools** 
      -InEditorBootstraper - script for running game from any scene from the very beggining
    **UserInput** - OnScreenJoystick and MouseTouchInput(**need to replace legacy input system**)
    **Other**
      **Booting**
      -AppSession - starts app session and loads initial state
      -BootstrapBase - base app bootstrapper
      -IBootstrapable - service for booting app services
      ****
      -CameraProvider - Saves Main RenderCamera
      -SceneLoader - async Unitask scene loading
      -SceneName - ingame scene names(**need to refactor to match audioservice**)
  **Models**
    **Analytics**
    
  
