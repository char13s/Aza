﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Text))]
public class SkillButton : MonoBehaviour
{
    private int mpRequired;
    private Skill skillAssigned;
    [SerializeField]private int skill;
    [SerializeField] private Text skillName;

    public static event UnityAction<SkillButton> sendSkillSlot;
    public int MpRequired { get => mpRequired; set => mpRequired = value; }
    public Skill SkillAssigned { get => skillAssigned; set { skillAssigned = value;SetSkill(); } }

    public Text SkillName { get => skillName; set => skillName = value; }

    private void Awake() {
        GameController.onQuitGame += NullSkill;
        GameController.onNewGame += NullSkill;
        GameController.onLoadGame += OnGameLoaded;
    }
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

        SkillName.text = skillAssigned.SkillName;
        MpRequired = skillAssigned.MpCost;
        skill = skillAssigned.SkillId;
    }
    private void OnGameLoaded() {
        skillAssigned.GetSkill(skill);
    }
    private void NullSkill() {
        skillAssigned = null;
    }
    private void GetSkill() {
        if (sendSkillSlot != null) {
            sendSkillSlot(this);
        }
    }
    public void UseSkill() {
        Player.GetPlayer().SkillId = skillAssigned.SkillId;
    }
}
