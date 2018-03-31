using UnityEngine;

namespace Klak.Math.Waves
{
    public class Wave2D {

        public AbstractWave xmod, ymod;
        public Vector2 pos;

        public Wave2D(AbstractWave x, AbstractWave y) {
            xmod = x;
            ymod = y;
            pos = new Vector2();
        }

        public void Render() {
            pos.x = xmod.Render();
            pos.y = ymod.Render();
        }
    }
}