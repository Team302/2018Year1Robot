using CTRE.Phoenix;

namespace yearone2018
{
    class Robotmap
    {
        // Singleton Initialization
        private static CANifier m_canifier = null;
        private static Robotmap instance = null;

        // CAN IDs
        private int CAN_ID_LeftDrive = 1;
        private int CAN_ID_RightDrive = 4;
        private int CAN_ID_Transfer = 2;
        private int CAN_ID_Intake = 5;
        private int CAN_ID_CANIFIER = 30;
        private int CAN_ID_PIGEON = 40;
        private int CAN_ID_ELEVATOR = 3;

        // Digital Input Pins
        private CANifier.GeneralPin DIGITAL_ID_LowerSensor = CANifier.GeneralPin.QUAD_A;
        private CANifier.GeneralPin DIGITAL_ID_UpperSensor = CANifier.GeneralPin.QUAD_B;
        private CANifier.GeneralPin DIGITAL_ID_BumperSensor = CANifier.GeneralPin.SPI_MISO_PWM2P;

        // Servo PWM Pins
        private uint PWM_ID_FlagGrabber = 1;
        private uint PWM_ID_DeliverMec = 0;





        // Create/Return CANifier Singleton
        public static CANifier GETCANController()
        {
            Robotmap map = Robotmap.GetInstance();
            if (m_canifier == null)
            {
                m_canifier = new CANifier((ushort) map.GetCANIFIER_ID());
            }
            return m_canifier;
        }

        // Create/Return Robotmap Singleton
        public static Robotmap GetInstance()
        {
            if (instance == null)
            {
                instance = new Robotmap();
            }
            return instance;
        }

        public int GetLeftDrive_ID ()  
        {
            return CAN_ID_LeftDrive;
        }

        public int GetRightDrive_ID ()
        {
            return CAN_ID_RightDrive;
        }

        public int GetTransfer_ID ()
        {
            return CAN_ID_Transfer;
        }

        public int GetIntake_ID ()
        {
            return CAN_ID_Intake;
        }

        public int GetCANIFIER_ID ()
        {
            return CAN_ID_CANIFIER;
        }

        public int Get_ID_Pigeon ()
        {
            return CAN_ID_PIGEON;
        }
        public int GetELEVATOR_ID ()
        {
            return CAN_ID_ELEVATOR;
        }
        public CANifier.GeneralPin GetLowerSensor_ID ()
        {
            return DIGITAL_ID_LowerSensor;
        }

        public CANifier.GeneralPin GetUpperSensor_ID ()
        {
            return DIGITAL_ID_UpperSensor;
        }

        public CANifier.GeneralPin GetBumperSensor_ID ()
        {
            return DIGITAL_ID_BumperSensor;
        }
        public uint GetFlagGrabberServo_ID ()
        {
            return PWM_ID_FlagGrabber;
        }

        public uint GetDeliverMec_ID ()
        {
            return PWM_ID_DeliverMec;
        }
    }
}