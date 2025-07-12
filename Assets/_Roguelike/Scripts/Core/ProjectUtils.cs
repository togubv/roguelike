using UnityEngine;

namespace Roguelike
{
    public class ProjectUtils
    {
        public static bool CanTagDamage(string tag, string[] damageTags)
        {
            for (int i = 0; i < damageTags.Length; i++)
            {
                if (damageTags[i] == tag)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
