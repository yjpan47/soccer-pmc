using System;
using System.Collections.Generic;
using System.Text;
using PAT.Common.Classes.Expressions.ExpressionClass;

//the namespace must be PAT.Lib, the class and method names can be arbitrary
namespace PAT.Lib
{

	enum Team
    {
        A,
        B
    }

    enum Position
    {
        Goalkeeper,
        RightFullback,
        LeftFullback,
        CenterBack,
        Sweeper,
        DefensiveMidfielder,
        RightMidfielder,
        CentralMidfielder,
        Striker,
        AttackingMidfielder,
        LeftMidfielder
    }

    enum Skill 
    {
        Shooting,
        Passing,
        Speed,
        BallHandling,
        Defence,
        
    }

    enum Boundary 
    {
        minX,
        maxX,
        minY,
        maxY,
    }

    public class ManagerA : ExpressionValue
    {
        private Team team;
        private int fieldMinX;
        private int fieldMaxX;
        private int fieldMinY;
        private int fieldMaxY;
        private int[] goalCoordinate;
        private Dictionary<Position, Dictionary<Skill, int>> playerStats;
        private Dictionary<Position, Dictionary<Boundary, int>> playerBoundaries;
        private Dictionary<Position, List<Position>> playerPassingTargets;

        public ManagerA() 
        {
            this.team = Team.A;

            this.setGoal();
            this.setFieldBoundaries();
            this.setPlayerBoundaries();
            this.setPlayerPassingTargets();
            this.setPlayerStats();
        }

        private void setGoal()
        {
            // Set the coordinate location of the goal
            this.goalCoordinate = new int[] {9, 3};
        }

        private void setFieldBoundaries()
        {
            // Set the boundaries of the entire playing field
            this.fieldMinX = 0;
            this.fieldMaxX = 9;
            this.fieldMinY = 0;
            this.fieldMaxY = 6;
        }

        private void setPlayerBoundaries()
        {
            // Set the boundaries for each player based on position
            // Team A follows a 4-4-2 Formation
            this.playerBoundaries = new Dictionary<Position, Dictionary<Boundary, int>>();
            
            // GoalKeeper
            this.playerBoundaries[Position.Goalkeeper] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.Goalkeeper][Boundary.minX] = 0;
            this.playerBoundaries[Position.Goalkeeper][Boundary.maxX] = 1;
            this.playerBoundaries[Position.Goalkeeper][Boundary.minY] = 2;
            this.playerBoundaries[Position.Goalkeeper][Boundary.maxY] = 4;

            // Right Fullback 
            this.playerBoundaries[Position.RightFullback] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.RightFullback][Boundary.minX] = 0;
            this.playerBoundaries[Position.RightFullback][Boundary.maxX] = 2;
            this.playerBoundaries[Position.RightFullback][Boundary.minY] = 0;
            this.playerBoundaries[Position.RightFullback][Boundary.maxY] = 2;

