
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class PlayerBattleSceneMovement : MonoBehaviour
{
    //Instance fields
    private List<Enemy> enemies = new List<Enemy>(16);

    private Player pc;

    private float x;
    private float y;
    private float z;
    [SerializeField] private GameObject wallDetector;
    [SerializeField] private GameObject battleCam;
    [SerializeField] private GameObject normalCamera;
    [SerializeField] private GameObject Canvas;
    private Coroutine loadCoroutine;
    private int t;//targeted enemy in the array of enemies
    [SerializeField] private GameObject backWallDetector;
    //Events



    //Properties
    public GameObject BattleCam { get => battleCam; set => battleCam = value; }
    public List<Enemy> Enemies { get => enemies; set => enemies = value; }

    public int T { get => t; set => t = value; }



    void Start()
    {
        Enemy.onAnyDefeated += RemoveTheDead;
        Player.onPlayerDeath += RemoveAllEnemies;
        GameController.onGameWasStarted += RemoveAllEnemies;
        T = 0;
        x = normalCamera.transform.rotation.x;
        y = normalCamera.transform.rotation.y;
        z = normalCamera.transform.rotation.z;

        pc = Player.GetPlayer();


        //enemies= new GameObject[5];
    }
    private void OnEnable()
    {

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
        //Debug.Log(enemies.Count);

        if (Enemies.Count > 0 && pc.Attacking)
        {
            SwitchLockOn();
            //wallDetector.SetActive(true);
            //ChangeView();
            BattleCam.SetActive(true);
            normalCamera.GetComponent<Camera>().enabled = false;
            Canvas.transform.SetParent(null);
            Canvas.transform.SetParent(BattleCam.transform);
            Canvas.transform.localPosition = new Vector3(0, 0, 0);
            GetInput();
        }
        else
        {
            //ChangeViewBack();
            //Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Canvas.transform.SetParent(null);
            Canvas.transform.SetParent(normalCamera.transform);
            Canvas.transform.localPosition = new Vector3(0, 0, 0);
            normalCamera.GetComponent<Camera>().enabled = true;
            BattleCam.SetActive(false);
            wallDetector.SetActive(false);
            normalCamera.transform.rotation = Quaternion.Euler(31.417f, y, z);
        }
        //CameraControls();

    }


    void ChangeView()
    {


    }
    void ChangeViewBack()
    {


    }

    void GetInput()
    {
        //AddEnemies();

        float x = Input.GetAxisRaw("Horizontal") * 20 * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * 60 * Time.deltaTime;
        float mH = Input.GetAxisRaw("MouseX");
        float jH = Input.GetAxisRaw("Camera");
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        if (Enemies.Count > 0)
        {

            LockOn(x, y, mH, jH);
        }
        if (pc.LeftDash)
        {
            //normalCamera.transform.RotateAround(enemies[t].transform.position, enemies[t].transform.up, 10 * Time.deltaTime);
            transform.RotateAround(Enemies[T].transform.position, Enemies[T].transform.up, 80 * Time.deltaTime);
        }
        if (pc.RightDash)
        {
            //normalCamera.transform.RotateAround(enemies[t].transform.position, enemies[t].transform.up, -10 * Time.deltaTime);
            transform.RotateAround(Enemies[T].transform.position, Enemies[T].transform.up, -80 * Time.deltaTime);
        }
    }

    void CameraControls()
    {
        if (Input.GetButtonDown("L1"))
        {
            if (!battleCam.activeSelf)
            {
                ChangeView();
            }
            else
            {
                Debug.Log("koayy");
                ChangeViewBack();
            }
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
            backWallDetector.SetActive(true);
            pc.Direction = 2;
        }
        else { backWallDetector.SetActive(false); }

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
        if (!Enemies[T].Dead) { 
            transform.rotation = Quaternion.LookRotation(Enemies[T].transform.position - pc.transform.position);
            if (!pc.Wall)
            {
                transform.RotateAround(Enemies[T].transform.position, Enemies[T].transform.up, -30 * x * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, Enemies[T].transform.position, 10 * y * Time.deltaTime);
            }
        }
    }

    public void SwitchLockOn()
    {

        /*if (Enemies[T].Dead)
        {
            Enemies.Remove(Enemies[T]);
        }*/

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
