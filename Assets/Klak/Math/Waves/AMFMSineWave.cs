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

namespace Klak.Math.Waves
{
    
/**
 * <p>
 * Amplitude and frequency modulated sine wave. Uses 2 secondary waves to
 * modulate the shape of the main wave.
 * </p>
 * 
 * <p>
 * <strong>Note:</strong> You must NEVER call the Render() method on the
 * modulating waves.
 * </p>
 */
public class AMFMSineWave : AbstractWave {
    
    public AbstractWave fmod;
    
    public AbstractWave amod;
    
    /**
     * Creates a new instance from
     * 
     * @param phase
     * @param freq
     * @param fmod
     * @param amod
     */
    public AMFMSineWave(float phase, float freq, AbstractWave fmod, AbstractWave amod) : base(phase, freq) {
        this.amod = amod;
        this.fmod = fmod;
    }
    
    /**
     * @param phase
     * @param freq
     * @param offset
     * @param fmod
     * @param amod
     */
    public AMFMSineWave(float phase, float freq, float offset, AbstractWave fmod, AbstractWave amod) : base(phase, freq, 1f, offset) {
        this.amod = amod;
        this.fmod = fmod;
    }
    
    
    public void Pop() {
        base.Pop();
        this.amod.Pop();
        this.fmod.Pop();
    }
    
    
    public void Push() {
        base.Push();
        this.amod.Push();
        this.fmod.Push();
    }
    
    public void Reset() {
        base.Reset();
        this.fmod.Reset();
        this.amod.Reset();
    }


    /**
     * Progresses the wave and Renders the result value. You must NEVER call the
     * Render() method on the 2 modulating wave since this is handled
     * automatically by this method.
     * 
     * @see Klak.Math.Waves.AbstractWave#Render()
     */
    public override float Render() {
        amp = this.amod.Render();
        value = ( (amp * Mathf.Sin(phase)) + offset);
        CyclePhase(frequency + this.fmod.Render());
        return value;
    }
}
}