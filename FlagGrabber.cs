using System.Threading;

namespace yearone2018
{
    public class FlagGrabber
    {
        private const double ClosedGrabber = 9; //This will change depending on how FAB mounts the servo
        private const double OpenGrabber = 10; // This will change depending on how FAB mounts the servo
        public enum FlagGrabberSTATE //State
        {   //The enum
            ClosedGrabber, 
            OpenGrabber
        }

        private CANifier flag_grab;

        private FlagGrabber()

        {
            Robotmap theWholeShabang = Robotmap.GetInstance();
            flag_grab = new CANifier(theWholeShabang.GetFlagGrabber_ID);
        }

        private static FlagGrabber instance = null;
            public static FlagGrabber GetInstance()
            {
                if (instance == null)
                {
                    instance = new FlagGrabber();
                }
                return instance;
            }
        public void setState()
        {
            switch(setState) 
            {
                case ClosedGrabber:
                ClosedGrabber();
                break;
            case OpenGrabber:
                OpenGrabber();
                break;
            default:
                     // TODO put error out
                break;
             }
         }   
        private void ClosedGrabber()
        {
            FlagGraberServo.SetAngle(OpenGrabber); 
        }
        private void OpenGrabber()
        {
            FlagGrabberServo.SetAngle(ClosedGrabber); 
        } //FLAGSURVO LOOK LOOK LOOOK LOOK LOOK LOOK LOOK DO THIS FIX IT HE SHOWED YOU YOU GOTTA DO IT LLLLLLOOOOOOOOOOOK
    }       
}