using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PauseCanvas : CanvasManager
{
	// Start is called before the first frame update

	private void Awake() {

		GameManager.pauseScreen += CanvasControl;
		//SkillCanvas.backToPause += CanvasControl;
		//EquipmentCanvas.backToPause += CanvasControl;
	}
}
