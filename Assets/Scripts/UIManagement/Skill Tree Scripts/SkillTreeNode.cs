using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class SkillTreeNode : MonoBehaviour
{
    [SerializeField] private SkillTreeNode[] prev;
    [SerializeField] private SkillTreeNode[] next;
    [SerializeField] private int cost;
    private bool unlocked;
    GameManager gm;
    Button button;
    public static event UnityAction<int> sendOrbs;
    public bool Unlocked { get => unlocked; set => unlocked = value; }

    //[SerializeField] private GameObject skill;
    //[SerializeField] private Text skillInfo;
    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(UnlockSkill);
    }
    // Start is called before the first frame update
    void Start() {
        gm = GameManager.GetManager();
        if (!CheckPrevNodes()) {
            button.interactable=false;
        }
    }
    public void ButtonAvailable() {
        button.interactable = true;
    }
    public abstract void OutsideEffect();
    private bool CheckPrevNodes() {
        foreach (SkillTreeNode node in prev) {
            if (!node.Unlocked) {
                return false;
            }
            else {
                continue;
            }
        }
        return true;
    }
    public virtual void UnlockSkill() {
        if (CheckPrevNodes() && gm.OrbAmt >= cost) {
            Unlocked = true;
            sendOrbs.Invoke(gm.OrbAmt - cost);
            button.interactable = false;
            foreach (SkillTreeNode node in next) {
                node.ButtonAvailable();
            }
        }
    }
}
