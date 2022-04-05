using Awarean.Sdk.Result;

namespace JwtAuthenticationServer;

public static class KnownErrors
{
    public static readonly Error USER_DOES_NOT_EXIST = GetError("INEXISTENT_USER", "The informed user does not exists within our records");

    private static Error GetError(string errorCode, string errorMessage) => Error.Create(errorCode, errorMessage);
}