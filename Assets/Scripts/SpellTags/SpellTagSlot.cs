using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Button))]
public class SpellTagSlot : MonoBehaviour
{
    public static event UnityAction spellInvent;
    public static event UnityAction<SpellTagSlot> sendThisTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AcessSpellInvent() {
        if (spellInvent != null) {
            spellInvent();
        }
        if (sendThisTag != null) {
            sendThisTag(this);
        }
    }
}
