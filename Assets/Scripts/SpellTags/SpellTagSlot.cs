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
    private Text spellName;

    public SpellTag Spell { get => spell; set => spell = value; }

    public static event UnityAction spellInvent;
    public static event UnityAction<SpellTagSlot> sendThisSlot;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(AcessSpellInvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate() {
        Spell.Activate();
        //CheckSpellTagQuantity();
        Debug.Log("Spell Activated");
    }
    private void CheckSpellTagQuantity() {
        if (Spell.Quantity == 0) {
            Spell = null;
            spellName.text = "none";
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
