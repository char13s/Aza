using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpisodeList : CanvasManager
{
    public override void AssignButtons() {
        PlayerInputs.close += CloseList;
    }
    private void CloseList() { 
    
    }
}
