using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Game
{
    private List<ItemData> items;
    private Stats stats;
    private GameObject spawn;
    private int skullCount;
    private int lighBulbCount;
    private int lastLevel;
    public List<ItemData> Items { get => items; set => items = value; }
    public Stats Stats { get => stats; set => stats = value; }
    public GameObject Spawn { get => spawn; set => spawn = value; }

    public Game(Player player){
        Spawn = GameController.GetGameController().Spawn;
        skullCount = Player.GetPlayer().SkullMask;
        lighBulbCount = Player.GetPlayer().Bulbs;
        Items = player.items.Items;
        stats = player.stats;
        Debug.Log("Game was saved");
    }
}
