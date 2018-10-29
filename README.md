# DelimitedQueryStringValueProvider.AspNetCore

A value provider that allows to specify multiple delimited values in a query string key

## Usage

Add `DeleteQueryString` attribute to a controller action and the values coming from GET parameter:

```
DELETE /api/values?ids=abd95f54-69f6-45e3-83b4-ea258d719948,9bb844bb-6573-45bf-8603-3bc58b9e6df4
```

will be split by a specified separator into an array of strings in action

```cs
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpDelete]
        [DelimitedQueryString(',')]
        public async Task<IActionResult> Delete(string[] ids)
        {
            ...
        }
    }
```