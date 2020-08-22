using UnityEngine;
using UnityEngine.Events;
public class CharacterUI : MonoBehaviour
{
    [SerializeField]private Sprite pyra;
    [SerializeField] private Sprite petra;
    [SerializeField] private Sprite electra;
    [SerializeField] private Sprite rikka;

    public static event UnityAction<int> setElement;
    // Start is called before the first frame update
    void Start()
    {
        Player.dpadUp += SelectPyra;
        Player.dpadDown += SelectPetra;
        Player.dpadLeft += SelectElectra;
        Player.dpadRight += SelectRikka;
    }
    private void SelectPyra() {
        if (setElement != null) { 
        setElement(0);
        }
        SelectCharacter(pyra);
    }
    private void SelectPetra() {
        if (setElement != null) {
            setElement(1);
        }
        SelectCharacter(petra);
    }
    private void SelectElectra() {
        if (setElement != null) {
            setElement(2);
        }
        SelectCharacter(electra);
    }
    private void SelectRikka() {
        if (setElement != null) {
            setElement(3);
        }
        SelectCharacter(rikka);
    }
    private void SelectCharacter(Sprite character) {
        //Resize image
    }
}
