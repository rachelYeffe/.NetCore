using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
namespace Middlware;
public class MyLogMiddlware{
    private readonly RequestDelegate next;
    private string filePath;
    public MyLogMiddlware(RequestDelegate next,IWebHostEnvironment webHost){
     this.filePath=Path.Combine(webHost.ContentRootPath, "Logs", "log.txt");
          using (var jsonFile = File.OpenText(filePath))
            {
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
            }
            this.next=next;
    }
public async Task Invoke(HttpContext c)
{
    var sw = new Stopwatch();
    sw.Start();
    await next.Invoke(c);
    File.AppendAllText(filePath, JsonSerializer.Serialize($"{c.Request.Path}.{c.Request.Method} took {sw.ElapsedMilliseconds}ms."
            + $" User: {c.User?.FindFirst("userId")?.Value ?? "unknown"}") + "\n");

}

}
public static partial class MiddlewareExtensions{
    public static IApplicationBuilder  UseMyLogMiddleware(this IApplicationBuilder builder )
    {
        return builder.UseMiddleware<MyLogMiddlware>();

    }
}
// using System;
// using System.IO;
// using System.Diagnostics;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using System.Text.Json;
// using System.Threading.Tasks;

// namespace Middleware
// {
//     public class MyLogMiddleware
//     {
//         private readonly RequestDelegate _next;
//         private readonly string _filePath;

//         public MyLogMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
//         {
//             _next = next;
//             _filePath = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "log.json");
//         }

//         public async Task Invoke(HttpContext context)
//         {
//             var sw = Stopwatch.StartNew();
//             await _next(context);
//             sw.Stop();

//             string logMessage = $"{context.Request.Path}.{context.Request.Method} took {sw.ElapsedMilliseconds}ms. User: {context.User?.FindFirst("userId")?.Value ?? "unknown"}";
//             File.AppendAllText(_filePath, JsonSerializer.Serialize(logMessage) + Environment.NewLine);

//             // Reading file content
//             string contents = File.ReadAllText(_filePath);
//             Console.WriteLine("File content after write: ");
//             Console.WriteLine(contents);
//         }
//     }

//     public static class MiddlewareExtensions
//     {
//         public static IApplicationBuilder UseMyLogMiddleware(this IApplicationBuilder builder)
//         {
//             return builder.UseMiddleware<MyLogMiddleware>();
//         }
//     }
// }
// using System;
// using System.IO;
// using System.Diagnostics;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using System.Text.Json;
// using System.Threading.Tasks;
// using System.Collections.Generic;

// namespace Middleware
// {
//     public class MyLogMiddleware
//     {
//         private readonly RequestDelegate _next;
//         private readonly string _filePath;
//         private List<string> _fileContent = new List<string>();

//         public MyLogMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
//         {
//             _next = next;
//             _filePath = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "log.json");

//             // Read file content and populate array if empty
//             if (File.Exists(_filePath))
//             {
//                 string[] lines = File.ReadAllLines(_filePath);
//                 _fileContent.AddRange(lines);
//             }
//         }

//         public async Task Invoke(HttpContext context)
//         {
//             var sw = Stopwatch.StartNew();
//             await _next(context);
//             sw.Stop();

//             string logMessage = $"{context.Request.Path}.{context.Request.Method} took {sw.ElapsedMilliseconds}ms. User: {context.User?.FindFirst("userId")?.Value ?? "unknown"}";
//             _fileContent.Add(JsonSerializer.Serialize(logMessage));

//             // Write array content to file
//             File.WriteAllLines(_filePath, _fileContent);

//             // Output file content
//             Console.WriteLine("File content after write: ");
//             foreach (var line in _fileContent)
//             {
//                 Console.WriteLine(line);
//             }
//         }
//     }

//     public static class MiddlewareExtensions
//     {
//         public static IApplicationBuilder UseMyLogMiddleware(this IApplicationBuilder builder)
//         {
//             return builder.UseMiddleware<MyLogMiddleware>();
//         }
//     }
// }
