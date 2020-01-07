
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#pragma warning disable 0649
public class PlayerBattleSceneMovement : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>(16);
    private Player pc;
    private int t;//targeted enemy in the array of enemies
    private Enemy enemyTarget;
    public static UnityAction onLockOn;
    public List<Enemy> Enemies { get => enemies; set => enemies = value; }
    public int T { get => t; set { t = value;Mathf.Clamp(t,0,Enemies.Count); } }
    public Enemy EnemyTarget { get => enemyTarget; set => enemyTarget = value; }

    private AxisButton dPadLeft = new AxisButton("DPad Left");
    private AxisButton dPadRight = new AxisButton("DPad Right");
    private bool pressed;

    private void Start()
    {
        Enemy.onAnyDefeated += RemoveTheDead;
        Player.onPlayerDeath += RemoveAllEnemies;
        AIKryll.teleport += TeleportAttacking;
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

    private void Update()
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

    private void GetInput()
    {
        float x = Input.GetAxisRaw("Horizontal") ;
        float y = Input.GetAxisRaw("Vertical") ;
        float mH = Input.GetAxisRaw("MouseX");
        float jH = Input.GetAxisRaw("Camera");
        if (Enemies.Count > 0)
        {
            LockOn(x, y, mH, jH,Enemies[T]);
        }

    }

    private void LockOff()
    {
        foreach (Enemy en in Enemies)
        {
            if (Enemy.GetEnemy(T) != en)
            {
                en.LockedOn = false;
            }
        }
    }
    private void TeleportAttacking(Vector3 location,int t) {
        transform.position = location;
        pc.CmdInput = 101;
        
        
        
    }
    private void EnemyLockedTo()
    {
        EnemyTarget = enemies[T]; //Enemy.GetEnemy(enemies.IndexOf(enemies[T])); stupid code -_-
    }
    private void LockOn(float x, float y, float mH, float jH,Enemy target)
    {
        LockOff();
        enemies[T].LockedOn = true;
        EnemyLockedTo();
        
        if (x == 0)
        {
            if (y > 0)//forward
            {
                pc.Direction = 0;

            }

            if (y < 0)//back
            {

                pc.Direction = 2;
            }
        }

        if (x > 0.3)//right
        {
            pc.Direction = 3;
            Debug.Log("right");
        }

        if (x < -0.3)//left
        {
            pc.Direction = 1;
            Debug.Log("left");
        }
        if (Mathf.Abs(x) >= 0.001 || Mathf.Abs(y) >= 0.001)
        {
            pc.Moving = true;
            pc.Animations = 1;
        }
        else
        {
            pc.Moving = false;
            pc.Animations = 0;
        }
        if (Enemies[T] != null)
        {
            //Player.GetPlayer().Nav.enabled = true;
            Vector3 delta = target.transform.position - pc.transform.position;
            delta.y = 0;
            transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
            //transform.LookAt(Enemies[T].transform.position,Vector3.up);
            transform.RotateAround(target.transform.position, target.transform.up, -x  *5* pc.MoveSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, pc.MoveSpeed * y * Time.deltaTime);

        }
    }
    
    private void SwitchLockOn()
    {
        if (Input.GetAxis("DPad Right")>0&&dPadRight.GetButtonDown())
        {
            T++;
            
        }
        if (Input.GetAxis("DPad Left")< 0&&!pressed)
        {
            if (T == 0) {
                T = Enemies.Count;
            }
            T--;
            pressed = true;
        }
        if (Input.GetAxis("DPad Right") >= 0) {
            pressed = false;
        }

        if (T == Enemies.Count)
        {
            T = 0;
        }
    }
}
