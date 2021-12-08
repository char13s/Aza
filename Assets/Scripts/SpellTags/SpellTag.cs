using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Button))]
public class SpellTag : EquipmentObj
{
    [SerializeField] private int spellId;
    [SerializeField] private string spellName;
    [SerializeField] private int cooldown;
    [SerializeField] private int maxCooldown;
    public static event UnityAction<SpellTag> sendThisSpell;
    public static event UnityAction spellListDown;
    #region Skill related events
    public static event UnityAction triggerZaWarudo;
    #endregion

    private int quantity;
    #region Skill related parameters
    private bool timeStopped;
    #endregion
    //private bool throwingSpell;

    public int Quantity { get => quantity; set => quantity = value; }
    public string SpellName { get => spellName; set => spellName = value; }
    public int Cooldown { get => cooldown; set => cooldown = value; }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetSpellToSlot);
    }

    // Update is called once per frame
    public override void Circle() {
        base.Circle();
        if (triggerZaWarudo != null) {
            triggerZaWarudo();
        }
        Debug.Log("ZA WARUDO");
    }
    public void Activate() {
        if (cooldown == 0) {
            UseSpell();
        }
        
        //Quantity--;
    }
    private void SetSpellToSlot() {
        if (sendThisSpell != null) {
            sendThisSpell(this);
        }
        if (spellListDown != null)
        {
            spellListDown();
        }
    }
    private void UseSpell() {
        //Quantity--;
        switch (spellId) {
            case 0:
                if (triggerZaWarudo != null) {
                    triggerZaWarudo();
                }
                Debug.Log("ZA WARUDO");
                break;
            case 1:
                //TimeStop
                
                break;
            case 2:
                //seals enemy
                break;
            case 3:
                //turns Player invisible
                break;
            case 4:
                //paralysis enemy
                break;
            case 5:
                //Scares off weaker enemies
                break;
            case 6:
                //issa bomb
                break;
            case 7:
                //turns enemies gravity off
                break;
            case 8:
                //makes enemy wet
                break;
            case 9:
                //makes enemy poison
                break;
            case 10:
                //sets enemy on fire
                break;
            case 11:
                //freezes enemy
                break;
        }
    }
    /*#region Coroutines
    private IEnumerator ResetTimeStop() {
        YieldInstruction wait = new WaitForSeconds(2);
        yield return wait;
        timeStopped = false;
    }
    #endregion*/
}
