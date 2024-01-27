using System.Collections.Generic;

public class GameConfig
{
    public Dictionary<string, string[]> Characters { get; private set; }
    public List<string> AllTheTags { get; private set; }

    public GameConfig()
    {
        Characters = new Dictionary<string, string[]>();
        Characters.Add("mickey", new string[]{ "Animación", "Rojo", "Animal", "Amarillo", "Poca ropa" });
        Characters.Add("alicia", new string[] { "Animación", "Azul", "Niños", "Amarillo" });
        Characters.Add("winnieh", new string[] { "Animación", "Animal", "Amarillo", "Poca ropa" });
        Characters.Add("popeye", new string[] { "Animación", "Fuerte", "Azul", "Mar" });
        Characters.Add("tarzan", new string[] { "Fuerte", "Poca ropa" });
        Characters.Add("cthulhu", new string[] { "Terror", "Verde", "Mar", "Poca ropa" });
        Characters.Add("dracula", new string[] { "Terror", "Rojo" });
        Characters.Add("caperucita", new string[] { "Rojo", "Niños" });
        Characters.Add("sherlock", new string[] { "Verde", "Justiciero" });
        Characters.Add("robinhood", new string[] { "Verde", "Justiciero" });
        Characters.Add("principito", new string[] { "Azul", "Amarillo" });

        AllTheTags = new List<string>();
        foreach (var character in Characters.Keys)
        {
            foreach (var tag in Characters[character])
            {
                if (!AllTheTags.Contains(tag))
                    AllTheTags.Add(tag);
            }
        }
    }
}

