﻿namespace LMS.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ToUser { get; set; }
        public User User { get; set; }

    }
}