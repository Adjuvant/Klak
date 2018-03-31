using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Klak.Math.Waves;
public class TickWaves : MonoBehaviour {
	public List<AbstractWave> waves;
	// Use this for initialization
	void Start () {
		
	}
	
	/// <summary>
	/// If OnAudioFilterRead is implemented, Unity will insert a custom filter into the
	/// audio DSP chain.
	/// </summary>
	/// <param name="data">An array of floats comprising the audio data.</param>
	/// <param name="channels">An int that stores the number of channels
	///                        of audio data passed to this delegate.</param>
	void OnAudioFilterRead(float[] data, int channels)
	{		
        int dataLen = data.Length / channels;

		int n = 0;
        while (n < dataLen)
        {            
            int i = 0;
            while (i < channels)
            {
				foreach (AbstractWave w in waves){
					// Gain factor
					data[n * channels + i] += w.Render() * 0.25f;
				}
                i++;
            }
            n++;
        }
	}
}
