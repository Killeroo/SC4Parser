using System;

namespace TestParser.Structures
{
    /// <summary>
    /// TypeGroupInstance (TGI) is used as an identifier for files within a sc4 save file (DBPF file)
    /// It consists of the items TypeID, GroupID and InstanceID. The combination of these fields creates
    /// a unique reference for the file. 
    /// It is used in comparisons a lot and the values for the fields are quite often referenced as hex 
    /// so it is represented here with all the neccessary methods for usage, conversion and comparison
    /// mainly for convience.
    /// More info here: https://www.wiki.sc4devotion.com/index.php?title=Type_Group_Instance
    /// </summary>
    struct TypeGroupInstance : IEquatable<TypeGroupInstance>
    {
        public uint TypeID { get; set; }
        public uint GroupID { get; set; }
        public uint InstanceID { get; set; }

        public TypeGroupInstance(uint type, uint group, uint instance)
            : this()
        {
            TypeID = type;
            GroupID = group;
            InstanceID = instance;
        }

        public TypeGroupInstance(string type_hex, string group_hex, string instance_hex)
            : this()
        {
            TypeID = uint.Parse(type_hex, System.Globalization.NumberStyles.HexNumber);
            GroupID = uint.Parse(group_hex, System.Globalization.NumberStyles.HexNumber);
            InstanceID = uint.Parse(instance_hex, System.Globalization.NumberStyles.HexNumber);
        }

        public override bool Equals(object obj)
        {
            if (obj is TypeGroupInstance)
            {
                return this.Equals((TypeGroupInstance)obj);
            }
            return false;
        }

        public bool Equals(TypeGroupInstance tgi)
        {
            return (TypeID == tgi.TypeID) && (GroupID == tgi.GroupID) && (InstanceID == tgi.InstanceID);
        }

        public override int GetHashCode()
        {
            return (int)(TypeID + GroupID + InstanceID);
        }

        public static bool operator ==(TypeGroupInstance lhs, TypeGroupInstance rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(TypeGroupInstance lhs, TypeGroupInstance rhs)
        {
            return !(lhs.Equals(rhs));
        }

        public static TypeGroupInstance FromHex(string type_hex)
        {
            return new TypeGroupInstance(
                type_hex,
                "0",
                "0"
            );
        }

        public static TypeGroupInstance FromHex(string type_hex, string group_hex)
        {
            return new TypeGroupInstance(
                type_hex,
                group_hex,
                "0"
            );
        }

        public static TypeGroupInstance FromHex(string type_hex, string group_hex, string instance_hex)
        {
            return new TypeGroupInstance(
                type_hex,
                group_hex,
                instance_hex
            );
        }

        public void Dump()
        {
            Console.WriteLine("{0} {1} {2}",
                TypeID.ToString("X"),
                GroupID.ToString("X"),
                InstanceID.ToString("X"));
        }
    }
}
