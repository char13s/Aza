using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReminatFader : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMesh;
    private Mesh m3;
    [SerializeField] private ParticleSystemRenderer cloneSystem;
    [SerializeField] private float refreshRate;
    // Start is called before the first frame update
    private void Awake() {
        skinnedMesh = GetComponent<SkinnedMeshRenderer>();
    }
    void Start() {
        StartCoroutine(UpdateMesh());
    }
    private IEnumerator UpdateMesh() {
        YieldInstruction wait = new WaitForSeconds(refreshRate);
        while (isActiveAndEnabled) {
            Mesh m = new Mesh();
            skinnedMesh.BakeMesh(m);
            Vector3[] vertices = m.vertices;
            Mesh m2 = m;
            m2.vertices = vertices;
            m2.name = "Mesh3";
            cloneSystem.mesh = m2;
            yield return wait;
        }

    }
}
