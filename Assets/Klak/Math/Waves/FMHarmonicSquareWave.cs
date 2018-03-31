using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Klak.Math.Waves
{
    /**
 * <p>
 * Frequency modulated <strong>bandwidth-limited</strong> square wave using a
 * fourier series of harmonics. Also uses a secondary wave to modulate the
 * frequency of the main wave.
 * </p>
 * 
 * <p>
 * <strong>Note:</strong> You must NEVER call the Render() method on the
 * modulating wave.
 * </p>
 */
    public class FMHarmonicSquareWave : AbstractWave {
        
        public AbstractWave fmod;
        
        /**
     * Maximum harmonics to add (make sure you stay under Nyquist freq), default
     * = 9
     */
        public int maxHarmonics = 3;
        
        public FMHarmonicSquareWave(float phase, float freq, AbstractWave fmod) : 
                base(phase, freq) {
            this.fmod = this.fmod;
        }

        /**
     * Convenience constructor to create a non frequency modulated square wave
     * 
     * @param phase
     * @param freq
     *            base frequency (in radians)
     * @param amp
     * @param offset
     */ 
        public FMHarmonicSquareWave(float phase, float freq, float amp, float offset) : 
                this(phase, freq, amp, offset, new ConstantWave(0)) {
        }
        
        public FMHarmonicSquareWave(float phase, float freq, float amp, float offset, AbstractWave fmod) : 
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
        
        /**
     * Progresses the wave and Renders the result value. You must NEVER call the
     * Render() method on the 2 modulating wave since this is handled
     * automatically by this method.
     * 
     * @see Klak.Math.Waves.AbstractWave#Render()
     */
        public override float Render() {
            value = 0;
            for (int i = 1; (i <= this.maxHarmonics); i += 2) {
                value = (value + (1f 
                            / ((float)i * ((Mathf.Sin((i * phase)))))));
            }
            
            value = (value * amp);
            value = (value + offset);
            CyclePhase((frequency + this.fmod.Render()));
            return value;
        }
    }
}