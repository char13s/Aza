using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Text))]
public class Skill : MonoBehaviour
{
    [SerializeField] private string skillName;
    [SerializeField] private int skillId;
    [SerializeField] private int mpCost;
    [SerializeField] private int unlockLevel;
    [SerializeField] private Text skillNameText;
    [SerializeField] private CharacterData character;
    private bool unlocked;
    
    private static Skill lastSkillSelected;

    public static event UnityAction<Skill> sendSkill;
    public static Skill LastSkillSelected { get => lastSkillSelected; set => lastSkillSelected = value; }
    public int MpCost { get => mpCost; set => mpCost = value; }
    public int SkillId { get => skillId; set => skillId = value; }
    
    public string SkillName { get => skillName; set => skillName = value; }

    // Start is called before the first frame update
    void Start()
    {
        skillNameText.text = SkillName;
        GetComponent<Button>().onClick.AddListener(SetSkill);
        CharacterData.onLevel+=CheckToUnlock;
    }

    public Skill GetSkill(int skill) {
        if (skillId == skill) {
            return this;
        }
        return null;
    }
    public void SetSkill()
    {
        if (sendSkill != null) {
            sendSkill(this);
        }
    }
    private void CheckToUnlock() {
        if (!unlocked) {
            if (character.Level == unlockLevel) {
                unlocked = true;
            }
        }
    }
}
