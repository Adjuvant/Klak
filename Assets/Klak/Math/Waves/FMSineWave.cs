using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Klak.Math.Waves
{
    public class FMSineWave : AbstractWave
    {
        public AbstractWave fmod;
    
    public FMSineWave(float phase, float freq, AbstractWave fmod) : 
            base(phase, freq) {
        this.fmod = fmod;
    }
    
    public FMSineWave(float phase, float freq, float amp, float offset) : 
            this(phase, freq, amp, offset, new ConstantWave(0)) {
    }
    
    public FMSineWave(float phase, float freq, float amp, float offset, AbstractWave fmod) : 
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
        value = (((float)((Mathf.Sin(phase) * amp))) + offset);
        CyclePhase((frequency + this.fmod.Render()));
        return value;
    }
    }
}