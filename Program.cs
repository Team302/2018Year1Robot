
using System.Threading;

namespace yearone2018
{
    public class Program
    {
        private int AutonLoopCount = 0;
        private int LoopSec = 50;
        private int AutonTime = 30;
        public static void Main()
        {
            TeleopControl Telecontrol = new TeleopControl();

            AutonControl Autocontrol = new AutonControl();

            int AutonLoops = LoopSec * AutonTime;

            while (true)                      // loop forever 
            {
                if (m_controller.GetConnectionStatus() == CTRE.Phoenix.UsbDeviceConection.Connected)
                {
                    CTRE.Phoenix.Watchdog.Feed();

                    bool runauton = m_controller.GetButton(A_BUTTON);

                    if (runauton && AutonLoopCount < AutonLoops)
                    {
                        Autocontrol.Run();

                        AutonLoopCount ++ ;
                    }

                    else if (AutonLoopCount >= AutonLoops)
                    {
                        Telecontrol.Run();
                    }

                    System.Threading.Thread.Sleep(20);
                }

            }
        }
    }
}
