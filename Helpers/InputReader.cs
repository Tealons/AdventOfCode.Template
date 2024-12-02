using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Helper_Methods
{
    public static class InputReader
    {
        // Basic version that you can use by copy/pasting the input into this function
        public static string[] ReadInput(string input)
        {
            return input.Split("\r\n");
        }

        // This is the version that automatically gets the input via your session cookie if you've set that up
        public static string[] GetPuzzleInputLines(HttpClient client, string session, int year, int dayNumber)
        {
            var input = GetPuzzleInputAsync(client, session, year, dayNumber);
            return input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        }

        /*
         * SESSION Cookie
         *  These are valid for a month, so you will only have to retrieve it once, preferably on the 30th of November
         *  How to get the cookie:
            - Login on AoC with github or whatever
            - Open browser's developer console (e.g. right click --> Inspect) and navigate to the Network tab
            - GET any input page, say adventofcode.com/2023/day/1/input, and look in the request headers. 
        */
        public static string GetPuzzleInputAsync(HttpClient client, string session, int year, int dayNumber)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://adventofcode.com/{year}/day/{dayNumber}/input");
            request.Headers.Add("Cookie", $"session={session}");

            var response = client.Send(request);

            var responseStream = new StreamReader(response.Content.ReadAsStream());
            return responseStream.ReadToEnd();
        }

        // You will have to create your own appsettings.json file in this project. It's automatically included in the git ignore, hence the file is missing.
        public static IConfigurationRoot GetConfig()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).AddJsonFile("appsettings.local.json", true, true);
            return builder.Build();
        }
    }
}
