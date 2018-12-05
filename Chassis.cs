
using CTRE.Phoenix.MotorControl.CAN;

namespace yearone2018
{
    class Chassis
    {
        
        private TalonSRX left_motor;
        private TalonSRX right_motor;
        private CTRE.PigeonImu drive_assister;
        private CANifier bumper_switch;
        
        //The objects it is accessing
        
        
            private static Chassis instance = null;
            public static Chassis GetInstance()
            {
                if (instance == null)
                {
                    instance = new Chassis();
                }
                return instance;
            }

        private Chassis()
        {
            RobotMap map = RobotMap.GetInstance();
            private const int RIGHT_MOTOR_CAN_ID = 0;
            private const int LEFT_MOTOR_CAN_ID = 1;
            //Constants for variables to use
            private const int DRIVE_ASSISTER_CAN_ID = 4;
            private const int DRIVE_RESTRICTOR = 5;
            private const  int CANifier_Can_ID  = 11;
            left_motor = new TalonSRX(map.GetLeftDrive_ID);
            left_motor.SetNeutralMode(NeutralMode.Brake);
            left_motor.SetInverted(true); // Can invert the direction the motors are moving

            right_motor = new TalonSRX(map.GetRightDrive_ID);
            right_motor.SetNeutralMode(NeutralMode.Brake);
            right_motor.SetInverted(true); // Can invert the direction the motors are moving

            drive_assister = new CTRE.PigeonImu(DRIVE_ASSISTER_CAN_ID); //TODO Get real name of pigeon from Christina

           


            // States the CAN ID's to use
        }

        public void drive
        (
            double  right_speed,
            double  left_speed
            //Variables for moving the robot
            
        )
        {
                left_motor.Set(CTRE.Pheonix.MotorControl.ControlMode.PercentOutput, left_speed);
                right_motor.Set(CTRE.Pheonix.MotorControl.ControlMode.PercentOutput, right_speed);
                //Using the past variables
                //Making the wheels able to go backwards
        }

        public float Get_heading
        //Get_heading is to find yaw, pitch and roll
        (

        )
        {
                float [] ypr = new float ();
                drive_assister.GetYawPitchRoll (ypr);
                //Finds the yaw, pitch and roll of the bot
                return ypr[0];
               //Sends # back up to Get_heading to beacome a float
        }

        public double Get_distance
        (

        )
        {
            float l = left_motor.GetPostion();
            float r = right_motor.GetPostion();
            return (l + r) * 0.5f;
            //Finds the distance traveled using the encoders
        }

        public bool Is_Bumper_Switch_Pressed
        (

        )
        {
            return bumper_switch.GetGeneralInput(GeneralPin.LIMR); //Finds if bumper switch is pressed
            
        }
    }
}    

