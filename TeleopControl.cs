
using CTRE.Phoenix;
using CTRE.Phoenix.Controller;

namespace yearone2018
{
    class TeleopControl
    {
        private GameController m_gamepad;
        private Chassis m_chassis;
        private Transfer m_transfer;
        private Deadband m_deadband = 0.1;
        private Intake m_intake;

         // BUTTON MAPPING
        public const int A_BUTTON = 2;
        public const int B_BUTTON = 3;
        public const int X_BUTTON = 1;
        public const int Y_BUTTON = 4;
        public const int RIGHT_BUMPER = 6;
        public const int LEFT_BUMPER = 5;

        // AXIS MAPPING
        public const int RIGHT_TRIGGER = CTRE.Phoenix.Controller.LogitechGamepad.kAxis_RightShoulder;
        public const int LEFT_TRIGGER = CTRE.Phoenix.Controller.LogitechGamepad.kAxis_LeftShoulder;
        public const int RIGHT_JOYSTICK_X = CTRE.Phoenix.Controller.LogitechGamepad.kAxis_RightX;
        public const int RIGHT_JOYSTICK_Y = CTRE.Phoenix.Controller.LogitechGamepad.kAxis_RightY;
        public const int LEFT_JOYSTICK_X = CTRE.Phoenix.Controller.LogitechGamepad.kAxis_LeftX;
        public const int LEFT_JOYSTICK_Y = CTRE.Phoenix.Controller.LogitechGamepad.kAxis_LeftY;
        public TeleopControl()
        {
            m_gamepad = new GameController(UsbHostDevice.GetInstance()); // create gamepad

            m_chassis = Chassis.GetInstance(); //Creates chassis

            m_transfer = Transfer.GetInstance(); //Creates "Transfer"

            m_intake = Intake.GetInstance(); // Creates Intake
           
        }

        public void DriveWithJoysticks()
        {
            double throttle = m_gamepad.GetAxis(0);      // read the gamepad 
            
            throttle = ( System.Maths.Abs(throttle) < m_deadband) ? 0.0  : throttle; //Makes very small movements on joystick not move the robot

            throttle = System.Math.Pow(throttle, 3.0); //Cubes the value

            double steer = m_gamepad.GetAxis(0);      // read the gamepad 
            
            steer = ( System.Maths.Abs(steer) < m_deadband) ? 0.0  : steer; //Makes very small movements on joystick not move the robot

            steer = (System.Math.Pow(steer, 3.0));        //Cubes the value

            double left_speed = throttle - steer;
            double right_speed = throttle + steer;
            // Formula for arcade drive

            
            double maxValue = (System.Maths.Abs(left_speed));
            maxValue = (System.Maths.Abs(right_speed) > maxValue) ? right_speed : maxValue;
            if (maxValue > .99)  //Makes the robot not go too fast
            { 
                 left_speed = left_speed/maxValue; // Stops the left side going too fast
                 right_speed = right_speed/maxValue; //Stops the right side going too fast
            }
            //How the robot moves
            m_chassis.drive (right_speed, left_speed);
            
        }

        public void BallHandler()
        {
            bool transferable = m_transfer.GetButton(A_BUTTON); //Returns a true or false for a button press

            bool runintake = m_gamepad.GetButton(LEFT_BUMPER); //whether the Intake button is pressed. bool is true or false
            
            bool runexpel = m_gamepad.GetButton(RIGHT_BUMPER); // Whether if the Expel button is pressed is true or false
            if (transferable) //Turns transfer on if button is pressed
            {
                m_transfer.SetState(Transfer.TRANSFER_STATE.TRANSFER_ON);
            }
            else  //Keeps transfer off if not pressed
            {
                m_transfer.Run();
            }

            if (runintake) // If/else if/else statement 
            {
                m_intake.setState(Sweep); // If the Sweep button is pressed then it will sweep
                m_transfer.SetState(TRANSFER_ON);
            }
            else if (runexpel)
            {
                m_intake.setState(Expel); // Else if the Expel button is pressed then it will expel
            }
            else
            {
                m_intake.Run(); // else no button is pressed then the sweep will be off.
            }
        }   
    }
}
