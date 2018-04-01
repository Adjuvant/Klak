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