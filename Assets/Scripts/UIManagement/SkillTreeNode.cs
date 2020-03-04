using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTreeNode : MonoBehaviour
{
    [SerializeField] private SkillTreeNode prev;
    [SerializeField] private SkillTreeNode next;
    [SerializeField]private int levelReq;
    [SerializeField] private Skill skill;
    [SerializeField] private Text skillInfo;
    // Start is called before the first frame update
    void Start() { 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
