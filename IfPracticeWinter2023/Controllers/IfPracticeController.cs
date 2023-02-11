using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IfPracticeWinter2023.Controllers
{
    public class IfPracticeController : ApiController
    {
        //HTTP GET:localhost:xxxx/api/IfPractice/Welceom/ -> "Hello World!"
        [Route("api/IfPractice/Welcome")]
        [HttpGet]
        public string Welcome()
        {
            return "Hello World!";
        }

        /// <summary>
        /// The function lets us know the current season given the input temperature
        /// </summary>
        /// <param name="temperature">the input temperature in deg C</param>
        /// <returns>
        /// The given season considering the temperature
        /// -5 downwards -> winter
        /// -5 to 0 -> fall
        /// 0 to 10 -> spring
        /// 10 and above -> summer
        /// </returns>
        /// <example>
        /// GET: api/IfPractice/Weather/10 -> "Summer!"
        /// GET: api/IfPractice/Weather/-19 -> "Winter!"
        /// GET: api/IfPractice/Weather/4 -> "Spring!"
        /// GET: api/IfPractice/Weather/0 -> "Fall!"
        /// </example>
        [Route("api/IfPractice/Weather/{temperature}")]
        [HttpGet]
        public string Weather(int temperature)
        {
            if (temperature <= -5)
            {
                return "Winter!";
            }
            else if (temperature > -5 && temperature < 0)
            {
                return "Fall!";
            }
            else if (temperature >= 0 && temperature < 10)
            {
                return "Spring";
            }
            else
            {
                return "Summer!";
            }
        }

        /// <summary>
        /// The function calculate the total price of the order
        /// </summary>
        /// <param name="qty">the quantity of Tshirt</param>
        /// <param name="cost">the cost of Tshirt</param>
        /// <returns>
        /// total cost of the tshirts plus shipping fee if any
        /// Qty:9 Cost:10 -> 105
        /// Qty:10 Cost:5 -> 50       
        /// Qty:5 Cost:20 -> 100
        /// </returns>
        /// <example>
        /// GET: api/IfPractice/Weather/9/10 -> "Total price: 105"
        /// GET: api/IfPractice/Weather/10/5 -> "Total price: 50"
        /// GET: api/IfPractice/Weather/5/20 -> "Total price: 100"
        /// </example>
        [Route("api/IfPractice/Tshirt/{cost}/{qty}")]
        [HttpGet]
        public IEnumerable<int> Tshirt(int qty, int cost)
        {
            int price = qty * cost;
            int shipping = 15;

            if (qty >= 10 || price >= 100)
            {
                shipping = 0;
            }
            else
            {
                shipping = 15;
            }
            int total = price + shipping;
            return new int[] {cost, shipping, total};

        }
        /// <summary>
        /// This function return TRUE if Jenny has enough money to purchase the toy, FALSE if cannot.
        /// </summary>
        /// <param name="numberOfNickles">Number of Nickles</param>
        /// <param name="numberOfDimes">Number of Dimes</param>
        /// <param name="numberOfQuarters">Number of Quarters</param>
        /// <param name="numberOfLoonies">Number of Loonies</param>
        /// <param name="numberOfTwoonies">Number of Twoonies</param>
        /// <returns>
        /// 0/0/0/15/0 -> TRUE
        /// 20/0/0/1/1 -> FALSE
        /// 100/20/2/4/0 -> TRUE
        /// </returns>
        /// <example>
        /// GET api/IfPractice/CoinComputer/0/0/0/15/0	-> TRUE
        /// GET api/IfPractice/CoinComputer/20/0/0/1/1	-> FALSE
        /// GET api/IfPractice/CoinComputer/100/20/2/4/0	-> TRUE
        /// </example>
        [Route("api/IfPractice/CoinComputer/{numberOfNickles}/{numberOfDimes}/{numberOfQuarters}/{numberOfLoonies}/{numberOfTwoonies}")]
        [HttpGet]
        public Boolean CoinComputer(int numberOfNickles, int numberOfDimes, int numberOfQuarters, int numberOfLoonies, int numberOfTwoonies) 
        {
            double total = numberOfNickles * 0.05 + numberOfDimes * 0.1 + numberOfQuarters * 0.25 + numberOfLoonies * 1 + numberOfTwoonies * 2;
            if (total >= 10.5) 
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        [Route("api/IfPractice/PointQuadrant/{x}/{y}")]
        [HttpGet]
        public int PointQuadrant(int x, int y)
        {
            if (x > 0 && y > 0) 
            { 
                return 1;
            }
            else if (x > 0 && y < 0)
            {
                return 4;
            }
            else if (x < 0 && y > 0)
            {
                return 2;
            }
            else if (x < 0 && y < 0)
            {
                return 3;
            }
            else
            {
                return 0;
            }

        }
        [Route("api/IfPractice/LineQuadrant/{x1}/{y1}/{x2}/{y2}")]
        [HttpGet]
        public int LineQuadrant(int x1, int y1, int x2, int y2)
        {
            int pointQuadrant1 = PointQuadrant(x1, y1);
            int pointQuadrant2 = PointQuadrant(x2, y2);

            bool isXOpposite = (x1 > 0 && x2 < 0) || (x1 < 0 && x2 > 0);
            bool isYOpposite = (y1 > 0 && y2 < 0) || (y1 < 0 && y2 > 0);

            //no line?
            if (x1 == x2 && y1 == y2)
            {
                return 0;
            }
            else
            {
                //line travel on exactly the x-axis or y-axis
                if (pointQuadrant1 == 0 && pointQuadrant2 == 0)
                {
                    return 0;
                }
                //two points in the same quadrant?
                else if (pointQuadrant1 == pointQuadrant2 || (!isXOpposite && !isYOpposite))
                {
                    return 1;
                }
                //neighbors quadrants? 
                else if (Math.Abs(pointQuadrant1 - pointQuadrant2) != 2)
                {
                    return 2;
                }
                else
                {
                    //line travel through the origin?
                    if (-x1 == x2 && -y1 == y2)
                    {
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
                }
            }
        }
        [HttpGet]
        [Route("api/IfPractice/LineQuadrant2/{x1}/{y1}/{x2}/{y2}")]
        public int LineQuadrant2(int x1, int x2, int y1, int y2)
        {
            int totalQuadrants;

            //If both points are the same, this is not a valid line
            if (x1 == x2 && y1 == y2)
            {
                totalQuadrants = 0;
                //return totalQuadrants;
            }
            else
            {
                //Determines if the line is horizontal
                bool isLineHorizontal = y1 == y2;
                //Determines if the line is vertical
                bool isLineVertical = x1 == x2;
                //line must be diagonal if it is (NOT(vertical) AND NOT(horizontal))
                bool isLineDiagonal = !isLineVertical && !isLineHorizontal;

                //i.e. is the sign in front of x1 and x2 different? (x1>0 and x2<0) or (x1<0 and x2>0)
                bool isXOpposite = (x1 > 0 && x2 < 0) || (x1 < 0 && x2 > 0);
                //i.e. is the sign in front of y1 and y2 different? (y1>0 and y2<0) or (y1<0 and y2>0)
                bool isYOpposite = (y1 > 0 && y2 < 0) || (y1 < 0 && y2 > 0);


                // Note that the line cannot both be horizontal and vertical (no longer a line)
                // 3 possibilities: Horizontal, Vertical, Diagonal.

                // Line is horizontal
                if (isLineHorizontal)
                {
                    Debug.WriteLine("Line is Horizontal");
                    //Does the line travel on exactly the x-axis?
                    if (y1 == 0 && y2 == 0)
                    {
                        //If so, the line does not pass through any quadrants.
                        totalQuadrants = 0;
                        //return totalQuadrants;
                    }
                    //Are x1 and x2 on opposite sides of the y axis?
                    else if (isXOpposite)
                    {
                        // If so, the line passes through two quadrants. 
                        // either (3 and 4) or (1 and 2)
                        totalQuadrants = 2;
                        //return totalQuadrants;
                    }
                    else
                    {
                        // In all other situations, the line only passes through one quadrant.
                        // either 1 or 2 or 3, or 4
                        totalQuadrants = 1;
                        //return totalQuadrants;
                    }
                }
                //Line is vertical
                else if (isLineVertical)
                {
                    Debug.WriteLine("Line is Vertical.");
                    //Does the line travel on exactly the x-axis?
                    if (x1 == 0 && x2 == 0)
                    {
                        //If so, the line does not pass through any quadrants.
                        totalQuadrants = 0;
                        //return totalQuadrants;
                    }
                    //Are y1 and y2 on opposite sides of the x axis?
                    else if (isYOpposite)
                    {
                        // If so, the line passes through two quadrants.
                        // either (1 and 4) or (2 and 3)
                        totalQuadrants = 2;
                        //return totalQuadrants;
                    }
                    else
                    {
                        //In all other situations, the line only passes through one quadrant.
                        // either 1 or 2 or 3 or 4
                        totalQuadrants = 1;
                        //return totalQuadrants;
                    }
                }
                //Line must be diagonal.
                else
                {
                    Debug.WriteLine("Line is Diagonal");
                    // only compute slope if you know the line is diagonal!
                    // we know y2!=y1 and x2!=x1 so this will always produce a positive or negative number.
                    decimal slope = (y2 - y1) / (x2 - x1);

                    // Solve for "B", the y-intercept.
                    // y = m * x + b
                    // y1 = slope*x1 + b
                    // y1 - slope*x1 = b

                    //This could be solved by providing the points of x2 and y2 as well.
                    decimal yIntercept = y1 - slope * x1;

                    //Does the line intercept with the origin (0,0)?
                    //Does y=0 when x=0?
                    if (slope * 0 + yIntercept == 0)
                    {
                        // If so, the line passes through the origin
                        // Because the line is also diagonal, it can only pass through 2 quadrants
                        // Either (1 and 3) or (2 and 4)
                        totalQuadrants = 2;
                        //return totalQuadrants;
                    }
                    // Do the two points exist in the same quadrant?
                    // i.e. is x not opposite and is y not opposite?
                    // note: you could also use the "PointQuadrant" method here and compare the quadrants of each point.
                    else if (!isXOpposite && !isYOpposite)
                    {
                        Debug.WriteLine("both points are 'same' quadrant");
                        totalQuadrants = 1;
                        //return totalQuadrants;
                    }
                    //Meaning
                    // - Line is Diagonal
                    // - Line does NOT pass through origin
                    // - Both points exist in DIFFERENT quadrants
                    // - Does it elapse 2 quadrants or 3?
                    else
                    {
                        int point1Quadrant = PointQuadrant(x1, y1);
                        int point2Quadrant = PointQuadrant(x2, y2);


                        //if the difference is 1 or -1, the quadrants are adjacent (neighbors)
                        //if the difference is 2 or -2, the quadrants are opposites (non-adjacent)
                        int quadrantDifference = Math.Abs(point1Quadrant - point2Quadrant);

                        //Are the quadrants neighbors?
                        if (quadrantDifference != 2)
                        {
                            // if so, the line passes through 2 quadrants
                            // either (1 and 2) or (2 and 3) or (3 and 4) or (1 and 4) 
                            totalQuadrants = 2;
                            //return totalQuadrants;
                        }
                        //Are the quadrants opposite?
                        else
                        {
                            // if so, the line passes through 3 quadrants
                            // either (4 and 1 and 2) or (1 and 2 and 3) or (2 and 3 and 4) or (3 and 4 and 1)
                            totalQuadrants = 3;
                            //return totalQuadrants;
                        }
                    }
                }
            }

            // link to wolfram alpha searching "line segment (x1,y1),  (x2,y2)"
            string evidence = "https://www.wolframalpha.com/input/?i=line+segment+%28" + x1 + "%2C" + y1 + "%29%2C+%28" + x2 + "%2C" + y2 + "%29";

            return totalQuadrants;
        }
    }
}
