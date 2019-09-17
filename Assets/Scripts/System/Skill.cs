using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill : MonoBehaviour
{
    [SerializeField] private string skillName;
    [SerializeField] private int skillId;
    [SerializeField] private int mpCost;
    [SerializeField] private int level;
    [SerializeField] private int exp;
    [SerializeField] private int expRequired;
    [SerializeField] private Slider expBar;
    [SerializeField] private Text expText;
    [SerializeField] private Text expRequiredText;
    [SerializeField] private Text skillNameText;
    [SerializeField] private Button assign;
    [SerializeField] private Button vidButton;
    private bool unlocked;
    private static Skill lastSkillSelected;

    public static Skill LastSkillSelected { get => lastSkillSelected; set => lastSkillSelected = value; }
    public int MpCost { get => mpCost; set => mpCost = value; }
    public int SkillId { get => skillId; set => skillId = value; }
    public int Exp { get => exp; set => exp = value; }
    public string SkillName { get => skillName; set => skillName = value; }

    // Start is called before the first frame update
    void Start()
    {
        skillNameText.text = SkillName;
        expRequiredText.text = expRequired.ToString();
        expText.text = exp.ToString();
        assign.GetComponent<Button>().onClick.AddListener(SetLastSelectedSkill);
        vidButton.GetComponent<Button>().onClick.AddListener(SetTutorialRef);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetLastSelectedSkill()
    {
        Debug.Log("SetLastSelectedSkill works");
        lastSkillSelected = this;
        Debug.Log(lastSkillSelected);
        UiManager.SkillAssignMenu.SetActive(true);
    }
    private void SetTutorialRef() {
        switch (skillId) {
            case 1:
                UiManager.GetUiManager().FireBallTutorialUp();
                break;
            case 3:
                UiManager.GetUiManager().FlameTornadoTutorialUp();
                break;
            case 6:
                UiManager.GetUiManager().HeavySwingTutorialUp();
                break;

        }
    }


}
