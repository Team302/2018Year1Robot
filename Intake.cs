using System.Threading;

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
        private const int INTAKE_CAN_ID = 4;
        private TalonSRX intakeMotor; // What the TalonSRX is and called.
        private const double INTAKE_SPEED=0.75 // Can change speed when needed.
        private const double EXPEL_SPEED=-0.75 // Can change speed when needed.
        public enum INTAKESTATE // A numbered list.
        {
            Sweep,
            Expel,
            Off
        }
        private INTAKESTATE m_currentState;
        private Intake()
        {
            intakeMotor = new TalonSRX(INTAKE_CAN_ID); // Creates the TalonSRX motor.
            intakeMotor. SetInverted(true); // If you get value i will invert it. if it is going the wrong way it can be inverted.
            m_currentState = INTAKESTATE.Off
        }
        public void setState(INTAKESTATE sweeper)
        {
            m_currentState = sweeper;
            Run();
        }
        public void Run()
        {
              switch(m_currentState); // If we are going to have sweep to be on, sweep to be off, expel, or default.
            {
            case Sweep:
                sweepOn();
                break;
            case Off:
                sweepOff();
                break;
            case Expel:
                expel();
                break;
            default:
                    // TODO put error out.
                break;
            }
        }
        private void sweepOn()
        {
            INTAKE_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, INTAKE_SPEED); // This means that the sweeper will sweep the balls in.
        }
        private void sweepOff()
        {
            INTAKE_STOP_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0.0) // The sweeper will stop.
        }
        private void expel()
        {
            EXPEL_SPEED.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, EXPEL_SPEED) // This means that the sweeper will expel the balls out.
        }
    }       
}