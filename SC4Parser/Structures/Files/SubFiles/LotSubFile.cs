using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Structures.Files.SubFiles
{
    class LotSubFile
    {
        List<Lot> Lots = new List<Lot>();

        public void Parse(byte[] buffer, int size)
        {
            uint bytesToRead = Convert.ToUInt32(size);
            uint offset = 0;

            while (bytesToRead > 0)
            {
                uint currentSize = BitConverter.ToUInt32(buffer, (int)offset);

                Lot lot = new Lot();
                byte[] b = new byte[currentSize];
                Array.Copy(buffer, offset, b, 0, (int)currentSize);
                lot.Parse(b, offset);
                Lots.Add(lot);

                lot.Dump();

                offset += currentSize;
                bytesToRead -= currentSize;

                Logger.Log($"lot read ({currentSize} bytes), offset {offset} got {bytesToRead}/{size} bytes left");
            }

            if (bytesToRead != 0)
            {
                Logger.Warning("Not all lots have been read from lot subfile (" + bytesToRead + " bytes left)");
            }
        }
    }
}
