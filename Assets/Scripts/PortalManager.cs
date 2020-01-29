using UnityEngine;
using UnityEngine.Events;
public class PortalManager : MonoBehaviour
{
    [SerializeField] private GameObject basePortal;
    [SerializeField] private GameObject portalConnector0;
    
    public static event UnityAction<Vector3,bool> backToBase;
    // Start is called before the first frame update
    private void Awake()
    {
        UiManager.portal += OpenPortal;
    }
    
    private void OpenPortal(int portal) {
        switch (portal) {
            case 0:
                
                if (backToBase != null)
                {
                    backToBase(basePortal.transform.position,true);
                }
                break;
            case 1:
                
                if (backToBase != null)
                {
                    backToBase(portalConnector0.transform.position,false);
                }
                break;
        }
    }
}
