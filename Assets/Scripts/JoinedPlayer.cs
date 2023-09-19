using System;

namespace Example
{
    [Serializable]
    public class JoinedPlayer
    {
        public uint UserID;
        public CharacterVisual CharacterVisual;
        public InputProvider InputProvider;
        public string Name;
    }
}