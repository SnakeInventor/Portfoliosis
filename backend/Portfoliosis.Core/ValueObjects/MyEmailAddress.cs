// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.RegularExpressions;
using Portfoliosis.Core.Errors;
using Portfoliosis.Core.Errors.Email;

namespace Portfoliosis.Core.ValueObjects;

public record MyEmailAddress
{
    // According to email standards(RFC 5321 and RFC 5322),
    // the maximum length of an email address is 320 characters:
    // 64 for local part, 1 for "@", 255 for domain
    public const int MaxTotalLength = 320;
    private const int MaxLocalLength = 64;
    private const int MaxDomainLength = 255;
    private const double RegexTimeoutMilliseconds = 100;

    private string _localPart;
    private string _domainPart;

    public string Value => _localPart + '@' + _domainPart;
    private MyEmailAddress(string value)
    {
        if (value.Split('@') is not [var local, var domain])
        {
            //TODO: write concise message
            throw new ArgumentException();
        }

        _localPart = local;
        _domainPart = domain;
    }

    public static Result<MyEmailAddress> TryCreate(string email)
    {

        if (email.Length == 0)
        {
            return Result.Failure<MyEmailAddress>(new EmailEmptyError());
        }
        if (email.Length > MaxTotalLength)
        {
            return Result.Failure<MyEmailAddress>(new EmailTooLongError(MaxTotalLength, email.Length));
        }
        if (email.Split('@') is [var local, var domain])
        {
            if (local.Length == 0)
            {
                return Result.Failure<MyEmailAddress>(new LocalEmptyError());
            }

            if (domain.Length == 0)
            {
                return Result.Failure<MyEmailAddress>(new DomainEmptyError());
            }
            if (local.Length > MaxLocalLength)
            {
                return Result.Failure<MyEmailAddress>(new LocalTooLongError(MaxLocalLength, local.Length));
            }

            if (domain.Length > MaxDomainLength)
            {
                return Result.Failure<MyEmailAddress>(new DomainTooLongError(MaxDomainLength, domain.Length));
            }

            bool regexCheckFailed = !Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(RegexTimeoutMilliseconds));

            if (regexCheckFailed)
            {
                return Result.Failure<MyEmailAddress>(new EmailInvalidFormatError(email));
            }

            return new MyEmailAddress(email);

        }
        else
        {
            return Result.Failure<MyEmailAddress>(new EmailInvalidFormatError(email));
        }
    }

}
