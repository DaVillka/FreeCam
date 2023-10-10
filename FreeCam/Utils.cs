using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace HardLife.Utils.FreeCamera
{
    public static class Utils
    {
        public static T CreateGamepadMetatable<T>(T keyboard, T gamepad)
        {
            return IsGamepadControl() ? gamepad : keyboard;
        }

        public static float Clamp(float x, float min, float max)
        {
            return Math.Min(Math.Max(x, min), max);
        }

        public static (float, float, float) ClampCameraRotation(float rotX, float rotY, float rotZ)
        {
            float x = Clamp(rotX, -90.0f, 90.0f);
            float y = rotY % 360;
            float z = rotZ % 360;
            return (x, y, z);
        }
        public static bool IsGamepadControl()
        {
            return !RAGE.Game.Pad.IsInputDisabled(2);
        }

        public static float GetSmartControlNormal(object control)
        {
            if (control is int[] controlArray)
            {
                float normal1 = GetDisabledControlNormal(0, controlArray[0]);
                float normal2 = GetDisabledControlNormal(0, controlArray[1]);
                return normal1 - normal2;
            }
            return GetDisabledControlNormal(0, Convert.ToInt32(control));
        }

        public static (Vector3, Vector3, Vector3) EulerToMatrix(float rotX, float rotY, float rotZ)
        {
            float radX = (float)Math.PI * rotX / 180.0f;
            float radY = (float)Math.PI * rotY / 180.0f;
            float radZ = (float)Math.PI * rotZ / 180.0f;

            float sinX = (float)Math.Sin(radX);
            float sinY = (float)Math.Sin(radY);
            float sinZ = (float)Math.Sin(radZ);
            float cosX = (float)Math.Cos(radX);
            float cosY = (float)Math.Cos(radY);
            float cosZ = (float)Math.Cos(radZ);

            float vecX_X = cosY * cosZ;
            float vecX_Y = cosY * sinZ;
            float vecX_Z = -sinY;

            float vecY_X = cosZ * sinX * sinY - cosX * sinZ;
            float vecY_Y = cosX * cosZ - sinX * sinY * sinZ;
            float vecY_Z = cosY * sinX;

            float vecZ_X = -cosX * cosZ * sinY + sinX * sinZ;
            float vecZ_Y = -cosZ * sinX + cosX * sinY * sinZ;
            float vecZ_Z = cosX * cosY;

            return (new Vector3(vecX_X, vecX_Y, vecX_Z), new Vector3(vecY_X, vecY_Y, vecY_Z), new Vector3(vecZ_X, vecZ_Y, vecZ_Z));
        }

        private static float GetDisabledControlNormal(int index, int control)
        {
            return RAGE.Game.Pad.GetDisabledControlNormal(index, control);
        }
    }
}
