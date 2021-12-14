using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInputs:MonoBehaviour {
    private ButtonInput button;

    public ButtonInput Button { get => button; set { button = value;DeleteSelf(); } }

    public enum ButtonInput
{ 
    Square,X,Triangle,Circle,Up,Down,Left,Right
}
    private void DeleteSelf() {
        Destroy(gameObject);
    }
}
