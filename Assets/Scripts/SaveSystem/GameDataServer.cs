using System;
using System.Collections.Generic;

namespace SaveSystem
{
    [Serializable]
    public class GameDataServer
    {
        public Dictionary<AnyBundle, List<FilePaths>> Paths;
    }
}