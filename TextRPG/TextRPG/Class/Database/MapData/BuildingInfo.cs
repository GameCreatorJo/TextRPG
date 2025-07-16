using System;

namespace TextRPG.Class.Database.MapData
{
    public class BuildingInfo
    {
        public int X { get; }
        public int Y { get; }
        public char[,] Pattern { get; }
        public string Type { get; }
        public string NextMap { get; }
        public int? NextMapSpawnX { get; }
        public int? NextMapSpawnY { get; }

        // 생성자
        public BuildingInfo(int x, int y, char[,] pattern, string type = null, string nextMap = null, int? nextMapSpawnX = null, int? nextMapSpawnY = null)
        {
            X = x;
            Y = y;
            Pattern = pattern;
            Type = type;
            NextMap = nextMap;
            NextMapSpawnX = nextMapSpawnX;
            NextMapSpawnY = nextMapSpawnY;
        }

        // 각 빌딩 타입별 아스키아트 패턴(여기서 관리)
        public static readonly char[,] InnPattern = new char[7, 6]
        {
            { ' ', ' ', '▲', ' ', ' ', ' ' },
            { ' ', '▲', '▲', '▲', ' ', ' ' },
            { '▲', '▲', '▲', '▲', '▲', ' ' },
            { '▣', '▣', '▣', '▣', '▣', ' ' },
            { '▣', '▣', '▣', '▣', '▣', 'I' },
            { '▣', '▣', '@', '▣', '▣', 'N' },
            { '▣', '▣', '@', '▣', '▣', 'N' }
        };

        public static readonly char[,] ShopPattern = new char[7, 6]
        {
            { ' ', ' ', '▲', ' ', ' ', ' ' },
            { ' ', '▲', '▲', '▲', ' ', ' ' },
            { '▲', '▲', '▲', '▲', '▲', ' ' },
            { '▣', '▣', '▣', '▣', '▣', 'S' },
            { '▣', '▣', '▣', '▣', '▣', 'H' },
            { '▣', '▣', '@', '▣', '▣', 'O' },
            { '▣', '▣', '@', '▣', '▣', 'P' }
        };

        public static readonly char[,] PortalCavePattern = new char[3, 7]
        {
            { ' ', ' ', '○', '○', '○', ' ', ' ' },
            { ' ', '○', '●', '●', '●', '○', ' ' },
            { '○', '●', '●', '@', '●', '●', '○' },
        };
        public static readonly char[,] DungeonExitPattern = new char[1, 1]
        {
            { '@' }
        };

        public static readonly char[,] ShopInPattern = new char[4, 7]
        {


            { '|', '○', ' ', ' ', ' ', '○','|' },
            { '|', '○', ' ', ' ', ' ', '○','|' },
            { '|', '○', '○', '@', '○', '○','|' },
            { '|', '-', '-', '-', '-','-', '|' }

        };
        public static readonly char[,] InnInPattern = new char[4, 7]
        {

            { '|', '○', ' ', ' ', ' ', '○','|' },
            { '|', '○', ' ', ' ', ' ', '○','|' },
            { '|', '○', '○', '@', '○', '○','|' },
            { '|', '-', '-', '-', '-','-', '|' }

        };

    }
}
