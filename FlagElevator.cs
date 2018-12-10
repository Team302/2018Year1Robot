
using CTRE.Phoenix.MotorControl.CAN;
using Microsoft.SPOT; 

namespace yearone2018
{
    public class FlagElevator
    {
        private static FlagElevator instance= null; // a singlex which makes it so that we don't have to make two flag Elevators if we have it for auton and teleop instead we can have one. 
        public static FlagElevator GetInstance()
        {
            if (instance == null)
            {
                instance =  new FlagElevator();
            }
            return instance;
        }
        private const int ELEVATOR_CAN_ID = 9; //TODO put in right CANID when Asigned.
        private TalonSRX elevatorMotor;

        private const double MOVE_UP_SPEED = 1; // TODO Change when testing the robot.

        private const double MOVE_DOWN_SPEED = -1; // TODO Change when testing the robot.

        public enum ELEVATORSTATE //A numbered list
        {
            StoppedLocation,
            Up,
            Down
        }
        private ELEVATORSTATE m_currentState;
        private FlagElevator() 
        {
            elevatorMotor = new TalonSRX(ELEVATOR_CAN_ID);

            elevatorMotor. SetInverted(true); //can change if the motor is mounted the wrong way.
            
            m_currentState = ELEVATORSTATE.StoppedLocation;
        }
        public ELEVATORSTATE GetCurrentState()
        {
            return m_currentState;
        }
        public void setState(ELEVATORSTATE elevator)
        {
           m_currentState = elevator;
        }
        public void Run()
        {
            switch(m_currentState)
            {
                case ELEVATORSTATE.StoppedLocation: // Stop location
                    stopLocation();
                    break;
                case ELEVATORSTATE.Up:
                    moveUp();
                    break;
                case ELEVATORSTATE.Down:
                    moveDown();
                    break;
                default:
                    Debug.Print("FlagElevator.setState called with invalid state");
                    break;
            }
        }
        private void stopLocation()
        {
            elevatorMotor.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0.0); // Will stop at when the button is pressed.
        }
        private void moveUp()
            {
                elevatorMotor.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, MOVE_UP_SPEED); // The Elevator will move up.
            }
        private void moveDown()
        {
             elevatorMotor.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, MOVE_DOWN_SPEED); // The Elevator will move down.
        }
    }       
}   
