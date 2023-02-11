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
        //HTTP GET:localhost:xxxx/api/IfPractice/Welcome/ -> "Hello World!"
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
    }
}
