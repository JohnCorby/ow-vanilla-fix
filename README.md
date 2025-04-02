![cover image](cover.png)

# VanillaFix
Fixes some vanilla bugs in Outer Wilds

## Fixes
- Makes the proxy sun's atmosphere look correct. (credit to MegaPiggy)
- Properly resets PlayerState on scene change.
- Adds some missing button prompt textures. (credit to MegaPiggy)
- Prevents SetLanguage from being called when it shouldn't be, fixing several mods.
- Lets input turn off when the game isn't in focus, meaning mods that turn on runInBackground work.
- Fixes places where the probe is not null checked (since it can be destroyed).
- Fixes a hard-to-find visual bug with projection pools.
- [Fixes an issue where probe would sometimes stretch when anchored to an object](https://github.com/JohnCorby/ow-vanilla-fix/issues/7). This isn't a perfect solution, but it is not possible to do it better in unity.
- Fixes an issue where fog shaders are not updated when the `PlayerFogWarpDetector` is disabled (resulted in permanent fog on screen if you leave Bramble too quickly, ie, by teleporting) (credit to xen)
- [Fixes an issue where dialogue nodes basically ignore persistent conditions](https://github.com/JohnCorby/ow-vanilla-fix/issues/17) 
- [Fix weird floating piece of signalscope UI](https://github.com/JohnCorby/ow-vanilla-fix/pull/28) (credit to Spacemike)
- [Fix line wrapping in CJK languages](https://github.com/JohnCorby/ow-vanilla-fix/pull/27) (credit to orclecle)
- [Fix noise sensor using the wrong position for the object making the noise](https://github.com/JohnCorby/ow-vanilla-fix/pull/30) (credit to Etherpod)
