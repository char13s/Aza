using UnityEngine;
using UnityEngine.UI;
public class HitText : MonoBehaviour {
    private string text;
    [SerializeField]private Text counter;
    [SerializeField]private Canvas canvas;
    public string Text { get => text; set { text = value;counter.text = text; } }
    public HitText(string text) {
        this.text = text;
        Debug.Log("bruh");
    }
    private void Awake() {
        //counter = GetComponentInChildren<Text>();
        Debug.Log("bruh woke");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {   canvas.transform.rotation = Quaternion.LookRotation(transform.position - CameraLogic.PrespCam.transform.position);
        transform.localPosition += new Vector3(0,3,0)*Time.deltaTime;
    }
   
}
