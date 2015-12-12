using UnityEngine;
using System.Collections;

public class Segment : MonoBehaviour {
	protected AudioSource soundSrc;
	protected bool growing;

	// Use this for initialization
 	protected virtual void Start () {
		soundSrc = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// OnTouch is called when this segment is touched
	public virtual void OnTouch () {

	}

	// Grow is called when this segment is to grow
	public virtual void Grow () {
		growing = true;
	}

	public bool Growing() {
		return growing;
	}
}
