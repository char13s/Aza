using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Text))]
public class Objective : MonoBehaviour
{

    [SerializeField]private string title;
    [SerializeField]private string[] description;
    [SerializeField]private int rewardMoney;
    [SerializeField]private Items rewardItem;
    [SerializeField]private int rewardExp;
     private int currentDescription;
    [System.NonSerialized] private bool isActive;
    [System.NonSerialized] private bool completed;

    public bool IsActive { get => isActive; set => isActive = value; }
    public bool Completed { get => completed; set { completed = value;if (completed) { RewardPlayer(); if(UiManager.missionCleared!=null)UiManager.missionCleared(); } } }

    public string[] Description { get => description; set => description = value; }
    public int CurrentDescription { get => currentDescription; set { currentDescription = value; SetButton(); } }

    public static event UnityAction<string> onObjectiveClick;
     
    // Start is called before the first frame update
    public virtual void Start()
    {
        CurrentDescription = 0;
        GetComponent<Button>().onClick.AddListener(IconClick);
        GetComponent<Text>().text = title;
        Debug.Log("Started Obj");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
    public void IconClick() {
        if (onObjectiveClick != null) {
            onObjectiveClick(Description[currentDescription]);
        }
        //UiManager.GetUiManager().ObjectiveDescription(Description[currentDescription]);
        //Debug.Log("This occurs");
    }
    public void RewardPlayer()
    {
        Debug.Log("Mission 1 works completely");
        Player.GetPlayer().Money += rewardMoney;
        Player.GetPlayer().items.AddItem(rewardItem.data);
        Player.GetPlayer().stats.AddExp(rewardExp);
    }
    private IEnumerator WaitCoroutine() {
        YieldInstruction wait = new WaitForSeconds(1.4f);
        yield return wait;
        RewardPlayer();
    }
    public void Intializing()
    {
        GameController.update += Update;

    }
    private void TextSpecifics() {
        GetComponent<Text>().resizeTextForBestFit = true;
        GetComponent<Text>().color = Color.black;
        GetComponent<Text>().font = UiManager.GetUiManager().LuckiestGuy;
    }
    private void SetButton() {
        GetComponent<Button>().onClick.AddListener(IconClick);
    }
}
