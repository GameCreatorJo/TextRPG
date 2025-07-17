using System;
using TextRPG.Class.Manager;
using TextRPG.Class.Test;
class Program
{
    static void Main(string[] args)
    {
        GameManager gameManager = new GameManager();
        gameManager.StartGame();
        
        //SystemTest.Run();
    }

}