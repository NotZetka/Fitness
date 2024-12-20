﻿namespace API.Data
{
    public class ExerciseTemplate : DbEntity
    {
        public int Sets { get; set; }
        public int FitnessPlanTemplateId { get; set; }
        public FitnessPlanTemplate FitnessPlanTemplate { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
