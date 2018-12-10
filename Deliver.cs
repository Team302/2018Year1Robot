using CTRE.Phoenix;
using Microsoft.SPOT;

namespace yearone2018
{
    public class Deliver //delivering the ball into the connect four box
    {
        private const float minPWMSignalRange = 553f;
        private const float maxPWMSignalRange = 2450f;
        private const float pwmOutput = 4200f;

        private static Deliver instance = null; // a singlex which will make it so we don't need to define deliver twice if we have it in auton and teleop instead have it once.
            public static Deliver GetInstance()
            {
                if (instance == null)
                {
                    instance = new Deliver();
                }
                return instance;
            }
        private const float DELIVER = 1.0f; // This will change depending on how FAB mounts the servo
        private const float HOLD = -1.0f; // This will change depending on how FAB mounts the servo
        private CANifier deliverServo; //What the servo is called
        public enum DELIVERSTATE //State
        {   //The enum
            HoldBalls, 
            Deliver
        }
        private DELIVERSTATE m_currentState; 
        private Deliver() //constructor same name as class name
        {   //Defining the servo
            Robotmap map = Robotmap.GetInstance();
            deliverServo = Robotmap.GETCANController();
            deliverServo.EnablePWMOutput((int)map.GetDeliverMec_ID(), true); 

            m_currentState = DELIVERSTATE.HoldBalls;
        }
        public DELIVERSTATE GetCurrentState() //current state
        {
            return m_currentState;
        }
        public void setState(DELIVERSTATE state) //allowing you to change the state/set state                                              
        {
            m_currentState = state;
        }
        public void Run()
        {
            switch(m_currentState) //This is telling us if we are going to Holdball, Deliver or default 
            {
                case DELIVERSTATE.HoldBalls:
                holdBalls();
                break;
            case DELIVERSTATE.Deliver:
                deliver();
                break;
            default:
                Debug.Print("deliver.setState called with invalid state");
                break;
             }
         }   
        private void holdBalls() 
        {
            Robotmap map = Robotmap.GetInstance();
            float pulses = LinearInterpolation.Calculate(HOLD, -1.0f, minPWMSignalRange, 1.0f, maxPWMSignalRange);
            float percentOut = pulses / pwmOutput;
            deliverServo.SetPWMOutput(map.GetFlagGrabberServo_ID(), pulses); //move servo        }
        }
        private void deliver()
        {
            Robotmap map = Robotmap.GetInstance();
            float pulses = LinearInterpolation.Calculate(DELIVER, -1.0f, minPWMSignalRange, 1.0f, maxPWMSignalRange);
            float percentOut = pulses / pwmOutput;
            deliverServo.SetPWMOutput(map.GetFlagGrabberServo_ID(), pulses); //move servo        
        }
    }       
}