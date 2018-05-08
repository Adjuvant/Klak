using UnityEngine;
using Klak.Math;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Conversion/Float To Int Range")]
    public class FloatToIntRange : NodeBase
    {
        #region Editable properties

        [SerializeField]
        int _intOutput0 = 0;

        [SerializeField]
        int _intOutput1 = 127;

        #endregion

        #region Node I/O

        [Inlet]
        public float parameter
        {
            set { 
                if (!enabled) return;
                var i = BasicMath.Lerp(_intOutput0, _intOutput1, Mathf.Clamp01(value));
                _intEvent.Invoke((int)i);
            }
        }

        [SerializeField, Outlet]
        IntegerEvent _intEvent = new IntegerEvent();

        #endregion
    }
}
