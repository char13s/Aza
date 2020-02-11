using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Button))]
public class SpellTagSlot : MonoBehaviour
{
    private SpellTag spell;
    [SerializeField]private Text spellName;

    public SpellTag Spell { get => spell; set => spell = value; }
    public Text SpellName { get => spellName; set => spellName = value; }

    public static event UnityAction spellInvent;
    public static event UnityAction<SpellTagSlot> sendThisSlot;
    // Start is called before the first frame update
    private void Awake() {
        
    }
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(AcessSpellInvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate() {
        if (spell != null) {
            Spell.Activate();
        }
        
        //CheckSpellTagQuantity();
        //Debug.Log("Spell Activated");
    }
    private void CheckSpellTagQuantity() {
        if (Spell.Quantity == 0) {
            Spell = null;
            SpellName.text = "none";
        }
    }
    
    private void AcessSpellInvent() {
        if (spellInvent != null) {
            spellInvent();
        }
        if (sendThisSlot != null) {
            sendThisSlot(this);
        }
    }
}
