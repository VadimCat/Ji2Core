using System.Collections.Generic;

namespace Ji2Core.Core.Audio
{
    public interface ISoundNamesCollection
    {
        public IEnumerable<string> SoundsList { get; }
    }
}