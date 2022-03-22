using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public float partitionIndx;
	public int numPartitions;
	public float[] aveMag;
	public int numDisplayedBins;
	private Vector3 baseScale = new Vector3(1f, 0.5f, 1f);
	void Start(){
		numPartitions = 8;
		aveMag = new float[numPartitions];
		partitionIndx = 0;
		numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)
	}

	void Update () {
		numPartitions = 6;
		aveMag = new float[numPartitions];
		partitionIndx = 0;
		numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)

		for (int i = 0; i < numDisplayedBins; i++) 
		{
			if(i < numDisplayedBins * (partitionIndx + 1) / numPartitions)
            {
				aveMag[(int)partitionIndx] += AudioPeer.spectrumData [i] / (512/numPartitions);
			}
			else
            {
				partitionIndx++;
				i--;
			}
		}

        //scale and bound the average magnitude.
		// values are usually close to 0, we magnify them for actual use
        for (int i = 0; i < numPartitions; i++)
        {
            aveMag[i] = aveMag[i] * 600;
			if(aveMag[i] < 0.5f){
				aveMag[i] = 0.5f; 
			} else if(aveMag[i] > 5){ aveMag[i] = 5f; }
        }

		// Set the middle of the EQ to always be "similar"
		if (gameObject.name == "Pickup (" + 0 + ")") {
			Vector3 nScale = new Vector3 (baseScale.x, aveMag[0], baseScale.z);
			if (nScale.y < 1) nScale.y = 0.5f * Random.Range(1.2f, 1.5f);
			transform.localScale = nScale;
		}
		// left node
		if (gameObject.name == "Pickup (" + 5 + ")") {
			Vector3 nScale = new Vector3 (baseScale.x, 0.5f * aveMag[0], baseScale.z);
			if (nScale.y < 1) nScale.y = 0.5f * Random.Range(1.2f, 1.5f);
			transform.localScale = nScale;
		}
		// right node
		if (gameObject.name == "Pickup (" + 6 + ")") {
			Vector3 nScale = new Vector3 (baseScale.x, 0.5f * aveMag[0], baseScale.z);
			if (nScale.y < 1) nScale.y = 0.5f * Random.Range(1.2f, 1.5f);
			transform.localScale = nScale;
		}

		// Sun
		if (gameObject.name == "Sun") {
			Vector3 nScale = new Vector3 (16, aveMag[0], baseScale.z);
			if (nScale.y < 5) nScale.y = 5f * Random.Range(1.1f, 1.3f);
			transform.localScale = nScale;
		}

		// Moving Clouds
		if (gameObject.name == "Cloud (" + 3 + ")") {
			transform.position += new Vector3(0, 2 * aveMag[0], 0) * Time.deltaTime;
			//Debug.Log(transform.position.y);
			if(transform.position.y >= 35){
				transform.position = new Vector3(-3, 0, 48);
			}
		}

		if (gameObject.name == "Cloud (" + 4 + ")") {
			transform.position += new Vector3(0, 2 * aveMag[0], 0) * Time.deltaTime;
			//Debug.Log(transform.position.y);
			if(transform.position.y >= 35){
				transform.position = new Vector3(16, 0, 48);
			}
		}

		if (gameObject.name == "Cloud (" + 5 + ")") {
			transform.position += new Vector3(0, 2.5f * aveMag[1], 0) * Time.deltaTime;
			//Debug.Log(transform.position.y);
			if(transform.position.y >= 35){
				transform.position = new Vector3(8, 0, 46);
			}
		}

		if (gameObject.name == "Cloud (" + 6 + ")") {
			transform.position += new Vector3(0, 2.5f * aveMag[1], 0) * Time.deltaTime;
			//Debug.Log(transform.position.y);
			if(transform.position.y >= 35){
				transform.position = new Vector3(-5, 0, 46);
			}
		}

        // Map the magnitudes to the visualizer.
		// sync 1,3 & 2, 4 together
		for (int i = 1; i < 3; i++)
		{
			if (gameObject.name == "Pickup (" + i + ")") {
				Vector3 nScale = new Vector3 (baseScale.x, aveMag[i], baseScale.z);
				if (nScale.y < 1) nScale.y = 0.5f * Random.Range(1.2f, 1.5f);
				transform.localScale = nScale;
			}
			if (gameObject.name == "Pickup (" + (i+2) + ")") {
				Vector3 nScale = new Vector3 (baseScale.x, aveMag[i], baseScale.z);
				if (nScale.y < 1) nScale.y = 0.5f * Random.Range(1.2f, 1.5f);
				transform.localScale = nScale;
			}
		}
	}
}

