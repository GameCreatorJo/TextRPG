using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Default
{
    internal abstract class DefaultScene
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Removed the unnecessary semicolon after the constructor block
        public DefaultScene()
        {
            Name = "Default Scene";
            Description = "This is the default scene description.";
        }

        public DefaultScene(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public virtual void Render()
        {
            Console.Clear();
            Console.WriteLine($"스파르타 마을에 오신걸 환영합니다.");
            Console.WriteLine($"어떤 행동을 하시겠습니까?.");
        }
    }
}
