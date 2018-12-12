namespace yearone2018
{
    class AutonControl
    {
        private const double RobotLength = 27.25; //How long the robot is
        private const int WheelDiameter = 6;  // The wheel diameter
        private const double RedorBlueside = 96; //Only one side of the field, not counting the game grid
        private const double Drive_Speed = .5; //Sets how fast the robot moves
        private const double SlowDownSpeed = .25; //Speed for slow down distance
        private const double SlowDownPercentage = .80; //Percentage of the whole distance slowing down will take
        private const double Correction = 0.01; //Changes the direction if off angle
        private const double ExtraDistancePercentage = 1.2; // if the bumber switch is not pressed we ill go an extra 20% distance
        private const double ExtraSpeed =0.2; //TODO put in speed that works when testing robot.
         
        private double DistanceToDrive;
        private double DesiredHeading;
        private double TargetDistance;
        private double SlowDownDistance; //The distance needed to slow down before the gamegrid
        private double right_correction;
        private double left_correction;
        private double ExtraDistance;
        private Chassis m_chassis; 
        private Deliver Deliver_Mechanism;
        public AutonControl()
        {   
            m_chassis = Chassis.GetInstance(); //Uses the chassis from the other chassis file
            Deliver_Mechanism = Deliver.GetInstance(); //Uses deliver from other deliver file
            DistanceToDrive = RedorBlueside - RobotLength; //Finds distance to drive
            TargetDistance = m_chassis.Get_distance() + DistanceToDrive; //Sets the distance needed to travel to get to the game grid
            SlowDownDistance = m_chassis.Get_distance() + (DistanceToDrive * SlowDownPercentage); //Finds the distance needed for slowing down
            DesiredHeading = m_chassis.Get_heading(); //Finds what heading we want and stored it as a variable
            ExtraDistance = m_chassis.Get_distance() + (DistanceToDrive * ExtraDistancePercentage);
        }

        public bool Run()
        {
            bool done = false;
           
            double heading = m_chassis.Get_heading(); //Finds the headingits traveling
            double error = DesiredHeading - heading; //Finds how far off the angle the bot is


            double distance = m_chassis.Get_distance();

            if (m_chassis.Is_Bumper_Switch_Pressed() )
            {
                m_chassis.drive(0, 0);//Stops the bot when bumper pressed
                Deliver_Mechanism.setState(Deliver.DELIVERSTATE.Deliver); //Delivers the balls
                Deliver_Mechanism.Run();
                done = true;
            }
            else
            {
                if (DistanceToDrive < SlowDownDistance)
                {
                    right_correction = Drive_Speed * (1 + (Correction * error)); //Corrects the right motor
                    left_correction = Drive_Speed * (1- (Correction * error)); //Corrects the left motor

                    m_chassis.drive (right_correction, left_correction);           //If else statement finds if the bot has hit its target. If it hasn't, then it keeps driving

                }
                 else if (distance < TargetDistance) //Sets the speed slower so the bot can hit the target more accurate
                {
                    right_correction = SlowDownSpeed * (1 + (Correction * error)); // The speed the right motor is going when at slow down
                    left_correction = SlowDownSpeed * (1- (Correction * error)); // The speed the left motor is going when at the slow down

                    m_chassis.drive (right_correction, left_correction); 

                }
                else if (distance < ExtraDistance) //Sets the speed slower so the bot can hit the target more accurate
                {
                    right_correction = ExtraSpeed * (1 + (Correction * error)); // The speed of the right motor is going when at extra distance
                    left_correction = ExtraSpeed * (1- (Correction * error)); //  The speed of the left motor is going when at extra distance
                    m_chassis.drive (right_correction, left_correction); 
                }
                else 
                {
                    m_chassis.drive (0,0); //Stops the motors
                    Deliver_Mechanism.setState(Deliver.DELIVERSTATE.Deliver); //Delivers the balls
                    Deliver_Mechanism.Run();
                    done = true;
                }
            }
            return done;
        }

    }
}
