using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PortalManager : MonoBehaviour
{
    [SerializeField] private GameObject basePortal;
    [SerializeField] private GameObject portalConnector0;

    public static event UnityAction<Vector3> backToBase;
    // Start is called before the first frame update
    private void Awake()
    {
        UiManager.portal += OpenPortal;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OpenPortal(int portal) {
        switch (portal) {
            case 0:
                if (backToBase != null)
                {
                    backToBase(basePortal.transform.position);
                }
                break;
            case 1:
                if (backToBase != null)
                {
                    backToBase(portalConnector0.transform.position);
                }
                break;


        }
    }
}
