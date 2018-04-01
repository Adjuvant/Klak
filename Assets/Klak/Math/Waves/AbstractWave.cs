//
// Klak - Utilities for creative coding with Unity
//
// Copyright (C) 2016 Keijiro Takahashi
//
// Klak Waves - Audio and control rate wave functions for Klak
// 
// Copyright (C) 2018, Thomas Deacon
//
// Klak Waves is a port of toxiclibs, Copyright (c) 2006-2011 Karsten Schmidt
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

/**
 * Abstract wave oscillator type which needs to be subclassed to implement
 * different waveforms. Please note that the frequency unit is radians, but
 * conversion methods to & from Hertz ({@link #hertzToRadians(float, float)})
 * are included in this base class.
 */
namespace Klak.Math.Waves
{
    public abstract class AbstractWave 
    {

        public static readonly float PI = 3.14159265358979323846f;
        public static readonly float TWO_PI = 2 * PI;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /**
		* Converts a frequency in Hertz into radians.
		* 
		* @param hz
		*            frequency to convert (in Hz)
		* @param sampleRate
		*            sampling rate in Hz (equals period length @ 1 Hz)
		* @return frequency in radians
		*/
        public static float hertzToRadians(float hz, float sampleRate)
        {
            return hz / sampleRate * TWO_PI;
        }

        /**
     * Converts a frequency from radians to Hertz.
     * 
     * @param f
     *            frequency in radians
     * @param sampleRate
     *            sampling rate in Hz (equals period length @ 1 Hz)
     * @return freq in Hz
     */
        public static float radiansToHertz(float f, float sampleRate)
        {
            return f / TWO_PI * sampleRate;
        }

        /**
         * Current wave phase
         */
        public float phase;
        public float frequency;
        public float amp;
        public float offset;
        public float value;

        protected float origPhase;
        protected Stack<WaveState> stateStack;

        public AbstractWave()
        {
        }

        /**
         * @param phase
         */
        public AbstractWave(float phase) : this(phase, 0, 1, 0) { }


        /**
         * 
         * @param phase
         * @param freq
         */
        public AbstractWave(float phase, float freq) : this(phase, freq, 1, 0) { }

        /**
         * @param phase
         * @param freq
         * @param amp
         * @param offset
         */
        public AbstractWave(float phase, float freq, float amp, float offset)
        {
            SetPhase(phase);
            this.frequency = freq;
            this.amp = amp;
            this.offset = offset;
        }

        /**
         * Ensures phase remains in the 0...TWO_PI interval.
         * 
         * @return current phase
         */
        public float CyclePhase()
        {
            phase %= TWO_PI;
            if (phase < 0)
            {
                phase += TWO_PI;
            }
            return phase;
        }

        /**
         * Progresses phase and ensures it remains in the 0...TWO_PI interval.
         * 
         * @param freq
         *            normalized progress frequency
         * @return update phase value
         */
        public float CyclePhase(float freq)
        {
            phase = (phase + freq) % TWO_PI;
            if (phase < 0)
            {
                phase += TWO_PI;
            }
            return phase;
        }

        public void Pop()
        {
            if (stateStack == null || (stateStack != null && stateStack.Count == 0))
            {
                throw new System.InvalidOperationException("no wave states on stack");
            }
            WaveState s = stateStack.Pop();
            phase = s.phase;
            frequency = s.frequency;
            amp = s.amp;
            offset = s.offset;
        }

        public void Push()
        {
            if (stateStack == null)
            {
                stateStack = new Stack<WaveState>();
            }
            stateStack.Push(new WaveState(phase, frequency, amp, offset));
        }

        /**
         * Resets the wave phase to the last set phase value (via
         * {@link #setPhase(float)}.
         */
        public void Reset()
        {
            phase = origPhase;
        }

        /**
         * Starts the wave from a new phase. The new phase position will also be
         * used for any later call to {{@link #reset()}
         * 
         * @param phase
         *            new phase
         */
        public void SetPhase(float phase)
        {
            this.phase = phase;
            CyclePhase();
            this.origPhase = phase;
        }

        /*
         * (non-Javadoc)
         * 
         * @see java.lang.Object#toString()
         */
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append(" phase: ").Append(phase);
            sb.Append(" frequency: ").Append(frequency);
            sb.Append(" amp: ").Append(amp);
            sb.Append(" offset: ").Append(offset);
            return sb.ToString();
        }

        /**
         * Updates the wave and returns new value. Implementing classes should
         * manually ensure the phase remains in the 0...TWO_PI interval or by
         * calling {@link #cyclePhase()}.
         * 
         * @return current (newly calculated) wave value
         */
        public abstract float Render();
    }
}
