namespace Klak.Math.Waves
{
    /**
 * <p>
 * Frequency modulated bandwidth unlimited pure sawtooth wave. Uses a secondary
 * wave to modulate the frequency of the main wave.
 * </p>
 * 
 * <p>
 * <strong>Note:</strong> You must NEVER call the Render() method on the
 * modulating wave.
 * </p>
 */
    public class FMSawtoothWave : AbstractWave
    {
        public AbstractWave fmod;
    
    public FMSawtoothWave(float phase, float freq, AbstractWave fmod) : 
            base(phase, freq) {
        this.fmod = fmod;
    }
    
    /**
     * Convenience constructor to create a non frequency modulated sawtooth.
     * 
     * @param phase
     * @param freq
     *            base frequency (in radians)
     * @param amp
     * @param offset
     */
    public FMSawtoothWave(float phase, float freq, float amp, float offset) : 
            this(phase, freq, amp, offset, new ConstantWave(0)) {
    }
    
    public FMSawtoothWave(float phase, float freq, float amp, float offset, AbstractWave fmod) : 
            base(phase, freq, amp, offset) {
        this.fmod = fmod;
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
        value = (((((phase / TWO_PI) 
                    * 2f) 
                    - 1f) 
                    * amp) 
                    + offset);
        CyclePhase((frequency + this.fmod.Render()));
        return value;
    }
    }
}