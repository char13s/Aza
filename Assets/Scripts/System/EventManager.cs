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
    public static event UnityAction<int> nextCam;
    public static event UnityAction demoRestart;
    public static event UnityAction demonForestUp;
    public static event UnityAction unloadDeathScene;

    [SerializeField]private GameObject spawn;
    [SerializeField] private GameObject firstDemon;
    [SerializeField] private GameObject firstDemonSpawn;

    [Header("objects refs")]
    [SerializeField] private GameObject firstDeath;
    [SerializeField] private GameObject secondDemoDeath;
    
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
                if (nextCam != null) {
                    nextCam(1000);
                }
                break;
            case 7:
                if (nextCam != null) {
                    nextCam(0);
                }
                break;
            case 8:
                if (demoRestart != null) {
                    demoRestart();
                }
                if (nextCam != null) {
                    nextCam(0);
                }
                break;
            case 9:
                if (demonForestUp != null) {
                    demonForestUp();
                }
                break;
            case 10:
                if (demoRestart != null) {
                    demoRestart();
                }
                if (unloadDeathScene != null) {
                    unloadDeathScene();
                }
                break;
        }
    }
    private void SpawnFirstDemon() {
        
        Instantiate(firstDemon, firstDemonSpawn.transform.position, Quaternion.identity);
    }
}
