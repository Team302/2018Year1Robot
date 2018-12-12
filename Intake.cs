using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
using Microsoft.SPOT;

namespace yearone2018
{
    public class Intake // Taking the ball from the game grid and the ground.
    {
        private static Intake instance = null; // a singlex which will make it so we don't need to define intake twice if we have it in auton and teleop instead have it once.
        public static Intake GetInstance()
        {
            if(instance == null)
            {
                instance= new Intake();
            }
            return instance;
        }
        private TalonSRX intakeMotor; // What the TalonSRX is and called.
        private const double INTAKE_SPEED = 1.0; // Can change speed when needed.
        private const double EXPEL_SPEED = -1.0; // Can change speed when needed.
        public enum INTAKESTATE // A numbered list.
        {
            Sweep,
            Expel,
            Off
        }
        private INTAKESTATE m_currentState;
        private Intake()
        {
            Robotmap map = Robotmap.GetInstance();
            intakeMotor = new TalonSRX(map.GetIntake_ID()); // Creates the TalonSRX motor.
            intakeMotor. SetInverted(true); // If you get value i will invert it. if it is going the wrong way it can be inverted.
            m_currentState = INTAKESTATE.Off;
        }
        public INTAKESTATE GetCurrentState()
        {
            return m_currentState;
        }
        public void setState(INTAKESTATE sweeper)
        {
            m_currentState = sweeper;
        }
        public void Run()
        {
              switch(m_currentState) // If we are going to have sweep to be on, sweep to be off, expel, or default.
            {
            case INTAKESTATE.Sweep:
                sweepOn();
                break;
            case INTAKESTATE.Off:
                sweepOff();
                break;
            case INTAKESTATE.Expel:
                expel();
                break;
            default:
                Debug.Print("Intake.setState called with invalid state");
                break;
            }
        }
        private void sweepOn()
        {
            intakeMotor.Set(ControlMode.PercentOutput, INTAKE_SPEED); // This means that the sweeper will sweep the balls in.
        }
        private void sweepOff()
        {
            intakeMotor.Set(ControlMode.PercentOutput, 0.0); // The sweeper will stop.
        }
        private void expel()
        {
            intakeMotor.Set(ControlMode.PercentOutput, EXPEL_SPEED); // This means that the sweeper will expel the balls out.
        }
    }       
}