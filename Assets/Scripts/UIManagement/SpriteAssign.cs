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
	[SerializeField] private Sprite sword;
	private static Sprite swordImage;
	[SerializeField] private Sprite skull13;
	private static Sprite skull13Image;
    [SerializeField] private Sprite shield;
    private static Sprite shieldImage;

    void Start()
    {
        slimeImage = slime;
        woodImage = wood;
        lowHpPotionImage = lowHpPotion;
		swordImage = sword;
		skull13Image = skull13;
        shieldImage = shield;
    }

    public static Sprite SetImage(ItemData item)
    {
        switch (item.ID)
        {
            case 1:
                return slimeImage;
            case 2:
                return woodImage;
			case 3:
				return swordImage;
            case 4:
                return shieldImage;
			case 113:
				return skull13Image;
            case 100:
                return lowHpPotionImage;
        }
        return null;
    }
}
