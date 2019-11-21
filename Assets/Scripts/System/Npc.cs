using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Npc : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private GameObject interactIcon;
    private byte currentBlock;
    [SerializeField] private DialogueBlock[] blocks;
    [SerializeField] private DialogueBlock[] defaultBlocks;
    public static UnityAction dialogueUp;
    public static UnityAction dialogueDown;
    [SerializeField] private int defaultBlock;
    private DialogueBlock presentBlock;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    public virtual void Start()
    {
        presentBlock = blocks[CurrentBlock];
    }

    // Update is called once per frame
    public virtual void Update()
    {
        PlayerIsInRange();
    }
    private float Distance => Vector3.Distance(Player.GetPlayer().transform.position, transform.position);

	public byte CurrentBlock { get => currentBlock; set => currentBlock = value; }

	private void PlayerIsInRange()
    {
        if (Distance < 2)
        {
            interactIcon.SetActive(true);
            transform.LookAt(Player.GetPlayer().transform.position);
            if (Input.GetButtonDown("X"))
            {
                GetDialogueUp();
                
                presentBlock.ReadLine();
                

                if (presentBlock.BlockIsDone)
                {
                    
                    
                    if (presentBlock.IsEndingBlock)
                    {
                        if (dialogueDown != null)
                        {
                            dialogueDown();
                        }
                        Debug.Log("default set");
                        presentBlock=defaultBlocks[defaultBlock];
                        //defaultBlocks[defaultBlock].BlockIsDone = false;
                        
                        
                    }
                    else {
                        presentBlock=blocks[++CurrentBlock];

                    }
                    

                    
                }
               
            }
        }
        
        else
        {
            interactIcon.SetActive(false);
        }

    }
    private void GetDialogueUp() {

        if (dialogueUp != null)
        {
            dialogueUp();
        }
    }
}
