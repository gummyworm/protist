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

	public bool move;	// if true, this chain will move to target
	public float speed; // the movement speed of the chain
	public Vector3 target;	// the target position this chain is to move toward

	protected List<Segment> segments;
	protected SphereCollider segCollider;
	protected float segRadius;
	protected bool repeatPulse = true;
	protected bool growing;

	// Use this for initialization
	void Start () {
		segments = new List<Segment> ();
		for (int i = 0; i < baseSize; ++i) {

		}
		StartCoroutine (Pulse ());
	}

	// Update is called once per frame
	void Update () {
		if (move) {
			move = false;
			//StartCoroutine(MoveTo (target));
		}
		if (growing) {
			return;
		}
		if (food > 0) {
			growing = true;
			StartCoroutine(AddSegment(false));
			food--;
		}
	}

	// Feed gives the chain the given amount of food
	public void Feed (int amnt) {
		food += amnt;
	}

	// Pulse calls each segments Pulse method (one after the next)
	public IEnumerator Pulse() {
		while (repeatPulse) {
			for (int i = 0; i < segments.Count; ++i) {
				yield return StartCoroutine (segments [i].Pulse ());
			}
			yield return null;
		}
	}

	// AddSegment adds a new segment to the chain
	IEnumerator AddSegment (bool adult) {
		Segment prevSeg;

		if (segments.Count > 0) {
			prevSeg = segments [segments.Count - 1];
		} else {
			prevSeg = null;
		}
		
		GameObject go = (GameObject)Instantiate(segment);
		go.transform.parent = transform;
		go.transform.localPosition = new Vector3 (0, 
		                                     -(go.transform.localScale.y * segments.Count), 
		                                     0);

		Segment seg = go.GetComponent<Segment>();

		if (seg != null) {
			seg.Grow (adult);
			segments.Add (seg);
		} 
		if (prevSeg != null) {
			prevSeg.Link(seg);
		}

		// wait for the segment to mature
		Vector3 basePos = transform.position;

		while (seg.Growing ()) {
			transform.position = basePos + new Vector3(0, seg.transform.localScale.y, 0);
			yield return null;
		}
		growing = false;
	}

	// MoveTo moves the chain's head to to
	public IEnumerator MoveTo(Vector3 to) {
		if (segments.Count <= 0) {
			return false;
		}
		while (Vector3.Distance(transform.position, to) > (Time.deltaTime * speed)) {
			transform.position = Vector3.MoveTowards(transform.position, to, Time.deltaTime * speed);
			// move the tail segments
			for (int i = 0; i < segments.Count - 1; ++i) {
				Segment s0 = segments[i];
				Segment s1 = segments[i+1];
				s1.transform.position = Vector3.MoveTowards(s1.transform.position, s0.transform.position, Time.deltaTime * speed);
			}
			yield return null;
		}
		transform.position = to;
	}
}
