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