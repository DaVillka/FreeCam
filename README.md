# Rage Freecam
=============
#Porting from fivem-freecam
Original link: https://github.com/Deltanic/fivem-freecam

Features
--------

- Easy to use freecam API
- Improved state accuracy over native GTA
- Moves with the minimap
- Adjustable moving speed
- Support for keyboard and gamepads
- Fully configurable

Controls
--------

These are the default controls for the freecam. Keep in mind controls may be
different depending on your game settings or keyboard layout.

### Keyboard

- Mouse to look around
- W and S to move forward and backward
- A and D to move left and right
- Q and E to move up and down
- Alt to slow down
- Shift to speed up

### Gamepad

- Left joystick to move around
- Right joystick to look around
- Left button to move down
- Right button to move up
- Left trigger to slow down
- Right trigger to speed up

Usage
-----

```C#
FreeCam.SetCameraSetting(CameraSetting.FOV, Cam.GetGameplayCamFov());
Input.Bind(RAGE.Ui.VirtualKeys.X, false, () =>
{
    FreeCam.Active = !FreeCam.Active;
});
```

