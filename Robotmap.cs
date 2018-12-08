
using CTRE.Phoenix.MotorControl.CAN;

namespace yearone2018
{
    class Robotmap
    {
        private static Robotmap instance = null;
            public static Robotmap GetInstance()
            {
                if (instance == null)
                {
                    instance = new Robotmap();
                }
                return instance;
            }

        private int CAN_ID_LeftDrive = 1;
            public int GetLeftDrive_ID ()  
            {
                return CAN_ID_LeftDrive;
            }

        private int CAN_ID_RightDrive = 2;
            public int GetRightDrive_ID ()
            {
                return CAN_ID_RightDrive;
            }

        private int CAN_ID_Transfer = 3;
            public int GetTransfer_ID ()
            {
                return CAN_ID_Transfer;
            }

        private int CAN_ID_Intake = 4;
            public int GetIntake_ID ()
            {
                return CAN_ID_Intake;
            }

        private int CAN_ID_LowerBanner = 5;
            public int GetLowerBanner_ID ()
            {
                return CAN_ID_LowerBanner;
            }

        private int CAN_ID_UpperBanner = 6;
            public int GetUpperBanner_ID ()
            {
                return CAN_ID_UpperBanner;
            }

        private int CAN_ID_LED = 7;
            public int GetLED_ID ()
            {
                return CAN_ID_LED;
            }

        private int CAN_ID_PIGEON = 8;
            public int Get_ID_Pigeon ()
            {
                return CAN_ID_PIGEON;
            }
        private int CAN_ID_ELEVATOR = 9;
            public int GetELEVATOR_ID ()
            {
                return CAN_ID_Elevator;
            }
            
        private int CAN_ID_FlagGrabber = 10;
            public int GetFlagGrabber_ID ()
            {
                return CAN_ID_FlagGrabber;
            }

        private int DIGITAL_ID_LowerSensor = 1;
            public int GetLowerSensor_ID ()
            {
                return DIGITAL_ID_LowerSensor;
            }

        private int DIGITAL_ID_UpperSensor = 2;
            public int GetUpperSensor_ID ()
            {
                return DIGITAL_ID_UpperSensor;
            }

        private int DIGITAL_ID_BumperSensor = 3;
            public int GetBumperSensor_ID ()
            {
                return DIGITAL_ID_BumperSensor;
            }

        private int DIGITAL_ID_FlagGrabber = 4;
            public int GetFlagGrabber_ID ()
            {
                return DIGITAL_ID_FlagGrabber;
            }
        private int DIGITAL_ID_TopSensor = 5;
            public int GetTopSensor_ID ()
            {
                return DIGITAL_ID_TopSensor;
            }
        private int DIGITAL_ID_BottomSensor = 6;
            public int GetBottomSensor_ID ()
            {
                return DIGITAL_ID_BottomSensor
            }
        private int PWM_ID_FlagGrabber = 1;
            public int GetFlagGrabber_ID ()

        private int PWM_ID_Flaggrabber = 1;
            public int GetFlaggrabber_ID ()
            {
                return PWM_ID_FlagGrabber;
            }

        private int PWM_ID_DeliverMec = 2;
            public int GetDeliverMec_ID ()
            {
                return PWM_ID_DeliverMec;
            }

        private int PWM_ID_flaggrabber = 3;
            public int Getflaggrabber_ID ()
            {
                return PWM_ID_flaggrabber;
            }
    }
}