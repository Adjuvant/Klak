using UnityEngine;

namespace Klak.Math.Waves
{
    public class SineWave : AbstractWave {
    
        public SineWave() : 
                base() {
        }
        
        public SineWave(float phase, float freq) : 
                base(phase, freq) {
        }
        
        public SineWave(float phase, float freq, float amp, float offset) : 
                base(phase, freq, amp, offset) {
        }
        
        public override float Render() {
            value = (((float)((Mathf.Sin(phase) * amp))) + offset);
            CyclePhase(frequency);
            return value;
        }
    }
}