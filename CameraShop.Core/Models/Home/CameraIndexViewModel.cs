﻿namespace CameraShop.Core.Models.Home
{
    public class CameraIndexViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }

        public int Year { get; set; }
    }
}
