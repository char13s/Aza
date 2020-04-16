using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Game
{


   
    private List<ItemData> items;
    private Stats stats;
    private float[] playerPosition;
    private int lasLevel;
    public List<ItemData> Items { get => items; set => items = value; }
    public float[] PlayerPosition { get => playerPosition; set => playerPosition = value; }
    public Stats Stats { get => stats; set => stats = value; }

    public Game(Player player){
        
        Items = player.items.Items;
        stats = player.stats;
        
        PlayerPosition = new float[3];
        PlayerPosition[0] = player.transform.position.x;
        PlayerPosition[1] = player.transform.position.y;
        PlayerPosition[2] = player.transform.position.z;
    }
    public void SetDefaults() {

    }
}
