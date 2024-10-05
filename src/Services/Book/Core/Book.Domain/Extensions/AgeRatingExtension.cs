using Book.Domain.Enums;

namespace Book.Domain.Extensions;

public static class AgeRatingExtension
{
    public static string ToFormattedString(this AgeRating ageRating)
    {
        if (!Enum.IsDefined(typeof(AgeRating), ageRating))
        {
            throw new ArgumentOutOfRangeException(nameof(ageRating), "Invalid age rating value");
        }
        return $"{(byte)ageRating}+";
    }
}