
using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using System.Threading;

namespace yearone2018
{
    public class Program
    {
        private static int AutonLoopCount = 0;
        private static int LoopSec = 50;
        private static int AutonTime = 30;
        public static void Main()
        {
            TeleopControl Telecontrol = new TeleopControl();

            AutonControl Autocontrol = new AutonControl();

            int AutonLoops = LoopSec * AutonTime;

            GameController controller = Telecontrol.GetController();

            bool okToRun = false;             // Can we run code or not
            while (true)                      // loop forever 
            {
                // If the USB controller isn't plugged in or if the stop button is pressed
                // it isn't ok to run the motors.  
                // If the USB is plugged in, use the start button to activate (it will stay 
                // that way unless the stop button is pressed or the USB becomes dislodged.
                if (controller.GetConnectionStatus() == UsbDeviceConnection.Connected)
                {
                    if ( controller.GetButton(Telecontrol.GetStartButton()))
                    {
                        okToRun = true;
                    }
                    else if ( controller.GetButton(Telecontrol.GetStopButton() ))
                    {
                        okToRun = false;
                    }
                }
                else
                {
                    okToRun = false;
                }

                if (okToRun)
                { 
                    Watchdog.Feed();

                    bool runauton = controller.GetButton( Telecontrol.GetAutonButton());

                    if (runauton && AutonLoopCount < AutonLoops)
                    {
                        Autocontrol.Run();

                        AutonLoopCount ++ ;
                    }

                    else
                    {
                        Telecontrol.Run();
                    }

                    Thread.Sleep(20);
                }
                else
                {
                    Telecontrol.CheckSensors();
                }

            }
        }
    }
}
