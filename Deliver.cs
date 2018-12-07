using System.Threading;

namespace yearone2018
{
    public class Deliver //delivering the ball into the connect four box
    {
        private static Deliver instance = null; // a singlex which will make it so we don't need to define deliver twice if we have it in auton and teleop instead have it once.
            public static Deliver GetInstance()
            {
                if (instance == null)
                {
                    instance = new Deliver();
                }
                return instance;
            }
        private const double HOLDBALL=9; //This will change depending on how FAB mounts the servo
        private const double DELIVER=10; // This will change depending on how FAB mounts the servo
        private Servo deliverServo; //What the servo is called
        public enum DELIVERSTATE //State
        {   //The enum
            HoldBalls, 
            Deliver
        }
        private DELIVERSTATE m_currentState 
        private Deliver()
        {   //Defining the servo
            RobotMap Map = RobotMap.GetInstance();
            deliverServo = new Servo(Map.GetDeliverMec_ID());  // its the ID for the servo
            Hardware.canifier.EnablePWMOutput((int)Constants.kMotorControlCh, false); //TODO this needs to be changed when they mount on the servo
            m_currentState = DELIVERSTATE.Off
        }
        public void setState(DELIVERSTATE deliver)
        {
            m_current = deliver
            Run();
        }
        public void Run()
        {
            switch(m_currentState) //This is telling us if we are going to Holdball, Deliver or default
            {
                case HoldBalls:
                holdBalls();
                break;
            case Deliver:
                deliver();
                break;
            default:
                     // TODO put error out
                break;
             }
         }   
        private void holdBalls()
        {
            deliverServo.SetAngle(HOLDBALL); //When holding the ball this is what the servo will do 
        }
        private void deliver()
        {
            deliverServo.SetAngle(DELIVER); //When delivering the ball this is what the servo will do
        }
    }       
}