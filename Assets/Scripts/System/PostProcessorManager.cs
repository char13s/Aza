using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
    
    public class PostProcessorManager : MonoBehaviour{

    [SerializeField] private PostProcessProfile forestProfile;
    [SerializeField] private PostProcessProfile graveyardProfile;
    [SerializeField] private PostProcessProfile darkEndProfile;
    [SerializeField] private GameObject forestProcessor;
    [SerializeField] private GameObject graveyardProcessor;
    [SerializeField] private GameObject darkEndProcessor;
    private static PostProcessorManager instance;

	private bool timeStop;
    public PostProcessProfile ForestProfile { get => forestProfile; set => forestProfile = value; }
    public PostProcessProfile GraveyardProfile { get => graveyardProfile; set => graveyardProfile = value; }
    public GameObject ForestProcessor { get => forestProcessor; set => forestProcessor = value; }
    public GameObject GraveyardProcessor { get => graveyardProcessor; set => graveyardProcessor = value; }
    public GameObject DarkEndProcessor { get => darkEndProcessor; set => darkEndProcessor = value; }
    public PostProcessProfile DarkEndProfile { get => darkEndProfile; set => darkEndProfile = value; }

    public static PostProcessorManager GetProcessorManager()=>instance.GetComponent<PostProcessorManager>();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        VirtualCameraManager.grey += TimeSlow;
        VirtualCameraManager.ungrey += Default;
		Player.zaWarudo += ZaWarudo;
        //Player.timeHasBegunToMove += Default;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void ZaWarudo() {
		ForestProfile.GetSetting<ColorGrading>().hueShift.value = -60;
		timeStop = true;
		StartCoroutine(Wait());
	}
	private IEnumerator Wait() {
		YieldInstruction wait = new WaitForSeconds(4);
		yield return wait;
		timeStop = false;
        Default();
	}
    public void Default() {
		if (!timeStop) { 
        ForestProfile.GetSetting<ColorGrading>().hueShift.value = 0;
       // DarkEndProfile.GetSetting<ColorGrading>().saturation.value = 12.7f;
			//ForestProfile.GetSetting<ColorGrading>().contrast.value = 81;
		}
    }
    public void Transformation() {
        ForestProfile.GetSetting<ColorGrading>().hueShift.value = -60;
    }
    private void TimeSlow() {
        //DarkEndProfile.GetSetting<ColorGrading>().saturation.value = 0;
        ForestProfile.GetSetting<ColorGrading>().hueShift.value = -60;
		ForestProfile.GetSetting<ColorGrading>().contrast.value = -16;
        Debug.Log("wtf");
    }
    

}
