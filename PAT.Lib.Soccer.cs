using System;
using System.Collections.Generic;
using System.Text;
using PAT.Common.Classes.Expressions.ExpressionClass;

//the namespace must be PAT.Lib, the class and method names can be arbitrary
namespace PAT.Lib
{
    /// <summary>
    /// The math library that can be used in your model.
    /// all methods should be declared as public static.
    /// 
    /// The parameters must be of type "int", or "int array"
    /// The number of parameters can be 0 or many
    /// 
    /// The return type can be bool, int or int[] only.
    /// 
    /// The method name will be used directly in your model.
    /// e.g. call(max, 10, 2), call(dominate, 3, 2), call(amax, [1,3,5]),
    /// 
    /// Note: method names are case sensetive
    /// </summary>
    public class SoccerGame : ExpressionValue
    {
    	public int minX;
        public int maxX;
        public int minY;
        public int maxY;
    
    	public SoccerGame(int length, int width)
        {
            this.minX = 0;
            this.maxX = length - 1;
            this.minY = 0;
            this.maxY = width - 1;
        }
        
        public int[] ActionWithoutBallProb(int[] player, int[] ball) {
            // [Stay, Move]
            int[] result = {0, 0};
            if (player[0] == ball[0] && player[1] == ball[1]) {
                // Ball is here
                result[0] += 1;
            } else {
                // Ball is not here
                result[1] += 1;
            }
            return result;
        }
        
        public int[] MoveProb(int[] player, int[] ball) {
            return this.MoveTowards(player, ball);
    	}
        
        public int[] ActionWithBallProb(int[] player, int[] teammate, int[] goal) {
            
            // [Stay, Dribble, Pass, Shoot]
            int[] result = {0, 0, 0, 0};
            
            double distToGoal = this.getDistance(player, goal);
            double teammateDistToGoal = this.getDistance(teammate, goal);

            if (distToGoal <= 1)
            {
                // Shoot only
                result[3] = 1;
                return result;
            }

            else if (distToGoal <= 2)
            {
                if (distToGoal <= teammateDistToGoal)
                {
                    // Shoot only
                    result[3] = 1;
                    return result;
                } 
                else
                {
                    // Pass or Shoot
                    result[2] = 1;
                    result[3] = 1;
                    return result;
                }
            }

            else 
            {
                if (distToGoal <= teammateDistToGoal)
                {
                    // Dribble only
                    result[1] = 1;
                    return result;
                } 
                else
                {
                    // Dribble or Pass
                    result[1] = 1;
                    result[2] = 1;
                    return result;
                }
            }
        }
        
        public int[] DribbleProb(int[] player, int[] goal) {
            return this.MoveTowards(player, goal);
        }
        
        public int[] MayLoseBallProb(int skill) {
            // [keep ball, lose ball]
            int[] result = {0, 0};
            double skillEffect = Convert.ToDouble(skill) / 100;
            result[0] = Convert.ToInt32(Math.Round(10 * skillEffect));
            result[1] = Math.Max(0, 10 - result[0]);
            return result;
        }
    
        public int[] PassProb(int skill, int[] player, int[] teammate) {

            // [Success, Fail on Kick, Fail on Receive]
            int[] result = {0, 0, 0};
            double skillEffect = Convert.ToDouble(skill) / 100;
            double distToTeammate = this.getDistance(player, teammate);

            if (distToTeammate <= 1)
            {
                int successWeight = Convert.ToInt32(Math.Round(8 * skillEffect));
                result[0] = successWeight;
                result[1] = 1;
                result[2] = 1;
                return result;
            }

            else if (distToTeammate <= 2)
            {
                int successWeight = Convert.ToInt32(Math.Round(7 * skillEffect));
                result[0] = successWeight;
                result[1] = 1;
                result[2] = 2;
                return result;
            }

            else 
            {
                int successWeight = Convert.ToInt32(Math.Round(5 * skillEffect));
                result[0] = successWeight;
                result[1] = 2;
                result[2] = 3;
                return result;
            }
        }
        
        public int[] ShootProb(int skill, int[] player, int[] goal) 
        {
            // [Success, Fail on Kick, Fail at Goal]
            int[] result = {0, 0, 0};
            double skillEffect = Convert.ToDouble(skill) / 100;
            double distToGoal = this.getDistance(player, goal);

            if (distToGoal == 0) {
                int successWeight = Convert.ToInt32(Math.Round(9 * skillEffect));
                result[0] = successWeight;
                result[1] = 0;
                result[2] = 1;
                return result;
            }

            if (distToGoal <= 1) {
                int successWeight = Convert.ToInt32(Math.Round(7 * skillEffect));
                result[0] = successWeight;
                result[1] = 1;
                result[2] = 2;
                return result;
            }

            else if (distToGoal <= 2) {
                int successWeight = Convert.ToInt32(Math.Round(5 * skillEffect));
                result[0] = successWeight;
                result[1] = 2;
                result[2] = 3;
                return result;
            }

            else {
                int successWeight = Convert.ToInt32(Math.Round(2 * skillEffect));
                result[0] = successWeight;
                result[1] = 4;
                result[2] = 4;
                return result;
            }
        }
        
        public double getDistance(int[] p1, int[] p2) 
        {
            return Math.Sqrt(Math.Pow(p1[0] - p2[0], 2) + Math.Pow(p1[1] - p2[1], 2));
        }
        
        public int[] MoveTowards(int[] player, int[] target)
        {
             // [Up, Right, Down, Left]
            int[] result = {0, 0, 0, 0};

            //  Target is up
            if (target[1] > player[1]) { result[0] += 1; }
            // Target is right 
            if (target[0] > player[0]) { result[1] += 1; }
            // Target is down
            if (target[1] < player[1]) { result[2] += 1; }
            // Target is left
            if (target[0] < player[0]) { result[3] += 1; }

            // Player cannot go up
            if (player[1] == this.maxY) { result[0] = 0; }
            // Player cannot go right
            if (player[0] == this.maxX) { result[1] = 0; }
            // Player cannot go down
            if (player[1] == this.minY) { result[2] = 0; }
            // Player cannot go left
            if (player[0] == this.minX) { result[3] = 0; }
            
            return result;
        }

        /// <summary>
        /// Please implement this method to provide the string representation of the datatype
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
        }

        /// <summary>
        /// Please implement this method to return a deep clone of the current object
        /// </summary>
        /// <returns></returns>
        public override ExpressionValue GetClone()
        {
            return this;
        }

        /// <summary>
        /// Please implement this method to provide the compact string representation of the datatype
        /// </summary>
        /// <returns></returns>
        public override string ExpressionID
        {
            get 
            {
            	return ""; 
            }
        }
    }
}
