using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Button))]
public class SkillTreeNode : MonoBehaviour
{
    [SerializeField] private SkillTreeNode prev;
    [SerializeField] private SkillTreeNode next;
    [SerializeField]private int levelReq;
    [SerializeField] private GameObject skill;
    [SerializeField] private Text skillInfo;
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(UnlockSkill);
    }
    // Start is called before the first frame update
    void Start() { 
        
    }

    // Update is called once per frame
    void Update(){
        
    }
    private void UnlockSkill() {
        skill.SetActive(true);
    }
}
