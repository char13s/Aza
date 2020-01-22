using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Button))]
public class StoreItems : MonoBehaviour
{
    [SerializeField] private Items item;

    public static event UnityAction<int> pullUpQuantity;
    public static event UnityAction<ItemData> sendItem;
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Select);
    }
    void Start()
    {
        
    }
    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Select() {
        if (pullUpQuantity != null) {
            pullUpQuantity(3);
        }
        if (sendItem != null) {
            sendItem(item.data);
        }
    }
}
