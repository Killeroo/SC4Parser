using System;

namespace SC4Parser.Types
{
    /// <summary>
    /// TypeGroupInstance (TGI) is used as an identifier for files within a SimCity 4 savegame (DBPF)
    /// It consists of the items TypeID, GroupID and InstanceID. The combination of these fields creates
    /// a unique reference for the file. 
    /// It is used in comparisons a lot and the values for the fields are quite often referenced as hex 
    /// so it is represented here with all the neccessary methods for usage, conversion and comparison
    /// mainly for convience.
    /// More info here: https://www.wiki.sc4devotion.com/index.php?title=Type_Group_Instance
    /// </summary>
    public struct TypeGroupInstance : IEquatable<TypeGroupInstance>
    {
        public uint Type { get; set; }
        public uint Group { get; set; }
        public uint Instance { get; set; }

        public TypeGroupInstance(uint type, uint group, uint instance)
            : this()
        {
            Type = type;
            Group = group;
            Instance = instance;
        }

        public TypeGroupInstance(string type_hex, string group_hex, string instance_hex)
            : this()
        {
            Type = uint.Parse(type_hex, System.Globalization.NumberStyles.HexNumber);
            Group = uint.Parse(group_hex, System.Globalization.NumberStyles.HexNumber);
            Instance = uint.Parse(instance_hex, System.Globalization.NumberStyles.HexNumber);
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
            return (Type == tgi.Type) && (Group == tgi.Group) && (Instance == tgi.Instance);
        }

        public override int GetHashCode()
        {
            return (int)(Type + Group + Instance);
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

        public new string ToString()
        {
            return string.Format("{0} {1} {2}",
                Type.ToString("X"),
                Group.ToString("X"),
                Instance.ToString("X"));
        }

        public void Dump()
        {
            Console.WriteLine("{0} {1} {2}",
                Type.ToString("X"),
                Group.ToString("X"),
                Instance.ToString("X"));
        }
    }
}
