using System.ComponentModel;

namespace Ji2Core
{
    public class SROptions
    {
        [Category("Integration")]
        public void ShowMediationDebugger()
        {
            MaxSdk.ShowMediationDebugger();
        } 
    }
}
