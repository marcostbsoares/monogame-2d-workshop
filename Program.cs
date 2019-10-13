using System;

namespace Mono_VsCode
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ShipGame())
                game.Run();
        }
    }
}
