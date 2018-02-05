using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumberDisplay : MonoBehaviour {
	
	public int damage;
	public bool dodged;
	public float time = 1f;

	private float currentTime;
	//private GUIStyle styleOutline;
	private GUIStyle style;
	private Rect rN;
	private Camera cam;

	void Start() {
		cam = Camera.main;

		rN = new Rect(-32,-32,32,32);
		SetupPositions();

		style = new GUIStyle();
		style.fontSize = (int)(25 * Screen.height / 512f);
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;
	}

	void Update() {
		currentTime += Time.deltaTime;
		if (currentTime > time)
			Destroy(gameObject,time);
		rN.y -= 0.2f;
	}

	void SetupPositions() {

		float size = 16*Screen.height/512;
		
		rN = new Rect(cam.WorldToScreenPoint(transform.position),new Vector2(size,size));
		rN.yMin = Screen.height-rN.yMin;
		rN.yMax = Screen.height-rN.yMax;
		rN.xMin -= size*0.5f;
		rN.xMax -= size*0.5f;
	}

	void OnGUI() {
		if (dodged) {
			GUI.Label(rN, "Evaded", style);
		}
		else if (damage <= 0) {
			GUI.Label(rN, "Resisted", style);
		}
		else {
			GUI.Label(rN,damage.ToString(),style);
		}
	}
}
