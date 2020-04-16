using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LevelObject : MonoBehaviour
{
    public static event UnityAction<Sprite[],string,int> selectLevel;
    [SerializeField] private Sprite[] rewards;
    [SerializeField] private string details;
    [SerializeField] private int level;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(IconClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void IconClick() {
        if (selectLevel != null) {
            selectLevel(rewards,details,level);
        }
    }
}
