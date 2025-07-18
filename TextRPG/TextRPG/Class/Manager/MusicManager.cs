using System;
using System.IO;
using System.Media;

namespace TextRPG.Class.Manager
{
    public class MusicManager
    {
        private SoundPlayer _soundPlayer;

        // Resource 폴더 아래 bgm 파일들
        private string basePath = AppDomain.CurrentDomain.BaseDirectory;

        private Dictionary<int, string> bgmPaths;

        public MusicManager()
        {
            // 실행 파일 위치 기준으로 Resource 폴더 경로를 설정
            bgmPaths = new Dictionary<int, string>
            {
                { 1, Path.Combine(basePath, "Resource", "BattleMusic.wav") },
                { 2, Path.Combine(basePath, "Resource", "DungeonMusic.wav") },
                { 3, Path.Combine(basePath, "Resource", "TownMusic.wav") }
                ,
                { 4, Path.Combine(basePath, "Resource", "StartMusic.wav") }
            };

            _soundPlayer = new SoundPlayer();
        }

        private void PlayBgm(int index)
        {
            if (!bgmPaths.TryGetValue(index, out var path) || !File.Exists(path))
            {
                Console.WriteLine($"BGM{index} 파일을 찾을 수 없습니다: {path}");
                return;
            }

            try
            {
                _soundPlayer.Stop();          
                _soundPlayer.SoundLocation = path;
                _soundPlayer.Load();
                _soundPlayer.PlayLooping();
            }
            catch (Exception ex)
            {
            }
        }

        public void PlayBgm1() => PlayBgm(1);
        public void PlayBgm2() => PlayBgm(2);
        public void PlayBgm3() => PlayBgm(3);
        public void PlayBgm4() => PlayBgm(4);

        public void Stop() => _soundPlayer?.Stop();
    }
}
