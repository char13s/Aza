
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class PlayerBattleSceneMovement : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>(16);
    private Player pc;
    private int t;//targeted enemy in the array of enemies
    public List<Enemy> Enemies { get => enemies; set => enemies = value; }
    public int T { get => t; set => t = value; }

    void Start()
    {
        Enemy.onAnyDefeated += RemoveTheDead;
        Player.onPlayerDeath += RemoveAllEnemies;
        
        GameController.onGameWasStarted += RemoveAllEnemies;
        T = 0;
        pc = Player.GetPlayer();
        
    }
    private void RemoveTheDead(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }
    private void RemoveAllEnemies()
    {
        Enemies.Clear();
    }
    
    void Update()
    {
        Vector3 position = transform.position;
        for (int i = 0; i < Enemy.TotalCount; i++)
        {
            Enemy current = Enemy.GetEnemy(i);
            bool shouldBeInList = Vector3.SqrMagnitude(current.transform.position - position) <= 169;
            int index = enemies.IndexOf(current);
            if (shouldBeInList != index >= 0)
            {
                if (shouldBeInList)
                {
                    enemies.Add(current);
                }
                else { enemies.RemoveAt(index); }
            }
        }

        if (Enemies.Count > 0 && pc.LockedOn)
        {
            SwitchLockOn();

            GetInput();
        }
    }

    void GetInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * 20 * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        float mH = Input.GetAxisRaw("MouseX");
        float jH = Input.GetAxisRaw("Camera");
        if (Enemies.Count > 0)
        {
            LockOn(x, y, mH, jH);
        }
        if (pc.LeftDash)
        {
            transform.RotateAround(Enemies[T].transform.position, Enemies[T].transform.up, 80 * Time.deltaTime);
        }
        if (pc.RightDash)
        {
            transform.RotateAround(Enemies[T].transform.position, Enemies[T].transform.up, -80 * Time.deltaTime);
        }
    }

    void LockOff()
    {
        foreach (Enemy en in Enemies)
        {
            if (Enemies[T] != en)
            {
                en.LockedOn = false;
            }
        }
    }

    void LockOn(float x, float y, float mH, float jH)
    {
        Enemies[T].LockedOn = true;
        LockOff();
        if (y > 0)//forward
        {
            pc.Direction = 0;
        }

        if (y < 0)//back
        {

            pc.Direction = 2;
        }

        if (x > 0)//right
        {
            pc.Direction = 3;
        }

        if (x < 0)//left
        {
            pc.Direction = 1;
        }
        if (Mathf.Abs(x) >= 0.001 || Mathf.Abs(y) >= 0.001)
        {
            pc.Moving = true;
        }
        else
        {
            pc.Moving = false;
        }
        if (!Enemies[T].Dead)
        {
            Vector3 delta = Enemies[T].transform.position - pc.transform.position;
            delta.y = 0;
            transform.rotation = Quaternion.LookRotation(delta,Vector3.up);
            //transform.LookAt(Enemies[T].transform.position,Vector3.up);
            transform.RotateAround(Enemies[T].transform.position, Enemies[T].transform.up,  -x *80* Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, Enemies[T].transform.position, 160 * y * Time.deltaTime);
            
        }
    }

    public void SwitchLockOn()
    {
        if (Input.GetButtonDown("L1"))
        {
            T++;
        }
        if (T == Enemies.Count)
        {
            T = 0;
        }
    }
}
