using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Dialogue : MonoBehaviour
{
	[SerializeField] private byte lineAmount;
	[SerializeField] private string[] lines;
	[SerializeField] private byte numberOfDifferentPhrases;
	[SerializeField] private byte[] endingLineOfEachPhrase;
	[SerializeField] private Text theTextBox;

	public static UnityAction dialogueUp;
	// Start is called before the first frame update
	private void Awake() {
		lines = new string[lineAmount];
		endingLineOfEachPhrase = new byte[numberOfDifferentPhrases];
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
