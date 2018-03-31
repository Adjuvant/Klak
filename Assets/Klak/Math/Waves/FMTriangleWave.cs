using Klak.Math;
using UnityEngine;

namespace Klak.Math.Waves
{
    public class FMTriangleWave : AbstractWave
    {
         public AbstractWave fmod;
    
    public FMTriangleWave(float phase, float freq) : 
            this(phase, freq, 1, 0) {
    }
    
    public FMTriangleWave(float phase, float freq, float amp, float offset) : 
            this(phase, freq, amp, offset, new ConstantWave(0)) {
    }
    
    public FMTriangleWave(float phase, float freq, float amp, float offset, AbstractWave fmod) : 
            base(phase, freq, amp, offset) {
        this.fmod = this.fmod;
    }
    
    public void Pop() {
        base.Pop();
        this.fmod.Pop();
    }
    
    public void Push() {
        base.Push();
        this.fmod.Push();
    }
    
    public void Reset() {
        base.Reset();
        this.fmod.Reset();
    }
    
    public override float Render() {
        value = ((2f 
                    * (amp 
                    * ((Mathf.Abs((PI - phase)) * MathUtils.INV_PI) 
                    - 0.5f))) 
                    + offset);
        CyclePhase((frequency + this.fmod.Render()));
        return value;
    }
    }
}