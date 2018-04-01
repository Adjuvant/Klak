using UnityEngine;
using System.Collections;
using System;
using Klak.Math.Waves;


namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Oscillator/Wave")]
    public class Wave : NodeBase
    {        

        #region Editable properties

        [SerializeField]
        WaveGenerator.Config _wave = WaveGenerator.Config.ControlSine;

        [SerializeField]
        WaveGenerator.Config _fmWave = WaveGenerator.Config.ControlSine;

        [SerializeField]
        float _phase;
        [SerializeField]
        float _frequency;
        [SerializeField]
        float _amplitude;
        [SerializeField]
        float _offset;
        [SerializeField]
        float _value;

        [SerializeField]
        float _fmPhase;
        [SerializeField]
        float _fmFrequency;
        [SerializeField]
        float _fmAmplitude;
        [SerializeField]
        float _fmOffset;
        [SerializeField]
        float _fmValue;

        #endregion

        #region Node I/O

        [SerializeField, Outlet]
        FloatEvent _outputEvent = new FloatEvent();

        #endregion

        #region Private members

        AbstractWave _osc;
        AbstractWave _fmod;

        #endregion

        #region Public Properties And Methods
        public WaveGenerator.Config WaveSettings
        {
            get
            {
                return _wave;
            }

            set
            {
                _wave = value;
            }
        }
        #endregion

        #region MonoBehaviour functions

        void Start()
        {
            if (!_wave.enabled) return;
            if (!_wave.isControlRate)
                gameObject.AddComponent<AudioSource>();

            if(_wave.waveType.ToString().Contains("FM")){
                _fmod = WaveGenerator.GenerateWave(_fmWave,
                                                   new WaveGenerator.ParameterData(_fmPhase,_fmFrequency,_fmAmplitude,_fmOffset)
                                                  );
            }
            WaveGenerator.ParameterData data;
            if (_wave.waveType == WaveGenerator.Config.WaveType.Constant)
            {
                data = new WaveGenerator.ParameterData(_value);
            } 
            else 
            {
                data = new WaveGenerator.ParameterData(_phase,
                                    _frequency,
                                    _amplitude,
                                    _offset);
            }



            if (_wave.waveType.ToString().Contains("FM"))
            {
                _osc = WaveGenerator.GenerateWave(_wave,
                                                   data,
                                                  _fmod
                                                  );
            }
            else
            {
                _osc = WaveGenerator.GenerateWave(_wave,
                                                   data
                                                  );
            }
        }

        void Update()
        {
            if (_wave.enabled) 
            {
                if(_osc != null){
                    _osc.frequency = _frequency;
                    _osc.amp = _amplitude;
                    _osc.offset = _offset;
                }

                if(_fmod != null){
                    _fmod.frequency = _fmFrequency;
                    _fmod.amp = _fmAmplitude;
                    _fmod.offset = _fmOffset;              
                }
            }
            else
            {
                return;
            }

            if (_wave.isControlRate)
            {
                //Debug.Log("osc enabled");
                //_debug = ;

                _outputEvent.Invoke(_osc.Render());
            }
        }

        /// <summary>
        /// If OnAudioFilterRead is implemented, Unity will insert a custom filter into the
        /// audio DSP chain.
        /// </summary>
        /// <param name="data">An array of floats comprising the audio data.</param>
        /// <param name="channels">An int that stores the number of channels
        ///                        of audio data passed to this delegate.</param>
        void OnAudioFilterRead(float[] data, int channels)
        {
            if (_wave.isControlRate) return;

            int dataLen = data.Length / channels;

            int n = 0;
            while (n < dataLen)
            {
                int i = 0;
                while (i < channels)
                {
                    
                    data[n * channels + i] += _osc.Render();
                    i++;
                }
                n++;
            }
        }

        #endregion
    }
}
