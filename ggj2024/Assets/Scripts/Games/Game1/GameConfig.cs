using System.Collections.Generic;
using System.Linq;

public class GameConfig
{
    public Dictionary<string, string[]> Characters { get; private set; }
    public List<string> AllTheTags { get; private set; }
    public List<LevelConfig> LevelConfigs { get; private set; }

    public GameConfig()
    {
        Characters = new Dictionary<string, string[]>();
        Characters.Add("mickey", new string[]{ "Animación", "Rojo", "Animal", "Amarillo", "Poca ropa" });
        Characters.Add("alicia", new string[] { "Animación", "Azul", "Niñez", "Amarillo" });
        Characters.Add("winnie", new string[] { "Animación", "Animal", "Furries", "Amarillo", "Poca ropa" });
        Characters.Add("popeye", new string[] { "Animación", "Fuerte", "Azul", "Mar" });
        Characters.Add("tarzan", new string[] { "Fuerte", "Poca ropa" });
        Characters.Add("cthulhu", new string[] { "Terror", "Verde", "Mar", "Poca ropa" });
        Characters.Add("dracula", new string[] { "Terror", "Rojo" });
        Characters.Add("caperucita", new string[] { "Rojo", "Niñez" });
        Characters.Add("sherlock", new string[] { "Verde", "Justiciero" });
        Characters.Add("robinhood", new string[] { "Verde", "Justiciero" });
        Characters.Add("principito", new string[] { "Azul", "Amarillo", "Niñez" });
        Characters.Add("kingkong", new string[] { "Animal", "Furry", "Fuerte" });
        Characters.Add("peterpan", new string[] { "Verde", "Niñez" });

        AllTheTags = new List<string>();
        foreach (var character in Characters.Keys)
        {
            foreach (var tag in Characters[character])
            {
                if (!AllTheTags.Contains(tag))
                    AllTheTags.Add(tag);
            }
        }

        LevelConfigs = new List<LevelConfig>();
        LevelConfigs.Add(new LevelConfig(1, 30));
        LevelConfigs.Add(new LevelConfig(1, 30));
        LevelConfigs.Add(new LevelConfig(2, 30));
        LevelConfigs.Add(new LevelConfig(2, 30));
        LevelConfigs.Add(new LevelConfig(3, 30));
        LevelConfigs.Add(new LevelConfig(3, 20));
        LevelConfigs.Add(new LevelConfig(3, 20));
        LevelConfigs.Add(new LevelConfig(4, 30));
        LevelConfigs.Add(new LevelConfig(4, 20));
        LevelConfigs.Add(new LevelConfig(4, 20));
        LevelConfigs.Add(new LevelConfig(4, 20));
        LevelConfigs.Add(new LevelConfig(4, 20));
        LevelConfigs.Add(new LevelConfig(5, 20));
        LevelConfigs.Add(new LevelConfig(5, 20));
        LevelConfigs.Add(new LevelConfig(5, 10));
    }

    public string[] GetRandomTags(int countTags)
    {
        return AllTheTags.OrderBy(x => UnityEngine.Random.value).Take(countTags).ToArray();
    }
}

public class LevelConfig
{
    public int CountTags { get; private set; }
    public float Time { get; private set; }

    public LevelConfig(int countTags, float time)
    {
        CountTags = countTags;
        Time = time;
    }
}

