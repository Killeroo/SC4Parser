using System;
using System.Collections.Generic;

namespace SC4Parser.DataStructures
{
    public class SaveGameProperty
    {
        public uint PropertyNameValue;
        public uint PropertyNameValueCopy;
        public uint Unknown1;
        public byte DataType;
        public byte KeyType;
        public ushort Unknown2;
        public uint DataRepeatedCount;
        public List<object> Data = new List<object>();

        // This parse functions works pretty similarly to other parse functions, but because they can return in the middle
        // of data entries, the calling function might need the current index after the data has been read so we return that.
        public int Parse(byte[] buffer, int offset = 0)
        {
            PropertyNameValue = BitConverter.ToUInt32(buffer, offset + 0);
            PropertyNameValueCopy = BitConverter.ToUInt32(buffer, offset + 4);
            Unknown1 = BitConverter.ToUInt32(buffer, offset + 8);
            DataType = buffer[offset + 12];
            KeyType = buffer[offset + 13];
            Unknown2 = BitConverter.ToUInt16(buffer, offset + 14);

            int currentOffset = offset + 16;

            if (KeyType == 0x80) // Reading multiple values
            {
                DataRepeatedCount = BitConverter.ToUInt32(buffer, currentOffset);
                currentOffset += 4;

                for (int i = 0; i < DataRepeatedCount; i++)
                {
                    switch (DataType)
                    {
                        case 0x01:
                            Data.Add(buffer[currentOffset]);
                            currentOffset += 1;
                            break;
                        case 0x02:
                            Data.Add(BitConverter.ToUInt16(buffer, currentOffset));
                            currentOffset += 2;
                            break;
                        case 0x03:
                            Data.Add(BitConverter.ToUInt32(buffer, currentOffset));
                            currentOffset += 4;
                            break;
                        case 0x07:
                            Data.Add(BitConverter.ToInt32(buffer, currentOffset));
                            currentOffset += 4;
                            break;
                        case 0x08:
                            Data.Add(BitConverter.ToInt64(buffer, currentOffset));
                            currentOffset += 8;
                            break;
                        case 0x09:
                            Data.Add(BitConverter.ToSingle(buffer, currentOffset));
                            currentOffset += 4;
                            break;
                        case 0x0B:
                            Data.Add(BitConverter.ToBoolean(buffer, currentOffset));
                            currentOffset += 1;
                            break;
                        case 0x0C:
                            Data.Add(Convert.ToChar(buffer[currentOffset]));
                            currentOffset += 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            else // Just reading one value
            {
                switch (DataType)
                {
                    case 0x01:
                        Data.Add(buffer[currentOffset]);
                        currentOffset += 1;
                        break;
                    case 0x02:
                        Data.Add(BitConverter.ToUInt16(buffer, currentOffset));
                        currentOffset += 2;
                        break;
                    case 0x03:
                        Data.Add(BitConverter.ToUInt32(buffer, currentOffset));
                        currentOffset += 4;
                        break;
                    case 0x07:
                        Data.Add(BitConverter.ToInt32(buffer, currentOffset));
                        currentOffset += 4;
                        break;
                    case 0x08:
                        Data.Add(BitConverter.ToInt64(buffer, currentOffset));
                        currentOffset += 8;
                        break;
                    case 0x09:
                        Data.Add(BitConverter.ToSingle(buffer, currentOffset));
                        currentOffset += 4;
                        break;
                    case 0x0B:
                        Data.Add(BitConverter.ToBoolean(buffer, currentOffset));
                        currentOffset += 1;
                        break;
                    case 0x0C:
                        Data.Add(Convert.ToChar(buffer[currentOffset]));
                        currentOffset += 1;
                        break;
                    default:
                        break;
                }
            }

            return currentOffset;
        }

        public void Dump()
        {
            Console.WriteLine("Property Name Value: {0} [{1}]", PropertyNameValue, PropertyNameValue.ToString("X"));
            Console.WriteLine("Property Name Value (copy): {0} [{1}]", PropertyNameValueCopy, PropertyNameValueCopy.ToString("X"));
            Console.WriteLine("Unknown1: {0} [{1}]", Unknown1, Unknown1.ToString("X"));
            Console.WriteLine("Data Type: {0} [{1}]", DataType, Constants.SIGPROP_DATATYPE_TYPE_STRINGS[DataType]);
            Console.WriteLine("Key Type: {0}", KeyType);
            Console.WriteLine("Unknown: {0}", Unknown2);
            Console.WriteLine("Data Repeats: {0}", DataRepeatedCount);

            Console.WriteLine("Values:");
            for (int i = 0; i < Data.Count; i++)
            {
                Console.WriteLine(" - {0}", Data[i]);
            }
        }

        // Extracts a bunch of SaveGame Properties and then returns the new offset after everything has been read
        public static List<SaveGameProperty> ExtractFromBuffer(byte[] buffer, uint count, ref uint offset)
        {
            List<SaveGameProperty> results = new List<SaveGameProperty>();
            int currentOffset = (int) offset;

            for (int i = 0; i < count; i++)
            {
                SaveGameProperty property = new SaveGameProperty();

                // Parse returns the new offset so we keep track of that 
                currentOffset = property.Parse(buffer, currentOffset);
                results.Add(property);
            }

            // Update the offset now that we have read all the SGPROPs
            // so the caller knows where to continue reading data from
            offset = (uint) currentOffset;

            return results;
        }
    }
}
