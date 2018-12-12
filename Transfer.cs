
using CTRE.Phoenix;
using CTRE.Phoenix.MotorControl.CAN;
using Microsoft.SPOT;

namespace yearone2018
{
    class Transfer
    {
        private const double transferMotorSpeed = 1.0; //The "original" speed setting for the transfer motor
        private const double transferMotorStop = 0.0; //The "stop" speed setting for the transfer motor
        private const double ExpelSpeed = -1.0;

        private const int MOTOR_CAN_ID = 3; //Identifies the motor controller
        private const int IN_TRANSFER = 0; //The original setting for whether or not a ball is in

        private const int TOP_TRANSFER = 0; //The original setting for whether or not a ball can be placed
        private TalonSRX transferMotor; //The motor for ball transfer
        private LED glowing; //The LEDs
        private Intake eject; //Hong Bing's code
        private CANifier CANController; //The thing sensing if the ball intake was successful
         //The thing sensing if the ball can be placed

        private int numberOfBalls; //The counter for the number of balls


        
        public enum TRANSFER_STATE //Tells you which state the transfer is in
            {
                TRANSFER_ON,
                TRANSFER_OFF,
                TRANSFER_EXPEL
            }

            private TRANSFER_STATE m_currentState;


        private Transfer()
        {
            Robotmap map = Robotmap.GetInstance();

            transferMotor = new TalonSRX(map.GetTransfer_ID()); //Creates the motor
            transferMotor.SetInverted(true);

            CANController = Robotmap.GETCANController(); //Creates the first sensor
            
            numberOfBalls = 0; //The original amount of balls

            glowing = LED.GetInstance();

            m_currentState = TRANSFER_STATE.TRANSFER_OFF;
            eject = Intake.GetInstance();
           
        }

        public TRANSFER_STATE GetCurrentState()
        {
            return m_currentState;
        }

        private static Transfer instance = null;
        public static Transfer GetInstance()
        {
            if (instance == null)
            {
                instance = new Transfer();
            }
            return instance;
        }

        public void SetState //Sets the state of the transfer
        (
           TRANSFER_STATE state
        )
        {
            m_currentState = state;
        }
        public void Run()
        {
            switch(m_currentState) //Changes the state the transfer is in
                {
                    case TRANSFER_STATE.TRANSFER_ON:
                        Start();
                        break;
                    case TRANSFER_STATE.TRANSFER_OFF:
                        Stop();
                        break;
                    case TRANSFER_STATE.TRANSFER_EXPEL:
                        Expel();
                        break;
                    default:
                        Debug.Print("Transfer.SetState called with invalid state");
                        break;
                }
        }
        private void Start() //Sets motor speed
        {
            //Lower sensor says when a ball passes through a the tube and incruments the ball
            //upper sensor tells us tha twe have balls in the tube.
            Robotmap map = Robotmap.GetInstance();
            transferMotor.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, transferMotorSpeed);
            bool ballPresent = IsLowerSensorTripped();
            if (ballPresent)
            {
                numberOfBalls += 1;
            }
            bool ballPresentTop = IsUpperSensorTripped();
            if (!ballPresentTop && numberOfBalls > 0)
            {
                numberOfBalls = 0;
            }
            if (numberOfBalls == 0)
            {
                glowing.set_color(LED.MECHANISM_LED.GREEN); 
                eject.setState(Intake.INTAKESTATE.Sweep);
            }
            else if (numberOfBalls == 1)
            {
                glowing.set_color(LED.MECHANISM_LED.YELLOW);
                eject.setState(Intake.INTAKESTATE.Sweep);
            }
            else 
            {
               glowing.set_color(LED.MECHANISM_LED.RED);
               eject.setState(Intake.INTAKESTATE.Expel);
            }
        }

        private void Stop() //Sets motor speed
        {
            transferMotor.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, transferMotorStop);
        }

        private void Expel()
        {
            transferMotor.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, ExpelSpeed);
        }

        public bool IsLowerSensorTripped()
        {
            Robotmap map = Robotmap.GetInstance();
            return CANController.GetGeneralInput(map.GetLowerSensor_ID());  // sensors are being wired in reverse
        }

        public bool IsUpperSensorTripped()
        {
            Robotmap map = Robotmap.GetInstance();
            return CANController.GetGeneralInput(map.GetUpperSensor_ID()); // sensors are being wired in reverse
        }
    }
}
