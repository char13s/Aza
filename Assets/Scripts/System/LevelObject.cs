using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LevelObject : MonoBehaviour
{
    public static event UnityAction<Sprite[],string,int> selectLevel;
    public static event UnityAction<GameObject> setSpawn;
    [SerializeField] private Sprite[] rewards;
    [SerializeField] private string details;
    [SerializeField] private int level;
    [SerializeField] private GameObject levelSpawn;
    [SerializeField] private bool button;
    // Start is called before the first frame update
    void Start()
    {
        if (button) {
            GetComponent<Button>().onClick.AddListener(IconClick);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void IconClick() {
        if (selectLevel != null) {
            selectLevel(rewards,details,level);
        }
        if (setSpawn != null) {
            setSpawn(levelSpawn);
        }
    }
    public void ActivateLevel() {
        IconClick();
    }
}
