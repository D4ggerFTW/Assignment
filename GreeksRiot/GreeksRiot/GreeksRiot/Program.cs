using System;

namespace GreeksRiot
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Riot game = new Riot())
            {
                game.Run();
            }
        }
    }
#endif
}

