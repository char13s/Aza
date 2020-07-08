using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Game {
    private List<ItemData> items;
    private Stats stats;
    private float[] position=new float[3];
    //private Vector3 spawn;
    private int skullCount;
    private int lighBulbCount;
    private int lastLevel;
    public List<ItemData> Items { get => items; set => items = value; }
    public Stats Stats { get => stats; set => stats = value; }
    public float[] Position { get => position; set => position = value; }

    //public Vector3 Spawn { get => spawn; set => spawn = value; }

    public Game(Player player){
        //Spawn = GameController.GetGameController().Spawn.transform.position;
        Debug.Log(Position[0].ToString());
        Position[0] = GameController.GetGameController().Spawn.transform.position.x;
        Position[1] = GameController.GetGameController().Spawn.transform.position.y;
        Position[2] = GameController.GetGameController().Spawn.transform.position.z;
        skullCount = Player.GetPlayer().SkullMask;
        lighBulbCount = Player.GetPlayer().Bulbs;
        Items = player.items.Items;
        stats = player.stats;
        Debug.Log("Game was saved");
    }
}
