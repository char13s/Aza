using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 0649
public class Items : MonoBehaviour
{
    [SerializeField] internal ItemData data = new ItemData();


    public static UnityAction onItemClick;


    // Start is called before the first frame update
    private void Awake()
    {

        data.Start();

    }
    void Start()
    {

    }

    public void IconClick()
    {
        switch (data.Type)
        {
            
            default:
                DefaultItem();
                break;
        }
    }
    private void DefaultItem()
    {
        if (onItemClick != null)
        {

            onItemClick();
        }
        


    }
    


}

