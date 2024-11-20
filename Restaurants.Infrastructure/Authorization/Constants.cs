namespace Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
}

public static class AppClaimTypes
{
    public const string HasNationality = "HasNationality";
    public const string DateOfBirth = "DateOfBirth";
}