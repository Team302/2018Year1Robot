using System.Threading;

namespace yearone2018
{
    public class Deliver //delivering the ball into the conect four box
    {
        private const int CANID = 0; //TODO Change CANID
        private const double HOLDBALL=9; //This will change depending on how FAB mounts the servo
        private const double DELIVER=10; // This will change depending on how FAB mounts the servo
        // TODO: will be on a Canifier
        private Servo deliverServo; //What the servo is called
        public enum DELIVERSTATE //State
        {   //The enum
            HoldBalls, 
            Deliver
        }
        private Deliver()
        {   //Defining the servo
            deliverServo = new Servo(CANID); // TODO change CANID 
        }

        private static Deliver instance = null;
            public static Deliver GetInstance()
            {
                if (instance == null)
                {
                    instance = new Deliver();
                }
                return instance;
            }
        public void setState( DELIVERSTATE state )
        {
            switch(state) //This is telling us if we are going to Holdball, Deliver or default
            {
                case DELIVERSTATE.HoldBalls:
                HoldBalls();
                break;
            case DELIVERSTATE.Deliver:
                Deliver();
                break;
            default:
                     // TODO put error out
                break;
             }
         }   
        private void HoldBalls()
        {
            deliverServo.SetAngle(DELIVER); //When holding the ball this is what the servo will do 
        }
        private void Deliver()
        {
            deliverServo.SetAngle(HOLDBALL); //When delivering the ball this is what the servo will do
        }
    }       
}