using UnityEngine;

public class SpinAttackSetUp : StateMachineBehaviour
{
    [SerializeField] private GameObject spark;
     
    private Player pc;
    private float charge;

    public float Charge { get => charge; set { charge = Mathf.Clamp(value, 0, 100);if (charge >= 0.75) { Spark();if (charge >= 0.99) { pc.BoutaSpin = true; } } ; } }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.MoveSpeed = 0;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (Input.GetButton("Triangle")) {
            Charge += 0.05f;
        }

        if (Input.GetButtonUp("Triangle")) {
            pc.LightAttack = false;
            pc.BoutaSpin = false;
            if (charge > 0.99) {
                pc.SpinAttack = true;
            }
            Charge = 0;
        }
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Charge = 0;
        pc.BoutaSpin = false;
    }
    private void Spark() {
        Instantiate(spark,pc.DemonSword.transform);
    }
}
