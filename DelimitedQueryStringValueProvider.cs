using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace DelimitedQueryStringValueProvider
{
    public class DelimitedQueryStringValueProvider : QueryStringValueProvider
    {
        private readonly CultureInfo culture;
        private readonly IQueryCollection queryCollection;

        public DelimitedQueryStringValueProvider(
            BindingSource bindingSource,
            IQueryCollection values,
            CultureInfo culture,
            char[] delimiters)
            : base(bindingSource, values, culture)
        {
            queryCollection = values;
            this.culture = culture;
            Delimiters = delimiters;
        }

        public char[] Delimiters { get; }

        public override ValueProviderResult GetValue(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var values = queryCollection[key];
            if (values.Count == 0)
            {
                return ValueProviderResult.None;
            }

            if (!values.Any(x => Delimiters.Any(x.Contains)))
                return new ValueProviderResult(values, culture);

            var stringValues = new StringValues(values
                .SelectMany(x => x.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries)).ToArray());
            return new ValueProviderResult(stringValues, culture);
        }
    }
}