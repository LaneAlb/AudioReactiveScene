using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public float partitionIndx;
	public int numPartitions;
	public float[] aveMag;
    public GameObject[] cloud;
	public int numDisplayedBins;
	void Start(){
		numPartitions = 8;
		aveMag = new float[numPartitions];
		partitionIndx = 0;
		numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)
        cloud = GameObject.FindGameObjectsWithTag("Clouds");
    }

    // Update is called once per frame
    void Update(){
        numPartitions = 3;
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
            aveMag[i] = aveMag[i] * 250;
			if(aveMag[i] < 0.8f){
				aveMag[i] = 0.8f; 
			} else if (aveMag[i] > 1f){ aveMag[i] = 1f;}
			
        }

        for(int i = 0; i < cloud.Length; i++){
			cloud[i].transform.localScale = new Vector3(aveMag[0], aveMag[i], aveMag[0]);
        }

    }
}
