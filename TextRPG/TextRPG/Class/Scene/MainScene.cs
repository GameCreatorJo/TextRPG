using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Default;

namespace TextRPG.Class.Scene
{
    internal class MainScene : DefaultScene
    { 
        public string Name { get; set; }
        public string Description { get; set; }

        // Removed the unnecessary semicolon after the constructor block
        public MainScene(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine($"스파르타 마을에 오신걸 환영합니다.");
            Console.WriteLine($"어떤 행동을 하시겠습니까?");
            Console.WriteLine("1. 상점에 가기");
            Console.WriteLine("2. 퀘스트를 받기");
            Console.WriteLine("3. 전투하기");
            Console.WriteLine("4. 게임 종료");
        }

    }
}
