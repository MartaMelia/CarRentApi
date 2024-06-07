namespace CarRent.Domain.Common.Services;

public static class EnumTranslator
{
    public static string UserRoleTranslate(UserRole role) => role switch 
    {
        UserRole.Member => nameof(UserRole.Member),
        UserRole.Admin =>  nameof(UserRole.Admin),
        _ => string.Empty
    };
}
