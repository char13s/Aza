using UnityEngine;
using UnityEngine.Events;
public class EventManager : MonoBehaviour
{
    public static event UnityAction endOfDeathIntro;
    public static event UnityAction<int> sceneChanger;
    public static event UnityAction<GameObject> setSpawner;
    public static event UnityAction chooseSword;
    public static event UnityAction secondDeath;
    public static event UnityAction demonSpawning;
    public static event UnityAction demonWryed;
    [SerializeField]private GameObject spawn;
    [SerializeField] private GameObject firstDemon;
    [SerializeField] private GameObject firstDemonSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneDialogue.sendEndEvent += FindEvent;
        VirtualCameraManager.spawnDemon += SpawnFirstDemon;
        Portal.sendEvent += FindEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FindEvent(int events) {
        switch (events) {
            case 0:

                break;
            case 1:
                if (endOfDeathIntro != null) {
                    endOfDeathIntro();
                }
                if (sceneChanger != null) {
                    sceneChanger(3);//tutorial area
                }
                if (setSpawner != null) {
                    setSpawner(spawn);
                }
                break;
            case 2:
                if (chooseSword != null) {
                    chooseSword();
                }
                break;
            case 3:
                if (secondDeath != null) {
                    secondDeath();
                }
                break;
            case 4:
                if (demonSpawning != null) {
                    demonSpawning();
                }
                break;
            case 5:
                if (demonWryed != null) {
                    demonWryed();
                }
                break;
            case 6:
                Debug.Log("Okay so far so good...");
                break;
        }
    }
    private void SpawnFirstDemon() {
        
        Instantiate(firstDemon, firstDemonSpawn.transform.position, Quaternion.identity);
    }
}
