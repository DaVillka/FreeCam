using System;
using System.Collections.Generic;
using System.Linq;

namespace HardLife.Utils.FreeCamera
{
    public enum CamControl : int
    {
        LOOK_X,
        LOOK_Y,
        MOVE_X,
        MOVE_Y,
        MOVE_Z_UP,
        MOVE_Z_DOWN,
        MOVE_FAST,
        MOVE_SLOW
    }
    public enum ControlSetting
    {
        LOOK_SENSITIVITY_X,
        LOOK_SENSITIVITY_Y,
        BASE_MOVE_MULTIPLIER,
        FAST_MOVE_MULTIPLIER,
        SLOW_MOVE_MULTIPLIER
    }
    public enum CameraSetting
    {
        FOV,
        ENABLE_EASING,
        EASING_DURATION,
        KEEP_POSITION,
        KEEP_ROTATION,
    }
    public class ControlMapping
    {
        public int LOOK_X { get; set; }
        public int LOOK_Y { get; set; }
        public int MOVE_X { get; set; }
        public int MOVE_Y { get; set; }
        public int[] MOVE_Z { get; set; }
        public int MOVE_FAST { get; set; }
        public int MOVE_SLOW { get; set; }

        public int this[CamControl control]
        {
            get
            {
                return control switch
                {
                    CamControl.LOOK_X => LOOK_X,
                    CamControl.LOOK_Y => LOOK_Y,
                    CamControl.MOVE_X => MOVE_X,
                    CamControl.MOVE_Y => MOVE_Y,
                    CamControl.MOVE_Z_UP => MOVE_Z[0],
                    CamControl.MOVE_Z_DOWN => MOVE_Z[1],
                    CamControl.MOVE_FAST => MOVE_FAST,
                    CamControl.MOVE_SLOW => MOVE_SLOW,
                    _ => -1,
                };
            }
            set
            {
                switch (control)
                {
                    case CamControl.LOOK_X: LOOK_X = value; break;
                    case CamControl.LOOK_Y: LOOK_Y = value; break;
                    case CamControl.MOVE_X: MOVE_X = value; break;
                    case CamControl.MOVE_Y: MOVE_Y = value; break;
                    case CamControl.MOVE_Z_UP: MOVE_Z[0] = value; break;
                    case CamControl.MOVE_Z_DOWN: MOVE_Z[1] = value; break;
                    case CamControl.MOVE_FAST: MOVE_FAST = value; break;
                    case CamControl.MOVE_SLOW: MOVE_SLOW = value; break;
                }
            }
        }
    }

    public class ControlSettings
    {
        public float LOOK_SENSITIVITY_X { get; set; }
        public float LOOK_SENSITIVITY_Y { get; set; }
        public int BASE_MOVE_MULTIPLIER { get; set; }
        public int FAST_MOVE_MULTIPLIER { get; set; }
        public int SLOW_MOVE_MULTIPLIER { get; set; }

        public object this[ControlSetting control]
        {
            get
            {
                return control switch
                {
                    ControlSetting.BASE_MOVE_MULTIPLIER => BASE_MOVE_MULTIPLIER,
                    ControlSetting.FAST_MOVE_MULTIPLIER => FAST_MOVE_MULTIPLIER,
                    ControlSetting.SLOW_MOVE_MULTIPLIER => SLOW_MOVE_MULTIPLIER,
                    ControlSetting.LOOK_SENSITIVITY_X => LOOK_SENSITIVITY_X,
                    ControlSetting.LOOK_SENSITIVITY_Y => LOOK_SENSITIVITY_Y,
                    _ => -1,
                };
            }
            set
            {
                switch (control)
                {
                    case ControlSetting.BASE_MOVE_MULTIPLIER: BASE_MOVE_MULTIPLIER = (int)value; break;
                    case ControlSetting.FAST_MOVE_MULTIPLIER: FAST_MOVE_MULTIPLIER = (int)value; break;
                    case ControlSetting.SLOW_MOVE_MULTIPLIER: SLOW_MOVE_MULTIPLIER = FAST_MOVE_MULTIPLIER = (int)value; break;
                    case ControlSetting.LOOK_SENSITIVITY_X: LOOK_SENSITIVITY_X = (float)value; break;
                    case ControlSetting.LOOK_SENSITIVITY_Y: LOOK_SENSITIVITY_Y = (float)value; break;
                }
            }
        }
    }

    public class CameraSettings
    {
        public float FOV { get; set; }
        public bool ENABLE_EASING { get; set; }
        public int EASING_DURATION { get; set; }
        public bool KEEP_POSITION { get; set; }
        public bool KEEP_ROTATION { get; set; }

