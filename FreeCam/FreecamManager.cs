using RAGE.Game;
using RAGE;
using static RAGE.Events;
using HardLife.Core.Events;
namespace HardLife.Utils.FreeCamera
{

    internal class FreecamManager : Script
    {
        #region Singleton
        private static FreecamManager _instance = null;
        public static FreecamManager Instances { get => _instance; }
        #endregion
        private readonly ControlSettings SETTINGS = ControlMappings.CONTROL_SETTINGS;
        private readonly ControlMapping CONTROLS = ControlMappings.CONTROL_MAPPING;

        protected FreecamManager() 
        {
            FreeCam.SetCameraSetting(CameraSetting.FOV, Cam.GetGameplayCamFov());
            Input.Bind(RAGE.Ui.VirtualKeys.X, false, () =>
            {
                FreeCam.Active = !FreeCam.Active;
            });
            Tick += (e) => UpdateCamera();
            _instance = this; 
        }
        private float GetSpeedMultiplier()
        {
            float fastNormal = Utils.GetSmartControlNormal(CONTROLS.MOVE_FAST);
            float slowNormal = Utils.GetSmartControlNormal(CONTROLS.MOVE_SLOW);

            float baseSpeed = SETTINGS.BASE_MOVE_MULTIPLIER;
            float fastSpeed = 1 + (SETTINGS.FAST_MOVE_MULTIPLIER - 1) * fastNormal;
            float slowSpeed = 1 + (SETTINGS.SLOW_MOVE_MULTIPLIER - 1) * slowNormal;

            float frameMultiplier = Misc.GetFrameTime() * 60;
            float speedMultiplier = baseSpeed * fastSpeed / slowSpeed;

            return speedMultiplier * frameMultiplier;
        }

        private void UpdateCamera()
        {
            if (!Camera.IsFreecamActive() || Ui.IsPauseMenuActive()) return;

            if (!Camera.IsFreecamFrozen())
            {
                (Vector3 vecX, Vector3 vecY, Vector3 vecZ, Vector3 pos) = Camera.GetFreecamMatrix();
                Vector3 _vecZ = new Vector3(0, 0, 1);

                Vector3 _pos = Camera.GetFreecamPosition();
                Vector3 _rot = Camera.GetFreecamRotation();

                float speedMultiplier = GetSpeedMultiplier();

                float lookX = Utils.GetSmartControlNormal(CONTROLS.LOOK_X);
                float lookY = Utils.GetSmartControlNormal(CONTROLS.LOOK_Y);

                float moveX = Utils.GetSmartControlNormal(CONTROLS.MOVE_X);
                float moveY = Utils.GetSmartControlNormal(CONTROLS.MOVE_Y);
                float moveZ = Utils.GetSmartControlNormal(CONTROLS.MOVE_Z);

                float rotX = _rot.X + -lookY * SETTINGS.LOOK_SENSITIVITY_X;
                float rotZ = _rot.Z + -lookX * SETTINGS.LOOK_SENSITIVITY_Y;
                float rotY = _rot.Y;

                _pos += vecX * moveX * speedMultiplier;
                _pos += vecY * -moveY * speedMultiplier;
                _pos += _vecZ * moveZ * speedMultiplier;


                Camera.SetFreecamPosition(_pos.X, _pos.Y, _pos.Z);
                Camera.SetFreecamRotation(rotX, rotY, rotZ);
            }

            //TriggerEvent("freecam:onTick");
        }
    }
}
