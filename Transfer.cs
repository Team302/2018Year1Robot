
using CTRE.Phoenix.MotorControl.CAN;

namespace yearone2018
{
    class Transfer
    {
        private const double transferMotorSpeed = 1; //The "original" speed setting for the transfer motor
        private const double transferMotorStop = 0; //The "stop" speed setting for the transfer motor

        private const int MOTOR_CAN_ID = 3; //Identifies the motor controller
        private const int IN_TRANSFER = 0; //The original setting for whether or not a ball is in

        private const int TOP_TRANSFER = 0; //The original setting for whether or not a ball can be placed
        private TalonSRX transferMotor; //The motor for ball transfer
        private LED glowing; //The LEDs
        private Intake eject; //Hong Bing's code

        // TODO:  Comment inconsistent usage here it is initialized as an int later it is used as a DigitalInput
        // TODO:  Comment need separate variables just like how MOTOR_CAN_ID  and transferMotor one with the id and 
        // TODO:  Comment one with the hardware object.   DigitalInputs are switching to Canifier-based objects
        private DigitalInput ballInTrasfer = 0; //The thing sensing if the ball intake was successful

        private DigitalInput ballTopTransfer = 1; //The thing sensing if the ball can be placed

        private int numberOfBalls; //The counter for the number of balls

        
        public enum TRANSFER_STATE //Tells you which state the transfer is in
            {
                TRANSFER_ON,
                TRANSFER_OFF
            }

        private Transfer()
        {
            RobotMap map = RobotMap.GetInstance();

            transferMotor = new TalonSRX(GetTransfer_ID); //Creates the motor

            ballInTrasfer = new DigitalInput(GetLowerSensor_ID); //Creates the first sensor

            ballTopTransfer = new DigitalInput(GetUpperSensor_ID); //Creates the second sensor

            numberOfBalls = 0; //The original amount of balls

            glowing = new LED();
           
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
            switch(state) //Changes the state the transfer is in
                {
                    case TRANSFER_STATE.TRANSFER_ON:
                        Start();
                        break;
                    case TRANSFER_STATE.TRANSFER_OFF:
                        Stop();
                        break;
                    default:
                        //TODO: Put an error
                        break;
                }
        }
        private Start() //Sets motor speed
        {
            transferMotor.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, transferMotorSpeed);
            bool ballPresent = ballInTrasfer.Get();
            if (ballPresent)
            {
                numberOfBalls += 1;
            }
            bool ballPresentTop = ballTopTransfer.Get();
            if (ballPresentTop)
            {
                numberOfBalls -= 1;
            }
            if (numberOfBalls == 0)
            {
                glowing.set_color(GREEN); 
            }
            else if (numberOfBalls == 1)
            {
                glowing.set_color(YELLOW);
            }
            else (numberOfBalls == 2)
            {
               glowing.set_color(RED);
               eject.setState(Expel);
            }
        }

        private Stop() //Sets motor speed
        {
            transferMotor.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, transferMotorStop);
        }
    }
}
