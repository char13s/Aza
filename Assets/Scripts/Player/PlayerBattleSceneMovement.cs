using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#pragma warning disable 0649
public class PlayerBattleSceneMovement : MonoBehaviour {
    private List<Enemy> enemies = new List<Enemy>(16);
    private Player pc;
    private int t;//targeted enemy in the array of enemies
    private Enemy enemyTarget;
    public static event UnityAction onLockOn;
    public List<Enemy> Enemies { get => enemies; set => enemies = value; }
    public int T { get => t; set { t = value;  Mathf.Clamp(t, 0, Enemies.Count); } }
    public Enemy EnemyTarget { get => enemyTarget; set { enemyTarget = value; Debug.Log("oof"); } }
    public float RotateSpeed { get => rotateSpeed; set { rotateSpeed = value;Mathf.Clamp(value,5,8); } }

    private AxisButton dPadLeft = new AxisButton("DPad Left");
    private AxisButton dPadRight = new AxisButton("DPad Right");
    private bool pressed;
    private bool locked;
    private float rotateSpeed;

    private AxisButton L2 = new AxisButton("L2");
    private bool slide;

    private void Awake() {
        Player.attackModeUp += LockOnFuctionality;
        Enemy.onAnyDefeated += RemoveTheDead;
        Player.onPlayerDeath += RemoveAllEnemies;
        AIKryll.teleport += TeleportAttacking;
        GameController.onGameWasStarted += RemoveAllEnemies;
        Player.findClosestEnemy += GetClosestEnemy;
        Player.playerIsLockedOn += Locked;
        Player.unlocked += Unlocked;
        T = 0;

    }
    private void Start() {

        pc = Player.GetPlayer();

    }
    private void RemoveTheDead(Enemy enemy) {
        Enemies.Remove(enemy);
    }
    private void RemoveAllEnemies() {
        Enemies.Clear();
    }

    private void Update() {
        Vector3 position = transform.position;
        for (int i = 0; i < Enemy.TotalCount; i++) {
            Enemy current = Enemy.GetEnemy(i);
            bool shouldBeInList = Vector3.SqrMagnitude(current.transform.position - position) <= 324;
            int index = enemies.IndexOf(current);
            if (shouldBeInList != index >= 0) {
                if (shouldBeInList) {
                    enemies.Add(current);
                }
                else { enemies.RemoveAt(index); }
            }
        }
        if (Enemies.Count > 0 && pc.LockedOn) {
            SwitchLockOn();
            GetInput();
        }
    }
    private void LockOnFuctionality() {
        


    }

    private void Locked() { locked = true; Debug.Log("lokced on to T"); }
    private void Unlocked() { locked = false; }
    private void GetInput() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float mH = Input.GetAxisRaw("MouseX");
        float jH = Input.GetAxisRaw("Camera");
        if (Enemies.Count > 0) {
            LockOn(x, y, mH, jH, Enemies[T]);
        }
        //MovementInputs(x,y);
    }

    private void LockOff() {
        foreach (Enemy en in Enemies) {
            if (Enemy.GetEnemy(T) != en) {
                en.LockedOn = false;
            }
        }
    }
    private void TeleportAttacking(Vector3 location, int t) {
        transform.position = location;
        pc.CmdInput = 101;
    }
    private void EnemyLockedTo() {
        EnemyTarget = enemies[T]; //Enemy.GetEnemy(enemies.IndexOf(enemies[T])); stupid code -_-
        Player.GetPlayer().BattleCamTarget.transform.position = EnemyTarget.transform.position;
        Debug.Log(T);
    }
    private void GetClosestEnemy() {
        float enDist = EnDist(enemies[T]);
        foreach (Enemy en in Enemies) {

            if (EnDist(en) < enDist) {
                T = Enemies.IndexOf(en);
            }
        }
    }
    private float EnDist(Enemy target) => Vector3.Distance(target.transform.position, pc.transform.position);
    private void LockOn(float x, float y, float mH, float jH, Enemy target) {
        LockOff();
        enemies[T].LockedOn = true;
        EnemyLockedTo();
        //if (!slide) { 
        if (x == 0) {
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
        if (Mathf.Abs(x) >= 0.001 || Mathf.Abs(y) >= 0.001) {
            pc.Moving = true;
            pc.Animations = 1;
        }
        else {
            pc.Moving = false;
            pc.Animations = 0;
        }
        if (Enemies[T] != null&&!slide) {

            if (onLockOn != null) {
                onLockOn();
            }
            RotateSpeed = 18 - EnDist(target);
            //Player.GetPlayer().Nav.enabled = true;
            Vector3 delta = target.transform.position - pc.transform.position;
            delta.y = 0;
            transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
            //transform.LookAt(Enemies[T].transform.position,Vector3.up);
            transform.RotateAround(target.transform.position, target.transform.up, -x * rotateSpeed * pc.MoveSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, pc.MoveSpeed * y * Time.deltaTime);

        }
        if (Enemies[T].Dead) {
            GetClosestEnemy();
        }
    }
    private void MovementInputs(float x,float y) {
        if (L2.GetButton()&&!slide) {
            slide = true;
            StartCoroutine(Dash());
            
            
        }if (x == 0) {
                if (y > 0)//forward
                {
                    Player.GetPlayer().RBody.AddForce(Player.GetPlayer().transform.forward * 10, ForceMode.VelocityChange);

                }

                if (y < 0)//back
                {
                    Player.GetPlayer().RBody.AddForce(Player.GetPlayer().transform.forward * -10, ForceMode.VelocityChange);

                }
            }

            if (x > 0.3)//right
            {
                Vector3 delta = enemyTarget.transform.position - pc.transform.position;
                delta.y = 0;
                transform.rotation = Quaternion.LookRotation(delta, Vector3.right);
                Player.GetPlayer().RBody.AddForce(Player.GetPlayer().transform.forward*10,ForceMode.VelocityChange);
            }

            if (x < -0.3)//left
            {
                Vector3 delta = enemyTarget.transform.position - pc.transform.position;
                delta.y = 0;
                transform.rotation = Quaternion.LookRotation(delta, -Vector3.right);
                Player.GetPlayer().RBody.AddForce(Player.GetPlayer().transform.forward * 10, ForceMode.VelocityChange);
            }
    }
    private IEnumerator Dash() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        slide = false;

    }
    private void SwitchLockOn() {
        if (Input.GetAxis("DPad Right") > 0 && dPadRight.GetButtonDown()) {
            T++;

        }
        if (Input.GetAxis("DPad Left") < 0 && !pressed) {
            if (T == 0) {
                T = Enemies.Count;
            }
            T--;
            pressed = true;
        }
        if (Input.GetAxis("DPad Right") >= 0) {
            pressed = false;
        }

        if (T == Enemies.Count) {
            T = 0;
        }
    }
}
