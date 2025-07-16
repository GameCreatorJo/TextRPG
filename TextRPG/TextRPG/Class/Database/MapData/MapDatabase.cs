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
            new BuildingInfo(7, 5, BuildingInfo.InnPattern, "Inn"),
            new BuildingInfo(15, 5, BuildingInfo.ShopPattern, "Shop"),
            new BuildingInfo(10, 15, BuildingInfo.PortalCavePattern, "Dungeon", "Dungeon", 10, 16)
        };

        // 던전 출구 정보 추가 (포탈 역할)
        public static readonly List<BuildingInfo> DungeonBuildings = new()
        {
            new BuildingInfo(10, 18, BuildingInfo.DungeonExitPattern, "Exit", "Town", 10, 10),
            new BuildingInfo(10, 10, BuildingInfo.MonsterPattern, "Monster", "Battle")
        };

        public static readonly List<BuildingInfo> ShopBuildings = new()
        {
            new BuildingInfo(10, 8, BuildingInfo.ShopInPattern, "Shop"),
            new BuildingInfo(10, 18, BuildingInfo.DungeonExitPattern, "Exit", "Town", 10, 10)

        };

        public static readonly List<BuildingInfo> InnBuildings = new()
        {
            new BuildingInfo(10, 8, BuildingInfo.InnInPattern, "Inn"),
            new BuildingInfo(10, 18, BuildingInfo.DungeonExitPattern, "Exit", "Town", 10, 10)
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

            var dungeonMonsters = new List<Monster>();
            foreach (var m in GameManager.Instance.CreateManager.MonsterDatabase.MonsterDictionary.Values)
                dungeonMonsters.Add(new Monster(m));

            var dungeon = new Map(20, 20, 0.2, DungeonBuildings);
            dungeon.Initialize();
            dungeon.BuildBuildings();

            // 플레이어 시작 위치 명확히 지정
            dungeon.PlayerX = DungeonSpawn.x;
            dungeon.PlayerY = DungeonSpawn.y;

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
