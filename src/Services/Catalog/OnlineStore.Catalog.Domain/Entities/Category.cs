﻿namespace OnlineStore.Catalog.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; }
        public Category? Partent { get; set; }

    }
}
