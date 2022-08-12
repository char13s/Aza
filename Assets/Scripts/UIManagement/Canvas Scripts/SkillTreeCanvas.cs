using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeCanvas : CanvasManager
{
    [SerializeField] private GameObject[] skillTrees;
    private int index;
    public override void AssignButtons() {
        PlayerInputs.close += CancelCanvas;
        PlayerInputs.turnPage += ChangeTree;
        PlayerInputs.pause += CancelCanvas;
    }
    public override void UnAssignButtons() {
        PlayerInputs.close -= CancelCanvas;
        PlayerInputs.turnPage -= ChangeTree;
        PlayerInputs.pause -= CancelCanvas;
    }
    private void ChangeTree(int val) {
        skillTrees[index].SetActive(false);
        index += val;
        AdjustIndex();
        skillTrees[index].SetActive(true);
    }
    private void AdjustIndex() {

        if (index > skillTrees.Length - 1) {
            index = 0;
        }
        else if (index < 0) {
            index = skillTrees.Length - 1;
        }
    }
}
