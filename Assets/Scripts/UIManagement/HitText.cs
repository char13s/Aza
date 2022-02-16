using UnityEngine;
using UnityEngine.UI;
public class HitText : MonoBehaviour
{
    private string text;
    private Player player;
    [SerializeField] private Text counter;
    [SerializeField] private Canvas canvas;
    public string Text { get => text; set { text = value; counter.text = text; } }
    public HitText(string text) {
        this.text = text;
        Debug.Log("bruh");
    }
    // Start is called before the first frame update
    void Start() {
        player = Player.GetPlayer();
        Destroy(gameObject, 0.5f);
    }
    // Update is called once per frame
    void Update() {
        Vector3 direction = player.MainCam.transform.position - transform.position;
        Quaternion qTo;
        qTo = Quaternion.LookRotation(direction);
        transform.rotation = qTo;
        transform.localPosition += new Vector3(0, 3, 0) * Time.deltaTime;
    }
}
