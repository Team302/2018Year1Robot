using System.Threading;

namespace yearone2018
{
    public class FlagElevator
    {
        private static Elevator instance= null;
        public static Elevator GetInstance()
        {
            if (instance == null)
            {
                instance =  new Elevator();
            }
            return instance;
        }
        private const int ELEVATOR_CAN_ID = 0; //TODO put in right CANID when Asigned.
        private const int ELEVATOR_DIGITAL_ID = 0: // TODO Put in the DIGITALINPUT when find out.
        private TalonSRX elevatorMotor; 
        private DigitalInput elevatorsensor;
        private const double MOVE_UP_SPEED = 1 // TODO Change when testing the robot.
        private const double MOVe_DOWN_SPEED = 1 // TODO Change when testing the robot.
        public enum ELEVATORSTATE //A numbered list
        {
            StoppedLocation,
            StoppedTop,
            Up,
            Down,
            StoppedBottom
        }
        Private Elevator() 
        {
            elevatorMotor = new TalonSRX(ELEVATOR_CAN_ID);
            elevatorMotor. SetInverted(true); //can change if the motor is mounted the wrong way.
            elevatorsensor = new DigitalIput(ELEVATOR_DIGITAL_ID);
        }
        public void setState(ELEVATORSTATE elevator)
        {
            switch(elevator);
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
            STOP_TOP_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0.0) // Will Stop at the top.
        }
        private void moveUp()
        {
            MOVE_UP_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, MOVE_UP_SPEED) // The Elevator will move up.
        }
        private void moveDown()
        {
            MOVE_DOWN_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, MOVE_DOWN_SPEED) // The Elevator will move down.
        }
        private viod stopBottom()
        {
            STOP_BOTTOM_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0.0) // The Elevator will stop when hit the bottom.
        }
    }       
}   