using System.Collections.Generic;

namespace Weapons.Bullets.LazerTools
{
    public class DistanceComparer:IComparer<CharacterDistance>
    {
        public int Compare(CharacterDistance x, CharacterDistance y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            //TODO：Person类实例X与Y的比较规则
            //按姓名由小到大排列，姓名相同的人年龄大的在前
            
             if (x.Distance > y.Distance) return -1;
             if (x.Distance < y.Distance) return 1;
            return 0;
        }
    }
}