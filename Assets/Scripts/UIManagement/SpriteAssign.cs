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
	

	[SerializeField] private Sprite skull13;
	private static Sprite skull13Image;

    [SerializeField] private Sprite shield;
    private static Sprite shieldImage;

    [SerializeField] private Sprite empty;
    private static Sprite emptyImage;
    [SerializeField] private Sprite bow;
    
    private static SpriteAssign instance;

    public Sprite Bow { get => bow; set => bow = value; }
    public Sprite Sword { get => sword; set => sword = value; }

    public static SpriteAssign GetSprite() => instance.GetComponent<SpriteAssign>(); 
    void Start()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        slimeImage = slime;
        woodImage = wood;
        lowHpPotionImage = lowHpPotion;
		
		skull13Image = skull13;
        shieldImage = shield;
        emptyImage=empty;
    }

    public static Sprite SetImage(ItemData item)
    {
        switch (item.ID)
        {
            case 0:
                return emptyImage;
            case 1:
                return slimeImage;
            case 2:
                return woodImage;
			case 3:
				return null;
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
