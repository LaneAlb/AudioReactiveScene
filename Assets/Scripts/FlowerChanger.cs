using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerChanger : MonoBehaviour
{
	public float partitionIndx;
	public int numPartitions;
	public int index;
	public float[] aveMag;
	public int numDisplayedBins;
	public Mesh[] flowerMeshes;
	private GameObject[] flowersToChange;
	private Vector3 baseScale = new Vector3(5.0f, 0.5f, 1f);
	private float timer;
	void Start(){
		timer = 0f;
		numPartitions = 8;
		aveMag = new float[numPartitions];
		partitionIndx = 0;
		numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)
		index  = 1;
		flowersToChange = GameObject.FindGameObjectsWithTag("Flowers");
	}

	void Update () {
		numPartitions = 6;
		aveMag = new float[numPartitions];
		partitionIndx = 0;
		numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)

		for (int i = 0; i < numDisplayedBins; i++) {
			if(i < numDisplayedBins * (partitionIndx + 1) / numPartitions) {
				aveMag[(int)partitionIndx] += AudioPeer.spectrumData [i] / (512/numPartitions);
			}
			else
            { partitionIndx++; i--;	}
		}

        //scale and bound the average magnitude.
		// values are usually close to 0, we magnify them for actual use
        for (int i = 0; i < numPartitions; i++)
        {
            aveMag[i] = aveMag[i] * 500;
			if(aveMag[i] < 0.5f){
				aveMag[i] = 0.5f; 
			}
        }

		if(aveMag[3] > 0.5 && timer > 1f){
			//Debug.Log("Change Flowers");
			// bkg.swapBackground(index);
			foreach(GameObject gObj in flowersToChange){
				gObj.GetComponent<MeshFilter>().mesh = flowerMeshes[index];
			}
			if(index >= 2){ index = 0; } else {index += 1;}
			timer = 0.0f;
		} else { timer += Time.deltaTime; }
	}
}
