using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonKnight : Enemy
{
    [SerializeField] private GameObject origin;
    private int minions;
    private int animations;
    //private static Mage instance;
    //private SlimeSpawner ssp;
    private int timer;
    public int Animations { get => animations; set { animations = value; Anim.SetInteger("Animations", animations); } }

    public GameObject Origin { get => origin; set => origin = value; }

    //public static DemonKnight GetMage() => instance;
    public override void Awake() {
        base.Awake();
        //if (instance != null && instance != this) {
        //    Destroy(gameObject);
        //}
        //else {
        //    instance = this;
        //}
        //ssp = GetComponent<SlimeSpawner>();
        //Boss = true;
    }
    // Start is called before the first frame update
    public override void Start() {
        base.Start();
EnemyHitBoxBehavior[] hitBoxBehaviors = Anim.GetBehaviours<EnemyHitBoxBehavior>();
        for (int i = 0; i < hitBoxBehaviors.Length; i++)
            hitBoxBehaviors[i].HitBox = hitBox;

        EnemyHitBehavior[] hitBehaviors = Anim.GetBehaviours<EnemyHitBehavior>();
        for (int i = 0; i < hitBehaviors.Length; i++)
            hitBehaviors[i].Enemy = this;

        EnemyChaseBehavior[] chaseBehaviors = Anim.GetBehaviours<EnemyChaseBehavior>();
        for (int i = 0; i < chaseBehaviors.Length; i++)
            chaseBehaviors[i].Enemy = this;
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
        transform.rotation = Quaternion.LookRotation((transform.position - Player.GetPlayer().transform.position));
        ConditionalActions();
    }
    public override void Idle() {
        base.Idle();
    }
    private IEnumerator WaitCoroutine() {
        YieldInstruction wait = new WaitForSeconds(AttackDelay);
        yield return wait;
        Animations = 1;
    }
    private IEnumerator AttackWaitCoroutine() {
        YieldInstruction wait = new WaitForSeconds(15);
        yield return wait;
        Animations = 1;
    }
    private void ConditionalActions() {

        if (HealthLeft < (HealthLeft * 0.75f)) {



        }

        if (State == EnemyAiStates.Chasing) {
            //Chasing();


        }
    }
    private void StateController() {



    }
    //public override void Chasing() {
    //    Animations = 1;
    //
    //}
}
