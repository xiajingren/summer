using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Summer.Domain.Exceptions;

namespace Summer.Domain.Extensions
{
    public static class IdentityErrorExtensions
    {
        public static DetailError ToDetailError(this IdentityError identityError)
        {
            return new DetailError(identityError.Code, identityError.Description);
        }

        public static IEnumerable<DetailError> ToDetailErrors(this IEnumerable<IdentityError> identityErrors)
        {
            return identityErrors.Select(identityError => identityError.ToDetailError());
        }
    }
}