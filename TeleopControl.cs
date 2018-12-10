
using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using Microsoft.SPOT;

namespace yearone2018
{
    class TeleopControl
    {
        private GameController m_gamepad;
        private Chassis m_chassis;
        private Transfer m_transfer;
        private FlagElevator m_elevator;
        private FlagGrabber m_grabber;
        private Deliver m_deliver;

        private double m_deadband = 0.1;
        private Intake m_intake;

        // BUTTON MAPPING
        private const uint A_BUTTON = 2;
        private const uint B_BUTTON = 3;
        private const uint X_BUTTON = 1;
        private const uint Y_BUTTON = 4;
        private const uint RIGHT_BUMPER = 6;
        private const uint LEFT_BUMPER = 5;
        private static uint SELECT_BUTTON = 9;
        private static uint START_BUTTON = 10;
        private static uint POWER_BUTTON = 13;

        // AXIS MAPPING
        private const uint RIGHT_TRIGGER = LogitechGamepad.kAxis_RightShoulder;
        private const uint LEFT_TRIGGER = LogitechGamepad.kAxis_LeftShoulder;
        private const uint RIGHT_JOYSTICK_X = LogitechGamepad.kAxis_RightX;
        private const uint RIGHT_JOYSTICK_Y = LogitechGamepad.kAxis_RightY;
        private const uint LEFT_JOYSTICK_X = LogitechGamepad.kAxis_LeftX;
        private const uint LEFT_JOYSTICK_Y = LogitechGamepad.kAxis_LeftY;

        public TeleopControl()
        {
            m_gamepad = new GameController(UsbHostDevice.GetInstance()); // create gamepad

            m_chassis = Chassis.GetInstance(); //Creates chassis

            m_deliver = Deliver.GetInstance(); // Create deliver 

            m_transfer = Transfer.GetInstance(); //Creates "Transfer"

            m_intake = Intake.GetInstance(); // Creates Intake

            m_elevator = FlagElevator.GetInstance(); //Creates Flag Elevator

            m_grabber = FlagGrabber.GetInstance(); //Creates Flag Grabber
           
        }

        public GameController GetController()
        {
            return m_gamepad;
        }

        public uint GetAutonButton()
        {
            return A_BUTTON;
        }
        public uint GetStartButton()
        {
            return START_BUTTON;
        }
        public uint GetStopButton()
        {
            return SELECT_BUTTON;
        }


        public void Run()
        {
            DriveWithJoysticks();
            BallHandler();
            FlagHandler();
        }

        public void CheckSensors()
        {
            Debug.Print("Bumper Switch " + m_chassis.Is_Bumper_Switch_Pressed().ToString());
            Debug.Print("Heading " + m_chassis.Get_heading().ToString());
            Debug.Print("Distance " + m_chassis.Get_distance().ToString());
            Debug.Print("Lower Banner " + m_transfer.IsLowerSensorTripped().ToString());
            Debug.Print("Upper Banner " + m_transfer.IsUpperSensorTripped().ToString());
        }

        public void DriveWithJoysticks()
        {
            double throttle = -1.0 * m_gamepad.GetAxis(LEFT_JOYSTICK_Y);      // read the gamepad 
            
            throttle = ( System.Math.Abs(throttle) < m_deadband) ? 0.0  : throttle; //Makes very small movements on joystick not move the robot

            throttle = System.Math.Pow(throttle, 3.0); //Cubes the value

            double steer = m_gamepad.GetAxis(RIGHT_JOYSTICK_X);      // read the gamepad 
            
            steer = ( System.Math.Abs(steer) < m_deadband) ? 0.0  : steer; //Makes very small movements on joystick not move the robot

            steer = (System.Math.Pow(steer, 3.0));        //Cubes the value

            double left_speed = throttle - steer;
            double right_speed = throttle + steer;
            // Formula for arcade drive

            
            double maxValue = (System.Math.Abs(left_speed));
            maxValue = (System.Math.Abs(right_speed) > maxValue) ? right_speed : maxValue;
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
            bool deliver = m_gamepad.GetButton(Y_BUTTON);

            bool runintake = m_gamepad.GetButton(LEFT_BUMPER); //whether the Intake button is pressed. bool is true or false
            
            bool runexpel = m_gamepad.GetButton(RIGHT_BUMPER); // Whether if the Expel button is pressed is true or false


            if (runintake) // If/else if/else statement 
            {
                m_intake.setState(Intake.INTAKESTATE.Sweep); // If the Sweep button is pressed then it will sweep
                m_transfer.SetState(Transfer.TRANSFER_STATE.TRANSFER_ON);
            }
            else if (runexpel)
            {
                m_intake.setState(Intake.INTAKESTATE.Expel); // Else if the Expel button is pressed then it will expel
                m_transfer.SetState(Transfer.TRANSFER_STATE.TRANSFER_OFF);
            }
            else
            {
                m_intake.setState(Intake.INTAKESTATE.Off); // else no button is pressed then the sweep will be off.
                m_transfer.SetState(Transfer.TRANSFER_STATE.TRANSFER_OFF);
            }

            if ( deliver )
            {
                m_deliver.setState(Deliver.DELIVERSTATE.Deliver);
            }
            else
            {
                m_deliver.setState(Deliver.DELIVERSTATE.HoldBalls);
            }

            m_intake.Run();
            m_transfer.Run();
            m_deliver.Run();
        }

        public void FlagHandler()
        {
            bool grabflag = m_gamepad.GetButton(X_BUTTON);

            bool releaseflag = m_gamepad.GetButton(B_BUTTON);

            double elevatorup = m_gamepad.GetAxis(LEFT_TRIGGER);

            double elevatordown = m_gamepad.GetAxis(RIGHT_TRIGGER);

            if (grabflag)
            {
                m_grabber.setState(FlagGrabber.FlagGrabberSTATE.GRABBER_CLOSED);
            }
            else if (releaseflag)
            {
                m_grabber.setState(FlagGrabber.FlagGrabberSTATE.GRABBER_OPEN);
            }
            m_grabber.Run();

            elevatorup = ( System.Math.Abs(elevatorup) < m_deadband) ? 0.0  : elevatorup;

            elevatordown = ( System.Math.Abs(elevatordown) < m_deadband) ? 0.0  : elevatordown;

            if (elevatorup > 0.4)
            {
                m_elevator.setState(FlagElevator.ELEVATORSTATE.Up);
            }

            else if (elevatordown > 0.4)
            {
                m_elevator.setState(FlagElevator.ELEVATORSTATE.Down);
            }
        }
    }
}
