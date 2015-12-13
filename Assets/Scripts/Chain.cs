using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Chain is a class that spawns a linear group of segments.
public class Chain : MonoBehaviour {
	public GameObject segment;	// the segment to compose the chain
	public int baseSize;	// the number of segments upon Start
	public int food; // the amount of food in reserve for the chain
	public int segmentCost; // the cost to grow one segment
	public int maxSize; // the maximum number of segments that this chain can contain
	
	protected List<Segment> segments;
	protected SphereCollider segCollider;
	protected float segRadius;
	protected bool repeatPulse = true;

	// Use this for initialization
	void Start () {
		segments = new List<Segment> ();
		for (int i = 0; i < baseSize; ++i) {
			AddSegment (true);
		}
		StartCoroutine (Pulse ());
	}
	void AddSegment (bool adult) {
		Segment prevSeg;
		if (segments.Count > 0) {
			prevSeg = segments [segments.Count - 1];
		} else {
			prevSeg = null;
		}

		GameObject go = (GameObject)Instantiate(segment, Vector3.zero, Quaternion.identity);
		go.transform.parent = transform;
		Segment seg = go.GetComponent<Segment>();

		if (seg != null) {
			seg.Grow (adult);
			segments.Add (seg);
		} 
		if (prevSeg != null) {
			prevSeg.Link(seg);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (segments [segments.Count - 1].Growing ()) {
			return;
		}
		if (food > 0) {
			AddSegment(false);
			food--;
		}
	}

	// Feed gives the chain the given amount of food.
	public void Feed (int amnt) {
		food += amnt;
	}

	public IEnumerator Pulse() {
		while (repeatPulse) {
			for (int i = 0; i < segments.Count; ++i) {
				yield return StartCoroutine (segments [i].Pulse ());
			}
		}
	}
}
