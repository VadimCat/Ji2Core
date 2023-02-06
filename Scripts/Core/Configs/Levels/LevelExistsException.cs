using System;

namespace Client
{
    public class LevelExistsException : Exception
    {
        public LevelExistsException(string level) : base(message: $"Level with id {level} already exists")
        {
        }
    }
}