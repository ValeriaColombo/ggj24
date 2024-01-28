using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameConfig
{
    public Dictionary<string, string[]> Characters { get; private set; }
    public List<string> AllTheTags { get; private set; }
    public List<LevelConfig> LevelConfigs { get; private set; }

    public GameConfig()
    {
        Characters = new Dictionary<string, string[]>();
        Characters.Add("mickey", new string[] { "animacion", "byn", "rojo", "animal", "amarillo", "pocaropa" });
        Characters.Add("alicia", new string[] { "animacion", "azul", "ninez", "amarillo" });
 //       Characters.Add("winnie", new string[] { "animacion", "animal", "furry", "amarillo", "pocaropa" });
        Characters.Add("popeye", new string[] { "animacion", "fuerte", "azul", "mar" });
        Characters.Add("tarzan", new string[] { "fuerte", "pocaropa" });
        Characters.Add("cthulhu", new string[] { "terror", "verde", "mar", "pocaropa" });
        Characters.Add("dracula", new string[] { "terror", "rojo" });
        Characters.Add("caperucita", new string[] { "rojo", "ninez" });
        Characters.Add("sherlock", new string[] { "verde", "justiciero" });
        Characters.Add("robinhood", new string[] { "verde", "justiciero" });
        Characters.Add("principito", new string[] { "azul", "amarillo", "ninez", "verde" });
        Characters.Add("kingkong", new string[] { "animal", "furry", "fuerte", "byn" });
 //       Characters.Add("peterpan", new string[] { "verde", "ninez" });


        string s = "";
        AllTheTags = new List<string>();
        foreach (var character in Characters.Keys)
        {
            foreach (var tag in Characters[character])
            {
                if (!AllTheTags.Contains(tag))
                {
                    AllTheTags.Add(tag);
                    s += "\n" + tag;
                }
            }
        }
        Debug.Log(s);
        LevelConfigs = new List<LevelConfig>();
        LevelConfigs.Add(new LevelConfig(1, 30));//1
        LevelConfigs.Add(new LevelConfig(2, 25));//2
        LevelConfigs.Add(new LevelConfig(2, 25));//3
        LevelConfigs.Add(new LevelConfig(2, 25));//4
        LevelConfigs.Add(new LevelConfig(3, 25));//5
        LevelConfigs.Add(new LevelConfig(3, 25));//6
        LevelConfigs.Add(new LevelConfig(3, 25));//7
        LevelConfigs.Add(new LevelConfig(3, 25));//8
        LevelConfigs.Add(new LevelConfig(4, 25));//9
        LevelConfigs.Add(new LevelConfig(4, 25));//10
        LevelConfigs.Add(new LevelConfig(4, 25));//11
        LevelConfigs.Add(new LevelConfig(4, 20));//12
        LevelConfigs.Add(new LevelConfig(4, 20));//13
        LevelConfigs.Add(new LevelConfig(5, 20));//14
        LevelConfigs.Add(new LevelConfig(5, 15));//15
        LevelConfigs.Add(new LevelConfig(5, 15));//16
        LevelConfigs.Add(new LevelConfig(5, 15));//17
        LevelConfigs.Add(new LevelConfig(5, 10));//18
        LevelConfigs.Add(new LevelConfig(5, 10));//19
        LevelConfigs.Add(new LevelConfig(5, 10));//20
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

