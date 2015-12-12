using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Chain is a class that spawns a linear group of segments.
public class Chain : MonoBehaviour {
	public GameObject segment;	// the segment to compose the chain
	public int food; // the amount of food in reserve for the chain
	public int segmentCost; // the cost to grow one segment
	public int maxSize; // the maximum number of segments that this chain can contain
	
	protected List<Segment> segments;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (segments [segments.Count - 1].Growing ()) {
			return;
		}
		if (food > 0) {
			GameObject go = (GameObject)Instantiate(segment);
			Segment seg = go.GetComponent<Segment>();
			if (seg != null) {
				segments.Add (seg);
				food--;
			}
		}
	}

	// Feed gives the chain the given amount of food.
	public void Feed (int amnt) {
		food += amnt;
	}
}
