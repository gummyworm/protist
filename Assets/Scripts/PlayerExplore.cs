using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerExplore : MonoBehaviour {
	public Text areaText; 
	public Color areaTextColor;

	protected const float areaFadeTime = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		// if entering a new area, display the area name
		if (other.tag.Equals ("Area")) {
			Color fadeColor = areaTextColor;
			fadeColor.a = 0.0f;
			StartCoroutine(FadeText(areaText, fadeColor, areaTextColor, areaFadeTime));
		}
	}

	IEnumerator FadeText(Text text, Color c0, Color c1, float s) {
		text.enabled = true;
		for (float t = 0.0f; t < s; t += Time.deltaTime) {
			text.color = Color.Lerp (c0, c1, t / s);
			yield return null;
		}
		for (float t = 0.0f; t < s; t += Time.deltaTime) {
			text.color = Color.Lerp (c1, c0, t / s);
			yield return null;
		}
		text.enabled = false;
	}
}
