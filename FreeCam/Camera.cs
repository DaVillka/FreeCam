using RAGE.NUI;
using Vector3 = RAGE.Vector3;
namespace HardLife.Utils.FreeCamera
{
    internal static class Camera
    {
        private static RAGE.Elements.Camera _internalCamera = null;
        private static bool _internalIsFrozen = false;
        private static Vector3 _internalPos = null;
        private static Vector3 _internalRot = null;
        private static float _internalFov = 0.0f;
        private static Vector3 _internalVecX = null;
        private static Vector3 _internalVecY = null;
        private static Vector3 _internalVecZ = null;

        private static CameraSettings CAMERA_SETTINGS = ControlMappings.CAMERA_SETTINGS;
        public static Vector3 GetInitialCameraPosition()
        {
            if (CAMERA_SETTINGS.KEEP_POSITION && _internalPos != null)
            {
                return _internalPos;
            }

            return RAGE.Game.Cam.GetGameplayCamCoord();
        }

        public static Vector3 GetInitialCameraRotation()
        {
            if (CAMERA_SETTINGS.KEEP_ROTATION && _internalRot != null)
            {
                return _internalRot;
            }

            var rot = RAGE.Game.Cam.GetGameplayCamRot(0);
            return new Vector3(rot.X, 0.0f, rot.Z);
        }

        public static bool IsFreecamFrozen()
        {
            return _internalIsFrozen;
        }

        public static void SetFreecamFrozen(bool frozen)
        {
            _internalIsFrozen = frozen;
        }

        public static Vector3 GetFreecamPosition()
        {
            return _internalPos;
        }

        public static void SetFreecamPosition(float x, float y, float z)
        {
            var pos = new Vector3(x, y, z);
            var interior = RAGE.Game.Interior.GetInteriorAtCoords(x, y, z);
            RAGE.Game.Interior.LoadInterior(interior);
            RAGE.Game.Streaming.SetFocusArea(x, y, z, 0, 0, 0);
            RAGE.Game.Ui.LockMinimapPosition(x, y);
            RAGE.Game.Cam.SetCamCoord(_internalCamera.Id, x, y, z);
            _internalPos = pos;
        }
        public static Vector3 GetFreecamRotation()
        {
            return _internalRot;
        }

        public static void SetFreecamRotation(float x, float y, float z)
        {
            (float X, float Y, float Z) = Utils.ClampCameraRotation(x, y, z);
            //vecX_X, vecX_Y, vecX_Z, vecY_X, vecY_Y, vecY_Z, vecZ_X, vecZ_Y, vecZ_Z
            (Vector3 vec1, Vector3 vec2, Vector3 vec3) = Utils.EulerToMatrix(X, Y, Z);

            RAGE.Game.Ui.LockMinimapAngle((int)Z);
            RAGE.Game.Cam.SetCamRot(_internalCamera.Id, X, Y, Z, 0);

            _internalRot = new Vector3(X, Y, Z);
            _internalVecX = vec1;
            _internalVecY = vec2;
            _internalVecZ = vec3;
        }

        public static float GetFreecamFov()
        {
            return _internalFov;
        }

        public static void SetFreecamFov(float fov)
        {
            var clampedFov = Utils.Clamp(fov, 0.0f, 90.0f);
            RAGE.Game.Cam.SetCamFov(_internalCamera.Id, clampedFov);
            _internalFov = clampedFov;
        }

        public static (Vector3, Vector3, Vector3, Vector3) GetFreecamMatrix()
        {
            return (_internalVecX, _internalVecY, _internalVecZ, _internalPos);
        }

        public static Vector3 GetFreecamTarget(float distance)
        {
            var target = _internalPos + _internalVecY * distance;
            return target;
        }

        public static bool IsFreecamActive()
        {
            if (_internalCamera != null) return RAGE.Game.Cam.IsCamActive(_internalCamera.Id);
            return false;
        }

        public static void SetFreecamActive(bool active)
        {
            if (active == IsFreecamActive()) return;


            var enableEasing = CAMERA_SETTINGS.ENABLE_EASING;
            var easingDuration = CAMERA_SETTINGS.EASING_DURATION;

            if (active)
            {
                var pos = GetInitialCameraPosition();
                var rot = GetInitialCameraRotation();

                _internalCamera = new RAGE.Elements.Camera((ushort)RAGE.Game.Cam.CreateCamera(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), true), 0);
                SetFreecamFov(CAMERA_SETTINGS.FOV);
                SetFreecamPosition(pos.X, pos.Y, pos.Z);
                SetFreecamRotation(rot.X, rot.Y, rot.Z);
                RAGE.Game.Cam.SetCamActive(_internalCamera.Id, true);
                RAGE.Events.CallRemote("freecam:onEnter");
            }
            else
            {
                RAGE.Game.Cam.SetCamActive(_internalCamera.Id, false);
                _internalCamera.Destroy();
                RAGE.Game.Streaming.ClearFocus();
                RAGE.Game.Ui.UnlockMinimapPosition();
                RAGE.Game.Ui.UnlockMinimapAngle();
                RAGE.Events.CallRemote("freecam:onExit");
            }
            RAGE.Game.Player.SetPlayerControl(!active, 0);
            RAGE.Game.Cam.RenderScriptCams(active, enableEasing, easingDuration, true, false, 0);
        }
    }
}
