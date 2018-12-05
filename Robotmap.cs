
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

                            private int PWM_ID_FlagGrabber = 1;
                                public int GetFlagGrabber_ID ()
                                {
                                    return PWM_ID_FlagGrabber;
                                }

                            private int PWM_ID_DeliverMec = 2;
                                public int GetDeliverMec_ID ()
                                {
                                    return PWM_ID_DeliverMec;
                                }
        // TODO:  looks like leftover stuff from LED
        enum MECHANISM_Robotmap
        {
        RED,
        };
        //
        private Robotmap()
        {
            lightie_boies = new CANifier(Robotmap_CAN_ID);

            //
        }

        public void set_color
        (
            MECHANISM_Robotmap color
        )
        {
            int r = -1;
            int g = -1;
            int b = -1 ;

            switch (color)
            {
             case RED:
                    r = 
                    break;
             default:
                    r = 0;
                    g = 0;
                    b = 0;
            }
            lightie_boies.SetLEDOutput(r, CANifier.LEDChannel.LEDChannelA);
            lightie_boies.SetLEDOutput(g, CANifier.LEDChannel.LEDChannelA);
            lightie_boies.SetLEDOutput(b, CANifier.LEDChannel.LEDChannelA);

            // 
        }
    }
}
