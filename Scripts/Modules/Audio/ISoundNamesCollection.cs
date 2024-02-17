using System.Collections.Generic;

namespace Ji2.Audio
{
    public interface ISoundNamesCollection
    {
        public IEnumerable<string> SoundsList { get; }
    }
}