using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Team5_ConestogaVirtualGameStore.Models;

namespace Team5_ConestogaVirtualGameStore.Controllers
{
    public class ReviewController : Controller
    {
        private readonly CVGS_Context db;
        public ReviewController(CVGS_Context context)
        {
            db = context;
        }
        public IActionResult SetReview(int gameId, int rating, string description)
        {
            Review review = new Review();
            review.GameId = gameId;
            review.Rating = rating;
            review.Description = description;
            review.Pending = true;
            review.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            db.Review.Add(review);
            db.SaveChanges();

            return RedirectToAction("Details", "Game", new { id = gameId });
        }

        public IActionResult AcceptReview(int id, int gameId)
        {
            Review reviewToEdit = db.Review.Find(id);

            reviewToEdit.Pending = false;
            db.SaveChanges();

            return RedirectToAction("Details", "Game", new { id = gameId});
        }
        public IActionResult DeclineReview(int id, int gameId)
        {
            Review reviewToEdit = db.Review.Find(id);
            db.Remove(reviewToEdit);
            db.SaveChanges();

            return RedirectToAction("Details", "Game", new { id = gameId });
        }
    }
}
