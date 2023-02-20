using System;

namespace SC4Parser
{
    public class OccupancyGroup
    {
        public int Index { get; private set; }
        public uint GroupId { get; private set; }
        public uint Population { get; private set; }
        public string Name { get; private set; }

        /// <summary>
        /// Constructs a Occupancy group
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="groupId"></param>
        /// <param name="population"></param>
        /// <remarks>Intended to be used as part of the RegionView Subfile</remarks>
        public OccupancyGroup(int index, string name, uint groupId, uint population)
        {
            Index = index;
            GroupId = groupId;
            Population = population;
            Name = name;
        }

        public void Dump()
        {
            Console.WriteLine("Index={0} Name=\"{1}\" GroupId=0x{2} Population={3}",
                Index,
                Name,
                GroupId.ToString("x2"),
                Population);
        }
    }
}
