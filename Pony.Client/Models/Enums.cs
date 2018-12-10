using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Pony.Client.Models
{
    public static class PonyEnumHelper
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()?
                            .GetMember(enumValue.ToString())?
                            .First()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name;
        }
    }
    public enum Difficulty
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10
    }
    public enum PonyName
    {
        [Display(Name = "Twilight Sparkle")]
        Twilight = 0,
        [Display(Name = "Applejack")]
        Applejack = 1,
        [Display(Name = "Fluttershy")]
        Fluttershy = 2,
        [Display(Name = "Rarity")]
        Rarity = 3,
        [Display(Name = "Pinkie Pie")]
        Pinkie = 4,
        [Display(Name = "Rainbow Dash")]
        Rainbow = 5,
        [Display(Name = "Spike")]
        Spike = 6
    }
}