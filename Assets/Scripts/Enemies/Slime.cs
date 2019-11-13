using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    

    // Start is called before the first frame update

    public override void Start()
    {
        base.Start();
        
    }
    
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        //anim.SetBool("Dead", Dead);
        //anim.SetBool("Hurt", Hit);
        //anim.SetBool("Attack", Attack);
        //anim.SetBool("Walking", Walk);

    }
    public override void Attacking()
    {
        base.Attacking();
    }
    void MegaSlime()
    {

    }

    
    //void CalculateDamage() { }
    void CalculateDefense() { }

}
