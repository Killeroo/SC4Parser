using System;

namespace SC4Parser
{
    /// <summary>
    /// Implements TypeGroupInstance (TGI) identifier used to identify elements and files in a SimCity 4 savegame (DBPF)
    /// </summary>
    /// <remarks>
    /// TypeGroupInstance (TGI) is used as an identifier for files within a SimCity 4 savegame (DBPF)
    /// It consists of the items TypeID, GroupID and InstanceID. The combination of these fields creates
    /// a unique reference for the file. 
    /// It is used in comparisons a lot and the values for the fields are quite often referenced as hex 
    /// so it is represented here with all the neccessary methods for usage, conversion and comparison
    /// mainly for convience.
    /// More info here: https://www.wiki.sc4devotion.com/index.php?title=Type_Group_Instance
    /// </remarks>
    public struct TypeGroupInstance : IEquatable<TypeGroupInstance>
    {
        /// <summary>
        /// Type ID
        /// </summary>
        public uint Type { get; set; }
        /// <summary>
        /// Group ID
        /// </summary>
        public uint Group { get; set; }
        /// <summary>
        /// Instance ID
        /// </summary>
        public uint Instance { get; set; }

        /// <summary>
        /// TypeGroupInstance (TGI) constructor using uint values of IDs
        /// </summary>
        /// <param name="type">Items Type ID</param>
        /// <param name="group">Items Group ID</param>
        /// <param name="instance">Items Instance ID</param>
        /// <example>
        /// <c>
        /// // Create Terrain Map Subfile's TGI
        /// TypeGroupInstance terrainMapTGI = new TypeGroupInstance(2849861620, 3918501157, 1);
        /// 
        /// // Use tgi to load subfile from save
        /// sc4Save.LoadIndexEntry(terrainMapTGI);
        /// </c>
        /// </example>
        /// <seealso cref="SC4Parser.TypeGroupInstance.TypeGroupInstance(string, string, string)"/>
        public TypeGroupInstance(uint type, uint group, uint instance)
            : this()
        {
            Type = type;
            Group = group;
            Instance = instance;
        }
        /// <summary>
        /// TypeGroupInstance (TGI) constructor using string hex values of IDs
        /// </summary>
        /// <param name="type_hex">Items Type ID</param>
        /// <param name="group_hex">Items Group ID</param>
        /// <param name="instance_hex">Items Instance ID</param>
        /// <example>
        /// <c>
        /// // Create Terrain Map Subfile's TGI
        /// TypeGroupInstance terrainMapTGI = new TypeGroupInstance("A9DD6FF4", "E98f9525", "00000001");
        /// 
        /// // Use tgi to load subfile from save
        /// sc4Save.LoadIndexEntry(terrainMapTGI);
        /// </c>
        /// </example>
        /// <remarks>
        /// Don't include the 0x at the start of any hex
        /// </remarks>
        /// <seealso cref="SC4Parser.TypeGroupInstance.TypeGroupInstance(uint, uint, uint)"/>
        public TypeGroupInstance(string type_hex, string group_hex, string instance_hex)
            : this()
        {
            Type = uint.Parse(type_hex, System.Globalization.NumberStyles.HexNumber);
            Group = uint.Parse(group_hex, System.Globalization.NumberStyles.HexNumber);
            Instance = uint.Parse(instance_hex, System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// General equals comparitor for TypeGroupInstance (TGI) 
        /// </summary>
        /// <param name="obj">Object to compare against</param>
        /// <returns>returns <c>true</c> if objects are equal, <c>false</c> if they are not</returns>
        public override bool Equals(object obj)
        {
            if (obj is TypeGroupInstance)
            {
                return this.Equals((TypeGroupInstance)obj);
            }
            return false;
        }
        /// <summary>
        /// Specific equals compaitor for TypeGroupInstance (TGI)
        /// </summary>
        /// <param name="tgi">TGI to compare against</param>
        /// <returns>returns <c>true</c> if objects are equal, <c>false</c> if they are not</returns>
        public bool Equals(TypeGroupInstance tgi)
        {
            return (Type == tgi.Type) && (Group == tgi.Group) && (Instance == tgi.Instance);
        }

        /// <summary>
        /// Get the hash for the TypeGroupInstance (TGI)
        /// </summary>
        /// <returns>The hash value of the TGI</returns>
        public override int GetHashCode()
        {
            return (int)(Type + Group + Instance);
        }

        /// <summary>
        /// Equals comparitor, checks if the 2 provided objects are equal
        /// </summary>
        /// <param name="lhs">Left hand side of comparison</param>
        /// <param name="rhs">Right habd sude if comparison</param>
        /// <returns>returns <c>true</c> if objects are equal, <c>false</c> if they are not</returns>
        public static bool operator ==(TypeGroupInstance lhs, TypeGroupInstance rhs)
        {
            return lhs.Equals(rhs);
        }
        /// <summary>
        /// Not equals comparitor, checks if the 2 provided objects are not equal
        /// </summary>
        /// <param name="lhs">Left hand side of comparison</param>
        /// <param name="rhs">Right habd sude if comparison</param>
        /// <returns>returns <c>true</c> if objects are not equal, <c>false</c> if they are not</returns>
        public static bool operator !=(TypeGroupInstance lhs, TypeGroupInstance rhs)
        {
            return !(lhs.Equals(rhs));
        }

        /// <summary>
        /// Creates a TypeGroupInstance (TGI) from a Type ID string
        /// </summary>
        /// <param name="type_hex">The item'ss Type ID</param>
        /// <returns>The created TGI</returns>
        public static TypeGroupInstance FromHex(string type_hex)
        {
            return new TypeGroupInstance(
                type_hex,
                "0",
                "0"
            );
        }
        /// <summary>
        /// Creates a TypeGroupInstance (TGI) from a Type ID and Group ID strings
        /// </summary>
        /// <param name="type_hex">The item's Type ID</param>
        /// <param name="group_hex">The item's Group ID</param>
        /// <returns>The created TGI</returns>
        public static TypeGroupInstance FromHex(string type_hex, string group_hex)
        {
            return new TypeGroupInstance(
                type_hex,
                group_hex,
                "0"
            );
        }
        /// <summary>
        /// Creates a TypeGroupInstance (TGI) from a Type ID, Group ID and Instance ID strings
        /// </summary>
        /// <param name="type_hex">The item's Type ID</param>
        /// <param name="group_hex">The item's Group ID</param>
        /// <param name="instance_hex">The item's Instance ID</param>
        /// <returns>The created TGI</returns>
        public static TypeGroupInstance FromHex(string type_hex, string group_hex, string instance_hex)
        {
            return new TypeGroupInstance(
                type_hex,
                group_hex,
                instance_hex
            );
        }

        /// <summary>
        /// Converts the TypeGroupInstance (TGI) to a string
        /// </summary>
        /// <returns>String representation of the TGI</returns>
        public new string ToString()
        {
            return string.Format("0x{0} 0x{1} 0x{2}",
                Type.ToString("X8"),
                Group.ToString("X8"),
                Instance.ToString("X8"));
        }

        /// <summary>
        /// Prints out the contents of the TypeGroupInstance (TGI)
        /// </summary>
        public void Dump()
        {
            Console.WriteLine("{0} {1} {2}",
                Type.ToString("X8"),
                Group.ToString("X8"),
                Instance.ToString("X8"));
        }
    }
}
