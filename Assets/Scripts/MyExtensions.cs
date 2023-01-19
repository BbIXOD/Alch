using UnityEngine;

namespace MyExtensions
{
    public static class MyExtensions
    {
        public static Color SetAlpha(this Color col, int alpha)
        {
            return alpha == 1 ? SetAlpha(col, (float)alpha) :
                new Color(col.r, col.g, col.b, (float)alpha / 255);
        }
        
        public static Color SetAlpha(this Color col, float alpha)
        {
            return new Color(col.r, col.g, col.b, alpha);
        }

        public static Vector3 SetWidth(this Vector3 size, float height)
        {
            return new Vector3(height, size.y, size.z);
        }
    }
}