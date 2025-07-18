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

        public static readonly char[,] BuildingPattern = new char[4, 3]
        {
            {' ', '▲', ' '},
            {'▲', '▲', '▲'},
            { '▣','@','▣'},
            { '▣','@','▣'}
        };
        public static readonly char[,] InnPannelPattern = new char[1, 3]
        {
            {'I', 'N', 'N'},
        };

        public static readonly char[,] ShopPannelPattern = new char[1, 4]
        {
            {'S', 'H', 'O','P'},
        };

        public static readonly char[,] GuildPannelPattern = new char[1, 5]
        {
            {'G', 'U', 'I','L','D'},
        };
        public static readonly char[,] DungeonPannelPattern = new char[1, 7]
        {
            {'D', 'U', 'N','G','E','O','N'},
        };

        public static readonly char[,] PortalCavePattern = new char[3, 7]
        {
            { ' ', ' ', '○', '○', '○', ' ', ' ' },
            { ' ', '○', '●', '●', '●', '○', ' ' },
            { '○', '●', '●', '@', '●', '●', '○' },
        };

        public static readonly char[,] PortalCave2Pattern = new char[6, 7]
        {
            { '╔', '═', '╦','╦', '╦', '═', '╗' },
            { '║', '█', '║','║', '║', '█', '║' },
            { '║', '█', '║','@', '║', '█', '║' },
            { '║', '█', '║','@', '║', '█', '║' },
            { '║', '█', '║','@', '║', '█', '║' },
            { '╚', '═', '╩','@', '╩', '═', '╝' },
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
        public static readonly char[,] GuildInPattern = new char[4, 7]
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

        public static readonly char[,] MonsterPattern = new char[3, 3]
        {
        { ' ', '^', ' ' },
        { '(', 'o', ')' },
        { ' ', '@', ' ' }
        };

    }
}
