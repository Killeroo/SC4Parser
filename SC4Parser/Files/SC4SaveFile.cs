using System;

namespace SC4Parser.Files
{
    /// <summary>
    /// SC4 save implementation, SC4 save files use the Maxis DBPF 1.1 file format
    /// This is a dud, inherited from DatabasePackedFile where the actual functionality resides
    /// Included for simplicity when referring to SC4saves
    /// </summary>
    public class SC4SaveFile : DatabasePackedFile
    {
        public SC4SaveFile(string path) : base(path) { }
    }
}
