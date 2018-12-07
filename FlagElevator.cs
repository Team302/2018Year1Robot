using System.Threading;

namespace yearone2018
{
    public class FlagElevator
    {
        private static Elevator instance= null; // a singlex which makes it so that we don't have to make two flag Elevators if we have it for auton and teleop instead we can have one. 
        public static Elevator GetInstance()
        {
            if (instance == null)
            {
                instance =  new Elevator();
            }
            return instance;
        }
        private const int ELEVATOR_CAN_ID = 9; //TODO put in right CANID when Asigned.
        private const int ELEVATOR_TOP_DIGITAL_ID = 5;
        private const int ELEVATOR_BOTTOM_DIGITAL_ID = 6;
        private TalonSRX elevatorMotor; 
        private DigitalInput elevatorsensortop;
        private DigitalInput elevatorsensorbottom;
        private const double MOVE_UP_SPEED = 1 // TODO Change when testing the robot.
        private const double MOVE_DOWN_SPEED = 1 // TODO Change when testing the robot.
        public enum ELEVATORSTATE //A numbered list
        {
            StoppedLocation,
            StoppedTop,
            Up,
            Down,
            StoppedBottom
        }
        private ELEVATORSTATE m_currentState;
        Private Elevator() 
        {
            elevatorMotor = new TalonSRX(ELEVATOR_CAN_ID);
            elevatorMotor. SetInverted(true); //can change if the motor is mounted the wrong way.
            elevatorsensortop = new DigitalIput(ELEVATOR_TOP_DIGITAL_ID);
            elevatorsensorbottom = new DigitalInput(ELEVATOR_BOTTOM_DIGITAL_ID);
            m_currentState = ELEVATORSTATE.Off
        }
        public void setState(ELEVATORSTATE elevator)
        {
           m_currentState = elevator;
           Run();
        }
        public void Run()
        {
            switch(m_currentState);
            {
                case StoppedLocation: // going to have it stop at the top (stopTop), Stop at any location (stopLocation), moveUp, moveDown, or stop at the bottom(stopBottom) 
                    stopLocation();
                    break;
                case StoppedTop:
                    stopTop();
                    break;
                case Up:
                    moveUp();
                    break;
                case Down;
                    moveDown();
                    break;
                case StoppedBottom:
                    stopBottom();
                    break;
                default:
                        // TODO add this in later.
                    break;
            }
        }
        private void stopLocation()
        {
            STOP_LOCATION_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0.0); // Will stop at when the button is pressed.
        }
        private void stopTop()
        {
            STOP_TOP_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0.0); // Will Stop at the top.
        }
        private void moveUp()
        {
            if (elevatorsensortop.Get())
            {
                setState(stopTop);
            }
            else 
            {
                MOVE_UP_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, MOVE_UP_SPEED); // The Elevator will move up.
            }
        }
        private void moveDown()
        {
            if (elevatorsensorbottom.Get())
            {
                setState(stopBottom);
            }
            else
            {
                MOVE_DOWN_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, MOVE_DOWN_SPEED); // The Elevator will move down.
            }
        private viod stopBottom()
        {
            STOP_BOTTOM_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0.0); // The Elevator will stop when hit the bottom.
        }
    }       
}   