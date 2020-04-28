using UnityEngine;
using UnityEngine.Events;
public class EventManager : MonoBehaviour
{
    public static event UnityAction endOfDeathIntro;
    public static event UnityAction<int> sceneChanger;
    public static event UnityAction<GameObject> setSpawner;
    [SerializeField]private GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        SceneDialogue.sendEndEvent += FindEvent;
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

        }
    }
}
