
using CTRE.Phoenix;
using CTRE.Phoenix.Sensors;
using CTRE.Phoenix.MotorControl.CAN;
using CTRE.Phoenix.MotorControl;

namespace yearone2018
{
    class Chassis
    {
        private const double Counts = 1440; //# of counts that the encoder uses
        private const double Drive_Gear_Ratio = 2.64 / 2.4; //The gear ratio of the drive train
        private const int WheelDiameter = 6;  // The wheel diameter
        private TalonSRX left_motor;
        private TalonSRX right_motor;
        private PigeonIMU drive_assister;
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
            Robotmap map = Robotmap.GetInstance();
            //Constants for variables to use
            left_motor = new TalonSRX(map.GetLeftDrive_ID());
            left_motor.SetNeutralMode(NeutralMode.Brake);
            left_motor.SetInverted(true); // Can invert the direction the motors are moving

            right_motor = new TalonSRX(map.GetRightDrive_ID());
            right_motor.SetNeutralMode(NeutralMode.Brake);
            right_motor.SetInverted(true); // Can invert the direction the motors are moving

            drive_assister = new PigeonIMU(map.Get_ID_Pigeon()); //TODO Get real name of pigeon from Christina

            bumper_switch = Robotmap.GETCANController();          
        }

        public void drive
        (
            double  right_speed,
            double  left_speed
            //Variables for moving the robot
        )
        {
            left_motor.Set(ControlMode.PercentOutput, left_speed);
            right_motor.Set(ControlMode.PercentOutput, right_speed);
                //Using the past variables
                //Making the wheels able to go backwards
        }

        public float Get_heading()        //Get_heading is to find yaw, pitch and roll
        {
                float[] ypr = new float[3];
                drive_assister.GetYawPitchRoll (ypr);
                //Finds the yaw, pitch and roll of the bot
                return ypr[0];
               //Sends # back up to Get_heading to beacome a float
        }

        public double Get_distance()
        {
            int l = left_motor.GetSelectedSensorPosition();
            int r = right_motor.GetSelectedSensorPosition();
            double average = (l + r) * 0.5; //Finds average of what the wheel distance
            //Finds the distance traveled using the encoders
            double counts_per_revolution = average / Counts; //Finds the counts per revolution
            double inches_per_revolution = (WheelDiameter * System.Math.PI) * Counts; //Finds how many inches will be travelled per revolution
            double inches_traveled = inches_per_revolution * Drive_Gear_Ratio; //Finds the inches travelled using gear ratio
            return inches_traveled;
        }

        public bool Is_Bumper_Switch_Pressed()
        {
            Robotmap map = Robotmap.GetInstance();
            return !bumper_switch.GetGeneralInput( map.GetBumperSensor_ID() ); //Finds if bumper switch is pressed (wired in reverse)
            
        }
    }
}    

