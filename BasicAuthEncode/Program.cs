using System;
using System.Net.Http.Headers;
using System.Text;

namespace BasicAuthEncode
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string username = "abcdefg";
            string password = "zxcvbn£";

            if (args.Length == 2)
            {
                username = args[0];
                password = args[1];
            }


            string encoded = EncodeBasicAuth(username, password);
            Console.WriteLine($"Basic Auth for username: {username} password: {password} = {encoded}");
            var decoded = DecodeBasicAuth(encoded);
            Console.WriteLine($"Encoded: {encoded} decoded username: {decoded[0]} password: {decoded[1]} ");

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            
            if (!username.Equals(decoded[0]))
            {
                Console.WriteLine("usernames are not equal");
            }

            if (!password.Equals(decoded[1]))
            {
                Console.WriteLine("passwords are not equal");
            }
            Console.ResetColor();

            Console.WriteLine("Press any key to exit");

            Console.ReadLine();




        }



        static string EncodeBasicAuth(string UserName, string Password)
        {
            var byteArray = Encoding.UTF8.GetBytes($"{UserName}:{Password}");
            var clientAuthrizationHeader = new AuthenticationHeaderValue("Basic",
                                                          Convert.ToBase64String(byteArray));
            return clientAuthrizationHeader.ToString();
        }

        static string[] DecodeBasicAuth(string encoded )
        {
            var authHeader = AuthenticationHeaderValue.Parse(encoded);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            return credentials;
        }
    }
}
