using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FreeFallMovement : MonoBehaviour
{
    [SerializeField] private float gravity;
    private CinemachineVirtualCamera freeCam;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject camFocalPoint;
    private bool stun;
    private int stunTimer;
    private GameObject mainCam;
    private Vector3 direction;
    private Vector2 displacement;
    private Vector3 speed;
    private CharacterController charCon;
    public Vector3 Direction { get => direction; set => direction = value; }
    public Vector2 Displacement { get => displacement; set => displacement = value; }
    public CinemachineVirtualCamera FreeCam { get => freeCam; set => freeCam = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Gravity { get => gravity; set => gravity = value; }
    public bool Stun { get => stun; set => stun = value; }
    public int StunTimer { get => stunTimer; set { stunTimer = value; if (stunTimer <= 0) { Stun = false; } } }

    private void OnEnable() {
        SwitchToFallGame.switchCam += Vcam;
        ParaylsisPoints.stun += Stunned;
    }
    private void OnDisable() {
        SwitchToFallGame.switchCam -= Vcam;
    }
    // Start is called before the first frame update
    void Start() {
        mainCam = GameManager.GetManager().Camera;
        //transform.rotation = new Quaternion(87, 0, 0, 0);
        charCon = GetComponent<CharacterController>();
    }
    private void FixedUpdate() {
        CalculateMove();
        //charCon.Move(speed * Time.deltaTime);
        if(!Stun)
            charCon.Move(new Vector3(speed.x, -Gravity, -speed.y) * Time.deltaTime);
    }
    private void CalculateMove() {
        Direction = mainCam.transform.TransformDirection(new Vector3(Displacement.x, 0, Displacement.y).normalized);
        if (Displacement.magnitude >= 0.1f) {
            direction.z = 0;
            Vector3 vector = Direction.normalized;
            speed.x = MoveSpeed * vector.x;
            speed.y = MoveSpeed * vector.y;
        }
        else {
            speed.x = 0;
            speed.y = 0;
        }
    }
    private void Vcam(int val) {
        if (val > 1) {
            freeCam.m_Follow = camFocalPoint.transform;
            freeCam.m_LookAt = camFocalPoint.transform;
            freeCam.m_Priority = val;
        }
        else {
            freeCam.m_Priority = val;
        }
    }
    private void Stunned() {
        Stun = true;
        StunTimer = 10;
        StartCoroutine(waitToUnStun());
    }
    IEnumerator waitToUnStun() {
        YieldInstruction wait = new WaitForSeconds(1);
        while (isActiveAndEnabled & StunTimer>0) {
            yield return wait;
            StunTimer--;
        }
    }
}
