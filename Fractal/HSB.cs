using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal
{
    class HSB
    {
        public float rChan, gChan, bChan;
        public HSB()
        {
            rChan = gChan = bChan = 0;
        }
        public void fromHSB(float h, float s, float b)
        {
            if (s == 0)
            {
                rChan = gChan = bChan = (int)(b * 255.0f + 0.5f);
            }
            else
            {
                h = (h - (float)Math.Floor(h)) * 6.0f;
                float f = h - (float)Math.Floor(h);
                float p = b * (1.0f - s);
                float q = b * (1.0f - s * f);
                float t = b * (1.0f - (s * (1.0f - f)));
                switch ((int)h)
                {
                    case 0:
                        rChan = (int)(b * 255.0f + 0.5f);
                        gChan = (int)(t * 255.0f + 0.5f);
                        bChan = (int)(p * 255.0f + 0.5f);
                        break;
                    case 1:
                        rChan = (int)(q * 255.0f + 0.5f);
                        gChan = (int)(b * 255.0f + 0.5f);
                        bChan = (int)(p * 255.0f + 0.5f);
                        break;
                    case 2:
                        rChan = (int)(p * 255.0f + 0.5f);
                        gChan = (int)(b * 255.0f + 0.5f);
                        bChan = (int)(t * 255.0f + 0.5f);
                        break;
                    case 3:
                        rChan = (int)(p * 255.0f + 0.5f);
                        gChan = (int)(q * 255.0f + 0.5f);
                        bChan = (int)(b * 255.0f + 0.5f);
                        break;
                    case 4:
                        rChan = (int)(t * 255.0f + 0.5f);
                        gChan = (int)(p * 255.0f + 0.5f);
                        bChan = (int)(b * 255.0f + 0.5f);
                        break;
                    case 5:
                        rChan = (int)(b * 255.0f + 0.5f);
                        gChan = (int)(p * 255.0f + 0.5f);
                        bChan = (int)(q * 255.0f + 0.5f);
                        break;
                }
            }
        }
    }


}
