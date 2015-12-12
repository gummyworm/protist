using UnityEngine;
using System.Collections;

public class Coccus : Segment {
	public AudioClip touchSound;
	public AnimationCurve growCurve;

	protected float growTime;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		growing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (growing) {
			growTime += Time.deltaTime;
			if(growTime >= growCurve.keys[growCurve.length-1].time) 	{
				growTime = growCurve.keys[growCurve.length-1].time;
				growing = false;
			}
			float s = growCurve.Evaluate(growTime);
			transform.localScale = new Vector3(s, s, s);
		}
	}

	public override void Grow ()
	{
		base.Grow ();
	}

	public override void OnTouch ()
	{
		soundSrc.clip = touchSound;		
		soundSrc.Play ();
	}
}
