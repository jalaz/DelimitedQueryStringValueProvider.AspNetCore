using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DelimitedQueryStringValueProvider
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class DelimitedQueryStringAttribute : Attribute, IResourceFilter
    {
        private readonly char[] delimiters;

        public DelimitedQueryStringAttribute(params char[] delimiters)
        {
            this.delimiters = delimiters;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.ValueProviderFactories.AddDelimitedValueProviderFactory(delimiters);
        }
    }
}
