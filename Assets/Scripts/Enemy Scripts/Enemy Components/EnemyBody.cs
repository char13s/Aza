using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
public class EnemyBody : MonoBehaviour
{
    [SerializeField] private Enemy body;
    [SerializeField] private Material bodyMat;
    [SerializeField] private Material attMat;
    [SerializeField] private Material attHeavyMat;
    [SerializeField] private Material attMagicMat;
    [SerializeField] private SkinnedMeshRenderer mesh;

    public static event UnityAction<float> force;
    private void OnTriggerEnter(Collider other) {
        Attacked(other);
        body.CalculateDamage(other.GetComponent<HitBox>().AdditionalDamage, other.GetComponent<HitBox>().Type);
        print("Hit");
        body.Hit = true;
        if (force != null) {
            force(50);
        }
        //body.KnockedUp();
    }
    private void Attacked(Collider other) {
        switch (other.GetComponent<HitBox>().Type) {
            case HitBoxType.Heavy:
                mesh.material = attHeavyMat;
                break;
            case HitBoxType.Magic:
                mesh.material = attMagicMat;
                break;
            default:
                mesh.material = attMat;
                break;

        }
        
        StartCoroutine(waitToReset());
    }
    IEnumerator waitToReset() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        mesh.material = bodyMat;
    }
}
