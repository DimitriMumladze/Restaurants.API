namespace Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string AtLeast20 = "AtLeast20";
}

public static class AppClaimTypes
{
    public const string Nationality = "HasNationality";
    public const string DateOfBirth = "DateOfBirth";
}