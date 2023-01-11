using Ji2Core.Core.Installers;
using UnityEngine;

namespace Ji2Core.Core.UserInput
{
    public class JoystickInstaller : MonoInstaller<Joystick>
    {
        [SerializeField] private Joystick joystick;
        
        protected override Joystick Create(Context context)
        {
            joystick.SetDependencies(context.GetService<CameraProvider>(), context.GetService<UpdateService>());
            return joystick;
        }
    }
}