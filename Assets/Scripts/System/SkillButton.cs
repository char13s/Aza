using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]

public class SkillButton : MonoBehaviour
{
    private int mpRequired;
    private Skill skillAssigned;
    [SerializeField] private Text skillName;
    public int MpRequired { get => mpRequired; set => mpRequired = value; }
    public Skill SkillAssigned { get => skillAssigned; set => skillAssigned = value; }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GetSkill);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSkill() {
        

        UiManager.SkillAssignMenu.SetActive(false);
    }
    private void GetSkill() {

        SkillAssigned = Skill.LastSkillSelected;
        skillName.text = Skill.LastSkillSelected.SkillName;

        MpRequired = Skill.LastSkillSelected.MpCost;
    }
    public void UseSkill() {
        Player.GetPlayer().SkillId = skillAssigned.SkillId;
    }
}
