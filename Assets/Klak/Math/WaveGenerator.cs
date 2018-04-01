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
using UnityEngine;
using System.Collections;
using Klak.Math.Waves;
using System;

namespace Klak.Math.Waves
{
    /// Holder of initial values
    public struct WaveGenerator
    {
        #region Nested Public Classes
        [Serializable]
        public struct ParameterData{
            [SerializeField]
            public float _phase;
            [SerializeField]
            public float _frequency;
            [SerializeField]
            public float _amplitude;
            [SerializeField]
            public float _offset;
            [SerializeField]
            public float _value;

            public ParameterData(float value): this(0,0,0,0){
                _value = value;
            }
            public ParameterData(float phase, float frequency, float amp, float offset){
                _phase = phase;
                _frequency = frequency;
                _amplitude = amp;
                _offset = offset;
                _value = 0;
            }
        }

        [Serializable]
        public class Config
        {
            public enum RateType
            {
                Control, Audio
            }

            public enum WaveType
            {
                AMFMSine, Constant, FMHarmonicSquare,
                FMSawtooth, FMSine, FMSquare,
                FMTriangle, Triangle, Sawtooth, 
                Sine, Square, Wave2D, 
                Disabled
            }

            [SerializeField]
            WaveType _waveType
            = WaveType.Sine;

            [SerializeField]
            RateType _rateType
            = RateType.Control;

            public WaveType waveType
            {
                get
                {
                    return _waveType;
                }

                set
                {
                    _waveType = value;
                }
            }

            public RateType rateType
            {
                get
                {
                    return _rateType;
                }

                set
                {
                    _rateType = value;
                }
            }

            public bool enabled
            {
                get { return waveType != WaveType.Disabled; }
            }

            public bool isControlRate
            {
                get { return rateType == RateType.Control; }
            }


            public ParameterData initialSettings { get; set; }

            public Config() { }

            public Config(RateType rate, WaveType type, float value)
            {
                _rateType = rate;
                _waveType = type;
                initialSettings = new ParameterData(value);
            }
            public Config(RateType rate, WaveType type, ParameterData d)
            {
                _rateType = rate;
                _waveType = type;
                initialSettings = d;
            }

            public static Config ControlConstant
            {
                get { return new Config(RateType.Control, WaveType.Constant, 0); }
            }

            public static Config ControlSine
            {
                get { return new Config(RateType.Control, WaveType.Sine, new ParameterData(0,0.1f,1,0)); }
            }

            public static Config AudioSine
            {
                get { return new Config(RateType.Audio, WaveType.Sine, new ParameterData(0, 440f, 1, 0)); }
            }
        }

        #endregion

        #region Private variables
        //AbstractWave osc;
        //float _phase;
        //float _frequency;
        //float _amp;
        //float _offset;
        //float _value;

        #endregion

        #region Constructors

        //public WaveGenerator(Config config, float phase, float frequency,
        //                    float amp, float offset, float value)
        //{
        //    this.config = config;

        //    if (config.waveType == Config.WaveType.Constant)
        //    {
        //        osc = new ConstantWave(value);
        //    }
        //    else if (config.waveType == Config.WaveType.Sine)
        //    {
        //        osc = new SineWave(phase, frequency,
        //                           amp, offset);
        //    }
        //    else{
        //        Debug.LogWarning("Osc null created");
        //        osc = null;
        //    }
        //}
        #endregion

        #region Public Properties And Methods
        //public Config config { get; set; }

        public static AbstractWave GenerateWave(Config _wave, ParameterData parameters){

            if (_wave.waveType == WaveGenerator.Config.WaveType.Constant)
            {
                return new ConstantWave(parameters._value);
            }
            else if (_wave.waveType == WaveGenerator.Config.WaveType.Sine)
            {
                return new SineWave(parameters._phase,
                                    parameters._frequency,
                                    parameters._amplitude,
                                    parameters._offset);
            }
            else if (_wave.waveType == WaveGenerator.Config.WaveType.Triangle)
            {
                return new FMTriangleWave(parameters._phase,
                                          parameters._frequency,
                                          parameters._amplitude,
                                          parameters._offset);
            }
            else if (_wave.waveType == WaveGenerator.Config.WaveType.Sawtooth)
            {
                return new FMSawtoothWave(parameters._phase,
                                          parameters._frequency,
                                          parameters._amplitude,
                                          parameters._offset);
            }
            else if (_wave.waveType == WaveGenerator.Config.WaveType.Square)
            {
                return new FMSquareWave(parameters._phase,
                                        parameters._frequency,
                                        parameters._amplitude,
                                        parameters._offset);
            }
            else
            {
                Debug.LogWarning("Osc null created");
                return null;
            }


        }

        public static AbstractWave GenerateWave(Config _wave, ParameterData parameters, AbstractWave fmod)
        {

            if (_wave.waveType == WaveGenerator.Config.WaveType.FMSine)
            {
                return new FMSineWave(parameters._phase,
                                      parameters._frequency,
                                      parameters._amplitude,
                                      parameters._offset, fmod);
            }
            else if (_wave.waveType == WaveGenerator.Config.WaveType.FMSquare)
            {
                return new FMSineWave(parameters._phase,
                                      parameters._frequency,
                                      parameters._amplitude,
                                      parameters._offset, fmod);
            }
            else if (_wave.waveType == WaveGenerator.Config.WaveType.FMSawtooth)
            {
                return new FMSineWave(parameters._phase,
                                      parameters._frequency,
                                      parameters._amplitude,
                                      parameters._offset, fmod);
            }
            else if (_wave.waveType == WaveGenerator.Config.WaveType.FMTriangle)
            {
                return new FMSineWave(parameters._phase,
                                      parameters._frequency,
                                      parameters._amplitude,
                                      parameters._offset, fmod);
            }
            else if (_wave.waveType == WaveGenerator.Config.WaveType.FMHarmonicSquare)
            {
                return new FMSineWave(parameters._phase,
                                      parameters._frequency,
                                      parameters._amplitude,
                                      parameters._offset, fmod);
            }
            else
            {
                Debug.LogWarning("Osc null created");
                return null;
            }


        }
        #endregion
    }
}