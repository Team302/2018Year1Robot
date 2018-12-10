using CTRE.Phoenix;
using Microsoft.SPOT;
namespace yearone2018
{
    public class FlagGrabber
    {
        private const float minPWMSignalRange = 553f;
        private const float maxPWMSignalRange = 2450f;
        private const float pwmOutput = 4200f; 

        private const float CLOSEGRABBER =1.0f; //This will change depending on how FAB mounts the servo
        private const float OPENGRABBER = -1.0f; // This will change depending on how FAB mounts the servo
        private CANifier CANController;
        public enum FlagGrabberSTATE //State
        {   //The enum
            GRABBER_CLOSED, 
            GRABBER_OPEN
        }

        private FlagGrabberSTATE m_currentState;
        private FlagGrabber()

        {
            Robotmap theWholeShabang = Robotmap.GetInstance();
            CANController = Robotmap.GETCANController();
            CANController.EnablePWMOutput((int)theWholeShabang.GetFlagGrabberServo_ID(), true); //move servo

            m_currentState = FlagGrabberSTATE.GRABBER_OPEN;
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
        public FlagGrabberSTATE GetCurrentState()
        {
            return m_currentState;
        }
        public void setState(FlagGrabberSTATE grabber)
        {
            m_currentState = grabber;
        }   
        public void Run()
        {
            switch(m_currentState) 
            {
                case FlagGrabberSTATE.GRABBER_CLOSED:
                CloseGrabber();
                break;
            case FlagGrabberSTATE.GRABBER_OPEN:
                OpenGrabber();
                break;
            default:
                Debug.Print("FlagGrabber.setState called with invalid state");
                break;
             }
         }   
        private void CloseGrabber()
        {
            Robotmap map = Robotmap.GetInstance();
            float pulses = LinearInterpolation.Calculate(CLOSEGRABBER, -1.0f, minPWMSignalRange, 1.0f, maxPWMSignalRange);
            float percentOut = pulses / pwmOutput;
            CANController.SetPWMOutput(map.GetFlagGrabberServo_ID(), percentOut); //move servo
        }
        private void OpenGrabber()
        {
            Robotmap map = Robotmap.GetInstance();
            float pulses = LinearInterpolation.Calculate(OPENGRABBER, -1.0f, minPWMSignalRange, 1.0f, maxPWMSignalRange);
            float percentOut = pulses / pwmOutput;
            CANController.SetPWMOutput(map.GetFlagGrabberServo_ID(), pulses); //move servo
        } //FLAGSURVO LOOK LOOK LOOOK LOOK LOOK LOOK LOOK DO THIS FIX IT HE SHOWED YOU YOU GOTTA DO IT LLLLLLOOOOOOOOOOOK
    }       
}