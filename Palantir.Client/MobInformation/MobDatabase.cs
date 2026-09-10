using System;
using System.Collections.Generic;
using System.Text;

namespace Palantir.MobInformation;

public partial class MobDatabase
{
    public enum AggroType
    {
        Sight = 1 << 0,
        Sound = 1 << 1,
        Proximity = 1 << 2,
    }

    public enum DangerLevel
    {
        Easy = 1 << 0,
        Caution = 1 << 1,
        Danger = 1 << 2,
    }

    public enum DeepDungeon
    {
        PotD = 1 << 0,
        HoH = 1 << 1,
        EO = 1 << 2,
        PT = 1 << 3,
        Unk = 1 << 20,
    }

    public enum ESPType
    {
        Enemy = 1 << 0,
        FriendlyEnemy = 1 << 1, // HoH mobs here. Useful to know
        Mimic = 1 << 2,
    }

    public class AggroInfo
    {
        public uint TerritoryId { get; set; } = 0;
        public AggroType AggroType { get; set; }
        public DangerLevel DangerLevel { get; set; }
        public bool Patrol { get; set; }
    }


    public class MobInfo
    {
        public required uint Id { get; set; } = 0;
        public uint BNcpId { get; set; } = 0;
        public uint TerritoryId { get; set; } = 0;
        public DeepDungeon Dungeon { get; set; } = DeepDungeon.Unk;
        public ESPType MobType { get; set; } = ESPType.Enemy;
        public AggroType AggroType { get; set; } = AggroType.Proximity;
        public DangerLevel DangerLevel { get; set; } = DangerLevel.Danger;
        public bool Patrol { get; set; } = false;
        public bool BossOrAdd { get; set; } = false;
        public bool Special { get; set; } = false;
        public List<AggroInfo> FloorAgro { get; set; } = new();

        public AggroInfo GetAggroInfo(uint TerritoryId)
        {
            var floorOverride = FloorAgro.FirstOrDefault(f => f.TerritoryId == TerritoryId);

            return new AggroInfo
            {
                TerritoryId = TerritoryId,
                AggroType = floorOverride?.AggroType ?? AggroType,
                DangerLevel = floorOverride?.DangerLevel ?? DangerLevel,
                Patrol = floorOverride?.Patrol ?? Patrol
            };
        }
    }

    public static Dictionary<uint, MobInfo> MobInformation = new();

    public static void RegisterMobInfo()
    {
        if (MobInformation.Count == 0)
        {
            Register_PotD();
            Register_HoH();
            Register_EO();
            Register_PT();
        }
    }
}
