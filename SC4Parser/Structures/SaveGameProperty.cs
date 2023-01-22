using System;
using System.Collections.Generic;

using SC4Parser.Logging;

namespace SC4Parser.DataStructures
{
    /// <summary>
    /// Represents a Savegame Property (SIGPROP). SIGPROPs are small structures used to store individual entries of information for a given object.
    /// </summary>
    /// <remarks>
    /// SIGPROPs are highly situational and their value and use depends on where they are used:
    /// Take, for example, SIGPROPs being used for storing specific information about buildings (patient capacity for a hospital, dispatches available for a firestation, 
    /// custom name given to a building). A SIGPROPs data can be several different types, they can also contain several values.
    /// (Writing this parsing was a pain)
    /// 
    /// Implemented using the following: https://wiki.sc4devotion.com/index.php?title=Building_Subfile#Appendix_1:_Structure_of_SGPROP_.28SaveGame_Properties.29
    /// </remarks>
    /// <see cref="SC4Parser.Constants.SIGPROP_DATATYPE_TYPES"/>
    /// <see cref="SC4Parser.Constants.SIGPROP_DATATYPE_TYPE_STRINGS"/>
    public class SaveGameProperty
    {
        /// <summary>
        /// SaveGame Property (SIGPROP) value, used to identify the use of the SIGPROP
        /// </summary>
        /// <example>
        /// A SIGPROP with a Property Name Value of 0x899AFBAD is used to store a buildings custom name
        /// </example>
        public uint PropertyNameValue { get; private set; }
        /// <summary>
        /// SaveGame Property (SIGPROP) value copy, duplicated for unknown reason.
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.SaveGameProperty.PropertyNameValue"/>
        public uint PropertyNameValueCopy { get; private set; }
        /// <summary>
        /// Unknown SaveGame Property value
        /// </summary>
        public uint Unknown1 { get; private set; }
        /// <summary>
        /// Data type stored in the SaveGame Property (SIGPROP)
        /// </summary>
        /// <remarks>
        /// 01=UInt8, 02=UInt16, 03=UInt32, 07=SInt32, 08=SInt64, 09=Float32, 0B=Boolean, 0C=String
        /// </remarks>
        /// <see cref="SC4Parser.Constants.SIGPROP_DATATYPE_TYPES"/>
        /// <see cref="SC4Parser.Constants.SIGPROP_DATATYPE_TYPE_STRINGS"/>
        public byte DataType { get; private set; }
        /// <summary>
        /// Determines if there is repeated/multiple data in the SaveGame Property (SIGPROP)
        /// </summary>
        public byte KeyType { get; private set; }
        /// <summary>
        /// Unknown SaveGame Property value
        /// </summary>
        public ushort Unknown2 { get; private set; }
        /// <summary>
        /// Amount of data that is stored in the SaveGame Property (SIGPROP) 
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.SaveGameProperty.Data"/>
        public uint DataRepeatedCount { get; private set; }
        /// <summary>
        /// Data that is stored in the SaveGame Property (SIGPROP)
        /// </summary>
        /// <see cref="SC4Parser.DataStructures.SaveGameProperty.DataRepeatedCount"/>
        /// <see cref="SC4Parser.DataStructures.SaveGameProperty.DataType"/>
        public List<object> Data { get; private set; } = new List<object>();

        /// <summary>
        /// Loads an individual SaveGame Property (SIGPROP) from a byte array
        /// </summary>
        /// <param name="buffer">Data to read the SIGPROP from</param>
        /// <param name="offset">Position in the data array to start reading the SIGPROP from</param>
        /// <returns>The new offset/position after the SIGPROP has been read</returns>
        /// <remarks>
        /// This parse functions works pretty similarly to other parse functions, but because they can return in the middle
        /// of data entries, the calling function might need the current index after the data has been read so we return that.
        /// 
        /// The data buffer provided may contain multiple SIGPROPs but the method is only designed to read one 
        /// </remarks>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
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
        /// Prints the values of SaveGame Property (SIGPROP)
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("Property Name Value: {0} [{1}]", PropertyNameValue, PropertyNameValue.ToString("x8"));
            Console.WriteLine("Property Name Value (copy): {0} [{1}]", PropertyNameValueCopy, PropertyNameValueCopy.ToString("x8"));
            Console.WriteLine("Unknown1: {0} [{1}]", Unknown1, Unknown1.ToString("x8"));
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
        /// Extracts a bunch of SaveGame Properties (SIGPROP) and then returns the new offset after everything has been read 
        /// </summary>
        /// <param name="buffer">Data to read SIGPROPs from</param>
        /// <param name="count">Number of SIGPROPs to try and read</param>
        /// <param name="offset">Offset/position to start reading the SIGPROPs from in the data array</param>
        /// <returns>A list of all parsed SIGPROPs</returns>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when trying to parse an element that is out of bounds in the data array
        /// </exception>
        /// <see cref="SC4Parser.DataStructures.SaveGameProperty"/>
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

                Logger.Log(LogLevel.Debug, "Read SaveGame Property @ offset {0}, {1} bytes left to read",
                    currentOffset,
                    ((buffer.Length - offset) + count) - currentOffset
                );
            }

            // Update the offset now that we have read all the SGPROPs
            // so the caller knows where to continue reading data from
            offset = (uint) currentOffset;

            return results;
        }

    }
}
