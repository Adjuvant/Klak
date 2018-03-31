namespace Klak.Math.Waves
{
    /**
 * Implements a constant value as waveform.
 * Can be used to turn fm oscilators into plain waves.
 */
    public class ConstantWave : AbstractWave
    {
        public ConstantWave(float value) : 
            base() {
            this.value = value;
        }
    
        public override float Render() {
            return value;
        }
    }
}