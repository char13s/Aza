using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class MagicComboExtend : SkillTreeNode
{
    public static UnityAction sendUpgrade;
    public override void OutsideEffect() {
        sendUpgrade.Invoke();
    }
    public override void UnlockSkill() {
        base.UnlockSkill();
        OutsideEffect();
    }
}