        public object this[CameraSetting cameraSetting]
        {
            get
            {
                return cameraSetting switch
                {
                    CameraSetting.FOV => FOV,
                    CameraSetting.ENABLE_EASING => ENABLE_EASING,
                    CameraSetting.EASING_DURATION => EASING_DURATION,
                    CameraSetting.KEEP_POSITION => KEEP_POSITION,
                    CameraSetting.KEEP_ROTATION => KEEP_ROTATION,
                    _ => -1,
                };
            }
            set
            {
                switch (cameraSetting)
                {
                    case CameraSetting.FOV: FOV = (float)value; break;
                    case CameraSetting.ENABLE_EASING: ENABLE_EASING = (bool)value; break;
                    case CameraSetting.EASING_DURATION: EASING_DURATION = (int)value; break;
                    case CameraSetting.KEEP_POSITION: KEEP_POSITION = (bool)value; break;
                    case CameraSetting.KEEP_ROTATION: KEEP_ROTATION = (bool)value; break;
                    default: return;
                }
            }
        }
    }

    public static class ControlMappingConstants
    {
        public const int INPUT_LOOK_LR = 1;
        public const int INPUT_LOOK_UD = 2;
        public const int INPUT_CHARACTER_WHEEL = 19;
        public const int INPUT_SPRINT = 21;
        public const int INPUT_MOVE_UD = 31;
        public const int INPUT_MOVE_LR = 30;
        public const int INPUT_VEH_ACCELERATE = 71;
        public const int INPUT_VEH_BRAKE = 72;
        public const int INPUT_PARACHUTE_BRAKE_LEFT = 152;
        public const int INPUT_PARACHUTE_BRAKE_RIGHT = 153;
    }

    public static class ControlMappings
    {
        public static ControlMapping KEYBOARD_CONTROL_MAPPING = new ControlMapping
        {
            LOOK_X = ControlMappingConstants.INPUT_LOOK_LR,
            LOOK_Y = ControlMappingConstants.INPUT_LOOK_UD,
            MOVE_X = ControlMappingConstants.INPUT_MOVE_LR,
            MOVE_Y = ControlMappingConstants.INPUT_MOVE_UD,
            MOVE_Z = new int[2] { ControlMappingConstants.INPUT_PARACHUTE_BRAKE_LEFT, ControlMappingConstants.INPUT_PARACHUTE_BRAKE_RIGHT },
            MOVE_FAST = ControlMappingConstants.INPUT_SPRINT,
            MOVE_SLOW = ControlMappingConstants.INPUT_CHARACTER_WHEEL
        };
        public static ControlMapping GAMEPAD_CONTROL_MAPPING = new ControlMapping
        {
            LOOK_X = ControlMappingConstants.INPUT_LOOK_LR,
            LOOK_Y = ControlMappingConstants.INPUT_LOOK_UD,
            MOVE_X = ControlMappingConstants.INPUT_MOVE_LR,
            MOVE_Y = ControlMappingConstants.INPUT_MOVE_UD,
            MOVE_Z = new int[2] { ControlMappingConstants.INPUT_PARACHUTE_BRAKE_LEFT, ControlMappingConstants.INPUT_PARACHUTE_BRAKE_RIGHT },
            MOVE_FAST = ControlMappingConstants.INPUT_VEH_ACCELERATE,
            MOVE_SLOW = ControlMappingConstants.INPUT_VEH_BRAKE,
        };

        public static ControlSettings KEYBOARD_CONTROL_SETTINGS = new ControlSettings
        {
            LOOK_SENSITIVITY_X = 5.0f,
            LOOK_SENSITIVITY_Y = 5.0f,
            BASE_MOVE_MULTIPLIER = 1,
            FAST_MOVE_MULTIPLIER = 10,
            SLOW_MOVE_MULTIPLIER = 10
        };
        public static ControlSettings GAMEPAD_CONTROL_SETTINGS = new ControlSettings
        {
            LOOK_SENSITIVITY_X = 2.0f,
            LOOK_SENSITIVITY_Y = 2.0f,
            BASE_MOVE_MULTIPLIER = 1,
            FAST_MOVE_MULTIPLIER = 10,
            SLOW_MOVE_MULTIPLIER = 10
        };
        public static CameraSettings CAMERA_SETTINGS = new CameraSettings
        {
            FOV = 45.0f,
            ENABLE_EASING = true,
            EASING_DURATION = 1000,
            KEEP_POSITION = false,
            KEEP_ROTATION = false
        };

        public static ControlMapping CONTROL_MAPPING = Utils.CreateGamepadMetatable(KEYBOARD_CONTROL_MAPPING, GAMEPAD_CONTROL_MAPPING);
        public static ControlSettings CONTROL_SETTINGS = Utils.CreateGamepadMetatable(KEYBOARD_CONTROL_SETTINGS, GAMEPAD_CONTROL_SETTINGS);
        //_G.CONTROL_MAPPING  = CreateGamepadMetatable(_G.KEYBOARD_CONTROL_MAPPING, _G.GAMEPAD_CONTROL_MAPPING)
        //_G.CONTROL_SETTINGS = CreateGamepadMetatable(_G.KEYBOARD_CONTROL_SETTINGS, _G.GAMEPAD_CONTROL_SETTINGS)

    }
}
