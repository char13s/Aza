using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class ShootBehavior : StateMachineBehaviour
{
    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject flameTornado;
    private GameObject castingFlame;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        castingFlame = Player.GetPlayer().FireCaster;
        castingFlame.SetActive(true);

    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        castingFlame.SetActive(false);
        switch (Player.GetPlayer().SkillId)
        {
            case 1:
                Instantiate(fireball, animator.rootPosition, Quaternion.identity);
                fireball.transform.position = animator.rootPosition;
                break;
            case 2:
                Instantiate(explosion, Player.GetPlayer().BattleMode.Enemies[Player.GetPlayer().BattleMode.T].transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(flameTornado, animator.rootPosition, Quaternion.identity);
                flameTornado.transform.position = animator.rootPosition;

                //Player.GetPlayer().transform.position = Vector3.MoveTowards(Player.GetPlayer().transform.position, Player.GetPlayer().transform.position-new Vector3(),50f *Time.deltaTime );
                    break;

        }
        
        
        //Player.GetPlayer().SkillId = 0;

    }

}
