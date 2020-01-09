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
    
    [SerializeField] private Text skillNameText;
    
    
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSkill()
    {
        Debug.Log("SetLastSelectedSkill works");
        if (sendSkill != null) {
            sendSkill(this);
        }
        //lastSkillSelected = this;
        Debug.Log(lastSkillSelected);
        
    }
    


}
