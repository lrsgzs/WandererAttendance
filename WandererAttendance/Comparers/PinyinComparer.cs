using System.Collections;
using System.Linq;
using WandererAttendance.Models;
using WandererAttendance.Models.Profile;
using WandererAttendance.Shared;

namespace WandererAttendance.Comparers;

public class PinyinComparer : IComparer
{
    public static readonly PinyinComparer Comparer = new();
    
    public int Compare(object? x, object? y)
    {
        if (x is Person p1 && y is Person p2)
        {
            return string.CompareOrdinal(
                PinyinHelper.GetFullPinyinList(p1.Name).FirstOrDefault(),
                PinyinHelper.GetFullPinyinList(p2.Name).FirstOrDefault());
        }

        if (x is PersonWithStatus ps1 && y is PersonWithStatus ps2)
        {
            return string.CompareOrdinal(
                PinyinHelper.GetFullPinyinList(ps1.Person.Name).FirstOrDefault(),
                PinyinHelper.GetFullPinyinList(ps2.Person.Name).FirstOrDefault());
        }
        
        if (x is string s1 && y is string s2)
        {
            return string.CompareOrdinal(
                PinyinHelper.GetFullPinyinList(s1).FirstOrDefault(),
                PinyinHelper.GetFullPinyinList(s2).FirstOrDefault());
        }
        
        return 0;
    }
}