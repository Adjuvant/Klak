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
namespace Klak.Math.Waves
{
    public class FMSquareWave : AbstractWave
    {
        public AbstractWave fmod;
    
    public FMSquareWave(float phase, float freq, AbstractWave fmod) : 
            base(phase, freq) {
        this.fmod = fmod;
    }
    
    public FMSquareWave(float phase, float freq, float amp, float offset) : 
            this(phase, freq, amp, offset, new ConstantWave(0)) {
    }
    
    public FMSquareWave(float phase, float freq, float amp, float offset, AbstractWave fmod) : 
            base(phase, freq, amp, offset) {
        this.fmod = fmod;
    }
    
    public new void Pop() {
        base.Pop();
        this.fmod.Pop();
    }
    
    public new void Push() {
        base.Push();
        this.fmod.Push();
    }
    
    public new void Reset() {
        base.Reset();
        this.fmod.Reset();
    }
    
    public override float Render() {
        value = phase / TWO_PI < 0.5 ? 1 : -1;
        value *= amp + offset;
        CyclePhase((frequency + this.fmod.Render()));
        return value;
    }
    }
}