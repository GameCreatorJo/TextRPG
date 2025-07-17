using System;
using System.Collections.Generic;
using TextRPG.Class.Database.MonsterData;
using TextRPG.Class.Default;
using TextRPG.Class.Manager;

namespace TextRPG.Class.Database.MapData
{
    public class MapDatabase
    {
        private readonly Dictionary<string, Map> maps;
        public IReadOnlyDictionary<string, Map> Maps => maps;
        public static readonly (int x, int y) DungeonSpawn = (1, 1);

        // Town 빌딩/포탈 정보(중심좌표, 패턴, 타입, 다음맵, 다음맵 등장좌표)
        public static readonly List<BuildingInfo> TownBuildings = new()
        {
            new BuildingInfo(3, 4, BuildingInfo.BuildingPattern, "Inn"),
            new BuildingInfo(2, 1, BuildingInfo.InnPannelPattern, "Inn"),
            new BuildingInfo(8, 4, BuildingInfo.BuildingPattern, "Shop"),
            new BuildingInfo(9, 1, BuildingInfo.ShopPannelPattern, "Shop"),
            new BuildingInfo(15, 4, BuildingInfo.BuildingPattern, "Guild"),
            new BuildingInfo(15, 1, BuildingInfo.GuildPannelPattern, "Guild"),
            new BuildingInfo(4, 15, BuildingInfo.PortalCavePattern, "Dungeon"),
            new BuildingInfo(4, 13, BuildingInfo.DungeonPannelPattern, "Dungeon"),
            new BuildingInfo(15, 15, BuildingInfo.PortalCave2Pattern, "Dungeon2"),
            new BuildingInfo(15, 11, BuildingInfo.DungeonPannelPattern, "Dungeon2"),

        };

        // 던전 출구 정보 추가 (포탈 역할)
        public static readonly List<BuildingInfo> DungeonBuildings = new()
        {
            new BuildingInfo(10, 18, BuildingInfo.DungeonExitPattern, "Exit"),
            new BuildingInfo(10, 10, BuildingInfo.MonsterPattern, "Monster", "Battle")
        };

        public static readonly List<BuildingInfo> ShopBuildings = new()
        {
            new BuildingInfo(10, 8, BuildingInfo.ShopInPattern, "Shop"),
            new BuildingInfo(10, 18, BuildingInfo.DungeonExitPattern, "Exit")

        };

        public static readonly List<BuildingInfo> InnBuildings = new()
        {
            new BuildingInfo(10, 8, BuildingInfo.InnInPattern, "Inn"),
            new BuildingInfo(10, 18, BuildingInfo.DungeonExitPattern, "Exit")
        };
        public static readonly List<BuildingInfo> GuildBuildings = new()
        {
            new BuildingInfo(10, 8, BuildingInfo.GuildInPattern, "Guild"),
            new BuildingInfo(10, 18, BuildingInfo.DungeonExitPattern, "Exit")
        };
        public static readonly List<BuildingInfo> Dungeon2Buildings = new()
        {
            new BuildingInfo(10, 18, BuildingInfo.DungeonExitPattern, "Exit"),
            new BuildingInfo(5, 5, BuildingInfo.MonsterPattern, "Monster", "Battle"),
            new BuildingInfo(5, 10, BuildingInfo.MonsterPattern, "Monster", "Battle")
        };



        public MapDatabase()
        {
            maps = new Dictionary<string, Map>(StringComparer.OrdinalIgnoreCase);
        }


        public void CreateMap()
        {
            var town = new Map(20, 20, 0.0, TownBuildings);
            town.Initialize();
            town.BuildBuildings();
            maps["Town"] = town;

            var dungeon = new Map(20, 20, 0.2, DungeonBuildings);
            dungeon.Initialize();
            dungeon.BuildBuildings();

            maps["Dungeon"] = dungeon;

            var shop = new Map(20, 20, 0.0, ShopBuildings);
            shop.Initialize();
            shop.BuildBuildings();
            maps["Shop"] = shop;

            // Inn 맵 생성
            var inn = new Map(20, 20, 0.0, InnBuildings);
            inn.Initialize();
            inn.BuildBuildings();
            maps["Inn"] = inn;

            var guild = new Map(20, 20, 0.0, GuildBuildings);
            guild.Initialize();
            guild.BuildBuildings();
            maps["Guild"] = guild;

            var dungeon2 = new Map(20, 20, 0.0, Dungeon2Buildings);
            dungeon2.Initialize();
            dungeon2.BuildBuildings();
            maps["Dungeon2"] = dungeon2;
        }

        public void AddMap(string name, Map map)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("맵 이름 null 불가", nameof(name));
            if (map == null) throw new ArgumentNullException(nameof(map));
            maps[name] = map;
        }

        public Map GetMap(string name)
        {
            if (!maps.TryGetValue(name, out var map))
                throw new KeyNotFoundException($"맵 '{name}' 없음");
            return map;
        }
    }
}
