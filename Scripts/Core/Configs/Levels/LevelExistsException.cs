using System;

namespace Ji2.Configs.Levels
{
    public class LevelExistsException : Exception
    {
        public LevelExistsException(string level) : base(message: $"Level with id {level} already exists")
        {
        }
    }
}