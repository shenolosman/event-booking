﻿using System.ComponentModel.DataAnnotations;

namespace EventiaWebapp.Models
{
    public class Attendee
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNummer { get; set; }
        public ICollection<Event> ListEvent { get; set; }
    }
}
