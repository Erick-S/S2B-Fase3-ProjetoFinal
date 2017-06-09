using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobius.Extensions;
using Mobius.Models;

/*
 * Rating system:
 * - User has a rating which ranges from -100 to 100
 * - "Positive Actions¹" Add +1 to rating
 * - "Negative Actions²" Removes 1 from rating
 * 
 * ¹) -Successfuly selling an product and being rated well (between 3-5);
 *    -Donating an product after it's expiration date.
 *    
 * ²) -Selling an product and being poorly (between 0-2);
 *    -Canceling an product's offer (before or after it's expiration date).
 *    
 *  Extra: 
 *      *(Frequently) Answering questions while they are still relevant: +1
 *      *(Frequently) Answering questions while they aren't relevant: -1
 *      
 *  Issues:
 *      - User Rating: How to access it?
 *          -> Use MyProfile cast?
 *              -> Will it update the db?
 */

namespace Mobius.ActionFilters
{
    public class PlusRatingActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            var a = filterContext.HttpContext.User.Identity as MyProfile;
            a.Rating += 1;
        }
    }

    public class MinusRatingActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            var a = filterContext.HttpContext.User.Identity as MyProfile;
            a.Rating -= 1;
        }
    }
}