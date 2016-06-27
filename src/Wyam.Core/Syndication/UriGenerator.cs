//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

using System;
using System.Globalization;
using System.Threading;

namespace Wyam.Core.Syndication
{
    class UriGenerator
    {
        long id;
        string prefix;

        public UriGenerator()
            : this("uuid")
        {
        }

        public UriGenerator(string scheme)
            : this(scheme, ";")
        {
        }

        public UriGenerator(string scheme, string delimiter)
        {
            if (scheme == null)
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("scheme"));

            if (scheme.Length == 0)
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString(SR.UriGeneratorSchemeMustNotBeEmpty), "scheme"));

            prefix = string.Concat(scheme, ":", Guid.NewGuid().ToString(), delimiter, "id=");
        }

        public string Next()
        {
            long nextId = Interlocked.Increment(ref id);
            return prefix + nextId.ToString(CultureInfo.InvariantCulture);
        }
    }
}