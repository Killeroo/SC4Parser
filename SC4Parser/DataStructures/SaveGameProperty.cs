using System;
using System.Collections.Generic;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// SaveGameProperties (SIGPROPs) are small structures used to store individual entries of information for a given object.
    /// They are highly situational and their value and use depends on where they are used:
    /// Take, for example, SIGPROPs being used for storing information about buildings (https://wiki.sc4devotion.com/index.php?title=Building_Subfile#Appendix_1:_Structure_of_SGPROP_.28SaveGame_Properties.29)
    /// A SIGPROPs data can be several different types, they can also contain several values.
    /// (Writing this parsing was a pain)
    /// </summary>
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

        /// <summary>
        /// Loads an individual SaveGame Property from a byte array
        /// NOTE: This parse functions works pretty similarly to other parse functions, but because they can return in the middle
        /// of data entries, the calling function might need the current index after the data has been read so we return that.
        /// </summary>
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

        /// <summary>
        /// Dumps the contents of a SIGPROP
        /// </summary>
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

        /// <summary>
        /// Extracts a bunch of SaveGame Properties and then returns the new offset after everything has been read 
        /// </summary>
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
