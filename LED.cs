
using CTRE.Phoenix.MotorControl.CAN;

namespace yearone2018
{
    class LED
    {
            private static LED instance = null;
            public static LED GetInstance()
            {
                if (instance == null)
                {
                    instance = new LED();
                }
                return instance;
            }

        private const int LED_CAN_ID = 10;
        private CANifier lightie_boies;
    

        enum MECHANISM_LED
        {
        RED,
        ORANGE,
        YELLOW,
        LIME,
        GREEN,
        TEAL,
        BLUE,
        PURPLE,
        PINK,
        BROWN 
        };
        //is that ; supposed to be there?

        private LED()
        {
            lightie_boies = new CANifier(LED_CAN_ID);

            // H-Hewo?? somewon pwease hewlp me
            // I'm havwing a wittle bit of twobule figuwring out what to do next 3;
        }

        public void set_color
        (
            MECHANISM_LED color
        )
        {
            int r = -1;
            int g = -1;
            int b = -1 ;

            switch (color)
            {
             case RED:
                    r = 255;
                    g = 229;
                    b = 229;
                    break;
             case ORANGE:
                    r = 255;
                    g = 242;
                    b = 229;
                    break;
             case YELLOW:
                    r = 255;
                    g = 255;
                    b = 229;
                    break;
             case LIME:
                    r = 242;
                    g = 255;
                    b = 229;
                    break;
             case GREEN:
                    r = 229;
                    g = 255;
                    b = 229;
                    break;
             case TEAL:
                    r = 229;
                    g = 255;
                    b = 242;
                    break;
             case BLUE: 
                    r = 229;
                    g = 229;
                    b = 255;
                    break;
             case PURPLE:
                    r = 242;
                    g = 229;
                    b = 255;
                    break;
             case PINK:
                    r = 255;
                    g = 229;
                    b = 242;
                    break;
             case BROWN:
                    r = 237;
                    g = 229;
                    b = 225;
                    break;
             default:
                    r = 0;
                    g = 0;
                    b = 0;
            }
            lightie_boies.SetLEDOutput(r, CANifier.LEDChannel.LEDChannelA);
            lightie_boies.SetLEDOutput(g, CANifier.LEDChannel.LEDChannelA);
            lightie_boies.SetLEDOutput(b, CANifier.LEDChannel.LEDChannelA);

            // H-Hewo?? This is my LED lightie boies code
            // Are you m-mr. obama??
            // D:
        }
    }
}
