using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#pragma warning disable 0649
public class PlayerBattleSceneMovement : MonoBehaviour {
    private List<Enemy> enemies = new List<Enemy>(16);
    private Player player;
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
    //private GameObject leftPoint;
    private bool rotLock;
    private bool cutscening;
    private bool playing;
    private bool casual; 
    private float rotationSpeed;
    public float RotationSpeed { get => rotationSpeed; set { rotationSpeed = value; Mathf.Clamp(value, 5, 8); } }
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

        player = Player.GetPlayer();
        aimPoint = player.AimmingPoint;
        //leftPoint = Player.GetPlayer().PlayerBody.LeftPoint;
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
    private void Update() {
        Vector3 position = transform.position;
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
        
        if (player.LockedOn) {
            if (enemies.Count == 0) {
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

        /*if (enemyTarget != null && !playing) {
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

        }*/
    }

    private void Locked() { locked = true; }
    private void Unlocked() { locked = false; }
    private void GetInput() {
        if (Enemies.Count != 0 && T < Enemies.Count) {

            LockOn(Enemies[T], player.DisplacementV.x, player.DisplacementV.y);
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
        player.CmdInput = 101;
    }
    private void EnemyLockedTo() {
        EnemyTarget = enemies[T]; //Enemy.GetEnemy(enemies.IndexOf(enemies[T])); stupid code -_-
        player.PlayerBody.BattleCamTarget.transform.position = EnemyTarget.transform.position;

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
                player.CombatAnimations = 1;
            }
            if (y >= 0.3f) {
                Debug.Log("Combat BackJump");
                player.CombatAnimations = 5;
            }
        }
        if (y == 0) {
            if (x <= -0.5f) {
                player.CombatAnimations = 2;
            }
            if (x >= 0.5f) {
                player.CombatAnimations = 3;
            }
        }
    }
    private float EnDist(GameObject target) => Vector3.Distance(target.transform.position, player.transform.position);
    private void SetLock() {
        rotLock = true;
    }
    private void ResetLock() {
        rotLock = false;
    }
    private void BasicMovement() {

        float x = player.DisplacementV.x;
        float y = player.DisplacementV.y;
        RotateSpeed = 18 - EnDist(player.PlayerBody.BattleCamTarget);
        Vector3 delta = player.PlayerBody.BattleCamTarget.transform.position;
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
        //transform.position = Vector3.MoveTowards(transform.position, aimPoint.transform.position, player.MoveSpeed * y * Time.deltaTime);
    }

    private void LockOn(Enemy target, float x, float y) {
        //SwitchLockOn();
        LockOff();
        enemies[T].LockedOn = true;
        EnemyLockedTo();
        //if (!slide) { 
        MovementInputs(x, y);

        if (Enemies[T] != null && !slide) {

            if (onLockOn != null) {
                onLockOn();
            }
            RotateSpeed = 18 - EnDist(target.gameObject);
            Vector3 delta = target.transform.position - player.transform.position;
            delta.y = 0;
            if (!rotLock) {
                transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
            }
            player.PlayerBody.FarHitPoint.transform.position = (Enemies[T].transform.position - transform.position) / 2;
            transform.RotateAround(target.transform.position, target.transform.up, -x * RotateSpeed * player.MoveSpeed * Time.deltaTime);

            if (y != 0) {
                Vector3 speed;
                speed = transform.forward * player.MoveSpeed * y;
            }
            if (Enemies[T].Dead) {
                GetClosestEnemy();
            }
        }
    }

    private void MovementInputs(float x, float y) {
        if (x == 0) {
            if (y > 0)//forward
            {
                player.Direction = 0;
            }

            if (y < 0)//back
            {
                player.Direction = 2;
            }
        }
        if (x > 0.3)//right
        {
            player.Direction = 3;
        }

        if (x < -0.3)//left
        {
            player.Direction = 1;

        }
        if (Mathf.Abs(x) >= 0.001 || Mathf.Abs(y) >= 0.001) { }

    }
}
