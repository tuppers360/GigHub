using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels.GigViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                //TODO: 
                //Expression<Func<GigsController, IActionResult>> update = (c => c.Update(this));
                //Expression<Func<GigsController, IActionResult>> update = (c => c.Update(this));
                //var action = (Id != 0) ? update : create;
                // return (action.Body as MethodCallExpression).Method.Name;
                return (Id != 0) ? "Update" : "Create";
            } 
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}
