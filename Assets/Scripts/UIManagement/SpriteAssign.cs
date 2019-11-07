using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class SpriteAssign : MonoBehaviour
{
    [SerializeField] private Sprite slime;
    private static Sprite slimeImage;
    [SerializeField] private Sprite wood;
    private static Sprite woodImage;
    [SerializeField] private Sprite lowHpPotion;
    private static Sprite lowHpPotionImage;

    void Start()
    {
        slimeImage = slime;
        woodImage = wood;
        lowHpPotionImage = lowHpPotion;
    }

    public static Sprite SetImage(ItemData item)
    {
        switch (item.ID)
        {
            case 1:
                return slimeImage;
            case 2:
                return woodImage;
            case 100:
                return lowHpPotionImage;
        }
        return null;
    }
}
