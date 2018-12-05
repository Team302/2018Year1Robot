
using CTRE.Phoenix;
using CTRE.Phoenix.Controller;

namespace yearone2018
{
    class AutonControl
    {
        private const double RobotLength = 27.25; //How long the robot is
        private const int WheelDiameter = 6;  // The wheel diameter
        private const float RedorBlueside = 96; //Only one side of the field, not counting the game grid
        private const double Drive_Speed = .75; //Sets how fast the robot moves
        private const double SlowDownSpeed = .25;
        private const double SlowDownPercentage = .80; //Percentage of the whole distance slowing down will take
        private const double Correction = 0.01; //Changes the direction if off angle
        private double DistanceToDrive;
        private double DesiredHeading;
        private double TargetDistance;
        private double SlowDownDistance; //The distance needed to slow down before the gamegrid
        private double right_correction;
        private double left_correction;
        private Chassis m_chassis; 
        private Deliver Deliver_Mechanism;
        public AutonControl()
        {   
            m_chassis = Chassis.GetInstance(); //Uses the chassis from the other chassis file
            Deliver_Mechanism = Deliver.GetInstance(); //Uses deliver from other deliver file
            DistanceToDrive = RedorBlueside - RobotLength; //Finds distance to drive
            TargetDistance = m_chassis.Get_Distance() + DistanceToDrive; //Sets the distance needed to travel to get to the game grid
            SlowDownDistance = m_chassis.Get_Distance() + (DistanceToDrive * SlowDownPercentage); //Finds the distance needed for slowing down
            DesiredHeading = m_chassis.Get_heading();
            left_motor.SetSensorPhase(true); //Sets encoders
            right_motor.SetSensorPhase(true); //Sets encoders
            left_motor.ConfigSelectedFeedbackSensor(FeedbackDevice.QuadEncoder, 0, 0);
            right_motor.ConfigSelectedFeedbackSensor(FeedbackDevice.QuadEncoder, 0,0);
            
        }

        public void Run()
        {
           
            double heading = m_chassis.Get_heading(); //Finds the headingits traveling
            error = DesiredHeading - heading; //Finds how far off the angle the bot is

            right_correction = Drive_Speed * (1 + (Correction * error)); //Corrects the right motor
            left_correction = Drive_Speed * (1- (Correction * error)); //Corrects the left motor

            double distance = m_chassis.Get_Distance();

            if (bumper_pressed)
            {
                m_chassis.drive(0); //Stops the bot when bumper pressed
                Deliver_Mechanism.setState(); //Delivers the balls
            }
            else
            {
                if (DistanceToDrive < SlowDownDistance)
                {
                    m_chassis.drive (Drive_Speed * right_correction, Drive_Speed * left_correction);           //If else statement finds if the bot has hit its target. If it hasn't, then it keeps driving

                }
                 else if (distance < TargetDistance) //Sets the speed slower so the bot can hit the target more accurate
                {
                    m_chassis.drive (SlowDownSpeed * right_correction, SlowDownSpeed * left_correction);

                }
                else
                {
                    m_chassis.drive (0);
                }
            }


        
        }

    }
}
