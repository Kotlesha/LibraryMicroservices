using Book.Domain.Enums;

namespace Book.Domain.Extensions;

public static class AgeRatingExtension
{
    public static string ToFormattedString(this AgeRating ageRating) => ageRating switch
    {
        AgeRating.ZeroPlus => "0+",
        AgeRating.SixPlus => "6+",
        AgeRating.TwelvePlus => "12+",
        AgeRating.SixteenPlus => "16+", 
        AgeRating.EighteenPlus => "18+",
        _ => throw new ArgumentOutOfRangeException(nameof(ageRating))
    };
}