using RAGE;
using HardLife.Utils.FreeCamera;

internal static class FreeCam
{

    public static bool Active { get => Camera.IsFreecamActive(); set => Camera.SetFreecamActive(value); }
    public static bool Frozen { get => Camera.IsFreecamFrozen(); set => Camera.SetFreecamFrozen(value); }
    public static float Fov { get => Camera.GetFreecamFov(); set => Camera.SetFreecamFov(value); }
    public static Vector3 Position { get => Camera.GetFreecamPosition(); set => Camera.SetFreecamPosition(value.X, value.Y, value.Z); }
    public static Vector3 Rotation { get => Camera.GetFreecamRotation(); set => Camera.SetFreecamRotation(value.X, value.Y, value.Z); }
    public static (Vector3, Vector3, Vector3, Vector3) GetMatrix { get => Camera.GetFreecamMatrix(); }
    public static Vector3 GetTarget(float distande) { return Camera.GetFreecamTarget(distande); }

    public static float GetGetPitch { get => Camera.GetFreecamRotation().X; }
    public static float GetRoll { get => Camera.GetFreecamRotation().Y; }
    public static float GetYaw { get => Camera.GetFreecamRotation().Z; }

    public static int GetKeyboardControl(CamControl key) { return ControlMappings.KEYBOARD_CONTROL_MAPPING[key]; }
    public static int GetGamepadControl(CamControl key) { return ControlMappings.GAMEPAD_CONTROL_MAPPING[key]; }
    public static T GetKeyboardSetting<T>(ControlSetting controlSetting) { return (T)ControlMappings.KEYBOARD_CONTROL_SETTINGS[controlSetting]; }
    public static T GetGamepadSetting<T>(ControlSetting controlSetting) { return (T)ControlMappings.GAMEPAD_CONTROL_SETTINGS[controlSetting]; }
    public static T GetCameraSetting<T>(CameraSetting cameraSetting) { return (T)ControlMappings.CAMERA_SETTINGS[cameraSetting]; }

    public static void SetKeyboardControl(CamControl key, int data) { ControlMappings.KEYBOARD_CONTROL_MAPPING[key] = data; }
    public static void SetGamepadControl(CamControl key, int data) {  ControlMappings.GAMEPAD_CONTROL_MAPPING[key] = data; }
    public static void SetKeyboardSetting<T>(ControlSetting controlSetting, T data) { ControlMappings.KEYBOARD_CONTROL_SETTINGS[controlSetting] = data; }
    public static void SetGamepadSetting<T>(ControlSetting controlSetting, T data) { ControlMappings.KEYBOARD_CONTROL_SETTINGS[controlSetting] = data; }
    public static void SetCameraSetting<T>(CameraSetting cameraSetting, T data ) { ControlMappings.CAMERA_SETTINGS[cameraSetting] = data; }

}
