using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Spritify.Web.Controllers
{
    [ApiController]
    [Route("/")]
    public class UiController : ControllerBase
    {
        private const string ScriptSrcPattern = "(?<=<script.*src=\")[/A-Za-z0-9._-]*(?=\")";
        private const string LinkHrefPattern = "(?<=<link.*href=\")[/A-Za-z0-9._-]*(?=\")";

        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public UiController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        [HttpGet("{*url:nonfile}")]
        public IActionResult Index()
        {
            return File(
                "~index.html",
                "text/html",
                ReplaceScriptPaths,
                ReplaceStyleAndIconPaths);
        }

        private string ReplaceScriptPaths(string inputHtml)
        {
            return ReplacePath(inputHtml, ScriptSrcPattern);
        }

        private string ReplaceStyleAndIconPaths(string inputHtml)
        {
            return ReplacePath(inputHtml, LinkHrefPattern);
        }

        private string ReplacePath(string inputHtml, string pattern)
        {
            var basePath = configuration.GetValue<string>("PathBase");
            if (string.IsNullOrWhiteSpace(basePath))
            {
                return inputHtml;
            }

            var replacedHtml = Regex.Replace(inputHtml, pattern, match => PrependBasePath(basePath, match));

            return replacedHtml;
        }

        private static string PrependBasePath(string basePath, Match match)
        {
            var originalValue = match.Value;
            // the base path should never contain a trailing slash (by definition)
            var newValue = basePath + "/" + originalValue;

            return newValue;
        }

        private FileContentResult File(string virtualPath, string contentType, params Func<string, string>[] manipulators)
        {
            var physicalPath = Path.Combine(environment.WebRootPath, virtualPath.TrimStart('~'));

            string fileContent;
            using (var reader = new StreamReader(physicalPath, Encoding.UTF8))
            {
                fileContent = reader.ReadToEnd();
            }

            var manipulatedContent = manipulators.Aggregate(fileContent, (current, manipulator) => manipulator(current));
            var manipulatedContentBytes = Encoding.UTF8.GetBytes(manipulatedContent);
            return File(manipulatedContentBytes, contentType);
        }
    }
}