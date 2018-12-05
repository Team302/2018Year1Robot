using System.Threading;

namespace yearone2018
{
    public class Intake // Taking the ball from the game grid and the ground.
    {
        private static Intake instance = null;
        public static Intake GetInstance()
        {
            if(instance == null)
            {
                instance= new Intake();
            }
            return instance;
        }
        private const int INTAKE_CAN_ID = 2;
        private TalonSRX intakeMotor; // What the TalonSRX is and called.
        private const double INTAKE_SPEED=0.75 // Can change speed when needed.
        private const double EXPEL_SPEED=-0.75 // Can change speed when needed.
        public enum INTAKESTATE // A numbered list.
        {
            Sweep,
            Expel,
            Off
        }
        private Intake()
        {
            intakeMotor = new TalonSRX(INTAKE_CAN_ID); // Creates the TalonSRX motor.
            intakeMotor. SetInverted(true); // If you get value i will invert it. if it is going the wrong way it can be inverted.
        }
        public void setState(INTAKESTATE sweeper)
        {
              switch(sweeper); // If we are going to have sweep to be on, sweepto be off, expel, or default.
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