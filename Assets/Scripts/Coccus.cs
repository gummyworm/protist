using UnityEngine;
using System.Collections;

public class Coccus : Segment {
	public AudioClip touchSound;
	public AnimationCurve growCurve;
	public Color color;

	protected float growTime;
	protected float prevSize;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		growTime = 0.0f;

		if (GetComponent<Renderer>() != null) {
			GetComponent<Renderer>().material.color = color;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (growing) {
			growTime += Time.deltaTime;
			if(growTime >= growCurve.keys[growCurve.length-1].time) 	{
				growTime = growCurve.keys[growCurve.length-1].time;
				growing = false;
			}
			Resize ();
		}
	}

	void Resize() {
		float s = growCurve.Evaluate(growTime);
		transform.position += new Vector3(0, (s - prevSize)/2.0f, 0);
		transform.localScale = new Vector3(s, s, s);
		prevSize = s;
	}

	public override void Grow (bool adult) {
		prevSize = growCurve.Evaluate (0.0f);
		growing = !adult;
		if (adult) {
			growTime = growCurve.keys [growCurve.length - 1].time;
		} else {
			growTime = 0.0f;
		}
		Resize ();
	}

	public override void Link(Segment other) {
		float r1 = 0.0f, r2 = 0.0f;
		if (GetComponent<SphereCollider>() != null) {
			r1 = GetComponent<SphereCollider>().radius;
		}
		if (other.GetComponent<SphereCollider> () != null) {
			r2 = other.GetComponent<SphereCollider>().radius;
		}
		other.transform.localPosition = transform.position + new Vector3 (0, 2 * (r1 + r2), 0);
	}

	public override void OnTouch ()
	{
		soundSrc.clip = touchSound;		
		soundSrc.Play ();
	}
}