            // Center Back
            this.playerBoundaries[Position.CenterBack] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.CenterBack][Boundary.minX] = 0;
            this.playerBoundaries[Position.CenterBack][Boundary.maxX] = 2;
            this.playerBoundaries[Position.CenterBack][Boundary.minY] = 1;
            this.playerBoundaries[Position.CenterBack][Boundary.maxY] = 3;

            // Sweeper
            this.playerBoundaries[Position.Sweeper] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.Sweeper][Boundary.minX] = 0;
            this.playerBoundaries[Position.Sweeper][Boundary.maxX] = 2;
            this.playerBoundaries[Position.Sweeper][Boundary.minY] = 3;
            this.playerBoundaries[Position.Sweeper][Boundary.maxY] = 5;

            // Left Fullback
            this.playerBoundaries[Position.LeftFullback] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.LeftFullback][Boundary.minX] = 0;
            this.playerBoundaries[Position.LeftFullback][Boundary.maxX] = 2;
            this.playerBoundaries[Position.LeftFullback][Boundary.minY] = 4;
            this.playerBoundaries[Position.LeftFullback][Boundary.maxY] = 6;

            // Right Midfielder
            this.playerBoundaries[Position.RightMidfielder] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.RightMidfielder][Boundary.minX] = 3;
            this.playerBoundaries[Position.RightMidfielder][Boundary.maxX] = 6;
            this.playerBoundaries[Position.RightMidfielder][Boundary.minY] = 0;
            this.playerBoundaries[Position.RightMidfielder][Boundary.maxY] = 1;

            // Defensive Midfielder
            this.playerBoundaries[Position.DefensiveMidfielder] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.DefensiveMidfielder][Boundary.minX] = 3;
            this.playerBoundaries[Position.DefensiveMidfielder][Boundary.maxX] = 6;
            this.playerBoundaries[Position.DefensiveMidfielder][Boundary.minY] = 2;
            this.playerBoundaries[Position.DefensiveMidfielder][Boundary.maxY] = 4;

            // Central Midfielder
            this.playerBoundaries[Position.CentralMidfielder] = new Dictionary<Boundary, int>(); 
            this.playerBoundaries[Position.CentralMidfielder][Boundary.minX] = 3;
            this.playerBoundaries[Position.CentralMidfielder][Boundary.maxX] = 6;
            this.playerBoundaries[Position.CentralMidfielder][Boundary.minY] = 2;
            this.playerBoundaries[Position.CentralMidfielder][Boundary.maxY] = 4;

            // Left Midfielder
            this.playerBoundaries[Position.LeftMidfielder] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.LeftMidfielder][Boundary.minX] = 3;
            this.playerBoundaries[Position.LeftMidfielder][Boundary.maxX] = 6;
            this.playerBoundaries[Position.LeftMidfielder][Boundary.minY] = 5;
            this.playerBoundaries[Position.LeftMidfielder][Boundary.maxY] = 6;

            // Striker
            this.playerBoundaries[Position.Striker] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.Striker][Boundary.minX] = 7;
            this.playerBoundaries[Position.Striker][Boundary.maxX] = 9;
            this.playerBoundaries[Position.Striker][Boundary.minY] = 0;
            this.playerBoundaries[Position.Striker][Boundary.maxY] = 3;

            // Attacking Midfielder
            this.playerBoundaries[Position.AttackingMidfielder] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.AttackingMidfielder][Boundary.minX] = 7;
            this.playerBoundaries[Position.AttackingMidfielder][Boundary.maxX] = 9;
            this.playerBoundaries[Position.AttackingMidfielder][Boundary.minY] = 3;
            this.playerBoundaries[Position.AttackingMidfielder][Boundary.maxY] = 6;
        }

        private void setPlayerPassingTargets()
        {
            // Set the 3 players that each player can pass to
            this.playerPassingTargets = new Dictionary<Position, List<Position>>();
            
            // GoalKeeper
            this.playerPassingTargets[Position.Goalkeeper] = new List<Position>();
            this.playerPassingTargets[Position.Goalkeeper].Add(Position.CenterBack);
            this.playerPassingTargets[Position.Goalkeeper].Add(Position.Sweeper);
            this.playerPassingTargets[Position.Goalkeeper].Add(Position.DefensiveMidfielder);

            // Right Fullback 
            this.playerPassingTargets[Position.RightFullback] = new List<Position>();
            this.playerPassingTargets[Position.RightFullback].Add(Position.CenterBack);
            this.playerPassingTargets[Position.RightFullback].Add(Position.RightMidfielder);
            this.playerPassingTargets[Position.RightFullback].Add(Position.DefensiveMidfielder);

            // Center Back
            this.playerPassingTargets[Position.CenterBack] = new List<Position>();
            this.playerPassingTargets[Position.CenterBack].Add(Position.RightFullback);
            this.playerPassingTargets[Position.CenterBack].Add(Position.Sweeper);
            this.playerPassingTargets[Position.CenterBack].Add(Position.DefensiveMidfielder);

            // Sweeper
            this.playerPassingTargets[Position.Sweeper] = new List<Position>();
            this.playerPassingTargets[Position.Sweeper].Add(Position.CenterBack);
            this.playerPassingTargets[Position.Sweeper].Add(Position.LeftFullback);
            this.playerPassingTargets[Position.Sweeper].Add(Position.DefensiveMidfielder);

            // Left Fullback
            this.playerPassingTargets[Position.LeftFullback] = new List<Position>();
            this.playerPassingTargets[Position.LeftFullback].Add(Position.Sweeper);
            this.playerPassingTargets[Position.LeftFullback].Add(Position.LeftMidfielder);
            this.playerPassingTargets[Position.LeftFullback].Add(Position.DefensiveMidfielder);

            // Right Midfielder
            this.playerPassingTargets[Position.RightMidfielder] = new List<Position>();
            this.playerPassingTargets[Position.RightMidfielder].Add(Position.CentralMidfielder);
            this.playerPassingTargets[Position.RightMidfielder].Add(Position.DefensiveMidfielder);
            this.playerPassingTargets[Position.RightMidfielder].Add(Position.Striker);

            // Defensive Midfielder
            this.playerPassingTargets[Position.DefensiveMidfielder] = new List<Position>();
            this.playerPassingTargets[Position.DefensiveMidfielder].Add(Position.RightMidfielder);
            this.playerPassingTargets[Position.DefensiveMidfielder].Add(Position.LeftMidfielder);
            this.playerPassingTargets[Position.DefensiveMidfielder].Add(Position.CentralMidfielder);

            // Central Midfielder
            this.playerPassingTargets[Position.CentralMidfielder] = new List<Position>();
            this.playerPassingTargets[Position.CentralMidfielder].Add(Position.DefensiveMidfielder);
            this.playerPassingTargets[Position.CentralMidfielder].Add(Position.Striker);
            this.playerPassingTargets[Position.CentralMidfielder].Add(Position.AttackingMidfielder);

            // Left Midfielder
            this.playerPassingTargets[Position.LeftMidfielder] = new List<Position>();
            this.playerPassingTargets[Position.LeftMidfielder].Add(Position.CentralMidfielder);
            this.playerPassingTargets[Position.LeftMidfielder].Add(Position.DefensiveMidfielder);
            this.playerPassingTargets[Position.LeftMidfielder].Add(Position.AttackingMidfielder);

            // Striker
            this.playerPassingTargets[Position.Striker] = new List<Position>();
            this.playerPassingTargets[Position.Striker].Add(Position.RightMidfielder);
            this.playerPassingTargets[Position.Striker].Add(Position.CentralMidfielder);
            this.playerPassingTargets[Position.Striker].Add(Position.AttackingMidfielder);

            // Attacking Midfielder
            this.playerPassingTargets[Position.AttackingMidfielder] = new List<Position>();
            this.playerPassingTargets[Position.AttackingMidfielder].Add(Position.LeftMidfielder);
            this.playerPassingTargets[Position.AttackingMidfielder].Add(Position.CentralMidfielder);
            this.playerPassingTargets[Position.AttackingMidfielder].Add(Position.Striker);
        }

        private void setPlayerStats()
        {
            // Set the player statistics of each of the 11 players
            // 
            // Each player has 5 skill components: 
            // 1. Shooting
            // 2. Passing
            // 3. Ball Handling
            // 4. Defence
            // 5. Speed

            this.playerStats = new Dictionary<Position, Dictionary<Skill, int>>();
            
            // GoalKeeper
            this.playerStats[Position.Goalkeeper] = new Dictionary<Skill, int>();
            this.playerStats[Position.Goalkeeper][Skill.Shooting] = 20;
            this.playerStats[Position.Goalkeeper][Skill.Passing] = 90;
            this.playerStats[Position.Goalkeeper][Skill.BallHandling] = 120;
            this.playerStats[Position.Goalkeeper][Skill.Defence] = 220;
            this.playerStats[Position.Goalkeeper][Skill.Speed] = 90;

            // Right Fullback
            this.playerStats[Position.RightFullback] = new Dictionary<Skill, int>();
            this.playerStats[Position.RightFullback][Skill.Shooting] = 60;
            this.playerStats[Position.RightFullback][Skill.Passing] = 55;
            this.playerStats[Position.RightFullback][Skill.BallHandling] = 90;
            this.playerStats[Position.RightFullback][Skill.Defence] = 100;
            this.playerStats[Position.RightFullback][Skill.Speed] = 80;

            // Center Back
            this.playerStats[Position.CenterBack] = new Dictionary<Skill, int>();
            this.playerStats[Position.CenterBack][Skill.Shooting] = 60;
            this.playerStats[Position.CenterBack][Skill.Passing] = 55;
            this.playerStats[Position.CenterBack][Skill.BallHandling] = 90;
            this.playerStats[Position.CenterBack][Skill.Defence] = 110;
            this.playerStats[Position.CenterBack][Skill.Speed] = 80;

            // Sweeper
            this.playerStats[Position.Sweeper] = new Dictionary<Skill, int>();
            this.playerStats[Position.Sweeper][Skill.Shooting] = 60;
            this.playerStats[Position.Sweeper][Skill.Passing] = 75;
            this.playerStats[Position.Sweeper][Skill.BallHandling] = 110;
            this.playerStats[Position.Sweeper][Skill.Defence] = 95;
            this.playerStats[Position.Sweeper][Skill.Speed] = 90;

            // Left Fullback
            this.playerStats[Position.LeftFullback] = new Dictionary<Skill, int>();
            this.playerStats[Position.LeftFullback][Skill.Shooting] = 65;
            this.playerStats[Position.LeftFullback][Skill.Passing] = 60;
            this.playerStats[Position.LeftFullback][Skill.BallHandling] = 95;
            this.playerStats[Position.LeftFullback][Skill.Defence] = 130;
            this.playerStats[Position.LeftFullback][Skill.Speed] = 70;

            // Right Midfielder
            this.playerStats[Position.RightMidfielder] = new Dictionary<Skill, int>();
            this.playerStats[Position.RightMidfielder][Skill.Shooting] = 65;
            this.playerStats[Position.RightMidfielder][Skill.Passing] = 120;
            this.playerStats[Position.RightMidfielder][Skill.BallHandling] = 150;
            this.playerStats[Position.RightMidfielder][Skill.Defence] = 65;
            this.playerStats[Position.RightMidfielder][Skill.Speed] = 90;

            // Defensive Midfielder
            this.playerStats[Position.DefensiveMidfielder] = new Dictionary<Skill, int>();
            this.playerStats[Position.DefensiveMidfielder][Skill.Shooting] = 75;
            this.playerStats[Position.DefensiveMidfielder][Skill.Passing] = 100;
            this.playerStats[Position.DefensiveMidfielder][Skill.BallHandling] = 130;
            this.playerStats[Position.DefensiveMidfielder][Skill.Defence] = 115;
            this.playerStats[Position.DefensiveMidfielder][Skill.Speed] = 120;

            // Central Midfielder
            this.playerStats[Position.CentralMidfielder] = new Dictionary<Skill, int>();
            this.playerStats[Position.CentralMidfielder][Skill.Shooting] = 100;
            this.playerStats[Position.CentralMidfielder][Skill.Passing] = 130;
            this.playerStats[Position.CentralMidfielder][Skill.BallHandling] = 160;
            this.playerStats[Position.CentralMidfielder][Skill.Defence] = 95;
            this.playerStats[Position.CentralMidfielder][Skill.Speed] = 140;

            // Left Midfielder
            this.playerStats[Position.LeftMidfielder] = new Dictionary<Skill, int>();
            this.playerStats[Position.LeftMidfielder][Skill.Shooting] = 70;
            this.playerStats[Position.LeftMidfielder][Skill.Passing] = 115;
            this.playerStats[Position.LeftMidfielder][Skill.BallHandling] = 140;
            this.playerStats[Position.LeftMidfielder][Skill.Defence] = 55;
            this.playerStats[Position.LeftMidfielder][Skill.Speed] = 80;

            // Striker
            this.playerStats[Position.Striker] = new Dictionary<Skill, int>();
            this.playerStats[Position.Striker][Skill.Shooting] = 210;
            this.playerStats[Position.Striker][Skill.Passing] = 180;
            this.playerStats[Position.Striker][Skill.BallHandling] = 125;
            this.playerStats[Position.Striker][Skill.Defence] = 40;
            this.playerStats[Position.Striker][Skill.Speed] = 140;

            // Attacking Midfielder
            this.playerStats[Position.AttackingMidfielder] = new Dictionary<Skill, int>();
            this.playerStats[Position.AttackingMidfielder][Skill.Shooting] = 155;
            this.playerStats[Position.AttackingMidfielder][Skill.Passing] = 165;
            this.playerStats[Position.AttackingMidfielder][Skill.BallHandling] = 150;
            this.playerStats[Position.AttackingMidfielder][Skill.Defence] = 50;
            this.playerStats[Position.AttackingMidfielder][Skill.Speed] = 120;
        }

        public int getGoalLocation()
        {
            return this.convertToLocationId(this.goalCoordinate);
        }
        
        public int[] getPlayerStartingLocations()
        {
            // Returns the start positions of all the players 

            int[] locations = new int[11];

            // Goalkeeper
            locations[(int)Position.Goalkeeper] = this.convertToLocationId(new int[] {0, 3});

            // Right Fullback
            locations[(int)Position.RightFullback] = this.convertToLocationId(new int[] {2, 1});
            
            // CenterBack
            locations[(int)Position.CenterBack] = this.convertToLocationId(new int[] {1, 2});
            
            // Sweeper
            locations[(int)Position.Sweeper] = this.convertToLocationId(new int[] {1, 4});
            
            // Left Fullback
            locations[(int)Position.LeftFullback] = this.convertToLocationId(new int[] {2, 5});
            
            // Right Midfielder
            locations[(int)Position.RightMidfielder] = this.convertToLocationId(new int[] {5, 0});
            
            // Defensive Midfielder
            locations[(int)Position.DefensiveMidfielder] = this.convertToLocationId(new int[] {4, 2});
            
            // Central Midfielder
            locations[(int)Position.CentralMidfielder] = this.convertToLocationId(new int[] {4, 4});            
            
            // Left Midfielder
            locations[(int)Position.LeftMidfielder] = this.convertToLocationId(new int[] {5, 5});

            // Striker
            locations[(int)Position.Striker] = this.convertToLocationId(new int[] {8, 2});
            
            // Attacking Midfielder
            locations[(int)Position.AttackingMidfielder] = this.convertToLocationId(new int[] {8, 4});

            return locations;
        }

        public int getBallHandlingSkill(int playerPositionId)
        {
            Position playerPosition = (Position)playerPositionId;
            return this.playerStats[playerPosition][Skill.BallHandling];
        }

        public int getDefenceSkill(int playerPositionId)
        {
            Position playerPosition = (Position)playerPositionId;
            return this.playerStats[playerPosition][Skill.Defence];
        }

        public bool isBallInPlayerBoundary(int playerPositionId, int ballLocationId)
        {
            // Check if the ball is located within the boundaries of the player, 
            // whether the ball is in the same zone

            Position playerPosition = (Position)playerPositionId;

            int minX = this.playerBoundaries[playerPosition][Boundary.minX];
            int maxX = this.playerBoundaries[playerPosition][Boundary.maxX];
            int minY = this.playerBoundaries[playerPosition][Boundary.minY];
            int maxY = this.playerBoundaries[playerPosition][Boundary.maxY];

            int[] ballCoordinate = this.convertToCoordinate(ballLocationId);
            int ballX = ballCoordinate[0];
            int ballY = ballCoordinate[1];

            return (ballX >= minX && ballX <= maxX && ballY >= minY && ballY <= maxY);
        }
        
        public bool isBallWithPlayer(int playerPositionId, int[] possession)
        {
            Team possessionTeam = (Team)possession[0];
            Position possessionPlayerPosition = (Position)possession[1];

            return (possessionTeam == this.team && possessionPlayerPosition == (Position)playerPositionId);
        }

        public bool isBallWithTeammate(int playerPositionId, int[] possession)
        {
            Team possessionTeam = (Team)possession[0];
            Position possessionPlayerPosition = (Position)possession[1];
            
            return (possessionTeam == this.team && possessionPlayerPosition != (Position)playerPositionId);
        }

        public int[] probActionWithPlayerPossession(int playerPositionId, int[] teamLocationIds)
        {
            // When the player has possession of the ball, 
            // determine the probabilities of the player dribbling, passing or shooting
            // Probabilities: [DRIBBLE, PASS, SHOOT]
            int[] result = new int[] { 0, 0, 0 };

            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            if (playerCoordinate[0] < 3)
            {
                result[0] = 30;
                result[1] = 70;
            }

            if (playerCoordinate[0] < 7)
            {
                result[0] = 40;
                result[1] = 50;
                result[2] = 10;
            }

            else
            {   
                result[0] = 20;
                result[1] = 20;
                result[2] = 60;
            }

            // result[0] = 10;
            // result[1] = 10;
            // result[2] = 80;

            return result;
        }

        public int[] probDribble(int playerPositionId, int[] teamLocationIds)
        {
            // Given player possession of the ball,
            // returns the probabilities of dribbling in different directions
            // Probabilities: [UP, RIGHT, DOWN, RIGHT, STAY]

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            // Move in the direction of the goal
            return this.moveTowards((int)playerPosition, playerCoordinate, this.goalCoordinate);
        }

        public int[] getPassingTargets(int playerPositionId)
        { 
            // Returns the 3 passing targets of a player
            
            Position playerPosition = (Position)playerPositionId;
            Position T1Position = this.playerPassingTargets[playerPosition][0];
            Position T2Position = this.playerPassingTargets[playerPosition][1];
            Position T3Position = this.playerPassingTargets[playerPosition][2];
            return new int[] { (int)T1Position, (int)T2Position, (int)T3Position };
        }

        public int[] probPassing(int playerPositionId, int[] teamLocationIds)
        {
            // Returns the probabilities of passing the ball to either of the 3 targets
            // Probabilities: [Target 1, Target 2, Target 3]
            int[] result = {0, 0, 0};

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            List<Position> teammates = this.playerPassingTargets[playerPosition];

            double minSumDist = Double.MaxValue;
            Position selectedTeammate = teammates[2];
            int selectedIndex = 2;

            for (int i = 0; i < 3; i++)
            {   
                // Teammate location coordinate
                Position teammate = teammates[i];
                int[] teammateCoordinate = this.getPlayerCoordinate((int)teammate, teamLocationIds);

                // Teammate's distance to player
                double distToPlayer = this.getDistance(teammateCoordinate, playerCoordinate);
                
                // Teammate's distance to goal
                double distToGoal = this.getDistance(teammateCoordinate, this.goalCoordinate);
                
                // Sum of distance:
                // Player --> Teammate --> Goal
                double sumDist = distToPlayer + distToGoal;
                
                if (sumDist < minSumDist)
                {
                    minSumDist = sumDist;
                    selectedTeammate = teammate;
                    selectedIndex = i;
                }
            }
            
            // Pass the ball to the player with the lowest sumDist
            result[selectedIndex] = 1;
            return result;
        }

        public int[] probPassTo(int playerPositionId, int teammatePositionId, int[] teamLocationIds)
        {
            // Given the player's decision to pass the ball to the teammate, 
            // calculate the probability of success
            // Probabilities: [SUCCESS, FAIL]
            int[] result = {0, 0};

            Position playerPosition = (Position)playerPositionId;
            
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);
            int[] teammateCoordinate = this.getPlayerCoordinate(teammatePositionId, teamLocationIds);

            int passingSkill = this.playerStats[playerPosition][Skill.Passing];
            double distToTeammate = this.getDistance(playerCoordinate, teammateCoordinate);

            // Adjust the probabilities of a successfully pass based on distance to teammate

            if (distToTeammate <= 2)
            {
                result[0] = this.updateWithSkill(9, passingSkill);
                result[1] = 1;
            }

            if (distToTeammate <= 3)
            {
                result[0] = this.updateWithSkill(8, passingSkill);
                result[1] = 2;
            }

            if (distToTeammate <= 4)
            {
                result[0] = this.updateWithSkill(7, passingSkill);
                result[1] = 3;
            }

            else
            {
                result[0] = this.updateWithSkill(5, passingSkill);
                result[1] = 5;
            }

            return result;
        }

        public int failPassBallLocation(int teammatePositionId, int[] teamLocationIds)
        {
            // When a pass fails, returns the location that the ball ends up
            // determined with respect to the location of the target teammate

            int[] teammateCoordinate = this.getPlayerCoordinate(teammatePositionId, teamLocationIds);
            Random rnd = new Random();

            int x = rnd.Next(teammateCoordinate[0] - 1, teammateCoordinate[0] + 2);
            x = Math.Min(x, this.fieldMaxX);
            x = Math.Max(x, this.fieldMinX);

            int y = rnd.Next(teammateCoordinate[1] - 1, teammateCoordinate[1] + 1);
            y = Math.Min(y, this.fieldMaxY);
            y = Math.Max(y, this.fieldMinY);

            int[] newCoordinate = new int[] { x, y };
            return this.convertToLocationId(newCoordinate);
        }

        public int[] probShoot(int playerPositionId, int[] teamLocationIds)
        {
            // Given the player's decision to shoot the ball, 
            // calculate the probability of success
            // Probabilities: [SUCCESS, FAIL]

            int[] result = {0, 0};

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            int shootingSkill = this.playerStats[playerPosition][Skill.Shooting];
            double distToGoal = this.getDistance(playerCoordinate, this.goalCoordinate);

            // Adjust the probabilities of a successfully pass based on distance to goal

            if (distToGoal == 0)
            {
                result[0] = this.updateWithSkill(9, shootingSkill);
                result[1] = 1;
            }

            if (distToGoal <= 1)
            {
                result[0] = this.updateWithSkill(8, shootingSkill);
                result[1] = 2;
            }

            if (distToGoal <= 2)
            {
                result[0] = this.updateWithSkill(7, shootingSkill);
                result[1] = 3;
            }

            if (distToGoal <= 3)
            {
                result[0] = this.updateWithSkill(5, shootingSkill);
                result[1] = 5;
            }

            if (distToGoal <= 4)
            {
                result[0] = this.updateWithSkill(3, shootingSkill);
                result[1] = 7;
            }

            else
            {
                result[0] = 0;
                result[1] = 1;
            }

            return result;
        }

        public int failGoalBallLocation()
        {
            // When a goal attempt fails, returns the location that the ball ends up
            // determined with respect to the location of the goalpost

            Random rnd = new Random();
            int x = rnd.Next(7, 10);
            int y = rnd.Next(0, 7);

            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            return newLocationId;
        }

        public int[] probAdvance(int playerPositionId, int[] teamLocationIds)
        {
            // Given teammate with possession of the ball, 
            // returns the probabilities of advancing in different directions
            // Probabilities: [UP, RIGHT, DOWN, RIGHT, STAY]

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);
            
            // Move in the direction of the goal
            return this.moveTowards((int)playerPosition, playerCoordinate, this.goalCoordinate);
        }

        public int[] probMove(int playerPositionId, int[] teamLocationIds, int ballLocationId)
        {
            // Given no possession of the ball, 
            // returns the probabilities of moving in different directions
            // Probabilities: [UP, RIGHT, DOWN, RIGHT, STAY]

            int playerLocationId = teamLocationIds[playerPositionId];
            int[] playerCoordinate = this.convertToCoordinate(playerLocationId);
            int[] ballCoordinate = this.convertToCoordinate(ballLocationId);

            // Move in the direction of the ball
            return this.moveTowards(playerPositionId, playerCoordinate, ballCoordinate);
        }

        public int[] moveTowards(int playerPositionId, int[] playerCoordinate, int[] targetCoordinate)
        {

            // Given a player, current coordinate and a target coordinate, 
            // returns the probabilities of moving in different directions
            // Probabilities: [UP, RIGHT, DOWN, RIGHT, STAY]

            int[] result = {0, 0, 0, 0, 0};

            // Player Position
            Position playerPosition = (Position)playerPositionId;

            // Boundaries for player
            int minX = this.playerBoundaries[playerPosition][Boundary.minX];
            int maxX = this.playerBoundaries[playerPosition][Boundary.maxX];
            int minY = this.playerBoundaries[playerPosition][Boundary.minY];
            int maxY = this.playerBoundaries[playerPosition][Boundary.maxY];

            // Speed of the player
            int speedSkill = this.playerStats[playerPosition][Skill.Speed];

            //  Target is up
            if (targetCoordinate[1] > playerCoordinate[1]) { result[0] = 1; }
            // Target is right 
            if (targetCoordinate[0] > playerCoordinate[0]) { result[1] = 1; }
            // Target is down
            if (targetCoordinate[1] < playerCoordinate[1]) { result[2] = 1; }
            // Target is left
            if (targetCoordinate[0] < playerCoordinate[0]) { result[3] = 1; }

            // Player cannot go up
            if (playerCoordinate[1] == maxY) { result[0] = 0; }
            // Player cannot go right
            if (playerCoordinate[0] == maxX) { result[1] = 0; }
            // Player cannot go down
            if (playerCoordinate[1] == minY) { result[2] = 0; }
            // Player cannot go left
            if (playerCoordinate[0] == minX) { result[3] = 0; }

            // Update based on speed
            result[0] = this.updateWithSkill(result[0], speedSkill);
            result[1] = this.updateWithSkill(result[1], speedSkill);
            result[2] = this.updateWithSkill(result[2], speedSkill);
            result[3] = this.updateWithSkill(result[3], speedSkill);

            // If no direction is viable, stay at current location
            int resultSum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                resultSum += result[i];
            }
            if (resultSum == 0)
            {
                result[4] = 1;
            }
            
            return result;
        }

        public int[] playerMoveUp(int playerPositionId, int[] teamLocationIds)
        {
            // Player moves up. Returns the new teamLocationIds.

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            int maxY = this.playerBoundaries[playerPosition][Boundary.maxY];

            int x = playerCoordinate[0];
            int y = Math.Min(playerCoordinate[1] + 1, maxY);
            
            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            
            teamLocationIds[playerPositionId] = newLocationId;

            return teamLocationIds;
        }

        public int[] playerMoveRight(int playerPositionId, int[] teamLocationIds)
        {

            // Player moves right. Returns the new teamLocationIds.

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            int maxX = this.playerBoundaries[playerPosition][Boundary.maxX];

            int x = Math.Min(playerCoordinate[0] + 1, maxX);
            int y = playerCoordinate[1];
            
            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            
            teamLocationIds[playerPositionId] = newLocationId;

            return teamLocationIds;
        }

        public int[] playerMoveDown(int playerPositionId, int[] teamLocationIds)
        {
            // Player moves down. Returns the new teamLocationIds.

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            int minY = this.playerBoundaries[playerPosition][Boundary.minY];

            int x = playerCoordinate[0];
            int y = Math.Max(playerCoordinate[1] - 1, minY);
            
            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            
            teamLocationIds[playerPositionId] = newLocationId;

            return teamLocationIds;
        }

        public int[] playerMoveLeft(int playerPositionId, int[] teamLocationIds)
        {

            // Player moves left. Returns the new teamLocationIds.

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            int minX = this.playerBoundaries[playerPosition][Boundary.minX];

            int x = Math.Max(playerCoordinate[0] - 1, minX);
            int y = playerCoordinate[1];
            
            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            
            teamLocationIds[playerPositionId] = newLocationId;
            return teamLocationIds;
        }

        public int ballMoveUp(int ball)
        {
            // Ball moves up. Returns the new ball locationId.

            int[] ballCoordinate = this.convertToCoordinate(ball);
            
            int x = ballCoordinate[0];
            int y = Math.Min(ballCoordinate[1] + 1, this.fieldMaxY);

            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            return newLocationId;
        }

        public int ballMoveRight(int ball)
        {
            // Ball moves right. Returns the new ball locationId.

            int[] ballCoordinate = this.convertToCoordinate(ball);
            
            int x = Math.Min(ballCoordinate[0] + 1, this.fieldMaxX);
            int y = ballCoordinate[1];

            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            return newLocationId;
        }

        public int ballMoveDown(int ball)
        {
            // Ball moves down. Returns the new ball locationId.

            int[] ballCoordinate = this.convertToCoordinate(ball);
            
            int x = ballCoordinate[0];
            int y = Math.Max(ballCoordinate[1] - 1, this.fieldMinY);

            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            return newLocationId;
        }

        public int ballMoveLeft(int ball)
        {
            // Ball moves left. Returns the new ball locationId.

            int[] ballCoordinate = this.convertToCoordinate(ball);
            
            int x = Math.Max(ballCoordinate[0] - 1, this.fieldMinX);
            int y = ballCoordinate[1];

            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            return newLocationId;
        }

        public int getLocation(int playerPositionId, int[] teamA)
        {   
            // Returns the locationId of the player
            return teamA[playerPositionId];
        }

        public int[] getPlayerCoordinate(int playerPositionId, int[] teamLocationIds)
        {
            Position playerPosition = (Position)playerPositionId;
            int playerLocationId = teamLocationIds[playerPositionId];
            return this.convertToCoordinate(playerLocationId);
        }

        public double getDistance(int[] point1, int[] point2) 
        {
            // Returns the distance between two points
            return Math.Sqrt(Math.Pow(point1[0] - point2[0], 2) + Math.Pow(point1[1] - point2[1], 2));
        }

        private int updateWithSkill(int weight, int skill)
        {
            // Update a probability weight with therespective skill point
            double skillEffect = Convert.ToDouble(skill) / 50;
            return Convert.ToInt32(Math.Round(weight * skillEffect));
        }

        private int convertToLocationId(int[] coordinate)
        {
            // Converts xy-coordinate to locationId
            return coordinate[1] * 10 + coordinate[0]; 
        }

        private int[] convertToCoordinate(int locationId)
        {
            // Converts locationId to xy-coordinate
            int x = locationId % 10;
            int y = locationId / 10;
            return new int[] {x, y};
        }

        public override string ToString()
        {
            return "";
        }

        public override ExpressionValue GetClone()
        {
            return this;
        }

        public override string ExpressionID
        {
            get {return ""; }
        }
    }
    
    public class ManagerB: ExpressionValue
    {
        private Team team;
    	private int fieldMinX;
        private int fieldMaxX;
        private int fieldMinY;
        private int fieldMaxY;
        private int[] goalCoordinate;
        private Dictionary<Position, Dictionary<Skill, int>> playerStats;
        private Dictionary<Position, Dictionary<Boundary, int>> playerBoundaries;
        private Dictionary<Position, List<Position>> playerPassingTargets;

        public ManagerB() 
        {
            this.team = Team.B;

            this.setGoal();
            this.setFieldBoundaries();
            this.setPlayerBoundaries();
            this.setPlayerPassingTargets();
            this.setPlayerStats();
            
            // this.boostDefence();
        }

        private void boostDefence()
        {
            for (int i = 0; i < 11; i++) 
            {
                this.playerStats[(Position)i][Skill.Defence] += 100;
            }
        }

        private void setGoal()
        {
            // Set the coordinate location of the goal
            this.goalCoordinate = new int[] {0, 3};
        }

        private void setFieldBoundaries()
        {
            // Set the boundaries of the entire playing field
            this.fieldMinX = 0;
            this.fieldMaxX = 9;
            this.fieldMinY = 0;
            this.fieldMaxY = 6;
        }

        private void setPlayerBoundaries()
        {
            // Set the boundaries for each player based on position
            // Team B follows a 4-4-2 Formation
            this.playerBoundaries = new Dictionary<Position, Dictionary<Boundary, int>>();
            
            // GoalKeeper
            this.playerBoundaries[Position.Goalkeeper] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.Goalkeeper][Boundary.minX] = 8;
            this.playerBoundaries[Position.Goalkeeper][Boundary.maxX] = 9;
            this.playerBoundaries[Position.Goalkeeper][Boundary.minY] = 2;
            this.playerBoundaries[Position.Goalkeeper][Boundary.maxY] = 4;

            // Right Fullback 
            this.playerBoundaries[Position.RightFullback] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.RightFullback][Boundary.minX] = 7;
            this.playerBoundaries[Position.RightFullback][Boundary.maxX] = 9;
            this.playerBoundaries[Position.RightFullback][Boundary.minY] = 4;
            this.playerBoundaries[Position.RightFullback][Boundary.maxY] = 6;

            // Center Back
            this.playerBoundaries[Position.CenterBack] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.CenterBack][Boundary.minX] = 7;
            this.playerBoundaries[Position.CenterBack][Boundary.maxX] = 9;
            this.playerBoundaries[Position.CenterBack][Boundary.minY] = 3;
            this.playerBoundaries[Position.CenterBack][Boundary.maxY] = 5;

            // Sweeper
            this.playerBoundaries[Position.Sweeper] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.Sweeper][Boundary.minX] = 7;
            this.playerBoundaries[Position.Sweeper][Boundary.maxX] = 9;
            this.playerBoundaries[Position.Sweeper][Boundary.minY] = 1;
            this.playerBoundaries[Position.Sweeper][Boundary.maxY] = 3;

            // Left Fullback
            this.playerBoundaries[Position.LeftFullback] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.LeftFullback][Boundary.minX] = 7;
            this.playerBoundaries[Position.LeftFullback][Boundary.maxX] = 9;
            this.playerBoundaries[Position.LeftFullback][Boundary.minY] = 0;
            this.playerBoundaries[Position.LeftFullback][Boundary.maxY] = 2;

            // Right Midfielder
            this.playerBoundaries[Position.RightMidfielder] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.RightMidfielder][Boundary.minX] = 4;
            this.playerBoundaries[Position.RightMidfielder][Boundary.maxX] = 6;
            this.playerBoundaries[Position.RightMidfielder][Boundary.minY] = 5;
            this.playerBoundaries[Position.RightMidfielder][Boundary.maxY] = 6;

            // Defensive Midfielder
            this.playerBoundaries[Position.DefensiveMidfielder] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.DefensiveMidfielder][Boundary.minX] = 4;
            this.playerBoundaries[Position.DefensiveMidfielder][Boundary.maxX] = 6;
            this.playerBoundaries[Position.DefensiveMidfielder][Boundary.minY] = 2;
            this.playerBoundaries[Position.DefensiveMidfielder][Boundary.maxY] = 4;

            // Central Midfielder
            this.playerBoundaries[Position.CentralMidfielder] = new Dictionary<Boundary, int>(); 
            this.playerBoundaries[Position.CentralMidfielder][Boundary.minX] = 4;
            this.playerBoundaries[Position.CentralMidfielder][Boundary.maxX] = 6;
            this.playerBoundaries[Position.CentralMidfielder][Boundary.minY] = 2;
            this.playerBoundaries[Position.CentralMidfielder][Boundary.maxY] = 4;

            // Left Midfielder
            this.playerBoundaries[Position.LeftMidfielder] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.LeftMidfielder][Boundary.minX] = 4;
            this.playerBoundaries[Position.LeftMidfielder][Boundary.maxX] = 6;
            this.playerBoundaries[Position.LeftMidfielder][Boundary.minY] = 0;
            this.playerBoundaries[Position.LeftMidfielder][Boundary.maxY] = 1;

            // Striker
            this.playerBoundaries[Position.Striker] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.Striker][Boundary.minX] = 0;
            this.playerBoundaries[Position.Striker][Boundary.maxX] = 2;
            this.playerBoundaries[Position.Striker][Boundary.minY] = 3;
            this.playerBoundaries[Position.Striker][Boundary.maxY] = 6;

            // Attacking Midfielder
            this.playerBoundaries[Position.AttackingMidfielder] = new Dictionary<Boundary, int>();
            this.playerBoundaries[Position.AttackingMidfielder][Boundary.minX] = 0;
            this.playerBoundaries[Position.AttackingMidfielder][Boundary.maxX] = 2;
            this.playerBoundaries[Position.AttackingMidfielder][Boundary.minY] = 0;
            this.playerBoundaries[Position.AttackingMidfielder][Boundary.maxY] = 3;
        }

        private void setPlayerPassingTargets()
        {
            // Set the 3 players that each player can pass to
            this.playerPassingTargets = new Dictionary<Position, List<Position>>();
            
            // GoalKeeper
            this.playerPassingTargets[Position.Goalkeeper] = new List<Position>();
            this.playerPassingTargets[Position.Goalkeeper].Add(Position.CenterBack);
            this.playerPassingTargets[Position.Goalkeeper].Add(Position.Sweeper);
            this.playerPassingTargets[Position.Goalkeeper].Add(Position.DefensiveMidfielder);

            // Right Fullback 
            this.playerPassingTargets[Position.RightFullback] = new List<Position>();
            this.playerPassingTargets[Position.RightFullback].Add(Position.CenterBack);
            this.playerPassingTargets[Position.RightFullback].Add(Position.RightMidfielder);
            this.playerPassingTargets[Position.RightFullback].Add(Position.DefensiveMidfielder);

            // Center Back
            this.playerPassingTargets[Position.CenterBack] = new List<Position>();
            this.playerPassingTargets[Position.CenterBack].Add(Position.RightFullback);
            this.playerPassingTargets[Position.CenterBack].Add(Position.Sweeper);
            this.playerPassingTargets[Position.CenterBack].Add(Position.DefensiveMidfielder);

            // Sweeper
            this.playerPassingTargets[Position.Sweeper] = new List<Position>();
            this.playerPassingTargets[Position.Sweeper].Add(Position.CenterBack);
            this.playerPassingTargets[Position.Sweeper].Add(Position.LeftFullback);
            this.playerPassingTargets[Position.Sweeper].Add(Position.DefensiveMidfielder);

            // Left Fullback
            this.playerPassingTargets[Position.LeftFullback] = new List<Position>();
            this.playerPassingTargets[Position.LeftFullback].Add(Position.Sweeper);
            this.playerPassingTargets[Position.LeftFullback].Add(Position.LeftMidfielder);
            this.playerPassingTargets[Position.LeftFullback].Add(Position.DefensiveMidfielder);

            // Right Midfielder
            this.playerPassingTargets[Position.RightMidfielder] = new List<Position>();
            this.playerPassingTargets[Position.RightMidfielder].Add(Position.CentralMidfielder);
            this.playerPassingTargets[Position.RightMidfielder].Add(Position.DefensiveMidfielder);
            this.playerPassingTargets[Position.RightMidfielder].Add(Position.Striker);

            // Defensive Midfielder
            this.playerPassingTargets[Position.DefensiveMidfielder] = new List<Position>();
            this.playerPassingTargets[Position.DefensiveMidfielder].Add(Position.RightMidfielder);
            this.playerPassingTargets[Position.DefensiveMidfielder].Add(Position.LeftMidfielder);
            this.playerPassingTargets[Position.DefensiveMidfielder].Add(Position.CentralMidfielder);

            // Central Midfielder
            this.playerPassingTargets[Position.CentralMidfielder] = new List<Position>();
            this.playerPassingTargets[Position.CentralMidfielder].Add(Position.DefensiveMidfielder);
            this.playerPassingTargets[Position.CentralMidfielder].Add(Position.Striker);
            this.playerPassingTargets[Position.CentralMidfielder].Add(Position.AttackingMidfielder);

            // Left Midfielder
            this.playerPassingTargets[Position.LeftMidfielder] = new List<Position>();
            this.playerPassingTargets[Position.LeftMidfielder].Add(Position.CentralMidfielder);
            this.playerPassingTargets[Position.LeftMidfielder].Add(Position.DefensiveMidfielder);
            this.playerPassingTargets[Position.LeftMidfielder].Add(Position.AttackingMidfielder);

            // Striker
            this.playerPassingTargets[Position.Striker] = new List<Position>();
            this.playerPassingTargets[Position.Striker].Add(Position.RightMidfielder);
            this.playerPassingTargets[Position.Striker].Add(Position.CentralMidfielder);
            this.playerPassingTargets[Position.Striker].Add(Position.AttackingMidfielder);

            // Attacking Midfielder
            this.playerPassingTargets[Position.AttackingMidfielder] = new List<Position>();
            this.playerPassingTargets[Position.AttackingMidfielder].Add(Position.LeftMidfielder);
            this.playerPassingTargets[Position.AttackingMidfielder].Add(Position.CentralMidfielder);
            this.playerPassingTargets[Position.AttackingMidfielder].Add(Position.Striker);
        }

        private void setPlayerStats()
        {
            // Set the player statistics of each of the 11 players
            // 
            // Each player has 5 skill components: 
            // 1. Shooting
            // 2. Passing
            // 3. Ball Handling
            // 4. Defence
            // 5. Speed

            this.playerStats = new Dictionary<Position, Dictionary<Skill, int>>();
            
            // GoalKeeper
            this.playerStats[Position.Goalkeeper] = new Dictionary<Skill, int>();
            this.playerStats[Position.Goalkeeper][Skill.Shooting] = 20;
            this.playerStats[Position.Goalkeeper][Skill.Passing] = 90;
            this.playerStats[Position.Goalkeeper][Skill.BallHandling] = 120;
            this.playerStats[Position.Goalkeeper][Skill.Defence] = 220;
            this.playerStats[Position.Goalkeeper][Skill.Speed] = 90;

            // Right Fullback
            this.playerStats[Position.RightFullback] = new Dictionary<Skill, int>();
            this.playerStats[Position.RightFullback][Skill.Shooting] = 60;
            this.playerStats[Position.RightFullback][Skill.Passing] = 55;
            this.playerStats[Position.RightFullback][Skill.BallHandling] = 90;
            this.playerStats[Position.RightFullback][Skill.Defence] = 100;
            this.playerStats[Position.RightFullback][Skill.Speed] = 80;

            // Center Back
            this.playerStats[Position.CenterBack] = new Dictionary<Skill, int>();
            this.playerStats[Position.CenterBack][Skill.Shooting] = 60;
            this.playerStats[Position.CenterBack][Skill.Passing] = 55;
            this.playerStats[Position.CenterBack][Skill.BallHandling] = 90;
            this.playerStats[Position.CenterBack][Skill.Defence] = 110;
            this.playerStats[Position.CenterBack][Skill.Speed] = 80;

            // Sweeper
            this.playerStats[Position.Sweeper] = new Dictionary<Skill, int>();
            this.playerStats[Position.Sweeper][Skill.Shooting] = 60;
            this.playerStats[Position.Sweeper][Skill.Passing] = 75;
            this.playerStats[Position.Sweeper][Skill.BallHandling] = 110;
            this.playerStats[Position.Sweeper][Skill.Defence] = 95;
            this.playerStats[Position.Sweeper][Skill.Speed] = 90;

            // Left Fullback
            this.playerStats[Position.LeftFullback] = new Dictionary<Skill, int>();
            this.playerStats[Position.LeftFullback][Skill.Shooting] = 65;
            this.playerStats[Position.LeftFullback][Skill.Passing] = 60;
            this.playerStats[Position.LeftFullback][Skill.BallHandling] = 95;
            this.playerStats[Position.LeftFullback][Skill.Defence] = 130;
            this.playerStats[Position.LeftFullback][Skill.Speed] = 70;

            // Right Midfielder
            this.playerStats[Position.RightMidfielder] = new Dictionary<Skill, int>();
            this.playerStats[Position.RightMidfielder][Skill.Shooting] = 65;
            this.playerStats[Position.RightMidfielder][Skill.Passing] = 120;
            this.playerStats[Position.RightMidfielder][Skill.BallHandling] = 150;
            this.playerStats[Position.RightMidfielder][Skill.Defence] = 65;
            this.playerStats[Position.RightMidfielder][Skill.Speed] = 90;

            // Defensive Midfielder
            this.playerStats[Position.DefensiveMidfielder] = new Dictionary<Skill, int>();
            this.playerStats[Position.DefensiveMidfielder][Skill.Shooting] = 75;
            this.playerStats[Position.DefensiveMidfielder][Skill.Passing] = 100;
            this.playerStats[Position.DefensiveMidfielder][Skill.BallHandling] = 130;
            this.playerStats[Position.DefensiveMidfielder][Skill.Defence] = 115;
            this.playerStats[Position.DefensiveMidfielder][Skill.Speed] = 120;

            // Central Midfielder
            this.playerStats[Position.CentralMidfielder] = new Dictionary<Skill, int>();
            this.playerStats[Position.CentralMidfielder][Skill.Shooting] = 100;
            this.playerStats[Position.CentralMidfielder][Skill.Passing] = 130;
            this.playerStats[Position.CentralMidfielder][Skill.BallHandling] = 160;
            this.playerStats[Position.CentralMidfielder][Skill.Defence] = 95;
            this.playerStats[Position.CentralMidfielder][Skill.Speed] = 140;

            // Left Midfielder
            this.playerStats[Position.LeftMidfielder] = new Dictionary<Skill, int>();
            this.playerStats[Position.LeftMidfielder][Skill.Shooting] = 70;
            this.playerStats[Position.LeftMidfielder][Skill.Passing] = 115;
            this.playerStats[Position.LeftMidfielder][Skill.BallHandling] = 140;
            this.playerStats[Position.LeftMidfielder][Skill.Defence] = 55;
            this.playerStats[Position.LeftMidfielder][Skill.Speed] = 80;

            // Striker
            this.playerStats[Position.Striker] = new Dictionary<Skill, int>();
            this.playerStats[Position.Striker][Skill.Shooting] = 210;
            this.playerStats[Position.Striker][Skill.Passing] = 180;
            this.playerStats[Position.Striker][Skill.BallHandling] = 125;
            this.playerStats[Position.Striker][Skill.Defence] = 40;
            this.playerStats[Position.Striker][Skill.Speed] = 140;

            // Attacking Midfielder
            this.playerStats[Position.AttackingMidfielder] = new Dictionary<Skill, int>();
            this.playerStats[Position.AttackingMidfielder][Skill.Shooting] = 155;
            this.playerStats[Position.AttackingMidfielder][Skill.Passing] = 165;
            this.playerStats[Position.AttackingMidfielder][Skill.BallHandling] = 150;
            this.playerStats[Position.AttackingMidfielder][Skill.Defence] = 50;
            this.playerStats[Position.AttackingMidfielder][Skill.Speed] = 120;
        }

        public int getGoalLocation()
        {
            return this.convertToLocationId(this.goalCoordinate);
        }
        
        public int[] getPlayerStartingLocations()
        {
            // Returns the start positions of all the players 

            int[] locations = new int[11];

            // Goalkeeper
            locations[(int)Position.Goalkeeper] = this.convertToLocationId(new int[] {9, 3});

            // Right Fullback
            locations[(int)Position.RightFullback] = this.convertToLocationId(new int[] {7, 5});
            
            // CenterBack
            locations[(int)Position.CenterBack] = this.convertToLocationId(new int[] {8, 4});
            
            // Sweeper
            locations[(int)Position.Sweeper] = this.convertToLocationId(new int[] {8, 2});
            
            // Left Fullback
            locations[(int)Position.LeftFullback] = this.convertToLocationId(new int[] {7, 1});
            
            // Right Midfielder
            locations[(int)Position.RightMidfielder] = this.convertToLocationId(new int[] {4, 5});
            
            // Defensive Midfielder
            locations[(int)Position.DefensiveMidfielder] = this.convertToLocationId(new int[] {4, 4});
            
            // Central Midfielder
            locations[(int)Position.CentralMidfielder] = this.convertToLocationId(new int[] {4, 2});         
            
            // Left Midfielder
            locations[(int)Position.LeftMidfielder] = this.convertToLocationId(new int[] {4, 1});

            // Striker
            locations[(int)Position.Striker] = this.convertToLocationId(new int[] {2, 4});
            
            // Attacking Midfielder
            locations[(int)Position.AttackingMidfielder] = this.convertToLocationId(new int[] {2, 2});

            return locations;
        }

        public int getBallHandlingSkill(int playerPositionId)
        {
            Position playerPosition = (Position)playerPositionId;
            return this.playerStats[playerPosition][Skill.BallHandling];
        }

        public int getDefenceSkill(int playerPositionId)
        {
            Position playerPosition = (Position)playerPositionId;
            return this.playerStats[playerPosition][Skill.Defence];
        }

        public bool isBallInPlayerBoundary(int playerPositionId, int ballLocationId)
        {
            // Check if the ball is located within the boundaries of the player, 
            // whether the ball is in the same zone

            Position playerPosition = (Position)playerPositionId;

            int minX = this.playerBoundaries[playerPosition][Boundary.minX];
            int maxX = this.playerBoundaries[playerPosition][Boundary.maxX];
            int minY = this.playerBoundaries[playerPosition][Boundary.minY];
            int maxY = this.playerBoundaries[playerPosition][Boundary.maxY];

            int[] ballCoordinate = this.convertToCoordinate(ballLocationId);
            int ballX = ballCoordinate[0];
            int ballY = ballCoordinate[1];

            return (ballX >= minX && ballX <= maxX && ballY >= minY && ballY <= maxY);
        }
        
        public bool isBallWithPlayer(int playerPositionId, int[] possession)
        {
            Team possessionTeam = (Team)possession[0];
            Position possessionPlayerPosition = (Position)possession[1];

            return (possessionTeam == this.team && possessionPlayerPosition == (Position)playerPositionId);
        }

        public bool isBallWithTeammate(int playerPositionId, int[] possession)
        {
            Team possessionTeam = (Team)possession[0];
            Position possessionPlayerPosition = (Position)possession[1];
            
            return (possessionTeam == this.team && possessionPlayerPosition != (Position)playerPositionId);
        }

        public int[] probActionWithPlayerPossession(int playerPositionId, int[] teamLocationIds)
        {
            // When the player has possession of the ball, 
            // determine the probabilities of the player dribbling, passing or shooting
            // Probabilities: [DRIBBLE, PASS, SHOOT]
            int[] result = new int[] { 0, 0, 0 };

            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            if (playerCoordinate[0] > 6)
            {
                result[0] = 30;
                result[1] = 70;
            }

            if (playerCoordinate[0] > 3)
            {
                result[0] = 40;
                result[1] = 50;
                result[2] = 10;
            }

            else
            {   
                result[0] = 20;
                result[1] = 20;
                result[2] = 60;
            }

            return result;
        }

        public int[] probDribble(int playerPositionId, int[] teamLocationIds)
        {
            // Given player possession of the ball,
            // returns the probabilities of dribbling in different directions
            // Probabilities: [UP, RIGHT, DOWN, RIGHT, STAY]

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            // Move in the direction of the goal
            return this.moveTowards((int)playerPosition, playerCoordinate, this.goalCoordinate);
        }

        public int[] getPassingTargets(int playerPositionId)
        { 
            // Returns the 3 passing targets of a player
            
            Position playerPosition = (Position)playerPositionId;
            Position T1Position = this.playerPassingTargets[playerPosition][0];
            Position T2Position = this.playerPassingTargets[playerPosition][1];
            Position T3Position = this.playerPassingTargets[playerPosition][2];
            return new int[] { (int)T1Position, (int)T2Position, (int)T3Position };
        }

        public int[] probPassing(int playerPositionId, int[] teamLocationIds)
        {
            // Returns the probabilities of passing the ball to either of the 3 targets
            // Probabilities: [Target 1, Target 2, Target 3]
            int[] result = {0, 0, 0};

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            List<Position> teammates = this.playerPassingTargets[playerPosition];

            double minSumDist = Double.MaxValue;
            Position selectedTeammate = teammates[2];
            int selectedIndex = 2;

            for (int i = 0; i < 3; i++)
            {   
                // Teammate location coordinate
                Position teammate = teammates[i];
                int[] teammateCoordinate = this.getPlayerCoordinate((int)teammate, teamLocationIds);

                // Teammate's distance to player
                double distToPlayer = this.getDistance(teammateCoordinate, playerCoordinate);
                
                // Teammate's distance to goal
                double distToGoal = this.getDistance(teammateCoordinate, this.goalCoordinate);
                
                // Sum of distance:
                // Player --> Teammate --> Goal
                double sumDist = distToPlayer + distToGoal;
                
                if (sumDist < minSumDist)
                {
                    minSumDist = sumDist;
                    selectedTeammate = teammate;
                    selectedIndex = i;
                }
            }
            
            // Pass the ball to the player with the lowest sumDist
            result[selectedIndex] = 1;
            return result;
        }

        public int[] probPassTo(int playerPositionId, int teammatePositionId, int[] teamLocationIds)
        {
            // Given the player's decision to pass the ball to the teammate, 
            // calculate the probability of success
            // Probabilities: [SUCCESS, FAIL]
            int[] result = {0, 0};

            Position playerPosition = (Position)playerPositionId;
            
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);
            int[] teammateCoordinate = this.getPlayerCoordinate(teammatePositionId, teamLocationIds);

            int passingSkill = this.playerStats[playerPosition][Skill.Passing];
            double distToTeammate = this.getDistance(playerCoordinate, teammateCoordinate);

            // Adjust the probabilities of a successfully pass based on distance to teammate

            if (distToTeammate <= 2)
            {
                result[0] = this.updateWithSkill(9, passingSkill);
                result[1] = 1;
            }

            if (distToTeammate <= 3)
            {
                result[0] = this.updateWithSkill(8, passingSkill);
                result[1] = 2;
            }

            if (distToTeammate <= 4)
            {
                result[0] = this.updateWithSkill(7, passingSkill);
                result[1] = 3;
            }

            else
            {
                result[0] = this.updateWithSkill(5, passingSkill);
                result[1] = 5;
            }

            return result;
        }

        public int failPassBallLocation(int teammatePositionId, int[] teamLocationIds)
        {
            // When a pass fails, returns the location that the ball ends up
            // determined with respect to the location of the target teammate

            int[] teammateCoordinate = this.getPlayerCoordinate(teammatePositionId, teamLocationIds);
            Random rnd = new Random();

            int x = rnd.Next(teammateCoordinate[0] - 2, teammateCoordinate[0] + 1);
            x = Math.Min(x, this.fieldMaxX);
            x = Math.Max(x, this.fieldMinX);

            int y = rnd.Next(teammateCoordinate[1] - 1, teammateCoordinate[1] + 1);
            y = Math.Min(y, this.fieldMaxY);
            y = Math.Max(y, this.fieldMinY);

            int[] newCoordinate = new int[] { x, y };
            return this.convertToLocationId(newCoordinate);
        }

        public int[] probShoot(int playerPositionId, int[] teamLocationIds)
        {
            // Given the player's decision to shoot the ball, 
            // calculate the probability of success
            // Probabilities: [SUCCESS, FAIL]

            int[] result = {0, 0};

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            int shootingSkill = this.playerStats[playerPosition][Skill.Shooting];
            double distToGoal = this.getDistance(playerCoordinate, this.goalCoordinate);

            // Adjust the probabilities of a successfully pass based on distance to goal

            if (distToGoal == 0)
            {
                result[0] = this.updateWithSkill(9, shootingSkill);
                result[1] = 1;
            }

            if (distToGoal <= 1)
            {
                result[0] = this.updateWithSkill(8, shootingSkill);
                result[1] = 2;
            }

            if (distToGoal <= 2)
            {
                result[0] = this.updateWithSkill(7, shootingSkill);
                result[1] = 3;
            }

            if (distToGoal <= 3)
            {
                result[0] = this.updateWithSkill(5, shootingSkill);
                result[1] = 5;
            }

            if (distToGoal <= 4)
            {
                result[0] = this.updateWithSkill(3, shootingSkill);
                result[1] = 7;
            }

            else
            {
                result[0] = 0;
                result[1] = 1;
            }

            return result;
        }

        public int failGoalBallLocation()
        {
            // When a goal attempt fails, returns the location that the ball ends up
            // determined with respect to the location of the goalpost

            Random rnd = new Random();
            int x = rnd.Next(0, 3);
            int y = rnd.Next(0, 7);

            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            return newLocationId;
        }

        public int[] probAdvance(int playerPositionId, int[] teamLocationIds)
        {
            // Given teammate with possession of the ball, 
            // returns the probabilities of advancing in different directions
            // Probabilities: [UP, RIGHT, DOWN, RIGHT, STAY]

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);
            
            // Move in the direction of the goal
            return this.moveTowards((int)playerPosition, playerCoordinate, this.goalCoordinate);
        }

        public int[] probMove(int playerPositionId, int[] teamLocationIds, int ballLocationId)
        {
            // Given no possession of the ball, 
            // returns the probabilities of moving in different directions
            // Probabilities: [UP, RIGHT, DOWN, RIGHT, STAY]

            int playerLocationId = teamLocationIds[playerPositionId];
            int[] playerCoordinate = this.convertToCoordinate(playerLocationId);
            int[] ballCoordinate = this.convertToCoordinate(ballLocationId);

            // Move in the direction of the ball
            return this.moveTowards(playerPositionId, playerCoordinate, ballCoordinate);
        }

        public int[] moveTowards(int playerPositionId, int[] playerCoordinate, int[] targetCoordinate)
        {

            // Given a player, current coordinate and a target coordinate, 
            // returns the probabilities of moving in different directions
            // Probabilities: [UP, RIGHT, DOWN, RIGHT, STAY]

            int[] result = {0, 0, 0, 0, 0};

            // Player Position
            Position playerPosition = (Position)playerPositionId;

            // Boundaries for player
            int minX = this.playerBoundaries[playerPosition][Boundary.minX];
            int maxX = this.playerBoundaries[playerPosition][Boundary.maxX];
            int minY = this.playerBoundaries[playerPosition][Boundary.minY];
            int maxY = this.playerBoundaries[playerPosition][Boundary.maxY];

            // Speed of the player
            int speedSkill = this.playerStats[playerPosition][Skill.Speed];

            //  Target is up
            if (targetCoordinate[1] > playerCoordinate[1]) { result[0] = 1; }
            // Target is right 
            if (targetCoordinate[0] > playerCoordinate[0]) { result[1] = 1; }
            // Target is down
            if (targetCoordinate[1] < playerCoordinate[1]) { result[2] = 1; }
            // Target is left
            if (targetCoordinate[0] < playerCoordinate[0]) { result[3] = 1; }

            // Player cannot go up
            if (playerCoordinate[1] == maxY) { result[0] = 0; }
            // Player cannot go right
            if (playerCoordinate[0] == maxX) { result[1] = 0; }
            // Player cannot go down
            if (playerCoordinate[1] == minY) { result[2] = 0; }
            // Player cannot go left
            if (playerCoordinate[0] == minX) { result[3] = 0; }

            // Update based on speed
            result[0] = this.updateWithSkill(result[0], speedSkill);
            result[1] = this.updateWithSkill(result[1], speedSkill);
            result[2] = this.updateWithSkill(result[2], speedSkill);
            result[3] = this.updateWithSkill(result[3], speedSkill);

            // If no direction is viable, stay at current location
            int resultSum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                resultSum += result[i];
            }
            if (resultSum == 0)
            {
                result[4] = 1;
            }
            
            return result;
        }

        public int[] playerMoveUp(int playerPositionId, int[] teamLocationIds)
        {
            // Player moves up. Returns the new teamLocationIds.

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            int maxY = this.playerBoundaries[playerPosition][Boundary.maxY];

            int x = playerCoordinate[0];
            int y = Math.Min(playerCoordinate[1] + 1, maxY);
            
            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            
            teamLocationIds[playerPositionId] = newLocationId;

            return teamLocationIds;
        }

        public int[] playerMoveRight(int playerPositionId, int[] teamLocationIds)
        {

            // Player moves right. Returns the new teamLocationIds.

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            int maxX = this.playerBoundaries[playerPosition][Boundary.maxX];

            int x = Math.Min(playerCoordinate[0] + 1, maxX);
            int y = playerCoordinate[1];
            
            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            
            teamLocationIds[playerPositionId] = newLocationId;

            return teamLocationIds;
        }

        public int[] playerMoveDown(int playerPositionId, int[] teamLocationIds)
        {
            // Player moves down. Returns the new teamLocationIds.

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            int minY = this.playerBoundaries[playerPosition][Boundary.minY];

            int x = playerCoordinate[0];
            int y = Math.Max(playerCoordinate[1] - 1, minY);
            
            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            
            teamLocationIds[playerPositionId] = newLocationId;

            return teamLocationIds;
        }

        public int[] playerMoveLeft(int playerPositionId, int[] teamLocationIds)
        {

            // Player moves left. Returns the new teamLocationIds.

            Position playerPosition = (Position)playerPositionId;
            int[] playerCoordinate = this.getPlayerCoordinate(playerPositionId, teamLocationIds);

            int minX = this.playerBoundaries[playerPosition][Boundary.minX];

            int x = Math.Max(playerCoordinate[0] - 1, minX);
            int y = playerCoordinate[1];
            
            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            
            teamLocationIds[playerPositionId] = newLocationId;
            return teamLocationIds;
        }

        public int ballMoveUp(int ball)
        {
            // Ball moves up. Returns the new ball locationId.

            int[] ballCoordinate = this.convertToCoordinate(ball);
            
            int x = ballCoordinate[0];
            int y = Math.Min(ballCoordinate[1] + 1, this.fieldMaxY);

            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            return newLocationId;
        }

        public int ballMoveRight(int ball)
        {
            // Ball moves right. Returns the new ball locationId.

            int[] ballCoordinate = this.convertToCoordinate(ball);
            
            int x = Math.Min(ballCoordinate[0] + 1, this.fieldMaxX);
            int y = ballCoordinate[1];

            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            return newLocationId;
        }

        public int ballMoveDown(int ball)
        {
            // Ball moves down. Returns the new ball locationId.

            int[] ballCoordinate = this.convertToCoordinate(ball);
            
            int x = ballCoordinate[0];
            int y = Math.Max(ballCoordinate[1] - 1, this.fieldMinY);

            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            return newLocationId;
        }

        public int ballMoveLeft(int ball)
        {
            // Ball moves left. Returns the new ball locationId.

            int[] ballCoordinate = this.convertToCoordinate(ball);
            
            int x = Math.Max(ballCoordinate[0] - 1, this.fieldMinX);
            int y = ballCoordinate[1];

            int[] newCoordinate = new int[] { x, y };
            int newLocationId = this.convertToLocationId(newCoordinate);
            return newLocationId;
        }

        public int getLocation(int playerPositionId, int[] teamA)
        {   
            // Returns the locationId of the player
            return teamA[playerPositionId];
        }

        public int[] getPlayerCoordinate(int playerPositionId, int[] teamLocationIds)
        {
            Position playerPosition = (Position)playerPositionId;
            int playerLocationId = teamLocationIds[playerPositionId];
            return this.convertToCoordinate(playerLocationId);
        }

        public double getDistance(int[] point1, int[] point2) 
        {
            // Returns the distance between two points
            return Math.Sqrt(Math.Pow(point1[0] - point2[0], 2) + Math.Pow(point1[1] - point2[1], 2));
        }

        private int updateWithSkill(int weight, int skill)
        {
            // Update a probability weight with therespective skill point
            double skillEffect = Convert.ToDouble(skill) / 50;
            return Convert.ToInt32(Math.Round(weight * skillEffect));
        }

        private int convertToLocationId(int[] coordinate)
        {
            // Converts xy-coordinate to locationId
            return coordinate[1] * 10 + coordinate[0]; 
        }

        private int[] convertToCoordinate(int locationId)
        {
            // Converts locationId to xy-coordinate
            int x = locationId % 10;
            int y = locationId / 10;
            return new int[] {x, y};
        }
    
    	public override string ToString()
        {
            return "";
        }

        public override ExpressionValue GetClone()
        {
            return this;
        }

        public override string ExpressionID
        {
            get {return ""; }
        }
    
    }
    
    public class GameManager : ExpressionValue
    {
        public GameManager() { }

        public int[] updatePossession(int[] possession, int ball, int[] teamA, int[] teamB) 
        {

            // The team that possesses the ball
            Team possessionTeam = (Team)possession[0];

            // The player that possesses the ball
            Position possessionPlayerPosition = (Position)possession[1];

            // Identify players in the same location as the ball
            List<Position> playersA = new List<Position>();
            List<Position> playersB = new List<Position>();
            
            for (int i = 0; i < 11; i++) {
                if (teamA[i] == ball) {
                    playersA.Add((Position)i);
                }
                if (teamB[i] == ball) {
                    playersB.Add((Position)i);
                }
            }

            // If no team possesses the ball - free ball
            if (possessionTeam != Team.A && possessionTeam != Team.B)
            {
                if (playersA.Count + playersB.Count == 0)
                {
                    // Posession remains - free ball
                    return possession;       
                } 

                Random rnd = new Random();
                int randNum = rnd.Next(0, playersA.Count + playersB.Count);

                if (randNum < playersA.Count)
                {
                    // A random player from Team A possess the ball
                    Position selected = playersA[randNum];
                    return new int[] { (int)Team.A, (int)selected };
                }
                else
                {
                    // A random player from Team A possess the ball
                    Position selected = playersB[randNum - playersA.Count];
                    return new int[] { (int)Team.B, (int)selected };
                }
            }

            // If the ball is possessed by the goalkeeper
            if (possessionPlayerPosition == Position.Goalkeeper)
            {
                // Possession remains
                return possession;
            }

            ManagerA managerA = new ManagerA();
            ManagerB managerB = new ManagerB();

            // If Team A possesses the ball 
            if (possessionTeam == Team.A) 
            {
                int ballHandlingSkill = managerA.getBallHandlingSkill((int)possessionPlayerPosition);
                foreach (Position position in playersB) {
                    int defenceSkill = managerB.getDefenceSkill((int)position);
                    if (defenceSkill > ballHandlingSkill) {
                        // If a player on Team B has high enough defence, Team B will possess the ball
                        return new int[] { (int)Team.B, (int)position };
                    } 
                }
                // Possession remains at Team A
                return possession;
            }

            // If Team B possesses the ball 
            if (possessionTeam == Team.B) 
            {
                int ballHandlingSkill = managerB.getBallHandlingSkill((int)possessionPlayerPosition);
                foreach (Position position in playersA) {
                    int defenceSkill = managerA.getDefenceSkill((int)position);
                    if (defenceSkill > ballHandlingSkill) {
                        // If a player on Team A has high enough defence, Team A will possess the ball
                        return new int[] { (int)Team.A, (int)position };
                    } 
                }
                // Possession remains at Team B
                return possession;
            }

            return possession;
        }
    
        public int getStartingBallLocation()
        {
            Random rnd = new Random();
            bool randBool = rnd.Next(0, 2) > 0;
            if (randBool)
            {
                return this.convertToLocationId(new int[] { 4, 3 });
            }
            else
            {
                return this.convertToLocationId(new int[] { 5, 3 });
            }
        }

        public int[] getStartingPossession()
        {
            return new int[] { -1, -1 };
        }

        private int convertToLocationId(int[] coordinate)
        {
            return coordinate[1] * 10 + coordinate[0]; 
        }
        
        public int[] getPossession(int teamId, int playerPositionId)
        {
            return new int[] { teamId, playerPositionId };
        }

        public override string ToString()
        {
            return "";
        }

        public override ExpressionValue GetClone()
        {
            return this;
        }

        public override string ExpressionID
        {
            get {return ""; }
        }

    }
}
