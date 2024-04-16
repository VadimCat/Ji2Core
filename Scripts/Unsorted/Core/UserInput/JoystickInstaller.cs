using Ji2.CommonCore;
using Ji2.Context;
using UnityEngine;

namespace Ji2.UserInput
{
    public class JoystickInstaller : MonoInstaller<Joystick>
    {
        [SerializeField] private Joystick joystick;
        
        protected override Joystick Create(DiContext diContext)
        {
            joystick.SetDependencies(diContext.Get<CameraProvider>(), diContext.Get<UpdateService>());
            return joystick;
        }
    }
}