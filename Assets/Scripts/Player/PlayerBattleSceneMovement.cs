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
    public static event UnityAction<int> playBattleTheme;
    private Enemy closestEnemy;
    private bool slide;
    private bool pressed;
    private bool locked;
    private float rotateSpeed;
    private GameObject aimPoint;
    private GameObject leftPoint;

    private AxisButton dPadLeft = new AxisButton("DPad Left");
    private AxisButton dPadRight = new AxisButton("DPad Right");
    private AxisButton L2 = new AxisButton("L2");
    private bool rotLock;
    private bool cutscening;
    private bool playing;
    private bool casual;

    public List<Enemy> Enemies { get => enemies; set => enemies = value; }
    public int T { get => t; set { t = value; Mathf.Clamp(t, 0, Enemies.Count); } }
    public Enemy EnemyTarget { get => enemyTarget; set { enemyTarget = value; } }
    public float RotateSpeed { get => rotateSpeed; set { rotateSpeed = value; Mathf.Clamp(value, 5, 8); } }

    public Enemy ClosestEnemy { get => closestEnemy; set => closestEnemy = value; }

    private void Awake() {
        //Player.attackModeUp += LockOnFuctionality;
        Enemy.onAnyDefeated += RemoveTheDead;
        Player.onPlayerDeath += RemoveAllEnemies;
        AIKryll.teleport += TeleportAttacking;
        GameController.onGameWasStarted += RemoveAllEnemies;
        GameController.returnToLevelSelect += RemoveAllEnemies;
        Player.findClosestEnemy += GetClosestEnemy;
        Player.playerIsLockedOn += Locked;
        Player.unlocked += Unlocked;
        CommandInputBehavior.stopMove += SetLock;
        CommandInputBehavior.resetMove += ResetLock;
        Dodge.stopMove += SetLock;
        Dodge.resetMove += ResetLock;
        UiManager.killAll += Cutscening;
        //UiManager.nullEnemies+=RemoveAllEnemies;
        //Dash.dashu += RemoveAllEnemies;
        T = 0;

    }
    private void Start() {

        pc = Player.GetPlayer();
        aimPoint = pc.AimmingPoint;
        leftPoint = Player.GetPlayer().LeftPoint;
    }
    private void RemoveTheDead(Enemy enemy) {
        Enemies.Remove(enemy);
    }
    private void RemoveAllEnemies() {
        Enemies.Clear();
    }
    private void Cutscening(bool val) {
        cutscening = val;
        ClosestEnemy = null;

    }
    private void Update() {//fucking hate update
        Vector3 position = transform.position;
        if (!cutscening) {
            for (int i = 0; i < Enemy.TotalCount; i++) {
                Enemy current = Enemy.GetEnemy(i);
                bool shouldBeInList = false;
                if (current != null) { shouldBeInList = Vector3.SqrMagnitude(current.transform.position - position) <= 361; }

                int index = enemies.IndexOf(current);
                if (shouldBeInList != index >= 0) {
                    if (shouldBeInList) {
                        enemies.Add(current);
                        if (enemies.Count > 1) {
                            GetClosestEnemy();

                        }

                    }
                    else { enemies.RemoveAt(index); }
                }
            }
        }
        if (pc.LockedOn) {
            SwitchLockOn();
            //pc.MoveSpeed = 3;

            if (enemies.Count == 0 && pc.CmdInput == 0) {
                BasicMovement();
            }
            else {
                GetInput();
            }

        }
        if (enemies.Count == 0) {
            ClosestEnemy = null;
        }
        if (t > enemies.Count && t > 0) {
            Debug.Log("wtf are you doing, imma move this down");
            T--;
        }
        if (T == Enemies.Count) {
            T = 0;
        }

        if (enemyTarget != null && !playing) {
            playing = true;
            casual = false;
            if (playBattleTheme != null) {
                playBattleTheme(8);
            }
        }
        if (enemyTarget == null && !casual) {
            playing = false;
            casual = true;
            if (playBattleTheme != null) {
                playBattleTheme(7);
            }

        }
    }

    private void Locked() { locked = true; }
    private void Unlocked() { locked = false; }
    private void GetInput() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (enemies.Count != 0 && t < enemies.Count) {

            LockOn(x, y, Enemies[T]);
        }
        //
        if (L2.GetButton()) {
            GetCombatMovement(x, y);

        }
        else {
            MovementInputs(x, y);

        }

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
        pc.BattleCamTarget.transform.position = EnemyTarget.transform.position;

    }
    private void GetClosestEnemy() {
        if (T < enemies.Count) {
            float enDist = EnDist(enemies[T].gameObject);

            foreach (Enemy en in Enemies) {
                ClosestEnemy = en;
                if (EnDist(en.gameObject) < enDist) {
                    T = Enemies.IndexOf(en);
                }
            }
        }
    }
    private void GetCombatMovement(float x, float y) {


        if (x == 0 && y == 0) {
            //Player.GetPlayer().CombatAnimations = 0;
        }
        if (x == 0) {
            Debug.Log("Combat Jump");
            if (y <= -0.3f) {
                Debug.Log("Combat BackJump");
                pc.CombatAnimations = 1;

            }
            if (y >= 0.3f) {
                Debug.Log("Combat BackJump");
                pc.CombatAnimations = 5;

            }
        }
        if (y == 0) {
            if (x <= -0.5f) {
                pc.CombatAnimations = 2;
            }
            if (x >= 0.5f) {
                pc.CombatAnimations = 3;
            }
        }


    }
    private float EnDist(GameObject target) => Vector3.Distance(target.transform.position, pc.transform.position);
    private void SetLock() {
        rotLock = true;
    }
    private void ResetLock() {
        rotLock = false;
    }
    private void BasicMovement() {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        RotateSpeed = 18 - EnDist(aimPoint);
        Vector3 delta = aimPoint.transform.position - pc.transform.position;
        delta.y = 0;
        if (!rotLock) {
            transform.rotation = Quaternion.LookRotation(delta, Vector3.up);

        }

        transform.position = Vector3.MoveTowards(transform.position, leftPoint.transform.position, pc.MoveSpeed * x * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, aimPoint.transform.position, pc.MoveSpeed * y * Time.deltaTime);
        MovementInputs(x, y);

    }

    private void LockOn(float x, float y, Enemy target) {

        //SwitchLockOn();
        LockOff();
        enemies[T].LockedOn = true;
        EnemyLockedTo();
        //if (!slide) { 


        if (Enemies[T] != null && !slide) {

            if (onLockOn != null) {
                onLockOn();
            }
            RotateSpeed = 18 - EnDist(target.gameObject);
            //Player.GetPlayer().Nav.enabled = true;
            Vector3 delta = target.transform.position - pc.transform.position;
            delta.y = 0;
            if (!rotLock) {
                transform.rotation = Quaternion.LookRotation(delta, Vector3.up);

            }

            //transform.LookAt(Enemies[T].transform.position,Vector3.up);
            transform.RotateAround(target.transform.position, target.transform.up, -x * rotateSpeed * pc.MoveSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, pc.MoveSpeed * y * Time.deltaTime);

        }
        if (Enemies[T].Dead) {
            GetClosestEnemy();
        }
    }

    private void MovementInputs(float x, float y) {
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

        }
        if (Mathf.Abs(x) >= 0.001 || Mathf.Abs(y) >= 0.001) {

            pc.Animations = 1;
        }
        else {

            pc.Animations = 0;
        }
    }

    private void SwitchLockOn() {
        if (Input.GetAxis("DPad Right") > 0 && dPadRight.GetButtonDown()) {
            T++;
            if (T == Enemies.Count) {
                T = 0;
            }
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
